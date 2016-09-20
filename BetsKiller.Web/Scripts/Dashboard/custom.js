jQuery(function ($) {

    /* ----------------------------------------------------------- */
    /*  SCROLL BUTTONS
    /* ----------------------------------------------------------- */

    //Check to see if the window is top if not then display button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('#scrollTop').fadeIn();
        } else {
            $('#scrollTop').fadeOut();
        }
    });

    //Click event to scroll to top
    $('#scrollTop').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800);
        return false;
    });
});