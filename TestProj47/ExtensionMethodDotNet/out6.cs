using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.IO.Ports;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#if NetFX
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * FormatIf
 * Conditionally formats any value type based on a Lambda Expression
 * 
 * Author: Patrick A. Lorenz
 * Submitted on: 3/19/2008 8:30:05 AM
 * 
 * Example: 
 * var dt = DateTime.Now;
Console.WriteLine(dt.FormatIf(d => d.Date != DateTime.Now.Date, "The selected day is {0:d}", "today"));
 */

        public static string FormatIf<T>(this T value, string format) where T : struct
        {
            return FormatIf(value, null, format, null);
        }

        public static string FormatIf<T>(this T value, string format, string defaultText) where T : struct
        {
            return FormatIf(value, null, format, defaultText);
        }

        public static string FormatIf<T>(this T value, Func<T, bool> condition, string format, string defaultText)
            where T : struct
        {
            if (condition == null) condition = v => v.IsStructNotEmpty();
            return (condition(value) ? string.Format(format, value) : defaultText);
        }



/*
 * IsTrue
 * Returns 'true' if a Boolean value is true.
 * 
 * Author: Bradley Grainger
 * Submitted on: 6/6/2015 9:19:01 PM
 * 
 * Example: 
 * if ((input == 42).IsTrue())
{
    // handle the case when input is 42
}
 */

        public static bool IsTrue(this bool value)
        {
            return value;
        }


/*
 * EnqueueAll
 * Enqueues an aray of items to a Queue rather than having to loop and call Enqueue for each item.
 * 
 * Author: Mike Cromwell
 * Submitted on: 10/18/2010 3:18:48 PM
 * 
 * Example: 
 * queue.EnqueueAll(new string[] 
                {
                    "foo", "bar"
                });
 */

        public static void EnqueueAll<T>(this Queue<T> queue, T[] items)
        {
            foreach (var item in items)
                queue.Enqueue(item);
        }


/*
 * HasMultipleInstancesOf
 * Determines whether a string has multiple occurrences of a particular character. May be helpful when parsing file names, or ensuring a particular string has already contains a given character. This may be extended to use strings, rather than a char.
 * 
 * Author: Phil Campbell
 * Submitted on: 11/9/2009 7:15:31 PM
 * 
 * Example: 
 * string myStr1= "Hello.World.txt";
string myStr2= "Hello World";

bool hasMultPeriods = myStr1.HasMultipleInstancesOf('.'); //returns true
bool hasMultSpaces = myStr2.HasMultipleInstancesOf(' '); //returns false
 */

        public static bool HasMultipleInstancesOf(this string input, char charToFind)
        {
            if ((string.IsNullOrEmpty(input)) || (input.Length == 0) || (input.IndexOf(charToFind) == 0))
                return false;

            if (input.IndexOf(charToFind) != input.LastIndexOf(charToFind))
                return true;

            return false;
        }



/*
 * HasValueAndEquals
 * Substitutes this: int? index = GetIndex(); if (index.HasValue && index.Value == 10) ...
 * 
 * Author: MatteoSp
 * Submitted on: 10/1/2008 10:49:59 PM
 * 
 * Example: 
 * int? index = GetIndex();
if (index.HasValueAndEquals(10))...
 */

        public static bool HasValueAndEquals<T>(this Nullable<T> source, T target)
            where T : struct
        {
            return source.HasValue && source.Value.Equals(target);
        }

        public static bool HasValueAndEquals<T>(this Nullable<T> source, Nullable<T> target)
            where T : struct
        {
            return source.HasValue && source.Value.Equals(target);
        }


#if NetFX
/*
 * NumericUpDown SafeValue()
 * http://peshir.blogspot.nl/2011/02/safely-set-numericupdown-control-value.html
 * 
 * Author: peSHIr
 * Submitted on: 5/6/2014 12:21:47 PM
 * 
 * Example: 
 * Use control.SafeValue(42) instead of control.Value = 42.
 */

        public static void SafeValue(this NumericUpDown c, decimal value)
        {
            c.Value = Math.Max(c.Minimum, Math.Min(value, c.Maximum));
        }
#endif

/*
 * GetMonthDiff
 * Compute dateTime difference
 * 
 * Author: Alex-LEWIS
 * Submitted on: 8/12/2015 3:23:05 PM
 * 
 * Example: 
 * DateTime dt1 = new DateTime(2015, 08, 11);
DateTime dt2 = new DateTime(1992, 10, 10);
var monthDiff = dt1.GetMonthDiff(dt2);
 */

        /// <summary>
        /// Compute dateTime difference
        /// Alex-LEWIS, 2015-08-11
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static int GetMonthDiff(this DateTime dt1, DateTime dt2)
        {
            var l = dt1 < dt2 ? dt1 : dt2;
            var r = dt1 >= dt2 ? dt1 : dt2;
            return (l.Day == r.Day ? 0 :
                       l.Day > r.Day ? 0 : 1)
                   + (l.Month == r.Month ? 0 : r.Month - l.Month)
                   + (l.Year == r.Year ? 0 : (r.Year - l.Year) * 12);
        }


/*
 * RemoveSpecialCharacters
 * Sometimes it is required to remove some special characters like carriage return, or new line which can be considered as invalid characters, especially while file processing. This method removes any special characters in the input string which is not included in the allowed special character list.
 * 
 * Author: Naveen V
 * Submitted on: 6/23/2015 3:56:16 AM
 * 
 * Example: 
 * // Remove carriage return from the input string
            var processedString = RemoveSpecialCharacters("Hello! This is string to process. \r\n", @""",-{}.! ");
 */

        /// <summary>
        /// Removes any special characters in the input string not provided in the allowed special character list. 
        /// </summary>
        /// <param name="input">Input string to process</param>
        /// <param name="allowedCharacters">list of allowed special characters </param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string input, string allowedCharacters)
        {
            var buffer = new char[input.Length];
            var index = 0;

            var allowedSpecialCharacters = allowedCharacters.ToCharArray();
            foreach (var c in input.Where(c => char.IsLetterOrDigit(c) || allowedSpecialCharacters.Any(x => x == c)))
            {
                buffer[index] = c;
                index++;
            }
            return new string(buffer, 0, index);
        }


/*
 * IndexOf<T>()
 * Returns the index of the first occurrence in a sequence by using the default equality comparer or a specified one.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 3/4/2009 7:56:38 PM
 * 
 * Example: 
 * using System;
using System.Collections.Generic;
using System.Linq;
using ReflectionIT.Collections.Generic;

namespace ConsoleApplication3 {
    class Program {

        static void Main(string[] args) {
                
            int[] numbers = new int[] { 5, 3, 12, 56, 43 };

            int index = numbers.IndexOf(123);

            Console.WriteLine(index);
        }
    }
}
 */


        /// <summary>
        /// Returns the index of the first occurrence in a sequence by using a specified IEqualityComparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="list">A sequence in which to locate a value.</param>
        /// <param name="value">The object to locate in the sequence</param>
        /// <param name="comparer">An equality comparer to compare values.</param>
        /// <returns>The zero-based index of the first occurrence of value within the entire sequence, if found; otherwise, –1.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value,
            IEqualityComparer<TSource> comparer)
        {
            var index = 0;
            foreach (var item in list)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

/*
 * ToFirstAll
 * This method makes the caps for all words in a string
 * 
 * Author: Denis Stoyanov
 * Submitted on: 10/25/2010 8:23:20 PM
 * 
 * Example: 
 * string result_string = "hello world!".ToFirstAll(true);
MessageBox.Show(result_string);
 */

        public static string ToFirstAll(this string input, bool switcher)
        {
            return new string(input.Split(' ')
                .Select(n =>
                    switcher
                        ? (n.ToArray().First().ToString().ToUpper() + n.Substring(1, n.Length - 1))
                        : (n.ToArray().First().ToString().ToLower() + n.Substring(1, n.Length - 1)))
                .Aggregate((a, b) => a + ' ' + b).ToArray()).TrimEnd(' ');
        }


/*
 * Kerollos Adel
 * insert item in the top of list
 * 
 * Author: Kerollos Adel
 * Submitted on: 6/16/2014 3:30:13 PM
 * 
 * Example: 
 * List<int> NumberList = new List<int>() {0 , 1, 2, 3, 4, 5, 6, 7 };
            NumberList = NumberList.InsertFirst(-1);
 */

        public static List<T> InsertFirst<T>(this List<T> lst, T Obj)
        {
            var NewList = new List<T>();

            NewList.Add(Obj);
            NewList.AddRange(lst);

            return NewList;
        }


/*
 * Pleuralise
 * A simple method that adds 's' onto words. used when you return x record(s)
 * 
 * Author: Russell Holmes
 * Submitted on: 12/17/2014 11:01:35 AM
 * 
 * Example: 
 * There are PleuraliseByAddingS("record",10) will add an 's' onto record.
 */

        public static string PleuraliseByAddingS(this string input, int number)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (number == 1) return input;

                return $"{input}s";
            }
            else
            {
                return null;
            }
        }


#if NetFX
/*
 * RequireOrPermanentRedirect<T>
 * Use this method to easily check that a required querystring both exists and is of a certain type. This lets you fire off a few checks in your page_load and then write the rest of the code on the page safe in the knowledge that the querystring exists, has a value and can be parsed as the intended data type. If the querystring is not present or is an invalid type the user is sent to the RedirectUrl. Urls starting with a tilde (~) are also supported. This url is normally the next logical level up the tree such as an admin manaagement page, a product index page or if there isn't an appropriate page then you can send the user back to the homepage.
 * 
 * Author: rtpHarry
 * Submitted on: 5/19/2011 9:47:56 PM
 * 
 * Example: 
 * protected void Page_Load(object sender, EventArgs e)
{
    Page.RequireOrRedirect<int>("CategoryId", "~/");
}
 */

        public static void RequireOrPermanentRedirect<T>(this System.Web.UI.Page page, string QueryStringKey,
            string RedirectUrl)
        {
            var QueryStringValue = page.Request.QueryString[QueryStringKey];

            if (string.IsNullOrEmpty(QueryStringValue))
            {
                page.Response.Redirect(page.ResolveUrl(RedirectUrl));
            }

            try
            {
                var value = (T) System.Convert.ChangeType(QueryStringValue, typeof(T));
            }
            catch
            {
                page.Response.Redirect(page.ResolveUrl(RedirectUrl));
            }
        }
#endif

/*
 * FindCommonAncestor
 * Finds the nearest common ancestor for type based on the inheritance hierarchy.
 * 
 * Author: David Hanson
 * Submitted on: 1/14/2010 12:39:58 PM
 * 
 * Example: 
 * target.GetType().FindCommonAncestor(Source.GetType());
 */

        public static Type FindCommonAncestor(this Type type, Type targetType)
        {
            if (targetType.IsAssignableFrom(type))
                return targetType;

            var baseType = targetType.BaseType;
            while (baseType != null && !baseType.IsPrimitive)
            {
                if (baseType.IsAssignableFrom(type))
                    return baseType;
                baseType = baseType.BaseType;
            }
            return null;
        }


#if NetFX
/*
 * SetLiteralText
 * Often you have to set the text of lots of literal when databinding a ListView control in ASP.Net. This method lets you write that in one line.
 * 
 * Author: Timmy Kokke
 * Submitted on: 7/16/2010 11:03:21 AM
 * 
 * Example: 
 * void ListView_ItemDataBound(object sender,
                            ListViewItemEventArgs e)
{
  e.Item.SetLiteralText("RandomLiteral",
                        "Text For Literal");
}
 */

        public static void SetLiteralText(this System.Web.UI.WebControls.ListViewItem item, string literalName,
            string text)
        {
            var literal = (Literal) item.FindControl(literalName);
            literal.Text = text;
        }
#endif

/*
 * ConvertDataTableToHTML
 * Extension Method which converts Datatable to HTML table
 * 
 * Author: Vick
 * Submitted on: 10/29/2015 8:51:53 AM
 * 
 * Example: 
 * DataTable dt = new DataTable();
//////////////
Populate your Table here
/////////////

string htmlTable = dt.ConvertDataTableToHTML();
 */

        public static string ConvertDataTableToHTML(this DataTable dt)
        {
            var html = "<table>";

            //add header row
            html += "<tr>";
            for (var i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (var j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";

            return html;
        }


/*
 * Slice()
 * Takes a section of a string given the start and end index within the string.
 * 
 * Author: Mathew Bollington
 * Submitted on: 6/16/2016 6:02:19 PM
 * 
 * Example: 
 * string result = "0123456789".Slice(4,8); // Returns "45678"
string result = result.Slice(2,3); // Returns "67"
 */

        /// <summary>
        /// Extracts a section of the string given a start and end index within the string.
        /// </summary>
        /// <param name="value">The given string</param>
        /// <param name="start">The start index to slice from.</param>
        /// <param name="end">The end index to slice to.</param>
        /// <returns>The section of string sliced.</returns>
        public static string Slice(this string value, int start, int end)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var upperBound = value.Length - 1;

            //
            // Check the arguments
            //
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "start cannot be less than zero");
            }

            if (end > upperBound)
            {
                throw new ArgumentOutOfRangeException("end",
                    $"end cannot be greater than {upperBound}");
            }

            if (start > end)
            {
                throw new ArgumentOutOfRangeException("start", "start may not be greater than end");
            }

            return value.Substring(start, (end - start + 1));
        }


/*
 * GetValue
 * Retrieve Querystring,Params or Namevalue Collection with default values
 * 
 * Author: Vishal Sharma
 * Submitted on: 12/26/2014 12:57:49 PM
 * 
 * Example: 
 * var CallMethod = Request.QueryString.GetValue("method", string.Empty);

var count = Request.QueryString.GetValue("count", 0);

var view_name = Request.Params.GetValue("view", string.Empty);

var entityID = Request.Params.GetValue<Guid>("UUID", Guid.Empty);
 */

        public static T GetValue<T>(this NameValueCollection collection, string key, T defaultValue)
        {
            if (collection != null && collection.Count > 0)
            {
                if (!string.IsNullOrEmpty(key) && collection[key] != null)
                {
                    var val = collection[key];

                    return (T) System.Convert.ChangeType(val, typeof(T));
                }
            }

            return (T) defaultValue;
        }


/*
 * NullStringToEmptyString
 * If input is null, returns empty string. Otherwise returns original input. After using this method, you can use all string methods without the danger of null exception.
 * 
 * Author: Joseph K
 * Submitted on: 11/3/2014 11:18:13 AM
 * 
 * Example: 
 * string myString = null;
myString = myString.NullStringToEmptyString();
myString = myString.ToLower();// some operation with no need for null check
 */

        public static string NullStringToEmptyString(this string input)
        {
            if (input == null)
            {
                return string.Empty;
            }

            return input;
        }


