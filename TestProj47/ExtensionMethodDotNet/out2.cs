using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using SD = System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CSharp;
using SDD2D = System.Drawing.Drawing2D;
#if NetFX
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Threading;
#endif

namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * ToHashTable
 * Converts a DataRow into a Hashtable.
 * 
 * Author: Jonnidip
 * Submitted on: 6/8/2009 2:05:04 PM
 * 
 * Example: 
 * Hashtable ht = myDataRow.ToHashTable();
 */

        /// <summary>
        /// Converts a DataRow object into a Hashtable object, 
        /// where the key is the ColumnName and the value is the row value.
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable ToHashTable(this DataRow dr)
        {
            var htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);

            return htReturn;
        }


/*
 * IList<T> to Excel file
 * An extension method that produce a excel file of List<T>. This would be useful if you want to automatically generate a Excel out of any other primative data type collection I provided 1 overloads of this method, that accepts a Path as string to save excel file to location on disk.
 * 
 * Author: Saurabh dubey
 * Submitted on: 12/5/2011 11:44:37 AM
 * 
 * Example: 
 * public class DummyData
    {

        [DisplayName("# ID")]
        public string ID { get; set; }

        [DisplayName("Full Name")]
        public string Name { get; set; }


        public string Address { get; set; }


        public string Age { get; set; }


    }



private void button1_Click(object sender, EventArgs e)
        {
            List<DummyData> dataList = new List<DummyData>();

            for (int i = 0; i < 10; i++)
            {
                dataList.Add(new DummyData()
                {
                    Address = "Addresss " + i,
                    Age = "Age " + i,
                    ID = "ID " + i,
                    Name = "Name " + i
                });
            }

            dataList.ToExcel("C:\\test.xls");
        }
 */


/*
 * Flatten
 * Flatten an IEnumerable<string>
 * 
 * Author: Yves Schelpe (yves.schelpe@gmail.com)
 * Submitted on: 9/7/2013 5:47:01 PM
 * 
 * Example: 
 * [Fact]
        public void Can_Flatten_With_Seperator()
        {
            // Arrange
            var list = Infrastructure.GetListOfStrings().Take(3);
            var expected = "monday; tuesday; wednesday";

            // Act
            var sut = list.Flatten("; ", "", "");

            // Assert
            Assert.Equal<string>(expected, sut);
        }

        [Fact]
        public void Can_Flatten_With_Head_And_Tail()
        {
            // Arrange
            var list = Infrastructure.GetListOfStrings().Take(3);
            var expected = "Days: monday, tuesday, wednesday.";

            // Act
            var sut = list.Flatten(", ", "Days: ", ".");

            // Assert
            Assert.Equal<string>(expected, sut);
        }

        [Fact]
        public void Can_Flatten_With_Prefix_And_Suffix()
        {
            // Arrange
            var list = Infrastructure.GetListOfStrings().Take(2);
            var expected = "<days><day>monday</day><day>tuesday</day></days>";

            // Act
            var sut = list.Flatten("<day>", "</day>", "<days>", "</days>");

            // Assert
            Assert.Equal<string>(expected, sut);
        }

        [Fact]
        public void Returns_Empty_String_With_Empty_Source()
        {
            // Arrange
            var list = Infrastructure.GetEmptyListOfStrings();
            var expected = String.Empty;

            // Act
            var sut = list.Flatten("; ", "head", "tail");

            // Assert
            Assert.Equal<string>(expected, sut);
        }

        [Fact]
        public void Returns_Empty_String_When_Source_Is_Null()
        {
            // Arrange
            var list = (List<string>)null;
            var expected = String.Empty;

            // Act
            var sut = list.Flatten("; ", "head", "tail");

            // Assert
            Assert.Equal<string>(expected, sut);
        }
 */

        /// <summary>
        /// Flattens an <see cref="IEnumerable"/> of <see cref="String"/> objects to a single string, seperated by an optional seperator and with optional head and tail.
        /// </summary>
        /// <param name="strings">The string objects to be flattened.</param>
        /// <param name="seperator">The seperator to be used between each string object.</param>
        /// <param name="head">The string to be used at the beginning of the flattened string. Defaulted to an empty string.</param>        
        /// <param name="tail">The string to be used at the end of the flattened string. Defaulted to an empty string.</param>
        /// <returns>Single string containing all elements seperated by the given seperator, with optionally a head and/or tail.</returns>
        public static string Flatten(this IEnumerable<string> strings, string seperator, string head, string tail)
        {
            // If the collection is null, or if it contains zero elements,
            // then return an empty string.
            if (strings == null || !strings.Any())
                return string.Empty;

            // Build the flattened string
            var flattenedString = new StringBuilder();

            flattenedString.Append(head);
            foreach (var s in strings)
                flattenedString.AppendFormat("{0}{1}", s, seperator); // Add each element with the given seperator.
            flattenedString.Remove(flattenedString.Length - seperator.Length,
                seperator.Length); // Remove the last seperator
            flattenedString.Append(tail);

            // Return the flattened string
            return flattenedString.ToString();
        }

        /// <summary>
        /// Flattens an <see cref="IEnumerable"/> of <see cref="String"/> objects to a single string with optional prefix and/or suffix for each string element.
        /// </summary>
        /// <param name="strings">The <see cref="IEnumerable"/> of <see cref="String"/> objects to flatten.</param>
        /// <param name="prefix">String placed before each string element.</param>
        /// <param name="suffix">String placed after each string element.</param>
        /// <param name="head">The string to be used at the beginning of the flattened string. Defaulted to an empty string.</param>        
        /// <param name="tail">The string to be used at the end of the flattened string. Defaulted to an empty string.</param>
        /// <returns>Single string containing all elements with the given preifx and/or suffix and with optionally a head and/or tail.</returns>
        public static string Flatten(this IEnumerable<string> strings, string prefix, string suffix, string head,
            string tail)
        {
            // Return the flattened string
            return strings
                .Select(s => $"{prefix}{s}{suffix}")
                .Flatten(string.Empty, head, tail);
        }


/*
 * Remove
 * Removes items from a list based on a condition you provide. I have a feeling this should exist already but I can't find it. You can get the same results using 'where' but this method operates on the collection itself.
 * 
 * Author: Chris Meijers
 * Submitted on: 10/25/2010 10:24:07 AM
 * 
 * Example: 
 * var numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

numbers.Remove(x => x > 5);
 */

        public static void Remove<T>(this ICollection<T> list, Func<T, bool> predicate)
        {
            var items = list.Where(predicate).ToList();

            foreach (var item in items)
            {
                list.Remove(item);
            }
        }


/*
 * GetSHA1Hash
 * Calculates the SHA1 hash value of a string and returns it as a base64 string.
 * 
 * Author: Steve Hiner
 * Submitted on: 1/6/2009 7:06:47 PM
 * 
 * Example: 
 * "Get the hash of this string".GetSHA1Hash()
 */

        /// <summary>
        /// Calculates the SHA1 hash of the supplied string and returns a base 64 string.
        /// </summary>
        /// <param name="stringToHash">String that must be hashed.</param>
        /// <returns>The hashed string or null if hashing failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToHash or key is null or empty.</exception>
        public static string GetSHA1Hash(this string stringToHash)
        {
            if (string.IsNullOrEmpty(stringToHash))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            var data = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            var hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            return System.Convert.ToBase64String(hash);
        }


/*
 * List To DataTable
 * List To Datatable
 * 
 * Author: Keyur Panchal
 * Submitted on: 8/11/2015 9:17:48 AM
 * 
 * Example: 
 * List<string> myList = New String[]{"a","b","c","d"}.ToList();
DataTable dt = new DataTable();
dt = myList.ToDataTable();
 */

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }


/*
 * ToShamsiDate
 * Convert DateTime to ShamsiDateString
 * 
 * Author: Abbas Noorali
 * Submitted on: 2/10/2011 8:49:17 AM
 * 
 * Example: 
 * string strShamsiDate1 = DateTime.Now.ToShamsiDateYMD();
string strShamsiDate2 = DateTime.Now.ToShamsiDateDMY();
string strShamsiDate3 = DateTime.Now.ToShamsiDateString();
 */

        /// <summary>
        /// Convert DateTime to Shamsi Date (YYYY/MM/DD)
        /// </summary>
        public static string ToShamsiDateYMD(this DateTime date)
        {
            var PC = new System.Globalization.PersianCalendar();
            var intYear = PC.GetYear(date);
            var intMonth = PC.GetMonth(date);
            var intDay = PC.GetDayOfMonth(date);
            return (intYear.ToString() + "/" + intMonth.ToString() + "/" + intDay.ToString());
        }

        /// <summary>
        /// Convert DateTime to Shamsi Date (DD/MM/YYYY)
        /// </summary>
        public static string ToShamsiDateDMY(this DateTime date)
        {
            var PC = new System.Globalization.PersianCalendar();
            var intYear = PC.GetYear(date);
            var intMonth = PC.GetMonth(date);
            var intDay = PC.GetDayOfMonth(date);
            return (intDay.ToString() + "/" + intMonth.ToString() + "/" + intYear.ToString());
        }

        /// <summary>
        /// Convert DateTime to Shamsi String 
        /// </summary>
        public static string ToShamsiDateString(this DateTime date)
        {
            var PC = new System.Globalization.PersianCalendar();
            var intYear = PC.GetYear(date);
            var intMonth = PC.GetMonth(date);
            var intDayOfMonth = PC.GetDayOfMonth(date);
            var enDayOfWeek = PC.GetDayOfWeek(date);
            string strMonthName, strDayName;
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }

            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    strDayName = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    strDayName = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    strDayName = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    strDayName = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    strDayName = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    strDayName = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    strDayName = "چهارشنبه";
                    break;
                default:
                    strDayName = "";
                    break;
            }

            return ($"{strDayName} {intDayOfMonth} {strMonthName} {intYear}");
        }

