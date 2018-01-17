using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
#if NetFX
using System.Data.Objects;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
#endif
    
namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * GetEnumDescription
 * Gets the description attribute assigned to an item in an Enum.
 * 
 * Author: Chirdeep Tomar
 * Submitted on: 4/20/2010 12:19:22 PM
 * 
 * Example: 
 * public enum EnumGrades
    {
        [Description("Passed")]
        Pass,
        [Description("Failed")]
        Failed,
        [Description("Promoted")]
        Promoted
    }

string description = EnumHelper<EnumGrades>.GetEnumDescription("pass");
 */

        public static class EnumHelper<T>
        {
            public static string GetEnumDescription(string value)
            {
                var type = typeof(T);
                var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase))
                    .Select(d => d).FirstOrDefault();

                if (name == null)
                {
                    return string.Empty;
                }
                var field = type.GetField(name);
                var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return customAttribute.Length > 0 ? ((DescriptionAttribute) customAttribute[0]).Description : name;
            }
        }


/*
 * OrderBy(string sortExpression)
 * Orders a list based on a sortexpression. Useful in object databinding scenarios where the objectdatasource generates a dynamic sortexpression (example: "Name desc") that specifies the property of the object sort on.
 * 
 * Author: C.F.Meijers
 * Submitted on: 4/25/2008 11:45:04 AM
 * 
 * Example: 
 * class Customer
{
  public string Name{get;set;}
}

var list = new List<Customer>();

list.OrderBy("Name desc");
 */

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            var parts = sortExpression.Split(' ');
            var descending = false;
            var property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                var prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return list.OrderByDescending(x => prop.GetValue(x, null));
                return list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }


/*
 * ConvertToByteArray
 * Convert a Stream to an array of bytes.
 * 
 * Author: Chuhukon
 * Submitted on: 2/13/2011 1:30:50 PM
 * 
 * Example: 
 * FileStream f = File.OpenRead(@"c:\testfile.txt");
byte[] b = f.ConvertToByteArray();
Console.WriteLine(b.Length);
 */

        /// <summary>
        /// This is a snippet by Chuhukon see:
        /// http://www.koodr.com/item/e207aa7f-0ff1-4e8a-a703-e96f2f175bc9
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(this Stream stream)
        {
            var streamLength = Convert.ToInt32(stream.Length);
            var data = new byte[streamLength + 1];

            //convert to to a byte array
            stream.Read(data, 0, streamLength);
            stream.Close();

            return data;
        }


/*
 * Replace
 * This extension method replaces an item in a collection that implements the ilist<t> interface
 * 
 * Author: Otto Beragg
 * Submitted on: 3/5/2008 3:20:09 PM
 * 
 * Example: 
 * List<string> strg = new List<string> { "test", "tesssssst2" };
strg.Replace(1,"test2");

foreach (string str in strg)
   Console.WriteLine(str);

Console.ReadKey();

Output : 
test
test2
 */

        /// <summary>
        /// This extension method replaces an item in a collection that implements the IList interface.
        /// </summary>
        /// <typeparam name="T">The type of the field that we are manipulating</typeparam>
        /// <param name="thisList">The input list</param>
        /// <param name="position">The position of the old item</param>
        /// <param name="item">The item we are goint to put in it's place</param>
        /// <returns>True in case of a replace, false if failed</returns>
        public static bool Replace<T>(this IList<T> thisList, int position, T item)
        {
            if (position > thisList.Count - 1)
                return false;
            // only process if inside the range of this list

            thisList.RemoveAt(position);
            // remove the old item
            thisList.Insert(position, item);
            // insert the new item at its position
            return true;
            // return success
        }


/*
 * ToXml
 * Converts entire DataTabel To XDocument
 * 
 * Author: Meysam Javadi
 * Submitted on: 10/31/2012 7:20:11 AM
 * 
 * Example: 
 * dtCert.ToXml("Certs")
 */

        public static XDocument ToXml(this DataTable dt, string rootName)
        {
            var xdoc = new XDocument
            {
                Declaration = new XDeclaration("1.0", "utf-8", "")
            };
            xdoc.Add(new XElement(rootName));
            foreach (DataRow row in dt.Rows)
            {
                var element = new XElement(dt.TableName);
                foreach (DataColumn col in dt.Columns)
                {
                    element.Add(new XElement(col.ColumnName, row[col].ToString().Trim(' ')));
                }
                if (xdoc.Root != null) xdoc.Root.Add(element);
            }

            return xdoc;
        }


/*
 * GetPropertyValue
 * Gets the value of a property in a object through relection
 * 
 * Author: Sanction10
 * Submitted on: 9/2/2010 2:00:10 PM
 * 
 * Example: 
 * user.GetPropertyValue<string>("Name");
 */

        public static T GetPropertyValue<T>(this object source, string property)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();

            var propertyValue = (from s in sourceProperties
                where s.Name.Equals(property)
                select s.GetValue(source, null)).FirstOrDefault();

            return propertyValue != null ? (T) propertyValue : default;
        }


/*
 * GetAttribute
 * Makes it easier to retrieve custom attributes of a given type from a reflected type.
 * 
 * Author: James Michael Hare (Black Rabbit Coder)
 * Submitted on: 10/14/2010 6:24:27 PM
 * 
 * Example: 
 * var attribute = typeof(SomeType).GetAttribute<XmlSerializationAttribute>();
 */

        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>The custom attribute of type T, if found.</returns>
        public static T GetAttribute<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            return GetAttributes<T>(typeWithAttributes).FirstOrDefault();
        }

        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>An enumeration of attributes of type T that were found.</returns>
        public static IEnumerable<T> GetAttributes<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            // Try to find the configuration attribute for the default logger if it exists
            object[] configAttributes = Attribute.GetCustomAttributes(typeWithAttributes,
                typeof(T), false);

            if (configAttributes != null)
            {
                foreach (T attribute in configAttributes)
                {
                    yield return attribute;
                }
            }
        }

/*
 * ToUnixTimestamp
 * Converts a System.DateTime object to Unix timestamp.
 * 
 * Author: Koen Rouwhorst
 * Submitted on: 4/11/2010 5:09:51 PM
 * 
 * Example: 
 * var currentUnixTimestamp = DateTime.Now.ToUnixTimestamp();
 */

        /// <summary>
        /// Converts a System.DateTime object to Unix timestamp
        /// </summary>
        /// <returns>The Unix timestamp</returns>
        public static long ToUnixTimestamp(this DateTime date)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            var unixTimeSpan = date - unixEpoch;

            return (long) unixTimeSpan.TotalSeconds;
        }


/*
 * DateDiff
 * DateDiff in SQL style. The following DateParts are implemented: - "year" (abbr. "yy", "yyyy") - "quarter" (abbr. "qq", "q") - "month" (abbr. "mm", "m") - "day" (abbr. "dd", "d") - "week" (abbr. "wk", "ww") - "hour" (abbr. "hh") - "minute" (abbr. "mi", "n") - "second" (abbr. "ss", "s") - "millisecond" (abbr. "ms")
 * 
 * Author: Jonnidip
 * Submitted on: 6/8/2009 10:06:37 AM
 * 
 * Example: 
 * // Gets the total days from 01/01/2000.
DateTime dt = new DateTime(2000, 01, 01);
Int64 days = dt.DateDiff("day", DateTime.Now);
// Gets the total hours from 01/01/2000.
Int64 hours = dt.DateDiff("hour", DateTime.Now);
 */

        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static long DateDiff(this DateTime StartDate, string DatePart, DateTime EndDate)
        {
            long DateDiffVal = 0;
            var cal = Thread.CurrentThread.CurrentCulture.Calendar;
            var ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year

                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = cal.GetYear(EndDate) - cal.GetYear(StartDate);
                    break;

                #endregion

                #region quarter

                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (((cal.GetYear(EndDate)
                                     - cal.GetYear(StartDate)) * 4)
                                   + ((cal.GetMonth(EndDate) - 1) / 3))
                                  - ((cal.GetMonth(StartDate) - 1) / 3);
                    break;

                #endregion

                #region month

                case "month":
                case "mm":
                case "m":
                    DateDiffVal = ((cal.GetYear(EndDate)
                                    - cal.GetYear(StartDate)) * 12
                                   + cal.GetMonth(EndDate))
                                  - cal.GetMonth(StartDate);
                    break;

                #endregion

                #region day

                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (long) ts.TotalDays;
                    break;

                #endregion

                #region week

                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (long) (ts.TotalDays / 7);
                    break;

                #endregion

                #region hour

                case "hour":
                case "hh":
                    DateDiffVal = (long) ts.TotalHours;
                    break;

                #endregion

                #region minute

                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (long) ts.TotalMinutes;
                    break;

                #endregion

                #region second

                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (long) ts.TotalSeconds;
                    break;

                #endregion

                #region millisecond

                case "millisecond":
                case "ms":
                    DateDiffVal = (long) ts.TotalMilliseconds;
                    break;

                #endregion

                default:
                    throw new Exception($"DatePart \"{DatePart}\" is unknown");
            }
            return DateDiffVal;
        }