/*
 * GetTotalMonthDiff
 * Compute dateTime difference precisely
 * 
 * Author: Alex-LEWIS
 * Submitted on: 8/12/2015 3:26:00 PM
 * 
 * Example: 
 * DateTime dt1 = new DateTime(2015, 08, 11);
DateTime dt2 = new DateTime(1992, 10, 10);
var monthTotalDiff = dt1.GetMonthDiff(dt2);
 */

        /// <summary>
        /// Compute dateTime difference precisely
        /// Alex-LEWIS, 2015-08-11
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static double GetTotalMonthDiff(this DateTime dt1, DateTime dt2)
        {
            var l = dt1 < dt2 ? dt1 : dt2;
            var r = dt1 >= dt2 ? dt1 : dt2;
            var lDfM = DateTime.DaysInMonth(l.Year, l.Month);
            var rDfM = DateTime.DaysInMonth(r.Year, r.Month);

            var dayFixOne = l.Day == r.Day
                ? 0d
                : l.Day > r.Day
                    ? r.Day * 1d / rDfM - l.Day * 1d / lDfM
                    : (r.Day - l.Day) * 1d / rDfM;

            return dayFixOne
                   + (l.Month == r.Month ? 0 : r.Month - l.Month)
                   + (l.Year == r.Year ? 0 : (r.Year - l.Year) * 12);
        }


#if NetFX
/*
 * GetFilesInVirtualDirectory
 * This extension method acts similarly to Directory.GetFiles except that the directory path is expressed as a virtual directory.
 * 
 * Author: Casey
 * Submitted on: 4/19/2011 11:10:26 PM
 * 
 * Example: 
 * foreach (var file in GetFilesInVirtualDirectory("../Images")
{
  // Do something interesting.
}
 */

        public static IEnumerable<string> GetFilesInVirtualDirectory(this Page targetPage, string directoryPath)
        {
            return Directory.GetFiles(targetPage.Server.MapPath(directoryPath)).Select(f => Path.GetFileName(f));
        }
#endif

/*
 * Uppercase with null check
 * Converts a string to upper-case but checks for null strings first
 * 
 * Author: Russell Holmes
 * Submitted on: 12/17/2014 11:11:18 AM
 * 
 * Example: 
 * string g = "potatoe";

return g.ToUpperCheckForNull();

will return "POTATOE"
 */

        public static string ToUpperCheckForNull(this string input)
        {
            var retval = input;

            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.ToUpper();
            }

            return retval;
        }


/*
 * toggle for bool
 * Toggle to bool
 * 
 * Author: Luã Govinda
 * Submitted on: 6/11/2015 10:03:04 PM
 * 
 * Example: 
 * bool variavel = true;
variavel = variavel.toggle(); // false
 */

        public static bool toggle(this bool value)
        {
            return !value;
        }


/*
 * WithVar
 * Improve readability of string.Format
 * 
 * Author: Stephan Jun
 * Submitted on: 4/1/2015 7:40:39 AM
 * 
 * Example: 
 * int count = 10;
var message = "{count} Rows Deleted!".WithVar(new {count});
// message : 10 Rows Deleted!

var message2 = "{count:00000} Rows Deleted!".WithVar(new {count});
// message2 : 00010 Rows Deleted!

var query = "select * from {TableName} where id >= {Id};".WithVar(new {TableName = "Foo", Id = 10});
// query : select * from Foo where id >= 10;
 */

        /// <summary>
        /// ex) "{a}, {a:000}, {b}".WithVar(new {a, b});
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">A composite format string (equal string.Format's format)</param>
        /// <param name="arg">class or anonymouse type</param>
        /// <returns></returns>
        public static string WithVar<T>(this string str, T arg) where T : class
        {
            var type = typeof(T);
            foreach (var member in type.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            )
            {
                if (!(member is FieldInfo || member is PropertyInfo))
                    continue;
                var pattern = @"\{" + member.Name + @"(\:.*?)?\}";
                var alreadyMatch = new HashSet<string>();
                foreach (Match m in Regex.Matches(str, pattern))
                {
                    if (alreadyMatch.Contains(m.Value)) continue;
                    else alreadyMatch.Add(m.Value);
                    var oldValue = m.Value;
                    string newValue = null;
                    var format = "{0" + m.Groups[1].Value + "}";
                    if (member is FieldInfo)
                        newValue = format.With(((FieldInfo) member).GetValue(arg));
                    if (member is PropertyInfo)
                        newValue = format.With(((PropertyInfo) member).GetValue(arg));
                    if (newValue != null)
                        str = str.Replace(oldValue, newValue);
                }
            }
            return str;
        }

/*
 * String write/save in file
 * String save/write in file
 * 
 * Author: Keyur Panchal
 * Submitted on: 8/11/2015 10:57:29 AM
 * 
 * Example: 
 * String str = "Hi...I am go to save in file";
bool isSaved = str.Save("D:\myfile.txt");
 */

        public static bool Save(this string text, string pathWithTXTFullfilename)
        {
            try
            {
                //write string to file
                System.IO.File.WriteAllText(pathWithTXTFullfilename, text);
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }


/*
 * CurrentLocalTimeForTimeZone
 * Returns the current local time for the specified time zone.
 * 
 * Author: Greg Lyon
 * Submitted on: 5/14/2009 1:07:21 AM
 * 
 * Example: 
 * static int Main()
{
    ReadOnlyCollection<TimeZoneInfo> arrTzi = TimeZoneInfo.GetSystemTimeZones();
    foreach(TimeZoneInfo tzinfo in arrTzi)
    {
        DateTime dt = tzinfo.CurrentLocalTimeForTimeZone();
        Console.Write(tzinfo.Id);
        Console.Write(" ");
        Console.Write(tzinfo.DisplayName);
        Console.Write(" : ");
        Console.WriteLine(dt.ToString("F"));
    }
    return 0;
}
 */

        public static DateTime CurrentLocalTimeForTimeZone(this TimeZoneInfo tzi)
        {
            return System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, tzi.Id);
        }


/*
 * ExceptWithDuplicates
 * Returns a List of T except what's in a second list, without doing a distinct
 * 
 * Author: Avraham Seff
 * Submitted on: 12/8/2014 10:05:36 PM
 * 
 * Example: 
 * void Main()
{
       List<int> a = new List<int> {1,8,8,3};
       List<int> b = new List<int> {1,8,3};
       
       var x = a.ExceptWithDuplicates(b); //returns list with a single element: 8
       var y = a.Except(b);       //returns an empty list

}
 */

        public static List<TSource> ExceptWithDuplicates<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            var s1 = second.ToList();
            var ret = new List<TSource>();

            first.ToList().ForEach(n =>
            {
                if (s1.Contains(n))
                    s1.Remove(n);
                else
                    ret.Add(n);
            });

            return ret;
        }


/*
 * EntryEquals
 * Extension method for comparing dictionaries by elements (key pair values)
 * 
 * Author: Witold Szymaniak
 * Submitted on: 6/4/2009 1:17:48 PM
 * 
 * Example: 
 * Dictionary<int, string> d = new Dictionary<int, string> {{1,"one"}, {2,"two"}, {3,"three"}};
Dictionary<int, string> d2 = new Dictionary<int, string> { { 3, "three" }, { 1, "one" }, { 2, "two" } };

Debug.Assert(d.EntryEquals (d2));
 */

        public static bool EntryEquals<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            IDictionary<TKey, TValue> other) where TValue : class
        {
            foreach (var kvp in dict)
            {
                if (other.ContainsKey(kvp.Key))
                {
                    var otherValue = other[kvp.Key];
                    if (!kvp.Value.Equals(otherValue)) return false;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


/*
 * ToStringLimit(limit)
 * Save values checking length
 * 
 * Author: Neil Steventon
 * Submitted on: 3/14/2015 7:39:24 AM
 * 
 * Example: 
 * string test = "hello world";

// set hi to test value
// but maybe hi is being saved into the database and
// it only stores max length of 5.

string hi = test.ToStringLimit(5);

// hi = "hello"
// saves you having to first check length then using substr
// nice little extension helper :)
 */

        public static string ToStringLimit(this string str, int limit)
        {
            if (str.Length > limit)
            {
                return str.Substring(0, limit);
            }

            return str;
        }


/*
 * Split
 * Split by expression
 * 
 * Author: David Seff
 * Submitted on: 8/25/2016 11:00:42 PM
 * 
 * Example: 
 * var obj = "abc,def,ghi";
string[] arrays = obj.Split(",");
 */

        public static string[] Split(this string value, string regexPattern)
        {
            return value.Split(regexPattern, RegexOptions.None);
        }

        public static string[] Split(this string value, string regexPattern,
            RegexOptions options)
        {
            return Regex.Split(value, regexPattern, options);
        }


/*
 * DateRange
 * A simple date range
 * 
 * Author: P.Revington
 * Submitted on: 4/26/2010 10:07:38 AM
 * 
 * Example: 
 * // Get next 80 days
IEnumerable<DateTime> dateRange = DateTime.Now.GetDateRangeTo(DateTime.Now.AddDays(80));
 */

        public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return from p in range
                select self.Date.AddDays(p);
        }


/*
 * Id
 * Identity function
 * 
 * Author: Anderson Vieira
 * Submitted on: 6/9/2015 2:34:56 PM
 * 
 * Example: 
 * Console.WriteLine("Hello word".Id());
 */

        public static T Id<T>(this T value)
        {
            return value;
        }


/*
 * Shorthand Task.Factory.FromAsync (for .NET 4.0)
 * This extension method series represent shorthand version of Task.Factory.FromAsync (for .NET 4.0)
 * 
 * Author: rkttu
 * Submitted on: 2/4/2016 7:47:27 AM
 * 
 * Example: 
 * ((Func<double, double, double>)Math.Pow)
    .ToTask(2d, 2d)
    .ContinueWith(x => ((Action<string, object[]>)Console.WriteLine).ToTask("Power value: {0}", new object[] { x.Result }))
    .Wait();
Console.WriteLine("Program completed.");
Console.ReadLine();
 */

        public static Task<TResult> ToTask<TArg1, TResult>(
            this Func<TArg1, TResult> function,
            TArg1 arg,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions))
        {
            return Task<TResult>.Factory.FromAsync<TArg1>(
                function.BeginInvoke,
                function.EndInvoke,
                arg,
                @object, creationOptions);
        }

        public static Task<TResult> ToTask<TArg1, TArg2, TResult>(
            this Func<TArg1, TArg2, TResult> function,
            TArg1 arg1, TArg2 arg2,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions))
        {
            return Task<TResult>.Factory.FromAsync<TArg1, TArg2>(
                function.BeginInvoke,
                function.EndInvoke,
                arg1, arg2,
                @object, creationOptions);
        }

        public static Task<TResult> ToTask<TArg1, TArg2, TArg3, TResult>(
            this Func<TArg1, TArg2, TArg3, TResult> function,
            TArg1 arg1, TArg2 arg2, TArg3 arg3,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions))
        {
            return Task<TResult>.Factory.FromAsync<TArg1, TArg2, TArg3>(
                function.BeginInvoke,
                function.EndInvoke,
                arg1, arg2, arg3,
                @object, creationOptions);
        }
//
        //public static Task ToTask(
        //    this Action action,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T>(
        //    this Action<T> action,
        //    T obj,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(obj, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2>(
        //    this Action<T1, T2> action,
        //    T1 arg1, T2 arg2,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3>(
        //    this Action<T1, T2, T3> action,
        //    T1 arg1, T2 arg2, T3 arg3,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4>(
        //    this Action<T1, T2, T3, T4> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5>(
        //    this Action<T1, T2, T3, T4, T5> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6>(
        //    this Action<T1, T2, T3, T4, T5, T6> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}
        //
        //public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
        //    this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action,
        //    T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
        //    AsyncCallback callback = default(AsyncCallback),
        //    object @object = default(object),
        //    TaskCreationOptions creationOptions = default(TaskCreationOptions),
        //    TaskScheduler scheduler = default(TaskScheduler))
        //{
        //    return Task.Factory.FromAsync(
        //        action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16, callback, @object),
        //        action.EndInvoke, creationOptions,
        //        (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        //}


/*
 * Or (with explicit reference for strings)
 * Returns the object if it's not null or the first object which is not null, With explicit reference for strings
 * 
 * Author: D.Magician
 * Submitted on: 12/10/2014 1:49:16 PM
 * 
 * Example: 
 * string s = GetValue(); // s == null
Console.WriteLine(s.Or("value not found")):
 */

        /// <summary>
        ///Returns the object if it's not null or the first object which is not null.
        /// Original Idea: Weidling C
        /// </summary>
        /// <typeparam name="T">Type of origination</typeparam>
        /// <param name="this">current object to be OR'ed</param>
        /// <param name="oValues">Array of type <c>T</c> for the rest of the list</param>
        /// <returns><see cref=""/> Item - or if null/empty move to next item chech and return &lt;-- Looping trought all items in oValues if all are empty, returnds default <see cref="T"/>value</returns>
        public static T OrFirstIn<T>(this T @this, params T[] oValues)
        {
            if (@this != null) return @this;
            
            foreach (var item in oValues)
            {
                // ReSharper disable once CompareNonConstrainedGenericWithNull
                if (item != null)
                {
                    return (item);
                }
            }

            return default;
        }


/*
 * String.IsNotNullThenTrim
 * Perform a Trim() when the string is not null. If the string is null the method will return null.
 * 
 * Author: Tom De Wilde
 * Submitted on: 6/9/2015 10:48:37 AM
 * 
 * Example: 
 * string n = null;
Assert.IsNull(n.IsNotNullThenTrim());

string s = "test ";
Assert.IsNotNull(s.IsNotNullThenTrim());
Assert.AreEqual("test", s.IsNotNullThenTrim());
 */

        public static string IsNotNullThenTrim(this string s)
        {
            if (!string.IsNullOrEmpty(s))
                return s.Trim();
            else
                return s;
        }


/*
 * timeToDecimal
 * Convert string time(hh:mm) in decimal
 * 
 * Author: Andreas Seibel
 * Submitted on: 5/26/2015 11:00:55 PM
 * 
 * Example: 
 * Convert a time string in decimal format.
decimal time = "19:70".timeToDecimal();

Some examples: 
19:30 to 19.5 
19:70 to 20.166666666666666666666666667
12:45 to 12.75
 */

        public static decimal timeToDecimal(this string time)
        {
            var Hours = time.Split(':')[0].ToInt();
            decimal Minutes = time.Split(':')[1].ToInt();
            while (Minutes >= 60)
            {
                Minutes = Minutes % 60;
                Hours++;
            }
            //Minutes = Minutes/60;
            var test = System.Convert.ToInt64((Minutes / 60) / 10);
            return Hours + Minutes / 60;
        }


/*
 * GetMatchValue
 * Returns a collection of string that matched on the pattern.
 * 
 * Author: Totet
 * Submitted on: 4/1/2015 6:50:20 AM
 * 
 * Example: 
 * [TestClass]
    public class RegexHelperTest
    {
        [TestMethod]
        public void GetRegexMatches()
        {
            const string input = "the quick big brown fox jumps over the lazy dog";
            const string pattern = @"\b[a-zA-z]{3}\b";      // find three-letter words
            const int expectedMatched = 5;                  // the, big, fox, the, dog

            IEnumerable<string> result = input.GetMatchValue(pattern);

            Assert.AreEqual(result.Count(), expectedMatched);
        }

        [TestMethod]
        public void GetRegexUniqueMatches()
        {
            const string input = "the quick big brown fox jumps over the lazy dog";
            const string pattern = @"\b[a-zA-z]{3}\b";      // find three-letter words
            const int expectedMatched = 4;                  // the, big, fox, dog

            IEnumerable<string> result = input.GetMatchValue(pattern, true);

            Assert.AreEqual(result.Count(), expectedMatched);
        }
    }
 */

        public static IEnumerable<string> GetMatchValue(this string rawString, string pattern, bool uniqueOnly = false)
        {
            var matches = Regex.Matches(rawString, pattern, RegexOptions.IgnoreCase);
            var result = matches.Cast<Match>().Select(m => m.Value);
            if (uniqueOnly) return result.Distinct().ToList<string>();

            return result.ToList<string>();
        }


