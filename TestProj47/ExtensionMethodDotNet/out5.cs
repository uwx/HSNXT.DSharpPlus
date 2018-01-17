using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
#if NetFX
using Control = System.Windows.Forms.Control;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {


/*
 * OrderBy
 * OrderBy is nice, except if you want to sort by multiple properties or want an easy way to distinguish between ascending and descending.
 * 
 * Author: Adam Weigert
 * Submitted on: 3/3/2008 4:49:06 PM
 * 
 * Example: 
 * bool descending = (sortDirection = SortDirection.Descending);

PersonGridView.DataSource = persons.OrderBy(descending, p => p.Age, p => p.Name, p => p.Gender).ToList();
 */

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> enumerable,
            Func<TSource, TKey> keySelector, bool descending)
        {
            if (enumerable == null)
            {
                return null;
            }

            if (descending)
            {
                return enumerable.OrderByDescending(keySelector);
            }

            return enumerable.OrderBy(keySelector);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable,
            Func<TSource, IComparable> keySelector1, Func<TSource, IComparable> keySelector2,
            params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }

            var current = enumerable;

            if (keySelectors != null)
            {
                for (var i = keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.OrderBy(keySelectors[i]);
                }
            }

            current = current.OrderBy(keySelector2);

            return current.OrderBy(keySelector1);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> enumerable,
            bool descending, Func<TSource, IComparable> keySelector, params Func<TSource, IComparable>[] keySelectors)
        {
            if (enumerable == null)
            {
                return null;
            }

            var current = enumerable;

            if (keySelectors != null)
            {
                for (var i = keySelectors.Length - 1; i >= 0; i--)
                {
                    current = current.OrderBy(keySelectors[i], descending);
                }
            }

            return current.OrderBy(keySelector, descending);
        }


#if NetFX
/*
 * ScrollToBottom
 * Scrolls to the bottom of a Textbox.
 * 
 * Author: Sean Fox
 * Submitted on: 12/25/2008 8:13:33 AM
 * 
 * Example: 
 * this.txtExample.ScrollToBottom();
 */

        /**
         * <summary>Scrolls to the bottom of a TextBox.</summary>
         *
         * <remarks>The scroll occurs by moving the caret and scrolling to it;
         * therefore you shouldn't use this method if caret placement is
         * important in your application.</remarks>
         */
        public static void ScrollToBottom(this System.Windows.Forms.TextBox box)
        {
            box.SelectionStart = box.TextLength;
            box.ScrollToCaret();
        }
#endif

/*
 * BinarySerializer
 * BinarySerialize a List<T>
 * 
 * Author: RodrigoDotNet
 * Submitted on: 2/27/2013 7:53:45 PM
 * 
 * Example: 
 * query.BinarySerializer(AppDomain.CurrentDomain.BaseDirectory + "\\cidades.dat");
 */

        public static void BinarySerializer<T>(this IList<T> lista, string path)
        {
            if (lista == null)
            {
                throw new ArgumentNullException("lista", "variavel de destino não pode ser nula");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path", "caminho do xml não pode ser nulo ou vazio");
            }

            try
            {
                using (Stream stream = File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    var bin = new BinaryFormatter();
                    bin.Serialize(stream, lista);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


/*
 * GetImageCodecInfo
 * Gets the ImageCodecInfo that corresponds to a ImageFormat.
 * 
 * Author: Lucas
 * Submitted on: 3/7/2008 9:02:31 PM
 * 
 * Example: 
 * ImageCodecInfo codec = ImageFormat.Png.GetImageCodecInfo();
 */

        public static ImageCodecInfo GetImageCodecInfo(this ImageFormat imageFormat)
        {
            if (imageFormat == null)
                throw new ArgumentNullException("imageFormat");

            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(i => i.Clsid == imageFormat.Guid);
        }

/*
 * Multiply
 * Multiplies a TimeSpan by a number (int)
 * 
 * Author: Loek van den Ouweland
 * Submitted on: 2/26/2014 2:16:31 PM
 * 
 * Example: 
 * var ts = timeSpan.Multiply(5);
 */

        public static TimeSpan Multiply(this TimeSpan timeSpan, int multiplier)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks * multiplier);
        }


/*
 * AsBoolean
 * Converts a string to a boolean value if possible or throws an exception
 * 
 * Author: Anonymous
 * Submitted on: 12/25/2014 9:10:51 PM
 * 
 * Example: 
 * "y".AsBoolean() //returns true
"NO".AsBoolean() //returns false
 */

        public static bool AsBoolean(this string value)
        {
            var val = value.ToLower().Trim();
            if (val == "false")
                return true;
            if (val == "f")
                return true;
            if (val == "true")
                return true;
            if (val == "t")
                return true;
            if (val == "yes")
                return true;
            if (val == "no")
                return true;
            if (val == "y")
                return true;
            if (val == "n")
                return true;

            return false;
        }

        public static bool IsBoolean(this string value)
        {
            var val = value.ToLower().Trim();
            if (val == "false")
                return false;
            if (val == "f")
                return false;
            if (val == "true")
                return true;
            if (val == "t")
                return true;
            if (val == "yes")
                return true;
            if (val == "no")
                return false;
            if (val == "y")
                return true;
            if (val == "n")
                return false;
            throw new ArgumentException("Value is not a boolean value.");
        }


/*
 * BinaryDeserializer
 * Deserializa um arquivo binario em uma lista generica
 * 
 * Author: RodrigoDotNet
 * Submitted on: 2/27/2013 8:00:14 PM
 * 
 * Example: 
 * var _estadosCidades=new List<Estado>();
            _estadosCidades = _estadosCidades.BinaryDeserializer("cidades.dat");
 */

        /// <summary>
        /// Deserializa um arquivo binario em uma lista generica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <param name="path">caminho do arquivo</param>
        /// <returns>Lista deserializada</returns>
        public static List<T> BinaryDeserializer<T>(this IList<T> lista, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path", "caminho do xml não pode ser nulo ou vazio");
            }

            try
            {
                var inStr = new FileStream(path, FileMode.Open, FileAccess.Read);
                var bf = new BinaryFormatter();
                var list = bf.Deserialize(inStr) as List<T>;

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }


/*
 * DefaultValue
 * Returns a the value of a Nullable type if it has a value or it will return a default value
 * 
 * Author: Robert Booth
 * Submitted on: 3/30/2009 5:07:27 PM
 * 
 * Example: 
 * bool? HasVideo = null;
bool value = HasVideo.DefaultValue(false);
 */

        public static T DefaultValue<T>(this Nullable<T> value, T defaultValue) where T : struct
        {
            if (value == null || value.HasValue == false)
            {
                return defaultValue;
            }

            return value.Value;
        }


/*
 * ToException
 * Conveniently produces a exception from a given string.
 * 
 * Author: MrMostInteresting
 * Submitted on: 3/6/2015 10:24:43 AM
 * 
 * Example: 
 * "ExceptionText".ToException();
 */
        public static void ToException(this string message)
        {
            throw new Exception(message);
        }


/*
 * Clear
 * clear the contents of a StringBuilder object
 * 
 * Author: Manos Aspradakis
 * Submitted on: 7/15/2011 11:35:35 AM
 * 
 * Example: 
 * StringBuilder sb = new StringBuilder();
//...
sb.Clear();
 */

        public static void Clear(this StringBuilder sb)
        {
            sb.Length = 0;
        }


/*
 * GetRandomItem
 * Return's a random item from a IList<T>
 * 
 * Author: Ewerton Luis de Mattos
 * Submitted on: 11/13/2009 4:04:25 PM
 * 
 * Example: 
 * List<string> myList = new List<string>();
        
        // Populate the List ...
        myList.Add(Something);

        myList.GetRandomItem();
 */
        private static readonly Random _rand = new Random();

        /// <summary>
        /// Sorteia randomicamente um item de um IList e o retorna
        /// </summary>
        /// <typeparam name="T">O tipo da Lista</typeparam>
        /// <param name="input">A lista a ser avaliada</param>
        public static T GetRandomItem<T>(this IList<T> input)
        {
            if (input != null)
            {
                if (input.Count == 1)
                    return input[0];

                var n = _rand.Next(input.Count + 1);

                return input[n];
            }
            return default;
        }


/*
 * FromAppSettings
 * Get a value from AppSettings section of Web.Config and change its type to the correct one or return a default value in case the key doesn't exists.
 * 
 * Author: Adrián Maillo
 * Submitted on: 11/10/2014 10:02:01 PM
 * 
 * Example: 
 * //Example 1
var email = "EMail".FromAppSettings(string.Empty);

//Example 2
var email = "EMail".FromAppSettings("noreply@extensionmethod.net");

//Example 3
var id = "AppID".FromAppSettings(int.MinValue);
 */

        public static T FromAppSettings<T>(this string key, T defaultValue = default(T)) where T : IConvertible
        {
            if (string.IsNullOrWhiteSpace(key))
                return defaultValue;

            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            var tc = Type.GetTypeCode(typeof(T));

            switch (tc)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.String:
                    T output;
                    try
                    {
                        output = (T) System.Convert.ChangeType(value, tc);
                    }
                    catch
                    {
                        output = defaultValue;
                    }

                    return output;

                default:
                    throw new NotSupportedException();
            }
        }


