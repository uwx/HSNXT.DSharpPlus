using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
#if NetFX
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * ToString
 * returns a formatted string on a nullable date
 * 
 * Author: C.F.Meijers
 * Submitted on: 12/11/2007 3:27:18 PM
 * 
 * Example: 
 * DateTime? dt = null;

string s = dt.ToString("yyyy-MM-dd");  //s = ""

dt =  DateTime.Now;

s = dt.ToString("yyy-MM-dd"); //s = "2007-31-12"
 */

        /// <summary>
        /// Returns a formatted date or emtpy string
        /// </summary>
        /// <param name="t">DateTime instance or null</param>
        /// <param name="format">datetime formatstring </param>
        /// <returns></returns>
        public static string ToString(this DateTime? t, string format)
        {
            if (t != null)
            {
                return t.Value.ToString(format);
            }

            return "";
        }


/*
 * Transpose
 * transposes the rows and columns of its argument
 * 
 * Author: Edmondo Pentangelo
 * Submitted on: 2/5/2009 2:00:32 AM
 * 
 * Example: 
 * //Input: transpose [[1,2,3],[4,5,6],[7,8,9]]
//Output: [[1,4,7],[2,5,8],[3,6,9]]
var result = new[] {new[] {1, 2, 3}, new[] {4, 5, 6}, new[] {7, 8, 9}}.Transpose();
 */

        public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> values)
        {
            if (values.Count() == 0)
                return values;
            if (values.First().Count() == 0)
                return Transpose(values.Skip(1));

            var x = values.First().First();
            var xs = values.First().Skip(1);
            var xss = values.Skip(1);
            return
                new[]
                    {
                        new[] {x}
                            .Concat(xss.Select(ht => ht.First()))
                    }
                    .Concat(new[] {xs}
                        .Concat(xss.Select(ht => ht.Skip(1)))
                        .Transpose());
        }


/*
 * GetValue
 * Get column value bu name from IDataReader.
 * 
 * Author: Dumitru Condrea
 * Submitted on: 12/10/2010 2:30:47 PM
 * 
 * Example: 
 * var value = rdr.GetValue<string>(columnName);
var value = rdr.GetValue(columnName);
var value = rdr.GetValue<int>(columnName);
var value = rdr.GetValue<int?>(columnName);
 */

        public static string GetValue(this IDataReader rdr, string columnName)
        {
            return rdr.GetValue<string>(columnName);
        }

        public static T GetValue<T>(this IDataReader rdr, string columnName)
        {
            var data = rdr.GetValue(rdr.GetOrdinal(columnName));
            if (!string.IsNullOrEmpty(data.ToString()))
                return (T) data;

            return default(T);
        }



/*
 * Enum.ParseWithDefault
 * .NET 4.5 version of Enum.ParseUnstrict
 * 
 * Author: JohnnyOddSmile
 * Submitted on: 4/28/2014 1:20:20 PM
 * 
 * Example: 
 * public enum Example{
   A = 0, // default Value
   B = 1,
   C = 2
}

var c = Enum.ParseWithDefault<Example>("D"); (c = Example.A)
var c = Enum.ParseWithDefault<Example>("D", Example.C); (c = Example.C)
 */

        public static T ParseWithDefault<T>(this Enum self, string value, T defaultValue = default(T)) where T : struct
        {
            T res;
            var done = Enum.TryParse(value, true, out res);

            return done ? res : defaultValue;
        }


/*
 * ItemArrayString
 * combin datarow's field value to string
 * 
 * Author: RainmakerHo
 * Submitted on: 4/26/2013 4:15:55 AM
 * 
 * Example: 
 * DataTable dt = new DataTable("test");
dt.Columns.Add("ID", typeof(int));
dt.Columns.Add("NAME", typeof(string));
dt.Columns.Add("SURNAME", typeof(string));
dt.Rows.Add(1, "Rainmaker", "MIKE");
DataRow dr = dt.Rows[0];
string drValues = drValues = dr.ItemArrayString(); // 1,Rainmaker,MIKE
drValues = dr.ItemArrayString(","); // 1,Rainmaker,MIKE
 */

        public static string ItemArrayString(this DataRow dr, string separator)
        {
            return string.Join(separator, dr.ItemArray);
        }

        public static string ItemArrayString(this DataRow dr)
        {
            return string.Join(",", dr.ItemArray);
        }


/*
 * HandleOnce
 * Creates a wrapper for the given event handler which unsubscribes from the event source immediately prior to calling the given event handler.
 * 
 * Author: Wayne Bloss
 * Submitted on: 3/26/2011 10:02:08 PM
 * 
 * Example: 
 * source.TestEvent += new EventHandler(source_TestEvent)
   .HandleOnce(wrapper => source.TestEvent -= wrapper);
 */

        /// <summary>
        /// Creates a wrapper for the given event handler which unsubscribes from the event source immediately prior to calling the given event handler.
        /// </summary>
        /// <param name="handler">Handler that will be wrapped.</param>
        /// <param name="remove">Action to remove the wrapped handler. (wrapper =&gt; source.MyEvent -= wrapper);</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// source.TestEvent += new EventHandler(source_TestEvent)
        ///		.HandleOnce(wrapper => source.TestEvent -= wrapper);
        ///	</code>
        /// </example>
        public static EventHandler HandleOnce(this EventHandler handler, Action<EventHandler> remove)
        {
            EventHandler wrapper = null;
            wrapper = delegate(object sender, EventArgs e)
            {
                remove(wrapper);
                handler(sender, e);
            };
            return wrapper;
        }


/*
 * EndOfTheDay
 * Returns datetime corresponding to day end
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:50:07 PM
 * 
 * Example: 
 * var endOfTheDay = DateTime.Now.EndOfTheDay();
 */

        public static DateTime EndOfTheDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }


/*
 * IndexOfOccurence
 * Finds the index of the nth occurrence of a string in a string
 * 
 * Author: Emad Alashi
 * Submitted on: 4/27/2011 4:30:22 PM
 * 
 * Example: 
 * Console.WriteLine(IndexOfOccurence("Emad Alashi found ash on his desk, he went mad, very mad", "mad", 2));
 */

        public static int IndexOfOccurence(this string str, string stringToBeFound, int occurrence)
        {
            var occurrenceCounter = 0;
            var indexOfPassedString = 0 - stringToBeFound.Length;

            do
            {
                indexOfPassedString = str.IndexOf(stringToBeFound, indexOfPassedString + stringToBeFound.Length);
                if (indexOfPassedString == -1)
                    break;
                occurrenceCounter++;
            } while (occurrenceCounter != occurrence);

            return indexOfPassedString;
        }


/*
 * ToNameValueCollection
 * Splits a string into a NameValueCollection, where each "namevalue" is separated by the "OuterSeparator". The parameter "NameValueSeparator" sets the split between Name and Value.
 * 
 * Author: Jonnidip
 * Submitted on: 5/28/2009 12:51:04 PM
 * 
 * Example: 
 * String a = "param1=value1;param2=value2";
NameValueCollection nv = a.ToNameValueCollection(';', '=');
 */

        /// <summary>
        /// Splits a string into a NameValueCollection, where each "namevalue" is separated by
        /// the "OuterSeparator". The parameter "NameValueSeparator" sets the split between Name and Value.
        /// Example: 
        ///             String str = "param1=value1;param2=value2";
        ///             NameValueCollection nvOut = str.ToNameValueCollection(';', '=');
        ///             
        /// The result is a NameValueCollection where:
        ///             key[0] is "param1" and value[0] is "value1"
        ///             key[1] is "param2" and value[1] is "value2"
        /// </summary>
        /// <param name="str">String to process</param>
        /// <param name="OuterSeparator">Separator for each "NameValue"</param>
        /// <param name="NameValueSeparator">Separator for Name/Value splitting</param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection(this string str, char OuterSeparator,
            char NameValueSeparator)
        {
            NameValueCollection nvText = null;
            str = str.TrimEnd(OuterSeparator);
            if (!string.IsNullOrEmpty(str))
            {
                var arrStrings = str.TrimEnd(OuterSeparator).Split(OuterSeparator);

                foreach (var s in arrStrings)
                {
                    var posSep = s.IndexOf(NameValueSeparator);
                    var name = s.Substring(0, posSep);
                    var value = s.Substring(posSep + 1);
                    if (nvText == null)
                        nvText = new NameValueCollection();
                    nvText.Add(name, value);
                }
            }
            return nvText;
        }


/*
 * IsInRange
 * Finds if the int is the specified range
 * 
 * Author: Marwan Aouida
 * Submitted on: 10/15/2010 6:34:17 PM
 * 
 * Example: 
 * int foo = 4;
Console.WriteLine(foo.IsInRange(2,6)); // output 'True'
 */

        public static bool IsInRange(this int target, int start, int end)
        {
            return target >= start && target <= end;
        }