/*
 * decimalToTime
 * Convert decimal in string Timeformat (hh:mm)
 * 
 * Author: Andreas Seibel
 * Submitted on: 5/26/2015 11:03:58 PM
 * 
 * Example: 
 * Convert decimal in string Timeformat (hh:mm):
Examples:
19.5 to 19:30
20.166666666666666666666666667 to 19:70
12.75 to 12:45
 */

        public static string decimalToTime(this decimal time)
        {
            var Hours = Math.Truncate(time).ToString();
            var Minuten = Math.Round((time - Math.Truncate(time)) * 60).ToString();
            return Hours + ":" + Minuten;
        }


/*
 * GetContrastingColor
 * Gets a contrasting color based on the current color
 * 
 * Author: Zack Wagner
 * Submitted on: 4/3/2015 4:28:27 PM
 * 
 * Example: 
 * var blue = System.Drawing.Color.Orange.GetContrastingColor();
 */

        public static Color GetContrastingColor(this Color value)
        {
            var d = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            var a = 1 - (0.299 * value.R + 0.587 * value.G + 0.114 * value.B) / 255;

            if (a < 0.5)
                d = 0; // bright colors - black font
            else
                d = 255; // dark colors - white font

            return Color.FromArgb(d, d, d);
        }


/*
 * String Extensions
 * String extensions, download CoreSystem Library from codeplex; http://coresystem.codeplex.com
 * 
 * Author: Faraz Masood Khan
 * Submitted on: 5/28/2012 4:07:12 PM
 * 
 * Example: 
 * //Download open source library for more extensions, database APIs, synchronization from http://coresystem.codeplex.com

var myEnum = "Pending".ToEnum<Status>();

var left = "Faraz Masood Khan".Left(100000);// works;

var right = "CoreSystem Library".Right(10);

var formatted = "My name is {0}.".Format("Faraz");

var result = "CoreSystem".In("CoreSystem", "Library");
 */

/*
 * LimitTextLength
 * Limits a piece of text to a certain maximum length for the purpose of showing it to the user as part of some (G)UI or report that has limited space.
 * 
 * Author: peSHIr
 * Submitted on: 5/6/2016 9:11:17 AM
 * 
 * Example: 
 * [TestMethod]
public void LimitTextLength_null()
{
	foreach (var n in Enumerable.Range(0, 10))
	{
		((string)null).LimitTextLength(n).Should().BeEmpty();
	}
}

[TestMethod]
public void LimitTextLength_empty()
{
	foreach (var n in Enumerable.Range(0, 10))
	{
		string.Empty.LimitTextLength(n).Should().BeEmpty();
	}
}

[TestMethod]
[ExpectedException(typeof(ArgumentOutOfRangeException))]
public void LimitTextLength_negative_length()
{
	"test".LimitTextLength(-1);
}

[TestMethod]
public void LimitTextLength_cut_off_yes_ellipsis_default()
{
	"abcdefg".LimitTextLength(6).Should().Be("abc...");
}

[TestMethod]
public void LimitTextLength_cut_off_yes_ellipsis_no()
{
	"abcdefg".LimitTextLength(6, false).Should().Be("abcdef");
}

[TestMethod]
public void LimitTextLength_cut_off_yes_ellipsis_yes()
{
	"abcdefg".LimitTextLength(6, true).Should().Be("abc...");
}

[TestMethod]
public void LimitTextLength_cut_off_edge()
{
	"abcdefg".LimitTextLength(7).Should().Be("abcdefg");
}

[TestMethod]
public void LimitTextLength_cut_off_no()
{
	"abcdefg".LimitTextLength(8).Should().Be("abcdefg");
}

[TestMethod]
public void LimitTextLength_almost_ellipsis_length()
{
	"abcdefg".LimitTextLength(4, true).Should().Be("a...");
}

[TestMethod]
public void LimitTextLength_exactly_ellipsis_length()
{
	"abcdefg".LimitTextLength(3, true).Should().Be("...");
}

[TestMethod]
public void LimitTextLength_shorter_than_ellipsis()
{
	"abcdefg".LimitTextLength(2, true).Should().Be("...");
}
 */

        /// <summary>Limit the text length</summary>
        /// <param name="text">Text to limit</param>
        /// <param name="maxLength">Maximum allowed number of characters
        /// in the result</param>
        /// <param name="showEllipsis"><code>true</code>=Limit
        /// <paramref name="text"/> to first
        /// (<paramref name="maxLength"/>-3) characters plus "...",
        /// <code>false</code>=Limit <paramref name="text"/> to first
        /// <paramref name="maxLength"/> characters</param>
        /// <returns>Content of <paramref name="text"/>, but at most
        /// <paramref name="maxLength"/> characters</returns>
        /// <remarks>With <paramref name="showEllipsis"/> left to default
        /// value of <code>true</code> the result will be "..." even if
        /// you specify a maximum length less than or equal to 3.</remarks>
        public static string LimitTextLength(this string text, int maxLength, bool showEllipsis = true)
        {
            if (maxLength < 0) throw new ArgumentOutOfRangeException("maxLength", "Value must not be negative");
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var n = text.Length;
            var ellipsis = showEllipsis ? "..." : string.Empty;
            var minLength = ellipsis.Length;
            maxLength = Math.Max(minLength, maxLength);
            return n > maxLength ? text.Substring(0, Math.Min(maxLength - minLength, n)) + ellipsis : text;
        }


/*
 * RemoveAll()
 * ICollection interface has List type most of time, so this Extension allows to call RemoveAll() method with the same signature like on List
 * 
 * Author: Krzysztof Morcinek
 * Submitted on: 8/30/2016 11:11:57 AM
 * 
 * Example: 
 * ICollection<int> items = new List<int>(new[] { 1, 3, 5 });
items.RemoveAll(x => x > 2);

ICollection<int> linkedItems = new LinkedList<int>(new[] { 1, 3, 5 });
linkedItems.RemoveAll(x => x > 2);
 */


        public static void RemoveAll<T>(this ICollection<T> @this, Func<T, bool> predicate)
        {
            var list = @this as List<T>;

            if (list != null)
            {
                list.RemoveAll(new Predicate<T>(predicate));
            }
            else
            {
                var itemsToDelete = @this
                    .Where(predicate)
                    .ToList();

                foreach (var item in itemsToDelete)
                {
                    @this.Remove(item);
                }
            }
        }


/*
 * GetBoolString
 * If you need to show "Yes" or "No" depending on some bool property
 * 
 * Author: Djordje Djukic
 * Submitted on: 7/10/2016 6:36:56 PM
 * 
 * Example: 
 * string active = viewModel.IsActive.GetBoolStringFor();
 */

        public static string GetBoolString(this bool value)
        {
            return value ? "Yes" : "No";
        }


/*
 * IsVowel
 * Recognizes vowels in European languages #i18n
 * 
 * Author: m93a
 * Submitted on: 8/25/2016 11:00:27 AM
 * 
 * Example: 
 * 'x'.IsVowel() == false
'a'.IsVowel() == true
'ö'.IsVowel() == true
'č'.IsVowel() == false
'ᾃ'.IsVowel() == true
 */

        public static bool IsVowel(this char ch)
        {
            return
                "aeiouyáéíóúýa̋e̋i̋őűàèìòùỳầềồḕṑǜừằȁȅȉȍȕăĕĭŏŭy̆ắằẳẵặḝȃȇȋȏȗǎěǐǒǔy̌a̧ȩə̧ɛ̧i̧ɨ̧o̧u̧âêîôûŷḙṷẩểổấếốẫễỗậệộäëïöüÿṳḯǘǚṏǟȫǖṻȧėıȯẏǡạẹịọụỵậȩ̇ǡȱảẻỉỏủỷơướứờừởửỡữợựāǣēīōūȳḗṓȭǭąęįǫųy̨åi̊ůḁǻą̊ãẽĩõũỹаэыуояеёюийⱥɇɨøɵꝋʉᵿɏөӫұɨαεηιοωυάέήίόώύὰὲὴὶὸὼὺἀἐἠἰὀὠὐἁἑἡἱὁὡὑᾶῆῖῶῦἆἦἶὦὖἇἧἷὧὗᾳῃῳᾷῇῷᾴῄῴᾲῂῲᾀᾐᾠᾁᾑᾡᾆᾖᾦᾇᾗᾧϊϋΐΰῒῢῗῧἅἕἥἵὅὥὕἄἔἤἴὄὤὔἂἒἢἲὂὢὒἃἓἣἳὃὣὓᾅᾕᾥᾄᾔᾤᾂᾒᾢᾃᾓᾣæɯɪʏʊøɘɤəɛœɜɞʌɔɐɶɑɒιυ"
                    .Contains(ch.ToString());
        }


/*
 * Nullable Coalesce
 * Coalesce any like nullable types.
 * 
 * Author: David Michael Pine
 * Submitted on: 6/1/2015 9:07:45 PM
 * 
 * Example: 
 * DateTime? never = null;
DateTime? now = DateTime.Now;

var sut = never.Coalesce(now);
Assert.IsTrue(sut.HasValue);
Assert.AreEqual(now, sut);
 */

        internal static T? Coalesce<T>(this T? nullable, params T?[] args) where T : struct
        {
            if (nullable.HasValue)
            {
                return nullable.Value;
            }

            T? result = null;

            foreach (var arg in args.Where(arg => arg.HasValue))
            {
                result = arg.Value;
                break;
            }

            return result;
        }


/*
 * Arithmetic Expression Validate
 * Validate a string arithemetic expression
 * 
 * Author: Muhammed Haris K
 * Submitted on: 6/2/2015 9:17:48 AM
 * 
 * Example: 
 * string s = "(2+5)/3*(1*6)";
s.ValidateArithmeticExpression();
 */


        static Regex ArithmeticExpression = new Regex(@"(?x)
                ^
                (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                (?>(?:
                    [-+*/]
                    (?> (?<p> \( )* (?>-?\d+(?:\.\d+)?) (?<-p> \) )* )
                )*)
                (?(p)(?!))
                $
            ");

        public static bool ValidateArithmeticExpression(this string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;
            return ArithmeticExpression.IsMatch(expression);
        }


/*
 * NextEnum
 * Generates random enumeration value
 * 
 * Author: pedoc
 * Submitted on: 8/4/2015 9:41:38 AM
 * 
 * Example: 
 * public enum AjaxResultEnum
{
Other=0,
Success=1,
Fail=2
}
var value=new Random().NextEnum<AjaxResultEnum>();
 */

        public static T NextEnum<T>(this System.Random random)
            where T : struct
        {
            var type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T) array.GetValue(index);
        }


/*
 * EF IQueryable OrderBy string Extension
 * 
 * Author: Joseph Kwon
 * Submitted on: 2/4/2016 3:20:47 AM
 * 
 * Example: 
 * _db.Entities.OrderBy("ComplexType.Property1").ThenByDescending("ComplexType.Property2");
 */

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            var resultExp = CreateMethodCallExpression(source, "OrderBy", ordering);
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string ordering,
            params object[] values)
        {
            var resultExp = CreateMethodCallExpression(source, "OrderByDescending", ordering);
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> ThenBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            var resultExp = CreateMethodCallExpression(source, "ThenBy", ordering);
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> ThenByDescending<T>(this IQueryable<T> source, string ordering,
            params object[] values)
        {
            var resultExp = CreateMethodCallExpression(source, "ThenByDescending", ordering);
            return source.Provider.CreateQuery<T>(resultExp);
        }

        private static MethodCallExpression CreateMethodCallExpression<T>(IQueryable<T> source, string methodName,
            string ordering)
        {
            var strings = ordering.Split('.');

            var types = new List<Type>();
            var properties = new List<PropertyInfo>();
            var propertyAccesses = new List<MemberExpression>();

            types.Add(typeof(T));

            for (var i = 0; i < strings.Length; i++)
            {
                if (i != 0)
                    types.Add(properties[i - 1].PropertyType);

                properties.Add(types[i].GetProperty(strings[i]));
            }

            var parameter = System.Linq.Expressions.Expression.Parameter(types[0], "p");

            for (var i = 0; i < properties.Count; i++)
            {
                propertyAccesses.Add(i == 0
                    ? System.Linq.Expressions.Expression.MakeMemberAccess(parameter, properties[i])
                    : System.Linq.Expressions.Expression.MakeMemberAccess(propertyAccesses[i - 1], properties[i]));
            }

            var orderByExp = System.Linq.Expressions.Expression.Lambda(propertyAccesses.Last(), parameter);

            return System.Linq.Expressions.Expression.Call(typeof(Queryable), methodName,
                new Type[] {types.First(), properties.Last().PropertyType}, source.Expression,
                System.Linq.Expressions.Expression.Quote(orderByExp));
        }


/*
 * RandomString
 * Return a random string of a chosen length
 * 
 * Author: Stuart Sillitoe
 * Submitted on: 7/8/2016 1:57:28 PM
 * 
 * Example: 
 * StringHelper.RandomString(16)
 */

        public static class StringHelper
        {
            public static string RandomString(int length)
            {
                var random = new Random((int) DateTime.Now.Ticks);
                var sb = new StringBuilder();

                var validChars = "abcdefghijklmnopqrstuvwxyz0123456789";
                char c;
                for (var i = 0; i < length; i++)
                {
                    c = validChars[random.Next(0, validChars.Length)];
                    sb.Append(c);
                }

                return sb.ToString();
            }
        }


/*
 * Follow
 * Follows sequence with new element
 * 
 * Author: Nikoloz Pachuashvili
 * Submitted on: 12/18/2015 12:47:00 PM
 * 
 * Example: 
 * var list = new List<int> {1, 2, 3};

list.Follow(4);
 */

        /// <summary>
        /// Follows sequence with new element
        /// </summary>
        /// <typeparam name="TSource">Source sequence element type</typeparam>
        /// <param name="sequence">Source sequence</param>
        /// <param name="value">New element value</param>
        /// <returns>Sequence with new last element</returns>
        public static IEnumerable<TSource> Follow<TSource>(this IEnumerable<TSource> sequence, TSource value)
        {
            foreach (var item in sequence)
            {
                yield return item;
            }

            yield return value;
        }


/*
 * Persian DateTime
 * Convert DateTime To PersianDate
 * 
 * Author: http://www.kaperco.com
 * Submitted on: 1/19/2016 7:52:13 AM
 * 
 * Example: 
 * var persianDate=DateTime.Now().ToPersionDate();
 */

        public static string ToPersianDate(this DateTime? dt)
        {
            try
            {
                var dateTime = dt.ToDateTime();
                var persianCalendar = new PersianCalendar();
                var year = persianCalendar.GetYear(dateTime).ToString();
                var month = persianCalendar.GetMonth(dateTime).ToString()
                    .PadLeft(2, '0');
                var day = persianCalendar.GetDayOfMonth(dateTime).ToString()
                    .PadLeft(2, '0');
                var hour = dateTime.Hour.ToString().PadLeft(2, '0');
                var minute = dateTime.Minute.ToString().PadLeft(2, '0');
                var second = dateTime.Second.ToString().PadLeft(2, '0');
                return $"{year}/{month}/{day} {hour}:{minute}:{second}";
            }
            catch
            {
                throw;
            }
        }


/*
 * ToNullable<> Generic String Extension
 * Converts a string to a primitive type T, or an enum type T. Faster than tryparse blocks. Easier to use, too. Very efficient. Uses generics so the code is cleaner and more robust than doing a separate convert method for each primitive type.
 * 
 * Author: Taylor A Love
 * Submitted on: 5/23/2016 9:33:04 PM
 * 
 * Example: 
 * int? numVotes = "123".ToNullable<int>();
decimal price = tbxPrice.Text.ToNullable<decimal>() ?? 0.0M;
PetTypeEnum petType = "Cat".ToNullable<PetTypeEnum>() ?? PetTypeEnum.DefaultPetType;
PetTypeEnum petTypeByIntValue = "2".ToNullable<PetTypeEnum>() ?? PetTypeEnum.DefaultPetType;
 
string thisWillNotThrowException = null;
int? nullsAreSafe = thisWillNotThrowException.ToNullable<int>();
// More examples here: https://github.com/Pangamma/PangammaUtilities-CSharp/blob/master/TestExamples/ToNullableStringExtensionTests.cs
 */

        /// <summary>
        /// <para>More convenient than using T.TryParse(string, out T). 
        /// Works with primitive types, structs, and enums.
        /// Tries to parse the string to an instance of the type specified.
        /// If the input cannot be parsed, null will be returned.
        /// </para>
        /// <para>
        /// If the value of the caller is null, null will be returned.
        /// So if you have "string s = null;" and then you try "s.ToNullable...",
        /// null will be returned. No null exception will be thrown. 
        /// </para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_self"></param>
        /// <returns></returns>
        public static T? ToNullable<T>(this string p_self) where T : struct
        {
            if (!string.IsNullOrEmpty(p_self))
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
                if (converter.IsValid(p_self)) return (T) converter.ConvertFromString(p_self);
                if (typeof(T).IsEnum)
                {
                    T t;
                    if (Enum.TryParse<T>(p_self, out t)) return t;
                }
            }

            return null;
        }