/*
 * IsBoolean
 * Checks whether the type is Boolean.
 * 
 * Author: kevinjong
 * Submitted on: 3/24/2010 8:23:24 AM
 * 
 * Example: 
 * Type type = (false).GetType();
bool isString = type.IsBoolean();
 */

        public static bool IsBoolean(this Type type)
        {
            return type.Equals(typeof(bool));
        }

/*
 * Clone<T>
 * Clones a DataRow - including strongly typed DataRows.
 * 
 * Author: Robert M. Downey
 * Submitted on: 10/18/2010 9:55:27 PM
 * 
 * Example: 
 * MyDataRow newDataRow = myDataRow.Clone<MyDataRow>(myDataRow.DataTable);
 */

        /// <summary>
        /// Creates a cloned and detached copy of a DataRow instance
        /// </summary>
        /// <typeparam name="T">The type of the DataRow if strongly typed</typeparam>
        /// <returns>
        /// An instance of the new DataRow
        /// </returns>
        public static T Clone<T>(this DataRow dataRow, DataTable parentTable)
            where T : DataRow
        {
            var clonedRow = (T) parentTable.NewRow();
            clonedRow.ItemArray = dataRow.ItemArray;
            return clonedRow;
        }


/*
 * Standard Deviation LINQ extension method (with overloads)
 * Typical standard deviation formula set in LINQ fluent syntax. For when Average, Min, and Max just aren't enough information. Works with int, double, float.
 * 
 * Author: ParrottSquawk
 * Submitted on: 8/6/2013 3:25:55 PM
 * 
 * Example: 
 * var nums1 = new[] { 11, 12, 13, 12, 13, 15, 12, 14, 15, 15, 12, 14, 15 };
            //Prints out the standard deviation of the entire data set (population)
            Console.WriteLine(nums1.StdDevP());
            //Prints out the standard deviation of the entire data set, but makes allowances for missing data points.
            Console.WriteLine(nums1.Take(10).StdDev());
            var nums2 = new[] { 11.0, 12.0, 13.0, 12.0, 13.0, 15.0, 12.0, 14.0, 15.0, 15.0, 12.0, 14.0, 15.0 };
            //Prints out the standard deviation of the entire data set (population)
            Console.WriteLine(nums2.StdDevP());
            //Prints out the standard deviation of the entire data set, but makes allowances for missing data points.
            Console.WriteLine(nums2.Take(10).StdDev());
            var nums3 = new[] { 11.0f, 12.0f, 13.0f, 12.0f, 13.0f, 15.0f, 12.0f, 14.0f, 15.0f, 15.0f, 12.0f, 14.0f, 15.0f };
            //Prints out the standard deviation of the entire data set (population)
            Console.WriteLine(nums3.StdDevP());
            //Prints out the standard deviation of the entire data set, but makes allowances for missing data points.
            Console.WriteLine(nums3.Take(10).StdDev());
 */

        public static double StdDevP(this IEnumerable<int> source)
        {
            return StdDevLogic(source, 0);
        }

        public static double StdDevP(this IEnumerable<double> source)
        {
            return StdDevLogic(source, 0);
        }

        public static double StdDevP(this IEnumerable<float> source)
        {
            return StdDevLogic(source, 0);
        }

        public static double StdDev(this IEnumerable<int> source)
        {
            return StdDevLogic(source);
        }

        public static double StdDev(this IEnumerable<double> source)
        {
            return StdDevLogic(source);
        }

        public static float StdDev(this IEnumerable<float> source)
        {
            return StdDevLogic(source);
        }

        private static double StdDevLogic(this IEnumerable<double> source, int buffer = 1)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var data = source.ToList();
            var average = data.Average();
            var differences = data.Select(u => Math.Pow(average - u, 2.0)).ToList();
            return Math.Sqrt(differences.Sum() / (differences.Count - buffer));
        }

        private static double StdDevLogic(this IEnumerable<int> source, int buffer = 1)
        {
            return StdDevLogic(source.Select(x => (double) x));
        }

        private static float StdDevLogic(this IEnumerable<float> source, int buffer = 1)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var data = source.ToList();
            var average = data.Average();
            var differences = data.Select(u => Math.Pow(average - u, 2.0)).ToList();
            return (float) Math.Sqrt(differences.Sum() / (differences.Count - buffer));
        }

/*
 * Linkify
 * Takes a string of text and replaces text matching a link pattern to a hyperlink
 * 
 * Author: Andy T
 * Submitted on: 5/6/2011 3:57:13 PM
 * 
 * Example: 
 * void Main() {   
    Console.WriteLine( "This goes to the https://www.test.com website".Linkify() );
    Console.WriteLine( "This goes to the http://www.test.com website".Linkify("_blank") );
    Console.WriteLine( "This goes to the www.test.com website".Linkify() );
    Console.WriteLine( "This goes to the test.com website".Linkify("_blank") );
    Console.WriteLine( "This goes to the test.com/page.html page".Linkify() );
    Console.WriteLine( "This goes to the https://wwwtest.com/folder/page.html page".Linkify("_blank") );
}
 */

        private static readonly Regex domainRegex =
            new Regex(
                @"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)",
                RegexOptions.Compiled | RegexOptions.Multiline);

        public static string Linkify(this string text, string target = "_self")
        {
            return domainRegex.Replace(
                text,
                match =>
                {
                    var link = match.ToString();
                    var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                    var url = new UriBuilder(link) {Scheme = scheme}.Uri.ToString();

                    return $@"<a href=""{url}"" target=""{target}"">{link}</a>";
                }
            );
        }


/*
 * int.IsNumber()
 * Checks if an integer is a number
 * 
 * Author: McBrover
 * Submitted on: 3/6/2015 12:04:40 PM
 * 
 * Example: 
 * 5.IsNumber() // returns true
 */

        public static bool IsNumber(this int i)
        {
            return true;
        }


/*
 * ToXML
 * Serializes an object to XML
 * 
 * Author: Jason Marcell
 * Submitted on: 10/27/2009 6:21:41 PM
 * 
 * Example: 
 * public class Foo
{
    public string bar { get; set; }
    public List<string> baz { get; set; }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var f = new Foo
                    {
                        bar = "hi",
                        baz = new List<string> {"quick", "brown", "fox"}
                    };
        Console.WriteLine(f.ToXML());

        //Output:
        //<?xml version="1.0"?>
        //<Foo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        //  <bar>hi</bar>
        //  <baz>
        //	<string>quick</string>
        //	<string>brown</string>
        //	<string>fox</string>
        //  </baz>
        //</Foo>
    }
}
 */

        public static string ToXML<T>(this T o)
            where T : new()
        {
            string retVal;
            using (var ms = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof(T));
                xs.Serialize(ms, o);
                ms.Flush();
                ms.Position = 0;
                var sr = new StreamReader(ms);
                retVal = sr.ReadToEnd();
            }
            return retVal;
        }


/*
 * UcFirst
 * Emulation of PHPs ucfirst()
 * 
 * Author: Stuart Sillitoe
 * Submitted on: 6/1/2016 12:50:07 PM
 * 
 * Example: 
 * "some string".UcFirst();

OR

string str = "a string";

str.UcFirst();
 */

        public static string UcFirst(this string theString)
        {
            if (string.IsNullOrEmpty(theString))
            {
                return string.Empty;
            }

            var theChars = theString.ToCharArray();
            theChars[0] = char.ToUpper(theChars[0]);

            return new string(theChars);
        }