#if NetFX
/*
 * SafeInvoke
 * Properly invokes an action if it is required. Best way to handle events and threaded operations on a form.
 * 
 * Author: David Harris
 * Submitted on: 8/21/2010 12:53:12 AM
 * 
 * Example: 
 * //(from an event handler or public method on a form)

this.SafeInvoke(() =>
{
	textBox1.Text = "Hello, World!";
});
 */

        public delegate void InvokeHandler();

        public static void SafeInvoke(this System.Windows.Forms.Control control, InvokeHandler handler)
        {
            if (control.InvokeRequired) control.Invoke(handler);
            else handler();
        }
#endif

/*
 * ThisWeekMonday
 * Returns a DateTime representing the Monday of the current week. Depends on System.Globalization
 * 
 * Author: Charles Cherry
 * Submitted on: 8/31/2010 10:19:23 PM
 * 
 * Example: 
 * var monday = DateTime.Now.ThisWeekMonday();
 */

        public static DateTime ThisWeekMonday(this DateTime dt)
        {
            var today = DateTime.Now;
            return new GregorianCalendar().AddDays(today, -((int) today.DayOfWeek) + 1);
        }


/*
 * CloneExplicit<T>
 * Creates an explicit copy of the given enumerable where the only values copied are the ones you designate.
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/7/2010 5:41:39 PM
 * 
 * Example: 
 * IEnumerable<SelectListItem> YesNo = new Collection<SelectListItem>
{
	new SelectListItem { Text = "Yes", Value = bool.TrueString, Selected = true },
	new SelectListItem { Text = "No", Value = bool.FalseString },
};

var v = YesNo.CloneExplicit((t, s) => { t.Text = s.Text; t.Value = s.Value; });
 */

        /// <summary>
        /// Creates an explicit clone assignment of the enumerable list
        /// </summary>
        /// <param name="obj">source enumeration</param>
        /// <param name="attributeAssignment">Action that does the assignment
        /// <para>First parameter is the new item</para>
        /// <para>Second parameter is the source item</para></param>
        /// <returns></returns>
        public static IEnumerable<T> CloneExplicit<T>(this IEnumerable<T> obj, Action<T, T> assign) where T : new()
        {
            Func<T, Action<T>, T> chain = (x, y) =>
            {
                y(x);
                return x;
            };

            return (from s in obj
                select chain(new T(), t => assign(t, s))).ToList();
        }

#if NetFX
/*
 * ClearControls
 * clean the controls on a form. Please send suggestions.
 * 
 * Author: Wallison R Santos
 * Submitted on: 6/22/2010 8:09:57 PM
 * 
 * Example: 
 * //In a button 'Reset' click event in a WinForm

this.ClearControls();
 */

        public static void ClearControls(this System.Windows.Forms.Form form)
        {
            foreach (var item in form.Controls)
            {
                if (item is System.Windows.Forms.TextBox)
                    (item as System.Windows.Forms.TextBox).Text = "";

                if (item is ComboBox)
                    (item as ComboBox).SelectedIndex = -1;

                //if (item is ...)
            }
        }
#endif

/*
 * کد کردن و دی کد کردن رشته در C#
 * mrchsoft.com
 * 
 * Author: Reza Chavoshi
 * Submitted on: 11/30/2014 7:21:04 AM
 * 
 * Example: 
 * Encrypt:uN3ZxZKd2a4=
Decryp2:123
 */

        /// <summary>
        /// کد کردن رشته ها
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(this string str)
        {
            var strEncrKey = "?pws#m";
            byte[] byKey;
            byte[] IV = {18, 52, 86, 120, 144, 171, 205, 239};

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.UTF8.GetBytes(str);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return System.Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// دی کد کردن رشته ها
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt(this string str)
        {
            var sDecrKey = "?pws#m";
            byte[] byKey;
            byte[] IV = {18, 52, 86, 120, 144, 171, 205, 239};

            byte[] inputByteArray;
            // inputByteArray.Length = strText.Length;

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = System.Convert.FromBase64String(str);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


/*
 * GetMostInner
 * Gets the most inner (deepest) exception of a given Exception object
 * 
 * Author: Jonnidip
 * Submitted on: 5/28/2009 3:32:41 PM
 * 
 * Example: 
 * Exception ex = new Exception("Text1", new Exception("Text2", new Exception("Text3", new Exception("Text4"))));
Exception ex2 = ex.GetMostInner();
 */

        /// <summary>
        /// Gets the most inner (deepest) exception of a given Exception object
        /// </summary>
        /// <param name="ex">Source Exception</param>
        /// <returns></returns>
        public static Exception GetMostInner(this Exception ex)
        {
            var ActualInnerEx = ex;

            while (ActualInnerEx != null)
            {
                ActualInnerEx = ActualInnerEx.InnerException;
                if (ActualInnerEx != null)
                    ex = ActualInnerEx;
            }
            return ex;
        }


/*
 * TimeElapsed
 * Inspiration for this extension method was another DateTime extension that determines difference in current time and a DateTime object. That one returned a string and it is more useful for my applications to have a TimeSpan reference instead. That is what I did with this extension method.
 * 
 * Author: jlafay
 * Submitted on: 9/3/2010 5:44:00 PM
 * 
 * Example: 
 * var myDate = DateTime.Now;
Thread.Sleep(3000);
Console.WriteLine( myDate.TimeElapsed() );
// result -> 00:00:03
 */

        public static TimeSpan TimeElapsed(this DateTime date)
        {
            return DateTime.Now - date;
        }


/*
 * If
 * Executes a function if a given predicate is true
 * 
 * Author: Adam
 * Submitted on: 7/18/2013 8:33:35 PM
 * 
 * Example: 
 * var lang = GetLanguage();

lang.If(l=>l.Name!="Spanish", l=> MessageBox.Show("Non-spanish language!"));
 */

        public static T If<T>(this T val, Func<T, bool> predicate, Func<T, T> func)
        {
            if (predicate(val))
            {
                return func(val);
            }
            return val;
        }


/*
 * ToDictionary()
 * Converts an IEnumerable<IGrouping<TKey,TValue>> from a GroupBy() clause to a Dictionary<TKey, List<TValue>>.
 * 
 * Author: James Michael Hare (BlackRabbitCoder)
 * Submitted on: 10/21/2010 6:14:17 PM
 * 
 * Example: 
 * Dictionary<string, List<Product>> results = products.GroupBy(product => product.Category).ToDictionary();
 */

        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
        public static Dictionary<TKey, List<TValue>> ToMultiDictionary<TKey, TValue>(
            this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }


#if NetFX
/*
 * DataBind
 * Bind to a ListControl (Dropdownlist, listbox, checkboxlist, radiobutton) in minimal amounts of code. Also returns true false if items are in the control after binding and sets the selected index to first value.
 * 
 * Author: Matt G
 * Submitted on: 7/30/2009 5:07:26 PM
 * 
 * Example: 
 * Dictionary<int, string> data = new Dictionary<int,string>();
        data.Add(0,"Apple");
        data.Add(1,"Orange");
        data.Add(2,"Pair");

        DropDownList ddl = new DropDownList();
        ddl.DataBind(data, "value", "key");

        ListBox lbx = new ListBox();
        lbx.DataBind(data, "value", "key");
 */

        public static bool DataBind(this System.Web.UI.WebControls.ListControl control, object datasource,
            string textField, string valueField)
        {
            return DataBind(control, datasource, textField, null, valueField);
        }

        public static bool DataBind(this System.Web.UI.WebControls.ListControl control, object datasource,
            string textField, string textFieldFormat, string valueField)
        {
            control.DataTextField = textField;
            control.DataValueField = valueField;

            if (!string.IsNullOrEmpty(textFieldFormat))
                control.DataTextFormatString = textFieldFormat;

            control.DataSource = datasource;
            control.DataBind();

            if (control.Items.Count > 0)
            {
                control.SelectedIndex = 0;
                return true;
            }
            else return false;
        }
#endif

/*
 * Pivot
 * Groups the elements of a sequence according to a specified firstKey selector function and rotates the unique values from the secondKey selector function into multiple values in the output, and performs aggregations.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 1/21/2009 9:15:07 PM
 * 
 * Example: 
 * class Program {

    internal class Employee {
        public string Name { get; set; }
        public string Department { get; set; }
        public string Function { get; set; }
        public decimal Salary { get; set; }
    }

    static void Main(string[] args) {

        var l = new List<Employee>() {
            new Employee() { Name = "Fons", Department = "R&D", Function = "Trainer", Salary = 2000 },
            new Employee() { Name = "Jim", Department = "R&D", Function = "Trainer", Salary = 3000 },
            new Employee() { Name = "Ellen", Department = "Dev", Function = "Developer", Salary = 4000 },
            new Employee() { Name = "Mike", Department = "Dev", Function = "Consultant", Salary = 5000 },
            new Employee() { Name = "Jack", Department = "R&D", Function = "Developer", Salary = 6000 },
            new Employee() { Name = "Demy", Department = "Dev", Function = "Consultant", Salary = 2000 }};

        var result1 = l.Pivot(emp => emp.Department, emp2 => emp2.Function, lst => lst.Sum(emp => emp.Salary));

        foreach (var row in result1) {
            Console.WriteLine(row.Key);
            foreach (var column in row.Value) {
                Console.WriteLine("  " + column.Key + "\t" + column.Value);

            }
        }

        Console.WriteLine("----");

        var result2 = l.Pivot(emp => emp.Function, emp2 => emp2.Department, lst => lst.Count());

        foreach (var row in result2) {
            Console.WriteLine(row.Key);
            foreach (var column in row.Value) {
                Console.WriteLine("  " + column.Key + "\t" + column.Value);

            }
        }

        Console.WriteLine("----");
    }
}
 */


        public static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>
            Pivot<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source,
                Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector,
                Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }



/*
 * SplitTo
 * Splits a string into an enumerable collection of the specified type containing the substrings in this instance that are delimited by elements of a specified Char array
 * 
 * Author: Magnus Persson
 * Submitted on: 6/1/2009 4:19:52 PM
 * 
 * Example: 
 * const string str = "1,2,3,4,5,6,7";
var col = str.SplitTo<int>(',');
 */

        /// <summary>
        /// Returns an enumerable collection of the specified type containing the substrings in this instance that are delimited by elements of a specified Char array
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="separator">
        /// An array of Unicode characters that delimit the substrings in this instance, an empty array containing no delimiters, or null.
        /// </param>
        /// <typeparam name="T">
        /// The type of the elemnt to return in the collection, this type must implement IConvertible.
        /// </typeparam>
        /// <returns>
        /// An enumerable collection whose elements contain the substrings in this instance that are delimited by one or more characters in separator. 
        /// </returns>
        public static IEnumerable<T> SplitTo<T>(this string str, params char[] separator) where T : IConvertible
        {
            foreach (var s in str.Split(separator, StringSplitOptions.None))
                yield return (T) System.Convert.ChangeType(s, typeof(T));
        }


/*
 * FileSize
 * Get the file size of a given filename.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 8:41:19 AM
 * 
 * Example: 
 * string path = @"D:\WWW\Proj\web.config";
Console.WriteLine("File Size is: {0} bytes.", path.FileSize());
 */

        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            try
            {
                var oFileInfo = new System.IO.FileInfo(filePath);
                bytes = oFileInfo.Length;
            }
            catch
            {
            }
            return bytes;
        }