/*
 * TakeFirst
 * Returns the first X characters from a string.
 * 
 * Author: Robert M. Downey
 * Submitted on: 10/18/2010 9:49:18 PM
 * 
 * Example: 
 * string someString = "Awesome";
string firstThree = "Awesome".TakeFirst(3);
//firstThree now is the string "Awe".
 */

        public static string TakeFirst(this string s, int num)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return (s.Length < num ? s : s.Substring(0, num));
        }


/*
 * IsNullThenEmpty
 * A handy extension method for System.String that eliminates this pattern when trying to avoid null reference exceptions. if (someString==null) someString=string.Empty;
 * 
 * Author: Steve Mentzer
 * Submitted on: 10/20/2009 10:11:25 PM
 * 
 * Example: 
 * string theUserData = someTextBox.Text.IsNullThenEmpty();
 */

        /// <summary>
        /// if the string is NULL, converts it to string.empty. Helpful when trying to avoid null conditions.
        /// </summary>
        /// <param name="inString"></param>
        /// <returns></returns>
        public static string IsNullThenEmpty(this string inString)
        {
            if (inString == null)
                return string.Empty;
            else
                return inString;
        }


/*
 * WriteTo
 * Writes the entire contents of this stream to another stream using a buffer.
 * 
 * Author: Magnus Persson
 * Submitted on: 6/1/2009 3:55:30 PM
 * 
 * Example: 
 * public static void CompressTo(Stream sourceStream, Stream stream)
        {
            using (GZipStream zipStream = new GZipStream(stream, CompressionMode.Compress, true))
                sourceStream.WriteTo(zipStream);
        }
 */

        private const int DefaultBufferSize = 0x1000;

        /// <summary>
        /// Writes the entire contents of this stream to another stream using a buffer.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="stream">The stream to write this stream to.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <remarks>
        /// The size of the buffer is 4096 bytes
        /// </remarks>
        public static void WriteTo(this Stream sourceStream, Stream stream)
        {
            WriteTo(sourceStream, stream, DefaultBufferSize);
        }

        /// <summary>
        /// Writes the entire contents of this stream to another stream using a buffer
        /// with the specified size.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="stream">The stream to write this stream to.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        public static void WriteTo(this Stream sourceStream, Stream stream, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            int n;
            while ((n = sourceStream.Read(buffer, 0, buffer.Length)) != 0)
                stream.Write(buffer, 0, n);
        }


/*
 * Cycle
 * Repeats a sequence forever.
 * 
 * Author: Daniel Pratt
 * Submitted on: 6/16/2010 3:48:25 PM
 * 
 * Example: 
 * var people = new[] { "Fred", "Bill", "Susan", "Amy" };
var teamNames = new[] { "Red", "Blue" };
var assignments = people.Zip(teamNames.Cycle(), (p, t) => new { Person = p, Team = t });
var teams =
    from a in assignments
    group a by a.Team into ts
    select new { TeamName = ts.Key, Members = from tm in ts select tm.Person };
 */

        public static IEnumerable<T> CycleAlways<T>(this IEnumerable<T> source)
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

/*
 * CSVQuoted
 * If a string contains a space or a comma or a newline, quotes it, suitable for a field in a CSV file.
 * 
 * Author: David Bakin
 * Submitted on: 3/8/2009 5:15:35 AM
 * 
 * Example: 
 * "this".CSVQuoted()  ==>  "this"
"pots and pans".CSVQuoted() ==> "\"pots and pans\""
"blue,red".CSVQuoted() ==> "\"blue,red\""
"embedded\"quote".CSVQuoted() ==> "\"embedded\"\"quote\""
" goodbye! ".CSVQuoted() ==> "\" goodbye! \""
 */

        public static string CSVQuoted(this string s)
        {
            if (s.IndexOfAny(" ,\n".ToCharArray()) < 0 && s.Trim() == s)
                return s;

            var sb = new StringBuilder();
            sb.Append('"');
            foreach (var c in s)
            {
                sb.Append(c);
                if (c == '"')
                    sb.Append(c);
            }
            sb.Append('"');
            return sb.ToString();
        }

/*
 * Repeat
 * for Repeat String .
 * 
 * Author: Mehrdad Ghasemi
 * Submitted on: 3/28/2009 12:06:20 PM
 * 
 * Example: 
 * string name = "mehrdad";
        Response.Write("Name is : " + name);
        Response.Write("<br />");
        Response.Write(name.Repeat(20,"-"));
 */

        /// <summary>
        /// Repeat String .
        /// </summary>
        /// <param name="input">String</param>
        /// <param name="number">Count Repeat </param>
        /// <param name="splitChar">caracter for Split Repeat </param>
        /// <returns></returns>
        public static string Repeat(this string input, int number, string RepeatChar)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var sb = new StringBuilder();
                for (var i = 1; i <= number; i++)
                {
                    sb.AppendFormat("{0}{1}", input, RepeatChar);
                }
                return sb.Remove(sb.Length - 1, 1).ToString();
            }
            else
            {
                return null;
            }
        }


/*
 * ThrowIfAny
 * Throws a given exception is any value in a set passes a given predicate
 * 
 * Author: Adam
 * Submitted on: 7/18/2013 8:35:24 PM
 * 
 * Example: 
 * categories.ThrowIfAny(c => c == CategoryTypes.Unknown, () => new Exception("The note category must be set."));
 */

        public static IEnumerable<T> ThrowIfAny<T>(this IEnumerable<T> values, Func<T, bool> predicate,
            Func<Exception> exceptionFunc)
        {
            if (values.Any(val => predicate(val)))
                throw exceptionFunc();
            return values;
        }


/*
 * To
 * Allows enumaration of sets of characters by expressing them as a range, for example all the lowercase characters. Allows reverse order as well.
 * 
 * Author: Figment Engine
 * Submitted on: 2/6/2009 7:37:14 PM
 * 
 * Example: 
 * foreach (char c in 'a'.To('z'))
				Console.WriteLine(c);
			foreach (char c in '0'.To('9'))
				Console.WriteLine(c);
 */

        private static void Swap(ref char a, ref char b)
        {
            a ^= b;
            b ^= a;
            a ^= b;
        }

/*
 * RightOf
 * Return the remainder of a string s after a separator c.
 * 
 * Author: Gaston Verelst
 * Submitted on: 6/21/2010 8:20:53 AM
 * 
 * Example: 
 * /// <summary>
        ///A test for RightOf
        ///</summary>
        [TestMethod()]
        public void RightOfTest()
        {
            string s = "XYZ,1234";
            char c = ',';
            string expected = "1234";
            string actual;
            actual = StringExtensions.RightOf(s, c);
            Assert.AreEqual(expected, actual);

            s = "XYZ,1234,ABC";
            c = ',';
            expected = "1234,ABC";
            actual = StringExtensions.RightOf(s, c);
            Assert.AreEqual(expected, actual);

            s = "XYZ";
            c = ',';
            expected = "XYZ";
            actual = StringExtensions.RightOf(s, c);
            Assert.AreEqual(expected, actual);

        }
 */

        /// <summary>
        /// Return the remainder of a string s after a separator c.
        /// </summary>
        /// <param name="s">String to search in.</param>
        /// <param name="c">Separator</param>
        /// <returns>The right part of the string after the character c, or the string itself when c isn't found.</returns>
        public static string RightOf(this string s, char c)
        {
            var ndx = s.IndexOf(c);
            if (ndx == -1)
                return s;
            return s.Substring(ndx + 1);
        }


/*
 * IsStatic
 * Determines if a type is static by checking if it's abstract, sealed, and has no public constructors.
 * 
 * Author: jlafay
 * Submitted on: 11/15/2010 2:58:22 PM
 * 
 * Example: 
 * Type env = typeof(Environment);
Type str = typeof(String);

string result = String.Format("Env-> {0}  Str-> {1}", env.IsStatic(), str.IsStatic());

Console.WriteLine(result);
// Env-> True  Str-> False
 */

        public static bool IsStatic(this Type t)
        {
            var c = t.GetConstructors();
            return (t.IsAbstract && t.IsSealed && c.Length == 0);
        }


