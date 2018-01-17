using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HSNXT.aResources;
using HSNXT.SuccincT.Functional;
using Newtonsoft.Json.Converters;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static T GetHead<T>(this IConsEnumerable<T> @this)
        {
            return @this.Cons().Head.Value;
        }
        
        /// <summary>
        /// Checks if this string is a valid VEVO user account.
        /// Requirements for valid VEVO user account strings:
        /// <ul>
        /// <li>Must only contain uppercase, lowercase, and digit characters.</li>
        /// <li>Must not exceed 80 characters length.</li>
        /// <li>Must not contain any whitespace characters (whitespaces, tabs, etc.)</li>
        /// <li>Must have "VEVO" substring at the end of string</li>
        /// </ul>
        /// </summary>
        /// <example>
        /// <p>Valid inputs:</p>
        /// <br/>AdeleVEVO
        /// <br/>ConnieTalbotVEVO
        /// <br/>SHMVEVO
        /// <br/>justimberlakeVEVO
        /// <br/>DJMartinJensenVEVO
        /// <br/>test123VEVO
        /// </example><example>
        /// <p>Invalid inputs:</p>
        /// <br/>syam kapuk
        /// <br/>jypentertainment
        /// <br/>Noche de Brujas
        /// <br/>testVEVO123
        /// </example>
        /// <param name="a">The string to check.</param>
        /// <returns></returns>
        public static bool IsVevo(this string a) =>
            !a.Contains("\t") & !a.Contains(" ") & a.Length < 81 & a.EndsWith("VEVO");
        
        /// <summary>
        /// Draws ASCII christmas lights!
        /// </summary>
        /// <param name="i">The amount of christmas lights to draw</param>
        /// <returns>ASCII christmas lights.</returns>
        public static string ChristmasLights(this int i)
        {
            var j = 1;
            var o = new StringBuilder("     .");
            for (; j++ < i;)
            {
                o.Append("--.__.--.");
            }
            o.Append("\n   _(_    ");
            for (; --j > 2;)
            {
                o.Append("   _Y_   ");
            }
            o.Append("    _)_");
            string[] a = {"  [___]  ", "  /:' \\  ", " |::   | ", " \\::.  / ", "  \\::./  ", "   '='   "};
            foreach (var b in a)
            {
                for (j = 0; j++ < i;)
                {
                    o.Append(j == 1 ? "\n" + b + " " :
                             j == i ? " " + b : b);
                }
            }
            return o.ToString();
        }

        /// <summary>
        /// Convert a byte array into a string of hexadecimal values.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes)
        {
            return BitConverter.ToString(bytes, 0).Replace("-", " ");
        }

        /// <summary>
        /// Convert a string containing 2-digit hexadecimal
        /// values separated by spaces or dashes into a byte array.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string hex)
        {
            // Separate the bytes.
            var values = hex.Split(' ', '-');

            // Make room.
            var numBytes = values.Length;
            var bytes = new byte[numBytes];

            // Parse the byte representations.
            for (var i = 0; i < numBytes; i++)
                bytes[i] = Convert.ToByte(values[i], 16);

            return bytes;
        }

        // Encrypt or decrypt the data in in_bytes[] and return the result.
        public static byte[] CryptBytes(string password,
            byte[] inBytes, bool encrypt)
        {
            // Make an AES service provider.
            var aesProvider =
                new AesCryptoServiceProvider {Padding = PaddingMode.Zeros};

            // Find a valid key size for this provider.
            var keySizeBits = 0;
            for (var i = 1024; i > 1; i--)
            {
                if (!aesProvider.ValidKeySize(i)) continue;
                keySizeBits = i;
                break;
            }
            Debug.Assert(keySizeBits > 0);
            Console.WriteLine(Resource1.CryptBytesKeySize + keySizeBits);

            // Get the block size for this provider.
            var blockSizeBits = aesProvider.BlockSize;

            // Generate the key and initialization vector.
            byte[] salt =
            {
                0x0, 0x20, 0x11, 0x27, 0x3A, 0xB4,
                0x57, 0xC6, 0xF1, 0xF0, 0xEE, 0x21, 0x22, 0x45
            };
            MakeKeyAndIv(password, salt, keySizeBits,
                blockSizeBits, out var key, out var iv);

            // Make the encryptor or decryptor.
            var cryptoTransform = encrypt ? aesProvider.CreateEncryptor(key, iv) : aesProvider.CreateDecryptor(key, iv);

            // Create the output stream.
            using (var outStream = new MemoryStream())
            {
                // Attach a crypto stream to the output stream.
                using (var cryptoStream =
                    new CryptoStream(outStream, cryptoTransform,
                        CryptoStreamMode.Write))
                {
                    // Write the bytes into the CryptoStream.
                    cryptoStream.Write(inBytes, 0, inBytes.Length);
                    try
                    {
                        cryptoStream.FlushFinalBlock();
                    }
                    catch (CryptographicException)
                    {
                        // Ignore this exception. The password is bad.
                    }

                    // return the result.
                    return outStream.ToArray();
                }
            }
        }

        // Use the password to generate key bytes.
        private static void MakeKeyAndIv(string password, byte[] salt,
            int keySizeBits, int blockSizeBits,
            out byte[] key, out byte[] iv)
        {
            var deriveBytes =
                new Rfc2898DeriveBytes(password, salt, 1000);

            key = deriveBytes.GetBytes(keySizeBits / 8);
            iv = deriveBytes.GetBytes(blockSizeBits / 8);
        }

        public static unsafe bool All(ulong* arr, int size, Func<ulong, bool> predicate)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            for (var i = 0; i < size; i++)
            {
                if (!predicate(arr[i]))
                    return false;
            }
            return true;
        }

        public static unsafe bool All(ulong* arr, int size, Func<ulong, int, bool> predicate)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            for (var i = 0; i < size; i++)
            {
                if (!predicate(arr[i], i))
                    return false;
            }
            return true;
        }

        public static unsafe ulong Aggregate(ulong* source, int size, Func<ulong, ulong, ulong> func)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (size == 0)
                return 0;
            if (size == 1)
                return source[0];

            var source1 = source[0];
            for (var i = 1; i < size; i++)
            {
                source1 = func(source1, source[i]);
            }
            return source1;
        }

        /// <summary>
        /// Get a task that resolves when all these tasks have successfully completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static Task All(this IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Get a task that resolves when any of these tasks have successfully completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static Task Any(this IEnumerable<Task> tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Get a task that resolves when all these tasks have successfully completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task All<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Get a task that resolves when any of these tasks have successfully completed
        /// </summary>
        /// <param name="tasks"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task Any<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAny(tasks);
        }

        public static void Benchmark(this Action a)
        {
            Benchmark(a, 10000);
        }

        public static void Benchmark(this Action a, int iterations, int numberOfBenchmarks = 1)
        {
            var process = Process.GetCurrentProcess();
            var oldAffinity = process.ProcessorAffinity;
            process.ProcessorAffinity = (IntPtr) 1;

            var totalMilliseconds = 0D;
            for (var benchmarkNumber = 0; benchmarkNumber < numberOfBenchmarks; benchmarkNumber++)
            {
                var stopwatch = new Stopwatch();

                stopwatch.Start();
                for (var i = 0; i < iterations; i++)
                {
                    a();
                }
                stopwatch.Stop();

                totalMilliseconds += stopwatch.Elapsed.TotalMilliseconds;
                Console.Write(stopwatch.Elapsed.TotalMilliseconds);
                if (numberOfBenchmarks > 1 && benchmarkNumber < numberOfBenchmarks)
                    Console.Write(Resource1.BenchmarkComma);
            }

            if (numberOfBenchmarks > 1)
                Console.Write(Resource1.BenchmarkAverage + (totalMilliseconds / numberOfBenchmarks));

            Console.WriteLine();

            process.ProcessorAffinity = oldAffinity;
        }

        /// <summary>
        /// Disable continue on captured context for this task
        /// </summary>
        /// <param name="task"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ConfiguredTaskAwaitable<T> Cfg<T>(this Task<T> task)
        {
            return task.ConfigureAwait(false);
        }

        /// <summary>
        /// Synchronously await this task
        /// </summary>
        /// <param name="task"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Sync<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously await this task
        /// </summary>
        /// <param name="task"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T AwaitSync<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }

        public static string UncollapseDigits(this string s)
        {
            return Regex.Replace(s, "one|tw|th|f|z|s|.i", " $0");
        }

        public static uint SwapEndianness(this uint n)
        {
            n = n >> 16 | n << 16;
            return(n & 0xFF00FF00) >> 8 | (n & 0xFF00FF) << 8;
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of days to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of whole and fractional days. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of days represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractDays(this DateTime self, double value)
        {
            return self.AddDays(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of hours to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of whole and fractional hours. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of hours represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractHours(this DateTime self, double value)
        {
            return self.AddHours(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of milliseconds to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of whole and fractional milliseconds. The <paramref name="value" /> parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of milliseconds represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractMilliseconds(this DateTime self, double value)
        {
            return self.AddMilliseconds(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of minutes to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of whole and fractional minutes. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of minutes represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractMinutes(this DateTime self, double value)
        {
            return self.AddMinutes(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of months to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of months. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the sum of the date and time represented by this instance and <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.-or-
        /// <paramref name="value" /> is less than -120,000 or greater than 120,000. </exception>
        public static DateTime SubtractMonths(this DateTime self, int value)
        {
            return self.AddMonths(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of seconds to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of whole and fractional seconds. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of seconds represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractSeconds(this DateTime self, double value)
        {
            return self.AddSeconds(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of ticks to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of 100-nanosecond ticks. The <paramref name="value" /> parameter can be positive or negative. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the time represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractTicks(this DateTime self, long value)
        {
            return self.AddTicks(-value);
        }

        /// <summary>Returns a new <see cref="T:System.DateTime" /> that subtracts the specified number of years to the value of this instance.</summary>
        /// <param name="self">This object</param>
        /// <param name="value">A number of years. The <paramref name="value" /> parameter can be negative or positive. </param>
        /// <returns>An object whose value is the date and time represented by this instance minus the number of years represented by <paramref name="value" />.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="value" /> or the resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />. </exception>
        public static DateTime SubtractYears(this DateTime self, int value)
        {
            return self.AddYears(-value);
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> items)
        {
            while (items.MoveNext())
            {
                yield return items.Current;
            }
        }

        public static IEnumerable<T> SideBySideEnumerable<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            using (var afirst = first.GetEnumerator())
            using (var asecond = second.GetEnumerator())
                while (afirst.MoveNext() && asecond.MoveNext())
                {
                    yield return afirst.Current;
                    yield return asecond.Current;
                }
        }

        public static IEnumerable<T> SideBySideEnumerable<T>(this IEnumerable<T> first, params IEnumerable<T>[] others)
        {
            using (var afirst = first.GetEnumerator())
            {
                var aothers = others.Select(e => e.GetEnumerator()).ToArray();
                while (afirst.MoveNext())
                {
                    foreach (var e in aothers)
                    {
                        if (!e.MoveNext()) yield break;
                    }
                    yield return afirst.Current;
                    foreach (var enumerator in aothers)
                    {
                        yield return enumerator.Current;
                    }
                }
            }
        }

        public static IEnumerable<(T1, T2)> SideBySideEnumerableTuple<T1, T2>(this IEnumerable<T1> first,
            IEnumerable<T2> second)
        {
            using (var afirst = first.GetEnumerator())
            using (var asecond = second.GetEnumerator())
                while (afirst.MoveNext() && asecond.MoveNext())
                {
                    yield return (afirst.Current, asecond.Current);
                }
        }

        public static T[] ReflectToArray<T>(this IList<T> instance)
        {
            foreach (var f in instance.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                T[] v;
                if (f.FieldType == typeof(T[]) && (v = f.GetValue(instance) as T[])?.Length >= instance.Count)
                {
                    return v;
                }
            }
            return null;
        }

        public static T[] ReflectToArray<T>(this List<T> instance)
        {
            foreach (var f in instance.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                T[] v;
                if (f.FieldType == typeof(T[]) && (v = f.GetValue(instance) as T[])?.Length >= instance.Count)
                {
                    return v;
                }
            }
            return null;
        }

        public static T[] ReflectToArrayReadOnly<T>(this IReadOnlyList<T> instance)
        {
            foreach (var f in instance.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                T[] v;
                if (f.FieldType == typeof(T[]) && (v = f.GetValue(instance) as T[])?.Length >= instance.Count)
                {
                    return v;
                }
            }
            return null;
        }

        public static string ToJson(this object entity)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new StringEnumConverter());
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, entity);
                return writer.ToString();
            }
        }

        public static bool EqualsAny<T>(this T a, params T[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (a.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool EqualsAll<T>(this T a, params T[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!a.Equals(s))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool EqualsNone<T>(this T a, params T[] b)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var s in b)
            {
                if (!a.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<T> ReturnEnumerable<T>(this T item)
        {
            return new[] {item};
        }

        public static string EnumerableToString(this IEnumerable<char> enumerable) => new string(enumerable.ToArray());

        public static string EnumerableToString(this IEnumerable<string> enumerable) => enumerable.StringJoin("");

        public static string EnumerableToString(this IEnumerable<IEnumerable<char>> enumerable) =>
            new string(enumerable.SelectMany(e => e).ToArray());

        /// <summary>
        /// Converts the target float in to a string with the specified number of decimal places.
        /// </summary>
        /// <param name="this">The extended float.</param>
        /// <param name="numDecimalPlaces">The number of decimal places to display.</param>
        /// <returns>A string that represents the target float.</returns>
        public static string ToString(this float @this, uint numDecimalPlaces)
        {
            var formatString = "{0:n" + numDecimalPlaces + "}";
            return string.Format(CultureInfo.InvariantCulture, formatString, @this);
        }

        public static IEnumerable<T[]> TakeWindow<T>(this IEnumerable<T> e, int count)
        {
            var window = new LinkedList<T>();
            foreach (var elem in e)
            {
                if (window.Count == count)
                {
                    yield return window.ToArray();
                    window.RemoveFirst();
                }
                window.AddLast(elem);
            }
            yield return window.ToArray();
        }

        public static string ToString<T>(this IEnumerable<T> list, Func<T, string> itemOutput, string seperator = ",")
        {
            list = list ?? new List<T>();
            seperator = seperator ?? "";
            itemOutput = itemOutput ?? (x => x.ToString());
            var builder = new StringBuilder();
            var tempSeperator = "";
            foreach (var item in list)
            {
                builder.Append(tempSeperator).Append(itemOutput(item));
                tempSeperator = seperator;
            }
            return builder.ToString();
        }

        public static string ToStringOrDefault<T>(this T? nullable, string defaultValue) where T : struct =>
            nullable?.ToString() ?? defaultValue;

        public static string ToStringOrDefault<T>(this T? nullable, string format, string defaultValue)
            where T : struct, IFormattable => nullable?.ToString(format, CultureInfo.CurrentCulture) ?? defaultValue;

        public static string ToStringOrDefaultInvariant<T>(this T? nullable, string format, string defaultValue)
            where T : struct, IFormattable => nullable?.ToString(format, CultureInfo.InvariantCulture) ?? defaultValue;

        /// <summary>
        /// this is really missing from C# - returns the key of the highest value in a dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key type (determined from the dictionary)</typeparam>
        /// <typeparam name="TValue">Value type (determined from the dictionary), must implement IComparable&lt;Value&gt;</typeparam>
        /// <param name="dictionary">The dictionary</param>
        /// <returns>The key of the highest value in the dic.</returns>
        public static TKey Max<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            where TValue : IComparable<TValue>
        {
            if (dictionary == null || dictionary.Count == 0) return default;
            var dicList = dictionary.ToList();
            var maxKvp = dicList.First();
            foreach (var kvp in dicList.Skip(1))
            {
                if (kvp.Value.CompareTo(maxKvp.Value) > 0)
                {
                    maxKvp = kvp;
                }
            }
            return maxKvp.Key;
        }

        public static IEnumerable<TDerived> WhereIs<TBase, TDerived>(this IEnumerable<TBase> source)
            where TBase : class
            where TDerived : class, TBase => source.OfType<TDerived>();


        public static string ToStringInvariant(this DateTime o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringCurrent(this DateTime o)
        {
            return o.ToString(CultureInfo.CurrentCulture);
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException(nameof(source));
            return list.Contains(source);
        }

        /// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers and the specified culture-specific formatting information.</summary>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
        public static string[] GetDateTimeFormatsInvariant(this DateTime o)
        {
            return o.GetDateTimeFormats(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.</summary>
        /// <param name="o">This object</param>
        /// <param name="format">A standard date and time format string (see Remarks). </param>
        /// <returns>A string array where each element is the representation of the value of this instance formatted with the <paramref name="format" /> standard date and time format specifier.</returns>
        /// <exception cref="T:System.FormatException">
        /// <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
        public static string[] GetDateTimeFormatsInvariant(this DateTime o, char format)
        {
            return o.GetDateTimeFormats(format, CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
        /// <returns>A Boolean value equivalent to the value of this instance.</returns>
        public static bool ToBooleanInvariant(this IConvertible o)
        {
            return o.ToBoolean(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
        /// <returns>A Unicode character equivalent to the value of this instance.</returns>
        public static char ToCharInvariant(this IConvertible o)
        {
            return o.ToChar(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
        public static sbyte ToSByteInvariant(this IConvertible o)
        {
            return o.ToSByte(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
        public static byte ToByteInvariant(this IConvertible o)
        {
            return o.ToByte(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
        public static short ToInt16Invariant(this IConvertible o)
        {
            return o.ToInt16(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ushort ToUInt16Invariant(this IConvertible o)
        {
            return o.ToUInt16(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public static int ToInt32Invariant(this IConvertible o)
        {
            return o.ToInt32(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public static uint ToUInt32Invariant(this IConvertible o)
        {
            return o.ToUInt32(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public static long ToInt64Invariant(this IConvertible o)
        {
            return o.ToInt64(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ulong ToUInt64Invariant(this IConvertible o)
        {
            return o.ToUInt64(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public static float ToSingleInvariant(this IConvertible o)
        {
            return o.ToSingle(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
        public static double ToDoubleInvariant(this IConvertible o)
        {
            return o.ToDouble(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
        public static decimal ToDecimalInvariant(this IConvertible o)
        {
            return o.ToDecimal(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
        public static DateTime ToDateTimeInvariant(this IConvertible o)
        {
            return o.ToDateTime(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
        public static string ToStringInvariant(this IConvertible o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
        /// <param name="o">This object</param>
        /// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
        /// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
        public static object ToTypeInvariant(this IConvertible o, Type conversionType)
        {
            return o.ToType(conversionType, CultureInfo.InvariantCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.</summary>
        /// <returns>A Boolean value equivalent to the value of this instance.</returns>
        public static bool ToBoolean(this IConvertible o)
        {
            return o.ToBoolean(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.</summary>
        /// <returns>A Unicode character equivalent to the value of this instance.</returns>
        public static char ToChar(this IConvertible o)
        {
            return o.ToChar(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit signed integer equivalent to the value of this instance.</returns>
        public static sbyte ToSByte(this IConvertible o)
        {
            return o.ToSByte(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 8-bit unsigned integer equivalent to the value of this instance.</returns>
        public static byte ToByte(this IConvertible o)
        {
            return o.ToByte(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit signed integer equivalent to the value of this instance.</returns>
        public static short ToInt16(this IConvertible o)
        {
            return o.ToInt16(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 16-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ushort ToUInt16(this IConvertible o)
        {
            return o.ToUInt16(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit signed integer equivalent to the value of this instance.</returns>
        public static int ToInt32(this IConvertible o)
        {
            return o.ToInt32(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 32-bit unsigned integer equivalent to the value of this instance.</returns>
        public static uint ToUInt32(this IConvertible o)
        {
            return o.ToUInt32(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit signed integer equivalent to the value of this instance.</returns>
        public static long ToInt64(this IConvertible o)
        {
            return o.ToInt64(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.</summary>
        /// <returns>An 64-bit unsigned integer equivalent to the value of this instance.</returns>
        public static ulong ToUInt64(this IConvertible o)
        {
            return o.ToUInt64(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A single-precision floating-point number equivalent to the value of this instance.</returns>
        public static float ToSingle(this IConvertible o)
        {
            return o.ToSingle(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.</summary>
        /// <returns>A double-precision floating-point number equivalent to the value of this instance.</returns>
        public static double ToDouble(this IConvertible o)
        {
            return o.ToDouble(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.Decimal" /> number using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.Decimal" /> number equivalent to the value of this instance.</returns>
        public static decimal ToDecimal(this IConvertible o)
        {
            return o.ToDecimal(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.DateTime" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.DateTime" /> instance equivalent to the value of this instance.</returns>
        public static DateTime ToDateTime(this IConvertible o)
        {
            return o.ToDateTime(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an equivalent <see cref="T:System.String" /> using the specified culture-specific formatting information.</summary>
        /// <returns>A <see cref="T:System.String" /> instance equivalent to the value of this instance.</returns>
        public static string ToString(this IConvertible o)
        {
            return o.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>Converts the value of this instance to an <see cref="T:System.Object" /> of the specified <see cref="T:System.Type" /> that has an equivalent value, using the specified culture-specific formatting information.</summary>
        /// <param name="o">This object</param>
        /// <param name="conversionType">The <see cref="T:System.Type" /> to which the value of this instance is converted. </param>
        /// <returns>An <see cref="T:System.Object" /> instance of type <paramref name="conversionType" /> whose value is equivalent to the value of this instance.</returns>
        public static object ToType(this IConvertible o, Type conversionType)
        {
            return o.ToType(conversionType, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts an Object to it's integer value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static int ToInteger(this object objectToConvert)
        {
            try
            {
                return Convert.ToInt32(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Integer", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's integer value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static long ToLong(this object objectToConvert)
        {
            try
            {
                return Convert.ToInt64(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Long", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's double value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static double ToDouble(this object objectToConvert)
        {
            try
            {
                return Convert.ToDouble(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to double", e);
            }
        }

        /// <summary>
        /// Converts an Object to it's decimal value
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object objectToConvert)
        {
            try
            {
                return Convert.ToDecimal(objectToConvert.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to decimal", e);
            }
        }

        /// <summary>
        /// Converts an str to it's decimal value
        /// </summary>
        /// <returns></returns>
        public static decimal ToDecimal(this string strToConvert)
        {
            try
            {
                return Convert.ToDecimal(strToConvert);
            }
            catch (Exception e)
            {
                throw new Exception("str cannot be converted to decimal", e);
            }
        }

        public static bool ToBool(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to Boolean", e);
            }
        }

        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                return Convert.ToDateTime(Convert.ToString(obj));
            }
            catch (Exception e)
            {
                throw new Exception("Object cannot be converted to DateTime. Object: " + obj, e);
            }
        }

        /// <summary>
        /// Selects specific number of rows from a datatable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static DataTable SelectRows(this DataTable dataTable, int rowCount)
        {
            try
            {
                var myTable = dataTable.Clone();
                var myRows = dataTable.Select();
                for (var i = 0; i < rowCount; i++)
                {
                    if (i >= myRows.Length) continue;
                    myTable.ImportRow(myRows[i]);
                    myTable.AcceptChanges();
                }

                return myTable;
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Accepts a date time value, calculates number of days, minutes or seconds and shows 'pretty dates'
        /// like '2 days ago', '1 week ago' or '10 minutes ago'
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetPrettyDate(this DateTime d)
        {
            // 1.
            // Get time span elapsed since the date.
            var s = DateTime.Now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            var dayDiff = (int) s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            var secDiff = (int) s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return d.ToStringInvariant();
            }

            // 5.
            // Handle same-day times.
            switch (dayDiff)
            {
                case 0:
                    // A.
                    // Less than one minute ago.
                    if (secDiff < 60)
                    {
                        return "just now";
                    }
                    // B.
                    // Less than 2 minutes ago.
                    if (secDiff < 120)
                    {
                        return "1 minute ago";
                    }
                    // C.
                    // Less than one hour ago.
                    if (secDiff < 3600)
                    {
                        return $"{Math.Floor((double) secDiff / 60)} minutes ago";
                    }
                    // D.
                    // Less than 2 hours ago.
                    if (secDiff < 7200)
                    {
                        return "1 hour ago";
                    }
                    // E.
                    // Less than one day ago.
                    if (secDiff < 86400)
                    {
                        return $"{Math.Floor((double) secDiff / 3600)} hours ago";
                    }
                    break;
                case 1:
                    return "yesterday";
            }
            // 6.
            // Handle previous days.
            if (dayDiff < 7)
            {
                return $"{dayDiff} days ago";
            }
            return dayDiff < 31 ? $"{Math.Ceiling((double) dayDiff / 7)} weeks ago" : null;
        }

        public static IEnumerable<string> SplitLazy(this string self, char c)
        {
            return new LazySplitter(self, c);
        }

        public static IEnumerable<(string substring, int index)> SplitLazyWithIndex(this string self, char c)
        {
            return new LazySplitterIndex(self, c);
        }

        public static void AddAll<T>(this ICollection<T> self, params T[] items) => self.AddRange(items);
    }
}