/*
 * Paramaters
 * This extension method will return all the parameters of an Uri in a Dictionary<string, string>. In case the uri doesn't contain any parameters a empty dictionary will be returned. Somehow I can't believe there is no standard method to do this though... Any additions and/or comments are quite welcome :)
 * 
 * Author: Andre Steenveld
 * Submitted on: 10/14/2010 9:57:19 AM
 * 
 * Example: 
 * Uri 
  without = new Uri( "http://www.abc.com" ),
  with    = new Uri( "http://www.abc.com?v=1" ),
  with2   = new Uri( "http://www.abc.com?v=1&v2=2" );

without.Parameters( ); // Empty dictionary
with.Parameters( );    // <string, string>{{"v", "1"}}
with2.Parameters( );   // <string, string>{{"v", "1"}, {"v2", "2"}}
 */

        public static Dictionary<string, string> Parameters(
            this Uri self)
        {
            return string.IsNullOrEmpty(self.Query)
                ? new Dictionary<string, string>()
                : self.Query.Substring(1).Split('&').ToDictionary(
                    p => p.Split('=')[0],
                    p => p.Split('=')[1]
                );
        }


/*
 * Or
 * Returns the first string with a non-empty non-null value.
 * 
 * Author: Phil Campbell
 * Submitted on: 10/25/2013 10:56:19 PM
 * 
 * Example: 
 * string someResult = someString1.Or(someString2).Or("foo");
 */

        public static string Or(this string input, string alternative)
        {
            return (string.IsNullOrEmpty(input) == false) ? input : alternative;
        }


/*
 * GetValue
 * Simply returns the value property from an XmlNode whether it's null or not. Simplifies using XmlDocuments.
 * 
 * Author: Dave Thieben
 * Submitted on: 6/16/2010 7:31:26 PM
 * 
 * Example: 
 * var xDoc = new XmlDocument();
xDoc.LoadXml(xml);

var xmlNode = xDoc.SelectSingleNode("//result/text()");
string result = xmlNode.GetValue();

// OR

string result = xDoc.SelectSingleNode("//result/text()").GetValue();
 */

        public static string GetValue(this XmlNode node)
        {
            if (node != null)
                return node.Value;
            else
                return string.Empty;
        }


/*
 * GetValue
 * Gets the value of a databinded property-path from an object. The property can have the form "Product.Type.Group".
 * 
 * Author: Chris Meijers
 * Submitted on: 10/26/2010 7:31:15 PM
 * 
 * Example: 
 * var binding = (column as DataGridBoundColumn).Binding;
var path = binding.Path.Path;
//path is for example "Product.Type.Group";

var currentValue = datarow.GetValue(path);
 */

//method recursively calls nested objects
        public static object GetValue(this object o, string path)
        {
            var index = path.LastIndexOf('.');

            if (index > 0)
            {
                var propPath = path.Substring(0, index);
                path = path.Substring(index + 1);
                o = GetValue(o, propPath);
            }

            if (o == null)
                return null;

            var property = o.GetType().GetProperty(path);

            var value = property.GetValue(o, null);

            return value;
        }

#if NetFX

/*
 * ConvertJsonStringToObject
 * Converts a JSON string to an object
 * 
 * Author: Otto Beragg
 * Submitted on: 3/11/2009 10:39:37 AM
 * 
 * Example: 
 * Person per = jsonString.ConvertJsonStringToObject<Person>();
 */

        public static T ConvertJsonStringToObject<T>(this string stringToDeserialize)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(stringToDeserialize);
        }
#endif

/*
 * DefaultIfEmpty
 * The provided DefaultIfEmpty will only accept an instance of T as the default value. Sometimes you need the default to be an IEnumerable<T>.
 * 
 * Author: ThatChuckGuy
 * Submitted on: 9/14/2013 3:48:02 AM
 * 
 * Example: 
 * var coverageList = Coverages.Where(x => x.Equals(coverage, planCode, stateCode))
        .DefaultIfEmpty(Coverages.Where(x => x.Equals(coverage, defaultPlanCode, stateCode)))
        .DefaultIfEmpty(Coverages.Where(x => x.Equals(coverage, defaultPlanCode, defaultStateCode)))
 */

        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source,
            IEnumerable<TSource> defaultValue)
        {
            return (source != null && source.Any()) ? source : defaultValue;
        }


/*
 * Identity
 * Returns the identity of a value
 * 
 * Author: McBrover
 * Submitted on: 3/6/2015 10:18:50 AM
 * 
 * Example: 
 * 5.Identity(); // should return 5
 */

        public static T Identity<T>(this T value)
        {
            return value;
        }


/*
 * LeftOf
 * Returns the left of a string, terminated by a certain character. If the character isn't found the whole string is returned. Ex: string s = "ab-23"; s.LeftOf(s, '-') returns "ab"
 * 
 * Author: Gaston Verelst
 * Submitted on: 10/2/2009 9:31:11 AM
 * 
 * Example: 
 * /// <summary>
///A test for LeftOf
///</summary>
[TestMethod()]
public void LeftOfTest()
{
	string s = "7011(7011)";
	char c = '('; 
	string expected = "7011";
	string actual;
	actual = StringExtensions.LeftOf(s, c);
	Assert.AreEqual(expected, actual);
	actual = StringExtensions.LeftOf(actual, c);	// actual is now 7011
	Assert.AreEqual(expected, actual);
}
 */

        /// <summary>
        /// Returns the first part of the strings, up until the character c. If c is not found in the
        /// string the whole string is returned.
        /// </summary>
        /// <param name="s">String to truncate</param>
        /// <param name="c">Character to stop at.</param>
        /// <returns>Truncated string</returns>
        public static string LeftOf(this string s, char c)
        {
            var ndx = s.IndexOf(c);
            if (ndx >= 0)
            {
                return s.Substring(0, ndx);
            }

            return s;
        }

#if NetFX
/*
 * Load & Save form configuration
 * This extension methods allows you to load/save location, size and window state (normal, maximized, minimized) of any form to single XML file at program runtime.
 * 
 * Author: Marcin Kozub
 * Submitted on: 7/26/2013 11:53:42 AM
 * 
 * Example: 
 * You can load and save form settings by calling methods directly from any place of code:
frmInstance.LoadSettings(@"D:\form.xml");
frmInstance.SaveSattings(@"D:\form.xml");

...or you can override OnLoad & OnClosed methods of any form to load/save settings automatically:
public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    //...
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.LoadSettings(@"D:\form.xml");
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        this.SaveSettings(@"D:\form.xml");
    }
    //...
}
 */

        [Serializable]
        public class FormSettings
        {
            public FormWindowState WindowState { get; set; }
            public Size Size { get; set; }
            public Point Location { get; set; }
        }

        public static void LoadSettings(this Form form, string path)
        {
            if (File.Exists(path))
            {
                using (var sr = new StreamReader(path))
                {
                    var xmlser = new XmlSerializer(typeof(FormSettings));
                    var formSettings = (FormSettings) xmlser.Deserialize(sr);
                    if (formSettings != null)
                    {
                        form.Size = formSettings.Size;
                        form.Location = formSettings.Location;
                        form.WindowState = formSettings.WindowState;
                    }
                }
            }
        }

        public static void SaveSettings(this Form form, string path)
        {
            var formSettings = new FormSettings();
            formSettings.WindowState = form.WindowState;
            if (form.WindowState == FormWindowState.Normal)
            {
                formSettings.Size = form.Size;
                formSettings.Location = form.Location;
            }
            else
            {
                formSettings.Size = form.RestoreBounds.Size;
                formSettings.Location = form.RestoreBounds.Location;
            }
            using (var sw = new StreamWriter(path))
            {
                var xmlSer = new XmlSerializer(typeof(FormSettings));
                xmlSer.Serialize(sw, formSettings);
            }
        }