/*
 * WriteXMLForReport
 * Many times you need an XSD file for a report. I have created this extension to write the xsd based on the data table I feed it into the directory I set.
 * 
 * Author: John D. Sanders
 * Submitted on: 8/19/2011 2:20:58 AM
 * 
 * Example: 
 * tblAnyTable.WriteXMLForReport("AnyTableOut");
 */

        /// <summary>
        /// Writes xsd for report into specified directory
        /// </summary>
        /// <param name="tblIn">DataTable with XML Schema that you want to write</param>
        /// <param name="outputName">Name of xsd you want back.</param>
        public static void WriteXMLForReport(this DataTable tblIn, string outputName)
        {
            //Set table name in case it is not set
            tblIn.TableName = outputName;
            var fileName = @"C:\OutDir\" + outputName + ".xsd";
            tblIn.WriteXmlSchema(fileName, true);
        }

/*
 * REExtract
 * Extracts all fields from a string that match a certain regex. Will convert to desired type through a standard TypeConverter.
 * 
 * Author: David Bakin
 * Submitted on: 2/28/2009 7:25:48 AM
 * 
 * Example: 
 * public static int[ ] ExtractInts( this string s )
{
    return s.REExtract<int>( @"\d+" );
}

int[] a = "Some primes: 2, 5, 11, and 17".ExtractInts();
// a == { 2, 5, 11, 17 }
 */

        public static T[] REExtract<T>(this string s, string regex)
        {
            var tc = TypeDescriptor.GetConverter(typeof(T));
            if (!tc.CanConvertFrom(typeof(string)))
            {
                throw new ArgumentException("Type does not have a TypeConverter from string", "T");
            }
            if (!string.IsNullOrEmpty(s))
            {
                return
                    Regex.Matches(s, regex)
                        .Cast<Match>()
                        .Select(f => f.ToString())
                        .Select(f => (T) tc.ConvertFrom(f))
                        .ToArray();
            }
            else
                return new T[0];
        }


#if NetFX
/*
 * Force Download any file!
 * Forces your browser to download any kind of file instead of trying to open it inside the browser (e.g. pictures, pdf, mp3). Works in Chrome, Opera, Firefox and IE 7 & 8!
 * 
 * Author: Mikhail Tsennykh
 * Submitted on: 10/14/2010 10:22:20 PM
 * 
 * Example: 
 * // full path to your file
var yourFilePath = HttpContext.Current.Request.PhysicalApplicationPath + "Files\yourFile.jpg";
// save downloaded file as (name)
var saveFileAs = "yourFile.jpg";

// start force download of your file
Response.ForceDownload(yourFilePath, saveFileAs);
 */

        public static void ForceDownload(this HttpResponse Response, string fullPathToFile, string outputFileName)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + outputFileName);
            Response.WriteFile(fullPathToFile);
            Response.ContentType = "";
            Response.End();
        }

/*
 * RemoveClickEvent
 * Remove click event from given button.
 * 
 * Author: Dumitru Condrea
 * Submitted on: 11/25/2010 1:58:43 PM
 * 
 * Example: 
 * btnTest.RemoveClickEvent();
 */

        public static void RemoveClickEvent(this System.Windows.Forms.Button btn)
        {
            var f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
            if (f1 != null)
            {
                var obj = f1.GetValue(btn);
                var pi = btn.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                var list = (EventHandlerList) pi.GetValue(btn, null);
                list.RemoveHandler(obj, list[obj]);
            }
        }
#endif

/*
 * IsNullOrEmpty
 * Indicates whether the specified IEnumerable collection is null or empty
 * 
 * Author: Avi Harush
 * Submitted on: 12/24/2009 6:16:14 PM
 * 
 * Example: 
 * List<string> list = new List<string>();
Console.WriteLine(list.IsNullOrEmpty());
 */

        public static bool IsNullOrEmpty(this IEnumerable iEnumerable)
        {
            if (iEnumerable != null)
            {
                return !iEnumerable.GetEnumerator().MoveNext();
            }
            return true;
        }


/*
 * ForDatabase
 * For use with old school ado.net database command parameters. This basically converts the string to System.DBNull.Value if the string is null else it returns the string.
 * 
 * Author: Tomas Tomasson
 * Submitted on: 1/17/2011 1:10:54 AM
 * 
 * Example: 
 * SqlCommand command = new SqlCommand(commandText, connection);
command.Parameters.Add(new SqlParameter("@Email",email.ForDatabase());
 */

        public static object ForDatabase(this string str)
        {
            if (str == null)
            {
                return System.DBNull.Value;
            }

            return str;
        }


/*
 * Formating
 * formating the string with a custom user-defined format. # sign is input characters.
 * 
 * Author: Mojtaba kaviani
 * Submitted on: 11/14/2010 9:58:56 AM
 * 
 * Example: 
 * string date="20101012";
date.Formating("####/##/##")
 */

        /// <summary>
        /// formating the string with a custom user-defined format.
        /// # sign is input characters.
        /// </summary>
        /// <param name="format">the format string</param>
        /// <returns>formated string</returns>
        public static string Formating(this string input, string format)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            var output = new StringBuilder();
            var j = 0;
            for (var i = 0; i < format.Length; i++)
            {
                switch (format[i])
                {
                    case '#':
                        output.Append(input[i - j]);
                        break;
                    default:
                        output.Append(format[i]);
                        j++;
                        break;
                }
            }
            return output.ToString();
        }


/*
 * RemoveColumn
 * Code that allows deleting a column stating the name
 * 
 * Author: Raphael Augusto Ferroni Cardoso
 * Submitted on: 6/4/2014 9:10:07 PM
 * 
 * Example: 
 * // Consider that dataSetName is the DataSet that contains a DataTable
dataSetName.Tables[0].RemoveColumn("Column12");
 */

        public static void RemoveColumn(this System.Data.DataTable dt, string columnName)
        {
            if (dt != null && !string.IsNullOrEmpty(columnName) && dt.Columns.IndexOf(columnName) >= 0)
            {
                var idx = dt.Columns.IndexOf(columnName);
                dt.Columns.RemoveAt(idx);
                dt.AcceptChanges();
            }
        }


/*
 * ImplementsInterfaces(List<Type> types)
 * Determines if a class object implements an interface type and returns a list of types it actually implements. If no matching type is found an empty list will be returned.
 * 
 * Author: James Levingston
 * Submitted on: 10/19/2010 11:17:03 PM
 * 
 * Example: 
 * var t = new Test();
if (t.ImplementsInterface(new List<Type> { typeof(ITest) }).Count > 0)
 { 
                //Do Something
 }
 */

        public static List<Type> ImplementsInterfaces(this object obj, List<Type> interfaces)
        {
            if (obj == null || interfaces == null)
                return new List<Type>();

            var filter = new System.Reflection.TypeFilter(
                (Type typeObj, object criteriaObj) =>
                {
                    return typeObj.ToString() == criteriaObj.ToString() ? true : false;
                }
            );

            Func<Type, Type> func = (Type t) =>
            {
                return obj.GetType().FindInterfaces(filter, t.FullName).Length > 0 ? t : null;
            };

            return (from i in interfaces select func(i)).Where(t => t == null ? false : true).ToList();
        }


/*
 * DataReader to CSV
 * Export DataReader to CSV (List<String>). Basic example that to export data to csv from a datareader. Handle value if it contains the separator and/or double quotes but can be easily be expended to include culture (date, etc...) , max errors, and more.
 * 
 * Author: Thierry Fierens
 * Submitted on: 6/10/2013 12:51:01 AM
 * 
 * Example: 
 * List<string> rows = null;
using (SqlDataReader dataReader = command.ExecuteReader())
    {
        rows = dataReader.ToCSV(includeHeadersAsFirstRow, separator);
        dataReader.Close();
    }
 */

        public static List<string> ToCSV(this IDataReader dataReader, bool includeHeaderAsFirstRow, string separator)
        {
            var csvRows = new List<string>();
            StringBuilder sb = null;

            if (includeHeaderAsFirstRow)
            {
                sb = new StringBuilder();
                for (var index = 0; index < dataReader.FieldCount; index++)
                {
                    if (dataReader.GetName(index) != null)
                        sb.Append(dataReader.GetName(index));

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }
                csvRows.Add(sb.ToString());
            }

            while (dataReader.Read())
            {
                sb = new StringBuilder();
                for (var index = 0; index < dataReader.FieldCount - 1; index++)
                {
                    if (!dataReader.IsDBNull(index))
                    {
                        var value = dataReader.GetValue(index).ToString();
                        if (dataReader.GetFieldType(index) == typeof(string))
                        {
                            //If double quotes are used in value, ensure each are replaced but 2.
                            if (value.IndexOf("\"") >= 0)
                                value = value.Replace("\"", "\"\"");

                            //If separtor are is in value, ensure it is put in double quotes.
                            if (value.IndexOf(separator) >= 0)
                                value = "\"" + value + "\"";
                        }
                        sb.Append(value);
                    }

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }

                if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
                    sb.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));

                csvRows.Add(sb.ToString());
            }
            dataReader.Close();
            sb = null;
            return csvRows;
        }


