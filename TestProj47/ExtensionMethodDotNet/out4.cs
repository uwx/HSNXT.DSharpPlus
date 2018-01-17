using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json;
#if NetFX
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * NextSunday
 * Get's the date of the upcoming Sunday.
 * 
 * Author: Charles Cherry
 * Submitted on: 3/3/2011 4:08:54 PM
 * 
 * Example: 
 * var nextSunday = DateTime.Now.NextSunday();
 */

        public static DateTime NextSunday(this DateTime dt)
        {
            return new GregorianCalendar().AddDays(dt, -((int) dt.DayOfWeek) + 7);
        }


/*
 * ToList<T>(Func<object, T> func)
 * Converts an array of any type to List<T> passing a mapping delegate Func<object, T> that returns type T. If T is null, it will not be added to the collection. If the array is null, then a new instance of List<T>() is returned.
 * 
 * Author: James Levingston
 * Submitted on: 10/19/2010 1:05:15 AM
 * 
 * Example: 
 * --> Make another extension method

public static List<T> ToList<T>(this object[] items)
        {
            return items.ToList<T>(o => { return (T)o; });
        }

Reduces this : http://extensionmethod.net/Details.aspx?ID=351

To this:

public static List<T> EnumToList<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).ToList<T>(enumVal => { return (T)Enum.Parse(typeof(T), enumVal.ToString()); });
        }

--> Use in Linq

var myItems = from i in array.ToList<MyType>( o => { return (MyType)o; }) select i;
 */

        public static List<T> ToList<T>(this Array items, Func<object, T> mapFunction)
        {
            if (items == null || mapFunction == null)
                return new List<T>();

            var coll = new List<T>();
            for (var i = 0; i < items.Length; i++)
            {
                var val = mapFunction(items.GetValue(i));
                if (val != null)
                    coll.Add(val);
            }
            return coll;
        }


/*
 * ElapsedSeconds
 * Gest the elapsed seconds since the input DateTime
 * 
 * Author: Jonnidip
 * Submitted on: 11/30/2010 12:29:54 PM
 * 
 * Example: 
 * DateTime dtStart = DateTime.Now;
// Do something
Double elapsed = dtStart.ElapsedSeconds();
 */

        /// <summary>
        /// Gest the elapsed seconds since the input DateTime
        /// </summary>
        /// <param name="input">Input DateTime</param>
        /// <returns>Returns a Double value with the elapsed seconds since the input DateTime</returns>
        /// <example>
        /// Double elapsed = dtStart.ElapsedSeconds();
        /// </example>
        /// <seealso cref="Elapsed()"/>
        public static double ElapsedSeconds(this DateTime input)
        {
            return DateTime.Now.Subtract(input).TotalSeconds;
        }

/*
 * IsNull
 * Essentially the implementation of the sql 'isnull' function, allowing the string type (when null) to be replaced with another value.
 * 
 * Author: Steve Mentzer
 * Submitted on: 10/20/2009 10:07:03 PM
 * 
 * Example: 
 * string myString = null;
string aSaferString = myString.IsNull(string.Empty);
 */

        /// <summary>
        /// If the string is null, converts it to the default string specified. Essentially the same as the SQL IsNull()
        /// function.
        /// </summary>
        /// <param name="inString"></param>
        /// <param name="defaultString"></param>
        /// <returns></returns>
        public static string IsNull(this string inString, string defaultString)
        {
            if (inString == null)
                return defaultString;
            else return inString;
        }

/*
 * GetOrThrow(string connectionStringName)
 * By default, ConfigurationManager.ConnectionStrings returns null if the requested connection string doesn't exist. Use this extension method if you want something a bit more snappy - an exception.
 * 
 * Author: Richard Dingwall
 * Submitted on: 12/9/2011 10:29:08 AM
 * 
 * Example: 
 * var connectionString = ConfigurationManager.ConnectionStrings.GetOrThrow("MyApp");
 */

        public static string GetOrThrow(this ConnectionStringSettingsCollection connectionStrings, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name was null or empty.", "name");

            var connectionString = connectionStrings[name];
            if (connectionString == null)
                throw new Exception($"The connection string '{name}' was not found.");

            return connectionString.ConnectionString;
        }

/*
 * ToBytes
 * Convert image to byte array
 * 
 * Author: Lucas
 * Submitted on: 3/7/2008 9:00:42 PM
 * 
 * Example: 
 * Image image = ...;
byte[] imageBytes = image.ToBytes(ImageFormat.Png);
 */

        public static byte[] ToBytes(this System.Drawing.Image image, ImageFormat format)
        {
            if (image == null)
                throw new ArgumentNullException("image");
            if (format == null)
                throw new ArgumentNullException("format");

            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }


/*
 * CombineWith()
 * Combines two strings (potentially each of them can be null) with an optional given separator the way you expect. Default separator is a single space.
 * 
 * Author: peSHIr
 * Submitted on: 5/6/2014 12:40:02 PM
 * 
 * Example: 
 * string firstName = "John";
string lastName = "Doe";
string fullName = firstName.CombineWith(lastName);
 */

        /// <summary>
        /// Combine two (optionally empty) strings the way you expect.
        /// </summary>
        /// <param name="input">First string to combine</param>
        /// <param name="suffix">Second string to append to <paramref name="input"/></param>
        /// <param name="separator">The separator to insert between <paramref name="input"/> and <paramref name="suffix"/> (default=a single space)</param>
        /// <returns>
        /// <c>"{input}{separator}{suffix}"</c> when both are not null/empty,
        /// <c>"{input}"</c> when <paramref name="suffix"/> is null/empty,
        /// <c>"{suffix}"</c> when <paramref name="input"/> is null/empty, or
        /// <c>string.Empty</c> when both are null/empty
        /// </returns>
        public static string CombineWith(this string input, string suffix, string separator = " ")
        {
            if (string.IsNullOrEmpty(input))
            {
                if (string.IsNullOrEmpty(suffix))
                {
                    return string.Empty;
                }
                else
                {
                    return suffix;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(suffix))
                {
                    return input;
                }
                else
                {
                    return $"{input}{separator}{suffix}";
                }
            }
        }

#if NetFX
/*
 * SelectItem
 * Select a item in a DropDownList by value.
 * 
 * Author: Carlos Alessandro Ribeiro
 * Submitted on: 7/21/2009 4:35:35 AM
 * 
 * Example: 
 * cbo.SelectItem("1");
 */

        /// <summary>
        /// Select a item in a DropDownList by informed value.
        /// </summary>
        /// <param name="ddl">DropDownList object.</param>
        /// <param name="value">Value to be selected.</param>
        public static void SelectItem(this DropDownList ddl, string value)
        {
            if (ddl.Items.FindByValue(value) != null)
                ddl.Items.FindByValue(value).Selected = true;
        }
#endif
        
/*
 * IsSingle
 * Determines whether the collection has exactly one element
 * 
 * Author: Brian Dukes
 * Submitted on: 10/21/2010 2:40:33 PM
 * 
 * Example: 
 * var items = new[] { 1, 2, 3 };
items.IsSingle(); // returns false
items.Take(1).IsSingle(); // returns true
new List<object>().IsSingle(); // returns false
 */

        /// <summary>
        /// Determines whether a sequence contains exactly one element.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/></typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to check for singularity.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="source"/> sequence contains exactly one element; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSingle<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            using (var enumerator = source.GetEnumerator())
            {
                return enumerator.MoveNext() && !enumerator.MoveNext();
            }
        }