#endif

/*
 * IsNullOrEmptyThenValue
 * برای حل مشکل مقدار پیشفرض وقتی مقداری وجود ندارد
 * 
 * Author: سید محسن میرشاهرضا
 * Submitted on: 6/9/2014 9:07:57 AM
 * 
 * Example: 
 * "".IsNullOrEmptyThenValue("mohsen") = "mohsen"
"ali".IsNullOrEmptyThenValue("mohsen") = "ali"
 */

        public static string IsNullOrEmptyThenValue(this string str, string value)
        {
            if (string.IsNullOrEmpty(str))
                return value;
            else
                return str;
        }


/*
 * Squared
 * Returns the squared value
 * 
 * Author: unknown
 * Submitted on: 1/24/2008 7:09:07 PM
 * 
 * Example: 
 * int result = 5.Squared();
 */

        public static int Squared(this int intToBeSquared)
        {
            return intToBeSquared * intToBeSquared;
        }

/*
 * CacheGeneratedResults
 * Caches the results of generator methods so that expensive enumerations are not repeated if they are enumerated multiple times. Yet it caches the results lazily, allowing for memory efficiency where possible.
 * 
 * Author: Andrew Arnott
 * Submitted on: 4/30/2012 1:03:52 AM
 * 
 * Example: 
 * // See http://bit.ly/Kl1kSJ for a more complete example and a blog post explaining it all.

public void PrintNumbers() {
	IEnumerable<int> numbers = GetNumbers().CacheGeneratedResults();

	// Print to the screen.
	foreach(int element in numbers) {
		Console.WriteLine(element);
	}

	// Print to the log file as well.
	foreach(int element in numbers) {
		Logger.Log(element);
	}
}
 */