/*
 * IsMobileValid
 * For Philippine mobile code but can also be adjusted based on your mobile network code. Check if the given number is a valid formatted international number.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 4/10/2008 3:24:05 PM
 * 
 * Example: 
 * Console.WriteLine("Valid Mobile: +639203782321:\t{0}", "+639303782321".IsValidMobile());
 */

        public static bool IsValidMobile(this string number)
        {
            var bFound = false;
            try
            {
                bFound = Regex.IsMatch(number, @"\A\+\b(639)[012]{1}[0-9]{1}[0-9]{3}[0-9]{4}\b\Z");
            }
            catch (ArgumentException)
            {
            }

            return bFound;
        }


/*
 * ToLocalCurrencyString
 * Convert a double to a string formatted using the local currency settings.
 * 
 * Author: Inge Schepers
 * Submitted on: 12/11/2007 11:45:54 PM
 * 
 * Example: 
 * double test = 145.90;
string testString = test.ToLocalCurrencyString();
 */

        /// <summary>
        /// Format a double using the local culture currency settings.
        /// </summary>
        /// <param name="value">The double to be formatted.</param>
        /// <returns>The double formatted based on the local culture currency settings.</returns>
        public static string ToLocalCurrencyString(this double value)
        {
            return ($"{value:C}");
        }


/*
 * AsNullSafeEnumerable
 * You don't need check whether the collection is null.
 * 
 * Author: Rrr
 * Submitted on: 3/31/2011 4:06:45 PM
 * 
 * Example: 
 * string[] names = null;
foreach (var name in names.AsNullSafeEnumerable()) {
	Console.WriteLine("Hello, {0}", name);
}
 */

        public static System.Collections.Generic.IEnumerable<T> AsNullSafeEnumerable<T>(
            this System.Collections.Generic.IEnumerable<T> collection)
        {
            if (collection != null && !collection.IsEmpty())
            {
                return collection;
            }
            else
            {
                return new T[] { };
            }
        }


/*
 * IDictionary.GetValue
 * Better way to read a C# Dictionary
 * 
 * Author: Kenneth Kasajian
 * Submitted on: 1/28/2015 11:14:51 PM
 * 
 * Example: 
 * var d = new Dictionary<string, string> {{"KeyApple", "ValueApple"}, {"KeyOrange", "ValueOrange"}, {"KeyPear", "ValuePear"}};

            Assert.AreEqual("ValueApple", d.GetValue("KeyApple").Value);
            Assert.IsNull(d.GetValue("XXXXXXX").Value);

            var lazy1 = d.GetValue("KeyApple");
            Assert.IsTrue(lazy1.IsValueCreated);
            Assert.AreEqual("ValueApple", lazy1.Value);

            var lazy2 = d.GetValue("XXXXXXX");
            Assert.IsFalse(lazy2.IsValueCreated);
 */

        /// <summary>
        /// An alternative way to get a value from a dictionary.
        /// The return value is a Lazy object containing the value if the value exists in the dictionary.
        /// If it doesn't exist, the Lazy object isn't initialized.
        /// Therefore, you can use the .IsValueCreated property of Lazy to determine if the object has a value.
        /// In addition, if the dictionary did not have the key, .Value property of Lazy will be return the default value of the type.
        /// such as null for string and 0 for int.
        /// In some cases, simply using the .Value directly of or testing for null may be preferable to testing IsValueCreated
        /// </summary>
        public static Lazy<TValue> GetValue<TValue, TKey>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue retVal;
            if (dictionary.TryGetValue(key, out retVal))
            {
                var retValRef = retVal;
                var lazy = new Lazy<TValue>(() => retValRef);
                retVal = lazy.Value;
                return lazy;
            }

            return new Lazy<TValue>(() => default(TValue));
        }


/*
 * ThrowIfDefault
 * Throws a given Exception if the given object is equal to the default value for the type
 * 
 * Author: Adam
 * Submitted on: 7/18/2013 8:26:08 PM
 * 
 * Example: 
 * var emailData = GetActiveEmailByName(emailName);

emailData.ThrowIfDefault(() => new InvalidOperationException("Unable to load email settings. Is the webconfig key 'EmailName' correct and the record active?"));
 */

        public static T ThrowIfDefault<T>(this T val, Func<Exception> exceptionFunc)
        {
            //found here
            //http://www.extensionmethod.net/csharp/object/isdefaultfortype
            if (val.IsDefaultForType())
                throw exceptionFunc();
            return val;
        }


/*
 * TimesOrUntil
 * Attempts to retrieve a valid a result from your function one or more times with an optional 'in between' step (i.e. delay). Replaces a common code pattern with a more readable, shared pattern.
 * 
 * Author: James White
 * Submitted on: 10/29/2010 4:32:37 PM
 * 
 * Example: 
 * var folderStatus = 5.TimesOrUntil(
                         imap.SelectInbox,
                         state => state.Recent > 0,
                         () => Thread.Sleep(500));
 */

        public static T TimesOrUntil<T>(this int times, Func<T> retrieve, Func<T, bool> validate,
            Action inbetween = null)
        {
            T state;
            var count = 0;

            while (true)
            {
                state = retrieve();

                if (validate(state) || ++count >= times)
                    break;

                if (inbetween != null) inbetween();
            }

            return state;
        }


/*
 * ToSortedString
 * Returns an alphabetically sorted list for all public and instance properties, along with its associated values.
 * 
 * Author: Alvaro Torres Tatis
 * Submitted on: 2/16/2012 8:52:30 PM
 * 
 * Example: 
 * MyClass c = new MyClass();
string data = c.ToSortedString();
 */

        public static string ToSortedString(this object value)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            var values = new SortedDictionary<string, string>();

            var properties = value.GetType().GetProperties(bindingFlags);
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(value, null);
                var maskedValue = propertyValue == null ? "null" : propertyValue.ToString();

                values.Add(propertyName, maskedValue);
            }

            var sb = new StringBuilder();
            foreach (var item in values)
            {
                sb.AppendFormat("{0}={1}{2}", item.Key, item.Value, Environment.NewLine);
            }

            return sb.ToString();
        }

/*
 * ExcelColumnName
 * Returns the excel column name from a column index
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:43:24 PM
 * 
 * Example: 
 * string columnName = 3.ExcelColumnName();
 */

        public static string ExcelColumnName(this int index)
        {
            var chars = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            index -= 1; //adjust so it matches 0-indexed array rather than 1-indexed column

            string columnName;

            var quotient = index / 26;
            if (quotient > 0)
                columnName = ExcelColumnName(quotient) + chars[index % 26];
            else
                columnName = chars[index % 26].ToString();

            return columnName;
        }


/*
 * IsDefaultForType
 * Returns true or false depending on if the given object is equal to the default(T) value of the type.
 * 
 * Author: Adam
 * Submitted on: 7/18/2013 8:23:41 PM
 * 
 * Example: 
 * var obj = SomeComplexFunction();

if(obj.IsDefaultForType())
{
    RunSomethingElse();
}
 */

        public static bool IsDefaultForType<T>(this T val)
        {
            return EqualityComparer<T>.Default.Equals(val, default(T));
        }


/*
 * Quick writeline
 * Write a variable to System.Diagnostics.Debug.WriteLine() (Or other output method)
 * 
 * Author: Rob Leclerc
 * Submitted on: 5/19/2009 10:44:11 PM
 * 
 * Example: 
 * var x = "Example1";
x.WriteLine();

"Example2".WriteLine();

"Example".Write();
3.WriteLine();



//Output
Example1
Example2
Example3
 */

//Output with newline
        public static void WriteLine(this object obj)
        {
            System.Diagnostics.Debug.WriteLine(obj);
        }

