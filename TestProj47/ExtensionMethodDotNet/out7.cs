using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if NetFX
using System.Data.Services.Client;
using System.Web.UI.WebControls;
using System.Windows.Forms;
#endif

// ReSharper disable PossibleNullReferenceException
#pragma warning disable 1574

namespace HSNXT
{
    public static partial class Extensions
    {
/*
 * TimeSpanToString
 * Converts a timespan to a string displaying hours and minutes
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 8:37:59 PM
 * 
 * Example: 
 * TimeSpan ts = new TimeSpan(1, 6, 4, 34);

string display = ts.TimeSpanToString(); // 30 hours 4 mins
 */

        /// <summary>
        /// Converts the seconds to an hour \ min display string.
        /// </summary>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>
        /// A string in the format x hours y mins.
        /// </returns>
        public static string TimeSpanToString(this TimeSpan timeSpan)
        {
            var s = TimeSpan.FromSeconds(timeSpan.TotalSeconds);

            return $"{(int) s.TotalHours} hours {s.Minutes} mins";
        }


/*
 * Load & Save configuration
 * Two methods that extends DataGridView control to save and load columns configuration to specified XML file. More informations (in Polish, example in English) at: http://kozub.net.pl/2012/02/22/datagridview-konfiguracja-kolumn-oraz-zapis-i-odczyt-stanu/ http://kozub.net.pl/2012/03/21/c-extension-methods/
 * 
 * Author: Marcin Kozub
 * Submitted on: 3/22/2012 1:40:08 PM
 * 
 * Example: 
 * dgvInstance.SaveConfiguration(@"C:\config.xml");
dgvInstance.LoadConfiguration(@"C:\config.xml");
 */

        [Serializable]
        public sealed class ColumnInfo
        {
            public string Name { get; set; }
            public int DisplayIndex { get; set; }
            public int Width { get; set; }
            public bool Visible { get; set; }
        }

#if NetFX
        /// <summary>
        /// Loads columns information from the specified XML file
        /// </summary>
        /// <param name="dgv">DataGridView control instance</param>
        /// <param name="fileName">XML configuration file</param>
        public static void LoadConfiguration(this DataGridView dgv, string fileName)
        {
            List<ColumnInfo> columns;
            using (var streamReader = new StreamReader(fileName))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
                columns = (List<ColumnInfo>) xmlSerializer.Deserialize(streamReader);
            }
            foreach (var column in columns)
            {
                dgv.Columns[column.Name].DisplayIndex = column.DisplayIndex;
                dgv.Columns[column.Name].Width = column.Width;
                dgv.Columns[column.Name].Visible = column.Visible;
            }
        }
    
        /// <summary>
        /// Saves columns information to the specified XML file
        /// </summary>
        /// <param name="dgv">DataGridView control instance</param>
        /// <param name="fileName">XML configuration file</param>
        public static void SaveConfiguration(this DataGridView dgv, string fileName)
        {
            var columns = new List<ColumnInfo>();
            for (var i = 0; i < dgv.Columns.Count; i++)
            {
                var column = new ColumnInfo
                {
                    Name = dgv.Columns[i].Name,
                    DisplayIndex = dgv.Columns[i].DisplayIndex,
                    Width = dgv.Columns[i].Width,
                    Visible = dgv.Columns[i].Visible
                };
                columns.Add(column);
            }
            using (var streamWriter = new StreamWriter(fileName))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<ColumnInfo>));
                xmlSerializer.Serialize(streamWriter, columns);
            }
        }
#endif

/*
 * StringToTimeSpan
 * Converts a string to a timespan
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 8:46:05 PM
 * 
 * Example: 
 * string s = "22:03:34";

// Returns a TimeSpan object with 22 Hours, 3 Minutes and 34 Seconds
TimeSpan ts = s.StringToTimeSpan();
 */

        /// <summary>
        /// Converts a string time to a timespan.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns>
        /// A timespan object.
        /// </returns>
        public static TimeSpan StringToTimeSpan(this string time)
        {
            var result = TimeSpan.TryParse(time, out var timespan);
            return result ? timespan : new TimeSpan(0, 0, 0);
        }


/*
 * EnqueueRange
 * Enqueues a generic collection of items
 * 
 * Author: David Seff
 * Submitted on: 7/19/2016 7:16:18 PM
 * 
 * Example: 
 * var queue = new Queue<string>();
var list = new List<string>() { "aaa", "bbb" };
queue.EnqueueRange(list);
 */

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                queue.Enqueue(item);
        }


/*
 * ToObservableCollection
 * Return observable collection for IList object.
 * 
 * Author: Muhammad Shoaib Ijaz
 * Submitted on: 9/16/2016 3:21:10 PM
 * 
 * Example: 
 * var cityList = new List<string> { "NewYork", "London" };
var cityListObsCollection = cityList.ToObservableCollection();
 */

        /// <summary>
        /// To the observable collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>ObservableCollection&lt;T&gt;.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> value) where T : class
        {
            if (value == null)
            {
                return null;
            }

            var observableCollection = new ObservableCollection<T>(value);
            return observableCollection;
        }


/*
 * DateTimeFloor;DateTimeCeiling
 * Floor, Ceiling, Midpoint and Rounding calculations for various time intervals.
 * 
 * Author: Jeff Banjavcic
 * Submitted on: 3/9/2017 2:37:45 AM
 * 
 * Example: 
 * DateTime currentHour = DateTime.Now.DateTimeFloor(DateExtensions.TimeInterval.OneHour); // Returns the current date and time with the minutes and seconds set to zero.
DateTime nextYear = DateTime.Now.DateTimeCeiling(DateExtensions.TimeInterval.YearFromJanuary); // Returns January 1 of next year at midnight.
 */

        public static DateTime DateTimeFloor(this DateTime dt, TimeInterval interval)
        {
            return WorkMethod(dt, 0L, interval);
        }

        public static DateTime DateTimeMidpoint(this DateTime dt, TimeInterval interval)
        {
            return WorkMethod(dt, 2L, interval);
        }

        public static DateTime DateTimeCeiling(this DateTime dt, TimeInterval interval)
        {
            return WorkMethod(dt, 1L, interval);
        }

        public static DateTime DateTimeCeilingUnbounded(this DateTime dt, TimeInterval interval)
        {
            return WorkMethod(dt, 1L, interval).AddTicks(-1);
        }

        public static DateTime DateTimeRound(this DateTime dt, TimeInterval interval)
        {
            return WorkMethod(dt, dt >= WorkMethod(dt, 2L, interval) ? 1L : 0L, interval);
        }

        public enum TimeInterval : long
        {
            YearFromJanuary = 120L,
            YearFromFebruary = 121L,
            YearFromMarch = 122L,
            YearFromApril = 123L,
            YearFromMay = 124L,
            YearFromJune = 125L,
            YearFromJuly = 126L,
            YearFromAugust = 127L,
            YearFromSeptember = 128L,
            YearFromOctober = 129L,
            YearFromNovember = 130L,
            YearFromDecember = 131L,
            HalfYearFromJanuary = 60L,
            HalfYearFromFebruary = 61L,
            HalfYearFromMarch = 62L,
            HalfYearFromApril = 63L,
            HalfYearFromMay = 64L,
            HalfYearFromJune = 65L,
            QuarterYearFromJanuary = 30L,
            QuarterYearFromFebruary = 31L,
            QuarterYearFromMarch = 32L,
            BiMonthlyFromJanuary = 20L,
            BiMonthlyFromFebruary = 21L,
            OneMonth = 10L,
            OneWeekFromSunday = 1L,
            OneWeekFromMonday = 2L,
            OneWeekFromTuesday = 3L,
            OneWeekFromWednesday = 4L,
            OneWeekFromThursday = 5L,
            OneWeekFromFriday = 6L,
            OneWeekFromSaturday = 7L,
            OneDay = TimeSpan.TicksPerDay,
            TwelveHour = TimeSpan.TicksPerHour * 12L,
            SixHour = TimeSpan.TicksPerHour * 6L,
            ThreeHour = TimeSpan.TicksPerHour * 3L,
            OneHour = TimeSpan.TicksPerHour,
            HalfHour = TimeSpan.TicksPerMinute * 30L,
            QuarterHour = TimeSpan.TicksPerMinute * 15L,
            OneMinute = TimeSpan.TicksPerMinute,
            HalfMinute = TimeSpan.TicksPerSecond * 30L,
            QuarterMinute = TimeSpan.TicksPerSecond * 15L,
            OneSecond = TimeSpan.TicksPerSecond,
            TenthOfASecond = TimeSpan.TicksPerSecond / 10L,
            HundrethOfASecond = TimeSpan.TicksPerSecond / 100L,
            ThousandthOfASecond = TimeSpan.TicksPerSecond / 1000L
        }

        private static DateTime WorkMethod(DateTime dt, long returnType, TimeInterval interval)
        {
            var interval1 = (long) interval;
            var ticksFromFloor = 0L;
            int intervalFloor;
            int floorOffset;
            DateTime floorDate;

            if (interval1 > 132L) //Set variables to calculate date for time interval less than one day.
            {
                floorDate = new DateTime(dt.Ticks - (dt.Ticks % interval1), dt.Kind);
                if (returnType != 0L)
                    ticksFromFloor = interval1 / returnType;
            }
            else if (interval1 < 8L) //Set variables to calculate date for time interval of one week.
            {
                intervalFloor = (int) (interval1) - 1;
                floorOffset = (int) dt.DayOfWeek * -1;
                floorDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, dt.Kind).AddDays(
                    -(intervalFloor > floorOffset ? floorOffset + 7 - intervalFloor : floorOffset - intervalFloor));
                if (returnType != 0L)
                    ticksFromFloor = TimeSpan.TicksPerDay * 7L / returnType;
            }
            else //Set variables to calculate date for time interval one month or greater.
            {
                var intervalLength = interval1 >= 130L ? 12 : (int) (interval1 / 10L);
                intervalFloor = (int) (interval1 % intervalLength);
                floorOffset = (dt.Month - 1) % intervalLength;
                floorDate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0, dt.Kind).AddMonths(-(intervalFloor > floorOffset
                    ? floorOffset + intervalLength - intervalFloor
                    : floorOffset - intervalFloor));
                if (returnType != 0L)
                {
                    var ceilingDate = floorDate.AddMonths(intervalLength);
                    ticksFromFloor = ceilingDate.Subtract(floorDate).Ticks / returnType;
                }
            }
            return floorDate.AddTicks(ticksFromFloor);
        }

