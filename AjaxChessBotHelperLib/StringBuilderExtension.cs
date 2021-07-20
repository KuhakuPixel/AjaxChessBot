using System;
using System.Collections.Generic;
using System.Text;

namespace AjaxChessBotHelperLib
{
    public static class StringBuilderExtension
    {
        public static List<string> ToList(this StringBuilder stringBuilder )
        {
            string[] strArr = stringBuilder.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<string> splittedStr = new List<string>();

            for(int i = 0; i < strArr.Length; i++)
            {
                if (!string.IsNullOrEmpty(strArr[i]))
                {
                    splittedStr.Add(strArr[i]);
                }
            }

            return splittedStr;
        }
    }
}