/*
 * ShowWebPage
 * This extension method trigers the default navigator with the address pointed by the string
 * 
 * Author: Fernando Bravo
 * Submitted on: 9/15/2013 6:52:01 AM
 * 
 * Example: 
 * "http://www.trevoit.com".ShowWebPage();
 */

        public static void ShowWebPage(this string pAddress)
        {
            var procFormsBuilderStartInfo = new ProcessStartInfo();
            procFormsBuilderStartInfo.FileName = pAddress;
            procFormsBuilderStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            var procFormsBuilder = new Process();
            procFormsBuilder.StartInfo = procFormsBuilderStartInfo;
            procFormsBuilder.Start();
        }


/*
 * TryGetAttribute
 * Try/Get pattern for XDocument attributes
 * 
 * Author: Mike Cales
 * Submitted on: 9/18/2013 7:13:43 PM
 * 
 * Example: 
 * XAttribute cultureAttribute;
                if (element.TryGetAttribute("Culture", out cultureAttribute))
                {
//Do something with the attribute here
}
 */

        public static bool TryGetAttribute(this XElement element, XName attributeName, out XAttribute attribute)
        {
            attribute = element.Attribute(attributeName);
            return (attribute != null);
        }

/*
 * IsDerived
 * Checks whether the type is derived from specified type or implemented of specified interface.
 * 
 * Author: kevinjong
 * Submitted on: 3/24/2010 8:42:25 AM
 * 
 * Example: 
 * if(typeof(UserEntity).IsDerived<IExtend>())
{
	// do something..
}
 */

        public static bool IsDerived<T>(this Type type)
        {
            var baseType = typeof(T);

            if (baseType.FullName == type.FullName)
            {
                return true;
            }

            if (type.IsClass)
            {
                return baseType.IsClass
                    ? type.IsSubclassOf(baseType)
                    : baseType.IsInterface
                        ? IsImplemented(type, baseType)
                        : false;
            }
            else if (type.IsInterface && baseType.IsInterface)
            {
                return IsImplemented(type, baseType);
            }
            return false;
        }

        public static bool IsImplemented(Type type, Type baseType)
        {
            var faces = type.GetInterfaces();
            foreach (var face in faces)
            {
                if (baseType.Name.Equals(face.Name))
                {
                    return true;
                }
            }
            return false;
        }


/*
 * CountOf
 * Returns whether the sequence contains a certain amount of elements, without having to traverse the entire collection.
 * 
 * Author: Steven Jeuris
 * Submitted on: 11/15/2011 9:35:39 AM
 * 
 * Example: 
 * int[] bigCollection = new[] { 0, 1, 2, 3, 4, .... int.MaxValue };
bool hasTen = bigCollection.CountOf( 10 ); // true
 */

        /// <summary>
        ///   Returns whether the sequence contains a certain amount of elements.
        /// </summary>
        /// <typeparam name = "T">The type of the elements of the input sequence.</typeparam>
        /// <param name = "source">The source for this extension method.</param>
        /// <param name = "count">The amount of elements the sequence should contain.</param>
        /// <returns>True when the sequence contains the specified amount of elements, false otherwise.</returns>
        public static bool CountOf<T>(this IEnumerable<T> source, int count)
        {
            Contract.Requires(source != null);
            Contract.Requires(count >= 0);

            return source.Take(count + 1).Count() == count;
        }


/*
 * ToOracleSqlDate
 * Converts a Timestamp to a String which can be used in a Oracle SQL Query
 * 
 * Author: csonic
 * Submitted on: 1/19/2012 3:22:37 PM
 * 
 * Example: 
 * DateTime.Now.ToOracleSqlDate()
 */

        public static string ToOracleSqlDate(this DateTime dateTime)
        {
            return $"to_date('{dateTime.ToString("dd.MM.yyyy HH:mm:ss")}','dd.mm.yyyy hh24.mi.ss')";
        }


/*
 * Thread safe event raising
 * Allows thread-safely raise any event.
 * 
 * Author: Marcin Kozub
 * Submitted on: 9/27/2013 12:14:41 PM
 * 
 * Example: 
 * public event EventHandler<SomeEventArgs> SomeEvent;

SomeEvent.Raise(this, e);
 */

        public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
        {
            var handler = eventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

/*
 * ContainsSameKeys<>
 * Checks if the two dictionaries have the same keys
 * 
 * Author: mInternauta
 * Submitted on: 5/1/2016 12:24:57 AM
 * 
 * Example: 
 * [TestMethod]
        public void Test_ContainsSameKeys()
        {
            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            Dictionary<string, string> dic2 = new Dictionary<string, string>();

            dic1.Add("Key01", "Value1");
            dic1.Add("Key04", "Value4");
            dic1.Add("Key03", "Value3");
            dic1.Add("Key02", "Value2");

            dic2.Add("Key01", "Value1");
            dic2.Add("Key04", "Value4");
            dic2.Add("Key02", "Value2");
            dic2.Add("Key03", "Value3");
            dic2.Add("Key05", "Value5");

            Assert.IsTrue(dic1.ContainsSameKeys(dic2));
        }
 */

        /// <summary>
        /// Check if the two dictionaries have the same keys
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="from">Parent Dictionary</param>
        /// <param name="to">Child Dictionary</param>
        /// <returns></returns>
        public static bool ContainsSameKeys<TKey, TValue>(this IDictionary<TKey, TValue> from,
            IDictionary<TKey, TValue> to)
        {
            var search = to
                .Keys
                .Where(p => from.Keys.Contains(p));
            return (search.Count() == from.Count);
        }


/*
 * ToCSV
 * An extension method that produce a comman separated values of string out of an IEnumerable<T>. This would be useful if you want to automatically generate a CSV out of integer, string, or any other primative data type collection or array. I provided 2 overloads of this method. One of them accepts a separator and the other uses comma "," as default separator. Also I am using another shortcut extension method for foreach loop.
 * 
 * Author: Muhammad Mosa
 * Submitted on: 12/16/2009 11:40:16 AM
 * 
 * Example: 
 * [TestMethod]
public void ToCSV_Should_Return_Correct_Comma_Separated_Values()
{
    var values = new[] {1, 2, 3, 4, 5};

    string csv = values.ToCSV();

    Assert.AreEqual("1,2,3,4,5",csv);
}

[TestMethod]
public void ToCSV_Should_Return_Correct_Comma_Separated_Values_Using_Specified_Separator()
{
    var values = new[] { 1, 2, 3, 4, 5 };

    string csv = values.ToCSV(';');

    Assert.AreEqual("1;2;3;4;5", csv);
}
 */

        public static string ToCSV<T>(this IEnumerable<T> instance, char separator)
        {
            StringBuilder csv;
            if (instance != null)
            {
                csv = new StringBuilder();
                instance.Each(value => csv.AppendFormat("{0}{1}", value, separator));
                return csv.ToString(0, csv.Length - 1);
            }
            return null;
        }

        public static string ToCSV<T>(this IEnumerable<T> instance)
        {
            StringBuilder csv;
            if (instance != null)
            {
                csv = new StringBuilder();
                instance.Each(v => csv.AppendFormat("{0},", v));
                return csv.ToString(0, csv.Length - 1);
            }
            return null;
        }


/*
 * IfType
 * Execute code only on certain types
 * 
 * Author: Loek van den Ouweland
 * Submitted on: 9/5/2014 11:48:36 AM
 * 
 * Example: 
 * target.IfType<ItemDoesNotExistNavigationTarget>(x => ShowFeedback(x.Message));

the action is only executed when called on a certain type. It Replaces type checking AND casting like this:

if (target is ItemDoesNotExistNavigationTarget){
  ShowFeedback(((ItemDoesNotExistNavigationTarget).Message));
}
 */

        public static void IfType<T>(this object item, Action<T> action) where T : class
        {
            if (item is T)
            {
                action(item as T);
            }
        }

/*
 * Format
 * string formator,replece string.Format
 * 
 * Author: pedoc
 * Submitted on: 8/4/2015 9:45:05 AM
 * 
 * Example: 
 * string result = StrFormater.Format(@"Hello @Name! Welcome to C#!", new { Name = "World" });
 */
        static string GetValue(Match match, object data)
        {
            var paraName = match.Groups[1].Value;
            try
            {
                var proper = data.GetType().GetProperty(paraName);
                return proper.GetValue(data).ToString();
            }
            catch (Exception)
            {
                var errMsg = $"Not find'{paraName}'";
                throw new ArgumentException(errMsg);
            }
        }


/*
 * GetResponseWithoutException
 * Allow to get the HttpWebResponse event if the request wasn't successful, in order, for example, to know what went wrong
 * 
 * Author: Krimog
 * Submitted on: 3/1/2013 3:09:38 PM
 * 
 * Example: 
 * var response = request.GetResponseWithoutException();

if (response.StatusCode == HttpStatusCode.OK)
{
    // Request was successful
}
else if (response.StatusCode == HttpStatusCode.NotFound)
{

}
 */

        public static HttpWebResponse GetResponseWithoutException(this HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse) request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response == null)
                {
                    throw;
                }
                return response;
            }
        }


