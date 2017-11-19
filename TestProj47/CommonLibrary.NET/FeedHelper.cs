using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace HSNXT.ComLib.Feeds
{
    /// <summary>
    /// Feed format.
    /// </summary>
    public enum FeedFormat
    {   
        /// <summary>
        /// Atom syndication format.
        /// </summary>
        Atom, 
        

        /// <summary>
        /// Rss syndication format
        /// </summary>
        Rss
    }


    /// <summary>
    /// 
    /// </summary>
    public struct FeedFormatDocType
    {
        /// <summary>
        /// Document type for rss 
        /// </summary>
        public const String Rss20 = "application/rss+xml";


        /// <summary>
        /// Document type for atom 
        /// </summary>
        public const String Atom10 = "application/atom+xml";
    }



    /// <summary>
    /// Helper class for reading/writing feeds.
    /// </summary>
    public static class FeedHelper
    {
        /// <summary>
        /// Load a syndication from the specified url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static SyndicationFeed LoadUrl(String url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException("Url must be supplied.");

            SyndicationFeed feed = null;
            try
            {                
                using (var reader = new XmlTextReader(url))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch(Exception){}
            return feed;
        }



        /// <summary>
        /// Loads the URL items Title and Link into a list of key/values.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="maxEntries">The max entries.</param>
        /// <returns></returns>
        public static List<KeyValue<string, string>> LoadUrlItemsTitle(string url, int maxEntries)
        {
            var feed = LoadUrl(url);
            if (feed == null || feed.Items == null || feed.Items.Count() == 0)
                return new List<KeyValue<string, string>>();

            var result = new List<KeyValue<string, string>>();
            foreach(var item in feed.Items)
            {
                var entry = new KeyValue<string, string>(item.Title.Text, item.Links[0].Uri.AbsoluteUri);
                result.Add(entry);

                if (result.Count == maxEntries)
                    break;
            }
            return result;
        }



        /// <summary>
        /// Load feed from url into an xml string.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static String LoadUrlAsXml(String url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException("Url must be supplied.");

            var feed = LoadUrl(url);

            // Checks
            if (feed == null) return String.Empty;

            var format = FeedFormat.Rss;
            var buffer = new StringBuilder();
            using (var sw = new StringWriter(buffer))
            {
                using (var xmlwriter = new XmlTextWriter(sw))
                {
                    Write(feed, format, xmlwriter);
                }
            }

            var xml = buffer.ToString();
            return xml;
        }



        /// <summary>
        /// Writes the specified feed to either rss or atom format.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="format">The format.</param>
        /// <param name="writer">The writer.</param>
        public static void Write(SyndicationFeed feed, FeedFormat format, XmlWriter writer)
        {
            if (format == FeedFormat.Rss)
                WriteRss20(feed, writer);

            else
                WriteAtom10(feed, writer);
        }


        /// <summary>
        /// Writes the feed as atom10.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="stream">The stream.</param>
        public static void WriteAtom10(SyndicationFeed feed, Stream stream)
        {
            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                var formatter = new Atom10FeedFormatter(feed);
                formatter.WriteTo(writer);
            }
        }



        /// <summary>
        /// Writes the feed as RSS20.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="stream">The stream.</param>
        public static void WriteRss20(SyndicationFeed feed, Stream stream)
        {
            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                var formatter = new Rss20FeedFormatter(feed, false);
                formatter.WriteTo(writer);
            }
        }



        /// <summary>
        /// Writes the atom10.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="writer"></param>
        public static void WriteAtom10(SyndicationFeed feed, XmlWriter writer)
        {
            var formatter = new Atom10FeedFormatter(feed);
            formatter.WriteTo(writer);
        }



        /// <summary>
        /// Writes the RSS20.
        /// </summary>
        /// <param name="feed"></param>
        /// <param name="writer"></param>
        public static void WriteRss20(SyndicationFeed feed, XmlWriter writer)
        {
            var formatter = new Rss20FeedFormatter(feed, false);
            formatter.WriteTo(writer);
        }
    }
}
