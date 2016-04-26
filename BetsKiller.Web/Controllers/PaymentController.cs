using BetsKiller.BL.Payment;
using BetsKiller.Helper.Constants;
using BetsKiller.Helper.Operations;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BetsKiller.Web.Controllers
{
    public class PaymentController : Controller
    {
        /// <summary>
        /// When PayPal gets a purchase with the IPN set up we get a POST with a bunch of variables.
        /// If all is good we verify by calling back (GET) with the same parameters, we get a "VERIFIED" response.
        /// We then in turn respond with a 200, all is OK.
        /// </summary>
        /// <returns>200 if all is good.</returns>
        /// <remarks>
        /// https://developer.paypal.com/webapps/developer/docs/classic/ipn/integration-guide/IPNIntro/
        /// </remarks>
        [AllowAnonymous]
        public ActionResult PaypalIpn()
        {
            StringBuilder sb = new StringBuilder();
            string baseUrl = "https://www.paypal.com/cgi-bin/webscr?cmd=_notify-validate";
            string optionName = "";
            string optionEmail = "";
            bool isPaymentCompleted = false;
            bool isTest = false;
            decimal mcFee = 0;
            decimal mcGross = 0;

            // build a list of the keys
            foreach (string key in Request.Form.AllKeys)
            {
                string value = Request.Form.Get(key);
                string encoded = HttpUtility.UrlEncode(value);

                sb.Append("&");
                sb.Append(key);
                sb.Append("=");
                sb.Append(encoded);

                if (String.IsNullOrEmpty(key))
                {
                    continue;
                }

                if (key.Equals("option_selection1", StringComparison.InvariantCultureIgnoreCase))
                {
                    optionName = value;
                }
                if (key.Equals("option_selection2", StringComparison.InvariantCultureIgnoreCase))
                {
                    optionEmail = HttpUtility.UrlDecode(value);
                }
                else if (key.Equals("test_ipn", StringComparison.InvariantCultureIgnoreCase) &&
                  value == "1")
                {
                    // test_ipn=1
                    baseUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_notify-validate";
                    isTest = true;
                }
                else if (key.Equals("payment_status", StringComparison.InvariantCultureIgnoreCase) &&
                     "Completed".Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    // payment_status=Refunded OR payment_status=Completed
                    isPaymentCompleted = true;
                }
                else if (key.Equals("mc_fee", StringComparison.InvariantCultureIgnoreCase))
                {
                    mcFee = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                }
                else if (key.Equals("mc_gross", StringComparison.InvariantCultureIgnoreCase))
                {
                    mcGross = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                }
            }

            LogManager.GetLogger(LoggerConst.PAYMENT).Trace("[RECEIVED REQUEST]\r\n" + sb.ToString());

            var url = baseUrl + sb;
            var ipnResponse = ExecuteIpnResponse(url);

            LogManager.GetLogger(LoggerConst.PAYMENT).Trace("[RECEIVED IPN RESPONSE]\r\n" + ipnResponse);

            if (ipnResponse.Equals("VERIFIED", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!isTest)
                {
                    if (String.IsNullOrEmpty(optionName))
                    {
                        LogManager.GetLogger(LoggerConst.PAYMENT).Trace("[NO PURCHASE TO MATCH]\r\n" + sb.ToString());
                    }

                    if (isPaymentCompleted == false)
                    {
                        LogManager.GetLogger(LoggerConst.PAYMENT).Trace("[PAYMENT SHOULD BE COMPLETED]\r\n" + sb.ToString());
                    }

                    if (isPaymentCompleted)
                    {
                        ReceivePayment.PaymentTypeEnum paymentType;

                        if (optionName == "Premium")
                        {
                            paymentType = ReceivePayment.PaymentTypeEnum.Premium;
                        }
                        else // optionName == "Ultimate"
                        {
                            paymentType = ReceivePayment.PaymentTypeEnum.Ultimate;
                        }

                        ReceivePayment receivePayment = new ReceivePayment(optionEmail, paymentType, mcGross - mcFee, sb.ToString());
                        receivePayment.Start();

                        LogManager.GetLogger(LoggerConst.PAYMENT).Info("[PURCHASE COMPLETED]\r\n" + sb.ToString());
                    }
                }
                else
                {
                    LogManager.GetLogger(LoggerConst.PAYMENT).Info("[THIS WAS ONLY TEST PAYMENT]");
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            LogManager.GetLogger(LoggerConst.PAYMENT).Error("[PAYPAL IPN FAILED]\r\n" + sb.ToString());
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        #region Method Helpers

        protected virtual string ExecuteIpnResponse(string url)
        {
            try
            {
                return InternetRequest.GetResponse(url);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(LoggerConst.PAYMENT).Error(ex, "[IPN RESPONSE ERROR]");
                return string.Empty;
            }
        }

        #endregion
    }
}