/*
 * AsDoesntThrow
 * Wraps an action with a try...catch of a specific exception
 * 
 * Author: ytoledano
 * Submitted on: 3/2/2013 2:09:59 PM
 * 
 * Example: 
 * var thisActionThrows = () => { throw new NullReferenceException() };
var thisDoesntThrow = thisActionThrows.AsDoesntThrow<NullReferenceException>();
 */

        public static Action AsDoesntThrow<T>(this Action action)
            where T : Exception
        {
            return (() =>
            {
                try
                {
                    action();
                }
                catch (T)
                {
                }
            });
        }


/*
 * Tail
 * Set the stream position to the place where for example 10 lines will be returned when read to end.
 * 
 * Author: smolesen
 * Submitted on: 12/29/2011 3:49:00 PM
 * 
 * Example: 
 * var s = File.OpenFile("text.txt");
s.Tail(10);
 */

        /// <summary>
        /// Set position to return n number of lines
        /// </summary>
        /// <param name="this"></param>
        /// <param name="n">number of lines</param>
        /// <returns>The stream with position set</returns>
        public static Stream Tail(this Stream @this, int n)
        {
            if (@this.Length == 0)
                return @this;
            @this.Seek(0, SeekOrigin.End);
            var count = 0;
            while (count <= n)
            {
                @this.Position--;
                var c = @this.ReadByte();
                @this.Position--;
                if (c == '\n')
                {
                    ++count;
                }
                if (@this.Position == 0)
                    break;
            }
            return @this;
        }


/*
 * Strip
 * Strips unwanted characters on the specified string.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 5/16/2008 4:30:02 PM
 * 
 * Example: 
 * string testString = "alpha129";
Console.WriteLine("Numeric Only: " + testString.Strip(@"[\D]"); // returns 129
Console.WriteLine("Alphabet Only: " + testString.Strip(@"[\d]"); // returns alpha
 */

        /// <summary>
        /// Strip unwanted characters and replace them with empty string
        /// </summary>
        /// <param name="data">the string to strip characters from.</param>
        /// <param name="textToStrip">Characters to strip. Can contain Regular expressions</param>
        /// <returns>The stripped text if there are matching string.</returns>
        /// <remarks>If error occurred, original text will be the output.</remarks>
        public static string Strip2(this string data, string textToStrip)
        {
            var stripText = data;

            if (string.IsNullOrEmpty(data)) return stripText;

            try
            {
                stripText = Regex.Replace(data, textToStrip, string.Empty);
            }
            catch
            {
            }
            return stripText;
        }

        /// <summary>
        /// Strips unwanted characters on the specified string
        /// </summary>
        /// <param name="data">the string to strip characters from.</param>
        /// <param name="textToStrip">Characters to strip. Can contain Regular expressions</param>
        /// <param name="textToReplace">the characters to replace the stripped text</param>
        /// <returns>The stripped text if there are matching string.</returns>
        /// <remarks>If error occurred, original text will be the output.</remarks>
        public static string Strip(this string data, string textToStrip, string textToReplace)
        {
            var stripText = data;

            if (string.IsNullOrEmpty(data)) return stripText;

            try
            {
                stripText = Regex.Replace(data, textToStrip, textToReplace);
            }
            catch
            {
            }
            return stripText;
        }