/*
 * ConvertTo<T>
 * Converts an Array of arbitrary type to an array of type T. If a suitable converter cannot be found to do the conversion, a NotSupportedException is thrown.
 * 
 * Author: Brandon Siegel
 * Submitted on: 6/3/2008 11:06:18 PM
 * 
 * Example: 
 * int[] ipParts = Request.UserHostAddress.Split('.').ConvertTo<int>();
 */

        public static T[] ConvertTo<T>(this Array ar)
        {
            var ret = new T[ar.Length];
            var tc = TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(ar.GetValue(0).GetType()))
            {
                for (var i = 0; i < ar.Length; i++)
                {
                    ret[i] = (T) tc.ConvertFrom(ar.GetValue(i));
                }
            }
            else
            {
                tc = TypeDescriptor.GetConverter(ar.GetValue(0).GetType());
                if (tc.CanConvertTo(typeof(T)))
                {
                    for (var i = 0; i < ar.Length; i++)
                    {
                        ret[i] = (T) tc.ConvertTo(ar.GetValue(i), typeof(T));
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            return ret;
        }


/*
 * TryParse
 * This method takes away the need for writing two lines for TryParse and gives users an option of returning a default number.
 * 
 * Author: Avik
 * Submitted on: 6/7/2012 10:39:26 AM
 * 
 * Example: 
 * "2".TryParse(-1)=2;
"Xe".TryParse(-1)=-1;
 */

        public static int TryParse(this string input, int defaultValue)
        {
            int value;
            if (int.TryParse(input, out value))
            {
                return value;
            }
            return defaultValue;
        }


/*
 * IsNull
 * Unified advanced generic check for: DbNull.Value, INullable.IsNull, !Nullable<>.HasValue, null reference. Omits boxing for value types.
 * 
 * Author: Den
 * Submitted on: 9/4/2011 10:52:57 AM
 * 
 * Example: 
 * object a = null;
int? b = null;
var c = DBNull.Value;
var d = SqlInt32.Null;
Console.WriteLine(a.IsNull());
Console.WriteLine(b.IsNull());
Console.WriteLine(c.IsNull());
Console.WriteLine(d.IsNull());
 */

        [DebuggerStepThrough]
        public static bool IsNull<T>
            (this T? me)
            where T : struct
        {
            return !me.HasValue;
        }


/*
 * FirstDayOfMonth / LastDayOfMonth
 * Simple way to Get the first and last day of the specified date.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/30/2008 8:57:51 AM
 * 
 * Example: 
 * DateTime dateToday = DateTime.Now;

Console.WriteLine("First Day: {0}", dateToday.FirstDayOfMonth());
Console.WriteLine("Last Day: {0}", dateToday.LastDayOfMonth());
 */

        public static string FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).ToString("MM/dd/yyyy");
        }


/*
 * Equals
 * Equals method that lets you specify on what property or field value you want to compare an object on. Also compares the object types. Useful to use in an overridden Equals method of an object.
 * 
 * Author: joaomgcd
 * Submitted on: 3/18/2009 4:44:39 PM
 * 
 * Example: 
 * When used inside a custom "File" class: public override bool Equals(object obj)
        {
            return this.Equals(obj, x => x.Path);
        }
 */

        public static bool Equals<T, TResult>(this T obj, object obj1, Func<T, TResult> selector)
        {
            return obj1 is T && selector(obj).Equals(selector((T) obj1));
        }


/*
 * GetBoolean(string fieldName), GetDateTime(string fieldName), etc...
 * Use the Get[Type] functions that are part of the IDataReader but by passing the field name as a string as opposed to the field ordinal as int. Allows assigning default values for null values returned by the datareader.
 * 
 * Author: Jeff Reddy
 * Submitted on: 8/9/2011 6:16:06 PM
 * 
 * Example: 
 * var itemID = datareader.GetInt32("ItemID");

//With Default

var ItemName = datareader.GetString("ItemName", "Unknown");
 */

        /// <summary>
        /// This method extends the GetBoolean method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Boolean value for</param>
        /// <returns></returns>
        public static bool GetBoolean(this IDataReader dataReader, string fieldName)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            var retVal = false;

            if (!dataReader.IsDBNull(fieldOrdinal))
            {
                try
                {
                    retVal = dataReader.GetBoolean(fieldOrdinal);
                }
                catch (InvalidCastException)
                {
                    //We will swallow this exception as it's expected if our value has a dataType of bit. 
                    //We will try and handle that by casting to an Int16.
                    //If it fails here, we will allow the exception to get thrown
                    return (dataReader.GetInt16(fieldOrdinal) == 1);
                }
            }

            return retVal;
        }

        /// <summary>
        /// This method extends the GetDateTime method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the DateTime value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDataReader dataReader, string fieldName,
            DateTime defaultValue = default)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetDateTime(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetDecimal method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Decimal value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(this IDataReader dataReader, string fieldName, decimal defaultValue = 0m)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetDecimal(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetDouble method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Double value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(this IDataReader dataReader, string fieldName, double defaultValue = 0d)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetDouble(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetFloat method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Float value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetFloat(this IDataReader dataReader, string fieldName, float defaultValue = 0f)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetFloat(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetGuid method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Guid value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid GetGuid(this IDataReader dataReader, string fieldName, Guid defaultValue = default)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetGuid(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetInt16 method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Int16 value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short GetInt16(this IDataReader dataReader, string fieldName, short defaultValue)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetInt16(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetInt32 method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Int32 value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt32(this IDataReader dataReader, string fieldName, int defaultValue = 0)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetInt32(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetInt64 method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the Int64 value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long GetInt64(this IDataReader dataReader, string fieldName, long defaultValue = 0)
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetInt64(fieldOrdinal);
        }

        /// <summary>
        /// This method extends the GetString method of the data reader to allow calling by the field name
        /// </summary>
        /// <param name="dataReader">The datareader object we are extending</param>
        /// <param name="fieldName">The field name that we are getting the string value for</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetString(this IDataReader dataReader, string fieldName, string defaultValue = "")
        {
            var fieldOrdinal = dataReader.GetOrdinal(fieldName);
            return dataReader.IsDBNull(fieldOrdinal) ? defaultValue : dataReader.GetString(fieldOrdinal);
        }


/*
 * AsEnumerable
 * Allows you to treat an IDataReader from a database query as enumerable so that you can perform LINQ operations on it.
 * 
 * Author: James Michael Hare
 * Submitted on: 10/20/2010 11:22:18 PM
 * 
 * Example: 
 * using (var connection = new SqlConnection("some connection string"))
			using (var command = new SqlCommand("select * from products", connection))
			{
				connection.Open();

				using (var reader = command.ExecuteReader())
				{
					var results = reader.AsEnumerable()
						.Select(record => new Product
									{
										Name = (string)record["product_name"],
										Id = (int)record["product_id"],
										Category = (string)record["product_category"]
									})
						.GroupBy(product => product.Category);
				}
			}
 */
        // Enumerates through the reads in an IDataReader.
        public static IEnumerable<IDataRecord> AsEnumerable(this IDataReader reader)
        {
            while (reader.Read())
            {
                yield return reader;
            }
        }


/*
 * IsValidIPAddress
 * Validates whether a string is a valid IPv4 address.
 * 
 * Author: Arjan Keene
 * Submitted on: 2/24/2008 8:53:39 PM
 * 
 * Example: 
 * //returns false
string s = "192.168.1.256";
bool b = s.IsValidIPAddress();
//returns true
string s = "192.168.1.254";
bool b = s.IsValidIPAddress();
 */

        public static bool IsValidIPAddress(this string s)
        {
            return Regex.IsMatch(s,
                @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }


/*
 * WhereIf
 * When building a LINQ query, you may need to involve optional filtering criteria. Avoids if statements when building predicates & lambdas for a query. Useful when you don't know at compile time whether a filter should apply. Borrowed from Andrew Robinson. http://bit.ly/1V36G9
 * 
 * Author: Phil Campbell
 * Submitted on: 11/17/2009 10:47:55 PM
 * 
 * Example: 
 * List<Customer> custs = new List<Customer>{
new Customer {FirstName = "Peggy", AcctBalance = 12442.98},
new Customer {FirstName = "Sally", AcctBalance = 32.39},
new Customer {FirstName = "Billy", AcctBalance = 25.33},
new Customer {FirstName = "Tommy", AcctBalance = 12345}
};

bool showAccountBalancesUnder5000 = false;

var custList = custs.WhereIf(showAccountBalancesUnder5000, c=>c.AcctBalance < 5000).ToList(); //will not perform the filtering

showAccountBalancesUnder5000 = true;

var custListUnder5000 = custs.WhereIf(showAccountBalancesUnder5000, c=>c.AcctBalance < 5000).ToList(); //will perform the filtering
 */

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition,
            Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition,
            Func<TSource, int, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            return source;
        }


/*
 * GetValueOrDefault
 * Some time you want to get a nested property but a property in the chain is null then to avoid exception of Null Exception you must check all property in chain opposite of null
 * 
 * Author: Reza Arab Ghaeni
 * Submitted on: 12/26/2012 7:54:56 AM
 * 
 * Example: 
 * class A { public B b { get; set; } }
class B { public C c { get; set; } }
class C { public D d { get; set; } }
class D { public int value { get; set; } }

var a = new A();
a.b = new B();
a.b.c = null;
var temp = a.b.c.d.value;//Exception!
var betterTemp=a.GetValueOrDefault(p=>p.b,p=>p.c,p=>p.d,p=>p.value);
//better is null without exception
 */

        public static T3 GetValueOrDefault<T1, T2, T3>(this T1 prop1, Func<T1, T2> prop2, Func<T2, T3> prop3)
        {
            var prop = prop1.GetValueOrDefault(prop2);
            return Comparer<T2>.Default.Compare(prop, default) != 0 ? prop3(prop) : default;
        }

        public static T4 GetValueOrDefault<T1, T2, T3, T4>(this T1 prop1, Func<T1, T2> prop2, Func<T2, T3> prop3,
            Func<T3, T4> prop4)
        {
            var prop = prop1.GetValueOrDefault(prop2, prop3);
            return Comparer<T3>.Default.Compare(prop, default) != 0 ? prop4(prop) : default;
        }

        public static T5 GetValueOrDefault<T1, T2, T3, T4, T5>(this T1 prop1, Func<T1, T2> prop2, Func<T2, T3> prop3,
            Func<T3, T4> prop4, Func<T4, T5> prop5)
        {
            var prop = prop1.GetValueOrDefault(prop2, prop3, prop4);
            return Comparer<T4>.Default.Compare(prop, default) != 0 ? prop5(prop) : default;
        }

        public static T6 GetValueOrDefault<T1, T2, T3, T4, T5, T6>(this T1 prop1, Func<T1, T2> prop2,
            Func<T2, T3> prop3, Func<T3, T4> prop4, Func<T4, T5> prop5, Func<T5, T6> prop6)
        {
            var prop = prop1.GetValueOrDefault(prop2, prop3, prop4, prop5);
            return Comparer<T5>.Default.Compare(prop, default) != 0 ? prop6(prop) : default;
        }

        public static T7 GetValueOrDefault<T1, T2, T3, T4, T5, T6, T7>(this T1 prop1, Func<T1, T2> prop2,
            Func<T2, T3> prop3, Func<T3, T4> prop4, Func<T4, T5> prop5, Func<T5, T6> prop6, Func<T6, T7> prop7)
        {
            var prop = prop1.GetValueOrDefault(prop2, prop3, prop4, prop5, prop6);
            return Comparer<T6>.Default.Compare(prop, default) != 0 ? prop7(prop) : default;
        }


/*
 * IsString
 * Checks whether the type is string.
 * 
 * Author: kevinjong
 * Submitted on: 3/24/2010 8:20:31 AM
 * 
 * Example: 
 * Type type = ("Taiwan").GetType();
bool isString = type.IsString();
 */

        public static bool IsString(this Type type)
        {
            return type.Equals(typeof(string));
        }


/*
 * IsSet
 * I did not write this I just found it very useful, check http://stackoverflow.com/questions/7244 for original post.
 * 
 * Author: JA
 * Submitted on: 9/8/2009 4:04:17 PM
 * 
 * Example: 
 * MyEnum tester = MyEnum.FlagA | MyEnum.FlagB;

if(tester.IsSet(MyEnum.FlagA))
 */

        public static bool IsSet(this Enum input, Enum matchTo)
        {
            return (Convert.ToUInt32(input) & Convert.ToUInt32(matchTo)) != 0;
        }

#if NetFX

/*
 * Strongly Typed Databinding
 * This is an extension that I use for doing strongly typed databinding to controls in a winforms project. I dislike using strings to databind because they do not generate compiler errors when the bound object changes. This extension allows you to, instead of using a string, use an expression to bind to for both the control property and the object property.
 * 
 * Author: http://stackoverflow.com/questions/3444294/strong-typed-windows-forms-databinding
 * Submitted on: 3/26/2012 10:46:13 PM
 * 
 * Example: 
 * //txtCLSId is a TextBox
//_TaskListItem is an object of type ProjectServicesTaskList
//ProjectServicesTaskList contains a property (int) CLSHeaderID

txtCLSId.Bind(c => c.Text, _TaskListItem, ProjectServicesTaskList p) => p.CLSHeaderId);
 */

        ///<seealso cref="http://stackoverflow.com/questions/3444294/strong-typed-windows-forms-databinding"/>
        /// <summary>Databinding with strongly typed object names</summary>
        /// <param name="control">The Control you are binding to</param>
        /// <param name="controlProperty">The property on the control you are binding to</param>
        /// <param name="dataSource">The object you are binding to</param>
        /// <param name="dataSourceProperty">The property on the object you are binding to</param>
        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty));
        }

        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty, bool formattingEnabled = false)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty), formattingEnabled);
        }

        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty, bool formattingEnabled,
            DataSourceUpdateMode updateMode)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty), formattingEnabled, updateMode);
        }

        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty, bool formattingEnabled,
            DataSourceUpdateMode updateMode, object nullValue)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty), formattingEnabled, updateMode, nullValue);
        }

        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty, bool formattingEnabled,
            DataSourceUpdateMode updateMode, object nullValue, string formatString)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty), formattingEnabled, updateMode, nullValue, formatString);
        }

        public static Binding Bind<TControl, TDataSourceItem>(this TControl control,
            Expression<Func<TControl, object>> controlProperty, object dataSource,
            Expression<Func<TDataSourceItem, object>> dataSourceProperty, bool formattingEnabled,
            DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo)
            where TControl : Control
        {
            return control.DataBindings.Add(PropertyName.For(controlProperty), dataSource,
                PropertyName.For(dataSourceProperty), formattingEnabled, updateMode, nullValue, formatString,
                formatInfo);
        }

        public static class PropertyName
        {
            public static string For<T>(Expression<Func<T, object>> property)
            {
                var member = property.Body as MemberExpression;
                if (null == member)
                {
                    var unary = property.Body as UnaryExpression;
                    if (null != unary) member = unary.Operand as MemberExpression;
                }
                return null != member ? member.Member.Name : string.Empty;
            }
        }