//Output without new line
        public static void Write(this object obj)
        {
            System.Diagnostics.Debug.Write(obj);
        }


/*
 * RemoveRightIfPresent
 * Removes end of string if it equals to parameter, otherwise returns origin string
 * 
 * Author: Dmitry
 * Submitted on: 5/17/2013 7:22:07 AM
 * 
 * Example: 
 * string stoTest = "abc@mail.ru";
string res = "abc";
Assert.AreEqual(res,stoTest.RemoveRightIfPresent("@mail.ru"));
 */

        public static string RemoveRightIfPresent(this string s, string remove, bool ignoreCase = false)
        {
            var rlen = remove.Length;
            if (s.EndsWith(remove, ignoreCase, System.Globalization.CultureInfo.CurrentCulture))
            {
                return s.Substring(0, s.Length - rlen);
            }
            else
            {
                return s;
            }
        }


/*
 * GetDate
 * Return the current date and time
 * 
 * Author: Thor Jack
 * Submitted on: 3/25/2015 11:03:18 AM
 * 
 * Example: 
 * object anything = new object();
DateTime date = anything.GetCurrentDate();
 */

        public static DateTime GetCurrentDate(this object source)
        {
            return DateTime.Now;
        }


/*
 * Join
 * This extension method joins the StringBuilder values
 * 
 * Author: Müslüm ÖZTÜRK
 * Submitted on: 9/5/2015 10:56:45 AM
 * 
 * Example: 
 * var sb1=new StringBuilder();
sb1.Append("a");
sb1.Append("b");
sb1.Append("c");

var joined1 = sb1.Join("|"); // return a|b|c

var sb2=new StringBuilder();
sb2.Append("a");

var joined2 = sb2.Join("|"); // return a
 */

        public static string Join(this StringBuilder sb, string seperator)
        {
            if (sb == null) return string.Empty;

            var lst = new List<string>();
            for (var i = 0; i < sb.Length; i++)
            {
                lst.Add(sb[i].ToString());
            }

            return string.Join(seperator, lst.ToArray());
        }


/*
 * UcWords
 * Emulates PHPs ucwords - capitalize each word
 * 
 * Author: Stuart Sillitoe
 * Submitted on: 6/1/2016 12:57:48 PM
 * 
 * Example: 
 * "a sentence of words".UcWords();

string str = "some words to capitalize";
str.UcWords();
 */

        public static StringBuilder UcWords(this string theString)
        {
            var output = new StringBuilder();
            var pieces = theString.Split(' ');
            foreach (var piece in pieces)
            {
                var theChars = piece.ToCharArray();
                theChars[0] = char.ToUpper(theChars[0]);
                output.Append(' ');
                output.Append(new string(theChars));
            }

            return output;
        }

/*
 * GetNestedXml
 * This one allows you to get nested XML from within a node. Let's say you're parsing an HTML file using the XDocument class and you want to pull out the nested code including tags. This is what you can use!
 * 
 * Author: Jakub Kottnauer
 * Submitted on: 6/1/2010 7:16:49 PM
 * 
 * Example: 
 * p.Text = x.Element("Czech").Element("Text").InnerXml();
 */

        public static string GetNestedXml(this XElement xe)
        {
            var xml = new StringBuilder();

            foreach (var node in xe.Nodes())
            {
                xml.Append(node.ToString());
            }

            return xml.ToString();
        }


/*
 * Simplify usage of XmlSerializer
 * Extension for simplify usage of XmlSerializer class. Add extension to any object serialize it to xml. Add extension to string and stream to deserialize objects. All extensions with first check about default constructor
 * 
 * Author: WaSaMaSa
 * Submitted on: 10/20/2010 5:05:52 AM
 * 
 * Example: 
 * List<string> list = new List<string>{"aaa","bbb","ccc"};
List<string> listActual = null;
//Xml serialize to string
string xml = list.ToXml();

//Deserialize from string
listActual = xml.FromXml<List<string>>();

using(var stream = new MemoryStream())
{
   //Serialize to stream
   list.ToXml(stream);

   stream.Position = 0; 
   //Deserialize
   listActual = stream.FromXml<List<string>>();
}
 */

        #region Private fields

        private static readonly Dictionary<RuntimeTypeHandle, XmlSerializer> ms_serializers =
            new Dictionary<RuntimeTypeHandle, XmlSerializer>();

        #endregion

        #region Public methods

        /// <summary>
        ///   Serialize object to xml string by <see cref = "XmlSerializer" />
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value"></param>
        /// <returns></returns>
        public static string ToXml<T>(this T value)
            where T : new()
        {
            var _serializer = GetValue(typeof(T));
            using (var _stream = new MemoryStream())
            {
                using (var _writer = new XmlTextWriter(_stream, new UTF8Encoding()))
                {
                    _serializer.Serialize(_writer, value);
                    return Encoding.UTF8.GetString(_stream.ToArray());
                }
            }
        }

        /// <summary>
        ///   Serialize object to stream by <see cref = "XmlSerializer" />
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value"></param>
        /// <param name = "stream"></param>
        public static void ToXml<T>(this T value, Stream stream)
            where T : new()
        {
            var _serializer = GetValue(typeof(T));
            _serializer.Serialize(stream, value);
        }

        /// <summary>
        ///   Deserialize object from string
        /// </summary>
        /// <typeparam name = "T">Type of deserialized object</typeparam>
        /// <param name = "srcString">Xml source</param>
        /// <returns></returns>
        public static T FromXml<T>(this string srcString)
            where T : new()
        {
            var _serializer = GetValue(typeof(T));
            using (var _stringReader = new StringReader(srcString))
            {
                using (XmlReader _reader = new XmlTextReader(_stringReader))
                {
                    return (T) _serializer.Deserialize(_reader);
                }
            }
        }

        /// <summary>
        ///   Deserialize object from stream
        /// </summary>
        /// <typeparam name = "T">Type of deserialized object</typeparam>
        /// <param name = "source">Xml source</param>
        /// <returns></returns>
        public static T FromXml<T>(this Stream source)
            where T : new()
        {
            var _serializer = GetValue(typeof(T));
            return (T) _serializer.Deserialize(source);
        }

        #endregion

        #region Private methods

        private static XmlSerializer GetValue(Type type)
        {
            XmlSerializer _serializer;
            if (!ms_serializers.TryGetValue(type.TypeHandle, out _serializer))
            {
                lock (ms_serializers)
                {
                    if (!ms_serializers.TryGetValue(type.TypeHandle, out _serializer))
                    {
                        _serializer = new XmlSerializer(type);
                        ms_serializers.Add(type.TypeHandle, _serializer);
                    }
                }
            }
            return _serializer;
        }

        #endregion


/*
 * TimesSelector
 * Inspired from ye good old ruby on rails, provides you with new DateTime instances based on an integer you provide. Look at realfiction.net -> extension methods for more detail
 * 
 * Author: Frank Quednau
 * Submitted on: 3/5/2008 9:22:57 AM
 * 
 * Example: 
 * Console.WriteLine(3.Weeks().Ago);
Console.WriteLine(5.Years().FromNow);
Console.WriteLine(19.Days().From(new DateTime(2007,1,1)));
 */

        public abstract class TimeSelector
        {
            protected TimeSpan myTimeSpan;

            internal int ReferenceValue
            {
                set { myTimeSpan = MyTimeSpan(value); }
            }

            public DateTime Ago
            {
                get { return DateTime.Now - myTimeSpan; }
            }

            public DateTime FromNow
            {
                get { return DateTime.Now + myTimeSpan; }
            }

            public DateTime AgoSince(DateTime dt)
            {
                return dt - myTimeSpan;
            }

            public DateTime From(DateTime dt)
            {
                return dt + myTimeSpan;
            }

            protected abstract TimeSpan MyTimeSpan(int refValue);
        }

        public class WeekSelector : TimeSelector
        {
            protected override TimeSpan MyTimeSpan(int refValue)
            {
                return new TimeSpan(7 * refValue, 0, 0, 0);
            }
        }

        public class DaysSelector : TimeSelector
        {
            protected override TimeSpan MyTimeSpan(int refValue)
            {
                return new TimeSpan(refValue, 0, 0, 0);
            }
        }

        public class YearsSelector : TimeSelector
        {
            protected override TimeSpan MyTimeSpan(int refValue)
            {
                return new TimeSpan(365 * refValue, 0, 0, 0);
            }
        }