#if NetFX
/*
 * RemoveSelectedRows
 * Removes all selected rows from datagridview and returns the response on success
 * 
 * Author: Pursanov Dmitry
 * Submitted on: 5/20/2009 9:22:39 AM
 * 
 * Example: 
 * DataGridView dgvCaseList = new DataGridView();
dgvCase.RemoveSelectedRows();
 */

        public static bool RemoveSelectedRows(this DataGridView dgv)
        {
            try
            {
                if (dgv.RowCount > 0)
                {
                    if (dgv.SelectedRows.Count == dgv.Rows.Count)
                    {
                        dgv.DataSource = null;
                    }

                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        dgv.Rows.Remove(row);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
#endif

/*
 * ToDictionary() - for enumerations of groupings
 * Converts an IEnumerable<IGrouping<TKey,TValue>> to a Dictionary<TKey,List<TValue>> so that you can easily convert the results of a GroupBy clause to a Dictionary of Groupings. The out-of-the-box ToDictionary() LINQ extension methods require a key and element extractor which are largely redundant when being applied to an enumeration of groupings, so this is a short-cut.
 * 
 * Author: James Michael Hare (BlackRabbitCoder)
 * Submitted on: 10/21/2010 6:28:55 PM
 * 
 * Example: 
 * Dictionary<string,List<Product>> results = productList.GroupBy(product => product.Category).ToDictionary();
 */


/*
 * BeginningOfTheDay
 * Returns datetime corresponding to day beginning
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:49:17 PM
 * 
 * Example: 
 * var beginningOfTheDay = DateTime.Now.BeginningOfTheDay();
 */

        public static DateTime BeginningOfTheDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }


/*
 * Increment
 * Increments a integer number by one
 * 
 * Author: MrMostInteresting
 * Submitted on: 3/6/2015 10:15:59 AM
 * 
 * Example: 
 * 5.Increment(); // result should be six
 */
        public static int Increment(this int i)
        {
            return ++i;
        }


/*
 * DeleteFiles
 * Delete all files found on the specified folder with a given file extension.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 10:18:57 AM
 * 
 * Example: 
 * string path = @"C:\Temp\test\Sharp";
path.DeleteFiles("cs"); // Deletes all files with a CS extension
 */
        public static void DeleteFiles(this string folderPath, string ext)
        {
            var mask = "*." + ext;

            try
            {
                var fileList = Directory.GetFiles(folderPath, mask);

                foreach (var file in fileList)
                {
                    var fileInfo = new FileInfo(file);
                    fileInfo.Delete();
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Error Deleting file. Reason: {0}", ex.Message);
            }
        }


/*
 * IsNull
 * A better IsNull() implementation. Returns true if object value is null or DBNull
 * 
 * Author: Manos Aspradakis
 * Submitted on: 7/15/2011 11:32:17 AM
 * 
 * Example: 
 * string foo = null;
if(foo.IsNull())
{
  //...
}
 */

        public static bool IsDbNull(this object obj)
        {
            return obj == DBNull.Value || System.Convert.IsDBNull(obj);
        }


/*
 * DoubleBuffered
 * DoubleBuffer any control
 * 
 * Author: Paul Kemper
 * Submitted on: 9/4/2014 11:45:10 AM
 * 
 * Example: 
 * this.DoubleBuffered(true);
 */

        public static void DoubleBuffered<T>(this T obj, bool isDoubleBuffered)
        {
            var dgvType = obj.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.SetProperty);
            pi.SetValue(obj, isDoubleBuffered, null);
        }


/*
 * DeleteChars
 * Remove from the given string, all characters provided in a params array of chars.
 * 
 * Author: Érico Luis Barrientos Leite
 * Submitted on: 1/14/2012 3:33:29 PM
 * 
 * Example: 
 * string Text = "#Hello world.  This is a [test]";
string cleanText1 = Text.DeleteChars('#', '[', ']');  //return Hello world.  This is a test 
string cleanText2 = Text.DeleteChars('#');
//return Hello world.  This is a [test]
 */

        public static string DeleteChars(this string input, params char[] chars)
        {
            return new string(input.Where((ch) => !chars.Contains(ch)).ToArray());
        }


/*
 * ToArray
 * Returns an array of int containing all caracters that compose the number.
 * 
 * Author: waldyrfelix
 * Submitted on: 8/11/2009 3:48:03 AM
 * 
 * Example: 
 * int num = 87559;
int[] arr = num.ToArray();
 */

        public static int[] ToArray(this int number)
        {
            if (number == 0)
            {
                return new int[0];
            }
            else if (number < 0)
            {
                number = -1 * number;
            }

            var list = new List<int>();
            while (number > 0)
            {
                list.Add(number % 10);
                number = number / 10;
            }
            list.Reverse();

            return list.ToArray();
        }


/*
 * Length
 * Determines how many numbers compose the integer if it was represented as a string.
 * 
 * Author: Phil Campbell
 * Submitted on: 3/24/2010 6:03:52 PM
 * 
 * Example: 
 * int ir = 84372;

Console.WriteLine(ir.Length()); //will write 5
 */

        public static int Length(this int i)
        {
            if (i <= 0) throw new ArgumentOutOfRangeException();

            return (int) Math.Floor(Math.Log10(i)) + 1;
        }


/*
 * CleanBRTags
 * Remove HTML <br \> tags from the string
 * 
 * Author: unknown
 * Submitted on: 1/24/2008 7:12:36 PM
 * 
 * Example: 
 * .
 */
        public static string CleanBRTags(this string theString)
        {
            return theString.Replace(@"<br \>", "");
        }


/*
 * ResizeAndFit
 * This method resizes a System.Drawing.Image and tries to fit it in the destination Size. The source image size may be smaller or bigger then the target size. Source and target layout orientation can be different. ResizeAndFit tries to fit it the best it can.
 * 
 * Author: Loek van den Ouweland
 * Submitted on: 2/13/2009 12:51:14 PM
 * 
 * Example: 
 * Image image = Image.FromStream(context.Request.InputStream).ResizeAndFit(new Size( 125, 100));
 */

        public static System.Drawing.Image ResizeAndFit(this System.Drawing.Image image, Size newSize)
        {
            var sourceIsLandscape = image.Width > image.Height;
            var targetIsLandscape = newSize.Width > newSize.Height;

            var ratioWidth = (double) newSize.Width / (double) image.Width;
            var ratioHeight = (double) newSize.Height / (double) image.Height;

            var ratio = 0.0;

            if (ratioWidth > ratioHeight && sourceIsLandscape == targetIsLandscape)
                ratio = ratioWidth;
            else
                ratio = ratioHeight;

            var targetWidth = (int) (image.Width * ratio);
            var targetHeight = (int) (image.Height * ratio);

            var bitmap = new Bitmap(newSize.Width, newSize.Height);
            var graphics = Graphics.FromImage((System.Drawing.Image) bitmap);

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var offsetX = ((double) (newSize.Width - targetWidth)) / 2;
            var offsetY = ((double) (newSize.Height - targetHeight)) / 2;

            graphics.DrawImage(image, (int) offsetX, (int) offsetY, targetWidth, targetHeight);
            graphics.Dispose();

            return (System.Drawing.Image) bitmap;
        }



/*
 * FirstOrNull
 * Returns the null when there's null first element in the sequence instead of throwing an exception
 * 
 * Author: Jason Briggs
 * Submitted on: 6/30/2015 8:17:55 PM
 * 
 * Example: 
 * using System.Reflection;
// in this example the MemberInfo for this.Name will be returned or 
// null if "this" doesn't have a member called Name 
var memberInfo = this.GetType()
    .GetMembers()
    .Where(m => m.Name == "Name")
    .FirstOrNull();
 */

        public static T FirstOrNull<T>(this IEnumerable<T> values) where T : class
        {
            return values.DefaultIfEmpty(null).FirstOrDefault();
        }


/*
 * Dump
 * Dumps the object as a json string. Can be used for logging object contents.
 * 
 * Author: Vincent van Proosdij
 * Submitted on: 6/3/2015 1:47:43 PM
 * 
 * Example: 
 * var obj = new Test
{
  Name = "aa", Title = "tt"
}
var result = obj.Dump()
 */

        /// <summary>
        /// Dumps the object as a json string
        /// Can be used for logging object contents.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to dump. Can be null</param>
        /// <param name="indent">To indent the result or not</param>
        /// <returns>the a string representing the object content</returns>
        public static string Dump<T>(this T obj, bool indent = false)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
        }


/*
 * FormatSafe
 * Formats a string safely, without throwing any exceptions. Adds an exception message to the resulting string instead.
 * 
 * Author: Wayne Bloss
 * Submitted on: 10/14/2010 10:10:32 PM
 * 
 * Example: 
 * var message = "Hello, {0}!";
var formattedMessage = message.FormatSafe("World");
// formattedMessage now "Hello, World!".

// Error Example:
var message = "Hello, {1}!";
var formattedMessage = message.FormatSafe("World");
// formattedMessage now contains "Hello, {1}! (Invalid format or args!)".
 */
        /// <summary>
        /// Formats the string safely, without throwing any exceptions.
        /// </summary>
        /// <param name="format">The format source string to apply the given arguments to.</param>
        /// <param name="args">Arguments to apply to the format source string.</param>
        /// <returns>The formatted string. Null if the format source string was null.</returns>
        /// <remarks>
        /// Basic Example:
        /// <example><![CDATA[
        /// var message = "Hello, {0}!";
        /// var formattedMessage = message.FormatSafe("World");
        /// // formattedMessage now contains the string "Hello, World!".
        /// ]]></example>
        /// Error Example:
        /// <example><![CDATA[
        /// var message = "Hello, {1}!";
        /// var formattedMessage = message.FormatSafe("World");
        /// // formattedMessage now contains the string "Hello, {0}! (Invalid format or args!)".
        /// ]]></example>
        /// </remarks>
        public static string FormatSafe(this string format, params object[] args)
        {
            const string badFormat = " (Invalid format or args!)";

            if (format != null)
            {
                try
                {
                    return string.Format(format, args);
                }
                catch
                {
                    return format + badFormat;
                }
            }
            return null;
        }


/*
 * ThisWeekFriday
 * Returns a DateTime representing the Friday of the current week. Depends on System.Globalization.
 * 
 * Author: Charles Cherry
 * Submitted on: 8/31/2010 10:21:36 PM
 * 
 * Example: 
 * var friday = DateTime.Now.ThisWeekFriday();
 */

        public static DateTime ThisWeekFriday(this DateTime dt)
        {
            var today = DateTime.Now;
            return new GregorianCalendar().AddDays(today, -((int) today.DayOfWeek) + 5);
        }


/*
 * EnqueueAll
 * Enqueues all objects from an IEnumerable<T> to the specified queue.
 * 
 * Author: Virtlink
 * Submitted on: 2/2/2012 1:33:18 AM
 * 
 * Example: 
 * Queue<string> queue = new Queue<string>();
queue.EnqueueAll(new string[]{"A", "B", "C"});
 */

        /// <summary>
        /// Enqueues all the specified objects.
        /// </summary>
        /// <typeparam name="T">The type of objects in the queue.</typeparam>
        /// <param name="queue">The queue.</param>
        /// <param name="items">The items to enqueue.</param>
        public static void EnqueueAll<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (var item in items)
                queue.Enqueue(item);
        }