//-----------------------------------------------------------------------
// <copyright file="EnumerableCache.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
//     This code is released under the Microsoft Public License (Ms-PL).
// </copyright>
//-----------------------------------------------------------------------

        /// <summary>
        /// Caches the results of enumerating over a given object so that subsequence enumerations
        /// don't require interacting with the object a second time.
        /// </summary>
        /// <typeparam name="T">The type of element found in the enumeration.</typeparam>
        /// <param name="sequence">The enumerable object.</param>
        /// <returns>
        /// Either a new enumerable object that caches enumerated results, or the original, <paramref name="sequence"/>
        /// object if no caching is necessary to avoid additional CPU work.
        /// </returns>
        /// <remarks>
        /// 	<para>This is designed for use on the results of generator methods (the ones with <c>yield return</c> in them)
        /// so that only those elements in the sequence that are needed are ever generated, while not requiring
        /// regeneration of elements that are enumerated over multiple times.</para>
        /// 	<para>This can be a huge performance gain if enumerating multiple times over an expensive generator method.</para>
        /// 	<para>Some enumerable types such as collections, lists, and already-cached generators do not require
        /// any (additional) caching, and this method will simply return those objects rather than caching them
        /// to avoid double-caching.</para>
        /// </remarks>
        public static IEnumerable<T> CacheGeneratedResults<T>(this IEnumerable<T> sequence)
        {
            // Don't create a cache for types that don't need it.
            if (sequence is IList<T> ||
                sequence is ICollection<T> ||
                sequence is Array ||
                sequence is EnumerableCache<T>)
            {
                return sequence;
            }

            return new EnumerableCache<T>(sequence);
        }

        /// <summary>
        /// A wrapper for <see cref="IEnumerable&lt;T&gt;"/> types and returns a caching <see cref="IEnumerator&lt;T&gt;"/>
        /// from its <see cref="IEnumerable&lt;T&gt;.GetEnumerator"/> method.
        /// </summary>
        /// <typeparam name="T">The type of element in the sequence.</typeparam>
        private class EnumerableCache<T> : IEnumerable<T>
        {
            /// <summary>
            /// The results from enumeration of the live object that have been collected thus far.
            /// </summary>
            private List<T> cache;

            /// <summary>
            /// The original generator method or other enumerable object whose contents should only be enumerated once.
            /// </summary>
            private IEnumerable<T> generator;

            /// <summary>
            /// The enumerator we're using over the generator method's results.
            /// </summary>
            private IEnumerator<T> generatorEnumerator;

            /// <summary>
            /// The sync object our caching enumerators use when adding a new live generator method result to the cache.
            /// </summary>
            /// <remarks>
            /// Although individual enumerators are not thread-safe, this <see cref="IEnumerable&lt;T&gt;"/> should be
            /// thread safe so that multiple enumerators can be created from it and used from different threads.
            /// </remarks>
            private object generatorLock = new object();

            /// <summary>
            /// Initializes a new instance of the EnumerableCache class.
            /// </summary>
            /// <param name="generator">The generator.</param>
            internal EnumerableCache(IEnumerable<T> generator)
            {
                if (generator == null)
                {
                    throw new ArgumentNullException("generator");
                }

                this.generator = generator;
            }

            #region IEnumerable<T> Members

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<T> GetEnumerator()
            {
                if (this.generatorEnumerator == null)
                {
                    this.cache = new List<T>();
                    this.generatorEnumerator = this.generator.GetEnumerator();
                }

                return new EnumeratorCache(this);
            }

            #endregion

            #region IEnumerable Members

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            /// <summary>
            /// An enumerator that uses cached enumeration results whenever they are available,
            /// and caches whatever results it has to pull from the original <see cref="IEnumerable&lt;T&gt;"/> object.
            /// </summary>
            private class EnumeratorCache : IEnumerator<T>
            {
                /// <summary>
                /// The parent enumeration wrapper class that stores the cached results.
                /// </summary>
                private EnumerableCache<T> parent;

                /// <summary>
                /// The position of this enumerator in the cached list.
                /// </summary>
                private int cachePosition = -1;

                /// <summary>
                /// Initializes a new instance of the <see cref="EnumerableCache&lt;T&gt;.EnumeratorCache"/> class.
                /// </summary>
                /// <param name="parent">The parent cached enumerable whose GetEnumerator method is calling this constructor.</param>
                internal EnumeratorCache(EnumerableCache<T> parent)
                {
                    if (parent == null)
                    {
                        throw new ArgumentNullException("parent");
                    }

                    this.parent = parent;
                }

                #region IEnumerator<T> Members

                /// <summary>
                /// Gets the element in the collection at the current position of the enumerator.
                /// </summary>
                /// <returns>
                /// The element in the collection at the current position of the enumerator.
                /// </returns>
                public T Current
                {
                    get
                    {
                        if (this.cachePosition < 0 || this.cachePosition >= this.parent.cache.Count)
                        {
                            throw new InvalidOperationException();
                        }

                        return this.parent.cache[this.cachePosition];
                    }
                }

                #endregion

                #region IEnumerator Properties

                /// <summary>
                /// Gets the element in the collection at the current position of the enumerator.
                /// </summary>
                /// <returns>
                /// The element in the collection at the current position of the enumerator.
                /// </returns>
                object System.Collections.IEnumerator.Current
                {
                    get { return this.Current; }
                }

                #endregion

                #region IDisposable Members

                /// <summary>
                /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
                /// </summary>
                public void Dispose()
                {
                    this.Dispose(true);
                    GC.SuppressFinalize(this);
                }

                #endregion

                #region IEnumerator Methods

                /// <summary>
                /// Advances the enumerator to the next element of the collection.
                /// </summary>
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">
                /// The collection was modified after the enumerator was created.
                /// </exception>
                public bool MoveNext()
                {
                    this.cachePosition++;
                    if (this.cachePosition >= this.parent.cache.Count)
                    {
                        lock (this.parent.generatorLock)
                        {
                            if (this.cachePosition >= this.parent.cache.Count)
                            {
                                if (this.parent.generatorEnumerator.MoveNext())
                                {
                                    this.parent.cache.Add(this.parent.generatorEnumerator.Current);
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;
                }

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// </summary>
                /// <exception cref="T:System.InvalidOperationException">
                /// The collection was modified after the enumerator was created.
                /// </exception>
                public void Reset()
                {
                    this.cachePosition = -1;
                }

                #endregion

                /// <summary>
                /// Releases unmanaged and - optionally - managed resources
                /// </summary>
                /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
                protected virtual void Dispose(bool disposing)
                {
                    // Nothing to do here.
                }
            }
        }


/*
 * Sort (Comparison<T> comparison)
 * stable, in-place sort (mergesort) of a LinkedList<T>. LinkedList<T> has O(1) insertion, great for large lists. this lets you sort it.
 * 
 * Author: Piers Haken
 * Submitted on: 10/30/2010 10:36:32 AM
 * 
 * Example: 
 * LinkedList<int> list = new LinkedList<int> (Enumerable.Range (0, 100).Reverse ());
list.Sort ();
Console.WriteLine (string.Join (", ", from l in list select l.ToString ()));
 */

//
// Summary:
//     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
//     the default comparer.
//
        public static void Sort<T>(this LinkedList<T> @this)
        {
            @this.Sort(Comparer<T>.Default.Compare);
        }

//
// Summary:
//     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
//     the specified comparer.
//
// Parameters:
//   comparer:
//     The System.Collections.Generic.IComparer<T> implementation to use when comparing
//     elements, or null to use the default comparer System.Collections.Generic.Comparer<T>.Default.
//
        public static void Sort<T>(this LinkedList<T> @this, IComparer<T> comparer)
        {
            if (comparer == null)
                comparer = Comparer<T>.Default;
            @this.Sort(comparer.Compare);
        }

//
// Summary:
//     Sorts the elements in the entire System.Collections.Generic.LinkedList<T> using
//     the specified System.Comparison<T>.
//
// Parameters:
//   comparison:
//     The System.Comparison<T> to use when comparing elements.
//
// Exceptions:
//   System.ArgumentNullException:
//     comparison is null.
//
        public static void Sort<T>(this LinkedList<T> @this, Comparison<T> comparison)
        {
            if (@this == null)
                throw new NullReferenceException();

            if (comparison == null)
                throw new ArgumentNullException("comparison");

            var count = @this.Count;
            if (count <= 1)
                return;

            // merge pairs of lists of doubling size
            for (var mergeLength = 1; mergeLength < count; mergeLength *= 2)
            {
                LinkedListNode<T> mergedTail = null;
                LinkedListNode<T> head2;
                for (var head1 = @this.First; head1 != null; head1 = head2)
                {
                    // skip over the 1st part to the start 2nd
                    head2 = head1;
                    int length1;
                    for (length1 = 0; length1 < mergeLength && head2 != null; ++length1)
                        head2 = head2.Next;

                    // assume we have a full-length 2nd part
                    var length2 = mergeLength;

                    // while we still have items to merge
                    while (length1 > 0 || (length2 > 0 && head2 != null))
                    {
                        LinkedListNode<T> next;

                        // determine which part the next item comes from
                        if (length1 != 0 &&
                            !(length2 != 0 && head2 != null && comparison(head1.Value, head2.Value) > 0))
                        {
                            // take item from 1st part
                            Debug.Assert(head1 != null);
                            next = head1;
                            head1 = head1.Next;

                            Debug.Assert(length1 > 0);
                            --length1;
                        }
                        else
                        {
                            // take item from 2nd part
                            Debug.Assert(head2 != null);
                            next = head2;
                            head2 = head2.Next;

                            Debug.Assert(length2 > 0);
                            --length2;
                        }

                        // append the next item to the merged list
                        if (mergedTail == null)
                        {
                            // start a new merged list at the front of the source list
                            if (@this.First != next) // check for no-op
                            {
                                @this.Remove(next);
                                @this.AddFirst(next);
                            }
                        }
                        else if (mergedTail.Next != next) // check for no-op
                        {
                            @this.Remove(next);
                            @this.AddAfter(mergedTail, next);
                        }

                        // advance the merged tail
                        mergedTail = next;
                    }
                }
            }
        }


/*
 * bool IsSorted (Comparison<T> comparison)
 * returns true if a sequence is sorted
 * 
 * Author: Piers Haken
 * Submitted on: 10/30/2010 10:44:55 AM
 * 
 * Example: 
 * Console.WriteLine (Enumerable.Range (0, 10000).IsSorted ());
 */

        public static bool IsSorted<T>(this IEnumerable<T> @this, Comparison<T> comparison = null)
        {
            if (comparison == null)
                comparison = Comparer<T>.Default.Compare;

            using (var e = @this.GetEnumerator())
            {
                if (!e.MoveNext())
                    return true;

                var prev = e.Current;
                while (e.MoveNext())
                {
                    var current = e.Current;
                    if (comparison(prev, current) > 0)
                        return false;

                    prev = current;
                }
            }
            return true;
        }


#if NetFX
/*
 * GetQueryStringValue
 * Gets a query string value from a System.Web.UI.UserControl HTTP Request object.
 * 
 * Author: James Levingston
 * Submitted on: 1/26/2011 9:21:43 PM
 * 
 * Example: 
 * var queryStringVal = this.GetQueryStringValue<int>("param");
 */

        public static T GetQueryStringValue<T>(this System.Web.UI.UserControl control, string name)
        {
            return (T) System.Convert.ChangeType(control.Request.QueryString[name], typeof(T));
        }
#endif

/*
 * HasItems
 * Determines whether an IEnumerable contains any items
 * 
 * Author: Juan Agüí
 * Submitted on: 10/25/2011 3:49:43 PM
 * 
 * Example: 
 * var strings = new []{"hello"};
strings.HasItems(); //true

strings = new String[0];
strings.HasItems(); //false

strings = null;
strings.HasItems(); //false
 */

        /// <summary>
        /// Determines whether an IEnumerable contains any item
        /// </summary>
        /// <param name="enumerable">the IEnumerable</param>
        /// <returns>false if enumerable is null or contains no items</returns>
        public static bool HasItems(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return false;

            try
            {
                var enumerator = enumerable.GetEnumerator();
                if (enumerator != null && enumerator.MoveNext())
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }


/*
 * WriteToConsole
 * Write all elements in the Enumeration to the Console
 * 
 * Author: Fons Sonnemans
 * Submitted on: 12/10/2007 8:47:09 PM
 * 
 * Example: 
 * // Uses class Employee with Name and Salary
var l = new List<Employee>()
{
    new Employee("Fons", 2000),
    new Employee("Jim", 3000),
    new Employee("Ellen", 4000)
};

l.WriteToConsole();

l.WriteToConsole(emp => emp.Name);
 */

        /// <summary>
        /// Write all elements in the Enumeration to the Console
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void WriteToConsole<T>(this IEnumerable<T> list)
        {
            foreach (var obj in list)
            {
                Console.WriteLine(obj);
            }
        }

        /// <summary>
        /// Write all elements in the Enumeration to the Console
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">the enumeration written to the list</param>
        /// <param name="predicate">a transform function to apply to each element</param>
        public static void WriteToConsole<T>(this IEnumerable<T> list, Func<T, object> transfer)
        {
            foreach (var obj in list)
            {
                Console.WriteLine(transfer(obj));
            }
        }


/*
 * IsInteger
 * Checks whether the type is integer.
 * 
 * Author: kevinjong
 * Submitted on: 3/24/2010 8:25:34 AM
 * 
 * Example: 
 * Type type = (1).GetType();
bool isString = type.IsInteger();
 */

        public static bool IsInteger(this Type type)
        {
            return type.Equals(typeof(int));
        }


#if NetFX
/*
 * RemoveCssClass
 * Removes a css class from the webcontrol. Let's say you have a webcontrol (a label for example) with more than one css class: "defaultClass loggedIn". With the RemoveCssClass extension method, you can easily remove one of them.
 * 
 * Author: Kristof Claes
 * Submitted on: 8/11/2010 7:57:32 AM
 * 
 * Example: 
 * ASP.NET Markup:
ASP.NET Markup:
<asp:Label ID="MyLabel" runat="server" CssClass="defaultClass loggedIn" />

C# Codebehind:
if (!userIsLoggedIn)
{
    MyLabel.RemoveCssClass("loggedIn");
}
 */

        public static void RemoveCssClass(this WebControl control, string cssClass)
        {
            var classes = from c in control.CssClass.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                where !c.Equals(cssClass, StringComparison.OrdinalIgnoreCase)
                select c;

            control.CssClass = string.Join(" ", classes);
        }
#endif

/*
 * Fill
 * Different way to use String.Format
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:35:12 PM
 * 
 * Example: 
 * ...
int transactionsCount = GetTransactionsCount();
string message = "We had {0} transactions.".Fill(transactionsCount);
...
 */

        public static string Fill(this string original, params object[] values)
        {
            return string.Format(original, values);
        }

#if NetFX
/*
 * ToJson() and FromJson<T>()
 * Convert an object to JSON an back
 * 
 * Author: Unknown
 * Submitted on: 1/24/2008 7:34:50 PM
 * 
 * Example: 
 * Employee emp = new Employee("Dummy", 5000);
string s = emp.ToJson();
emp = null;
emp = s.FromJson<Employee>();
 */


        public static string ToJson(this object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }

        public static T FromJson<T>(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(obj as string);
        }
#endif

/*
 * Object properties to dictionary converter
 * Takes all public properties of any object and inserts then into a dictionary
 * 
 * Author: Lasse Sjørup
 * Submitted on: 2/9/2015 1:18:52 PM
 * 
 * Example: 
 * var t = new
                        {
                            Bingo = true,
                            Bango = "Test",
                            Bongo = new DateTime(2015, 02, 05)
                        };
            var propertyDictionary = t.GetPropertyDictionary();

            foreach (var o in propertyDictionary)
            {
                Debug.WriteLine(o.Key + " : " + o.Value + " [" + o.Value.GetType().Name + "]");
            }

//Returns
//Bingo : True [Boolean]
//Bango : Test [String]
//Bongo : 05-02-2015 00:00:00 [DateTime]
 */

        public static Dictionary<string, object> GetPropertyDictionary(this object source)
        {
            var properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var result = properties.ToDictionary(propertyInfo => propertyInfo.Name,
                propertyInfo => propertyInfo.GetValue(source));

            return result;
        }


/*
 * ConvertTo
 * Método de Extensión para convertir un String a cualquier tipo de Dato
 * 
 * Author: Jorge García Casalett
 * Submitted on: 6/14/2014 12:10:22 AM
 * 
 * Example: 
 * var texto = "123123";
          var res = texto.ConvertTo<int>();
          res++;
 */

        public static TValue ConvertTo<TValue>(this string text)
        {
            var res = default(TValue);
            var tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
            if (tc.CanConvertFrom(text.GetType()))
                res = (TValue) tc.ConvertFrom(text);
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(text.GetType());
                if (tc.CanConvertTo(typeof(TValue)))
                    res = (TValue) tc.ConvertTo(text, typeof(TValue));
                else
                    throw new NotSupportedException();
            }
            return res;
        }


/*
 * Shuffle
 * Shuffle an array in O(n) time (fastest possible way in theory and practice!)
 * 
 * Author: Maarten van Duren
 * Submitted on: 3/20/2013 12:31:41 AM
 * 
 * Example: 
 * int[] a = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
a.Shuffle();
 */

        public static T[] Shuffle<T>(this T[] list)
        {
            var r = new Random((int) DateTime.Now.Ticks);
            for (var i = list.Length - 1; i > 0; i--)
            {
                var j = r.Next(0, i - 1);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }


/*
 * IsLeapDay
 * Checks if the current day is a leap day
 * 
 * Author: Ricardo Peres
 * Submitted on: 2/29/2012 4:41:00 PM
 * 
 * Example: 
 * Boolean isLeapDay = new DateTime(2012, 2, 29).IsLeapDay();
 */

        public static bool IsLeapDay(this DateTime date)
        {
            return(date.Month == 2 && date.Day == 29);
        }


/*
 * CopyTo
 * Copies a stream to another stream using a passed buffer. it also has an overload to pass a buffer length.
 * 
 * Author: Maziar Rezaei
 * Submitted on: 5/15/2011 11:09:36 AM
 * 
 * Example: 
 * Stream fSource=File.OpenRead(@"C:\test.bin");
Stream fDest=File.Create(@"C:\CopyOfTest.bin");
fSource.CopyTo(fDest,10*1024);
 */

        /// <summary>
        /// Copies an stream to another stream using the buffer passed.
        /// </summary>
        /// <param name="source">source stream</param>
        /// <param name="destination">destination stream</param>
        /// <param name="buffer">buffer to use during copy</param>
        /// <returns>number of bytes copied</returns>
        public static long CopyTo(this Stream source, Stream destination, byte[] buffer)
        {
            long total = 0;
            int bytesRead;
            while (true)
            {
                bytesRead = source.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    return total;
                total += bytesRead;
                destination.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// Copies an stream to another stream using the buffer with specified size.
        /// </summary>
        /// <param name="source">source stream</param>
        /// <param name="destination">destination stream</param>
        /// <param name="bufferLen">length of buffer to create</param>
        /// <returns>number of bytes copied</returns>
        public static long CopyTo(this Stream source, Stream destination, int bufferLen)
        {
            return source.CopyTo(destination, new byte[bufferLen]);
        }


/*
 * SpinThread
 * Spins up and executes the action within a thread. Basically fire and forget. Real big question here. Does anybody see any issues with thread management? I would like to update this with any code necessary to manage thread cleanup if necessary. I realize that this has the ability to create unsafe thread referencing if not written such that the contents of the action are exclusive to the scope of the action, but that is outside the purview of this extension
 * 
 * Author: Daniel Gidman
 * Submitted on: 1/12/2011 6:00:31 PM
 * 
 * Example: 
 * var parms = new {
    id = 1,
    text = "asd"
};

parms.SpinThread(action => {

// perform some logic with action.id and action.text

});
 */

        public static void SpinThread<T>(this T parms, Action<T> action)
        {
            var t =
                new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(p => action((T) p)));
            t.IsBackground = true;
            t.Start(parms);
        }


/*
 * BeginningOfTheMonth
 * Returns datetime corresponding to first day of the month
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:50:55 PM
 * 
 * Example: 
 * var firstDay = DateTime.Now.BeginningOfTheMonth();
 */

        public static DateTime BeginningOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }


/*
 * LCM
 * Uses the Euclidean Algorithm to determine the Least Common Multiplier for an array of integers
 * 
 * Author: Jeff Reddy
 * Submitted on: 12/1/2011 10:39:31 PM
 * 
 * Example: 
 * var numbers = new[] { 21, 63, 91, 119, 154 };
            Console.WriteLine(numbers.LCM());

            var veryLargeNumbers = new [] {596523235,835132529,477218588,2147483646,2028178999,1073741823};
            Console.WriteLine(veryLargeNumbers.LCM());
 */

        /// <summary>
        /// Uses the Euclidean Algorithm to determine the Least Common Multiplier for an array of integers
        /// </summary>
        /// <param name="values">Array of int values</param>
        /// <returns>The Lease Common Multiplier for values provided</returns>
        public static int LCM(this int[] values)
        {
            var retval = values[0];
            for (var i = 1; i < values.Length; i++)
            {
                retval = GCD(retval, values[i]);
            }
            return retval;
        }

        private static int GCD(int val1, int val2)
        {
            while (val1 != 0 && val2 != 0)
            {
                if (val1 > val2)
                    val1 %= val2;
                else
                    val2 %= val1;
            }
            return Math.Max(val1, val2);
        }


/*
 * NullDateToString
 * Prints out a nullable datetime's value (if its not null) in the string format specified as a parameter. A final parameter is specified for what to print if the nullable datetime was, in fact, null.
 * 
 * Author: Graham Peel
 * Submitted on: 12/6/2011 8:08:17 PM
 * 
 * Example: 
 * var nullableDate = GetDateWhichCouldBeNull();
Response.Write(nullableDate.NullDateToString("mm/dd/yy", "-no date found-"));
 */

        public static string NullDateToString(this DateTime? dt, string format = "M/d/yyyy", string nullResult = "")
        {
            if (dt.HasValue)
                return dt.Value.ToString(format);
            else
                return nullResult;
        }

/*
 * Shuffle
 * Shuffle an ArrayList in O(n) time (fastest possible way in theory and practice!)
 * 
 * Author: Maarten van Duren
 * Submitted on: 3/20/2013 12:33:42 AM
 * 
 * Example: 
 * ArrayList a = new ArrayList() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
a.Shuffle();
 */

        public static ArrayList Shuffle(this ArrayList list)
        {
            var r = new Random((int) DateTime.Now.Ticks);
            for (var i = list.Count - 1; i > 0; i--)
            {
                var j = r.Next(0, i - 1);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }

/*
 * Pipe
 * It is like pipe operator in F# and is useful for chaining function calls especially in expressions. (More in http://foop.codeplex.com/)
 * 
 * Author: Kaveh Shahbazian
 * Submitted on: 1/4/2010 12:23:49 AM
 * 
 * Example: 
 * <%= variable.Pipe(x => this.Fun1(x)).Pipe(x =>
{
    ...;
    return this.F2(x);
}) %>
 */

        public static R Pipe<T, R>(this T o, Func<T, R> func)
        {
            if (func == null) throw new ArgumentNullException("func", "'func' can not be null.");
            var buffer = o;
            return func(buffer);
        }

        public static T Pipe<T>(this T o, Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action", "'action' can not be null.");
            var buffer = o;
            action(buffer);
            return buffer;
        }


/*
 * join
 * --
 * 
 * Author: Thomas Kowalski
 * Submitted on: 2/20/2009 4:39:49 PM
 * 
 * Example: 
 * List<string> fruits = new List() { "Apple", "Banana", "Citron" }; fruits.join(", "); //=> "Apple, Banana, Citron"
 */

        public static string join<T>(this IEnumerable<T> list, string separator)
        {
            var s = "";
            foreach (object x in list)
            {
                s += x.ToString() + separator;
            }
            return (s.Length > separator.Length) ? s.Remove(s.Length - separator.Length) : string.Empty;
        }


/*
 * IsEqualMoney
 * Compares two money (decimal) variables ignoring differences above 0.01. Useful for comparing two calculated decimals. 73,414.IsEqualMoney(73,41) returns true.
 * 
 * Author: Loek van den Ouweland
 * Submitted on: 10/1/2010 12:58:46 AM
 * 
 * Example: 
 * if (faktuur.ReedsBetaaldInc.IsEqualMoney
       (faktuur.BedragTotaalInc))
   return new SolidColorBrush(Colors.Green);
        else
   return new SolidColorBrush(Colors.Red);
 */

        public static bool IsEqualMoney(this decimal money1, decimal money2)
        {
            return Math.Abs(money1 - money2) <= 0.01M;
        }


/*
 * Apply a function
 * applies a function to the given value - best used with static methods
 * 
 * Author: Carsten König
 * Submitted on: 2/28/2014 9:08:53 AM
 * 
 * Example: 
 * "Hello".Apply(System.String.IsNullOrWhiteSpace); // = false
"3".Apply(System.Int32.Parse);                   // = 3 (Int32)
 */

        public static B Apply<A, B>(this A a, System.Func<A, B> f)
        {
            return f(a);
        }


/*
 * ToView
 * Extend collections implementing IList to return a DataView. In cases where filters need to be applied to data, this extension will prove handy.
 * 
 * Author: Felipe Ramos
 * Submitted on: 2/23/2011 4:37:03 PM
 * 
 * Example: 
 * public class TestEntity
        {
            public int ID { get; private set; }
            public string Name { get; set; }

            public TestEntity(int ID, string Name)
            {
                this.ID = ID;
                this.Name = Name;
            }

            public static List<TestEntity> GetTestData()
            {
                List<TestEntity> test = new List<TestEntity>();
                test.Add(new TestEntity(1, "Test 1"));
                test.Add(new TestEntity(2, "Test 2"));
                test.Add(new TestEntity(3, "Test 3"));
                test.Add(new TestEntity(4, "Test 4"));

                return test;
            }

            public static System.Data.DataView GetView(List<TestEntity> list)
            {
                return list.ToView();
            }
        }
 */

        /// <summary>
        /// Extend any collection implementing IList to return a DataView.
        /// </summary>
        /// <param name="list">IList (Could be List<Type>)</param>
        /// <returns>DataView</returns>
        public static DataView ToView(this IList list)
        {
            // Validate Source
            if (list.Count < 1)
                return null;

            // Initialize DataTable and get all properties from the first Item in the List.
            var table = new DataTable(list.GetType().Name);
            var properties = list[0].GetType().GetProperties();

            // Build all columns from properties found. (Custom attributes could be added later)
            foreach (var info in properties)
            {
                try
                {
                    table.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                }
                catch (NotSupportedException)
                {
                    // DataTable does not support Nullable types, we want to keep underlying type.
                    table.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));
                }
                catch (Exception)
                {
                    table.Columns.Add(new DataColumn(info.Name, typeof(object)));
                }
            }

            // Add all rows
            for (var index = 0; index < list.Count; index++)
            {
                var row = new object[properties.Length];

                for (var i = 0; i < row.Length; i++)
                {
                    row[i] = properties[i].GetValue(list[index], null); // Get the value for each items property
                }

                table.Rows.Add(row);
            }

            return new DataView(table);
            ;
        }

/*
 * AddTime
 * Adds time to existing DateTime
 * 
 * Author: Cătălin Rădoi
 * Submitted on: 5/21/2015 11:06:02 AM
 * 
 * Example: 
 * var today = DateTime.Today;
var myDate = today.AddTime(1, 55);
 */

        public static DateTime AddTime(this DateTime date, int hour, int minutes)
        {
            return date + new TimeSpan(hour, minutes, 0);
        }


/*
 * GetStringInBetween
 * Get string in between two seprators
 * 
 * Author: Narender Singh
 * Submitted on: 2/22/2011 10:13:40 AM
 * 
 * Example: 
 * string TestGetStringInBetween = "<h1>Hello Narender</h1>";
string[] result;
 result=TestGetStringInBetween.GetStringInBetween("<h1>", "</h1>", false, false);
        Response.Write("<br /><br />StringInBetween is :" + result[0]);
 */

        public static string[] GetStringInBetween(this string strSource, string strBegin, string strEnd,
            bool includeBegin, bool includeEnd)
        {
            string[] result = {"", ""};

            var iIndexOfBegin = strSource.IndexOf(strBegin);

            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired

                if (includeBegin)

                    iIndexOfBegin -= strBegin.Length;

                strSource = strSource.Substring(iIndexOfBegin
                                                + strBegin.Length);

                var iEnd = strSource.IndexOf(strEnd);

                if (iEnd != -1)
                {
                    // include the End string if desired

                    if (includeEnd)

                        iEnd += strEnd.Length;

                    result[0] = strSource.Substring(0, iEnd);

                    // advance beyond this segment

                    if (iEnd + strEnd.Length < strSource.Length)

                        result[1] = strSource.Substring(iEnd
                                                        + strEnd.Length);
                }
            }

            else

                // stay where we are

                result[1] = strSource;

            return result;
        }

/*
 * GetStrMoney
 * this method is convert integer or float money data to separated comma string that is simple to read
 * 
 * Author: vahid shahbazian
 * Submitted on: 11/16/2011 10:35:40 AM
 * 
 * Example: 
 * value in TextBox : 12345
textbox1.text.GetStrMoney() => 12,345

value in TextBox : 1234567.325
textbox1.text.GetStrMoney() => 1,234,567.325
 */

        public static string GetStrMoney(this string digit)
        {
            var afterPoint = string.Empty;
            var strDigit = digit;
            var pos = digit.IndexOf('.');
            if (digit.IndexOf('.') != -1)
            {
                strDigit = digit.Substring(0, pos);
                afterPoint = digit.Substring(pos, digit.Length - pos);
            }

            var len = strDigit.Length;
            if (len <= 3)
                return digit;

            strDigit = strDigit.ReverseString();
            var result = string.Empty;
            for (var i = 0; i < len; i++)
            {
                result += strDigit[i];
                if ((i + 1) % 3 == 0 && i != len - 1)
                    result += ',';
            }

            if (string.IsNullOrEmpty(afterPoint))
                result = result.ReverseString();
            else result = result.ReverseString() + afterPoint;
            return result;
        }

        public static string ReverseString(this string s)
        {
            var c = s.ToCharArray();
            var ts = string.Empty;

            for (var i = c.Length - 1; i >= 0; i--)
                ts += c[i].ToString();

            return ts;
        }


/*
 * Enum.PaseUnstrict
 * Permit Enum Parse everytime with valid values using a defaultValue param
 * 
 * Author: rallets
 * Submitted on: 5/22/2009 9:53:28 PM
 * 
 * Example: 
 * EStatus status = (EStatus)Enum.ParseUnstrict(typeof(EStatus), value, EStatus.unknown));
 */

        public static object ParseUnstrict(this Type enumtype, string value, object defaultValue)
        {
            var res = defaultValue;
            try
            {
                if (value == "")
                {
                    return res;
                }
                res = System.Enum.Parse(enumtype, value);
            }
            catch (Exception)
            {
                //do nothing
            }
            return res;
        }

/*
 * IntToGuid
 * Converts an integer to a Guid. This could be used within a unit test to mock objects.
 * 
 * Author: Tom De Wilde
 * Submitted on: 5/29/2013 1:56:49 PM
 * 
 * Example: 
 * class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public IEnumerable<Customer> GetCustomers()
{
    for (int i = 0; i < 10; i++)
    {
        yield return new Customer
        {
            Id = i.ToGuid(),
            Name = "Name " + i
        };
    }
}
 */

        public static Guid ToGuid(this int value)
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

/*
 * ToStringReccurent
 * Sometimes it is required to collect exception information in textual format. This method serializes general info about exception and all included exceptions reccursively. I'm using this for sending email error reports.
 * 
 * Author: Andriy Kvasnytsya
 * Submitted on: 5/22/2009 10:19:19 AM
 * 
 * Example: 
 * private void appDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
{
	e.Handled = true;
	MessageBox.Show("Application encountered an error, sorry for inconvinience.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
	Process.Start(string.Format("mailto:{0}?subject={1}&body={2}", DeveloperEMail,
		"StatusFlags Exception", e.Exception.ToStringReccurent().UrlEncode()));
	Shutdown();
}
 */

        public static string ToStringReccurent(this Exception exception)
        {
            if (exception == null)
            {
                return "empty";
            }
            return
                $"Exception: {exception.GetType()}\nMessage: {exception.Message}\nStack Trace: {exception.StackTrace}\nInner {exception.InnerException.ToStringReccurent()}";
        }


/*
 * ToTiny
 * Converts a given URI to a TinyUrl.com address. Utilises the TinyUrl.com website so requires that the application can access the server
 * 
 * Author: Craig Hawker
 * Submitted on: 4/6/2009 2:15:48 PM
 * 
 * Example: 
 * Uri longUri = new Uri("http://www.google.com");
            Uri shortUri = longUri.ToTiny();
 */

        public static Uri ToTiny(this Uri longUri)
        {
            var request = WebRequest.Create($"http://tinyurl.com/api-create.php?url={UrlEncode(longUri.ToString())}");
            var response = request.GetResponse();
            Uri returnUri = null;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                returnUri = new Uri(reader.ReadToEnd());
            }
            return returnUri;
        }

        #region Reflected from System.Web.HttpUtility

        private static string UrlEncode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }

        private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count,
            bool alwaysCreateReturnValue)
        {
            var num = 0;
            var num2 = 0;
            for (var i = 0; i < count; i++)
            {
                var ch = (char) bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsSafe(ch))
                {
                    num2++;
                }
            }
            if ((!alwaysCreateReturnValue && (num == 0)) && (num2 == 0))
            {
                return bytes;
            }
            var buffer = new byte[count + (num2 * 2)];
            var num4 = 0;
            for (var j = 0; j < count; j++)
            {
                var num6 = bytes[offset + j];
                var ch2 = (char) num6;
                if (IsSafe(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num4++] = 0x2b;
                }
                else
                {
                    buffer[num4++] = 0x25;
                    buffer[num4++] = (byte) IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte) IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        internal static bool IsSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '\'':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        internal static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char) (n + 0x30);
            }
            return (char) ((n - 10) + 0x61);
        }

        #endregion