/*
 * ReverseWords
 * Reverse Words
 * 
 * Author: Ali HajiAbadi
 * Submitted on: 1/22/2017 8:55:15 AM
 * 
 * Example: 
 * var sentence = "the quick brown fox jumps over the lazy dog";
sentence.ReverseWords();
 */

        public static string ReverseWords(this string sentence)
        {
            var words = sentence.Split(' ');
            Array.Reverse(words);
            //string reversed = words.Aggregate((workingSentence, next) => next + " " + workingSentence);

            return string.Join(" ", words);
        }


/*
 * AsSequenceTo
 * Creates a numeric list of integers starting at the current instance and ending at the maximum value.
 * 
 * Author: Duane Wingett
 * Submitted on: 2/25/2016 11:12:35 AM
 * 
 * Example: 
 * [TestMethod]
        public void AsSequenceTo_EndsAtMximumValue()
        {
            // ARRANGE
            const int initialValue = 3;
            const int maximumValue = 5;
            const int expectedEnd = maximumValue;

            // ACT
            var result = initialValue.AsSequenceTo(maximumValue);
            var actual = result.Last();

            // ASSERT
            Assert.AreEqual(expectedEnd, actual);
        }
 */


        /// <summary>
        /// Creates a numeric list of integers starting at the current instance and ending 
        /// at the maximum value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="maxValue">The initial value.</param>
        /// <returns></returns>
        public static List<int> AsSequenceTo(this int instance, int maxValue)
        {
            // Validate arguments
            if (maxValue < instance)
                throw new ArgumentOutOfRangeException(nameof(maxValue), maxValue,
                    "maxValue must not be less than the instance. ");

            var count = (maxValue - instance + 1);
            return Enumerable.Range(instance, count).ToList();
        }


/*
 * IntegerToTimeSpan
 * Converts an integer to a timespan
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 8:49:50 PM
 * 
 * Example: 
 * int seconds = 78654;

// Returns a TimeSpan object with 21 Hours, 50 Minutes and 54 Seconds
TimeSpan ts = seconds.IntegerToTimeSpan();
 */

        /// <summary>
        /// Converts the seconds to a timespan object.
        /// </summary>
        /// <param name="totalSeconds">The total seconds.</param>
        /// <returns>A timespan object.</returns>
        public static TimeSpan IntegerToTimeSpan(this int totalSeconds)
        {
            return new TimeSpan(0, 0, totalSeconds);
        }


/*
 * ConcatItem / ConcatTo
 * Concats a single item to an IEnumerable
 * 
 * Author: Bart Kemps
 * Submitted on: 12/6/2016 4:52:38 PM
 * 
 * Example: 
 * var x = new[]{3,4,5}.ConcatTo(1,2); // yields 1,2,3,4,5

var x = new[]{3,4,5}.Concat(1); // yields 3,4,5,1
 */

        /// <summary>
        /// Adds the specified element at the end of the IEnummerable.
        /// </summary>
        /// <typeparam name="T">The type of elements the IEnumerable contans.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="item">The item to be concatenated.</param>
        /// <returns>An IEnumerable, enumerating first the items in the existing enumerable</returns>
        public static IEnumerable<T> ConcatItem<T>(this IEnumerable<T> target, T item)
        {
            foreach (var t in target) yield return t;
            yield return item;
        }

// or:

        /// <summary>
        /// Adds the specified element at the end of the IEnummerable.
        /// </summary>
        /// <typeparam name="T">The type of elements the IEnumerable contans.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="items">The items to be concatenated.</param>
        /// <returns>An IEnumerable, enumerating first the items in the existing enumerable</returns>
        public static IEnumerable<T> ConcatItems<T>(this IEnumerable<T> target, params T[] items)
        {
            return target.Concat(items);
        }

        /// <summary>
        /// Inserts the specified element at the start of the IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements the IEnumerable contans.</typeparam>
        /// <param name="target">The IEnummerable.</param>
        /// <param name="items">The items to be concatenated.</param>
        /// <returns>An IEnumerable, enumerating first the target elements, and then the new elements.</returns>
        public static IEnumerable<T> ConcatTo<T>(this IEnumerable<T> target, params T[] items)
        {
            return items.Concat(target);
        }


/*
 * DisplayDouble
 * Converts a Double to a String with precision
 * 
 * Author: K M Thomas
 * Submitted on: 3/25/2016 8:13:24 PM
 * 
 * Example: 
 * double number = 2.2365;

string display = number.DisplayDouble(2); // 2.24
 */

        public static string DisplayDouble(this double value, int precision)
        {
            return value.ToString("N" + precision);
        }


/*
 * ToJson
 * Json Conversion, uses DataContractJsonSerializer to deserialize item
 * 
 * Author: Arek Bal
 * Submitted on: 10/30/2012 10:21:39 AM
 * 
 * Example: 
 * [Authorize]
public ActionResult Process(MyLovelyModel model)
{
  processor.Process(model);
  Json(model.JsonDeserialize());
}
 */

        public static string ToJson<T>(this T item, Encoding encoding = null,
            DataContractJsonSerializer serializer = null)
        {
            encoding = encoding ?? Encoding.Default;
            serializer = serializer ?? new DataContractJsonSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, item);
                var json = encoding.GetString((stream.ToArray()));

                return json;
            }
        }