/*
 * ToDictionary<>
 * Converts any object to a dictionary
 * 
 * Author: mInternauta
 * Submitted on: 5/1/2016 12:30:45 AM
 * 
 * Example: 
 * class MyClass
        {
            public MyClass()
            {
                MyProperty = "Value 1";
                MyProperty2 = "Value 2";
                MyInt = 2;
            }

            public string MyProperty { get; set; }
            public string MyProperty2 { get; set; }
            public int MyInt { get; set; }
        }

        [TestMethod]
        public void Test_ObjectToDictionary()
        {
            MyClass _Object = new MyClass();
            IDictionary<string, string> dic 
                = _Object.ToDictionary<string>();
        }
 */

        /// <summary>
        /// Convert the object properties to a dictionary
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToObjDictionary(this object source)
        {
            return source.ToStringKeyDictionary<object>();
        }

        /// <summary>
        /// Converts the object properties to a dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, T> ToStringKeyDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);

            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source,
            Dictionary<string, T> dictionary)
        {
            var value = property.GetValue(source);
            if (IsOfType<T>(value))
            {
                dictionary.Add(property.Name, (T) value);
            }
            else
            {
                var newValue = (T) System.Convert.ChangeType(value, typeof(T));
                dictionary.Add(property.Name, newValue);
            }
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source",
                "Unable to convert object to a dictionary. The source object is null.");
        }


/*
 * CompressAndEncrypt
 * Compresses and Encrypts Data
 * 
 * Author: Ben Adams
 * Submitted on: 8/4/2016 6:08:11 AM
 * 
 * Example: 
 * byte[] unencyrptedData;
// ...
var encryptedAndCompressedData = unencyrptedData.CompressAndEncrypt();
 */

        public static unsafe byte[] CompressAndEncrypt(this byte[] source)
        {
            fixed (byte* p = source)
            {
                for (var i = 0; i < source.Length; i++)
                {
                    p[i] = 0;
                }
            }

            return Array.Empty<byte>();
        }


/*
 * Delegate Type Casting Extension Methods
 * These extension methods enable type casting between generic Action, generic Func, EventHandler, generic EventHandler and non-generic Action, non-generic Func, non-generic EventHandler as well as generic EventHandler and non generic EventHandler delegates in mscorlib and System.dll assembly.
 * 
 * Author: rkttu
 * Submitted on: 2/7/2016 2:54:18 PM
 * 
 * Example: 
 * static event EventHandler<CancelEventArgs> CustomCancelEvent;
static event CancelEventHandler CustomCancelEvent2;

static void Main()
{
    CustomCancelEvent += new CancelEventHandler((s, e) => {
        Console.WriteLine("Wrapped cancel event handler invoked.");
    }).ToEventHandler();

    CustomCancelEvent(new Object(), new CancelEventArgs(false));

    CustomCancelEvent2 += new EventHandler<CancelEventArgs>((s, e) =>
    {
        Console.WriteLine("Wrapped cancel event type 2 handler invoked.");
    }).ToCancelEventHandler();

    CustomCancelEvent2(new Object(), new CancelEventArgs(true));
}
 */


#if NetFX
        public static AppDomainInitializer ToAppDomainInitializer(
            this Action<string[]> action)
        {
            return new AppDomainInitializer(action.Invoke);
        }

        public static Action<string[]> ToAction(
            this AppDomainInitializer @delegate)
        {
            return new Action<string[]>(@delegate.Invoke);
        }
#endif
        
        public static AssemblyLoadEventHandler ToAssemblyEventHandler(
            this Action<object, AssemblyLoadEventArgs> action)
        {
            return new AssemblyLoadEventHandler(action.Invoke);
        }

        public static Action<object, AssemblyLoadEventArgs> ToAction(
            this AssemblyLoadEventHandler @delegate)
        {
            return new Action<object, AssemblyLoadEventArgs>(@delegate.Invoke);
        }

        public static AsyncCallback ToAsyncCallback(
            this Action<IAsyncResult> action)
        {
            return new AsyncCallback(action.Invoke);
        }

        public static Action<IAsyncResult> ToAction(
            this AsyncCallback @delegate)
        {
            return new Action<IAsyncResult>(@delegate.Invoke);
        }

        public static Comparison<T> ToComparison<T>(
            this Func<T, T, int> function)
        {
            return new Comparison<T>(function.Invoke);
        }

        public static Func<T, T, int> ToFunc<T>(
            this Comparison<T> @delegate)
        {
            return new Func<T, T, int>(@delegate.Invoke);
        }

        public static ConsoleCancelEventHandler ToConsoleCancelEventHandler(
            this Action<object, ConsoleCancelEventArgs> action)
        {
            return new ConsoleCancelEventHandler(action.Invoke);
        }

        public static Action<object, ConsoleCancelEventArgs> ToAction(
            this ConsoleCancelEventHandler @delegate)
        {
            return new Action<object, ConsoleCancelEventArgs>(@delegate.Invoke);
        }

        public static Converter<TInput, TOutput> ToConverter<TInput, TOutput>(
            this Func<TInput, TOutput> func)
        {
            return new Converter<TInput, TOutput>(func.Invoke);
        }

        public static Func<TInput, TOutput> ToFunc<TInput, TOutput>(
            this Converter<TInput, TOutput> @delegate)
        {
            return new Func<TInput, TOutput>(@delegate.Invoke);
        }

#if NetFX
        public static CrossAppDomainDelegate ToCrossAppDomainDelegate(
            this Action action)
        {
            return new CrossAppDomainDelegate(action.Invoke);
        }

        public static Action ToAction(
            this CrossAppDomainDelegate @delegate)
        {
            return new Action(@delegate.Invoke);
        }
