using OfxSharpLib;
using System;
using System.IO;
using System.Xml;

namespace OFXSharp
{
    public static class OFXHelperMethods
    {
        /// <summary>
        /// Converts string representation of AccountInfo to enum AccountInfo
        /// </summary>
        /// <param name="bankAccountType">representation of AccountInfo</param>
        /// <returns>AccountInfo</returns>
        public static BankAccountType GetBankAccountType(this string bankAccountType)
        {
            return (BankAccountType)Enum.Parse(typeof(BankAccountType), bankAccountType);
        }

        /// <summary>
        /// Flips date from YYYYMMDD to DDMMYYYY         
        /// </summary>
        /// <param name="date">Date in YYYYMMDD format</param>
        /// <returns>Date in format DDMMYYYY</returns>
        public static DateTime ToDate(this string date)
        {
            try
            {
                if (date.Length < 8)
                {
                    return new DateTime();
                }

                int dd;
                Int32.TryParse(date.Substring(6, 2),out dd);
                int mm;
                Int32.TryParse(date.Substring(4, 2),out mm);
                int yyyy;
                Int32.TryParse(date.Substring(0, 4), out yyyy);

                if (yyyy == 0 || mm == 0 || dd == 0)
                    return new DateTime(1, 1, 1);

                return new DateTime(yyyy, mm, dd);
            }
            catch
            {
                throw new OfxParseException("Unable to parse date");
            }
        }

        /// <summary>
        /// Returns value of specified node
        /// </summary>
        /// <param name="node">Node to look for specified node</param>
        /// <param name="xpath">XPath for node you want</param>
        /// <returns></returns>
        public static string GetValue(this XmlNode node, string xpath)
        {
            // workaround to search values on root node
            var fixedNode = new XmlDocument();
            fixedNode.Load(new StringReader(node.OuterXml));

            var tempNode = fixedNode.SelectSingleNode(xpath);
            return tempNode != null ? tempNode.FirstChild.Value : "";
        }
    }
}