/*
 * FromIso8601WeekNumber / ToIso8601WeekNumber
 * Converts to and from ISO 8601 Week numbers
 * 
 * Author: Bart Kemps
 * Submitted on: 12/6/2016 4:56:48 PM
 * 
 * Example: 
 * var week = new DateTime(2017,1,1).ToIso8601WeekNumber(); // yields 52

var date = FromIso8601Weeknumber(52, 2016, DayOfWeek.Sunday); // yields 1 jan 2017

This is correct: week 52 of 2016 stretches into 2017
 */

        /// <summary>
        /// Converts a date to a week number.
        /// ISO 8601 week 1 is the week that contains the first Thursday that year.
        /// </summary>
        public static int ToIso8601Weeknumber(this DateTime date)
        {
            var thursday = date.AddDays(3 - date.DayOfWeek.DayOffset());
            return (thursday.DayOfYear - 1) / 7 + 1;
        }

        /// <summary>
        /// Converts a week number to a date.
        /// Note: Week 1 of a year may start in the previous year.
        /// ISO 8601 week 1 is the week that contains the first Thursday that year, so
        /// if December 28 is a Monday, December 31 is a Thursday,
        /// and week 1 starts January 4.
        /// If December 28 is a later day in the week, week 1 starts earlier.
        /// If December 28 is a Sunday, it is in the same week as Thursday January 1.
        /// </summary>
        public static DateTime FromIso8601Weeknumber(int weekNumber, int? year = null, DayOfWeek day = DayOfWeek.Monday)
        {
            var dec28 = new DateTime((year ?? DateTime.Today.Year) - 1, 12, 28);
            var monday = dec28.AddDays(7 * weekNumber - dec28.DayOfWeek.DayOffset());
            return monday.AddDays(day.DayOffset());
        }

        /// <summary>
        /// Iso8601 weeks start on Monday. This returns 0 for Monday.
        /// </summary>
        private static int DayOffset(this DayOfWeek weekDay)
        {
            return ((int) weekDay + 6) % 7;
        }


/*
 * IsNotIn
 * Determines if an instance is not contained in a sequence. Is the equivalent of Contains == false, but allows a more fluent reading "if item is not in list", specially useful in LINQ extension methods like Where.
 * 
 * Author: Joan Comas
 * Submitted on: 4/20/2016 5:09:28 PM
 * 
 * Example: 
 * var exceptionList = new List<string> { "exception1", "exception2" };
var query = myEntities.MyEntity
                     .Select(e => e.Name)
                     .Where(e => e.IsNotIn(exceptionList));
 */

        public static bool IsNotIn<T>(this T keyObject, IEnumerable<T> collection)
        {
            return keyObject.IsIn(collection) == false;
        }

/*
 * AwaitableTaskEnumerableExtensions
 * Awaitable fluent extensions for enumerables of task
 * 
 * Author: Aleix Domènech
 * Submitted on: 4/6/2017 8:35:27 PM
 * 
 * Example: 
 * private static void Main(string[] args)
        {
            FooBarAsync().Wait();
        }

        private static IEnumerable<int> foos = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public static async Task FooBarAsync()
        {
            var watch = new Stopwatch();
            watch.Start();

            await foos.Select(foo => BarAsync(foo)).WhenAll();
            var results = await foos.Select(foo => BarAsync(foo)).WhenAll();

            watch.Stop();
            Console.WriteLine("WhenAll -->:" + watch.Elapsed);
            watch.Reset();

            watch.Start();

            await foos.Select(foo => BarAsync(foo)).WhenAny();
            var result = await foos.Select(foo => BarAsync(foo)).WhenAny();

            watch.Stop();
            Console.WriteLine("WhenAny -->:" + watch.Elapsed);
            
            Console.ReadKey();
        }

        public static async Task<int> BarAsync(int something)
        {
            await Task.Delay(new Random((int)DateTime.Now.Ticks).Next(100, 1500));

            return something;
        }
 */

        /// <summary>
        ///     Linq extension to be able to fluently wait for all of <see cref="IEnumerable{T}" /> of <see cref="Task" /> just
        ///     like <see cref="Task.WhenAll(System.Threading.Tasks.Task[])" />.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>An awaitable task</returns>
        /// <remarks></remarks>
        /// <example>
        ///     var something = await foos.Select(foo => BarAsync(foo)).WhenAll();
        /// </example>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static Task WhenAll(this IEnumerable<Task> tasks)
        {
            var enumeratedTasks = tasks as Task[] ?? tasks?.ToArray();

            return Task.WhenAll(enumeratedTasks);
        }

        /// <summary>
        ///     Linq extension to be able to fluently wait for any of <see cref="IEnumerable{T}" /> of <see cref="Task" /> just
        ///     like <see cref="Task.WhenAll(System.Threading.Tasks.Task[])" />.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>An awaitable task</returns>
        /// <remarks></remarks>
        /// <example>
        ///     var something = await foos.Select(foo => BarAsync(foo)).WhenAll();
        /// </example>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static Task WhenAny(this IEnumerable<Task> tasks)
        {
            var enumeratedTasks = tasks as Task[] ?? tasks.ToArray();

            return Task.WhenAny(enumeratedTasks);
        }

        /// <summary>
        ///     Linq extension to be able to fluently wait for all of <see cref="IEnumerable{T}" /> of <see cref="Task" /> just
        ///     like <see cref="Task.WhenAll(System.Threading.Tasks.Task{TResult}[])" />.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>An awaitable task</returns>
        /// <remarks></remarks>
        /// <example>
        ///     var bars = await foos.Select(foo => BarAsync(foo)).WhenAll();
        /// </example>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static async Task<IEnumerable<TResult>> WhenAll<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            var enumeratedTasks = tasks as Task<TResult>[] ?? tasks.ToArray();

            var result = await Task.WhenAll(enumeratedTasks);
            return result;
        }

        /// <summary>
        ///     Linq extension to be able to fluently wait for all of <see cref="IEnumerable{T}" /> of <see cref="Task" /> just
        ///     like <see cref="Task.WhenAny(System.Threading.Tasks.Task{TResult}[])" />.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        /// <returns>An awaitable task</returns>
        /// <remarks></remarks>
        /// <example>
        ///     var bar = await foos.Select(foo => BarAsync(foo)).WhenAll();
        /// </example>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static async Task<TResult> WhenAny<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            var enumeratedTasks = tasks as Task<TResult>[] ?? tasks.ToArray();

            var result = await await Task.WhenAny(enumeratedTasks);
            return result;
        }


/*
 * ToList(capacity)
 * LINQ ToList() extension method with an extra capacity argument. This can boost the speed of creating the list.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 4/3/2017 9:29:54 AM
 * 
 * Example: 
 * IEnumerable<string> data = GetData();
var l = data.ToList(100000);

Console.WriteLine(l.Count);
Console.WriteLine(l.Capacity);
 */

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source, int capacity)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Non-negative number required.");
            }
            var list = new List<TSource>(capacity);
            foreach (var item in source)
            {
                list.Add(item);
            }
            return list;
        }


/*
 * CurrentDateTimeInAmsterdam
 * Get the current date time in Amsterdam
 * 
 * Author: Marc van Steijn
 * Submitted on: 11/22/2016 2:19:31 PM
 * 
 * Example: 
 * label1.Text = new DateTime().TimeInAmsterdam().ToString();
 */

        public static DateTime CurrentDateTimeInAmsterdam(this DateTime date)
        {
            var localZone =
                TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZone);

            return localTime;
        }


/*
 * WeekOfYearISO8601
 * Gets the number of the week according to the definition of the ISO 8601
 * 
 * Author: João Mata
 * Submitted on: 12/1/2016 8:32:55 PM
 * 
 * Example: 
 * int weekNumber = DateTime.Now.WeekOfYearISO8601();
 */

        public static int WeekOfYearIso8601(this DateTime date)
        {
            var day = (int) CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
            var week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)),
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            return week;
        }