#endif
        
/*
 * GetDisplayName()
 * Converts the pascal-cased Name property of a type to a displayable name.
 * 
 * Author: Jeroen de Zeeuw
 * Submitted on: 4/23/2008 4:08:15 PM
 * 
 * Example: 
 * comboBox1.DisplayMember = "Name";
comboBox1.DataSource = typeof(CodeExpression).Assembly.GetTypes().Where(t => t.BaseType == typeof(CodeExpression)).Select(t => new { Name = t.GetDisplayName(), Type = t }).ToList();
 */

        public static string GetDisplayName(this Type input)
        {
            var displayName = input.Name;

            for (var i = displayName.Length - 1; i >= 0; i--)
            {
                if (displayName[i] == char.ToUpper(displayName[i]))
                    if (i > 0)
                        displayName = displayName.Insert(i, " ");
            }
            return displayName;
        }


/*
 * Mask
 * A set of extension methods that make it easy to mask a string (to protect account numbers or other personal data). For example, you could mask a SSN of 123456789 to be ******789.
 * 
 * Author: James Michael Hare (BlackRabbitCoder)
 * Submitted on: 10/14/2010 8:21:08 PM
 * 
 * Example: 
 * var someInput = "123-45-6789"

var maskedId = someInput.Mask('X', 3);

// outputs: XXXXXXXX789
Console.WriteLine(maskedId);

var maskedWithDashes = someInput.Mask('*', 3, MaskStyle.AlphaNumericOnly);

// outputs: ***-**-*789
Console.WriteLine(maskedWithDashes);
 */

        /// <summary>
        /// An enumeration of the types of masking styles for the Mask() extension method
        /// of the string class.
        /// </summary>
        public enum MaskStyle
        {
            /// <summary>
            /// Masks all characters within the masking region, regardless of type.
            /// </summary>
            All,

            /// <summary>
            /// Masks only alphabetic and numeric characters within the masking region.
            /// </summary>
            AlphaNumericOnly
        }

        /// <summary>
        /// Default masking character used in a mask.
        /// </summary>
        public static readonly char DefaultMaskCharacter = '*';


        /// <summary>
        /// Returns true if the string is non-null and at least the specified number of characters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="length">The minimum length.</param>
        /// <returns>True if string is non-null and at least the length specified.</returns>
        /// <exception>throws ArgumentOutOfRangeException if length is not a non-negative number.</exception>
        public static bool IsLengthAtLeast(this string value, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length,
                    "The length must be a non-negative number.");
            }

            return value != null
                ? value.Length >= length
                : false;
        }

        /// <summary>
        /// Mask the source string with the mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar, int numExposed, MaskStyle style)
        {
            var maskedString = sourceValue;

            if (sourceValue.IsLengthAtLeast(numExposed))
            {
                var builder = new StringBuilder(sourceValue.Length);
                var index = maskedString.Length - numExposed;

                if (style == MaskStyle.AlphaNumericOnly)
                {
                    CreateAlphaNumMask(builder, sourceValue, maskChar, index);
                }
                else
                {
                    builder.Append(maskChar, index);
                }

                builder.Append(sourceValue.Substring(index));
                maskedString = builder.ToString();
            }

            return maskedString;
        }

        /// <summary>
        /// Mask the source string with the mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar, int numExposed)
        {
            return Mask(sourceValue, maskChar, numExposed, MaskStyle.All);
        }

        /// <summary>
        /// Mask the source string with the mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar)
        {
            return Mask(sourceValue, maskChar, 0, MaskStyle.All);
        }

        /// <summary>
        /// Mask the source string with the default mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, int numExposed)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, MaskStyle.All);
        }

        /// <summary>
        /// Mask the source string with the default mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, MaskStyle.All);
        }

        /// <summary>
        /// Mask the source string with the mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar, MaskStyle style)
        {
            return Mask(sourceValue, maskChar, 0, style);
        }

        /// <summary>
        /// Mask the source string with the default mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, int numExposed, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed, style);
        }

        /// <summary>
        /// Mask the source string with the default mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="style">The masking style to use (all characters or just alpha-nums).</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, MaskStyle style)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0, style);
        }

        /// <summary>
        /// Masks all characters for the specified length.
        /// </summary>
        /// <param name="buffer">String builder to store result in.</param>
        /// <param name="source">The source string to pull non-alpha numeric characters.</param>
        /// <param name="mask">Masking character to use.</param>
        /// <param name="length">Length of the mask.</param>
        private static void CreateAlphaNumMask(StringBuilder buffer, string source, char mask, int length)
        {
            for (var i = 0; i < length; i++)
            {
                buffer.Append(char.IsLetterOrDigit(source[i])
                    ? mask
                    : source[i]);
            }
        }


