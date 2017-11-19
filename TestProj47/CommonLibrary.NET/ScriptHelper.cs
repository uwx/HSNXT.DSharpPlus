using System;
using System.Xml;

namespace HSNXT.ComLib.Automation
{
    /// <summary>
    /// Contains helper methods for script
    /// </summary>
    class ScriptHelper
    {
        /// <summary>
        /// Tries to load the content as an xml document.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static BoolMessageItem<XmlDocument> LoadXml(string content)
        {
            var doc = new XmlDocument();
            var success = true;
            var message = string.Empty;
            try
            {
                doc.LoadXml(content);
            }
            catch (Exception ex)
            {
                success = false;
                message = "Unable to load xml : " + ex.Message;
            }
            return new BoolMessageItem<XmlDocument>(doc, success, message);
        }
    }
}