/*
 * NextAnniversary
 * Calculates the next anniversary of an event after the initial date on the Gregorian calendar. Use the original event date or the event month/event day as a parameters. The optional parameter, preserveMonth will determine how to handle an event date of 2/29. Set to true will use February 28 for a standard year anniversary and set to false will use March 1 for a standard year anniversary.
 * 
 * Author: Jeff Banjavcic
 * Submitted on: 3/15/2017 4:03:50 AM
 * 
 * Example: 
 * DateTime hireDate = new DateTime(1998, 10, 5);
DateTime nextHireAnnivers = DateTime.Now.NextAnniversary(hireDate); // Returns the next occurance of October 5.
DateTime nextAnnivers = DateTime.Now.NextAnniversary(12, 16); // Returns the next occurance of December 16.
DateTime leapDayEvent = DateTime.Now.NextAnniversary(2, 29, true); // Returns the next occurance of February 28 or February 29 depending on if the next anniversary is in a leap year.
 */

        public static DateTime NextAnniversary(this DateTime dt, DateTime eventDate, bool preserveMonth = false)
        {
            if (dt.Date < eventDate.Date) // Return the original event date if it occurs later than initial input date.
                return new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, 0, 0, 0, dt.Kind);

            var calcDate = new DateTime(
                dt.Year + (dt.Month < eventDate.Month || dt.Month == eventDate.Month && dt.Day < eventDate.Day ? 0 : 1),
                eventDate.Month, 1, 0, 0, 0, dt.Kind).AddDays(eventDate.Day - 1);

            if (eventDate.Month == calcDate.Month || !preserveMonth)
                return calcDate;
            else
                return calcDate.AddYears(dt.Month == 2 && dt.Day == 28 ? 1 : 0).AddDays(-1);
        }

        public static DateTime NextAnniversary(this DateTime dt, int eventMonth, int eventDay,
            bool preserveMonth = false)
        {
            if (eventDay > 31 || eventDay < 1 || eventMonth > 12 || eventMonth < 1 ||
                ((eventMonth == 4 || eventMonth == 6 || eventMonth == 9 || eventMonth == 11) && eventDay > 30) ||
                (eventMonth == 2 && eventDay > 29))
                throw new Exception("Invalid combination of Event Year and Event Month.");

            var calcDate = new DateTime(
                dt.Year + (dt.Month < eventMonth || dt.Month == eventMonth && dt.Day < eventDay ? 0 : 1), eventMonth, 1,
                0, 0, 0, dt.Kind).AddDays(eventDay - 1);

            if (eventMonth == calcDate.Month || !preserveMonth)
                return calcDate;
            else
                return calcDate.AddYears(dt.Month == 2 && dt.Day == 28 ? 1 : 0).AddDays(-1);
        }

#if NetFX
/*
 * SelectionValue
 * Returns Dropdownlist Selected Value as Integer
 * 
 * Author: Andi Haviari
 * Submitted on: 4/29/2016 11:40:32 AM
 * 
 * Example: 
 * var selectedValue = ddl.SelectedValue();
 */

        public static int SelectionValue(this DropDownList ddl)
        {
            int.TryParse(ddl.SelectedValue, out var r);
            return r;
        }
#endif

