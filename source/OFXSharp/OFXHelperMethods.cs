using System;
using System.IO;
using System.Text.RegularExpressions;
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
				string resultDate = null;

				if (IsDateInOFXDefaultEspecification(date, out resultDate))
				{
					var dd = Int32.Parse(resultDate.Substring(6, 2));
					var mm = Int32.Parse(resultDate.Substring(4, 2));
					var yyyy = Int32.Parse(resultDate.Substring(0, 4));

					return new DateTime(yyyy, mm, dd);
				}

				return new DateTime();
			}
			catch
			{
				throw new OFXParseException("Unable to parse date");
			}
        }

		/// <summary>
		/// Check if date this in YYYYMMDD format.
		/// </summary>
		/// <param name="date">String containing date.</param>
		/// <returns>Returns true if format is valid.</returns>
		public static bool IsDateInOFXDefaultEspecification(string date, out string result)
		{
			Regex regex = new Regex(@"^\d{4}((0\d)|(1[012]))(([012]\d)|3[01])");
			bool isDateInOFXFormat = regex.IsMatch(date);

			if (isDateInOFXFormat)
				result = regex.Match(date).Value;
			else
				result = null;

			return isDateInOFXFormat;
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