/*
 * TrimDuplicates
 * Trims or removes duplicate delimited characters and leave only one instance of that character. If you like to have a comma delimited value and you like to remove excess commas, this extension method is for you. Other characters are supported too, this includes pipe and colon.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 11/12/2010 7:12:13 AM
 * 
 * Example: 
 * static void Main(string[] args)
{
	string input = string.Empty;

	Console.WriteLine("Clean Commas");
	input = "justin,tesla,,nine,,,,,,elevate,,,joel";
	Console.WriteLine("INPUT: {0}\nOUTPUT: {1}\n", input, input.TrimDuplicates(Strings.TrimType.Comma));

	// OUTPUTS: justin,tesla,nine,elevate,joel

	Console.WriteLine("Clean Pipe");
	input = "justin|tesla||nine||||||elevate|||joel";
	Console.WriteLine("INPUT: {0}\nOUTPUT: {1}\n", input, input.TrimDuplicates(Strings.TrimType.Pipe));

	// OUTPUTS: justin|tesla|nine|elevate|joel

	Console.WriteLine("Clean Colon");
	input = "justin:tesla::nine::::::elevate:::joel";
	Console.WriteLine("INPUT: {0}\nOUTPUT: {1}\n", input, input.TrimDuplicates(Strings.TrimType.Colon));

	// OUTPUTS: justin:tesla:nine:elevate:joel

	Console.ReadLine();
}
 */

        public enum TrimType
        {
            Comma,
            Pipe,
            Colon
        }

        public static string TrimDuplicates(this string input, TrimType trimType)
        {
            var result = string.Empty;

            switch (trimType)
            {
                case TrimType.Comma:
                    result = input.TrimCharacter(',');
                    break;
                case TrimType.Pipe:
                    result = input.TrimCharacter('|');
                    break;
                case TrimType.Colon:
                    result = input.TrimCharacter(':');
                    break;
                default:
                    break;
            }

            return result;
        }

        private static string TrimCharacter(this string input, char character)
        {
            var result = string.Empty;

            result = string.Join(character.ToString(), input.Split(character)
                .Where(str => str != string.Empty)
                .ToArray());

            return result;
        }


/*
 * DoesNotEndWith
 * It returns true if string does not end with the character otherwise returns false. If you pass null or empty string, false will be returned.
 * 
 * Author: Omkar Panhalkar
 * Submitted on: 5/7/2012 3:54:40 AM
 * 
 * Example: 
 * Assert.IsTrue("Test".DoesNotEndWith('?'));
 */

        public static bool DoesNotEndWith(this string input, string pattern)
        {
            return string.IsNullOrEmpty(pattern) ||
                   input.IsNullOrEmptyOrWhiteSpace() ||
                   !input.EndsWith(pattern, StringComparison.CurrentCulture);
        }


/*
 * SkipLast
 * take all but the last item from an IEnumerable<T>
 * 
 * Author: Clinton Sheppard
 * Submitted on: 6/9/2009 8:31:20 PM
 * 
 * Example: 
 * List<int> items = new List<int>(new[]{ 1, 2, 3, 4, 5, 6});
foreach(int item in items.SkipLast())
{
  Console.WriteLine(item);
}

gets:
1
2
3
4
5
 */

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source)
        {
            if (!source.Any())
            {
                yield break;
            }
            var items = new Queue<T>();
            items.Enqueue(source.First());
            foreach (var item in source.Skip(1))
            {
                yield return items.Dequeue();
                items.Enqueue(item);
            }
        }


/*
 * ListFiles
 * List/Get all files in a specified folder using LINQ. Doesn't include sub-directory files.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 12:37:24 PM
 * 
 * Example: 
 * path = @"C:\temp";
List<string> files = path.ListFiles();
if(files != null)
{
	foreach (var file in files)
	{
		Console.WriteLine(file);
	}               
}
 */
        public static List<string> ListFiles(this string folderPath)
        {
            if (!Directory.Exists(folderPath)) return null;
            return (from f in Directory.GetFiles(folderPath)
                select Path.GetFileName(f)).ToList();
        }


/*
 * CopyToFile
 * Writes the specified StringBuilder to the file using the specified path. If the file already exists, it is overwritten.
 * 
 * Author: Érico Luis Barrientos Leite
 * Submitted on: 2/5/2012 11:40:05 PM
 * 
 * Example: 
 * new StringBuilder("Hello World!!!").CopyToFile(@"c:\helloworld.txt");
 */

        public static void CopyToFile(this StringBuilder sBuilder, string path)
        {
            File.WriteAllText(path, sBuilder.ToString());
        }