/*
 * ToObservableCollection<T>()
 * Convert a IEnumerable<T> to a ObservableCollection<T> and can be used in XAML (Silverlight and WPF) projects
 * 
 * Author: Fons Sonnemans
 * Submitted on: 11/26/2008 12:58:42 PM
 * 
 * Example: 
 * var list = new ObservableCollection<Employee>()

// add some employees

list = list.OrderBy(emp => emp.Salary).ToObservableCollection();
 */

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            var c = new ObservableCollection<T>();
            foreach (var e in coll)
                c.Add(e);
            return c;
        }


/*
 * toSlug
 * Generate slugs for friendly urls.
 * 
 * Author: Lucas
 * Submitted on: 11/30/2009 6:13:11 PM
 * 
 * Example: 
 * Console.WriteLine(@"I'm a cute Post Title/""\/".toSlug());
// => i_m_a_cute_post_title
 */

        public static string toSlug(this string text)
        {
            var value = text.Normalize(NormalizationForm.FormD).Trim();
            var builder = new StringBuilder();

            foreach (var c in text.ToCharArray())
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);

            value = builder.ToString();

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);

            value = Regex.Replace(
                Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(),
                @"\s+", "_");

            return value.ToLowerInvariant();
        }


/*
 * Async
 * Starts execution of IQueryable on a ThreadPool thread and returns immediately with a "end" method to call once the result is needed.
 * 
 * Author: ulrikb.worldpress.com
 * Submitted on: 10/14/2010 2:26:41 PM
 * 
 * Example: 
 * // Define some expensive query
IQueryable<string> myExpensiveQuery = context.SystemLog.Where(l => l.Timestamp >= DateTime.Today.AddDays(-10));

// Start async processing
Func<string[]> waitForQueryData = myExpensiveQuery.Async(e => e.ToArray());

// Do a lot of other work, e.g. other queries

// Need my query result now, so block until it's ready and get result
string[] myQueryResults = waitForQueryData();
 */

        public static Func<TResult> Async<T, TResult>(this IEnumerable<T> enumerable,
            Func<IEnumerable<T>, TResult> asyncSelector)
        {
            Debug.Assert(!(enumerable is ICollection),
                "Async does not work on arrays/lists/collections, only on true enumerables/queryables.");

            // Create delegate to exec async
            Func<IEnumerable<T>, TResult> work = (e => asyncSelector(e));

            // Launch it
            var r = work.BeginInvoke(enumerable, null, null);

            // Return method that will block until completed and rethrow exceptions if any
            return () => work.EndInvoke(r);
        }


/*
 * StringBuilder AppendIf
 * Makes it possible to conditionally append to a StringBuilder while keeping it fluent
 * 
 * Author: Lasse Sjørup
 * Submitted on: 4/9/2013 10:42:05 PM
 * 
 * Example: 
 * var keyBuilder = new StringBuilder();

keyBuilder.AppendIf(ctrl, "[ctrl]")
    .AppendIf(shift, "[shift]")
    .AppendIf(alt, "[alt]")
    .Append(" ")
    .Append(key);
 */

        public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
        {
            if (condition) builder.Append(value);
            return builder;
        }


/*
 * Split
 * somtimes one needs to split a larger collection into multiple smaller ones, this one does so use deferred execution
 * 
 * Author: Eckhard Schwabe
 * Submitted on: 3/6/2009 7:51:35 AM
 * 
 * Example: 
 * var nums = Enumerable.Range(0, 18);
            var split = nums.Split(5);

            foreach (var item in split)
            {
                foreach (var inner in item)
                {
                    Console.WriteLine(inner);
                }
            }
 */

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int splitSize)
        {
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return InnerSplit(enumerator, splitSize);
                }
            }
        }

        private static IEnumerable<T> InnerSplit<T>(IEnumerator<T> enumerator, int splitSize)
        {
            var count = 0;
            do
            {
                count++;
                yield return enumerator.Current;
            } while (count % splitSize != 0
                     && enumerator.MoveNext());
        }


/*
 * Inject object properties into string
 * Supplements String.Format by letting you get properties from objects
 * 
 * Author: Lasse Sjørup
 * Submitted on: 8/19/2013 9:47:44 PM
 * 
 * Example: 
 * var sourceObject = new { simpleString = "string", integer = 3, Date = new DateTime(2013, 08, 19) };

Debug.WriteLine("Gettings property by name : '{0:simpleString}' event cast insensitive : {0:date}".Inject(sourceObject));

Debug.WriteLine("The property can be formatted by appending standard String.Format syntax after the property name like this {0:date:yyyy-MM-dd}".Inject(sourceObject));

Debug.WriteLine("Use culture info to format the value to a specific culture '{0:date:dddd}'".Inject(CultureInfo.GetCultureInfo("da-DK"), sourceObject));

Debug.WriteLine("Inject more values and event build in types {0:integer} {1} with build in properties {1:length}".Inject(sourceObject, "simple string"));
 */

        public static string Inject(this string source, IFormatProvider formatProvider, params object[] args)
        {
            var objectWrappers = new object[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                objectWrappers[i] = new ObjectWrapper(args[i]);
            }

            return string.Format(formatProvider, source, objectWrappers);
        }

        public static string Inject(this string source, params object[] args)
        {
            return Inject(source, CultureInfo.CurrentUICulture, args);
        }

        private class ObjectWrapper : IFormattable
        {
            private readonly object wrapped;
            private static readonly Dictionary<string, FormatInfo> Cache = new Dictionary<string, FormatInfo>();

            public ObjectWrapper(object wrapped)
            {
                this.wrapped = wrapped;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                if (string.IsNullOrEmpty(format))
                {
                    return this.wrapped.ToString();
                }

                var type = this.wrapped.GetType();
                var key = type.FullName + ":" + format;

                FormatInfo wrapperCache;
                lock (Cache)
                {
                    if (!Cache.TryGetValue(key, out wrapperCache))
                    {
                        wrapperCache = CreateFormatInfo(format, type);
                        Cache.Add(key, wrapperCache);
                    }
                }

                var propertyInfo = wrapperCache.PropertyInfo;
                var outputFormat = wrapperCache.OutputFormat;

                var value = propertyInfo != null ? propertyInfo.GetValue(this.wrapped) : this.wrapped;

                return string.Format(formatProvider, outputFormat, value);
            }

            private static FormatInfo CreateFormatInfo(string format, IReflect type)
            {
                var spilt = format.Split(new[] {':'}, 2);
                var param = spilt[0];
                var hasSubFormat = spilt.Length == 2;
                var subFormat = hasSubFormat ? spilt[1] : string.Empty;

                var propertyInfo = type.GetProperty(param,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                var outputFormat = propertyInfo != null
                    ? (hasSubFormat ? "{0:" + subFormat + "}" : "{0}")
                    : "{0:" + format + "}";

                return new FormatInfo(propertyInfo, outputFormat);
            }

            private class FormatInfo
            {
                public FormatInfo(PropertyInfo propertyInfo, string form)
                {
                    this.PropertyInfo = propertyInfo;
                    this.OutputFormat = form;
                }

                public PropertyInfo PropertyInfo { get; private set; }

                public string OutputFormat { get; private set; }
            }
        }

#if NetFX

/*
 * Add<T>
 * A generic method to add databinding to a control. This method brings type safety for the sake of better code maintainability.
 * 
 * Author: Alireza Mokhtaripour
 * Submitted on: 3/26/2013 2:34:10 PM
 * 
 * Example: 
 * var txtName = new System.Windows.Forms.TextBox();
var ds = new System.Windows.Forms.BindingSource();
txtName.DataBindings.Add<tblProduct>("Text", ds, p => p.ProductName, true, DataSourceUpdateMode.OnValidation, "");
 */

        public static Binding Add<T>(this ControlBindingsCollection bindingCollection, string property,
            object datasource, Expression<Func<T, object>> expression)
        {
            var relatedNameChain = "";
            if (expression.Body is UnaryExpression)
                relatedNameChain = ((expression.Body as UnaryExpression).Operand as MemberExpression).ToString();
            else if (expression.Body is MemberExpression)
                relatedNameChain = (expression.Body as MemberExpression).ToString();

            var skippedName = string.Join(".", relatedNameChain.Split('.').Skip(1).ToArray());
            return bindingCollection.Add(property, datasource, skippedName);
        }
#endif
/*
 * SelectRandom
 * This method selects a random element from an Enumerable with only one pass (O(N) complexity). It contains optimizations for argumens that implement ICollection<T> by using the Count property and the ElementAt LINQ method. The ElementAt LINQ method itself contains optimizations for IList<T>
 * 
 * Author: Stilgar
 * Submitted on: 10/16/2010 2:21:32 PM
 * 
 * Example: 
 * int[] ints = new[] { 1, 2, 3, 4, 5 };
Console.WriteLine(ints.SelectRandom());

IEnumerable<int> seq = Enumerable.Range(1, 5);
Console.WriteLine(seq.SelectRandom());
 */

        private static Random random = new Random();

        public static T SelectRandom<T>(this IEnumerable<T> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException();
            }

            if (!sequence.Any())
            {
                throw new ArgumentException("The sequence is empty.");
            }

            //optimization for ICollection<T>
            if (sequence is ICollection<T>)
            {
                var col = (ICollection<T>) sequence;
                return col.ElementAt(random.Next(col.Count));
            }

            var count = 1;
            var selected = default(T);

            foreach (var element in sequence)
            {
                if (random.Next(count++) == 0)
                {
                    //Select the current element with 1/count probability
                    selected = element;
                }
            }

            return selected;
        }


