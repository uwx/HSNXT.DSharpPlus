using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;

namespace HSNXT.ComLib.Feeds
{
    /// <summary>
    /// Helper class for build feeds.
    /// </summary>
    public class FeedBuilder
    {
        /// <summary>
        /// Build the feed using the IPublishable items.
        /// </summary>
        /// <param name="feedAuthor">The author of the feed.</param>
        /// <param name="feedTitle">The title of the feed</param>
        /// <param name="feedDescription">The description of the feed.</param>
        /// <param name="feedUrl">The url of the feed</param>
        /// <param name="posts">The feed entries.</param>
        public static string BuildRss(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts)
        {
            return BuildAsXml(feedAuthor, feedTitle, feedDescription, feedUrl, posts, feed => new Rss20FeedFormatter(feed));
        }


        /// <summary>
        /// Build the feed using the IPublishable items.
        /// </summary>
        /// <param name="feedAuthor">The author of the feed.</param>
        /// <param name="feedTitle">The title of the feed</param>
        /// <param name="feedDescription">The description of the feed.</param>
        /// <param name="feedUrl">The url of the feed</param>
        /// <param name="posts">The feed entries.</param>
        public static string BuildAtom(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts)
        {
            return BuildAsXml(feedAuthor, feedTitle, feedDescription, feedUrl, posts, feed => new Atom10FeedFormatter(feed));
        }


        /// <summary>
        /// Build the feed using the IPublishable items.
        /// </summary>
        /// <param name="feedAuthor">The author of the feed.</param>
        /// <param name="feedTitle">The title of the feed</param>
        /// <param name="feedDescription">The description of the feed.</param>
        /// <param name="feedUrl">The url of the feed</param>
        /// <param name="posts">The feed entries.</param>
        /// <param name="writer">The writer to write the feed to</param>
        public static void BuildRss(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts, TextWriter writer)
        {
            BuildAsXml(feedAuthor, feedTitle, feedDescription, feedUrl, posts, writer, feed => new Rss20FeedFormatter(feed));
        }


        /// <summary>
        /// Build the feed using the IPublishable items.
        /// </summary>
        /// <param name="feedAuthor">The author of the feed.</param>
        /// <param name="feedTitle">The title of the feed</param>
        /// <param name="feedDescription">The description of the feed.</param>
        /// <param name="feedUrl">The url of the feed</param>
        /// <param name="posts">The feed entries.</param>
        /// <param name="writer">The writer to write the feed to</param>
        public static void BuildAtom(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts, TextWriter writer)
        {
            BuildAsXml(feedAuthor, feedTitle, feedDescription, feedUrl, posts, writer, feed => new Atom10FeedFormatter(feed));
        }


        /// <summary>
        /// Build the feed using the IPublishable items.
        /// </summary>
        /// <param name="feedAuthor">The author of the feed.</param>
        /// <param name="feedTitle">The title of the feed</param>
        /// <param name="feedDescription">The description of the feed.</param>
        /// <param name="feedUrl">The url of the feed</param>
        /// <param name="posts">The feed entries.</param>
        public static SyndicationFeed Build(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts)
        {
            var feedUri = new Uri(feedUrl);
            var feed = new SyndicationFeed(feedTitle, feedDescription, feedUri, feedTitle, DateTime.Now);
            feed.Authors.Add(new SyndicationPerson(string.Empty, feedAuthor, string.Empty));
            IList<SyndicationItem> items = new List<SyndicationItem>();
            foreach (var post in posts)
            {
                var item = new SyndicationItem(post.Title, SyndicationContent.CreateHtmlContent(post.Description), null, post.GuidId, post.UpdateDate);
                item.Authors.Add(new SyndicationPerson(string.Empty, post.Author, string.Empty));
                items.Add(item);
            }
            feed.Items = items;
            return feed;
        }


        /// <summary>
        /// Builds as XML.
        /// </summary>
        /// <param name="feedAuthor">The feed author.</param>
        /// <param name="feedTitle">The feed title.</param>
        /// <param name="feedDescription">The feed description.</param>
        /// <param name="feedUrl">The feed URL.</param>
        /// <param name="posts">The posts.</param>
        /// <param name="formatFetcher">The format fetcher.</param>
        /// <returns></returns>
        public static string BuildAsXml(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts, 
            Func<SyndicationFeed, SyndicationFeedFormatter> formatFetcher)
        {
            var writer = new StringWriter();
            BuildAsXml(feedAuthor, feedTitle, feedDescription, feedUrl, posts, writer, formatFetcher);
            var xml = writer.ToString();
            return xml;
        }


        /// <summary>
        /// Builds as XML.
        /// </summary>
        /// <param name="feedAuthor">The feed author.</param>
        /// <param name="feedTitle">The feed title.</param>
        /// <param name="feedDescription">The feed description.</param>
        /// <param name="feedUrl">The feed URL.</param>
        /// <param name="posts">The posts.</param>
        /// <param name="twriter">The writer to write the feed to.</param>
        /// <param name="formatFetcher">The format fetcher.</param>
        /// <returns></returns>
        public static void BuildAsXml(string feedAuthor, string feedTitle, string feedDescription, string feedUrl, IList<IPublishable> posts,
            TextWriter twriter, Func<SyndicationFeed, SyndicationFeedFormatter> formatFetcher)
        {
            var feed = Build(feedAuthor, feedTitle, feedDescription, feedUrl, posts);
            var formatter = formatFetcher(feed);
            XmlWriter writer = new XmlTextWriter(twriter);
            formatter.WriteTo(writer);
            writer.Flush();
        }


        /// <summary>
        /// Builds as XML.
        /// </summary>
        /// <param name="feed">The feed.</param>
        /// <param name="twriter">The writer to write the feed to.</param>
        /// <param name="format">The format either "rss" | "atom"</param>
        /// <returns></returns>
        public static void Build(SyndicationFeed feed, TextWriter twriter, string format)
        {
            SyndicationFeedFormatter formatter = null;
            if (string.IsNullOrEmpty(format) || format == "rss")
                formatter = new Rss20FeedFormatter(feed);
            else
            {
                formatter = new Atom10FeedFormatter(feed);
            }
            XmlWriter writer = new XmlTextWriter(twriter);
            formatter.WriteTo(writer);
            writer.Flush();
        }
    }
}