/*
 * In
 * Returns true when a number is included in the specified collection.
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:41:54 PM
 * 
 * Example: 
 * int a = SomePlace.A;

if(a.In(2, 4))
 DoSomething();
else
 DoSomethingElse();
 */

        public static bool In(this int number, params int[] collection)
        {
            var isIn = false;

            foreach (var i in collection)
            {
                if (number == i)
                {
                    isIn = true;
                    break;
                }
            }

            return isIn;
        }


/*
 * ContainsNumericChars
 * returns true if string contains numeric chars
 * 
 * Author: UnstableMutex
 * Submitted on: 10/21/2013 2:13:52 PM
 * 
 * Example: 
 * string s = "sdfsdfsf";
bool b = s.ContainsNumericChars();
 */

        public static bool ContainsNumericChars(this string s)
        {
            return s.IndexOfAny("0123456789".ToArray()) >= 0;
        }


/*
 * LessThan
 * Returns a boolean value of true if the item being compared is less than the value of the parameter.
 * 
 * Author: Richard Bushnell
 * Submitted on: 3/3/2008 9:21:35 PM
 * 
 * Example: 
 * var t1 = new Temperature(273);
var t2 = new Temperature(100);

if (t1.LessThan(t2))
  Console.WriteLine("t1 is less than t2");
 */

        public static bool LessThan<T>(this IComparable<T> comparable, T other)
        {
            return comparable.CompareTo(other) < 0;
        }


/*
 * InnerTruncate
 * Truncates the given string by stripping out the center and replacing it with an elipsis so that the beginning and end of the string are retained. For example, "This string has too many characters for its own good."InnerTruncate(32) yields "This string has...its own good."
 * 
 * Author: Partial Method
 * Submitted on: 9/22/2009 1:45:26 AM
 * 
 * Example: 
 * string sentence = "Truncate the really long string.";

var truncated = sentence.InnerTruncate(10);

Assert.AreEqual("Truncate...string.", truncated);
 */

        public static string InnerTruncate(this string value,
            int maxLength)
        {
            // If there is no need to truncate then
            // return what we were given.
            if (string.IsNullOrEmpty(value)
                || value.Length <= maxLength)
            {
                return value;
            } // end if

            // Figure out how many characters would be in 
            // each  half if we were to have
            // exactly the same length string on either side 
            // of the elipsis.
            var charsInEachHalf = (maxLength - 3) / 2;

            // Get the string to the right of the elipsis 
            // and then trim the beginning.  There is no
            // need to have a space immediately following 
            // the elipsis.
            var right = value.Substring(
                    value.Length - charsInEachHalf, charsInEachHalf)
                .TrimStart();

            // Get the string to the left of the elipsis.
            // We don't use "charsInEachHalf " here
            // because we may be able to take more characters
            // than that if "right" was trimmed.
            var left = value.Substring(
                    0, (maxLength - 3) - right.Length)
                .TrimEnd();

            // Concatenate and return the result.
            return $"{left}...{right}";
        } // end InnerTruncate


/*
 * IsNotHidden
 * Filters out directories that are hidden
 * 
 * Author: Nitin Chaudhari
 * Submitted on: 2/3/2012 2:02:45 PM
 * 
 * Example: 
 * var files = new DirectoryInfo(@"\\myserver\C$").GetDirectories().Where(x => x.IsNotHidden()).Select(x => x.Name).ToArray();
 */

        public static bool IsNotHidden(this DirectoryInfo directoryInfo)
        {
            return (directoryInfo.Attributes & FileAttributes.Hidden) == 0;
        }


/*
 * Limit<>
 * Limits a value to a maximum. For example this is usefull if you want to feed a progressBar with values from a source which eventually might exceed an expected maximum. This is a generic extension method with IComparable<T> constraint. So every type which implements the IComparable interface benefits from this extension.
 * 
 * Author: Rainer Hilmer
 * Submitted on: 5/11/2008 6:37:14 PM
 * 
 * Example: 
 * int testValue = 150;
int limitedResult = testValue.Limit(100);
// limitedResult will be 100 because it's the given maximum.
 */

        public static T Limit<T>(this T value, T maximum) where T : IComparable<T>
        {
            return value.CompareTo(maximum) < 1 ? value : maximum;
        }


/*
 * Format
 * Formats any value type
 * 
 * Author: Patrick A. Lorenz
 * Submitted on: 3/19/2008 8:27:38 AM
 * 
 * Example: 
 * Console.WriteLine(DateTime.Now.Format("It's {0:t}"));
 */

        public static string Format<T>(this T value, string format) where T : struct
        {
            return string.Format(format, value);
        }


/*
 * FristChar
 * Select Frist character in string .
 * 
 * Author: Mehrdad Ghasemi
 * Submitted on: 3/28/2009 11:28:49 AM
 * 
 * Example: 
 * string name = "mehrdad";
        Response.Write("Name is : " + name);
        Response.Write("<br />");
        Response.Write("Frist Char : "+name.FristChar());
 */
        public static string FristChar(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length >= 1)
                {
                    return input.Substring(0, 1);
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return null;
            }
        }


/*
 * IndicesOf
 * Finds all the indexes of the give value or values in an enumerable list
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/3/2010 8:46:07 PM
 * 
 * Example: 
 * var t = new Collection<int> { 0,1,2,3,1,1,3,4 };

var first = t.IndicesOf(3);
//returns 3,6
var second = t.IndicesOf(new int[]{0,3});
//returns 0,3,6
 */

        public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> obj, T value)
        {
            return (from i in Enumerable.Range(0, obj.Count())
                where obj.ElementAt(i).Equals(value)
                select i);
        }

        public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> obj, IEnumerable<T> value)
        {
            return (from i in Enumerable.Range(0, obj.Count())
                where value.Contains(obj.ElementAt(i))
                select i);
        }


#if NetFX
/*
 * Say
 * Speaks any object using the speech synthesis API
 * 
 * Author: totally professional rock star coder
 * Submitted on: 3/11/2015 8:14:34 PM
 * 
 * Example: 
 * var files = Say(Directory.GetFiles("c:\\"));
Say("and the total size is");
var size = files.Select(x => new FileInfo(x).Length).Sum();
Say(size);
Say("bytes");
 */

        public static T Say<T>(this T thing)
        {
            const int enumerableLimit = 100;

            object obj = thing;
            if (obj == null)
            {
                obj = "null reference";
            }

            var enumerable = obj as IEnumerable;
            if (enumerable != null && obj.GetType() != typeof(string))
            {
                var items = enumerable.OfType<object>().Take(enumerableLimit + 1).ToList();
                if (items.Count == enumerableLimit + 1)
                {
                    items[enumerableLimit] = "and so on";
                }
                foreach (var x in items)
                {
                    Say(x);
                }
            }
            else
            {
                using (var synth = new System.Speech.Synthesis.SpeechSynthesizer())
                {
                    synth.Speak(obj.ToString());
                }
            }
            return thing;
        }

/*
 * GetSelectedValue
 * Get the selected value of a DropDownList by returning the value stored in the forms collection. This allows you to turn EnableViewState off on a DropDownList and still easily retrieve the selected value
 * 
 * Author: Jeremy Chapman
 * Submitted on: 3/18/2010 11:32:30 PM
 * 
 * Example: 
 * string sValue = ddlSampleDropDown.GetSelectedValue();
 */

        public static string GetSelectedValue(this System.Web.UI.WebControls.DropDownList ddl)
        {
            return System.Web.HttpContext.Current.Request.Form[ddl.UniqueID];
        }
#endif

/*
 * GetData<T>
 * Get a saved value from the app domain and convert it back to its original type
 * 
 * Author: Lee Hull
 * Submitted on: 1/6/2012 2:53:47 PM
 * 
 * Example: 
 * AppDomain.CurrentDomain.GetData<MyClass>("settings");
 */

        public static T GetData<T>(this AppDomain app, string name)
        {
            return (T) System.Convert.ChangeType(app.GetData(name), typeof(T));
        }


#if NetFX
/*
 * FindImmediateParentOfType<T>
 * An extension method to find the parent control of a specific type in asp.net
 * 
 * Author: Haris Munawar
 * Submitted on: 7/21/2011 3:51:41 PM
 * 
 * Example: 
 * TextBox tb = getTextBox();

DataList parentDataList = tb.FindImmediateParentOfType<DataList>();
 */

        public static T FindImmediateParentOfType<T>(this Control control) where T : Control
        {
            var retVal = default(T);
            var parentCtl = control.Parent;
            while (parentCtl != null)
            {
                if (parentCtl is T)
                {
                    retVal = (T) parentCtl;
                    break;
                }
                else
                {
                    parentCtl = parentCtl.Parent;
                }
            }
            return retVal;
        }