/*
 * Enum<T>.Parse() and Enum<T>.TryParse()
 * Parses the specified string value into the Enum type passed. Also contains a bool to determine whether or not the case should be ignored.
 * 
 * Author: Dan Atkinson
 * Submitted on: 12/22/2009 1:01:34 PM
 * 
 * Example: 
 * //Enum
public enum MyEnum
{
  Foo,
  Bar,
}

MyEnum a = Enum<MyEnum>.Parse("foo"); //Returns MyEnum.Foo
MyEnum b = Enum<MyEnum>.Parse("foo", false); //Fails as the parse is case sensitive

//Try Parse
MyEnum c = MyEnum.Foo;

if(Enum<MyEnum>.TryParse("Fi", out c) == false)
{
  //throw an error
}
 */

        public static class Enum<T>
        {
            public static T Parse(string value)
            {
                return Parse(value, true);
            }

            public static T Parse(string value, bool ignoreCase)
            {
                return (T) Enum.Parse(typeof(T), value, ignoreCase);
            }

            public static bool TryParse(string value, out T returnedValue)
            {
                return TryParse(value, true, out returnedValue);
            }

            public static bool TryParse(string value, bool ignoreCase, out T returnedValue)
            {
                try
                {
                    returnedValue = (T) Enum.Parse(typeof(T), value, ignoreCase);
                    return true;
                }
                catch
                {
                    returnedValue = default;
                    return false;
                }
            }
        }

/*
 * KB,MB,GB,TB
 * Simplest way to get a number of bytes at different measures. KB, MB, GB or TB,
 * 
 * Author: José Fidalgo H.
 * Submitted on: 6/13/2013 11:00:50 PM
 * 
 * Example: 
 * var 1kb = 1.KB();
var 1mb = 1.MB();
var 1gb = 1.GB();
var 1tb = 1.TB();
 */

        /// <summary>
        /// Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary>
        /// Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary>
        /// Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary>
        /// Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return value.GB() * (long) 1024;
        }


/*
 * IsNullOrEmptyOrWhiteSpace
 * It returns true if string is null or empty or just a white space otherwise it returns false.
 * 
 * Author: Omkar Panhalkar
 * Submitted on: 5/7/2012 3:56:30 AM
 * 
 * Example: 
 * Assert.IsTrue(IsNullOrEmptyOrWhiteSpace(null));
Assert.IsTrue(IsNullOrEmptyOrWhiteSpace(string.Empty));
Assert.IsTrue(IsNullOrEmptyOrWhiteSpace("   "));
Assert.IsFalse(IsNullOrEmptyOrWhiteSpace("TestValue"));
 */

        public static bool IsNullOrEmptyOrWhiteSpace(this string input)
        {
            return string.IsNullOrEmpty(input) || input.Trim() == string.Empty;
        }
#if NetFX

/*
 * Include
 * Type-safe Include: a completely type-safe way to include nested objects in scenarios with DomainServices and RIA in, for example, Silverlight applications. Example: Include(x=>x.Parent) instead of Include("Parent"). A more detailed explanation can be found at http://www.chrismeijers.com/post/Type-safe-Include-for-RIA-DomainServices.aspx
 * 
 * Author: Chris Meijers
 * Submitted on: 10/25/2010 5:25:00 PM
 * 
 * Example: 
 * Instead of writing:
ObjectContext.Employee.Include("Address").Include("Department.Division");

one can now use a completely type-safe variant:
ObjectContext.Employee.Include(x=>x.Address).Include(y=>y.Department, z=>z.Division);

This finally gets rid of that nasty piece of untyping in an otherwise lovely type-safe system...
 */

        public static ObjectQuery<T> Include<T, T2>(this ObjectQuery<T> data,
            Expression<Func<T, ICollection<T2>>> property1, Expression<Func<T2, object>> property2)
            where T : class
            where T2 : class
        {
            var name1 = (property1.Body as MemberExpression).Member.Name;
            var name2 = (property2.Body as MemberExpression).Member.Name;

            return data.Include(name1 + "." + name2);
        }

        public static ObjectQuery<T> Include<T, T2>(this ObjectQuery<T> data, Expression<Func<T, T2>> property1,
            Expression<Func<T2, object>> property2) where T : class
        {
            var name1 = (property1.Body as MemberExpression).Member.Name;
            var name2 = (property2.Body as MemberExpression).Member.Name;

            return data.Include(name1 + "." + name2);
        }

        public static ObjectQuery<T> Include<T>(this ObjectQuery<T> data, Expression<Func<T, object>> property)
            where T : class
        {
            var name = (property.Body as MemberExpression).Member.Name;

            return data.Include(name);
        }
#endif

/*
 * IsInRange
 * Determines if a date is within a given date range
 * 
 * Author: Charles Cherry
 * Submitted on: 3/3/2011 4:14:48 PM
 * 
 * Example: 
 * var monday = DateTime.Now.ThisWeekMonday();
var friday = DateTime.Now.ThisWeekFriday();

If (DateTime.Now.IsInRange(monday, friday) {
    ...do something...
}
 */

        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }
        
#if NetFX
/*
 * GetCurrentDataRow
 * The System.Windows.Forms.BindingSource has a property to return the current row as DataRowView object. But most of the time we need this as DataRow to manipulate the data. This extension method checks the Current property of BindingSource for nullity, returns null if it is null and returns the current Row as DataRow object if the Current property is not null.
 * 
 * Author: V J Reddy
 * Submitted on: 10/9/2011 8:27:51 AM
 * 
 * Example: 
 * DataRow currentRow = bindingSource.GetCurrentDataRow();
 */

//Get the current row of binding source as datarow    
        public static DataRow GetCurrentDataRow(this BindingSource bindingSource)
        {
            if (bindingSource.Current == null)
                return null;
            return ((DataRowView) bindingSource.Current).Row;
        }
#endif

/*
 * DefaultIfNull
 * return default value if string is null
 * 
 * Author: Alexander Gubenko
 * Submitted on: 4/22/2011 5:45:21 AM
 * 
 * Example: 
 * string str = null;
str.DefaultIfNull("I'm nil") // return "I'm nil"

string str1 = "Hello!";
str1.DefaultIfNull("I'm nil") // return "Hello!"
 */

        public static string DefaultIfNull(this string str, string defaultValue)
        {
            return str ?? defaultValue;
        }


/*
 * Extend
 * An extenssion function to work like the extend method of javascript. It takes the object and merge with oder, but only if the property of the other object has value.
 * 
 * Author: Luiz
 * Submitted on: 10/2/2012 8:58:11 PM
 * 
 * Example: 
 * foo = foo.extend(foo2);
 */

        public static T Extend<T>(this T target, T source) where T : class
        {
            var properties =
                target.GetType().GetProperties().Where(pi => pi.CanRead && pi.CanWrite);

            foreach (var propertyInfo in properties)
            {
                var targetValue = propertyInfo.GetValue(target, null);
                var defaultValue = propertyInfo.PropertyType.GetDefault();

                if (targetValue != null && !targetValue.Equals(defaultValue)) continue;

                var sourceValue = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(target, sourceValue, null);
            }

            return target;
        }