/*
 * Upgrade
 * Upgrades a hashtable to a generic dictionary
 * 
 * Author: C.F.Meijers
 * Submitted on: 12/11/2007 3:58:06 PM
 * 
 * Example: 
 * Hashtable t1 = new Hashtable();
 t1.Add("pi", Math.PI);
 t1.Add("e", Math.E);

 var col = t1.Upgrade<string, double>();
 */

        /// <summary>
        /// Converts a hashtable to a generic ditionary
        /// </summary>
        /// <typeparam name="TKey">The type of the key in the hashtable</typeparam>
        /// <typeparam name="TValue">The type of the object in the hashtable</typeparam>
        /// <param name="t">The hashtable</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Upgrade<TKey, TValue>(this Hashtable t)
        {
            var dic = new Dictionary<TKey, TValue>();

            foreach (DictionaryEntry entry in t)
            {
                dic.Add((TKey) entry.Key, (TValue) entry.Value);
            }

            return dic;
        }


/*
 * GetPermutations
 * GetPermutations
 * 
 * Author: Fons Sonnemans
 * Submitted on: 9/3/2013 11:51:53 AM
 * 
 * Example: 
 * var l = new List<int> { 1, 2, 3, 4 };

foreach (var item in l.GetPermutations()) {

    foreach (var value in item) {
        Console.Write(value);
        Console.Write(" ");
    }
    Console.WriteLine();
}
 */

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> items)
        {
            if (items.Count() > 1)
            {
                return items.SelectMany(item => GetPermutations(items.Where(i => !i.Equals(item))),
                    (item, permutation) => new[] {item}.Concat(permutation));
            }
            else
            {
                return new[] {items};
            }
        }


/*
 * GzipString
 * if you want to lost wight of string , you can use gzip
 * 
 * Author: Phakawan Wongpetanan
 * Submitted on: 1/23/2015 5:27:18 AM
 * 
 * Example: 
 * string zipstring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".CompressString();

string unzipstring = zipstring.DecompressString();
 */

        public static string CompressString(this string text)
        {
            var buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            System.Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return System.Convert.ToBase64String(gZipBuffer);
        }

        public static string DecompressString(string compressedText)
        {
            var gZipBuffer = System.Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                var dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

/*
 * ToString(NullOptions)
 * This ToString() version is null aware. That means it has different behaviors if the object's value is null or DBNull according to the NullOptions enum.
 * 
 * Author: Hadi Lababidi
 * Submitted on: 2/19/2010 1:48:43 PM
 * 
 * Example: 
 * object obj = DBNull.Value;
            MessageBox.Show(obj.ToString(NullOptions.ConvertAllNullsToEmpty));
            MessageBox.Show(obj.ToString(NullOptions.ConvertDbNullsToNulls));
 */

        public enum NullOptions
        {
            ConvertDbNullsToNulls,
            ConvertAllNullsToEmpty
        }

        public static string ToString(this object obj, NullOptions nullOption)
        {
            var isDbNull = false;
            var isNull = false;

            if (obj.GetType() == typeof(DBNull))
                isDbNull = true;

            if (obj == null)
                isNull = true;

            if ((isNull || isDbNull) == false)
                return obj.ToString();

            if (nullOption == NullOptions.ConvertDbNullsToNulls)
            {
                return null;
            }
            else
            {
                return string.Empty;
            }
        }


/*
 * ToException
 * String Typed Exception Extension
 * 
 * Author: Ferhat Celik
 * Submitted on: 6/9/2015 7:02:52 AM
 * 
 * Example: 
 * "ferhat".ToException<FileNotFoundException>();
"ferhat".ToException<Exception>();
"ferhat".ToException<IOException>();
 */

        public static void ToException<T>(this string message) where T : Exception, new()
        {
            var e = (T) Activator.CreateInstance(typeof(T), message);
            throw e;
        }


/*
 * RenameColumn
 * Rename a code that allows only stating the current name column and a new name.
 * 
 * Author: Raphael Augusto Ferroni Cardoso
 * Submitted on: 6/4/2014 8:49:05 PM
 * 
 * Example: 
 * // Consider that dataSetName is the DataSet that contains a DataTable
dataSetName.Tables[0].RenameColumn("Column1", "Name");
dataSetName.Tables[0].RenameColumn("Column2", "Email");
dataSetName.Tables[0].RenameColumn("Column3", "RegistrationDate");
 */

        public static void RenameColumn(this System.Data.DataTable dt, string oldName, string newName)
        {
            if (dt != null && !string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName) && oldName != newName)
            {
                var idx = dt.Columns.IndexOf(oldName);
                dt.Columns[idx].ColumnName = newName;
                dt.AcceptChanges();
            }
        }


/*
 * IsMatch
 * Matches yourFace to myButt
 * 
 * Author: jdoe
 * Submitted on: 8/31/2010 10:26:34 PM
 * 
 * Example: 
 * var billysFace = new Object();
var myButt = new Object();

Console.WriteLine( billysFace.IsMatch(myButt) ); //true
 */

        public static bool IsMatch(this object yourFace, object myButt)
        {
            return true;
        }


/*
 * UpdateCollection
 * Updates items from the collection using a modified version of this collection. Useful in MVVM scenarios needing cancellable edition and delayed persistence.
 * 
 * Author: Lionel Ringenbach
 * Submitted on: 10/13/2011 11:31:19 AM
 * 
 * Example: 
 * ObservableCollection<Uri> links = new ObservableCollection<Uri>(originalLinks)

...

// Do some operations on the collection

...

originalLinks.UpdateCollection(links);
 */

        public static void UpdateCollection<T>(this ICollection<T> originalCollection, ICollection<T> updatedCollection)
        {
            if (originalCollection.IsReadOnly)
            {
                throw new InvalidOperationException("Cannot update a read-only collection.");
            }

            var oldItems = new Collection<T>();
            var newItems = new Collection<T>();

            foreach (var originalItem in originalCollection)
            {
                if (!updatedCollection.Contains(originalItem))
                {
                    oldItems.Add(originalItem);
                }
            }

            foreach (var updatedItem in updatedCollection)
            {
                if (!originalCollection.Contains(updatedItem))
                {
                    newItems.Add(updatedItem);
                }
            }

            foreach (var oldItem in oldItems)
            {
                originalCollection.Remove(oldItem);
            }

            foreach (var newItem in newItems)
            {
                originalCollection.Add(newItem);
            }
        }


/*
 * DoesNotStartWith
 * It returns true if string does not start with the character otherwise returns false if you pass null or empty string, false will be returned.
 * 
 * Author: Omkar Panhalkar
 * Submitted on: 5/7/2012 3:48:35 AM
 * 
 * Example: 
 * Assert.IsTrue("Test".DoesNotStartWith('?'));
 */

        public static bool DoesNotStartWith(this string input, string pattern)
        {
            return string.IsNullOrEmpty(pattern) ||
                   input.IsNullOrEmptyOrWhiteSpace() ||
                   !input.StartsWith(pattern, StringComparison.CurrentCulture);
        }