/*
 * ToStringFormat
 * StringFormat Extension Style
 * 
 * Author: Mehran KeyArash
 * Submitted on: 3/27/2013 9:48:01 PM
 * 
 * Example: 
 * Console.WriteLine("{0} {1}".ToStringFormat("Parameter 1", "Parameter 2"));

Console.WriteLine("\nPress [Enter] key to exit.");
while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
 */

        public static string ToStringFormat(this string stringFormat, params string[] stringParams)
        {
            return string.Format(stringFormat, stringParams);
        }


/*
 * SelectRows
 * Wraps the usage of some DataTable.DefaultView properties to return sorted rows filtered on a custom filterexpression. For documentation on what to put in the whereExpression and the orderByExpression, refer to the MSDN documentation of DataTable.DefaultView.RowFilter and DataTable.DefaultView.Sort.
 * 
 * Author: Dan Pettersson
 * Submitted on: 12/21/2009 6:43:09 PM
 * 
 * Example: 
 * DataTable selection = dt.SelectRows("ID > 10", "Name");
 */

        public static DataTable SelectRows(this DataTable dt, string whereExpression, string orderByExpression)
        {
            dt.DefaultView.RowFilter = whereExpression;
            dt.DefaultView.Sort = orderByExpression;
            return dt.DefaultView.ToTable();
        }


/*
 * ToListAsync
 * Async create of a System.Collections.Generic.List<T> from an System.Collections.Generic.IQueryable<T>.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 5/28/2012 10:49:55 AM
 * 
 * Example: 
 * private async void Foo() {

    using (var db = new Database1Entities()) {

        var qry = await (from emp in db.Employees
                    where emp.Salary > 1
                    select emp).ToListAsync();


        foreach (var item in qry) {
            Debug.WriteLine(item.Name);
        }
                
    }

}
 */

        /// <summary>
        /// Async create of a System.Collections.Generic.List<T> from an 
        /// System.Collections.Generic.IQueryable<T>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="list">The System.Collections.Generic.IEnumerable<T> 
        /// to create a System.Collections.Generic.List<T> from.</param>
        /// <returns> A System.Collections.Generic.List<T> that contains elements 
        /// from the input sequence.</returns>
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
        {
            return Task.Run(() => list.ToList());
        }


/*
 * Generic Enum to List<T> converter
 * http://devlicio.us/blogs/joe_niland/archive/2006/10/10/Generic-Enum-to-List_3C00_T_3E00_-converter.aspx
 * 
 * Author: Joe Niland
 * Submitted on: 10/16/2010 11:56:11 PM
 * 
 * Example: 
 * List<DayOfWeek> weekdays =
    EnumHelper.EnumToList<DayOfWeek>().FindAll(
        delegate (DayOfWeek x)
        {
            return x != DayOfWeek.Sunday && x != DayOfWeek.Saturday;
        });
 */

        public static List<T> EnumToList<T>()
        {
            var enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var enumValArray = Enum.GetValues(enumType);

            var enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T) Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }


/*
 * Clone
 * Allows you to clone an etire generic list of cloneable items.
 * 
 * Author: Jeff Reddy
 * Submitted on: 8/19/2011 3:14:58 PM
 * 
 * Example: 
 * public class Item :ICloneable {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public object Clone() {
            return MemberwiseClone();
        }
    }

static class CloneTest {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T:ICloneable {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static void  RunTest(){
            //Populate our initial list
            var items = new List<Item> {new Item {ID = 1, ItemName = "Item1"}, 
                                        new Item {ID = 2, ItemName = "Item2"},
                                        new Item {ID = 3, ItemName = "Item3"},
                                        new Item {ID = 4, ItemName = "Item4"}};
            //Create a Clone
            var itemsClone = items.Clone();

            //Baseline test, expect Match comparing 2nd item in list
            Console.WriteLine(items[2] == items[2] ? "Match" : "Different");

            //Clone test, expect Different comparing 2nd item of each
            Console.WriteLine(items[2] == itemsClone[2] ? "Match" : "Different");
        }
    }

static void Main() {
            CloneTest.RunTest();
            Console.ReadLine();
            return;
}


RESULTS:
Match
Different
 */

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T) item.Clone()).ToList();
        }


/*
 * GetMD5
 * Gets the MD5 value of a given file.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 10:29:24 AM
 * 
 * Example: 
 * string file = @"C:\Temp\filename.txt";
Console.WriteLine("MD5 Hash is: {0}.", file.GetMD5());
 */

        /// <summary>
        /// Read and get MD5 hash value of any given filename.
        /// </summary>
        /// <param name="filename">full path and filename</param>
        /// <returns>lowercase MD5 hash value</returns>
        public static string GetMD5(this string filename)
        {
            var result = string.Empty;
            string hashData;

            FileStream fileStream;
            var md5Provider = new MD5CryptoServiceProvider();

            try
            {
                fileStream = GetFileStream(filename);
                var arrByteHashValue = md5Provider.ComputeHash(fileStream);
                fileStream.Close();

                hashData = BitConverter.ToString(arrByteHashValue).Replace("-", "");
                result = hashData;
            }
            catch (Exception)
            {
                // Console.WriteLine(ex.Message);
            }

            return (result.ToLower());
        }

        private static FileStream GetFileStream(string pathName)
        {
            return (new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

/*
 * IsNumeric
 * Returns true if the type can be considered numeric
 * 
 * Author: Tim Scott (lunaverse.wordpress.com)
 * Submitted on: 10/18/2010 7:44:34 AM
 * 
 * Example: 
 * if (someType.IsNumeric())
{
    do something that requires type be numeric
}
 */

        public static bool IsNumeric(this Type t)
        {
            var type = t.GetTypeWithoutNullability();
            return
                type == typeof(short) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(ushort) ||
                type == typeof(uint) ||
                type == typeof(ulong) ||
                type == typeof(decimal) ||
                type == typeof(float) ||
                type == typeof(double);
        }

        public static Type GetTypeWithoutNullability(this Type t)
        {
            return t.IsNullable() ? new NullableConverter(t).UnderlyingType : t;
        }


/*
 * ToCsv
 * Returns a string that represent a csv representation of the referenced T in the IEnumerable<T>. You can also generate a columns header (the first row) with the name of the serialized properties. You can specify the name of the properties to include in the csv file. If you don't specify anything it will includes all the public properties.
 * 
 * Author: Lorenzo Melato
 * Submitted on: 3/16/2011 4:45:19 PM
 * 
 * Example: 
 * var list = new List<Employee>();
list.Add(new List(){FirstName = "Jon", LastName = "Doe"});
list.Add(new List(){FirstName = "Scott", LastName = "Gu"});

string csv = list.ToCsv(true);

or

string csv = list.ToCsv(true, new[] {"FirstName", "LastName"});
 */

        public static string ToCsv<T>(this IEnumerable<T> instance, bool includeColumnHeader, string[] properties)
        {
            if (instance == null)
                return null;

            var csv = new StringBuilder();

            if (includeColumnHeader)
            {
                var header = new StringBuilder();
                foreach (var property in properties)
                    header.AppendFormat("{0},", property);

                csv.AppendLine(header.ToString(0, header.Length - 1));
            }

            foreach (var item in instance)
            {
                var row = new StringBuilder();

                foreach (var property in properties)
                    row.AppendFormat("{0},", item.GetPropertyValue<object>(property));

                csv.AppendLine(row.ToString(0, row.Length - 1));
            }

            return csv.ToString();
        }

        public static string ToCsv<T>(this IEnumerable<T> instance, bool includeColumnHeader)
        {
            if (instance == null)
                return null;

            var properties = (from p in typeof(T).GetProperties()
                select p.Name).ToArray();

            return ToCsv(instance, includeColumnHeader, properties);
        }


/*
 * Piero Alvarez Fuentes
 * Converts any type to another.
 * 
 * Author: Piero Alvarez Fuentes
 * Submitted on: 11/15/2012 12:50:38 PM
 * 
 * Example: 
 * string a = "1234";
int b = a.ChangeType<int>(); //Successful conversion to int (b=1234)
string c = b.ChangeType<string>(); //Successful conversion to string (c="1234")
string d = "foo";
int e = d.ChangeType<int>(); //Exception System.InvalidCastException
int f = d.ChangeType(0); //Successful conversion to int (f=0)
 */

        public static T ChangeType<T>(this object source, T returnValueIfException)
        {
            try
            {
                return source.ChangeType<T>();
            }
            catch
            {
                return returnValueIfException;
            }
        }
    
#if NetFX
/*
 * CSharpCompile
 * Compiles a string into an assembly. .NET 4
 * 
 * Author: Dan Gidman
 * Submitted on: 11/17/2011 3:39:13 PM
 * 
 * Example: 
 * var results = @"include System;

public class Example1
{
   public string Prop { get; set; }
}".CSharpCompile();
 */

        public static CompilerResults CSharpCompile(this string code, string dllName = "dynamicCompile",
            params string[] additionalAssemblies)
        {
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() {{"CompilerVersion", "v4.0"}});
            var parameters = new CompilerParameters
            {
                ReferencedAssemblies =
                {
                    "mscorlib.dll",
                    "System.Core.dll",
                },
                OutputAssembly = dllName,
                GenerateExecutable = false,
                GenerateInMemory = true,
            };

            parameters.ReferencedAssemblies.AddRange(additionalAssemblies);

            return csc.CompileAssemblyFromSource(parameters, code);
        }
#endif
        
/*
 * Enum HasDescription
 * Multiple ways to check if an enum has description
 * 
 * Author: Nitin Chaudhari
 * Submitted on: 6/23/2014 7:44:21 AM
 * 
 * Example: 
 * class Program
    {
        static void Main()
        {
            const Colors someColor = Colors.Red;
            string description = someColor.Description();
            if (someColor.HasDescription())
            {
                if (someColor.HasDescription("indicates stop", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Works");
                }
            }
        }
    }
    public enum Colors
    {
        [Description("Indicates Stop")]
        Red,
        [Description("Indicates Nothing")]
        Blue,
        [Description("Indicates Go")]
        Green
    }
 */

        public static string Description(this Enum someEnum)
        {
            var memInfo = someEnum.GetType().GetMember(someEnum.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute) attrs[0]).Description;
            }
            return someEnum.ToString();
        }

        public static bool HasDescription(this Enum someEnum)
        {
            return !string.IsNullOrWhiteSpace(someEnum.Description());
        }

        public static bool HasDescription(this Enum someEnum, string expectedDescription)
        {
            return someEnum.Description().Equals(expectedDescription);
        }

        public static bool HasDescription(this Enum someEnum, string expectedDescription,
            StringComparison comparisionType)
        {
            return someEnum.Description().Equals(expectedDescription, comparisionType);
        }


