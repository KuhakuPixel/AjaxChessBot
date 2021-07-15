using System;
using System.Linq;

namespace AjaxChessBotHelperLib
{
    public class AjaxStringHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="charStart"></param>
        /// <param name="charEnd"></param>
        /// <param name="isInclusive">if true include the two char when returning that are the separator</param>
        /// <returns></returns>
        public static string GetStringBetweenTwoChar(string str, char charStart, char charEnd, bool isInclusive)
        {
            //bad args
            if (!str.Contains(charEnd) || !str.Contains(charStart))
            {
                throw new ArgumentException("str doesn't contain fromChar or toChar");
            }
            string strBetweenChar = "";
            for (int i = 0; i < str.Length; i++)
            {
                //found the content starting from this "fromChar"
                if (str[i] == charStart)
                {
                    for (int j = i; j < str.Length; j++)
                    {


                        //end
                        if (str[j] != charEnd)
                        {
                            if (str[j] == charStart && isInclusive)
                                strBetweenChar += str[j];
                            else if (str[j] != charStart)
                                strBetweenChar += str[j];
                        }
                        else if(!string.IsNullOrEmpty(strBetweenChar))
                        {
                            if (isInclusive)
                                strBetweenChar += str[j];
                            break;
                        }
                       



                    }
                    break;
                }
            }
            return strBetweenChar;
        }



    }
}