/*
 * IfIs<T>
 * optionally executes an Action if the object is of the given type.
 * 
 * Author: dave thieben
 * Submitted on: 10/24/2010 8:41:48 PM
 * 
 * Example: 
 * item.IfIs<ILogicalDelete>( x => x.DeletedBy = User.Identity.Name );

var username = item.IfIs<IIdentity>( x => x.Name );
 */

        /// <summary>
        /// allows an action to be taken on an object if it is castable as the given type, with no return value.
        /// if the target does not match the type, does nothing
        /// </summary>
        public static void IfIs<T>(this object target, Action<T> method)
            where T : class
        {
            var cast = target as T;
            if (cast != null)
            {
                method(cast);
            }
        }

        /// <summary>
        /// allows an action to be taken on an object if it is castable as the given type, with a return value.
        /// if the target does not match the type, returns default(T)
        /// </summary>
        public static TResult IfIs<T, TResult>(this object target, Func<T, TResult> method)
            where T : class
        {
            var cast = target as T;
            if (cast != null)
            {
                return method(cast);
            }
            else
            {
                return default(TResult);
            }
        }


/*
 * IsValidCodeMelli
 * صحت کد ملی
 * 
 * Author: Mehdi Miri
 * Submitted on: 3/26/2013 5:30:09 AM
 * 
 * Example: 
 * string codeMelli = "4990003519";
bool isValid = codeMelli.IsValidCodeMelli();
 */

        public static bool IsValidCodeMelli(this string codeMelli)
        {
            long number;
            int sum = 0, temp;
            long.TryParse(codeMelli, out number);
            if (Math.Log10(number) > 6 && Math.Log10(number) < 10)
            {
                temp = System.Convert.ToInt16(number % 10);
                number /= 10;
                for (var i = 2; i < 11; i++)
                {
                    sum += System.Convert.ToInt16(i * (number % 10));
                    number /= 10;
                }
                if (((sum % 11 < 2) && (sum % 11 == temp)) || ((sum % 11 >= 2) && ((11 - sum % 11) == temp)))
                    return true;
            }
            return false;
        }


/*
 * SetSocketKeepAliveValues
 * Using IOControl code to configue socket KeepAliveValues for line disconnection detection(because default is toooo slow)
 * 
 * Author: [T] NepTunic
 * Submitted on: 3/22/2010 3:15:12 AM
 * 
 * Example: 
 * TcpClient t = new TcpClient();
t.SetSocketKeepAliveValues(300000, 1000);
 */

        /// <summary>
        /// Using IOControl code to configue socket KeepAliveValues for line disconnection detection(because default is toooo slow) 
        /// </summary>
        /// <param name="tcpc">TcpClient</param>
        /// <param name="KeepAliveTime">The keep alive time. (ms)</param>
        /// <param name="KeepAliveInterval">The keep alive interval. (ms)</param>
        public static void SetSocketKeepAliveValues(this TcpClient tcpc, int KeepAliveTime, int KeepAliveInterval)
        {
            //KeepAliveTime: default value is 2hr
            //KeepAliveInterval: default value is 1s and Detect 5 times

            uint dummy = 0; //lenth = 4
            var inOptionValues =
                new byte[System.Runtime.InteropServices.Marshal.SizeOf(dummy) * 3]; //size = lenth * 3 = 12
            var OnOff = true;

            BitConverter.GetBytes((uint) (OnOff ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint) KeepAliveTime).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint) KeepAliveInterval).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);
            // of course there are other ways to marshal up this byte array, this is just one way
            // call WSAIoctl via IOControl

            // .net 1.1 type
            //int SIO_KEEPALIVE_VALS = -1744830460; //(or 0x98000004)
            //socket.IOControl(SIO_KEEPALIVE_VALS, inOptionValues, null); 

            // .net 3.5 type
            tcpc.Client.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }


/*
 * ToSpecificCurrencyString
 * Convert a double to a string formatted using the culture settings (string representation) passed into the procedure.
 * 
 * Author: Inge Schepers
 * Submitted on: 12/15/2007 1:10:30 PM
 * 
 * Example: 
 * double test = 120.45;
string testString = test.ToSpecificCurrencyString("en-US");
 */

        /// <summary>
        /// Format a double using specific culture currency settings.
        /// </summary>
        /// <param name="value">The double to be formatted.</param>
        /// <param name="cultureName">The string representation for the culture to be used, for instance "en-US" for US English.</param>
        /// <returns>The double formatted based on specific culture currency settings.</returns>
        public static string ToSpecificCurrencyString(this double value, string cultureName)
        {
            var currentCulture = new CultureInfo(cultureName);
            return (string.Format(currentCulture, "{0:C}", value));
        }


/*
 * RandomElements
 * Returns a number of random elements from a collection
 * 
 * Author: Timmy Kokke
 * Submitted on: 3/9/2011 9:47:45 AM
 * 
 * Example: 
 * //Get 4 random elements
IEnumerable<int> seq = Enumerable.Range(1, 50);
seq.RandomElements(4).ToList().ForEach(Console.WriteLine);

// randomize all (note: is slow on large collection!)
IEnumerable<int> seq2 = Enumerable.Range(1, 50);
seq2.RandomElements().ToList().ForEach(Console.WriteLine);
 */

        public static IEnumerable<T> RandomElements<T>(this IEnumerable<T> collection, int count = 0)
        {
            if (count > collection.Count() || count <= 0)
                count = collection.Count();

            var usedIndices = new List<int>();
            var random = new Random((int) DateTime.Now.Ticks);
            while (count > 0)
            {
                var index = random.Next(collection.Count());
                if (!usedIndices.Contains(index))
                {
                    yield return collection.ElementAt(index);
                    usedIndices.Add(index);
                    count--;
                }
            }
        }


/*
 * RemoveAtFast
 * Fast version of the RemoveAt function. Overwrites the element at the specified index with the last element in the list, then removes the last element, thus lowering the inherent O(n) cost to O(1). IMPORTANT: Intended to be used on *unordered* lists only.
 * 
 * Author: José María Calvo Iglesias
 * Submitted on: 6/5/2009 1:21:46 PM
 * 
 * Example: 
 * using ListExtender;

void Test()
{
    List<int> intList = new List<int>( 10000 );

    // Populate list
    for( int n = 0; n < 10000; ++n )
    {
        intList.Add( n );
    }

    // Remove all elements. Note that not a single memory 
    // xfer will be performed since internally we're 
    // always removing the last element regardless the 
    // index we passed in.
    for( int n = 0; n < 10000; ++n )
    {
        intList.RemoveAtFast( 0 );
    }
}
 */

        /// <summary>
        /// Fast version of the RemoveAt function. Overwrites the element at the specified index
        /// with the last element in the list, then removes the last element, thus lowering the 
        /// inherent O(n) cost to O(1). Intended to be used on *unordered* lists only.
        /// </summary>
        /// <param name="_list">List.</param>
        /// <param name="_index">Index of the element to be removed.</param>
        public static void RemoveAtFast<T>(this IList<T> _list, int _index)
        {
            _list[_index] = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
        }