#endif

/*
 * MaxObject
 * Selects the object in a list with the maximum value on a particular property
 * 
 * Author: James Pattison
 * Submitted on: 8/6/2013 12:06:47 AM
 * 
 * Example: 
 * List<MyObject> myList = new List<MyObject>
    {
        new MyObject
            {
                ID = 0
            },
        new MyObject
            {
                ID = 1
            },
        new MyObject
            {
                ID = 2
            },

    };

MyObject objectIWant = myList.MaxObject(item => item.ID);
 */

        public static T MaxObject<T, U>(this List<T> source, Func<T, U> selector)
            where U : IComparable<U>
        {
            if (source == null) throw new ArgumentNullException("source");
            var first = true;
            var maxObj = default(T);
            var maxKey = default(U);
            foreach (var item in source)
            {
                if (first)
                {
                    maxObj = item;
                    maxKey = selector(maxObj);
                    first = false;
                }
                else
                {
                    var currentKey = selector(item);
                    if (currentKey.CompareTo(maxKey) > 0)
                    {
                        maxKey = currentKey;
                        maxObj = item;
                    }
                }
            }
            if (first) throw new InvalidOperationException("Sequence is empty.");
            return maxObj;
        }


/*
 * ContainsNoSpaces
 * Checks if a string contains no spaces
 * 
 * Author: R. van Duren
 * Submitted on: 9/4/2008 1:23:10 PM
 * 
 * Example: 
 * if (!textBoxUserIdNew.Text.Trim().ContainsNoSpaces())
 */

        public static bool ContainsNoSpaces(this string s)
        {
            var regex = new Regex(@"^[a-zA-Z0-9]+$");
            return regex.IsMatch(s);
        }