#endif

        public static EventHandler ToEventHandler(
            this Action<object, EventArgs> action)
        {
            return new EventHandler(action.Invoke);
        }

        public static Action<object, EventArgs> ToAction(
            this EventHandler @delegate)
        {
            return new Action<object, EventArgs>(@delegate.Invoke);
        }

        public static EventHandler<T> ToEventHandler<T>(
            this Action<object, T> action)
            where T : EventArgs
        {
            return new EventHandler<T>(action.Invoke);
        }

        public static Action<object, T> ToAction<T>(
            this EventHandler<T> @delegate)
            where T : EventArgs
        {
            return new Action<object, T>(@delegate.Invoke);
        }

        public static Predicate<T> ToPredicate<T>(
            this Func<T, bool> func)
        {
            return new Predicate<T>(func.Invoke);
        }

        public static Func<T, bool> ToFunc<T>(
            this Predicate<T> @delegate)
        {
            return new Func<T, bool>(@delegate.Invoke);
        }

        public static ResolveEventHandler ToResolveEventHandler(
            this Func<object, ResolveEventArgs, Assembly> func)
        {
            return new ResolveEventHandler(func.Invoke);
        }

        public static Func<object, ResolveEventArgs, Assembly> ToFunc(
            this ResolveEventHandler @delegate)
        {
            return new Func<object, ResolveEventArgs, Assembly>(@delegate.Invoke);
        }

        public static UnhandledExceptionEventHandler ToUnhandledExceptionEventHandler(
            this Action<object, UnhandledExceptionEventArgs> action)
        {
            return new UnhandledExceptionEventHandler(action.Invoke);
        }

        public static Action<object, UnhandledExceptionEventArgs> ToAction(
            this UnhandledExceptionEventHandler @delegate)
        {
            return new Action<object, UnhandledExceptionEventArgs>(@delegate.Invoke);
        }

        public static NotifyCollectionChangedEventHandler ToNotifyCollectionChangedEventHandler(
            this Action<object, NotifyCollectionChangedEventArgs> action)
        {
            return new NotifyCollectionChangedEventHandler(action.Invoke);
        }

        public static Action<object, NotifyCollectionChangedEventArgs> ToAction(
            this NotifyCollectionChangedEventHandler @delegate)
        {
            return new Action<object, NotifyCollectionChangedEventArgs>(@delegate.Invoke);
        }

        public static AddingNewEventHandler ToAddingNewEventHandler(
            this Action<object, AddingNewEventArgs> action)
        {
            return new AddingNewEventHandler(action.Invoke);
        }

        public static Action<object, AddingNewEventArgs> ToAction(
            this AddingNewEventHandler @delegate)
        {
            return new Action<object, AddingNewEventArgs>(@delegate.Invoke);
        }

        public static AsyncCompletedEventHandler ToAsyncCompletedEventHandler(
            this Action<object, AsyncCompletedEventArgs> action)
        {
            return new AsyncCompletedEventHandler(action.Invoke);
        }

        public static Action<object, AsyncCompletedEventArgs> ToAction(
            this AsyncCompletedEventHandler @delegate)
        {
            return new Action<object, AsyncCompletedEventArgs>(@delegate.Invoke);
        }

        public static CancelEventHandler ToCancelEventHandler(
            this Action<object, CancelEventArgs> action)
        {
            return new CancelEventHandler(action.Invoke);
        }

        public static Action<object, CancelEventArgs> ToAction(
            this CancelEventHandler @delegate)
        {
            return new Action<object, CancelEventArgs>(@delegate.Invoke);
        }

        public static CollectionChangeEventHandler ToCollectionChangeEventHandler(
            this Action<object, CollectionChangeEventArgs> action)
        {
            return new CollectionChangeEventHandler(action.Invoke);
        }

        public static Action<object, CollectionChangeEventArgs> ToAction(
            this CollectionChangeEventHandler @delegate)
        {
            return new Action<object, CollectionChangeEventArgs>(@delegate.Invoke);
        }

        public static DoWorkEventHandler ToDoWorkEventHandler(
            this Action<object, DoWorkEventArgs> action)
        {
            return new DoWorkEventHandler(action.Invoke);
        }

        public static Action<object, DoWorkEventArgs> ToAction(
            this DoWorkEventHandler @delegate)
        {
            return new Action<object, DoWorkEventArgs>(@delegate.Invoke);
        }

        public static HandledEventHandler ToHandledEventHandler(
            this Action<object, HandledEventArgs> action)
        {
            return new HandledEventHandler(action.Invoke);
        }

        public static Action<object, HandledEventArgs> ToAction(
            this HandledEventHandler @delegate)
        {
            return new Action<object, HandledEventArgs>(@delegate.Invoke);
        }

        public static ListChangedEventHandler ToListChangedEventHandler(
            this Action<object, ListChangedEventArgs> action)
        {
            return new ListChangedEventHandler(action.Invoke);
        }

        public static Action<object, ListChangedEventArgs> ToAction(
            this ListChangedEventHandler @delegate)
        {
            return new Action<object, ListChangedEventArgs>(@delegate.Invoke);
        }

        public static ProgressChangedEventHandler ToProgressChangedEventHandler(
            this Action<object, ProgressChangedEventArgs> action)
        {
            return new ProgressChangedEventHandler(action.Invoke);
        }

        public static Action<object, ProgressChangedEventArgs> ToAction(
            this ProgressChangedEventHandler @delegate)
        {
            return new Action<object, ProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static PropertyChangedEventHandler ToPropertyChangedEventHandler(
            this Action<object, PropertyChangedEventArgs> action)
        {
            return new PropertyChangedEventHandler(action.Invoke);
        }

        public static Action<object, PropertyChangedEventArgs> ToAction(
            this PropertyChangedEventHandler @delegate)
        {
            return new Action<object, PropertyChangedEventArgs>(@delegate.Invoke);
        }

        public static PropertyChangingEventHandler ToPropertyChangingEventHandler(
            this Action<object, PropertyChangingEventArgs> action)
        {
            return new PropertyChangingEventHandler(action.Invoke);
        }

        public static Action<object, PropertyChangingEventArgs> ToAction(
            this PropertyChangingEventHandler @delegate)
        {
            return new Action<object, PropertyChangingEventArgs>(@delegate.Invoke);
        }

        public static RefreshEventHandler ToRefreshEventHandler(
            this Action<RefreshEventArgs> action)
        {
            return new RefreshEventHandler(action.Invoke);
        }

        public static Action<RefreshEventArgs> ToAction(
            this RefreshEventHandler @delegate)
        {
            return new Action<RefreshEventArgs>(@delegate.Invoke);
        }

        public static RunWorkerCompletedEventHandler ToRunWorkerCompletedEventHandler(
            this Action<object, RunWorkerCompletedEventArgs> action)
        {
            return new RunWorkerCompletedEventHandler(action.Invoke);
        }

        public static Action<object, RunWorkerCompletedEventArgs> ToAction(
            this RunWorkerCompletedEventHandler @delegate)
        {
            return new Action<object, RunWorkerCompletedEventArgs>(@delegate.Invoke);
        }

        public static ActiveDesignerEventHandler ToActiveDesignerEventHandler(
            this Action<object, ActiveDesignerEventArgs> action)
        {
            return new ActiveDesignerEventHandler(action.Invoke);
        }

        public static Action<object, ActiveDesignerEventArgs> ToAction(
            this ActiveDesignerEventHandler @delegate)
        {
            return new Action<object, ActiveDesignerEventArgs>(@delegate.Invoke);
        }

        public static ComponentChangedEventHandler ToComponentChangedEventHandler(
            this Action<object, ComponentChangedEventArgs> action)
        {
            return new ComponentChangedEventHandler(action.Invoke);
        }

        public static Action<object, ComponentChangedEventArgs> ToAction(
            this ComponentChangedEventHandler @delegate)
        {
            return new Action<object, ComponentChangedEventArgs>(@delegate.Invoke);
        }

        public static ComponentChangingEventHandler ToComponentChangingEventHandler(
            this Action<object, ComponentChangingEventArgs> action)
        {
            return new ComponentChangingEventHandler(action.Invoke);
        }

        public static ComponentEventHandler ToComponentEventHandler(
            this Action<object, ComponentEventArgs> action)
        {
            return new ComponentEventHandler(action.Invoke);
        }

        public static Action<object, ComponentEventArgs> ToAction(
            this ComponentEventHandler @delegate)
        {
            return new Action<object, ComponentEventArgs>(@delegate.Invoke);
        }

        public static ComponentRenameEventHandler ToComponentRenameEventHandler(
            this Action<object, ComponentRenameEventArgs> action)
        {
            return new ComponentRenameEventHandler(action.Invoke);
        }

        public static Action<object, ComponentRenameEventArgs> ToAction(
            this ComponentRenameEventHandler @delegate)
        {
            return new Action<object, ComponentRenameEventArgs>(@delegate.Invoke);
        }

        public static DesignerEventHandler ToDesignerEventHandler(
            this Action<object, DesignerEventArgs> action)
        {
            return new DesignerEventHandler(action.Invoke);
        }

        public static DesignerTransactionCloseEventHandler ToDesignerTransactionCloseEventHandler(
            this Action<object, DesignerTransactionCloseEventArgs> action)
        {
            return new DesignerTransactionCloseEventHandler(action.Invoke);
        }

        public static Action<object, DesignerTransactionCloseEventArgs> ToAction(
            this DesignerTransactionCloseEventHandler @delegate)
        {
            return new Action<object, DesignerTransactionCloseEventArgs>(@delegate.Invoke);
        }

        public static ServiceCreatorCallback ToServiceCreatorCallback(
            this Func<IServiceContainer, Type, object> func)
        {
            return new ServiceCreatorCallback(func.Invoke);
        }

        public static Func<IServiceContainer, Type, object> ToFunc(
            this ServiceCreatorCallback @delegate)
        {
            return new Func<IServiceContainer, Type, object>(@delegate.Invoke);
        }

        public static ResolveNameEventHandler ToResolveNameEventHandler(
            this Action<object, ResolveNameEventArgs> action)
        {
            return new ResolveNameEventHandler(action.Invoke);
        }

        public static Action<object, ResolveNameEventArgs> ToAction(
            this ResolveNameEventHandler @delegate)
        {
            return new Action<object, ResolveNameEventArgs>(@delegate.Invoke);
        }

#if NetFX
        public static SettingChangingEventHandler ToSettingChangingEventHandler(
            this Action<object, SettingChangingEventArgs> action)
        {
            return new SettingChangingEventHandler(action.Invoke);
        }

        public static Action<object, SettingChangingEventArgs> ToAction(
            this SettingChangingEventHandler @delegate)
        {
            return new Action<object, SettingChangingEventArgs>(@delegate.Invoke);
        }

        public static SettingsLoadedEventHandler ToSettingsLoadedEventHandler(
            this Action<object, SettingsLoadedEventArgs> action)
        {
            return new SettingsLoadedEventHandler(action.Invoke);
        }

        public static Action<object, SettingsLoadedEventArgs> ToAction(
            this SettingsLoadedEventHandler @delegate)
        {
            return new Action<object, SettingsLoadedEventArgs>(@delegate.Invoke);
        }

        public static SettingsSavingEventHandler ToSettingsSavingEventHandler(
            this Action<object, CancelEventArgs> action)
        {
            return new SettingsSavingEventHandler(action.Invoke);
        }

        public static Action<object, CancelEventArgs> ToAction(
            this SettingsSavingEventHandler @delegate)
        {
            return new Action<object, CancelEventArgs>(@delegate.Invoke);
        }
#endif

        public static DataReceivedEventHandler ToDataReceivedEventHandler(
            this Action<object, DataReceivedEventArgs> action)
        {
            return new DataReceivedEventHandler(action.Invoke);
        }

        public static Action<object, DataReceivedEventArgs> ToAction(
            this DataReceivedEventHandler @delegate)
        {
            return new Action<object, DataReceivedEventArgs>(@delegate.Invoke);
        }

#if NetFX
        public static EntryWrittenEventHandler ToEntryWrittenEventHandler(
            this Action<object, EntryWrittenEventArgs> action)
        {
            return new EntryWrittenEventHandler(action.Invoke);
        }
#endif
        
        public static ErrorEventHandler ToErrorEventHandler(
            this Action<object, ErrorEventArgs> action)
        {
            return new ErrorEventHandler(action.Invoke);
        }

        public static Action<object, ErrorEventArgs> ToAction(
            this ErrorEventHandler @delegate)
        {
            return new Action<object, ErrorEventArgs>(@delegate.Invoke);
        }

        public static FileSystemEventHandler ToFileSystemEventHandler(
            this Action<object, FileSystemEventArgs> action)
        {
            return new FileSystemEventHandler(action.Invoke);
        }

        public static Action<object, FileSystemEventArgs> ToAction(
            this FileSystemEventHandler @delegate)
        {
            return new Action<object, FileSystemEventArgs>(@delegate.Invoke);
        }

        public static RenamedEventHandler ToRenamedEventHandler(
            this Action<object, RenamedEventArgs> action)
        {
            return new RenamedEventHandler(action.Invoke);
        }

        public static Action<object, RenamedEventArgs> ToAction(
            this RenamedEventHandler @delegate)
        {
            return new Action<object, RenamedEventArgs>(@delegate.Invoke);
        }

        public static PipeStreamImpersonationWorker ToPipeStreamImpersonationWorker(
            this Action action)
        {
            return new PipeStreamImpersonationWorker(action.Invoke);
        }

        public static Action ToAction(
            this PipeStreamImpersonationWorker @delegate)
        {
            return new Action(@delegate.Invoke);
        }

        public static SerialDataReceivedEventHandler ToSerialDataReceivedEventHandler(
            this Action<object, SerialDataReceivedEventArgs> action)
        {
            return new SerialDataReceivedEventHandler(action.Invoke);
        }

        public static Action<object, SerialDataReceivedEventArgs> ToAction(
            this SerialDataReceivedEventHandler @delegate)
        {
            return new Action<object, SerialDataReceivedEventArgs>(@delegate.Invoke);
        }

        public static SerialErrorReceivedEventHandler ToSerialErrorReceivedEventHandler(
            this Action<object, SerialErrorReceivedEventArgs> action)
        {
            return new SerialErrorReceivedEventHandler(action.Invoke);
        }

        public static Action<object, SerialErrorReceivedEventArgs> ToAction(
            this SerialErrorReceivedEventHandler @delegate)
        {
            return new Action<object, SerialErrorReceivedEventArgs>(@delegate.Invoke);
        }

        public static SerialPinChangedEventHandler ToSerialPinChangedEventHandler(
            this Action<object, SerialPinChangedEventArgs> action)
        {
            return new SerialPinChangedEventHandler(action.Invoke);
        }

        public static Action<object, SerialPinChangedEventArgs> ToAction(
            this SerialPinChangedEventHandler @delegate)
        {
            return new Action<object, SerialPinChangedEventArgs>(@delegate.Invoke);
        }

        public static AuthenticationSchemeSelector ToAuthenticationSchemeSelector(
            this Func<HttpListenerRequest, AuthenticationSchemes> func)
        {
            return new AuthenticationSchemeSelector(func.Invoke);
        }

        public static Func<HttpListenerRequest, AuthenticationSchemes> ToFunc(
            this AuthenticationSchemeSelector @delegate)
        {
            return new Func<HttpListenerRequest, AuthenticationSchemes>(@delegate.Invoke);
        }

        public static BindIPEndPoint ToBindIPEndPoint(
            this Func<ServicePoint, IPEndPoint, int, IPEndPoint> func)
        {
            return func.Invoke;
        }

        public static Func<ServicePoint, IPEndPoint, int, IPEndPoint> ToFunc(
            this BindIPEndPoint @delegate)
        {
            return new Func<ServicePoint, IPEndPoint, int, IPEndPoint>(@delegate.Invoke);
        }

        public static DownloadDataCompletedEventHandler ToDownloadDataCompletedEventHandler(
            this Action<object, DownloadDataCompletedEventArgs> action)
        {
            return new DownloadDataCompletedEventHandler(action.Invoke);
        }

        public static Action<object, DownloadDataCompletedEventArgs> ToAction(
            this DownloadDataCompletedEventHandler @delegate)
        {
            return new Action<object, DownloadDataCompletedEventArgs>(@delegate.Invoke);
        }

        public static DownloadProgressChangedEventHandler ToDownloadProgressChangedEventHandler(
            this Action<object, DownloadProgressChangedEventArgs> action)
        {
            return new DownloadProgressChangedEventHandler(action.Invoke);
        }

        public static Action<object, DownloadProgressChangedEventArgs> ToAction(
            this DownloadProgressChangedEventHandler @delegate)
        {
            return new Action<object, DownloadProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static DownloadStringCompletedEventHandler ToDownloadStringCompletedEventHandler(
            this Action<object, DownloadStringCompletedEventArgs> action)
        {
            return new DownloadStringCompletedEventHandler(action.Invoke);
        }

        public static Action<object, DownloadStringCompletedEventArgs> ToAction(
            this DownloadStringCompletedEventHandler @delegate)
        {
            return new Action<object, DownloadStringCompletedEventArgs>(@delegate.Invoke);
        }

        public static HttpContinueDelegate ToHttpContinueDelegate(
            this Action<int, WebHeaderCollection> action)
        {
            return new HttpContinueDelegate(action.Invoke);
        }

        public static Action<int, WebHeaderCollection> ToAction(
            this HttpContinueDelegate @delegate)
        {
            return new Action<int, WebHeaderCollection>(@delegate.Invoke);
        }

        public static OpenReadCompletedEventHandler ToOpenReadCompletedEventHandler(
            this Action<object, OpenReadCompletedEventArgs> action)
        {
            return new OpenReadCompletedEventHandler(action.Invoke);
        }

        public static Action<object, OpenReadCompletedEventArgs> ToAction(
            this OpenReadCompletedEventHandler @delegate)
        {
            return new Action<object, OpenReadCompletedEventArgs>(@delegate.Invoke);
        }

        public static OpenWriteCompletedEventHandler ToOpenWriteCompletedEventHandler(
            this Action<object, OpenWriteCompletedEventArgs> action)
        {
            return new OpenWriteCompletedEventHandler(action.Invoke);
        }

        public static Action<object, OpenWriteCompletedEventArgs> ToAction(
            this OpenWriteCompletedEventHandler @delegate)
        {
            return new Action<object, OpenWriteCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadDataCompletedEventHandler ToUploadDataCompletedEventHandler(
            this Action<object, UploadDataCompletedEventArgs> action)
        {
            return new UploadDataCompletedEventHandler(action.Invoke);
        }

        public static Action<object, UploadDataCompletedEventArgs> ToAction(
            this UploadDataCompletedEventHandler @delegate)
        {
            return new Action<object, UploadDataCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadFileCompletedEventHandler ToUploadFileCompletedEventHandler(
            this Action<object, UploadFileCompletedEventArgs> action)
        {
            return new UploadFileCompletedEventHandler(action.Invoke);
        }

        public static Action<object, UploadFileCompletedEventArgs> ToAction(
            this UploadFileCompletedEventHandler @delegate)
        {
            return new Action<object, UploadFileCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadProgressChangedEventHandler ToUploadProgressChangedEventHandler(
            this Action<object, UploadProgressChangedEventArgs> action)
        {
            return new UploadProgressChangedEventHandler(action.Invoke);
        }

        public static Action<object, UploadProgressChangedEventArgs> ToAction(
            this UploadProgressChangedEventHandler @delegate)
        {
            return new Action<object, UploadProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static UploadStringCompletedEventHandler ToUploadStringCompletedEventHandler(
            this Action<object, UploadStringCompletedEventArgs> action)
        {
            return new UploadStringCompletedEventHandler(action.Invoke);
        }

        public static Action<object, UploadStringCompletedEventArgs> ToAction(
            this UploadStringCompletedEventHandler @delegate)
        {
            return new Action<object, UploadStringCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadValuesCompletedEventHandler ToUploadValuesCompletedEventHandler(
            this Action<object, UploadValuesCompletedEventArgs> action)
        {
            return new UploadValuesCompletedEventHandler(action.Invoke);
        }

        public static Action<object, UploadValuesCompletedEventArgs> ToAction(
            this UploadValuesCompletedEventHandler @delegate)
        {
            return new Action<object, UploadValuesCompletedEventArgs>(@delegate.Invoke);
        }

        public static SendCompletedEventHandler ToSendCompletedEventHandler(
            this Action<object, AsyncCompletedEventArgs> action)
        {
            return new SendCompletedEventHandler(action.Invoke);
        }

        public static Action<object, AsyncCompletedEventArgs> ToAction(
            this SendCompletedEventHandler @delegate)
        {
            return new Action<object, AsyncCompletedEventArgs>(@delegate.Invoke);
        }

        public static NetworkAddressChangedEventHandler ToNetworkAddressChangedEventHandler(
            this Action<object, EventArgs> action)
        {
            return new NetworkAddressChangedEventHandler(action.Invoke);
        }

        public static Action<object, EventArgs> ToAction(
            this NetworkAddressChangedEventHandler @delegate)
        {
            return new Action<object, EventArgs>(@delegate.Invoke);
        }

        public static NetworkAvailabilityChangedEventHandler ToNetworkAvailabilityChangedEventHandler(
            this Action<object, NetworkAvailabilityEventArgs> action)
        {
            return new NetworkAvailabilityChangedEventHandler(action.Invoke);
        }

        public static Action<object, NetworkAvailabilityEventArgs> ToAction(
            this NetworkAvailabilityChangedEventHandler @delegate)
        {
            return new Action<object, NetworkAvailabilityEventArgs>(@delegate.Invoke);
        }

        public static PingCompletedEventHandler ToPingCompletedEventHandler(
            this Action<object, PingCompletedEventArgs> action)
        {
            return new PingCompletedEventHandler(action.Invoke);
        }

        public static Action<object, PingCompletedEventArgs> ToAction(
            this PingCompletedEventHandler @delegate)
        {
            return new Action<object, PingCompletedEventArgs>(@delegate.Invoke);
        }

        public static LocalCertificateSelectionCallback ToLocalCertificateSelectionCallback(
            this Func<object, string, X509CertificateCollection, X509Certificate, string[], X509Certificate> func)
        {
            return new LocalCertificateSelectionCallback(func.Invoke);
        }

        public static Func<object, string, X509CertificateCollection, X509Certificate, string[], X509Certificate>
            ToFunc(
                this LocalCertificateSelectionCallback @delegate)
        {
            return new Func<object, string, X509CertificateCollection, X509Certificate, string[], X509Certificate>(
                @delegate.Invoke);
        }

        public static RemoteCertificateValidationCallback ToRemoteCertificateValidationCallback(
            this Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> func)
        {
            return new RemoteCertificateValidationCallback(func.Invoke);
        }

        public static Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool> ToFunc(
            this RemoteCertificateValidationCallback @delegate)
        {
            return new Func<object, X509Certificate, X509Chain, SslPolicyErrors, bool>(@delegate.Invoke);
        }

        public static MemberFilter ToMemberFilter(
            this Func<MemberInfo, object, bool> func)
        {
            return new MemberFilter(func.Invoke);
        }

        public static Func<MemberInfo, object, bool> ToFunc(
            this MemberFilter @delegate)
        {
            return new Func<MemberInfo, object, bool>(@delegate.Invoke);
        }

        public static ModuleResolveEventHandler ToModuleResolveEventHandler(
            this Func<object, ResolveEventArgs, Module> func)
        {
            return new ModuleResolveEventHandler(func.Invoke);
        }

        public static Func<object, ResolveEventArgs, Module> ToFunc(
            this ModuleResolveEventHandler @delegate)
        {
            return new Func<object, ResolveEventArgs, Module>(@delegate.Invoke);
        }

        public static TypeFilter ToTypeFilter(
            this Func<Type, object, bool> func)
        {
            return new TypeFilter(func.Invoke);
        }

        public static Func<Type, object, bool> ToFunc(
            this TypeFilter @delegate)
        {
            return new Func<Type, object, bool>(@delegate.Invoke);
        }

#if NetFX
        public static ObjectCreationDelegate ToObjectCreationDelegate(
            this Func<IntPtr, IntPtr> func)
        {
            return new ObjectCreationDelegate(func.Invoke);
        }

        public static Func<IntPtr, IntPtr> ToFunc(
            this ObjectCreationDelegate @delegate)
        {
            return new Func<IntPtr, IntPtr>(@delegate.Invoke);
        }

        public static CrossContextDelegate ToCrossContextDelegate(
            this Action action)
        {
            return new CrossContextDelegate(action.Invoke);
        }

        public static Action ToAction(
            this CrossContextDelegate @delegate)
        {
            return new Action(@delegate.Invoke);
        }

        public static HeaderHandler ToHeaderHandler(
            this Func<Header[], object> func)
        {
            return new HeaderHandler(func.Invoke);
        }

        public static Func<Header[], object> ToFunc(
            this HeaderHandler @delegate)
        {
            return new Func<Header[], object>(@delegate.Invoke);
        }

        public static MessageSurrogateFilter ToMessageSurrogateFilter(
            this Func<string, object, bool> func)
        {
            return new MessageSurrogateFilter(func.Invoke);
        }

        public static Func<string, object, bool> ToFunc(
            this MessageSurrogateFilter @delegate)
        {
            return new Func<string, object, bool>(@delegate.Invoke);
        }
#endif
        public static MatchEvaluator ToMatchEvaluator(
            this Func<Match, string> func)
        {
            return new MatchEvaluator(func.Invoke);
        }

        public static Func<Match, string> ToFunc(
            this MatchEvaluator @delegate)
        {
            return new Func<Match, string>(@delegate.Invoke);
        }

        public static ContextCallback ToContextCallback(
            this Action<object> action)
        {
            return new ContextCallback(action.Invoke);
        }

        public static Action<object> ToAction(
            this ContextCallback @delegate)
        {
            return new Action<object>(@delegate.Invoke);
        }

        public static ParameterizedThreadStart ToParameterizedThreadStart(
            this Action<object> action)
        {
            return new ParameterizedThreadStart(action.Invoke);
        }

        public static Action<object> ToAction(
            this ParameterizedThreadStart @delegate)
        {
            return new Action<object>(@delegate.Invoke);
        }

        public static SendOrPostCallback ToSendOrPostCallback(
            this Action<object> action)
        {
            return new SendOrPostCallback(action.Invoke);
        }

        public static Action<object> ToAction(
            this SendOrPostCallback @delegate)
        {
            return new Action<object>(@delegate.Invoke);
        }

        public static ThreadExceptionEventHandler ToThreadExceptionEventHandler(
            this Action<object, ThreadExceptionEventArgs> action)
        {
            return new ThreadExceptionEventHandler(action.Invoke);
        }

        public static Action<object, ThreadExceptionEventArgs> ToAction(
            this ThreadExceptionEventHandler @delegate)
        {
            return new Action<object, ThreadExceptionEventArgs>(@delegate.Invoke);
        }

        public static ThreadStart ToThreadStart(
            this Action action)
        {
            return new ThreadStart(action.Invoke);
        }

        public static Action ToAction(
            this ThreadStart @delegate)
        {
            return new Action(@delegate.Invoke);
        }

        public static TimerCallback ToTimerCallback(
            this Action<object> action)
        {
            return new TimerCallback(action.Invoke);
        }

        public static Action<object> ToAction(
            this TimerCallback @delegate)
        {
            return new Action<object>(@delegate.Invoke);
        }

        public static WaitCallback ToWaitCallback(
            this Action<object> action)
        {
            return new WaitCallback(action.Invoke);
        }

        public static Action<object> ToAction(
            this WaitCallback @delegate)
        {
            return new Action<object>(@delegate.Invoke);
        }

        public static WaitOrTimerCallback ToWaitOrTimerCallback(
            this Action<object, bool> action)
        {
            return new WaitOrTimerCallback(action.Invoke);
        }

        public static Action<object, bool> ToAction(
            this WaitOrTimerCallback @delegate)
        {
            return new Action<object, bool>(@delegate.Invoke);
        }

        public static ElapsedEventHandler ToElapsedEventHandler(
            this Action<object, ElapsedEventArgs> action)
        {
            return new ElapsedEventHandler(action.Invoke);
        }

        public static Action<object, ElapsedEventArgs> ToAction(
            this ElapsedEventHandler @delegate)
        {
            return new Action<object, ElapsedEventArgs>(@delegate.Invoke);
        }

        public static AssemblyLoadEventHandler ToAssemblyEventHandler(
            this EventHandler<AssemblyLoadEventArgs> @delegate)
        {
            return new AssemblyLoadEventHandler(@delegate.Invoke);
        }

        public static EventHandler<AssemblyLoadEventArgs> ToEventHandler(
            this AssemblyLoadEventHandler @delegate)
        {
            return new EventHandler<AssemblyLoadEventArgs>(@delegate.Invoke);
        }

        public static ConsoleCancelEventHandler ToConsoleCancelEventHandler(
            this EventHandler<ConsoleCancelEventArgs> @delegate)
        {
            return new ConsoleCancelEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ConsoleCancelEventArgs> ToEventHandler(
            this ConsoleCancelEventHandler @delegate)
        {
            return new EventHandler<ConsoleCancelEventArgs>(@delegate.Invoke);
        }

        public static EventHandler ToEventHandler(
            this EventHandler<EventArgs> @delegate)
        {
            return new EventHandler(@delegate.Invoke);
        }

        public static EventHandler<EventArgs> ToEventHandler(
            this EventHandler @delegate)
        {
            return new EventHandler<EventArgs>(@delegate.Invoke);
        }

        public static UnhandledExceptionEventHandler ToUnhandledExceptionEventHandler(
            this EventHandler<UnhandledExceptionEventArgs> @delegate)
        {
            return new UnhandledExceptionEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UnhandledExceptionEventArgs> ToEventHandler(
            this UnhandledExceptionEventHandler @delegate)
        {
            return new EventHandler<UnhandledExceptionEventArgs>(@delegate.Invoke);
        }

        public static NotifyCollectionChangedEventHandler ToNotifyCollectionChangedEventHandler(
            this EventHandler<NotifyCollectionChangedEventArgs> @delegate)
        {
            return new NotifyCollectionChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<NotifyCollectionChangedEventArgs> ToEventHandler(
            this NotifyCollectionChangedEventHandler @delegate)
        {
            return new EventHandler<NotifyCollectionChangedEventArgs>(@delegate.Invoke);
        }

        public static AddingNewEventHandler ToAddingNewEventHandler(
            this EventHandler<AddingNewEventArgs> @delegate)
        {
            return new AddingNewEventHandler(@delegate.Invoke);
        }

        public static EventHandler<AddingNewEventArgs> ToEventHandler(
            this AddingNewEventHandler @delegate)
        {
            return new EventHandler<AddingNewEventArgs>(@delegate.Invoke);
        }

        public static AsyncCompletedEventHandler ToAsyncCompletedEventHandler(
            this EventHandler<AsyncCompletedEventArgs> @delegate)
        {
            return new AsyncCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<AsyncCompletedEventArgs> ToEventHandler(
            this AsyncCompletedEventHandler @delegate)
        {
            return new EventHandler<AsyncCompletedEventArgs>(@delegate.Invoke);
        }

        public static CancelEventHandler ToCancelEventHandler(
            this EventHandler<CancelEventArgs> @delegate)
        {
            return new CancelEventHandler(@delegate.Invoke);
        }

        public static EventHandler<CancelEventArgs> ToEventHandler(
            this CancelEventHandler @delegate)
        {
            return new EventHandler<CancelEventArgs>(@delegate.Invoke);
        }

        public static CollectionChangeEventHandler ToCollectionChangeEventHandler(
            this EventHandler<CollectionChangeEventArgs> @delegate)
        {
            return new CollectionChangeEventHandler(@delegate.Invoke);
        }

        public static EventHandler<CollectionChangeEventArgs> ToEventHandler(
            this CollectionChangeEventHandler @delegate)
        {
            return new EventHandler<CollectionChangeEventArgs>(@delegate.Invoke);
        }

        public static DoWorkEventHandler ToDoWorkEventHandler(
            this EventHandler<DoWorkEventArgs> @delegate)
        {
            return new DoWorkEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DoWorkEventArgs> ToEventHandler(
            this DoWorkEventHandler @delegate)
        {
            return new EventHandler<DoWorkEventArgs>(@delegate.Invoke);
        }

        public static HandledEventHandler ToHandledEventHandler(
            this EventHandler<HandledEventArgs> @delegate)
        {
            return new HandledEventHandler(@delegate.Invoke);
        }

        public static EventHandler<HandledEventArgs> ToEventHandler(
            this HandledEventHandler @delegate)
        {
            return new EventHandler<HandledEventArgs>(@delegate.Invoke);
        }

        public static ListChangedEventHandler ToListChangedEventHandler(
            this EventHandler<ListChangedEventArgs> @delegate)
        {
            return new ListChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ListChangedEventArgs> ToEventHandler(
            this ListChangedEventHandler @delegate)
        {
            return new EventHandler<ListChangedEventArgs>(@delegate.Invoke);
        }

        public static ProgressChangedEventHandler ToProgressChangedEventHandler(
            this EventHandler<ProgressChangedEventArgs> @delegate)
        {
            return new ProgressChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ProgressChangedEventArgs> ToEventHandler(
            this ProgressChangedEventHandler @delegate)
        {
            return new EventHandler<ProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static PropertyChangedEventHandler ToPropertyChangedEventHandler(
            this EventHandler<PropertyChangedEventArgs> @delegate)
        {
            return new PropertyChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<PropertyChangedEventArgs> ToEventHandler(
            this PropertyChangedEventHandler @delegate)
        {
            return new EventHandler<PropertyChangedEventArgs>(@delegate.Invoke);
        }

        public static PropertyChangingEventHandler ToPropertyChangingEventHandler(
            this EventHandler<PropertyChangingEventArgs> @delegate)
        {
            return new PropertyChangingEventHandler(@delegate.Invoke);
        }

        public static EventHandler<PropertyChangingEventArgs> ToEventHandler(
            this PropertyChangingEventHandler @delegate)
        {
            return new EventHandler<PropertyChangingEventArgs>(@delegate.Invoke);
        }

        public static RunWorkerCompletedEventHandler ToRunWorkerCompletedEventHandler(
            this EventHandler<RunWorkerCompletedEventArgs> @delegate)
        {
            return new RunWorkerCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<RunWorkerCompletedEventArgs> ToEventHandler(
            this RunWorkerCompletedEventHandler @delegate)
        {
            return new EventHandler<RunWorkerCompletedEventArgs>(@delegate.Invoke);
        }

        public static ActiveDesignerEventHandler ToActiveDesignerEventHandler(
            this EventHandler<ActiveDesignerEventArgs> @delegate)
        {
            return new ActiveDesignerEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ActiveDesignerEventArgs> ToEventHandler(
            this ActiveDesignerEventHandler @delegate)
        {
            return new EventHandler<ActiveDesignerEventArgs>(@delegate.Invoke);
        }

        public static ComponentChangedEventHandler ToComponentChangedEventHandler(
            this EventHandler<ComponentChangedEventArgs> @delegate)
        {
            return new ComponentChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ComponentChangedEventArgs> ToEventHandler(
            this ComponentChangedEventHandler @delegate)
        {
            return new EventHandler<ComponentChangedEventArgs>(@delegate.Invoke);
        }

        public static ComponentChangingEventHandler ToComponentChangingEventHandler(
            this EventHandler<ComponentChangingEventArgs> @delegate)
        {
            return new ComponentChangingEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ComponentChangingEventArgs> ToEventHandler(
            this ComponentChangingEventHandler @delegate)
        {
            return new EventHandler<ComponentChangingEventArgs>(@delegate.Invoke);
        }

        public static ComponentEventHandler ToComponentEventHandler(
            this EventHandler<ComponentEventArgs> @delegate)
        {
            return new ComponentEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ComponentEventArgs> ToEventHandler(
            this ComponentEventHandler @delegate)
        {
            return new EventHandler<ComponentEventArgs>(@delegate.Invoke);
        }

        public static ComponentRenameEventHandler ToComponentRenameEventHandler(
            this EventHandler<ComponentRenameEventArgs> @delegate)
        {
            return new ComponentRenameEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ComponentRenameEventArgs> ToEventHandler(
            this ComponentRenameEventHandler @delegate)
        {
            return new EventHandler<ComponentRenameEventArgs>(@delegate.Invoke);
        }

        public static DesignerEventHandler ToDesignerEventHandler(
            this EventHandler<DesignerEventArgs> @delegate)
        {
            return new DesignerEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DesignerEventArgs> ToEventHandler(
            this DesignerEventHandler @delegate)
        {
            return new EventHandler<DesignerEventArgs>(@delegate.Invoke);
        }

        public static DesignerTransactionCloseEventHandler ToDesignerTransactionCloseEventHandler(
            this EventHandler<DesignerTransactionCloseEventArgs> @delegate)
        {
            return new DesignerTransactionCloseEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DesignerTransactionCloseEventArgs> ToEventHandler(
            this DesignerTransactionCloseEventHandler @delegate)
        {
            return new EventHandler<DesignerTransactionCloseEventArgs>(@delegate.Invoke);
        }

        public static ResolveNameEventHandler ToResolveNameEventHandler(
            this EventHandler<ResolveNameEventArgs> @delegate)
        {
            return new ResolveNameEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ResolveNameEventArgs> ToEventHandler(
            this ResolveNameEventHandler @delegate)
        {
            return new EventHandler<ResolveNameEventArgs>(@delegate.Invoke);
        }

#if NetFX
        public static SettingChangingEventHandler ToSettingChangingEventHandler(
            this EventHandler<SettingChangingEventArgs> @delegate)
        {
            return new SettingChangingEventHandler(@delegate.Invoke);
        }

        public static EventHandler<SettingChangingEventArgs> ToEventHandler(
            this SettingChangingEventHandler @delegate)
        {
            return new EventHandler<SettingChangingEventArgs>(@delegate.Invoke);
        }

        public static SettingsLoadedEventHandler ToSettingsLoadedEventHandler(
            this EventHandler<SettingsLoadedEventArgs> @delegate)
        {
            return new SettingsLoadedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<SettingsLoadedEventArgs> ToEventHandler(
            this SettingsLoadedEventHandler @delegate)
        {
            return new EventHandler<SettingsLoadedEventArgs>(@delegate.Invoke);
        }

        public static SettingsSavingEventHandler ToSettingsSavingEventHandler(
            this EventHandler<CancelEventArgs> @delegate)
        {
            return new SettingsSavingEventHandler(@delegate.Invoke);
        }

        public static EventHandler<CancelEventArgs> ToEventHandler(
            this SettingsSavingEventHandler @delegate)
        {
            return new EventHandler<CancelEventArgs>(@delegate.Invoke);
        }

        public static SettingsSavingEventHandler ToSettingsSavingEventHandler(
            this CancelEventHandler @delegate)
        {
            return new SettingsSavingEventHandler(@delegate.Invoke);
        }

        public static CancelEventHandler ToCancelEventHandler(
            this SettingsSavingEventHandler @delegate)
        {
            return new CancelEventHandler(@delegate.Invoke);
        }
#endif

        public static DataReceivedEventHandler ToDataReceivedEventHandler(
            this EventHandler<DataReceivedEventArgs> @delegate)
        {
            return new DataReceivedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DataReceivedEventArgs> ToEventHandler(
            this DataReceivedEventHandler @delegate)
        {
            return new EventHandler<DataReceivedEventArgs>(@delegate.Invoke);
        }

#if NetFX
        public static EntryWrittenEventHandler ToEntryWrittenEventHandler(
            this EventHandler<EntryWrittenEventArgs> @delegate)
        {
            return new EntryWrittenEventHandler(@delegate.Invoke);
        }

        public static EventHandler<EntryWrittenEventArgs> ToEventHandler(
            this EntryWrittenEventHandler @delegate)
        {
            return new EventHandler<EntryWrittenEventArgs>(@delegate.Invoke);
        }
#endif

        public static ErrorEventHandler ToErrorEventHandler(
            this EventHandler<ErrorEventArgs> @delegate)
        {
            return new ErrorEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ErrorEventArgs> ToEventHandler(
            this ErrorEventHandler @delegate)
        {
            return new EventHandler<ErrorEventArgs>(@delegate.Invoke);
        }

        public static FileSystemEventHandler ToFileSystemEventHandler(
            this EventHandler<FileSystemEventArgs> @delegate)
        {
            return new FileSystemEventHandler(@delegate.Invoke);
        }

        public static RenamedEventHandler ToRenamedEventHandler(
            this EventHandler<RenamedEventArgs> @delegate)
        {
            return new RenamedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<RenamedEventArgs> ToEventHandler(
            this RenamedEventHandler @delegate)
        {
            return new EventHandler<RenamedEventArgs>(@delegate.Invoke);
        }

        public static SerialDataReceivedEventHandler ToSerialDataReceivedEventHandler(
            this EventHandler<SerialDataReceivedEventArgs> @delegate)
        {
            return new SerialDataReceivedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<SerialDataReceivedEventArgs> ToEventHandler(
            this SerialDataReceivedEventHandler @delegate)
        {
            return new EventHandler<SerialDataReceivedEventArgs>(@delegate.Invoke);
        }

        public static SerialErrorReceivedEventHandler ToSerialErrorReceivedEventHandler(
            this EventHandler<SerialErrorReceivedEventArgs> @delegate)
        {
            return new SerialErrorReceivedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<SerialErrorReceivedEventArgs> ToEventHandler(
            this SerialErrorReceivedEventHandler @delegate)
        {
            return new EventHandler<SerialErrorReceivedEventArgs>(@delegate.Invoke);
        }

        public static SerialPinChangedEventHandler ToSerialPinChangedEventHandler(
            this EventHandler<SerialPinChangedEventArgs> @delegate)
        {
            return new SerialPinChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<SerialPinChangedEventArgs> ToEventHandler(
            this SerialPinChangedEventHandler @delegate)
        {
            return new EventHandler<SerialPinChangedEventArgs>(@delegate.Invoke);
        }

        public static DownloadDataCompletedEventHandler ToDownloadDataCompletedEventHandler(
            this EventHandler<DownloadDataCompletedEventArgs> @delegate)
        {
            return new DownloadDataCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DownloadDataCompletedEventArgs> ToEventHandler(
            this DownloadDataCompletedEventHandler @delegate)
        {
            return new EventHandler<DownloadDataCompletedEventArgs>(@delegate.Invoke);
        }

        public static DownloadProgressChangedEventHandler ToDownloadProgressChangedEventHandler(
            this EventHandler<DownloadProgressChangedEventArgs> @delegate)
        {
            return new DownloadProgressChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DownloadProgressChangedEventArgs> ToEventHandler(
            this DownloadProgressChangedEventHandler @delegate)
        {
            return new EventHandler<DownloadProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static DownloadStringCompletedEventHandler ToDownloadStringCompletedEventHandler(
            this EventHandler<DownloadStringCompletedEventArgs> @delegate)
        {
            return new DownloadStringCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<DownloadStringCompletedEventArgs> ToEventHandler(
            this DownloadStringCompletedEventHandler @delegate)
        {
            return new EventHandler<DownloadStringCompletedEventArgs>(@delegate.Invoke);
        }

        public static OpenReadCompletedEventHandler ToOpenReadCompletedEventHandler(
            this EventHandler<OpenReadCompletedEventArgs> @delegate)
        {
            return new OpenReadCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<OpenReadCompletedEventArgs> ToEventHandler(
            this OpenReadCompletedEventHandler @delegate)
        {
            return new EventHandler<OpenReadCompletedEventArgs>(@delegate.Invoke);
        }

        public static OpenWriteCompletedEventHandler ToOpenWriteCompletedEventHandler(
            this EventHandler<OpenWriteCompletedEventArgs> @delegate)
        {
            return new OpenWriteCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<OpenWriteCompletedEventArgs> ToEventHandler(
            this OpenWriteCompletedEventHandler @delegate)
        {
            return new EventHandler<OpenWriteCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadDataCompletedEventHandler ToUploadDataCompletedEventHandler(
            this EventHandler<UploadDataCompletedEventArgs> @delegate)
        {
            return new UploadDataCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UploadDataCompletedEventArgs> ToEventHandler(
            this UploadDataCompletedEventHandler @delegate)
        {
            return new EventHandler<UploadDataCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadFileCompletedEventHandler ToUploadFileCompletedEventHandler(
            this EventHandler<UploadFileCompletedEventArgs> @delegate)
        {
            return new UploadFileCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UploadFileCompletedEventArgs> ToEventHandler(
            this UploadFileCompletedEventHandler @delegate)
        {
            return new EventHandler<UploadFileCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadProgressChangedEventHandler ToUploadProgressChangedEventHandler(
            this EventHandler<UploadProgressChangedEventArgs> @delegate)
        {
            return new UploadProgressChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UploadProgressChangedEventArgs> ToEventHandler(
            this UploadProgressChangedEventHandler @delegate)
        {
            return new EventHandler<UploadProgressChangedEventArgs>(@delegate.Invoke);
        }

        public static UploadStringCompletedEventHandler ToUploadStringCompletedEventHandler(
            this EventHandler<UploadStringCompletedEventArgs> @delegate)
        {
            return new UploadStringCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UploadStringCompletedEventArgs> ToEventHandler(
            this UploadStringCompletedEventHandler @delegate)
        {
            return new EventHandler<UploadStringCompletedEventArgs>(@delegate.Invoke);
        }

        public static UploadValuesCompletedEventHandler ToUploadValuesCompletedEventHandler(
            this EventHandler<UploadValuesCompletedEventArgs> @delegate)
        {
            return new UploadValuesCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<UploadValuesCompletedEventArgs> ToEventHandler(
            this UploadValuesCompletedEventHandler @delegate)
        {
            return new EventHandler<UploadValuesCompletedEventArgs>(@delegate.Invoke);
        }

        public static SendCompletedEventHandler ToSendCompletedEventHandler(
            this EventHandler<AsyncCompletedEventArgs> @delegate)
        {
            return new SendCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<AsyncCompletedEventArgs> ToEventHandler(
            this SendCompletedEventHandler @delegate)
        {
            return new EventHandler<AsyncCompletedEventArgs>(@delegate.Invoke);
        }

        public static SendCompletedEventHandler ToSendCompletedEventHandler(
            this AsyncCompletedEventHandler @delegate)
        {
            return new SendCompletedEventHandler(@delegate.Invoke);
        }

        public static AsyncCompletedEventHandler ToAsyncCompletedEventHandler(
            this SendCompletedEventHandler @delegate)
        {
            return new AsyncCompletedEventHandler(@delegate.Invoke);
        }

        public static NetworkAddressChangedEventHandler ToNetworkAddressChangedEventHandler(
            this EventHandler<EventArgs> @delegate)
        {
            return new NetworkAddressChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<EventArgs> ToEventHandler(
            this NetworkAddressChangedEventHandler @delegate)
        {
            return new EventHandler<EventArgs>(@delegate.Invoke);
        }

        public static NetworkAddressChangedEventHandler ToNetworkAddressChangedEventHandler(
            this EventHandler @delegate)
        {
            return new NetworkAddressChangedEventHandler(@delegate.Invoke);
        }

        public static NetworkAvailabilityChangedEventHandler ToNetworkAvailabilityChangedEventHandler(
            this EventHandler<NetworkAvailabilityEventArgs> @delegate)
        {
            return new NetworkAvailabilityChangedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<NetworkAvailabilityEventArgs> ToEventHandler(
            this NetworkAvailabilityChangedEventHandler @delegate)
        {
            return new EventHandler<NetworkAvailabilityEventArgs>(@delegate.Invoke);
        }

        public static PingCompletedEventHandler ToPingCompletedEventHandler(
            this EventHandler<PingCompletedEventArgs> @delegate)
        {
            return new PingCompletedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<PingCompletedEventArgs> ToEventHandler(
            this PingCompletedEventHandler @delegate)
        {
            return new EventHandler<PingCompletedEventArgs>(@delegate.Invoke);
        }

        public static ThreadExceptionEventHandler ToThreadExceptionEventHandler(
            this EventHandler<ThreadExceptionEventArgs> @delegate)
        {
            return new ThreadExceptionEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ThreadExceptionEventArgs> ToEventHandler(
            this ThreadExceptionEventHandler @delegate)
        {
            return new EventHandler<ThreadExceptionEventArgs>(@delegate.Invoke);
        }

        public static ElapsedEventHandler ToElapsedEventHandler(
            this EventHandler<ElapsedEventArgs> @delegate)
        {
            return new ElapsedEventHandler(@delegate.Invoke);
        }

        public static EventHandler<ElapsedEventArgs> ToEventHandler(
            this ElapsedEventHandler @delegate)
        {
            return new EventHandler<ElapsedEventArgs>(@delegate.Invoke);
        }


/*
 * Shorthand Task.Factory.FromAsync (for .NET 4.5)
 * This extension method series represent shorthand version of Task.Factory.FromAsync (for .NET 4.5)
 * 
 * Author: rkttu
 * Submitted on: 2/4/2016 9:32:21 AM
 * 
 * Example: 
 * ((Func<double, double, double>)Math.Pow)
    .ToTask(2d, 2d)
    .ContinueWith(x => ((Action<string, object[]>)Console.WriteLine).ToTask("Power value: {0}", new object[] { x.Result }))
    .Wait();
Console.WriteLine("Program completed.");
Console.ReadLine();
 */

        public static Task<TResult> ToTask<TResult>(
            this Func<TResult> function,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T, TResult>(
            this Func<T, TResult> function,
            T arg,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, TResult>(
            this Func<T1, T2, TResult> function,
            T1 arg1, T2 arg2,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, TResult>(
            this Func<T1, T2, T3, TResult> function,
            T1 arg1, T2 arg2, T3 arg3,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, TResult>(
            this Func<T1, T2, T3, T4, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, TResult>(
            this Func<T1, T2, T3, T4, T5, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, callback,
                    @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12,
                    callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14, T15 arg15,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, arg15, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task<TResult> ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16,
            TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> function,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task<TResult>.Factory.FromAsync(
                function.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, arg15, arg16, callback, @object),
                function.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask(
            this Action action,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T>(
            this Action<T> action,
            T obj,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(obj, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2>(
            this Action<T1, T2> action,
            T1 arg1, T2 arg2,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 arg1, T2 arg2, T3 arg3,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5>(
            this Action<T1, T2, T3, T4, T5> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6>(
            this Action<T1, T2, T3, T4, T5, T6> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7>(
            this Action<T1, T2, T3, T4, T5, T6, T7> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, callback,
                    @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, callback,
                    @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14, T15 arg15,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, arg15, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }

        public static Task ToTask<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11,
            T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16,
            AsyncCallback callback = default(AsyncCallback),
            object @object = default(object),
            TaskCreationOptions creationOptions = default(TaskCreationOptions),
            TaskScheduler scheduler = default(TaskScheduler))
        {
            return Task.Factory.FromAsync(
                action.BeginInvoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13,
                    arg14, arg15, arg16, callback, @object),
                action.EndInvoke, creationOptions,
                (scheduler ?? TaskScheduler.Current) ?? TaskScheduler.Default);
        }



/*
 * Merge
 * Merges two dictionaries
 * 
 * Author: mInternauta
 * Submitted on: 5/1/2016 12:26:51 AM
 * 
 * Example: 
 * public void Test_MergeDictionary()
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

            dic1.Merge(dic2);
        }
 */

        /// <summary>
        /// Merges the current data to a new data
        /// </summary>
        /// <param name="data"></param>
        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> to, IDictionary<TKey, TValue> data)
        {
            foreach (var item in data)
            {
                if (to.ContainsKey(item.Key) == false)
                {
                    to.Add(item.Key, item.Value);
                }
            }
        }


/*
 * IsIn
 * Determines if an instance is contained in a sequence. Is the equivalent of Contains, but allows a more fluent reading "if item is in list", specially useful in LINQ extension methods like Where
 * 
 * Author: Joan Comas
 * Submitted on: 4/20/2016 5:05:51 PM
 * 
 * Example: 
 * var inclusionList = new List<string> { "inclusion1", "inclusion2" };
var query = myEntities.MyEntity
                     .Select(e => e.Name)
                     .Where(e => e.IsIn(inclusionList));
 */

        public static bool IsIn<T>(this T keyObject, IEnumerable<T> collection)
        {
            return collection.Contains(keyObject);
        }


/*
 * Ori Samara
 * Extracts the underylying SQL query from an IQueryable datatype
 * 
 * Author: Ori Samara
 * Submitted on: 2/10/2016 3:32:45 PM
 * 
 * Example: 
 * var res = new List<EntityName>();
            string query = "";
            using (Entities sp = new Entities())
            {
                var q = sp.EntityName;
                query = q.ToSQLQuery();
                res = q.ToList();

            }

            return res;
 */

        /// <summary>
        /// Extracts the underylying SQL query from an IQueryable datatype
        /// </summary>
        /// <param name="source">https://visualstudiomagazine.com/blogs/tool-tracker/2011/11/seeing-the-sql.aspx</param>
        /// <returns>Returns a SQL Query in a string datatype</returns>
        public static string ToSQLQuery<T>(this IQueryable<T> source)
        {
            var x = IsNullOrEmpty(System.Convert.ToString(source)) ? "" : source.ToString().Replace("[Extent", "[D");
            return x;
        }


/*
 * NextDayOfWeek
 * Will return the next occurring day of week
 * 
 * Author: Tony Musico
 * Submitted on: 3/22/2016 8:44:56 PM
 * 
 * Example: 
 * DateTime.Now.NextDayOfWeek(DayOfWeek.Tuesday)
 */

        public static DateTime NextDayOfWeek(this DateTime dt, DayOfWeek day)
        {
            var d = new GregorianCalendar().AddDays(dt, -((int) dt.DayOfWeek) + (int) day);
            return (d.Day < dt.Day) ? d.AddDays(7) : d;
        }


/*
 * Perason Correlation Coefficient for a datatable
 * An extension method that add the possibility calculate the Pearson correlation coefficient using the names of two columns of the data table in question.
 * 
 * Author: Mahmood Salamah
 * Submitted on: 8/26/2016 12:50:36 PM
 * 
 * Example: 
 * DataTable tblWeather = GetWeatherInformation();

double dblCorrelation = tblWeather.PearsonCorrelation("WindSpeed" , "Temperature");
 */

        public static double PearsonCorrelate(this DataTable aSourceTable, string aColumn1, string aColumn2)
        {
            double dblResult = 0;

            // Buffer
            double tmpX = 0;
            double tmpY = 0;

            // These will be used in the Pearson correalation formula
            double X = 0; // Sum of X
            double Y = 0; // Sum of Y
            double XX = 0; // Sum of X * X
            double YY = 0; // Sum of Y * Y
            double XY = 0; // Sum of X * Y

            var iRowCount = aSourceTable.Rows.Count;

            foreach (DataRow row in aSourceTable.Rows)
            {
                tmpX = System.Convert.ToDouble(row[aColumn1]);
                tmpY = System.Convert.ToDouble(row[aColumn2]);

                X += tmpX;
                Y += tmpY;

                XX += tmpX * tmpX;
                YY += tmpY * tmpY;
                XY += tmpX * tmpY;
            }

            dblResult = (iRowCount * XY - X * Y) / Math.Sqrt((iRowCount * XX - X * X) * (iRowCount * YY - Y * Y));

            return dblResult;
        }


/*
 * IEnumerable.Chunk
 * Splits an enumerable into chunks of a specified size.
 * 
 * Author: Entroper
 * Submitted on: 8/2/2016 12:43:23 PM
 * 
 * Example: 
 * var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

var chunks = list.Chunk(3);
// returns { { 1, 2, 3 }, { 4, 5, 6 }, { 7 } }
 */

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("chunkSize must be greater than 0.");
            }

            while (list.Any())
            {
                yield return list.Take(chunkSize);
                list = list.Skip(chunkSize);
            }
        }


/*
 * SecondsToString
 * Converts the number of seconds to a string displaying hours and minutes
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 8:31:34 PM
 * 
 * Example: 
 * int seconds = 7863;

string display = seconds.SecondsToString(); // 2 hours 11 mins
 */

        /// <summary>
        /// Converts the seconds to an hour \ min display string.
        /// </summary>
        /// <param name="totalSeconds">The \total seconds.</param>
        /// <returns>A string in the format x hours y mins.</returns>
        public static string SecondsToString(this int totalSeconds)
        {
            var s = TimeSpan.FromSeconds(totalSeconds);

            return $"{(int) s.TotalHours} hours {s.Minutes} mins";
        }


/*
 * ReaderWriterLockSlim
 * Simplified and elegant usage of ReaderWriterLockSlim that
 * 
 * Author: Sharp IT, Maciej Rychter
 * Submitted on: 1/1/2016 1:57:33 PM
 * 
 * Example: 
 * [Test]
        public void ThreadExtensions()
        {
            var cacheLock = new ReaderWriterLockSlim();
            var innerCache = new Dictionary<int, string>();

            var value = "value1";
            var readvalue = string.Empty;

            cacheLock.Write(() => innerCache[1] = value);
            cacheLock.Read(() => readvalue = innerCache[1]);

            Assert.AreEqual(value, readvalue);

        }
 */

        public static void Write(this ReaderWriterLockSlim rwlock, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            rwlock.EnterWriteLock();

            try
            {
                action();
            }
            finally
            {
                rwlock.ExitWriteLock();
            }
        }

        public static void Read(this ReaderWriterLockSlim rwlock, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            rwlock.EnterReadLock();

            try
            {
                action();
            }
            finally
            {
                rwlock.ExitReadLock();
            }
        }

        public static void ReadUpgradable(this ReaderWriterLockSlim rwlock, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            rwlock.EnterUpgradeableReadLock();

            try
            {
                action();
            }
            finally
            {
                rwlock.ExitUpgradeableReadLock();
            }
        }

/*
 * Cache()
 * Caches the results of an IEnumerable.
 * 
 * Author: YellPika
 * Submitted on: 10/16/2012 5:52:19 AM
 * 
 * Example: 
 * var cached = mySequence.Cache();
 */

        public static IEnumerable<T> Cache<T>(this IEnumerable<T> source)
        {
            return CacheHelper(source.GetEnumerator());
        }

        private static IEnumerable<T> CacheHelper<T>(IEnumerator<T> source)
        {
            var isEmpty = new Lazy<bool>(() => !source.MoveNext());
            var head = new Lazy<T>(() => source.Current);
            var tail = new Lazy<IEnumerable<T>>(() => CacheHelper(source));

            return CacheHelper(isEmpty, head, tail);
        }

        private static IEnumerable<T> CacheHelper<T>(
            Lazy<bool> isEmpty,
            Lazy<T> head,
            Lazy<IEnumerable<T>> tail)
        {
            if (isEmpty.Value)
                yield break;

            yield return head.Value;
            foreach (var value in tail.Value)
                yield return value;
        }


/*
 * Extract
 * Extract a string from an other string between 2 char
 * 
 * Author: tmcuh1982
 * Submitted on: 7/12/2016 3:46:22 PM
 * 
 * Example: 
 * "my message {yes} {no}".Extract("{", "}");  // return yes
"my message {yes} {no}".Extract("{", "}",2);  // return no
"my message {yes} {no}".Extract("", "{");     // return my message
 */

        public static string Extract(this string value, string begin_text, string end_text, int occur = 1)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                // Search Begin
                var start = -1;
                // search with number of occurs
                for (var i = 1; i <= occur; i++)
                    start = value.IndexOf(begin_text, start + 1);

                if (start < 0)
                    return value;
                start += begin_text.Length;


                // Search End
                if (string.IsNullOrEmpty(end_text))
                    return value.Substring(start);
                var end = value.IndexOf(end_text, start);
                if (end < 0)
                    return value.Substring(start);

                end -= start;

                // End Final
                return value.Substring(start, end);
            }
            else
            {
                return value;
            }
        }


/*
 * ExpandoObject Print
 * Dynamic Print method for ExpandoObject
 * 
 * Author: Radu Matei
 * Submitted on: 8/4/2016 2:05:13 PM
 * 
 * Example: 
 * var dynamicObject = new ExpandoObject();

dynamicObject["Property"] = "Value";
dynamicObject.OtherProperty = 46;

dynamicObject.Print();
 */

        public static void Print(this ExpandoObject dynamicObject)
        {
            var dynamicDictionary = dynamicObject as IDictionary<string, object>;

            foreach (var property in dynamicDictionary)
            {
                Console.WriteLine("{0}: {1}", property.Key, property.Value.ToString());
            }
            Console.WriteLine();
        }


/*
 * Replace
 * Use this extention method with a lambda expression to replace the first item that satisfies the condition
 * 
 * Author: Brecht Bocket
 * Submitted on: 6/24/2016 9:01:30 AM
 * 
 * Example: 
 * public static void BookReplace()
        {
            var books = new List<Book>
            {
                new Book { Author = "Robert Martin", Title = "Clean Code", Pages = 464 },
                new Book { Author = "Oliver Sturm", Title = "Functional Programming in C#", Pages = 270 },
                new Book { Author = "Martin Fowler", Title = "Patterns of Enterprise Application Architecture", Pages = 533 },
                new Book { Author = "Bill Wager", Title = "Effective C#", Pages = 328 },
            };

            Book replacementBook = new Book
            {
                Author = "Test",
                Pages = 152,
                Title = "Once upon a test"
            };

            books.Replace(replacementBook, item => item.Author == "Bill Wager");
        }
 */

        public static IEnumerable<TSource> Replace<TSource, Tkey>(this IList<TSource> source, TSource replacement,
            Func<TSource, Tkey> selector)
        {
            foreach (var item in source)
            {
                var key = selector(item);

                if (key.Equals(true))
                {
                    var index = source.IndexOf(item);
                    source.Remove(item);
                    source.Insert(index, replacement);
                    break;
                }
            }
            return source;
        }


/*
 * isRandomSecure
 * blowdart random test
 * 
 * Author: Ben Adams
 * Submitted on: 8/4/2016 5:55:30 AM
 * 
 * Example: 
 * var num = 10;
if( num.isRandomSecure() ) {
  Console.WriteLine("num is random");
}
else {
  Console.WriteLine("num is not random");
}
 */

        private static RandomNumberGenerator rand = System.Security.Cryptography.RandomNumberGenerator.Create();

        public static bool isRandomSecure(this int source)
        {
            var data = new byte[4];
            rand.GetBytes(data);

            return source == ((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | data[3]);
        }


/*
 * ReplaceIgnoreCase
 * ReplaceIgnoreCase
 * 
 * Author: Unknown
 * Submitted on: 7/25/2016 11:07:08 PM
 * 
 * Example: 
 * var @string = ReplaceIgnoreCase("Test replace, test replace.", "Test", "tested");
 */

        /// <summary>
        /// Extension method to do case-insensitive string replace
        /// </summary>
        /// <param name="str">        </param>
        /// <param name="pattern">    </param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceIgnoreCase(this string str, string pattern, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            var upperString = str.ToUpper();
            var upperPattern = pattern.ToUpper();
            var inc = (str.Length / pattern.Length) *
                      (replacement.Length - pattern.Length);
            var chars = new char[str.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                       position0)) != -1)
            {
                for (var i = position0; i < position1; ++i)
                    chars[count++] = str[i];
                for (var i = 0; i < replacement.Length; ++i)
                    chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return str;
            for (var i = position0; i < str.Length; ++i)
                chars[count++] = str[i];
            return new string(chars, 0, count);
        }


/*
 * FindParent(string parentName) - For XElement
 * Find parent XElement from a provided name. Returns null if no match
 * 
 * Author: Andi Haviari
 * Submitted on: 5/31/2016 5:25:16 PM
 * 
 * Example: 
 * XElement parentXElement = childXElement.FindParent("nodename");
 */

        public static XElement FindParent(this XElement e, string Name)
        {
            XElement r = null;

            if (e == null)
                return r;

            if (e.Parent != null && e.Parent.Name == Name)
            {
                r = e.Parent;
            }
            else
            {
                r = e.Parent.FindParent(Name);
            }

            return r;
        }


/*
 * AddOrdinal
 * Add an ordinal to a number,
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 7:44:25 PM
 * 
 * Example: 
 * int number = 1;

var ordinal = number.AddOrdinal(); // 1st
 */

        /// <summary>
        /// Adds an ordinal to a number
        /// </summary>
        /// <param name="number">The number to add the ordinal too.</param>
        /// <returns>A string with an number and ordinal</returns>
        public static string AddOrdinal(this int number)
        {
            if (number <= 0)
            {
                return number.ToString();
            }

            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + "th";
            }

            switch (number % 10)
            {
                case 1:
                    return number + "st";

                case 2:
                    return number + "nd";

                case 3:
                    return number + "rd";

                default:
                    return number + "th";
            }
        }


/*
 * BooleanExt
 * Extension Method to Execute Delegate Based on Boolean Value
 * 
 * Author: Brian Pruitt
 * Submitted on: 1/16/2017 3:07:56 PM
 * 
 * Example: 
 * BooleanExt.delExecuteMethod d = delegate () { Console.WriteLine("TaDa"); };

            bool state = true;
            // Should execute delegate
            state.When(true, d);
            // Should not execute delegate
            state.When(false, d);

            state = false;
            // Should execute delegate
            state.When(false, d);
            // Should not execute delegate
            state.When(true, d);

            state = true;
            // Should execute delegate
            state.WhenTrue(d);
            // Should not execute delegate
            state.WhenFalse(d);

            state = false;
            // Should not execute delegate
            state.WhenTrue(d);
            // Should execute delegate
            state.WhenFalse(d);
 */

        public delegate void delExecuteMethod();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Current value of Boolean</param>
        /// <param name="executeWhen">Value of Boolean when to execute delegate.</param>
        /// <param name="executeMethod">Delegate method to execute.</param>
        public static void When(this bool value, bool executeWhen, delExecuteMethod executeMethod)
        {
            if (value == executeWhen)
                executeMethod.Invoke();
        }

        /// <summary>
        /// Executes delegate when the boolean value is true.
        /// </summary>
        /// <param name="value">Current value of the boolean.</param>
        /// <param name="executeMothod">Delegate method to execute.</param>
        public static void WhenTrue(this bool value, delExecuteMethod executeMothod)
        {
            if (value)
                executeMothod.Invoke();
        }

        /// <summary>
        /// Executes the delegate when the boolean value is false.
        /// </summary>
        /// <param name="value">Current value of the boolean.</param>
        /// <param name="executeMothod">Delegate method to execute.</param>
        public static void WhenFalse(this bool value, delExecuteMethod executeMothod)
        {
            if (!value)
                executeMothod.Invoke();
        }
    }
}