/*
 * ValidateAndConvertDictionaryData
 * Dictionary Extension
 * 
 * Author: Cem Basaranoglu
 * Submitted on: 12/22/2016 8:48:03 AM
 * 
 * Example: 
 * Dictionary<string, object> dictionary = new Dictionary<string, object> {{"FOO", 1}};
  string foo = dictionary.ValidateAndConvertDictionaryData<string>("FOO");
 */

        public static T ValidateAndConvertDictionaryData<T>(this Dictionary<string, object> dictionary,
            string dictionaryKey)
        {
            if (dictionary != null && dictionary.ContainsKey(dictionaryKey))
            {
                dictionary.TryGetValue(dictionaryKey, out var dictionaryValue);

                if (dictionaryValue != null)
                {
                    return dictionaryValue.TryToCast<T>();
                }
            }

            return default;
        }

        public static T TryToCast<T>(this object value)
        {
            if (value is T variable)
            {
                return variable;
            }
            try
            {
                if (typeof(T).IsEnum)
                {
                    return (T) Enum.Parse(typeof(T), value.ToString());
                }

                return (T) Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }


/*
 * XML TO Class
 * Parse XML String to Class
 * 
 * Author: Keyur Panchal
 * Submitted on: 11/3/2016 10:24:10 AM
 * 
 * Example: 
 * None
 */

        public static T ParseToClass<T>(this string xml) where T : class
        {
            try
            {
                if (!string.IsNullOrEmpty(xml) && !string.IsNullOrWhiteSpace(xml))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    using (var stringReader = new StringReader(xml))
                    {
                        return (T) xmlSerializer.Deserialize(stringReader);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }


/*
 * FindMin() and FindMax()
 * Selects the object in a list with the minimum or maximum value on a particular property
 * 
 * Author: Fons Sonnemans
 * Submitted on: 10/7/2016 11:00:52 AM
 * 
 * Example: 
 * var l = new List<Employee>();
var r = new Random();
for (int i = 0; i < 1000000; i++) {
    l.Add(new Employee { Name = i.ToString(), Salary = r.Next(10000) });
}

Console.WriteLine(l.FindMin(emp => emp.Salary)?.Name);
Console.WriteLine(l.FindMax(emp => emp.Salary)?.Name);
 */

        public static T FindMin<T, TValue>(this IEnumerable<T> list, Func<T, TValue> predicate)
            where TValue : IComparable<TValue>
        {
            var en = list as T[] ?? list.ToArray();
            var result = en.FirstOrDefault();
            if (result != null)
            {
                var bestMin = predicate(result);
                foreach (var item in en.Skip(1))
                {
                    var v = predicate(item);
                    if (v.CompareTo(bestMin) < 0)
                    {
                        bestMin = v;
                        result = item;
                    }
                }
            }
            return result;
        }

        public static T FindMax<T, TValue>(this IEnumerable<T> list, Func<T, TValue> predicate)
            where TValue : IComparable<TValue>
        {
            var en = list as T[] ?? list.ToArray();
            var result = en.FirstOrDefault();
            if (result != null)
            {
                var bestMax = predicate(result);
                foreach (var item in en.Skip(1))
                {
                    var v = predicate(item);
                    if (v.CompareTo(bestMax) > 0)
                    {
                        bestMax = v;
                        result = item;
                    }
                }
            }
            return result;
        }


/*
 * IEnumerable(string).Join
 * Joins a series of strings connected by a separator.
 * 
 * Author: Entroper
 * Submitted on: 8/2/2016 12:39:00 PM
 * 
 * Example: 
 * var strings = new List<string> { "Ace", "Bil", "Cal", "Dan" };

var everyone = strings.Join(", "); // returns "Ace, Bil, Cal, Dan"
 */

        public static string Join(this IEnumerable<string> source, string separator) => string.Join(separator, source);


/*
 * Convert a Rectangular to a Jagged Array
 * Converts a T[,] (rectangular array) to a T[][] (jagged array).
 * 
 * Author: Siddhartha Gandhi
 * Submitted on: 12/1/2016 5:51:19 PM
 * 
 * Example: 
 * string[,] rectangular_array = new string[3,3];
string[][] jagged_array = rectangular_array.ToJaggedArray();
 */

        public static T[][] ToJaggedArray<T>(this T[,] multiArray)
        {
            var firstElement = multiArray.GetLength(0);
            var secondElement = multiArray.GetLength(1);

            var jaggedArray = new T[firstElement][];

            for (var c = 0; c < firstElement; c++)
            {
                jaggedArray[c] = new T[secondElement];
                for (var r = 0; r < secondElement; r++)
                {
                    jaggedArray[c][r] = multiArray[c, r];
                }
            }
            return jaggedArray;
        }


/*
 * SplitPascalCase
 * Splits the given string by pascal case.
 * 
 * Author: Sheraz Naseeb
 * Submitted on: 11/15/2016 4:04:27 PM
 * 
 * Example: 
 * var str = "ThisIsAPascalCaseString";
return str.SplitPascalCase();

"This Is A Pascal Case String"
 */

/*
Requires a reference System.Text.RegularExpressions.
*/

        /// <summary>
        /// Splits the string by pascal case.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string SplitPascalCase(this string text)
        {
            return string.IsNullOrEmpty(text) ? text : Regex.Replace(text, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }


/*
 * HashBy
 * Implict hashing
 * 
 * Author: cia48621793
 * Submitted on: 4/28/2016 6:04:24 PM
 * 
 * Example: 
 * Console.WriteLine("Hello World!".ToByteArray().HashBy<MD5>());
 */

        public static byte[] HashBy<T>(this byte[] x) where T : HashAlgorithm
        {
            HashAlgorithm algo;
            try
            {
                algo = typeof(T)
                    .GetMethod("Create", BindingFlags.Public | BindingFlags.Static, null, new Type[] { }, null)
                    .Invoke(null, null) as HashAlgorithm;
            }
            catch
            {
                algo = Activator.CreateInstance<T>();
            }
            return algo.ComputeHash(x);
        }


/*
 * FirstMondayOfYear
 * FirstMondayOfYear
 * 
 * Author: Hakan YILMAZ
 * Submitted on: 4/4/2017 9:42:02 AM
 * 
 * Example: 
 * DateTime mnd = DateExtensions.FirstMondayOfYear(2017);
 */

        public static DateTime FirstMondayOfYear(int thisYear)
        {
            var firstDay = new DateTime(thisYear, 1, 1);
            return new DateTime(thisYear, 1, (8 - (int) firstDay.DayOfWeek) % 7 + 1);
        }


/*
 * IsValidIranianSocialCode
 * بررسی اعتبار کد ملی
 * 
 * Author: محمد مهدوی منش 09120611300
 * Submitted on: 1/14/2017 10:39:42 PM
 * 
 * Example: 
 * Console.WriteLine("2300060647".IsValidIranianSocialCode()); // true
            Console.WriteLine("2300060648".IsValidIranianSocialCode()); //false
            Console.WriteLine("2222222222".IsValidIranianSocialCode()); //false
 */

        public static bool IsValidIranianSocialCode(this string codeMelli)
        {
            switch (codeMelli)
            {
                case "1111111111":
                case "2222222222":
                case "3333333333":
                case "4444444444":
                case "5555555555":
                case "6666666666":
                case "7777777777":
                case "8888888888":
                case "9999999999":
                case "0000000000":
                    return false;
            }

            if (long.TryParse(codeMelli, out var result))
            {
                switch (result.NoOfDigits())
                {
                    // because in some cases user ignore the two starting zeros
                    case 8:
                        long.TryParse((string.Concat("00", result.ToString())), out result);
                        return result.CheckValidity();


                    // because in some cases user ignore the first starting zero
                    case 9:
                        long.TryParse((string.Concat("0", result.ToString())), out result);
                        return result.CheckValidity();


                    case 10:
                        return result.CheckValidity();

                    default: // below 8 digits is not valid Social No. Code
                        return false;
                }
            }
            else return false;
        }

        public static int NoOfDigits(this long input)
        {
            return input.ToString().Length;
        }

        public static bool CheckValidity(this long socialCode)
        {
            var splitSocialCode = socialCode.ToString().ToCharArray();
            var sum = 0;
            for (int i = 0, j = 10; i < 9 & j > 1; sum += int.Parse(splitSocialCode[i++].ToString()) * j--)
            {
            }
            var reminder = sum % 11;
            if (reminder < 2)
                return reminder == int.Parse(splitSocialCode[9].ToString()); // controlling digit
            else
                return (11 - reminder) == int.Parse(splitSocialCode[9].ToString());
        }


/*
 * Parse XML Physical Path to Class
 * Get XML from Physical Path and Parse into Class
 * 
 * Author: Keyur PANCHAL
 * Submitted on: 11/3/2016 10:27:02 AM
 * 
 * Example: 
 * none
 */

        public static T DeserilizeXml<T>(this string physicalPath) where T : class
        {
            try
            {
                if (!string.IsNullOrEmpty(physicalPath) && !string.IsNullOrWhiteSpace(physicalPath))
                {
                    if (File.Exists(physicalPath))
                    {
                        try
                        {
                            var xmlSerializer = new XmlSerializer(typeof(List<T>));
                            using (TextReader textReader = new StreamReader(physicalPath))
                            {
                                return (T) xmlSerializer.Deserialize(textReader);
                            }
                        }
                        catch (Exception)
                        {
                            return File.ReadAllText(physicalPath).ParseToClass<T>();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return null;
        }


/*
 * Split
 * Extension method to split string by number of characters.
 * 
 * Author: Ahmer Sohail
 * Submitted on: 9/14/2017 11:25:44 AM
 * 
 * Example: 
 * string[] A = "Ahmer-Sohail-Shamsi".Split(6, 6);
MessageBox.Show(A[0]);
MessageBox.Show(A[1]);
MessageBox.Show(A[2]);

/*
OUTPUT:

Sohail
Ahmer-
-Shamsi
 */

        /// <summary>
        /// Extension method to split string by number of characters.
        /// </summary>
        /// <param name="str">this object</param>
        /// <param name="startindex">The zero-based position to split the specified string.</param>
        /// <param name="length">The number of characters to split</param>
        public static string[] Split(this string str, int startindex, int length)
        {
            var strrtn = new string[3];
            if (startindex == 0)
                strrtn = new[] {str.Substring(startindex, length), str.Remove(startindex, length)};
            else if (startindex > 0)
                strrtn = new[]
                {
                    str.Substring(startindex, length), str.Substring(0, startindex), str.Remove(0, length + startindex)
                };
            return strrtn;
        }

#if NetFX
/*
 * ValidateNumber
 * Validates that input text is a number
 * 
 * Author: Ahmer Sohail
 * Submitted on: 8/24/2017 6:23:12 PM
 * 
 * Example: 
 * private void txtRollNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            (sender as TextBox).ValidateNumber(e, false);
        }
 */

        /// <summary>
        /// Extension method to validate that input text is a number.
        /// </summary>
        /// <param name="txt">this object</param>
        /// <param name="e">Key Press Event Initialization.</param>
        /// <param name="isCalculation">if true then decimal point (.) is allowed, if false then decimal point (.) is not allowed</param>
        public static void ValidateNumber(this System.Windows.Forms.TextBox txt, KeyPressEventArgs e,
            bool isCalculation)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == '.' && isCalculation)
                e.Handled = txt.Text.IndexOf('.') > -1;
            else e.Handled = true;
        }
#endif

/*
 * Zero Index CopyTo
 * CopyTo without the second parameter, for when you just want to copy array A to array B verbatim and size is not a concern.
 * 
 * Author: Benedict Tough
 * Submitted on: 7/10/2017 2:01:14 PM
 * 
 * Example: 
 * array.CopyTo(target);   // no index argument required
 */

        public static void CopyTo<T>(this T[] array, T[] target)
        {
            array.CopyTo(target, 0);
        }

/*
 * Get Percentage
 * Gets the specified percentage of the given value.
 * 
 * Author: Nicky Ernste
 * Submitted on: 8/25/2017 1:25:00 PM
 * 
 * Example: 
 * var value = 100d;
var result = value.GetPercentage(50); //Get 50% of 100 which is 50.
 */

        /// <summary>
        /// Get a certain percentage of the specified number.
        /// </summary>
        /// <param name="value">The number to get the percentage of.</param>
        /// <param name="percentage">The percentage of the specified number to get.</param>
        /// <returns>The actual specified percentage of the specified number.</returns>
        public static double GetPercentage(this double value, int percentage)
        {
            var percentAsDouble = (double) percentage / 100;
            return value * percentAsDouble;
        }


/*
 * Get
 * IDataReader extension to get values
 * 
 * Author: Everton Thomas
 * Submitted on: 8/17/2017 8:49:31 PM
 * 
 * Example: 
 * int id = reader.Get("Id", 10);
                        string nome = reader.Get("Nome", "...");
                        DateTime nascimento = reader.Get("Nascimento", DateTime.Now);
                        float valor = reader.Get("Valor", 0.05f);
                        decimal valorDecimal = reader.Get("Valor", new Decimal(15.75));
 */

        public static T Get<T>(this IDataReader rd, string column)
        {
            return rd.Get(column, default(T));
        }

        public static T Get<T>(this IDataReader rd, string column, T defaultValue)
        {
            try
            {
                var ordinal = rd.GetOrdinal(column);

                var value = rd[ordinal];

                if (rd.IsDBNull(ordinal))
                {
                    value = defaultValue;
                }

                return (T) Convert.ChangeType(value, typeof(T));
            }
            catch (Exception e)
            {
                throw new DataReaderParseFieldException(
                    $"Erro na conversão de valores do atributo: [{column}] para tipo [{typeof(T)}]", e);
            }
        }

        public class DataReaderParseFieldException : Exception
        {
            public DataReaderParseFieldException(string s, Exception exception) : base(s, exception)
            {
            }
        }


/*
 * StartOfWeek
 * TakeStartOfWeek
 * 
 * Author: Hakan YILMAZ
 * Submitted on: 9/8/2017 3:37:29 PM
 * 
 * Example: 
 * DateTime weekBeginning = Week.TakeStartOfWeek(DayOfWeek.Monday);
 */

        public static DateTime TakeStartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }


/*
 * GetSaturday
 * This code will provide the Sunday DateTime from the week of DateTime object the extension method is called from.
 * 
 * Author: Brent Coppock (original by Charles Cherry)
 * Submitted on: 11/23/2017 1:34:51 AM
 * 
 * Example: 
 * This code will provide the Saturday DateTime from the week of DateTime object the extension method is called from.
 */

        public static DateTime GetSaturday(this DateTime dt)
        {
            var date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            return new GregorianCalendar().AddDays(date, -((int) date.DayOfWeek) + 6);
        }


/*
 * GetSunday
 * This code will provide the Sunday DateTime from the week of DateTime object the extension method is called from.
 * 
 * Author: Brent Coppock (original by Charles Cherry)
 * Submitted on: 11/23/2017 1:30:05 AM
 * 
 * Example: 
 * DateTime oldDate = new DateTime(2017,5,5);
DateTime Sunday = oldDate.GetSunday();

results in a value of 2017-April-30 which is correct in this instance.
 */

        public static DateTime GetSunday(this DateTime dt)
        {
            var date = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            return new GregorianCalendar().AddDays(date, -((int) date.DayOfWeek));
        }


/*
 * ToLogString
 * Creates a string for logging purposes from an Exception. Includes the InnerException(s), Stacktrace et cetera.
 * 
 * Author: Mark de Rover
 * Submitted on: 1/2/2008 10:30:11 AM
 * 
 * Example: 
 * Exception ex = new Exception(“This is an error”);
string log = ex.ToLogString(“A fatal error occurred!”);
 */

        #region Exception.ToLogString

        /// <summary>
        /// <para>Creates a log-string from the Exception.</para>
        /// <para>The result includes the stacktrace, innerexception et cetera, separated by <seealso cref="Environment.NewLine"/>.</para>
        /// </summary>
        /// <param name="ex">The exception to create the string from.</param>
        /// <param name="additionalMessage">Additional message to place at the top of the string, maybe be empty or null.</param>
        /// <returns></returns>
        public static string ToLogStringEx(this Exception ex, string additionalMessage)
        {
            var msg = new StringBuilder();

            if (!string.IsNullOrEmpty(additionalMessage))
            {
                msg.Append(additionalMessage);
                msg.Append(Environment.NewLine);
            }

            if (ex == null) return msg.ToString();
            var orgEx = ex;

            msg.Append("Exception:");
            msg.Append(Environment.NewLine);
            while (orgEx != null)
            {
                msg.Append(orgEx.Message);
                msg.Append(Environment.NewLine);
                orgEx = orgEx.InnerException;
            }

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (ex.Data != null)
            {
                foreach (var i in ex.Data)
                {
                    msg.Append("Data :");
                    msg.Append(i);
                    msg.Append(Environment.NewLine);
                }
            }

            if (ex.StackTrace != null)
            {
                msg.Append("StackTrace:");
                msg.Append(Environment.NewLine);
                msg.Append(ex.StackTrace);
                msg.Append(Environment.NewLine);
            }

            if (ex.Source != null)
            {
                msg.Append("Source:");
                msg.Append(Environment.NewLine);
                msg.Append(ex.Source);
                msg.Append(Environment.NewLine);
            }

            if (ex.TargetSite != null)
            {
                msg.Append("TargetSite:");
                msg.Append(Environment.NewLine);
                msg.Append(ex.TargetSite);
                msg.Append(Environment.NewLine);
            }

            var baseException = ex.GetBaseException();
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (baseException != null)
            {
                msg.Append("BaseException:");
                msg.Append(Environment.NewLine);
                msg.Append(ex.GetBaseException());
            }
            return msg.ToString();
        }

        #endregion Exception.ToLogString


/*
 * FormatWithMask
 * Formats a string with the specified mask
 * 
 * Author: Vincent van Proosdij
 * Submitted on: 12/6/2011 1:12:58 PM
 * 
 * Example: 
 * var s = "aaaaaaaabbbbccccddddeeeeeeeeeeee".FormatWithMask("Hello ########-#A###-####-####-############ Oww");
            s.ShouldEqual("Hello aaaaaaaa-bAbbb-cccc-dddd-eeeeeeeeeeee Oww");

var s = "abc".FormatWithMask("###-#");
            s.ShouldEqual("abc-");

var s = "".FormatWithMask("Hello ########-#A###-####-####-############ Oww");
            s.ShouldEqual("");
 */

        /// <summary>
        /// Formats the string according to the specified mask
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
        /// <returns>The formatted string</returns>
        public static string FormatWithMask(this string input, string mask)
        {
            if (input.IsNullOrEmpty()) return input;
            var output = string.Empty;
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output += input[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }


/*
 * ToException
 * Turns any object to Exception. Very useful!
 * 
 * Author: Francisca Garse
 * Submitted on: 11/16/2012 2:10:47 PM
 * 
 * Example: 
 * Object o = new Object();
throw o.ToException();
 */

        public static Exception ToException(this object o)
        {
            return new Exception(o.ToString());
        }


/*
 * CreateDirectory
 * Recursively create directory based on the given path. If the given path doesn't exist, it will create until all the folders in the path are satisfied.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 4:06:18 PM
 * 
 * Example: 
 * string path = @"C:\temp\one\two\three";

var dir = new DirectoryInfo(path);
dir.CreateDirectory();
 */

        /// <summary>
        /// Recursively create directory
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }


/*
 * DefaultIfEmpty
 * returns default value if string is null or empty or white spaces string
 * 
 * Author: Alexander Gubenko
 * Submitted on: 4/22/2011 6:07:49 AM
 * 
 * Example: 
 * string str = null;
str.DefaultIfEmpty("I'm nil") // return "I'm nil"

string str1 = string.Empty;
str1.DefaultIfEmpty("I'm Empty") // return "I'm Empty!"

string str1 = "     ";
str1.DefaultIfEmpty("I'm WhiteSpaces strnig!", true) // return "I'm WhiteSpaces strnig!"
 */

        public static string DefaultIfEmpty(this string str, string defaultValue,
            bool considerWhiteSpaceIsEmpty = false)
        {
            return (considerWhiteSpaceIsEmpty ? string.IsNullOrWhiteSpace(str) : string.IsNullOrEmpty(str))
                ? defaultValue
                : str;
        }


/*
 * EnumToDictionary
 * Converts an Enumeration type into a dictionary of its names and values.
 * 
 * Author: Daniel Gidman
 * Submitted on: 12/3/2010 9:50:29 PM
 * 
 * Example: 
 * var dictionary = typeof(UriFormat).EnumToDictionary();

/* returns
key => value
SafeUnescaped => 3
Unescaped => 2
UriEscaped => 1
 */

        /// <summary>
        /// Converts Enumeration type into a dictionary of names and values
        /// </summary>
        /// <param name="t">Enum type</param>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            var names = Enum.GetNames(t);
            var values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new {Key = names[i], Value = (int) values.GetValue(i)})
                .ToDictionary(k => k.Key, k => k.Value);
        }


/*
 * ContainsAny
 * Checks if a given string contains any of the characters in the passed array of characters.
 * 
 * Author: Jonas Butt
 * Submitted on: 8/24/2008 1:10:27 AM
 * 
 * Example: 
 * char[] invalidFileNameCharacters = Path.GetInvalidFileNameChars();

if (newFileName.ContainsAny(invalidFileNameCharacters))
{
	MessageBox.Show("File name contains invalid characters.");
}
 */

        public static bool ContainsAny(this string theString, char[] characters)
        {
            foreach (var character in characters)
            {
                if (theString.Contains(character.ToString()))
                {
                    return true;
                }
            }
            return false;
        }


/*
 * Combinations
 * Returns all combinations of a chosen amount of selected elements in the sequence.
 * 
 * Author: Steven Jeuris
 * Submitted on: 11/15/2011 9:28:20 AM
 * 
 * Example: 
 * int[] numbers = new[] { 0, 1 };
var result = numbers.Combinations( 2, true );
// result == {{0, 0}, {0, 1}, {1, 0}, {1, 1}}
 */

        /// <summary>
        ///   Returns all combinations of a chosen amount of selected elements in the sequence.
        /// </summary>
        /// <typeparam name = "T">The type of the elements of the input sequence.</typeparam>
        /// <param name = "source">The source for this extension method.</param>
        /// <param name = "select">The amount of elements to select for every combination.</param>
        /// <param name = "repetition">True when repetition of elements is allowed.</param>
        /// <returns>All combinations of a chosen amount of selected elements in the sequence.</returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select,
            bool repetition = false)
        {
            Contract.Requires(source != null);
            Contract.Requires(select >= 0);

            var en = source as T[] ?? source.ToArray();
            return select == 0
                ? new[] {new T[0]}
                : en.SelectMany((element, index) =>
                    en
                        .Skip(repetition ? index : index + 1)
                        .Combinations(select - 1, repetition)
                        .Select(c => new[] {element}.Concat(c)));
        }


/*
 * AddRange<T>()
 * I have created this AddRange<T>() method on ObservableCollection<T> because the LINQ Concat() method didn't trigger the CollectionChanged event. This method does.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 4/1/2008 9:28:29 PM
 * 
 * Example: 
 * list.AddRange(anotherList);
 */

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }
        }