/*
 * IsEmpty / IsNotEmpty
 * Checks if any value type is empty.
 * 
 * Author: Patrick A. Lorenz
 * Submitted on: 3/19/2008 8:26:25 AM
 * 
 * Example: 
 * var dt = DateTime.MinValue;
if(dt.IsEmpty()) {
 ...
}
 */

        public static bool IsStructEmpty<T>(this T value) where T : struct
        {
            return value.Equals(default(T));
        }

        public static bool IsStructNotEmpty<T>(this T value) where T : struct
        {
            return !value.IsStructEmpty();
        }


/*
 * List AddElement
 * Make adding to list fluent and conditioned
 * 
 * Author: Lasse Sjørup
 * Submitted on: 4/11/2013 10:39:30 AM
 * 
 * Example: 
 * var list = new List<string>();
var condition = true;
list.AddElement("line 1")
    .AddElementIf(condition, "line 2")
    .AddElementRange(new[] {"line 3", "line 4"})
    .AddElementRangeIf(condition, new[] {"line 5", "line 6"});
 */

        public static List<T> AddElement<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> AddElementIf<T>(this List<T> list, bool condition, T item)
        {
            if (condition)
            {
                list.Add(item);
            }

            return list;
        }

        public static List<T> AddElementRange<T>(this List<T> list, IEnumerable<T> items)
        {
            list.AddRange(items);
            return list;
        }

        public static List<T> AddElementRangeIf<T>(this List<T> list, bool condition, IEnumerable<T> items)
        {
            if (condition)
            {
                list.AddRange(items);
            }
            return list;
        }


/*
 * SelectDistinct
 * "SELECT DISTINCT" over a DataTable. Handles multiple columns selection.
 * 
 * Author: Jonnidip
 * Submitted on: 5/28/2009 3:15:59 PM
 * 
 * Example: 
 * DataTable dt2 = dt.SelectDistinct("Column1, Column2");
 */

        #region Select Distinct

        /// <summary>
        /// "SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="SourceTable">Input DataTable</param>
        /// <param name="FieldName">Fields to select (distinct)</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(this DataTable SourceTable, string FieldName)
        {
            return SelectDistinct(SourceTable, FieldName, string.Empty);
        }

        /// <summary>
        ///"SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="SourceTable">Input DataTable</param>
        /// <param name="FieldNames">Fields to select (distinct)</param>
        /// <param name="Filter">Optional filter to be applied to the selection</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(this DataTable SourceTable, string FieldNames, string Filter)
        {
            var dt = new DataTable();
            var arrFieldNames = FieldNames.Replace(" ", "").Split(',');
            foreach (var s in arrFieldNames)
            {
                if (SourceTable.Columns.Contains(s))
                    dt.Columns.Add(s, SourceTable.Columns[s].DataType);
                else
                    throw new Exception($"The column {s} does not exist.");
            }

            object[] LastValues = null;
            foreach (var dr in SourceTable.Select(Filter, FieldNames))
            {
                var NewValues = GetRowFields(dr, arrFieldNames);
                if (LastValues == null || !(ObjectComparison(LastValues, NewValues)))
                {
                    LastValues = NewValues;
                    dt.Rows.Add(LastValues);
                }
            }

            return dt;
        }

        #endregion

        #region Private Methods

        private static object[] GetRowFields(DataRow dr, string[] arrFieldNames)
        {
            if (arrFieldNames.Length == 1)
                return new[] {dr[arrFieldNames[0]]};
            var itemArray = new ArrayList();
            foreach (var field in arrFieldNames)
                itemArray.Add(dr[field]);

            return itemArray.ToArray();
        }

        /// <summary>
        /// Compares two values to see if they are equal. Also compares DBNULL.Value.
        /// </summary>
        /// <param name="a">Object A</param>
        /// <param name="b">Object B</param>
        /// <returns></returns>
        private static bool ObjectComparison(object a, object b)
        {
            if (a == DBNull.Value && b == DBNull.Value) //  both are DBNull.Value
                return true;
            if (a == DBNull.Value || b == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (a.Equals(b)); // value type standard comparison
        }

        /// <summary>
        /// Compares two value arrays to see if they are equal. Also compares DBNULL.Value.
        /// </summary>
        /// <param name="A">Object Array A</param>
        /// <param name="B">Object Array B</param>
        /// <returns></returns>
        private static bool ObjectComparison(object[] a, object[] b)
        {
            var retValue = true;
            var singleCheck = false;

            if (a.Length == b.Length)
                for (var i = 0; i < a.Length; i++)
                {
                    if (!(singleCheck = ObjectComparison(a[i], b[i])))
                    {
                        retValue = false;
                        break;
                    }
                    retValue = retValue && singleCheck;
                }

            return retValue;
        }

        #endregion

#if NetFX
/*
 * AddCssClass
 * Adds a css class to the webcontrol. Instead of having to pass one string to the CssClass property, you can add them one by one with the AddCssClass extension method. This can come in handy when a webcontrol has a default class (from the ASP.NET markup) and then needs additional classes based on a condition (like whether or not a user is logged in).
 * 
 * Author: Kristof Claes
 * Submitted on: 8/11/2010 7:52:27 AM
 * 
 * Example: 
 * ASP.NET Markup:
<asp:Label ID="MyLabel" runat="server" CssClass="defaultClass" />

C# Codebehind:
if (userIsLoggedIn)
{
    MyLabel.AddCssClass("loggedIn");
}
else
{
    MyLabel.AddCssClass("anonymous");
}
 */

        public static void AddCssClass(this WebControl control, string cssClass)
        {
            control.CssClass += " " + cssClass;
        }
#endif

#if NetFX
/*
 * Session GetValue
 * A cleaner way to get values from Session
 * 
 * Author: Mark Wiseman
 * Submitted on: 5/28/2014 6:27:19 AM
 * 
 * Example: 
 * String value1 = Session.GetValue<String>("key1");
String value2 = Session.GetValue<String>("key2", "default text");

MyObject value3 = Session.GetValue<MyObject>("key3");
MyObject value4 = Session.GetValue<MyObject>("key4", new MyObject());
 */

        public static T GetValue<T>(this HttpSessionStateBase session, string key)
        {
            return session.GetValue(key, default(T));
        }

        public static T GetValue<T>(this HttpSessionStateBase session, string key, T defaultValue)
        {
            if (session[key] != null)
            {
                return (T) Convert.ChangeType(session[key], typeof(T));
            }

            return defaultValue;
        }
#endif

/*
 * FixPersianChars
 * متدی برای حل مشکل وارد کردن ي و ك عربی توسط کاربر و تبدیل به ی و ک فارسی. توسط محمد کمائی
 * 
 * Author: Mohammad Komaei
 * Submitted on: 11/9/2012 9:53:16 PM
 * 
 * Example: 
 * string txtLastName = "بابك کمائي";
MessageBox.Show(txtLastName);
MessageBox.Show(txtLastName.FixPersianChars());
 */

        /// <summary>
        /// متدی برای حل مشکل وارد کردن ي و ك عربی توسط کاربر و تبدیل به ی و ک فارسی. توسط محمد کمائی
        /// </summary>
        public static string FixPersianChars(this string Value)
        {
            return Value.Replace('ي', 'ی').Replace("ك", "ک");
        }


/*
 * ToMd5Hash
 * Convert a input string to a byte array and compute the hash.
 * 
 * Author: Alvaro Torres Tatis
 * Submitted on: 6/5/2012 8:52:38 PM
 * 
 * Example: 
 * string value = "this is a string";
Console.WriteLine(value.ToMd5Hash());
 */

        /// <summary>
        /// Convert a input string to a byte array and compute the hash.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToMd5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                var originalBytes = ASCIIEncoding.Default.GetBytes(value);
                var encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }


/*
 * DataTable to CSV export
 * Export datatable to CSV file
 * 
 * Author: John
 * Submitted on: 1/29/2010 3:41:06 PM
 * 
 * Example: 
 * DataTable dataTableExportToCSV;
dataTableExportToCSV.ToCSV (",",false);
 */

        public static void ToCSV(this DataTable table, string delimiter, bool includeHeader)
        {
            var result = new StringBuilder();


            if (includeHeader)
            {
                foreach (DataColumn column in table.Columns)
                {
                    result.Append(column.ColumnName);

                    result.Append(delimiter);
                }


                result.Remove(--result.Length, 0);

                result.Append(Environment.NewLine);
            }


            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    if (item is DBNull)

                        result.Append(delimiter);

                    else
                    {
                        var itemAsString = item.ToString();

                        // Double up all embedded double quotes

                        itemAsString = itemAsString.Replace("\"", "\"\"");


                        // To keep things simple, always delimit with double-quotes

                        // so we don't have to determine in which cases they're necessary

                        // and which cases they're not.

                        itemAsString = "\"" + itemAsString + "\"";


                        result.Append(itemAsString + delimiter);
                    }
                }


                result.Remove(--result.Length, 0);

                result.Append(Environment.NewLine);
            }


            using (var writer = new StreamWriter(@"C:\log.csv", true))
            {
                writer.Write(result.ToString());
            }
        }