/*
 * CSVSplit
 * Given a line from a CSV-encoded file, split it into fields.
 * 
 * Author: David Bakin
 * Submitted on: 3/8/2009 5:18:13 AM
 * 
 * Example: 
 * "this,\"that, those\",\"the other\", "embedded\"\"quote\"" =>

    string[] {
        "this",
        "that, those",
        "the other",
        "embedded\"quote" 
    }
 */

        private enum CSVSplitState
        {
            Normal,
            InQuotes,
            InQuotesFoundQuote
        }

        public static IEnumerable<string> CSVSplit(this string s)
        {
            var state = CSVSplitState.Normal;
            var token = new StringBuilder();
            foreach (var c in s)
            {
                switch (state)
                {
                    case CSVSplitState.Normal:
                        if (c == ',')
                        {
                            yield return token.ToString();
                            token = new StringBuilder();
                        }
                        else if (c == '"')
                            state = CSVSplitState.InQuotes;
                        else
                            token.Append(c);
                        break;

                    case CSVSplitState.InQuotes:
                        if (c == '"')
                            state = CSVSplitState.InQuotesFoundQuote;
                        else
                            token.Append(c);
                        break;

                    case CSVSplitState.InQuotesFoundQuote:
                        if (c == '"')
                        {
                            token.Append(c);
                            state = CSVSplitState.InQuotes;
                        }
                        else
                        {
                            state = CSVSplitState.Normal;
                            goto case CSVSplitState.Normal;
                        }
                        break;
                }
            }
            yield return token.ToString();
        }


/*
 * NotEmpty
 * Determines if the object is null or empty. string is evaluated with empty. Collections, Arrays and Dictionaries are evaluated for 0 items (items themselves may be null) All other objects are evaluated as null or not null.
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/13/2010 2:53:00 PM
 * 
 * Example: 
 * bool b = (new string[] {}).NotEmpty();
b = (new string[] {"", ""}).NotEmpty();
b = string.Empty.NotEmpty();
b = "".NotEmpty();
b = "123".NotEmpty();
b = (new List<string>()).NotEmpty();
b = (new Dictionary<string, string>()).NotEmpty();

// etc...
 */

        /// <summary>
        /// Checks an object to see if it is null or empty.
        /// <para>Empty is any collection, array or dictionary with an item count of 0 or a string that is empty.</para>
        /// </summary>
        public static bool NotEmpty(this object obj)
        {
            if (obj != null && obj is ICollection)
                return ((ICollection) obj).Count > 0;
            if (obj != null && obj is IDictionary)
                return ((IDictionary) obj).Keys.Count > 0;
            if (obj != null && obj is Array)
                return ((Array) obj).Length > 0;
            return !(obj == null || string.IsNullOrEmpty(obj.ToString()));
        }


/*
 * Dedup
 * This method will take any DataTable and remove duplicate rows based on any column.
 * 
 * Author: John D. Sanders
 * Submitted on: 8/17/2011 9:57:54 AM
 * 
 * Example: 
 * DataTable tbl1 = tbl.Dedup("ColName");

or

tbl1 = tbl1.Dedup("ColName");
 */

        public static DataTable Dedup(this DataTable tblIn, string KeyColName)
        {
            var tblOut = tblIn.Clone();
            foreach (DataRow row in tblIn.Rows)
            {
                var found = false;
                var caseIDToTest = row[KeyColName].ToString();
                foreach (DataRow row2 in tblOut.Rows)
                {
                    if (row2[KeyColName].ToString() == caseIDToTest)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    tblOut.ImportRow(row);
            }
            return tblOut;
        }


/*
 * Zip
 * Merges three sequences by using the specified predicate function.
 * 
 * Author: Steven Jeuris
 * Submitted on: 11/15/2011 9:21:22 AM
 * 
 * Example: 
 * byte[] reds = new { 0, 1, 2 };
byte[] greens = new { 3, 4, 5 };
byte[] blues = new { 6, 7, 8 };
var mergedColors = reds.Zip( greens, blues, Color.FromRgb );
 */

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            Contract.Requires(first != null && second != null && third != null && resultSelector != null);

            using (var iterator1 = first.GetEnumerator())
            using (var iterator2 = second.GetEnumerator())
            using (var iterator3 = third.GetEnumerator())
            {
                while (iterator1.MoveNext() && iterator2.MoveNext() && iterator3.MoveNext())
                {
                    yield return resultSelector(iterator1.Current, iterator2.Current, iterator3.Current);
                }
            }
        }


/*
 * IsGuid
 * Validate if a String contains a GUID in groups of 8, 4, 4, 4, and 12 digits with hyphens between the groups. The entire GUID can optionally be enclosed in matching braces or parentheses.
 * 
 * Author: Alvaro Torres Tatis
 * Submitted on: 2/29/2012 5:23:37 AM
 * 
 * Example: 
 * string testValue = "{CA761232-ED42-11CE-BACD-00AA0057B223}";
bool isGuid = testValue.IsGuid();
 */

        public static bool IsGuid(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            const string pattern =
                @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$";
            return Regex.IsMatch(value, pattern);
        }


/*
 * Strip
 * Strip a string of the specified substring or set of characters
 * 
 * Author: C.F.Meijers
 * Submitted on: 12/11/2007 2:50:12 PM
 * 
 * Example: 
 * string s= "abcde";
s = s.Strip("cd");   // s becomes "abe"
s = s.Strip("c");    // s becomes "abde"
s = s.Strip('a', 'e'); // s becomes "bcd"
 */

        /// <summary>
        /// Strip a string of the specified character.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="char">character to remove from the string</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip('b');  //s becomes 'acde;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, char character)
        {
            s = s.Replace(character.ToString(), "");

            return s;
        }

        /// <summary>
        /// Strip a string of the specified characters.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="chars">list of characters to remove from the string</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip('a', 'd');  //s becomes 'bce;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, params char[] chars)
        {
            foreach (var c in chars)
            {
                s = s.Replace(c.ToString(), "");
            }

            return s;
        }

        /// <summary>
        /// Strip a string of the specified substring.
        /// </summary>
        /// <param name="s">the string to process</param>
        /// <param name="subString">substring to remove</param>
        /// <example>
        /// string s = "abcde";
        /// 
        /// s = s.Strip("bcd");  //s becomes 'ae;
        /// </example>
        /// <returns></returns>
        public static string Strip(this string s, string subString)
        {
            s = s.Replace(subString, "");

            return s;
        }


/*
 * FirstIndex() and LastIndex()
 * Finds the index of the first of last occurence which matches the predicate.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 6/9/2011 12:58:48 PM
 * 
 * Example: 
 * int[] l = new int[] { 3, 2, 1, 2, 6 };

Console.WriteLine(l.FirstIndex(n => n == 2)); // 1
Console.WriteLine(l.LastIndex(n => n == 2));  // 3
 */

        public static int FirstIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static int LastIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in list.Reverse())
            {
                if (predicate(item))
                {
                    return list.Count() - index - 1;
                }
                index++;
            }
            return -1;
        }


/*
 * IsStrongPassword
 * Validates whether a string is compliant with a strong password policy.
 * 
 * Author: Arjan Keene
 * Submitted on: 2/24/2008 8:40:43 PM
 * 
 * Example: 
 * //returns false
string s = "test1234";
bool b = s.IsStrongPassword();
//returns true
string s = "tesT12#4";
bool b = s.IsStrongPassword();
 */

        public static bool IsStrongPassword(this string s)
        {
            var isStrong = Regex.IsMatch(s, @"[\d]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[a-z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[A-Z]");
            if (isStrong) isStrong = Regex.IsMatch(s, @"[\s~!@#\$%\^&\*\(\)\{\}\|\[\]\\:;'?,.`+=<>\/]");
            if (isStrong) isStrong = s.Length > 7;
            return isStrong;
        }


/*
 * ScaleImage
 * Scales a Image to make it fit inside of a Height/Width
 * 
 * Author: Robert Booth
 * Submitted on: 3/30/2009 10:27:44 PM
 * 
 * Example: 
 * Image test = someImg.ScaleImage(591, 1096);
 */

        public static SD.Image ScaleImage(this SD.Image img, int height, int width)
        {
            if (img == null || height <= 0 || width <= 0)
            {
                return null;
            }
            var newWidth = (img.Width * height) / (img.Height);
            var newHeight = (img.Height * width) / (img.Width);
            var x = 0;
            var y = 0;

            var bmp = new Bitmap(width, height);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = SDD2D.InterpolationMode.HighQualityBilinear;

            // use this when debugging.
            //g.FillRectangle(Brushes.Aqua, 0, 0, bmp.Width - 1, bmp.Height - 1);
            if (newWidth > width)
            {
                // use new height
                x = (bmp.Width - width) / 2;
                y = (bmp.Height - newHeight) / 2;
                g.DrawImage(img, x, y, width, newHeight);
            }
            else
            {
                // use new width
                x = (bmp.Width / 2) - (newWidth / 2);
                y = (bmp.Height / 2) - (height / 2);
                g.DrawImage(img, x, y, newWidth, height);
            }
            // use this when debugging.
            //g.DrawRectangle(new Pen(Color.Red, 1), 0, 0, bmp.Width - 1, bmp.Height - 1);
            return bmp;
        }