/*
 * Intersects
 * Returns true if two date ranges intersect.
 * 
 * Author: Walter Quesada
 * Submitted on: 5/24/2012 8:28:19 PM
 * 
 * Example: 
 * bool eventsInterect = eventXStartDate.Intersects(eventXEndDate, eventYStartDate, eventYEndDate);
 */

        public static bool Intersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate,
            DateTime intersectingEndDate)
        {
            return (intersectingEndDate >= startDate && intersectingStartDate <= endDate);
        }


/*
 * SplitUp()
 * This SplitUp() extension method takes a sequence and splits it up into subsequences that each have a maximum length. See http://peshir.blogspot.nl/2011/02/example-of-c-lazy-functional.html for more information.
 * 
 * Author: peSHIr
 * Submitted on: 5/6/2014 12:47:06 PM
 * 
 * Example: 
 * namespace SplitUpExample
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using peSHIr.Utilities;
 
  class Program
  {
    static bool TraceDataCreation;
         
    static Action<string> println = text => Console.WriteLine(text);
    static Action<string> print = text => Console.Write(text);
    static Action newline = () => Console.WriteLine();
 
    static void Main(string[] args)
    {
      newline();
      println("* How can SplitUp() be used for paging");
      TraceDataCreation = false;
             
      var allData = TestData(64);
      var pagedData = allData.SplitUp(7);
      foreach (var page in pagedData)
      {
        print("Page:");
        foreach (int i in page)
        {
           print(" ");
           print(i.ToString());
        }
        newline();
      }
 
      newline();
      println("* And is it really lazy?");
      TraceDataCreation = true;
             
      println("Calling SplitUp() on infinite sequence now");
      var pagedInfinity = TestData().SplitUp(4);
 
      println("Retrieving first page now");
      var page1 = pagedInfinity.ElementAt(0);
             
      println("Retrieving third page now");
      var page3 = pagedInfinity.ElementAt(2);
             
      Action<string,int,int> results = (text,sum,count)
        => Console.WriteLine("{0}: {1}, {2}", text, sum, count);
 
      println("Showing results:");
      results("First page", page1.Sum(), page1.Count());
      results("Third page", page3.Sum(), page3.Count());
      println("So yes, SplitUp() is lazy like LINQ! ;-)");
 
#if DEBUG
      newline();
      println("(Key to quit)");
      Console.ReadKey();
#endif
    }
 
    static IEnumerable<int> TestData(int n)
    {
      return TestData().Take(n);
    }
 
    static IEnumerable<int> TestData()
    {
      // WARNING: this returns an infinite sequence!
      // Or at least: until int overflows... ;-)
      int i = 0;
      while (true)
      {
        if (TraceDataCreation)
          Console.WriteLine("Yielding {0}", i);
        yield return i++;
      }
    }
 
  }
 
}
 */

        /// <summary>Split up sequence of items</summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="input">Input sequence</param>
        /// <param name="n">Maximum number of items per sublists</param>
        /// <returns>Sequence of lists with a maximum
        /// of <paramref name="n"/> items</returns>
        /// <remarks>Might need a suppression of code analysis rule
        /// CA1006 because of the nested generic type in the method
        /// signature.</remarks>
        public static IEnumerable<IEnumerable<T>>
            SplitUp<T>(this IEnumerable<T> input, int n)
        {
            // Non-lazy error checking
            if (input == null) throw new ArgumentNullException("input");
            if (n < 1) throw new ArgumentOutOfRangeException("n", n, "<1");
            return SplitUpLazy(input, n);
        }

        private static IEnumerable<IEnumerable<T>>
            SplitUpLazy<T>(IEnumerable<T> input, int n)
        {
            // Lazy yield based implementation
            var list = new List<T>();
            foreach (var item in input)
            {
                list.Add(item);
                if (list.Count == n)
                {
                    yield return list;
                    list = new List<T>();
                }
            }
            if (list.Count > 0) yield return list;
            yield break;
        }


