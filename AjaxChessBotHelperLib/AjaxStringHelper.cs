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
        /// <param name="fromChar"></param>
        /// <param name="toChar"></param>
        /// <param name="isInclusive">if true include the two char when returning that are the separator</param>
        /// <returns></returns>
        public string GetStringBetweenTwoChar(string str,char fromChar,char toChar,bool isInclusive)
        {
            //bad args
            if (!str.Contains(toChar)||!str.Contains(fromChar))
            {
                throw new ArgumentException("str doesn't contain fromChar or toChar");
            }
            string strBetweenChar = "";
            for(int i = 0; i < str.Length; i++)
            {
                //found the content starting from this "fromChar"
                if (str[i] == fromChar)
                {
                    for(int j = i; j < str.Length; j++)
                    {
                        //end
                        if (str[j] == toChar)
                        {
                            if(isInclusive)
                                strBetweenChar += str[j];

                            break;
                        }
                        else
                        {
                            if (str[j] == fromChar && isInclusive)
                                strBetweenChar += str[j];
                            else if(str[j] != fromChar)
                                strBetweenChar += str[j];

                        }
                      
                    }
                    break;
                }
            }
            return strBetweenChar;
        }
        

        
    }
}
