using System;
using System.Linq;

namespace AjaxChessBotHelperLib
{
    public class AjaxStringHelper
    {
        /// <summary>
        /// Will return the first occurance of element that are between charStart and charEnd
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
                        if (str[j] != charEnd)
                        {
                            if (str[j] == charStart && isInclusive)
                                strBetweenChar += str[j];
                            else if (str[j] != charStart)
                                strBetweenChar += str[j];
                        }
                        //end
                        else if (!string.IsNullOrEmpty(strBetweenChar))
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

        /// <summary>
        /// startIndex is inclusive
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="subString"></param>
        /// <returns></returns>
        public static bool IsSubStringInTheFirstSubStringOfString(string str,int startIndex,string subString)
        {
            if (startIndex >= str.Length)
            {
                throw new IndexOutOfRangeException("startIndex is out of range "+"startIndex = "+startIndex.ToString()+",length of str = "+str.Length);
            }
            else if (subString.Length>str.Length-startIndex)
            {
                throw new IndexOutOfRangeException("subString.Length +startIndex is bigger than  str.Length");
            }
            else if (startIndex < 0)
            {
                throw new IndexOutOfRangeException("startIndex is less than 0 ,startIndex ="+startIndex.ToString());
            }
            for(int i = 0; i < subString.Length; i++)
            {
                if ( subString[i]!=str[i+startIndex])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