/*
 * Evaluate/Calculate
 * This is submitted as two extension methods as they work together. It is based off of an class designed by sfabriz @ http://www.osix.net/modules/article/?id=761 He has another class that does something a little different but I thought this was a wonderful piece of code so encapsulated it here. I only claim authorship of the conversion and not the underlying logic.
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/21/2010 4:12:48 PM
 * 
 * Example: 
 * string e = "10+10*4".Evaluate();
double d = "10+10*4".Calculate();
 */

        /// <summary>
        /// JavaScript style Eval for simple calculations
        /// http://www.osix.net/modules/article/?id=761
        /// This is a safe evaluation.  IE will not allow for injection.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Evaluate(this string e)
        {
            Func<string, bool> VerifyAllowed = e1 =>
            {
                var allowed = "0123456789+-*/()%.,";
                for (var i = 0; i < e1.Length; i++)
                {
                    if (allowed.IndexOf("" + e1[i]) == -1)
                    {
                        return false;
                    }
                }
                return true;
            };

            if (e.Length == 0)
            {
                return string.Empty;
            }
            if (!VerifyAllowed(e))
            {
                return "String contains illegal characters";
            }
            if (e[0] == '-')
            {
                e = "0" + e;
            }
            var res = "";
            try
            {
                res = Calculate(e).ToString();
            }
            catch
            {
                return "The call caused an exception";
            }
            return res;
        }

        /// <summary>
        /// JavaScript Eval Calculations for simple calculations
        /// http://www.osix.net/modules/article/?id=761
        /// This is an unsafe calculation. IE may allow for injection.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static double Calculate(this string e)
        {
            e = e.Replace(".", ",");
            if (e.IndexOf("(") != -1)
            {
                var a = e.LastIndexOf("(");
                var b = e.IndexOf(")", a);
                var middle = Calculate(e.Substring(a + 1, b - a - 1));
                return Calculate(e.Substring(0, a) + middle.ToString() + e.Substring(b + 1));
            }
            double result = 0;
            var plus = e.Split('+');
            if (plus.Length > 1)
            {
                // there were some +
                result = Calculate(plus[0]);
                for (var i = 1; i < plus.Length; i++)
                {
                    result += Calculate(plus[i]);
                }
                return result;
            }
            else
            {
                // no +
                var minus = plus[0].Split('-');
                if (minus.Length > 1)
                {
                    // there were some -
                    result = Calculate(minus[0]);
                    for (var i = 1; i < minus.Length; i++)
                    {
                        result -= Calculate(minus[i]);
                    }
                    return result;
                }
                else
                {
                    // no -
                    var mult = minus[0].Split('*');
                    if (mult.Length > 1)
                    {
                        // there were some *
                        result = Calculate(mult[0]);
                        for (var i = 1; i < mult.Length; i++)
                        {
                            result *= Calculate(mult[i]);
                        }
                        return result;
                    }
                    else
                    {
                        // no *
                        var div = mult[0].Split('/');
                        if (div.Length > 1)
                        {
                            // there were some /
                            result = Calculate(div[0]);
                            for (var i = 1; i < div.Length; i++)
                            {
                                result /= Calculate(div[i]);
                            }
                            return result;
                        }
                        else
                        {
                            // no /
                            var mod = mult[0].Split('%');
                            if (mod.Length > 1)
                            {
                                // there were some %
                                result = Calculate(mod[0]);
                                for (var i = 1; i < mod.Length; i++)
                                {
                                    result %= Calculate(mod[i]);
                                }
                                return result;
                            }
                            else
                            {
                                // no %
                                return double.Parse(e);
                            }
                        }
                    }
                }
            }
        }


/*
 * Clone<T>()
 * Makes a copy from the object.
 * 
 * Author: Carlos Alessandro Ribeiro
 * Submitted on: 7/21/2009 2:01:21 PM
 * 
 * Example: 
 * Customer customerCopy = customer.Clone<Customer>();
 */

        /// <summary>
        /// Makes a copy from the object.
        /// Doesn't copy the reference memory, only data.
        /// </summary>
        /// <typeparam name="T">Type of the return object.</typeparam>
        /// <param name="item">Object to be copied.</param>
        /// <returns>Returns the copied object.</returns>
        public static T Clone<T>(this object item)
        {
            if (item != null)
            {
                var formatter = new BinaryFormatter();
                var stream = new MemoryStream();

                formatter.Serialize(stream, item);
                stream.Seek(0, SeekOrigin.Begin);

                var result = (T) formatter.Deserialize(stream);

                stream.Close();

                return result;
            }
            else
                return default(T);
        }


/*
 * First(), Last(), Any()
 * Helper methods to simplify development. Prevent common LINQ performance mistakes.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 1/24/2014 8:57:53 AM
 * 
 * Example: 
 * var l = new List<int> { 4, 12, 562, 1 };

Console.WriteLine(l.First()); // 4
Console.WriteLine(l.Last());  // 1
Console.WriteLine(l.Any());   // true
 */

        public static T First<T>(this IList<T> list)
        {
            return list[0];
        }

        public static T Last<T>(this IList<T> list)
        {
            return list[list.Count - 1];
        }

        public static bool Any<T>(this ICollection<T> list)
        {
            return list.Count > 0;
        }


/*
 * ToFriendlyDateString
 * The idea behind the ToFriendlyDateString() method is representing dates in a user friendly way. For example, when displaying a news article on a webpage, you might want articles that were published one day ago to have their publish dates represented as "yesterday at 12:30 PM". Or if the article was publish today, show the date as "Today, 3:33 PM".
 * 
 * Author: jaycent
 * Submitted on: 2/12/2008 10:21:15 PM
 * 
 * Example: 
 * using Utils;

......

DateTime dt = new DateTime(2008, 2, 10, 8, 48, 20);
Console.WriteLine(dt.ToFriendlyDateString());
 */

        public static string ToFriendlyDateString(this DateTime Date)
        {
            var FormattedDate = "";
            if (Date.Date == DateTime.Today)
            {
                FormattedDate = "Today";
            }
            else if (Date.Date == DateTime.Today.AddDays(-1))
            {
                FormattedDate = "Yesterday";
            }
            else if (Date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                FormattedDate = Date.ToString("dddd").ToString();
            }
            else
            {
                FormattedDate = Date.ToString("MMMM dd, yyyy");
            }

            //append the time portion to the output
            FormattedDate += " @ " + Date.ToString("t").ToLower();
            return FormattedDate;
        }




/*
 * String format
 * Extention method to string for String.Format
 * 
 * Author: Rino Reji Cheriyan
 * Submitted on: 7/5/2014 7:42:11 PM
 * 
 * Example: 
 * void StringFormatTest()
        {
            var formatedText = "{0}-{1}-{2}".UseFormat(1, 2, 3);
        }
 */

        public static string UseFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }


/*
 * Return<TIn, TOut>
 * A 'fluent' logic extension method that takes a value (can be anything) and a function that returns another value (can be anything) based on its logic. This is useful for both evaluating and optionally returning a value without declaring a temporary variable for the value.
 * 
 * Author: James White
 * Submitted on: 1/5/2011 5:51:36 PM
 * 
 * Example: 
 * return foo.Bar().Return( bar => bar.IsBaz 
   ? (Baz)bar : Baz.Empty );

// Alternative to:

var bar = foo.Bar();
return bar.IsBaz ? (Baz)bar : Baz.Empty;
 */

        /// <summary>
        /// Returns a value based on an provided value and evaluation function
        /// </summary>
        public static TOut Return<TIn, TOut>(
            this TIn value,
            Func<TIn, TOut> evaluateFunc)
        {
            return evaluateFunc(value);
        }


/*
 * IsDateTime
 * Checks whether the type is DateTime.
 * 
 * Author: kevinjong
 * Submitted on: 3/24/2010 8:27:14 AM
 * 
 * Example: 
 * Type type = DateTime.Now.GetType();
bool isString = type.IsDateTime();
 */

        public static bool IsDateTime(this Type type)
        {
            return type.Equals(typeof(DateTime));
        }


/*
 * RemoveDuplicates
 * Removes items from a collection based on the condition you provide. This is useful if a query gives you some duplicates that you can't seem to get rid of. Some Linq2Sql queries are an example of this. Use this method afterward to strip things you know are in the list multiple times
 * 
 * Author: Chris Meijers
 * Submitted on: 10/25/2010 10:16:20 AM
 * 
 * Example: 
 * var employees = (from x in context.Employees
                 join t in context.PhonesNumbers on x.EmpId equals t.EmpId
                 select new { Employee = x, Phone = t });

//we now have multiple employee records if an employee had more than one phone number

employees = employees.RemoveDuplicates(x => x.EmpId);
 */

        public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
        {
            var dict = new Dictionary<int, T>();

            foreach (var item in list)
            {
                if (!dict.ContainsKey(Predicate(item)))
                {
                    dict.Add(Predicate(item), item);
                }
            }

            return dict.Values.AsEnumerable();
        }


/*
 * Call Action / Func
 * Allows user to call an action / func delegate without having to check for null delegate
 * 
 * Author: Juan Lopez
 * Submitted on: 6/27/2013 4:10:44 AM
 * 
 * Example: 
 * Action action = null;
action.Call();

Action<string> action1 = null;
action1.Call("This won't run");

action1 = str => Console.WriteLine(str);
action1.Call("This will run");

// Same behavior with the rest of the Func
 */

        public static void Call<T1, T2, T3>(this Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            if (action != null)
                action(t1, t2, t3);
        }

        public static R Call<R>(this Func<R> func, R r = default(R))
        {
            return (func != null) ? func() : r;
        }

        public static R Call<T, R>(this Func<T, R> func, T t, R r = default(R))
        {
            return (func != null) ? func(t) : r;
        }

        public static R Call<T1, T2, R>(this Func<T1, T2, R> func, T1 t1, T2 t2, R r = default(R))
        {
            return (func != null) ? func(t1, t2) : r;
        }

        public static R Call<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T1 t1, T2 t2, T3 t3, R r = default(R))
        {
            return (func != null) ? func(t1, t2, t3) : r;
        }