/*
 * Upgrade
 * Upgrades an ArrayList to a generic List
 * 
 * Author: C.F.Meijers
 * Submitted on: 12/11/2007 4:04:58 PM
 * 
 * Example: 
 * ArrayList l = new ArrayList();
 l.Add("a");
 l.Add("b");

 var list = l.Upgrade<string>();
 */

        /// <summary>
        /// Converts an arraylist to a generic List
        /// </summary>
        /// <typeparam name="T">The type of the elements in the arraylist</typeparam>
        /// <param name="l">The arraylist</param>
        /// <returns></returns>
        public static List<T> Upgrade<T>(this ArrayList l)
        {
            return l.Cast<T>().ToList();
        }


/*
 * EnqueueWithCapacity
 * Sometimes you may need a Queue<T> that, once it hits a capacity, dequeues items automatically to maintain a certain maximum. While it may be best to derive a new type from Queue<T>, this will get it done much more quickly. This is very useful for maintaining a rolling average or a "history" feature.
 * 
 * Author: David Harris
 * Submitted on: 8/21/2010 1:02:41 AM
 * 
 * Example: 
 * Queue<int> q = new Queue<int>();
q.EnqueueWithCapacity(0, 3); // {0}
q.EnqueueWithCapacity(1, 3); // {0, 1}
q.EnqueueWithCapacity(2, 3); // {0, 1, 2}
q.EnqueueWithCapacity(3, 3); // {1, 2, 3}
 */

        public static void EnqueueWithCapacity<T>(this Queue<T> q, T item, int MaxSize)
        {
            if (q.Count >= MaxSize) q.Dequeue();
            q.Enqueue(item);
        }


/*
 * ConcatTo
 * Adds a single element at the beginning of an enumerator
 * 
 * Author: B.W. Kemps
 * Submitted on: 8/22/2011 9:39:07 PM
 * 
 * Example: 
 * IEnumerable<String> x = new[]{"foo", "bar"};
x = x.ConcatTo("bla");
Console.WriteLine(string.Join('--',x));

// returns bla--foo--bar
 */

        static IEnumerable<T> ConcatTo<T>(IEnumerable<T> target, T element)
        {
            return new[] {element}.Concat(target);
        }


/*
 * LoadBitmapFromResource
 * Create new Bitmap from resource image
 * 
 * Author: Algel
 * Submitted on: 12/16/2009 3:56:15 PM
 * 
 * Example: 
 * Bitmap bitmap = Assembly.GetExecutingAssembly().LoadBitmapFromResource("Resources.Progress.gif");
 */

        public static Bitmap LoadBitmapFromResource(this Assembly assembly, string imageResourcePath)
        {
            var stream = assembly.GetManifestResourceStream(imageResourcePath);
            return stream != null ? new Bitmap(stream) : null;
        }

/*
 * Product
 * Computes a product of all elements in the sequence.
 * 
 * Author: Edmondo Pentangelo
 * Submitted on: 2/4/2009 11:44:44 PM
 * 
 * Example: 
 * Func<int,int> fac = n => Enumerable.Range(1, n).Product();
 */

        public static int Product(this IEnumerable<int> values)
        {
            return values.Aggregate((a, b) => a * b);
        }


/*
 * EnsureFileNameIsUnique
 * Ensures given file name will return a unique file name, using the format Filename - Copy, or Filename - Copy (n) where n > 1
 * 
 * Author: John Cornell
 * Submitted on: 6/5/2014 12:59:02 AM
 * 
 * Example: 
 * //Move file to new location
string fileToMove = @"C:/myfiletomove.txt";
string newLocation = Path.Combine(directoryName, fileName);

//Ensure if File Exists, then File.Move will use a unique name
//In the Windows "FileName - Copy [(#)]" format
File.Move(fileToMove, newLocation.EnsureFileNameIsUnique());
 */

        public static string EnsureFileNameIsUnique(this string intendedName)
        {
            if (!File.Exists(intendedName)) return intendedName;

            var file = new FileInfo(intendedName);
            var extension = file.Extension;
            var basePath = intendedName.Substring(0, intendedName.Length - extension.Length);

            var counter = 1;

            var newPath = "";

            do
            {
                newPath = $"{basePath} - Copy{(counter == 1 ? "" : $" ({counter})")}{extension}";

                counter += 1;
            } while (File.Exists(newPath));

            return newPath;
        }


/*
 * Randomize
 * Randomize the Items in the list
 * 
 * Author: Ramaraju
 * Submitted on: 7/30/2009 11:22:12 AM
 * 
 * Example: 
 * List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            intList.Add(4);
            intList.Add(5);
intList.Randomize(); // this will randomize the items in the list
 */

        public static void Randomize<t>(this IList<t> target)
        {
            var RndNumberGenerator = new Random();
            var newList = new SortedList<int, t>();
            foreach (var item in target)
            {
                newList.Add(RndNumberGenerator.Next(), item);
            }
            target.Clear();
            for (var i = 0; i < newList.Count; i++)
            {
                target.Add(newList.Values[i]);
            }
        }


/*
 * FolderSize
 * Using LINQ, gets the total size of a specified folder. It can also check sizes of subdirectory under it as a parameter.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/28/2008 6:20:07 AM
 * 
 * Example: 
 * path = @"D:\INCOMING\Books";
var size = new DirectoryInfo(path).FolderSize(true);
Console.WriteLine("Size of {0} is {1}", path, size);

// you can format the size return by using my other extension method, FormatSize()
// var size = new DirectoryInfo(path).FolderSize(true).FomatSize();
 */
        public static long FolderSize(this DirectoryInfo dir, bool bIncludeSub)
        {
            long totalFolderSize = 0;

            if (!dir.Exists) return 0;

            var files = from f in dir.GetFiles()
                select f;
            foreach (var file in files) totalFolderSize += file.Length;

            if (bIncludeSub)
            {
                var subDirs = from d in dir.GetDirectories()
                    select d;
                foreach (var subDir in subDirs) totalFolderSize += FolderSize(subDir, true);
            }

            return totalFolderSize;
        }

#if NetFX
/*
 * FindControlsByType
 * Used in conjunction with GetChildren(), it will return a list of T from a list of children of a control. Get Children is located at: http://www.extensionmethod.net/Details.aspx?ID=309
 * 
 * Author: Ryan Helms
 * Submitted on: 7/18/2010 12:02:26 AM
 * 
 * Example: 
 * List<TextBox> textboxes = Control.FindControlsByType<TextBox>();
 */

        public static List<T> FindControlsByType<T>(this Control ctrl)
        {
            return ctrl.GetChildren().OfType<T>().ToList();
        }
#endif

/*
 * JSON to XML
 * json to xml
 * 
 * Author: Keyur Panchal
 * Submitted on: 8/11/2015 10:53:52 AM
 * 
 * Example: 
 * {"menu": {
  "id": "file",
  "value": "File",
  "popup": {
    "menuitem": [
      {"value": "New", "onclick": "CreateNewDoc()"},
      {"value": "Open", "onclick": "OpenDoc()"},
      {"value": "Close", "onclick": "CloseDoc()"}
    ]
  }
}}
 */

//// Install nuget package for Newtonsoft json
//// https://www.nuget.org/packages/Newtonsoft.Json/
//// Install-Package Newtonsoft.Json

        public static string ToXML(this string json)
        {
            // To convert JSON text contained in string json into an XML node
            var doc = JsonConvert.DeserializeXmlNode(json);
            return doc.ToString();
        }
    }
}