﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsKiller.API.Rotoworld.Helpers
{
    public class StringTransformator
    {
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ' || c == '/' || c == ':' || c == '-' || c == '?' || c == '=')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        } 
    }
}