/*
 * ToString
 * Concatenates a specified separator String between each element of a specified enumeration, yielding a single concatenated string.
 * 
 * Author: Fons Sonnemans
 * Submitted on: 1/31/2008 10:19:34 AM
 * 
 * Example: 
 * var i = new int[] { 5, 12, 44, -4 };
Console.WriteLine(i.ToString(":"));
 */

        /// <summary>
        /// Concatenates a specified separator String between each element of a specified enumeration, yielding a single concatenated string.
        /// </summary>
        /// <typeparam name="T">any object</typeparam>
        /// <param name="list">The enumeration</param>
        /// <param name="separator">A String</param>
        /// <returns>A String consisting of the elements of value interspersed with the separator string.</returns>
        public static string ToString<T>(this IEnumerable<T> list, string separator)
        {
            var sb = new StringBuilder();
            foreach (var obj in list)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(obj);
            }
            return sb.ToString();
        }


/*
 * IsValidNIP, IsValidREGON, IsValidPESEL
 * Validation algorithms for Polish identification numbers NIP, REGON & PESEL.
 * 
 * Author: Marcin Kozub
 * Submitted on: 3/22/2012 10:08:31 PM
 * 
 * Example: 
 * string nip = "1234567890";
string regon = "123456789";
string pesel = "12345678901";

Console.WriteLine(nip.IsValidNIP() ? "NIP is valid" : "NIP is not valid");
Console.WriteLine(regon.IsValidREGON() ? "REGON is valid" : "REGON is not valid");
Console.WriteLine(pesel.IsValidPESEL() ? "PESEL is valid" : "PESEL is not valid");
 */

        public static bool IsValidNip(this string input)
        {
            int[] weights = {6, 5, 7, 2, 3, 4, 5, 6, 7};
            var result = false;
            if (input.Length == 10)
            {
                var controlSum = CalculateControlSum(input, weights);
                var controlNum = controlSum % 11;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                var lastDigit = int.Parse(input[input.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        public static bool IsValidRegon(this string input)
        {
            int controlSum;
            if (input.Length == 7 || input.Length == 9)
            {
                int[] weights = {8, 9, 2, 3, 4, 5, 6, 7};
                var offset = 9 - input.Length;
                controlSum = CalculateControlSum(input, weights, offset);
            }
            else if (input.Length == 14)
            {
                int[] weights = {2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8};
                controlSum = CalculateControlSum(input, weights);
            }
            else
            {
                return false;
            }

            var controlNum = controlSum % 11;
            if (controlNum == 10)
            {
                controlNum = 0;
            }
            var lastDigit = int.Parse(input[input.Length - 1].ToString());

            return controlNum == lastDigit;
        }

        public static bool IsValidPesel(this string input)
        {
            int[] weights = {1, 3, 7, 9, 1, 3, 7, 9, 1, 3};
            var result = false;
            if (input.Length == 11)
            {
                var controlSum = CalculateControlSum(input, weights);
                var controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                var lastDigit = int.Parse(input[input.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            var controlSum = 0;
            for (var i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }


#if NetFX
/*
 * QueryAsync
 * Returns a Task<IEnumerable<TResult>> to be used with the new async / await keyword.
 * 
 * Author: Sebastian Dau
 * Submitted on: 6/13/2012 1:36:23 PM
 * 
 * Example: 
 * BingSearchContainer _bsc = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/Search/"));

var webQ = _bsc.Image("search", null, null, null, null, null);

var result = await webQ.QueryAsync() ;
 */

        public static Task<IEnumerable<TResult>> QueryAsync<TResult>(this DataServiceQuery<TResult> query)
        {
            return Task<IEnumerable<TResult>>.Factory.FromAsync(query.BeginExecute, query.EndExecute, null);
        }
#endif

/*
 * Memoize<T, TResult>
 * Memoize afunction
 * 
 * Author: P.Revington
 * Submitted on: 4/26/2010 10:38:44 AM
 * 
 * Example: 
 * Func<string, string> format = new Func<string, string>(s =>
        {
            // a long running operation
            System.Threading.Thread.Sleep(2000);
            return String.Format("hello {0}", s);
        }).Memoize();
        // takes 2000 ms
        foreach (var item in Enumerable.Range(0, 100))
        {
            Response.WriteLine(format(" world"));
        }
 */

        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func)
        {
            var t = new Dictionary<T, TResult>();
            return n =>
            {
                if (t.ContainsKey(n)) return t[n];
                else
                {
                    var result = func(n);
                    t.Add(n, result);
                    return result;
                }
            };
        }


/*
 * IsIsin
 * Determines if a string is a valid ISIN (International Securities Identification Number) code.
 * 
 * Author: JP Negri
 * Submitted on: 3/31/2011 8:22:04 PM
 * 
 * Example: 
 * string s = "US0378331005";
Debug.Assert(s.IsIsin());

s = "AU0000XVGZA3";
Debug.Assert(s.IsIsin());

s = "GB0002634946";
Debug.Assert(s.IsIsin());

s = null;
Debug.Assert(!s.IsIsin());

s = "";
Debug.Assert(!s.IsIsin());

s = "us0378331005"; // lowercase
Debug.Assert(!s.IsIsin());

s = "US0378331004"; // wrong digit
Debug.Assert(!s.IsIsin());
 */

        private static readonly Regex Pattern = new Regex("[A-Z]{2}([A-Z0-9]){10}", RegexOptions.Compiled);

        /// <summary>
        /// True se a string passada for um ISIN válido
        /// </summary>
        public static bool IsIsin(this string isin)
        {
            if (string.IsNullOrEmpty(isin))
            {
                return false;
            }
            if (!Pattern.IsMatch(isin))
            {
                return false;
            }

            var digits = new int[22];
            var index = 0;
            for (var i = 0; i < 11; i++)
            {
                var c = isin[i];
                if (c >= '0' && c <= '9')
                {
                    digits[index++] = c - '0';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    var n = c - 'A' + 10;
                    var tens = n / 10;
                    if (tens != 0)
                    {
                        digits[index++] = tens;
                    }
                    digits[index++] = n % 10;
                }
                else
                {
                    // Not a digit or upper-case letter.
                    return false;
                }
            }
            var sum = 0;
            for (var i = 0; i < index; i++)
            {
                var digit = digits[index - 1 - i];
                if (i % 2 == 0)
                {
                    digit *= 2;
                }
                sum += digit / 10;
                sum += digit % 10;
            }

            var checkDigit = isin[11] - '0';
            if (checkDigit < 0 || checkDigit > 9)
            {
                // Not a digit.
                return false;
            }
            var tensComplement = (sum % 10 == 0) ? 0 : ((sum / 10) + 1) * 10 - sum;
            return checkDigit == tensComplement;
        }


/*
 * Aggregate
 * Since System.Linq.Enumerable.Aggregate throws a System.InvalidOperationException in case the given list is empty you can't use this function in a complex linq expression. This aggregate version simply returns a defaultvalue if the list is empty
 * 
 * Author: RW
 * Submitted on: 2/18/2009 11:37:02 AM
 * 
 * Example: 
 * return (  from x in _map.Keys
	  where CheckValue(x)
	  select _map[x]).			    Aggregate(MyFlags.None, (x, y) => x | y);
 */

        public static T Aggregate2<T>(
            this IEnumerable<T> list, Func<T, T, T> aggregateFunction)
        {
            return Aggregate2(list, default, aggregateFunction);
        }

        public static T Aggregate2<T>(this IEnumerable<T> list, T defaultValue,
            Func<T, T, T> aggregateFunction)
        {
            var en = list as T[] ?? list.ToArray();
            return en.Length <= 0 ? defaultValue : en.Aggregate(aggregateFunction);
        }


/*
 * Move FileInfo and automatically rename it
 * Moves a FileInfo instance to a specified path and rename it when already existing.
 * 
 * Author: Peter Rietveld
 * Submitted on: 4/22/2013 9:41:51 AM
 * 
 * Example: 
 * FileInfo fileInfo = new FileInfo(@"c:\test.txt");
File.Create(fileInfo.FullName).Dispose();
fileInfo.MoveTo(@"d:\", true);
 */

        /// <summary>
        /// Move current instance and rename current instance when needed
        /// </summary>
        /// <param name="fileInfo">Current instance</param>
        /// <param name="destFileName">The Path to move current instance to, which can specify a different file name</param>
        /// <param name="renameWhenExists">Bool to specify if current instance should be renamed when exists</param>
        public static void MoveTo(this FileInfo fileInfo, string destFileName, bool renameWhenExists = false)
        {
            var newFullPath = string.Empty;

            if (renameWhenExists)
            {
                var count = 1;

                var fileNameOnly = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                var extension = Path.GetExtension(fileInfo.FullName);
                newFullPath = Path.Combine(destFileName, fileInfo.Name);

                while (File.Exists(newFullPath))
                {
                    var tempFileName = $"{fileNameOnly}({count++})";
                    newFullPath = Path.Combine(destFileName, tempFileName + extension);
                }
            }

            fileInfo.MoveTo(renameWhenExists ? newFullPath : destFileName);
        }


/*
 * Filter
 * Allows you to filter an IEnumerable<T>
 * 
 * Author: Jeff Reddy
 * Submitted on: 8/9/2011 6:06:20 PM
 * 
 * Example: 
 * static void Main() {

            var items = new List<TestItem> {
                    new TestItem {ItemID = 1, ItemName = "TestItem"},
                    new TestItem {ItemID = 2, ItemName = "Wigit"},
                    new TestItem {ItemID = 3, ItemName = "TestItem2"},
                    new TestItem {ItemID = 4, ItemName = "Foo"},
                    new TestItem {ItemID = 5, ItemName = "Bar"},
                    new TestItem {ItemID = 6, ItemName = "TestFooBarItem"}
            };

            Console.WriteLine("Items starting with Test using delegate");
            Func<TestItem, bool> itemNameFilter = delegate(TestItem testItem) { return testItem.ItemName.StartsWith("Test"); };
            foreach (var testItem in items.Filter(itemNameFilter)) {
                Console.WriteLine(testItem.ItemName);
            }

            Console.WriteLine("Items with ItemName containing Item and ItemID > 2 using Lamda Expression");
            foreach (var testItem in items.Filter(x => x.ItemName.StartsWith("Test") && x.ItemID > 2)) {
                Console.WriteLine(testItem.ItemName);
            }

            Console.ReadLine();
        }
 */

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> filterParam)
        {
            return list.Where(filterParam);
        }


/*
 * ToProperCase
 * Converts string to a title case.
 * 
 * Author: Earljon Hidalgo
 * Submitted on: 3/26/2008 5:29:23 AM
 * 
 * Example: 
 * string s = "tHiS is a sTring TesT";
Console.WriteLine("Title Case: {0}", s.ToProperCase());

// Output: This Is A String Test
 */

        public static string ToProperCase(this string text)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }

/*
 * GetLastDayOfMonth
 * Gets the last date of the month of the DateTime.
 * 
 * Author: Brendan Enrick
 * Submitted on: 1/22/2009 4:31:49 PM
 * 
 * Example: 
 * DateTime lastDay = DateTime.Now.GetLastDayOfMonth();
 */

        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

/*
 * IsSubclassOfRawGeneric
 * Is essentially a modified version of Type.IsSubClassOf that supports checking whether a class derives from a generic base-class without specifying the type parameters. For instance, it supports typeof(List<>) to see if a class derives from the List<T> class. The actual code was borrowed from http://stackoverflow.com/questions/457676/c-reflection-check-if-a-class-is-derived-from-a-generic-class.
 * 
 * Author: Dennis Doomen
 * Submitted on: 2/17/2009 10:05:31 AM
 * 
 * Example: 
 * bool isDerived = someType.IsSubclassOfRawGeneric(typeof(List<>));
 */

        /// <summary>
        /// Alternative version of <see cref="Type.IsSubclassOf"/> that supports raw generic types (generic types without
        /// any type parameters).
        /// </summary>
        /// <param name="baseType">The base type class for which the check is made.</param>
        /// <param name="toCheck">To type to determine for whether it derives from <paramref name="baseType"/>.</param>
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type baseType)
        {
            while (toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (baseType == cur)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }
    }
}