/*
 * ToUrlSlug
 * If you get Turkish inputs you can use this method to create url slugs
 * 
 * Author: Serdar Büyüktemiz
 * Submitted on: 1/12/2013 10:50:05 AM
 * 
 * Example: 
 * var name = "Serdar Büyüktemiz çşğüİö";
var urlName = name.ToUrlSlug(); // returns serdar-buyuktemiz-csguio
 */

        public static string ToUrlSlug(this string text)
        {
            return Regex.Replace(
                Regex.Replace(
                    Regex.Replace(
                        text.Trim().ToLower()
                            .Replace("ö", "o")
                            .Replace("ç", "c")
                            .Replace("ş", "s")
                            .Replace("ı", "i")
                            .Replace("ğ", "g")
                            .Replace("ü", "u"),
                        @"\s+", " "), // multiple spaces to one space
                    @"\s", "-"), // spaces to hypens
                @"[^a-z0-9\s-]", ""); // removing invalid chars
        }


/*
 * EndOfTheMonth
 * Returns datetime corresponding to last day of the month
 * 
 * Author: Victor Rodrigues
 * Submitted on: 5/13/2009 6:51:35 PM
 * 
 * Example: 
 * var lastDay = DateTime.Now.EndOfTheMonth();
 */

        public static DateTime EndOfTheMonth(this DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            return endOfTheMonth;
        }



#if NetFX
/*
 * DataGridView columns visibility configuration window
 * This code allows you to change visibility of columns of any DataGridView component at program runtime. It shows simple window filled with list of columns of DataGridView. You can check columns on the list you want to be visible. Use this code with my other DataGridView extension methods http://extensionmethod.net/csharp/datagridview/load-save-configuration.
 * 
 * Author: Marcin Kozub
 * Submitted on: 7/23/2013 11:06:59 PM
 * 
 * Example: 
 * dgvInstance.ShowConfigurationWindow();
 */

        public static void ShowConfigurationWindow(this DataGridView dataGridView)
        {
            using (var frmConfig = new FrmColumnsConfig(dataGridView))
            {
                frmConfig.ShowDialog();
            }
        }

        public class FrmColumnsConfig : Form
        {
            private DataGridView _dataGridView;

            public FrmColumnsConfig(DataGridView dataGridView)
            {
                InitializeComponent();
                _dataGridView = dataGridView;
            }

            protected override void OnLoad(System.EventArgs e)
            {
                base.OnLoad(e);
                for (var i = 0; i < _dataGridView.Columns.Count; i++)
                {
                    lstColumns.Items.Add(_dataGridView.Columns[i].HeaderText, _dataGridView.Columns[i].Visible);
                }
            }

            private void lstColumns_ItemCheck(object sender, ItemCheckEventArgs e)
            {
                _dataGridView.Columns[e.Index].Visible = e.NewValue == CheckState.Checked;
            }

            private System.Windows.Forms.CheckedListBox lstColumns;
            private System.ComponentModel.IContainer components = null;

            private void InitializeComponent()
            {
                this.lstColumns = new System.Windows.Forms.CheckedListBox();
                this.SuspendLayout();
                this.lstColumns.Dock = System.Windows.Forms.DockStyle.Fill;
                this.lstColumns.FormattingEnabled = true;
                this.lstColumns.Location = new SD.Point(0, 0);
                this.lstColumns.Name = "lstColumns";
                this.lstColumns.Size = new SD.Size(258, 214);
                this.lstColumns.TabIndex = 0;
                this.lstColumns.CheckOnClick = true;
                this.lstColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstColumns_ItemCheck);

                this.AutoScaleDimensions = new SD.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new SD.Size(258, 214);
                this.Controls.Add(this.lstColumns);
                this.Name = "FrmColumnsConfig";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                this.Text = "Columns configuration";
                this.ResumeLayout(false);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
        }
#endif

/*
 * Enum Name To Display Name
 * Convert an CamelCase enum name to displayable string
 * 
 * Author: AdamTha
 * Submitted on: 2/3/2015 10:35:08 AM
 * 
 * Example: 
 * foreach(HttpStatusCode c in Enum.GetValues(typeof(HttpStatusCode)))
{
    Console.WriteLine("Name:{0} - {1}", c, c.AsUpperCamelCaseName());
}
 */

        private static Regex UpperCamelCaseRegex =
            new Regex(@"(?<!^)((?<!\d)\d|(?=(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", RegexOptions.Compiled);

        public static string AsUpperCamelCaseName(this Enum e)
        {
            return UpperCamelCaseRegex.Replace(e.ToString(), " $1");
        }


/*
 * ToUIString
 * Converts a decimal to a string using the current UI culture
 * 
 * Author: Guy Van den Nieuwenhof
 * Submitted on: 7/24/2012 11:44:01 PM
 * 
 * Example: 
 * var amount = 10.25m;
Amount.Text = amount.ToUIString();
 */
        public static string ToUIString(this decimal value)
        {
            return value.ToString(CultureInfo.CurrentUICulture);
        }

/*
 * SetAllValues
 * Sets all values.
 * 
 * Author: Nathan Brown
 * Submitted on: 10/15/2010 2:48:33 AM
 * 
 * Example: 
 * double[] allOnes = new double[10].SetAllValues(1);
 */

        /// <summary>
        /// Sets all values.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array that will be modified.</typeparam>
        /// <param name="array">The one-dimensional, zero-based array</param>
        /// <param name="value">The value.</param>
        /// <returns>A reference to the changed array.</returns>
        public static T[] SetAllValues<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }

            return array;
        }


/*
 * ToRFC822DateString
 * Converts a regular DateTime to a RFC822 date string used for RSS feeds
 * 
 * Author: unknown
 * Submitted on: 3/7/2008 3:29:48 PM
 * 
 * Example: 
 * var s = DateTime.Now.ToRFC822DateString();
 */

        /// <summary>
        /// Converts a regular DateTime to a RFC822 date string.
        /// </summary>
        /// <returns>The specified date formatted as a RFC822 date string.</returns>
        public static string ToRFC822DateString(this DateTime date)
        {
#if NetFX
            var offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
#else
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours;
#endif
            var timeZone = "+" + offset.ToString().PadLeft(2, '0');
            if (offset < 0)
            {
                var i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }
            return date.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'),
                System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        }


/*
 * ToHtmlTable
 * Converts an IEnumberable<T> to an HTML DataTable.
 * 
 * Author: Alex Friedman
 * Submitted on: 6/5/2009 6:20:18 AM
 * 
 * Example: 
 * public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
        private void button1_Click(object sender, EventArgs e)
        {
            var personList = new List<Person>();
            personList.Add(new Person
            {
                FirstName = "Alex",
                LastName = "Friedman",
                Age = 27
            });
            personList.Add(new Person
            {
                FirstName = "Jack",
                LastName = "Bauer",
                Age = 45

            });

            personList.Add(new Person
            {
                FirstName = "Cloe",
                LastName = "O'Brien",
                Age = 35
            });
            personList.Add(new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            });



            string html = @"<style type = ""text/css""> .tableStyle{border: solid 5 green;} 
th.header{ background-color:#FF3300} tr.rowStyle { background-color:#33FFFF; 
border: solid 1 black; } tr.alternate { background-color:#99FF66; 
border: solid 1 black;}</style>";
            html += personList.ToHtmlTable("tableStyle", "header", "rowStyle", "alternate");
            this.webBrowser1.DocumentText = html;
        }
 */

        public static string ToHtmlTable<T>(this IEnumerable<T> list, string tableSyle, string headerStyle,
            string rowStyle, string alternateRowStyle)
        {
            var result = new StringBuilder();
            if (string.IsNullOrEmpty(tableSyle))
            {
                result.Append("<table id=\"" + typeof(T).Name + "Table\">");
            }
            else
            {
                result.Append("<table id=\"" + typeof(T).Name + "Table\" class=\"" + tableSyle + "\">");
            }

            var propertyArray = typeof(T).GetProperties();
            foreach (var prop in propertyArray)
            {
                if (string.IsNullOrEmpty(headerStyle))
                {
                    result.AppendFormat("<th>{0}</th>", prop.Name);
                }
                else
                {
                    result.AppendFormat("<th class=\"{0}\">{1}</th>", headerStyle, prop.Name);
                }
            }

            for (var i = 0; i < list.Count(); i++)
            {
                if (!string.IsNullOrEmpty(rowStyle) && !string.IsNullOrEmpty(alternateRowStyle))
                {
                    result.AppendFormat("<tr class=\"{0}\">", i % 2 == 0 ? rowStyle : alternateRowStyle);
                }
                else
                {
                    result.AppendFormat("<tr>");
                }

                foreach (var prop in propertyArray)
                {
                    var value = prop.GetValue(list.ElementAt(i), null);
                    result.AppendFormat("<td>{0}</td>", value ?? string.Empty);
                }
                result.AppendLine("</tr>");
            }
            result.Append("</table>");
            return result.ToString();
        }


/*
 * EqualsByContent
 * Checks if two DataTable objects have the same content.
 * 
 * Author: Jonas Butt
 * Submitted on: 8/24/2008 1:26:10 AM
 * 
 * Example: 
 * // Create two DataTable objects.
var dataTable1 = new DataTable();
var dataTable2 = new DataTable();

// Create two columns in each DataTable.
dataTable1.Columns.Add("Column1");
dataTable2.Columns.Add("Column1");
dataTable1.Columns.Add("Column2");
dataTable2.Columns.Add("Column2");

// Add one row to each DataTable.
dataTable1.Rows.Add(new object[] { "Hello World", DateTime.Today });
dataTable2.Rows.Add(new object[] { "Hello World", DateTime.Today.AddYears(1) });

// Write results. 
// Expected result is false.
Console.WriteLine(dataTable1.EqualsByContent(dataTable2));
 */

        public static bool EqualsByContent(this DataTable thisDataTable, DataTable otherDataTable)
        {
            // Compare row count.
            if (thisDataTable.Rows.Count != otherDataTable.Rows.Count)
            {
                return false;
            }

            // Compare column count.
            if (thisDataTable.Columns.Count != otherDataTable.Columns.Count)
            {
                return false;
            }

            // Compare data in each cell of each row.
            for (var i = 0; i < thisDataTable.Rows.Count; i++)
            {
                for (var j = 0; j < thisDataTable.Columns.Count; j++)
                {
                    if (!thisDataTable.Rows[i][j].Equals(otherDataTable.Rows[i][j]))
                    {
                        return false;
                    }
                }
            }

            // The two DataTables contain the same data.
            return true;
        }