#if NetFX
/*
 * Resize To Text Width
 * Resizes width of a Windows control to the text that contains.
 * 
 * Author: Gabriel Espinoza
 * Submitted on: 7/15/2013 10:28:19 PM
 * 
 * Example: 
 * private void AddLabel()
        {
                Label lbl= new Label();
                
                lbl.Name = "myName";
                lbl.Padding = new Padding(0);
                lbl.Margin = new Padding(0);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
                lbl.Text = "Some Random Text";

                lbl.ResizeToTextWidth();
                
                this.Controls.Add(lbl);
}
 */

        /// <summary>
        /// Resizes the control width to the content text
        /// </summary>
        /// <param name="control"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static void ResizeToTextWidth(this Control control)
        {
            using (var g = control.CreateGraphics())
            {
                control.Width = (int) Math.Ceiling(g.MeasureString(control.Text, control.Font).Width);
            }
        }
#endif

#if NetFX
/*
 * FindControlByType
 * Used in conjunction with GetChildren(), it will return a T from a list of children of a control. If you are looking to return a list of T, use FindControlsByType() at http://www.extensionmethod.net/Details.aspx?ID=310 Get Children is located at: http://www.extensionmethod.net/Details.aspx?ID=309
 * 
 * Author: Ryan Helms
 * Submitted on: 7/18/2010 12:08:29 AM
 * 
 * Example: 
 * DistinctType dt= Control.FindControlByType<DistinctType>();
 */

        public static T FindControlByType<T>(this Control ctrl)
        {
            return ctrl.GetChildren().OfType<T>().SingleOrDefault();
        }