/*
 * Concat
 * Adds an element to an IEnumerable (System.Linq.Concat only adds multiple elements)
 * 
 * Author: B.W. Kemps
 * Submitted on: 8/22/2011 2:48:55 PM
 * 
 * Example: 
 * IEnumerable<String> x = new[]{"foo", "bar"};
x = x.Concat("bla");
Console.WriteLine(string.Join('--',x));

// returns foo--bar--bla
 */

        static IEnumerable<T> Concat<T>(this IEnumerable<T> target, T element)
        {
            foreach (var e in target) yield return e;
            yield return element;
        }


/*
 * IsUnicode
 * IsUnicode
 * 
 * Author: Mehdi Miri
 * Submitted on: 3/26/2013 5:37:03 AM
 * 
 * Example: 
 * bool isUnicode = "سید مهدی میری".IsUnicode();
 */

        public static bool IsUnicode(this string value)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(value);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(value);

            if (asciiBytesCount != unicodBytesCount)
            {
                return true;
            }
            return false;
        }

#if NetFX
/*
 * GetChildren
 * This is a recursive function to get all child controls under the target control.
 * 
 * Author: Ryan Helms
 * Submitted on: 7/17/2010 8:04:12 PM
 * 
 * Example: 
 * IEnumerable<Control> children = Page.GetChildren();

or

IEnumerable<Control> children = Container.GetChildren();
 */

        public static IEnumerable<Control> GetChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();
            return children.SelectMany(GetChildren).Concat(children);
        }
#endif

#if NetFX
/*
 * AddJavaScript
 * Dynamically adds a javascript file (.js) to a page even if using master page.
 * 
 * Author: Paulo Castilho
 * Submitted on: 6/1/2010 4:29:52 PM
 * 
 * Example: 
 * public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AddJavaScript("jsFile.js");
    }
}
 */

        public static void AddJavaScript(this Page page, string url)
        {
            var js = new HtmlGenericControl("script");
            js.Attributes["type"] = "text/javascript";
            js.Attributes["src"] = url;
            page.Header.Controls.Add(js);
        }
#endif
#if NetFX
/*
 * AddCSS
 * Dynamically adds a cascading style sheet (a.k.a. CSS) file to a page even if using master page.
 * 
 * Author: Paulo Castilho
 * Submitted on: 6/1/2010 4:33:06 PM
 * 
 * Example: 
 * public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.AddCSS("cssFile.css");
    }
}
 */

        public static void AddCSS(this Page page, string url)
        {
            var link = new HtmlLink();
            link.Href = url;
            link.Attributes["rel"] = "stylesheet";
            link.Attributes["type"] = "text/css";
            page.Header.Controls.Add(link);
        }
#endif

/*
 * TryDispose
 * Dispose an object if it implement IDisposable. Especially useful when working with interfaces and object factories, and IDisposable may or may not found on concrete class.
 * 
 * Author: MatteoSp
 * Submitted on: 10/1/2008 10:42:14 PM
 * 
 * Example: 
 * IWhatever obj = factory.Create();  obj.TryDispose();
 */

        public static void TryDispose(this object target, bool throwException)
        {
            var disposable = target as IDisposable;
            if (disposable == null)
                return;

            try
            {
                disposable.Dispose();
            }
            catch (Exception)
            {
                if (throwException)
                    throw;
            }
        }


/*
 * Randomize
 * OrderBy() is nice when you want a consistent & predictable ordering. This method is NOT THAT! Randomize() - Use this extension method when you want a different or random order every time! Useful when ordering a list of things for display to give each a fair chance of landing at the top or bottom on each hit. {customers, support techs, or even use as a randomizer for your lottery ;) }
 * 
 * Author: Phil Campbell
 * Submitted on: 11/2/2009 6:41:32 AM
 * 
 * Example: 
 * // use this on any collection that implements IEnumerable!
// List, Array, HashSet, Collection, etc

List<string> myList = new List<string> { "hello", "random", "world", "foo", "bar", "bat", "baz" };

foreach (string s in myList.Randomize())
{
    Console.WriteLine(s);
}
 */

        public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target)
        {
            var r = new Random();

            return target.OrderBy(x => (r.Next()));
        }


/*
 * Select
 * It returns reader lines which can be retrieved from lamba statement
 * 
 * Author: Credit goes to Jon Skeet
 * Submitted on: 5/7/2012 4:00:32 AM
 * 
 * Example: 
 * using (DataReader reader = ...)
{
    List<Customer> customers = reader.Select(r => new Customer {
        CustomerId = r["id"] is DBNull ? null : r["id"].ToString(),
        CustomerName = r["name"] is DBNull ? null : r["name"].ToString() 
    }).ToList();
}
 */

        public static IEnumerable<T> Select<T>(this SqlDataReader reader, Func<SqlDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }

/*
 * With and Without
 * Fake immutability in an existing list class by adding a "With" and "Without" method
 * 
 * Author: unknown
 * Submitted on: 2/8/2008 4:35:29 PM
 * 
 * Example: 
 * List<int> nums = new List<int>() { 1, 2, 3, 4, 5 };

foreach (int i in nums.With(6))
{
    Console.WriteLine(i);
}

foreach (int i in nums.Without(3))
{
    Console.WriteLine(i);
}
 */

        public static TList With<TList, T>(this TList list, T item) where TList : IList<T>, new()
        {
            var l = new TList();

            foreach (var i in list)
            {
                l.Add(i);
            }
            l.Add(item);

            return l;
        }

        public static TList Without<TList, T>(this TList list, T item) where TList : IList<T>, new()
        {
            var l = new TList();

            foreach (var i in list.Where(n => !n.Equals(item)))
            {
                l.Add(i);
            }

            return l;
        }

/*
 * ToInt
 * It converts string to integer and assigns a default value if the conversion is not a success
 * 
 * Author: Sathish Kumar
 * Submitted on: 9/28/2011 9:30:40 AM
 * 
 * Example: 
 * public static void Main()
   {
     string s = "3";
    Console.WriteLine(s.ToInt(1));
     s = "a3";
    Console.WriteLine(s.ToInt(100));


}

Output:
3
100
 */

        public static int ToInt(this string number, int defaultInt)
        {
            var resultNum = defaultInt;
            try
            {
                if (!string.IsNullOrEmpty(number))
                    resultNum = Convert.ToInt32(number);
            }
            catch
            {
            }
            return resultNum;
        }

/*
 * convert Internet dot address to network address
 * Csharp equivalent of Linux / Unix Command: inet_aton. The inet_aton() extension method converts the specified ipaddress, in the Internet standard dot notation, to a network address.
 * 
 * Author: DotNerd
 * Submitted on: 6/18/2008 3:38:09 PM
 * 
 * Example: 
 * IPAddress ipAddress = IPAddress.Parse(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
double ipNumber = ipAddress.inet_aton();
 */

        public static double inet_aton(this IPAddress IPaddress)
        {
            int i;
            double num = 0;
            if (IPaddress.ToString() == "")
            {
                return 0;
            }
            var arrDec = IPaddress.ToString().Split('.');
            for (i = arrDec.Length - 1; i >= 0; i--)
            {
                num += ((int.Parse(arrDec[i]) % 256) * Math.Pow(256, (3 - i)));
            }
            return num;
        }

#if NetFX
/*
 * FindParent
 * A simple type safe method to find a parent control
 * 
 * Author: Rob White
 * Submitted on: 12/13/2010 8:57:02 PM
 * 
 * Example: 
 * someControl.FindParent<RepeaterItem>().UniqueID;
 */

        public static T FindParent<T>(this Control target) where T : Control
        {
            if (target.Parent == null)
            {
                return null;
            }

            var parent = target.Parent as T;
            if (parent != null)
            {
                return parent;
            }

            return target.Parent.FindParent<T>();
        }
#endif

/*
 * Clamp
 * Limit a value to a certain range. When the value is smaller/bigger than the range, snap it to the range border.
 * 
 * Author: Steven Jeuris
 * Submitted on: 11/15/2011 9:42:21 AM
 * 
 * Example: 
 * int 50;
int clamped = 50.Clamp( 0, 20 ); // clamped == 20
 */

        /// <summary>
        ///   Limit a value to a certain range. When the value is smaller/bigger than the range, snap it to the range border.
        /// </summary>
        /// <typeparam name = "T">The type of the value to limit.</typeparam>
        /// <param name = "source">The source for this extension method.</param>
        /// <param name = "start">The start of the interval, included in the interval.</param>
        /// <param name = "end">The end of the interval, included in the interval.</param>
        public static T Clamp<T>(this T source, T start, T end)
            where T : IComparable
        {
            var isReversed = start.CompareTo(end) > 0;
            var smallest = isReversed ? end : start;
            var biggest = isReversed ? start : end;

            return source.CompareTo(smallest) < 0
                ? smallest
                : source.CompareTo(biggest) > 0
                    ? biggest
                    : source;
        }
    }
}