/*
 * DeleteFiles
 * Deletes the files in a certain directory that comply to the searchpattern. The searchpattern can contain * and ? (the normal wildcard characters). The function can also search in the subdirectories.
 * 
 * Author: Gaston Verelst
 * Submitted on: 10/30/2009 11:35:42 AM
 * 
 * Example: 
 * DirectoryInfo di = new DirectoryInfo(@"c:\temp");
di.DeleteFiles("*.xml");  // Delete all *.xml files 
di.DeleteFiles("*.xml", true);  // Delete all, recursively
 */

        /// <summary>
        /// Delete files in a folder that are like the searchPattern, don't include subfolders.
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <returns>Number of files that have been deleted.</returns>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern)
        {
            return DeleteFiles(di, searchPattern, false);
        }

        /// <summary>
        /// Delete files in a folder that are like the searchPattern
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <param name="includeSubdirs"></param>
        /// <returns>Number of files that have been deleted.</returns>
        /// <remarks>
        /// This function relies on DirectoryInfo.GetFiles() which will first get all the FileInfo objects in memory. This is good for folders with not too many files, otherwise
        /// an implementation using Windows APIs can be more appropriate. I didn't need this functionality here but if you need it just let me know.
        /// </remarks>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern, bool includeSubdirs)
        {
            var deleted = 0;
            foreach (var fi in di.GetFiles(searchPattern,
                includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }


/*
 * ToColor
 * Convert a (A)RGB string to a Silverlight Color object
 * 
 * Author: Fons Sonnemans
 * Submitted on: 11/23/2008 7:38:59 PM
 * 
 * Example: 
 * Color c = "ff00bb".ToColor();
LayoutRoot.Background = new SolidColorBrush(c);
 */


        /// <summary>
        /// Convert a (A)RGB string to a Color object
        /// </summary>
        /// <param name="argb">An RGB or an ARGB string</param>
        /// <returns>a Color object</returns>
        public static Color ToColor(this string argb)
        {
            argb = argb.Replace("#", "");
            var a = System.Convert.ToByte("ff", 16);
            byte pos = 0;
            if (argb.Length == 8)
            {
                a = System.Convert.ToByte(argb.Substring(pos, 2), 16);
                pos = 2;
            }
            var r = System.Convert.ToByte(argb.Substring(pos, 2), 16);
            pos += 2;
            var g = System.Convert.ToByte(argb.Substring(pos, 2), 16);
            pos += 2;
            var b = System.Convert.ToByte(argb.Substring(pos, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

/*
 * ColumnExists
 * Returns true if the column exists in the DataReader, else returns false
 * 
 * Author: Jonnidip
 * Submitted on: 12/9/2010 11:28:31 AM
 * 
 * Example: 
 * Boolean b = datareader.ColumnExists("ColumnNameToCheck");
 */

        /// <summary>
        /// Checks if a column exists in the DataReader
        /// </summary>
        /// <param name="dr">DataReader</param>
        /// <param name="ColumnName">Name of the column to find</param>
        /// <returns>Returns true if the column exists in the DataReader, else returns false</returns>
        public static bool ColumnExists(this IDataReader dr, string ColumnName)
        {
            for (var i = 0; i < dr.FieldCount; i++)
                if (dr.GetName(i).Equals(ColumnName, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }


/*
 * FormatSize
 * Nicely formatted file size. This method will return file size with bytes, KB, MB and GB in it. You can use this alongside the Extension method named FileSize.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 9:48:05 AM
 * 
 * Example: 
 * // Using another Extension Method: FileSize to get the size of the file
string path = @"D:\WWW\Proj\web.config";
Console.WriteLine("File Size is: {0}.", path.FileSize().FormatSize());
 */

        public static string FormatFileSize(this long fileSize)
        {
            string[] suffix = {"bytes", "KB", "MB", "GB"};
            long j = 0;

            while (fileSize > 1024 && j < 4)
            {
                fileSize = fileSize / 1024;
                j++;
            }
            return (fileSize + " " + suffix[j]);
        }


/*
 * ToString
 * Returns a formatted string on a nullable double
 * 
 * Author: C.F. Meijers
 * Submitted on: 12/11/2007 3:29:07 PM
 * 
 * Example: 
 * double? pi = null;

string s = pi.ToString("0.00"); //s = ""
pi = Math.Pi;

s = pi.ToString("0.00");  //s = 3.14
 */

        /// <summary>
        /// Returns a formatted double or emtpy string
        /// </summary>
        /// <param name="t">double or null</param>
        /// <param name="format">double formatstring </param>
        /// <returns></returns>
        public static string ToString(this double? t, string format)
        {
            if (t != null)
            {
                return t.Value.ToString(format);
            }

            return "";
        }

/*
 * AddWorkDays (fixed version)
 * Fixed version of AddWorkDays
 * 
 * Author: Kevin Cook
 * Submitted on: 5/20/2014 10:26:17 PM
 * 
 * Example: 
 * DateTime due = DateTime.Today.AddWorkdays(10); // due in 10 workdays

//Fixed version
 */

        public static bool IsWeekday(this DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    return false;

                default:
                    return true;
            }
        }

        public static bool IsWeekend(this DayOfWeek dow)
        {
            return !dow.IsWeekday();
        }

        public static DateTime AddWorkdays(this DateTime startDate, int days)
        {
            // start from a weekday        
            while (startDate.DayOfWeek.IsWeekend())
                startDate = startDate.AddDays(1.0);

            for (var i = 0; i < days; ++i)
            {
                startDate = startDate.AddDays(1.0);

                while (startDate.DayOfWeek.IsWeekend())
                    startDate = startDate.AddDays(1.0);
            }
            return startDate;
        }

/*
 * GetParentDirectoryPath
 * On the layers of the directory path of a directory
 * 
 * Author: 亂馬客
 * Submitted on: 7/12/2012 5:05:58 AM
 * 
 * Example: 
 * MessageBox.Show(Application.ExecutablePath.GetDirectoryPath().GetParentDirectoryPath());
            MessageBox.Show(Application.ExecutablePath.GetDirectoryPath().GetParentDirectoryPath(2));
 */


        /// <summary>
        /// 取得某目錄的上幾層的目錄路徑
        /// </summary>
        /// <param name="folderPath">目錄路徑</param>
        /// <param name="levels">要往上幾層</param>
        /// <returns></returns>
        public static string GetParentDirectoryPath(this string folderPath, int levels)
        {
            var result = folderPath;
            for (var i = 0; i < levels; i++)
            {
                if (Directory.GetParent(result) != null)
                {
                    result = Directory.GetParent(result).FullName;
                }
                else
                {
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 取得某目錄的上層的目錄路徑
        /// </summary>
        /// <param name="folderPath">目錄路徑</param>
        /// <returns></returns>
        public static string GetParentDirectoryPath(this string folderPath)
        {
            return GetParentDirectoryPath(folderPath, 1);
        }

        /// <summary>
        /// 取得路徑的目錄路徑
        /// </summary>
        /// <param name="filePath">路徑</param>
        /// <returns></returns>
        public static string GetDirectoryPath(this string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

#if NetFX
/*
 * InvokeAction
 * A set of Dispatcher extenstions that make it easy to cleanly queue lambdas on the Dispatcher.
 * 
 * Author: Will Sullivan
 * Submitted on: 10/14/2010 7:32:16 PM
 * 
 * Example: 
 * // old way
dispatcher.Invoke((Action<string>)((x) => { Console.Write(x); }), "annoying");
// this way
dispatcher.InvokeAction(x=>Console.Write(X), "yay lol");
 */

        /// <summary>
        /// Invokes the specified <paramref name="action"/> on the given <paramref name="dispatcher"/>.
        /// </summary>
        /// <param name="dispatcher">The dispatcher on which the <paramref name="action"/> executes.</param>
        /// <param name="action">The <see cref="Action"/> to execute.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/>.  Defaults to <see cref="DispatcherPriority.ApplicationIdle"/></param>
        public static void InvokeAction(this Dispatcher dispatcher, Action action, DispatcherPriority priority)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            if (action == null)
                throw new ArgumentNullException("action");
            dispatcher.Invoke(action, priority);
        }

        /// <summary>
        /// Invokes the specified <paramref name="action"/> on the given <paramref name="dispatcher"/>.
        /// </summary>
        /// <typeparam name="T">The type of the argument of the <paramref name="action"/>.</typeparam>
        /// <param name="dispatcher">The dispatcher on which the <paramref name="action"/> executes.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute.</param>
        /// <param name="arg">The first argument of the action.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/>.  Defaults to <see cref="DispatcherPriority.ApplicationIdle"/></param>
        public static void InvokeAction<T>(this Dispatcher dispatcher, Action<T> action, T arg,
            DispatcherPriority priority = DispatcherPriority.ApplicationIdle)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            if (action == null)
                throw new ArgumentNullException("action");
            dispatcher.Invoke(action, priority, arg);
        }

        /// <summary>
        /// Invokes the specified <paramref name="action"/> on the given <paramref name="dispatcher"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument of the <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second argument of the <paramref name="action"/>.</typeparam>
        /// <param name="dispatcher">The dispatcher on which the <paramref name="action"/> executes.</param>
        /// <param name="action">The <see cref="Action{T1,T2}"/> to execute.</param>
        /// <param name="arg1">The first argument of the action.</param>
        /// <param name="arg2">The second argument of the action.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/>.  Defaults to <see cref="DispatcherPriority.ApplicationIdle"/></param>
        public static void InvokeAction<T1, T2>(this Dispatcher dispatcher, Action<T1, T2> action, T1 arg1, T2 arg2,
            DispatcherPriority priority = DispatcherPriority.ApplicationIdle)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            if (action == null)
                throw new ArgumentNullException("action");
            dispatcher.Invoke(action, priority, arg1, arg2);
        }

        /// <summary>
        /// Invokes the specified <paramref name="action"/> on the given <paramref name="dispatcher"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the first argument of the <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second argument of the <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third argument of the <paramref name="action"/>.</typeparam>
        /// <param name="dispatcher">The dispatcher on which the <paramref name="action"/> executes.</param>
        /// <param name="action">The <see cref="Action{T1,T2,T3}"/> to execute.</param>
        /// <param name="arg1">The first argument of the action.</param>
        /// <param name="arg2">The second argument of the action.</param>
        /// <param name="arg3">The third argument of the action.</param>
        /// <param name="priority">The <see cref="DispatcherPriority"/>.  Defaults to <see cref="DispatcherPriority.ApplicationIdle"/></param>
        public static void InvokeAction<T1, T2, T3>(this Dispatcher dispatcher, Action<T1, T2, T3> action, T1 arg1,
            T2 arg2, T3 arg3, DispatcherPriority priority = DispatcherPriority.ApplicationIdle)
        {
            if (dispatcher == null)
                throw new ArgumentNullException("dispatcher");
            if (action == null)
                throw new ArgumentNullException("action");
            dispatcher.Invoke(action, priority, arg1, arg2, arg3);
        }
#endif
        
/*
 * None(), OneOf(), Many(), XOf()
 * Count-based extensions which make checking the length of something more readable. * Updated on 2010-01-16 following suggestion from @Sane regarding the use of Count() in None(). Switched to Any(). Thanks!
 * 
 * Author: Dan Atkinson
 * Submitted on: 1/9/2010 2:26:45 PM
 * 
 * Example: 
 * List<string> myList = new List<string>() { "foo", "bar", "fong", "foo" };

//returns false
myList.None();

//returns false
myList.None(x=> x == "bar");

//returns true
myList.None(x=> x == "bang");

//returns true
myList.Many();

//returns true
myList.Many(x=> x == "foo");

//returns false
myList.Many(x=> x == "bar");

//returns false
myList.OneOf();

//returns false
myList.OneOf(x=> x == "foo");

//returns true
myList.OneOf(x=> x == "bar");

//returns true
myList.Xof(4);

//returns true
myList.XOf(x=> x == "foo", 2);

//returns false
myList.XOf(x=> x == "foo", 3);
 */

        public static bool None<T>(this IEnumerable<T> source)
        {
            return source.Any() == false;
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Any(query) == false;
        }

        public static bool Many<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }

        public static bool Many<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }

        public static bool OneOf<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool OneOf<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }

        public static bool XOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }

        public static bool XOf<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }


/*
 * To<> Convert
 * To<> Convert
 * 
 * Author: Majid Safari
 * Submitted on: 8/14/2015 3:39:42 PM
 * 
 * Example: 
 * var intAge = age.To<int>();

var para = age.To<DateTime>();
 */

        /// <summary>
        /// converts one type to another
        /// Example:
        /// var age = "28";
        /// var intAge = age.To<int>();
        /// var doubleAge = intAge.To<double>();
        /// var decimalAge = doubleAge.To<decimal>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this IConvertible value)
        {
            try
            {
                var t = typeof(T);
                var u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (value == null || value.Equals(""))
                        return default(T);

                    return (T) System.Convert.ChangeType(value, u);
                }
                else
                {
                    if (value == null || value.Equals(""))
                        return default(T);

                    return (T) System.Convert.ChangeType(value, t);
                }
            }

            catch
            {
                return default(T);
            }
        }

        public static T To<T>(this IConvertible value, IConvertible ifError)
        {
            try
            {
                var t = typeof(T);
                var u = Nullable.GetUnderlyingType(t);

                if (u != null)
                {
                    if (value == null || value.Equals(""))
                        return (T) ifError;

                    return (T) System.Convert.ChangeType(value, u);
                }
                else
                {
                    if (value == null || value.Equals(""))
                        return (T) (ifError.To<T>());

                    return (T) System.Convert.ChangeType(value, t);
                }
            }
            catch
            {
                return (T) ifError;
            }
        }

/*
 * ComputeHash
 * Computes the hash of a string using one of the following algorithms: HMAC, HMACMD5, HMACSHA1, HMACSHA256, HMACSHA384, HMACSHA512,MACTripleDES, MD5, RIPEMD160, SHA1, SHA256, SHA384, SHA512.
 * 
 * Author: Cosmin Pirlitu
 * Submitted on: 5/26/2012 3:31:23 PM
 * 
 * Example: 
 * string s = "Hello world!";
string hash = s.ComputeHash(Hasher.eHashType.RIPEMD160);

MessageBox.Show(hash);
// 7f772647d88750add82d8e1a7a3e5c0902a346a3
 */

        /// <summary>
        /// Supported hash algorithms
        /// </summary>
        public enum eHashType
        {
            HMAC,
            HMACMD5,
            HMACSHA1,
            HMACSHA256,
            HMACSHA384,
            HMACSHA512,
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512,
#if NetFX
            MACTripleDES,
            RIPEMD160,
#endif
        }

        private static byte[] GetHash(string input, eHashType hash)
        {
            var inputBytes = Encoding.ASCII.GetBytes(input);

            switch (hash)
            {
                case eHashType.HMAC:
                    return HMAC.Create().ComputeHash(inputBytes);

                case eHashType.HMACMD5:
                    return HMACMD5.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA1:
                    return HMACSHA1.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA256:
                    return HMACSHA256.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA384:
                    return HMACSHA384.Create().ComputeHash(inputBytes);

                case eHashType.HMACSHA512:
                    return HMACSHA512.Create().ComputeHash(inputBytes);

                case eHashType.MD5:
                    return MD5.Create().ComputeHash(inputBytes);

                case eHashType.SHA1:
                    return SHA1.Create().ComputeHash(inputBytes);

                case eHashType.SHA256:
                    return SHA256.Create().ComputeHash(inputBytes);

                case eHashType.SHA384:
                    return SHA384.Create().ComputeHash(inputBytes);

                case eHashType.SHA512:
                    return SHA512.Create().ComputeHash(inputBytes);

#if NetFX
                case eHashType.MACTripleDES:
                    return MACTripleDES.Create().ComputeHash(inputBytes);

                case eHashType.RIPEMD160:
                    return RIPEMD160.Create().ComputeHash(inputBytes);
#endif
                
                default:
                    return inputBytes;
            }
        }

        /// <summary>
        /// Computes the hash of the string using a specified hash algorithm
        /// </summary>
        /// <param name="input">The string to hash</param>
        /// <param name="hashType">The hash algorithm to use</param>
        /// <returns>The resulting hash or an empty string on error</returns>
        public static string ComputeHash(this string input, eHashType hashType)
        {
            try
            {
                var hash = GetHash(input, hashType);
                var ret = new StringBuilder();

                for (var i = 0; i < hash.Length; i++)
                    ret.Append(hash[i].ToString("x2"));

                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
#if NetFX
/*
 * AddToEnd
 * Adds an item to a listbox as the last item, and makes sure it is visible.
 * 
 * Author: David Bakin
 * Submitted on: 3/8/2009 5:28:04 AM
 * 
 * Example: 
 * ListBox1.AddToEnd( "foo" );
ListBox1.AddToEnd( "bar" );
 */

        public static void AddToEnd(this System.Windows.Forms.ListBox lb, object o)
        {
            lb.Items.Add(o);
            lb.TopIndex = lb.Items.Count - 1;
            lb.ClearSelected();
        }

/*
 * Add
 * Add an string array to ListControl ( dropdown, listbox, radiobuttonlist, checkbox).
 * 
 * Author: Matt G
 * Submitted on: 7/30/2009 5:14:50 PM
 * 
 * Example: 
 * DropDownList ddl = new DropDownList();
ddl.Items.Add(new string[] { "Apple", "orange", "Pair" });
 */

        public static void Add(this ListItemCollection col, string[] array)
        {
            foreach (var s in array)
            {
                col.Add(s);
            }
        }
#endif

/*
 * IncrementAt<T>
 * Increment counter at the key passed as argument. Dictionary is <TKey, Int>
 * 
 * Author: Krzysztof Morcinek
 * Submitted on: 10/28/2012 11:01:32 PM
 * 
 * Example: 
 * var animalQuantities = new Dictionary<string, int>();
animalQuantities.IncrementAt("cat");
animalQuantities.IncrementAt("cat");

Console.WriteLine(animalQuantities["cat"]); // 2
 */

        public static void IncrementAt<T>(this Dictionary<T, int> dictionary, T index)
        {
            var count = 0;

            dictionary.TryGetValue(index, out count);

            dictionary[index] = ++count;
        }


/*
 * Resize
 * takes a byte[], and ints for width/height. returns a byte[] for the new image. keeps a static copy of previously provided sizes to reduce GC activity.
 * 
 * Author: esp
 * Submitted on: 4/17/2011 8:27:14 AM
 * 
 * Example: 
 * public byte[] ImageResult(byte[] imageData, int width, int height) {
         return imageData.Resize(width, height);
      }
 */

        private static readonly IList<Tuple<Rectangle, SD.Image>> BitmapSizes =
            new List<Tuple<Rectangle, SD.Image>>();

        //adapted from http://www.webcosmoforums.com/asp/321-create-high-quality-thumbnail-resize-image-dynamically-asp-net-c-code.html
        //TODO: figure out how expensive this method is
        //TODO: see if there's a way to make the IDisposable objects static
        //TODO: make sure caching is set up properly to reduce usage
        public static byte[] Resize(this byte[] source, int width, int height)
        {
            using (var image = SD.Image.FromStream(new MemoryStream(source), true, true))
            {
                var srcWidth = image.PhysicalDimension.Width;
                var srcHeight = image.PhysicalDimension.Height;
                if (srcWidth == width && srcHeight == height)
                    return source;
                var scaleW = (double) width / srcWidth;
                var scaleH = (double) height / srcHeight;
                if (scaleW < scaleH)
                {
                    width = (int) Math.Round((scaleW * srcWidth));
                    height = (int) Math.Round((scaleW * srcHeight));
                }
                else
                {
                    width = (int) Math.Round((scaleH * srcWidth));
                    height = (int) Math.Round((scaleH * srcHeight));
                }
                var bmpTouple = BitmapSizes.FirstOrDefault(t => t.Item1.Height == height && t.Item1.Width == width);
                if (bmpTouple == null)
                {
                    bmpTouple = new Tuple<Rectangle, SD.Image>(
                        new Rectangle(0, 0, width, height),
                        new Bitmap(width, height));
                    BitmapSizes.Add(bmpTouple);
                }
                var rect = bmpTouple.Item1;
                var bmp = bmpTouple.Item2;
                using (var gr = Graphics.FromImage(bmp))
                {
                    gr.SmoothingMode = SDD2D.SmoothingMode.HighQuality;
                    gr.CompositingQuality = SDD2D.CompositingQuality.HighQuality;
                    gr.InterpolationMode = SDD2D.InterpolationMode.High;
                    gr.DrawImage(image, rect);
                    using (var outStream = new MemoryStream())
                    {
                        bmp.Save(outStream, ImageFormat.Jpeg);
                        return outStream.ToArray();
                    }
                }
            }
        }


/*
 * IndexOf
 * Gets the index of the give value in a collection. Is overloaded to take a start parameter as well.
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/3/2010 8:41:36 PM
 * 
 * Example: 
 * var i = new Collection<int>{0,1,2,3,4,5,6,7};

int first = i.IndexOf(0);
int second = i.IndexOf(0,1);
 */

        public static int IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return obj.IndexOf(value, 0);
        }

        public static int IndexOf<T>(this IEnumerable<T> obj, T value, int start)
        {
            if (start >= obj.Count()) return -1;
            if (start < 0) throw new IndexOutOfRangeException("start must be a non-negative integer");
            for (var i = start; i < obj.Count(); ++i)
            {
                if (value.Equals(obj.ElementAt(i))) return i;
            }
            return -1;
        }


/*
 * WordCount
 * Count all words in a given string. Excludes whitespaces, tabs and line breaks.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 4/21/2008 5:23:02 AM
 * 
 * Example: 
 * string word = "the quick brown\r\nfox    jumps over the lazy \tdog.";
Console.WriteLine("Total Words: {0}", word.WordCount());
// returns 9
 */

        /// <summary>
        /// Count all words in a given string
        /// </summary>
        /// <param name="input">string to begin with</param>
        /// <returns>int</returns>
        public static int WordCount(this string input)
        {
            var count = 0;
            try
            {
                // Exclude whitespaces, Tabs and line breaks
                var re = new Regex(@"[^\s]+");
                var matches = re.Matches(input);
                count = matches.Count;
            }
            catch
            {
            }
            return count;
        }
        
#if NetFX
/*
 * Generates a Hyper Link to redirect user to Authentication form
 * this method generates a Hyper Link to redirect user to Authentication form . gets Titla attribute of tag and inner Text of Tag and generate tag A . then returns user to referrer page .
 * 
 * Author: http://mb-seifollahi.ir
 * Submitted on: 4/6/2013 10:19:50 AM
 * 
 * Example: 
 * string Tag_A = Page.SPUtilGenerateAuthenticationHyperLink("To Login Click Here", " Here ");
 lit_Msg.Text = "You are not logged in . please log in at first & click " + Tag_A + " to show Authentication Form";
 */

        public static string SPUtilGenerateAuthenticationHyperLink(this Page p, string title, string InnerText)
        {
            try
            {
                var url = p.Request.Url.Scheme + "://" + p.Request.Url.Authority +
                             "/_layouts/Authenticate.aspx?Source=" + p.Request.Url.LocalPath;
                var Tag_A = "<a href=\"" + url + "\" title=\"" + title + "\">" + InnerText + "</a>";
                return Tag_A;
            }
            catch
            {
                var Tag_A = "<a href=\"#ERROR\" title=\"" + title + "\">" + InnerText + "</a>";

                return Tag_A;
            }
        }
#endif

/*
 * ToUrlString
 * takes a string, replacing special characters and spaces with - (one dash per one or many contiguous special charachters or spaces). makes lower-case and trims. good for seo.
 * 
 * Author: esp
 * Submitted on: 4/17/2011 8:45:00 AM
 * 
 * Example: 
 * MyWebItem.Name.ToUrlString()
 */

        public static string ToUrlString(this string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            // Unicode Character Handling: http://blogs.msdn.com/b/michkap/archive/2007/05/14/2629747.aspx
            var stFormD = str.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var t in
                from t in stFormD
                let uc = CharUnicodeInfo.GetUnicodeCategory(t)
                where uc != UnicodeCategory.NonSpacingMark
                select t)
            {
                sb.Append(t);
            }
            return Regex.Replace(sb.ToString().Normalize(NormalizationForm.FormC), "[\\W\\s]{1,}", "-").Trim('-');
        }


/*
 * SplitIntoParts
 * Splits long string into smaller parts with given length.
 * 
 * Author: Marcin Kozub
 * Submitted on: 3/1/2014 10:57:15 PM
 * 
 * Example: 
 * string longString = "This is a very long string, which we want to split on smaller parts every max. 30 characters long."; // Length: 98

var partLength = 30;
var parts = longString.SplitIntoParts(partLength);
Console.WriteLine("String: " + longString);
Console.WriteLine("Total length: " + longString.Length);
Console.WriteLine("Part length: " + partLength);
Console.WriteLine("Total parts: " + parts.Count);
Console.WriteLine("Parts:");
foreach (var part in parts)
{
    Console.WriteLine("{0}: {1}", part.Length.ToString("D3"), part);
}
Console.ReadLine();


// OUTPUT:
// String: This is a very long string, which we want to split on smaller parts every max. 30 characters long.
// Total length: 98
// Part length: 30
// Total parts: 4
// Parts:
// 030: This is a very long string, wh
// 030: ich we want to split on smalle
// 030: r parts every max. 30 characte
// 008: rs long.
 */

        public static List<string> SplitIntoParts(this string input, int partLength)
        {
            var result = new List<string>();
            var partIndex = 0;
            var length = input.Length;
            while (length > 0)
            {
                var tempPartLength = length >= partLength ? partLength : length;
                var part = input.Substring(partIndex * partLength, tempPartLength);
                result.Add(part);
                partIndex++;
                length -= partLength;
            }
            return result;
        }


/*
 * IsNullOrDBNull
 * We all know that objects can be null, but when dealing with databases, a new null type shows up, the DBNull. This extention method detects it along with the null.
 * 
 * Author: Hadi Lababidi
 * Submitted on: 2/19/2010 1:13:52 PM
 * 
 * Example: 
 * Object obj = null;
bool b = obj.IsNullOrDBNull();
 */

        public static bool IsNullOrDBNull(this object obj)
        {
            if (obj == null || obj.GetType() == typeof(DBNull))
                return true;
            else
                return false;
        }


/*
 * Convert
 * Converts from one type to another.
 * 
 * Author: Adam Weigert
 * Submitted on: 3/3/2008 9:12:12 PM
 * 
 * Example: 
 * int[] integers = new int[] { 5, 25, 50, 100 };
string[] strings = integers.Convert(i => i.ToString());
 */

        public static IEnumerable<TDestination> ConvertType<TSource, TDestination>(this IEnumerable<TSource> enumerable,
            Func<TSource, TDestination> converter)
        {
            if (enumerable == null)
            {
                return null;
            }

            var items = new List<TDestination>();

            foreach (var item in enumerable)
            {
                items.Add(converter(item));
            }

            return items.ToArray();
        }


/*
 * ToDelimitedString<T>(char delimiter, Func<T, PropertyInfo, string> func)
 * Map any object T to a delimited string and control how that string is formatted.
 * 
 * Author: James Levingston
 * Submitted on: 10/19/2010 3:38:43 AM
 * 
 * Example: 
 * --> Expand with more extensions

  public static string ToCommaDelimitedString(this object obj)
        {
            return obj.ToDelimitedString<object>(',',
                (object o, System.Reflection.PropertyInfo p) => { return (string.Format("{0}.{1}={2}", p.ReflectedType.Name, p.Name, Convert.ToString(p.GetValue(o, null)))); });
        }

        public static string ToPipeDelimitedString(this object obj)
        {
            return obj.ToDelimitedString<object>('|',
                (object o, System.Reflection.PropertyInfo p) => { return (string.Format("{0}.{1}={2}", p.ReflectedType.Name, p.Name, Convert.ToString(p.GetValue(o, null)))); });
        }

--> Above examples in use is below.

var commaLine = obj.ToCommaDelimitedString();
var pipeLine = obj.ToPipeDelimitedString();
 */

        public static string ToDelimitedString<T>(this T obj, char delimiter,
            Func<T, System.Reflection.PropertyInfo, string> func)
        {
            if (obj == null || func == null)
                return null;

            var builder = new StringBuilder();
            var props = obj.GetType().GetProperties();
            for (var p = 0; p < props.Length; p++)
                builder.Append(func(obj, props[p]) + delimiter.ToString());

            //Remove the last character, the last delimiter
            if (builder.Length > 0)
                return builder.ToString().Remove(builder.ToString().Length - 1);

            return null;
        }


/*
 * ToNullableString()
 * Calling Value.ToString on a System.Nullable<T> type where the value is null will result in an "Nullable object must have a value." exception being thrown. This extension method can be used in place of .ToString to prevent this exception from occurring.
 * 
 * Author: Chris Rock
 * Submitted on: 3/31/2008 6:27:52 PM
 * 
 * Example: 
 * System.Nullable<byte> value = GetValueFromDatabase();
Textbox1.Text = value.ToNullableString();

' GetValueFromDatabase is simulating retrieving a System.Nullable<byte> value from the database.
 */

        public static string ToNullableString<T>(this System.Nullable<T> param) where T : struct
        {
            if (param.HasValue)
            {
                return param.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}