#endif

/*
 * To
 * Creates a range of integers as an IEnumerable<Int32>.
 * 
 * Author: Richard Bushnell
 * Submitted on: 3/3/2008 9:14:36 PM
 * 
 * Example: 
 * var numbers = 1.To(10);
 */

        public static IEnumerable<int> To(this int first, int last)
        {
            return Enumerable.Range(first, last - first + 1);
        }


/*
 * IsLastDayOfTheMonth
 * Returns whether the given date is the last day of the month.
 * 
 * Author: Tom De Wilde
 * Submitted on: 12/13/2013 9:22:47 AM
 * 
 * Example: 
 * var isLastDay = DateTime.Now.IsLastDayOfTheMonth()
 */

        public static bool IsLastDayOfTheMonth(this DateTime dateTime)
        {
            return dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }


/*
 * GetAttributes
 * Gets an enumeration of assembly attributes of the specified type from the assembly it is called from.
 * 
 * Author: James Michael Hare (BlackRabbitCoder)
 * Submitted on: 10/14/2010 6:06:27 PM
 * 
 * Example: 
 * var attributes = Assembly.GetExecutingAssembly().GetAttributes<InstallPerformanceCounterAttribute>();
 */

        /// <summary>
        /// Gets the attributes from an assembly.
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="callingAssembly">The calling assembly to search.</param>
        /// <returns>An enumeration of attributes of type T that were found.</returns>
        public static IEnumerable<T> GetAttributes<T>(this Assembly callingAssembly)
            where T : Attribute
        {
            // Try to find the configuration attribute for the default logger if it exists
            object[] configAttributes = Attribute.GetCustomAttributes(callingAssembly,
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
 * XML to Json
 * XML to Json
 * 
 * Author: Keyur Panchal
 * Submitted on: 8/11/2015 9:23:08 AM
 * 
 * Example: 
 * String xmlString = "C:my.xml";
var jsonResult = xmlString.ToJSON();
 */

//// Install nuget package for Newtonsoft json
//// https://www.nuget.org/packages/Newtonsoft.Json/
//// Install-Package Newtonsoft.Json

        public static string ToJSON(this string xml)
        {
            // To convert an XML node contained in string xml into a JSON string
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return JsonConvert.SerializeXmlNode(doc);
        }


/*
 * ThrowIf
 * Throw's a given exception is a given predicate is True
 * 
 * Author: Adam
 * Submitted on: 7/18/2013 8:30:37 PM
 * 
 * Example: 
 * int? tempId = Submit();

tempId.ThrowIf(i=>!i.HasValue,()=>ServiceFault<FatalExceptionFault>.FaultFor("Unable to submit to Temp Database!"));
 */

        public static T ThrowIf<T>(this T val, Func<T, bool> predicate, Func<Exception> exceptionFunc)
        {
            if (predicate(val))
                throw exceptionFunc();
            return val;
        }
    }
}