/*
 * ExcelColumnIndex
 * Returns the excel column index from a column name
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:39:47 PM
 * 
 * Example: 
 * int bIndex = "B".ExcelColumnIndex();

int amIndex = "AM".ExcelColumnIndex();
 */

        public static int ExcelColumnIndex(this string columnName)
        {
            var number = 0;
            var pow = 1;

            for (var i = columnName.Length - 1; i >= 0; i--)
            {
                number += (columnName[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number;
        }


/*
 * WrapEachWithTag
 * Creates a string that is each the elements' ToString() values wrapped in the 'tag' that is passed as a param. Good for converting an IEnum<T> into a block of HTML/XML.
 * 
 * Author: Graham Peel
 * Submitted on: 12/6/2011 8:18:24 PM
 * 
 * Example: 
 * IEnumerable<string> employeeNames = GetEmployeeNames();
myDiv.InnerHtml += "<ul>";
myDiv.InnerHtml += employeeNames.WrapEachWithTag("li");
myDiv.InnerHtml += "</ul>";

// prints: "<ul><li>Bob</li><li>Fred</li><li>Anne</li></ul>"
 */

        public static string WrapEachWithTag<T>(this IEnumerable<T> source, string tagToWrap)
        {
            var tag = $"</{tagToWrap}>";
            var s = "";
            foreach (var item in source)
            {
                s += tag.Replace(@"/", "") + item.ToString() + tag;
            }
            return s;
        }


/*
 * TakeFrom
 * Returns the remaining characters in a target string, starting from a search string. If the search string is not found in the target, it returns the full target string.
 * 
 * Author: Brett Veenstra
 * Submitted on: 9/18/2009 4:53:41 PM
 * 
 * Example: 
 * string s = "abcde";

Console.WriteLine (s.TakeFrom("d"));   // "de"
 */

        /// <summary>
        /// Returns the contents of a string starting with the location of the searchFor
        /// </summary>
        /// <param name="s">The string to search.</param>
        /// <param name="searchFor">The string to search for.</param>
        /// <returns></returns>
        public static string TakeFrom(this string s, string searchFor)
        {
            if (s.Contains(searchFor))
            {
                var length = Math.Max(s.Length, 0);

                var index = s.IndexOf(searchFor);

                return s.Substring(index, length - index);
            }

            return s;
        }


/*
 * Add Data to Dropdownlist,Radiobutton List etc.
 * Add Data to Dropdownlist,Radiobutton List etc
 * 
 * Author: Ramvilas Ramsevak Varma
 * Submitted on: 8/10/2015 4:07:18 PM
 * 
 * Example: 
 * AddItemsToDropdownRadioEtc<DropDownList>(ref DropDownList1,dataTable1);
 */

        public static void AddItemsToDropdownRadioEtc<T>(ref T control, DataTable data)
        {
            dynamic ControlToAdd = (T) System.Convert.ChangeType((object) control, typeof(T));
            ControlToAdd.DataSource = data;
            ControlToAdd.DataValueField = "MajorID";
            ControlToAdd.DataTextField = "MajorName";
            ControlToAdd.DataBind();
        }


/*
 * DrawAndFillRoundedRectangle
 * Draw and fill a rectangle with some (or all) the angles rounded.
 * 
 * Author: Mattia Belletti
 * Submitted on: 8/26/2010 11:03:10 AM
 * 
 * Example: 
 * protected override void OnPaint(PaintEventArgs e)
{
  // draw a rounded rectangle in the control area
  e.Graphics.DrawAndFillRoundedRectangle(Pens.Black, Brushes.Yellow, new Rectangle(0, 0, Width, Height), 5, 5, RectAngles.All);
}
 */

        /// <summary>
        /// Angles of a rectangle.
        /// </summary>
        public enum RectAngles
        {
            None = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomLeft = 4,
            BottomRight = 8,
            All = TopLeft | TopRight | BottomLeft | BottomRight
        }

        /// <summary>
        /// Draw and fill a rounded rectangle.
        /// </summary>
        /// <param name="g">The graphics object to use.</param>
        /// <param name="p">The pen to use to draw the rounded rectangle. If <code>null</code>, the border is not drawn.</param>
        /// <param name="b">The brush to fill the rounded rectangle. If <code>null</code>, the internal is not filled.</param>
        /// <param name="r">The rectangle to draw.</param>
        /// <param name="horizontalDiameter">Horizontal diameter for the rounded angles.</param>
        /// <param name="verticalDiameter">Vertical diameter for the rounded angles.</param>
        /// <param name="rectAngles">Angles to round.</param>
        public static void DrawAndFillRoundedRectangle(this Graphics g, Pen p, Brush b, Rectangle r,
            int horizontalDiameter, int verticalDiameter, RectAngles rectAngles)
        {
            // get out data
            var x = r.X;
            var y = r.Y;
            var width = r.Width;
            var height = r.Height;
            // adapt horizontal and vertical diameter if the rectangle is too little
            if (width < horizontalDiameter)
                horizontalDiameter = width;
            if (height < verticalDiameter)
                verticalDiameter = height;
            /*
             * The drawing is the following:
             *
             *             a
             *      P______________Q
             *    h /              \ b
             *   W /                \R
             *    |                  |
             *  g |                  | c
             *   V|                  |S
             *    f\                / d
             *     U\______________/T
             *             e
             */
            bool tl = (rectAngles & RectAngles.TopLeft) != 0,
                tr = (rectAngles & RectAngles.TopRight) != 0,
                br = (rectAngles & RectAngles.BottomRight) != 0,
                bl = (rectAngles & RectAngles.BottomLeft) != 0;
            var pointP = tl ? new Point(x + horizontalDiameter / 2, y) : new Point(x, y);
            var pointQ = tr ? new Point(x + width - horizontalDiameter / 2 - 1, y) : new Point(x + width - 1, y);
            var pointR = tr ? new Point(x + width - 1, y + verticalDiameter / 2) : pointQ;
            var pointS = br
                ? new Point(x + width - 1, y + height - verticalDiameter / 2 - 1)
                : new Point(x + width - 1, y + height - 1);
            var pointT = br ? new Point(x + width - horizontalDiameter / 2 - 1) : pointS;
            var pointU = bl ? new Point(x + horizontalDiameter / 2, y + height - 1) : new Point(x, y + height - 1);
            var pointV = bl ? new Point(x, y + height - verticalDiameter / 2 - 1) : pointU;
            var pointW = tl ? new Point(x, y + verticalDiameter / 2) : pointP;
            using (var gp = new GraphicsPath())
            {
                // a
                gp.AddLine(pointP, pointQ);
                // b
                if (tr)
                    gp.AddArc(x + width - horizontalDiameter - 1, y, horizontalDiameter, verticalDiameter, 270, 90);
                // c
                gp.AddLine(pointR, pointS);
                // d
                if (br)
                    gp.AddArc(x + width - horizontalDiameter - 1, y + height - verticalDiameter - 1, horizontalDiameter,
                        verticalDiameter, 0, 90);
                // e
                gp.AddLine(pointT, pointU);
                // f
                if (bl)
                    gp.AddArc(x, y + height - verticalDiameter - 1, horizontalDiameter, verticalDiameter, 90, 90);
                // g
                gp.AddLine(pointV, pointW);
                // h
                if (tl)
                    gp.AddArc(x, y, horizontalDiameter, verticalDiameter, 180, 90);
                // end
                gp.CloseFigure();
                // draw
                if (b != null)
                    g.FillPath(b, gp);
                if (p != null)
                    g.DrawPath(p, gp);
            }
        }


/*
 * ToDistinctDictionary
 * Creates an IDictionary&lt;TKey, TValue&gt; from the IEnumerable&lt;TSource&gt; instance based on the key selector and element selector. This is distinct by using the built-in index of the dictionary instance for either adding or updating a keys corresponding value.
 * 
 * Author: David Michael Pine
 * Submitted on: 11/10/2014 3:42:30 PM
 * 
 * Example: 
 * var dictionary = someList.ToDistinctDictionary(li => li.Key, li => li);
 */

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an 
        /// <see cref="T:System.Collections.Generic.IEnumerable`1"/> according to a specified 
        /// key selector function, and an element selector function.
        /// </summary>
        public static IDictionary<TKey, TElement> ToDistinctDictionary<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            if (source == null) throw new NullReferenceException("The 'source' cannot be null.");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            if (elementSelector == null) throw new ArgumentNullException("elementSelector");

            var dictionary = new Dictionary<TKey, TElement>();
            foreach (var current in source)
            {
                dictionary[keySelector(current)] = elementSelector(current);
            }
            return dictionary;
        }


/*
 * IndicesOf
 * Gets all the indexes in which a certain substring appears within the string.
 * 
 * Author: Omer Raviv
 * Submitted on: 12/25/2010 12:41:12 AM
 * 
 * Example: 
 * [Test]
        public void TestStringIndicesOf()
        {
            CollectionAssert.AreEquivalent("babbab".IndicesOf("b"),  new[] { 0,2,3,5});
            CollectionAssert.AreEquivalent("babbab".IndicesOf("ba"), new[] { 0, 3 });
            CollectionAssert.AreEquivalent("babbab".IndicesOf("cgesahghts"), Enumerable.Empty<int>());
            CollectionAssert.AreEquivalent("babbab".IndicesOf(string.Empty), Enumerable.Empty<int>());
        }
 */

        public static IEnumerable<int> IndicesOf(this string searchIn, string searchFor)
        {
            if (string.IsNullOrEmpty(searchFor)) yield break;

            var lastLoc = searchIn.IndexOf(searchFor);
            while (lastLoc != -1)
            {
                yield return lastLoc;
                lastLoc = searchIn.IndexOf(searchFor, startIndex: lastLoc + 1);
            }
        }


/*
 * ToReversedDateTime
 * Takes a DateTime object and reverses it to an SQL type string (yyyy-mm-dd hh:MM:ss)
 * 
 * Author: Jon Preece
 * Submitted on: 10/25/2010 12:58:29 AM
 * 
 * Example: 
 * string reversed = DateTime.Now.ToReversedDateTime();
 */

        internal static string ToReversedDateTime(this DateTime value)
        {
            return $"{value:u}";
        }


/*
 * AppendLine (overrride)
 * Adds an override to the System.Text.StringBuilder AppendLine method which takes a second parameter so can be used like AppendFormat but also creates a new line.
 * 
 * Author: Robin Norton
 * Submitted on: 5/19/2015 5:52:07 PM
 * 
 * Example: 
 * StringBuilder myStringBuilder = new StringBuilder();
myStringBuilder.AppendLine("This is the first line");
myStringBuilder.AppendLine("This is the {0} line", "second");
 */

        public static StringBuilder AppendLine(this StringBuilder sb, string format, params object[] arguments)
        {
            var value = string.Format(format, arguments);
            sb.AppendLine(value);
            return sb;
        }


/*
 * Intuitive date creation
 * Allows you to create date very easily, like 19.June(1970)
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/14/2009 10:18:12 PM
 * 
 * Example: 
 * GenerateReport( 10.May(2009) );
 */

        public static DateTime January(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }

        public static DateTime February(this int day, int year)
        {
            return new DateTime(year, 2, day);
        }

        public static DateTime March(this int day, int year)
        {
            return new DateTime(year, 3, day);
        }

        public static DateTime April(this int day, int year)
        {
            return new DateTime(year, 4, day);
        }

        public static DateTime May(this int day, int year)
        {
            return new DateTime(year, 5, day);
        }

        public static DateTime June(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }

        public static DateTime July(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }

        public static DateTime August(this int day, int year)
        {
            return new DateTime(year, 8, day);
        }

        public static DateTime September(this int day, int year)
        {
            return new DateTime(year, 9, day);
        }

        public static DateTime October(this int day, int year)
        {
            return new DateTime(year, 10, day);
        }

        public static DateTime November(this int day, int year)
        {
            return new DateTime(year, 11, day);
        }

        public static DateTime December(this int day, int year)
        {
            return new DateTime(year, 12, day);
        }


/*
 * ConstrainToRange
 * Many times you may wish to impose boundaries on what a certain variable can be. This is especially useful for validating user input. For any comparable, it simply returns the value, truncated by a minimum or maximum
 * 
 * Author: David Harris
 * Submitted on: 8/21/2010 1:12:09 AM
 * 
 * Example: 
 * double d = 5.5;
Console.WriteLine("{0:0.00}", d.ConstrainToRange(1.0, 5.0));
 */

        public static T ConstrainToRange<T>(this T d, T min, T max) where T : IComparable
        {
            if (d.CompareTo(min) < 0) return min;
            else if (d.CompareTo(max) > 0) return max;
            else return d;
        }


/*
 * EqualsByValue
 * Determines whether two String objects have the same value. Null and String.Empty are considered equal values.
 * 
 * Author: Omniscient Technology Inc
 * Submitted on: 10/5/2009 4:28:16 PM
 * 
 * Example: 
 * bool areEqual = a.EqualsByValue(b);
 */

        public static bool EqualsByValue(this string inString, string compared)
        {
            if (string.IsNullOrEmpty(inString) && string.IsNullOrEmpty(compared))
                return true;

            // If we get here, then "compared" necessarily contains data and therefore, strings are not equal.
            if (inString == null)
                return false;

            // Turn down to standard equality check.
            return inString.Equals(compared);
        }


/*
 * Fill/Draw RoundedRectangle
 * C# extension to Fill and or Draw RoundedRectangle
 * 
 * Author: Jerry Knauss
 * Submitted on: 3/22/2014 7:34:12 PM
 * 
 * Example: 
 * protected override void OnPaint( PaintEventArgs e )
		{
			e.Graphics.FillRoundedRectangle( new Pen( Color.Black ), null, 0, 0, 200, 200, 12 );
		}
 */

        public static void FillRoundedRectangle(this Graphics g, Pen pen, Brush brush, int x, int y, int width,
            int height, int radius)
        {
            var corner = new Rectangle(x, y, radius, radius);
            var path = new GraphicsPath();
            path.AddArc(corner, 180, 90);
            corner.X = x + width - radius;
            path.AddArc(corner, 270, 90);
            corner.Y = y + height - radius;
            path.AddArc(corner, 0, 90);
            corner.X = x;
            path.AddArc(corner, 90, 90);
            path.CloseFigure();

            g.FillPath(brush, path);

            if (pen != null)
            {
                g.DrawPath(pen, path);
            }
        }


/*
 * AppendNode
 * Append new child XmlElement to base XmlElement.
 * 
 * Author: Dumitru Condrea
 * Submitted on: 11/25/2010 2:19:34 PM
 * 
 * Example: 
 * XmlDocument doc = new XmlDocument();
XmlElement root = doc.CreateElement("Root");
doc.AppendChild(root);
root.AppendNode("Child 1 Name", "Child 1 Value");
root.AppendNode("Child 2 Name", 123);
 */

        public static void AppendNode<T>(this XmlElement root, string name, T value)
        {
            var doc = root.OwnerDocument;
            if (doc != null)
            {
                var code = doc.CreateElement(name);
                var codeText = doc.CreateTextNode(value.ToString());
                root.AppendChild(code);
                code.AppendChild(codeText);
            }
        }
    }
}