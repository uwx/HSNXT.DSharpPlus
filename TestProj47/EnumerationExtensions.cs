using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Includes an enumerated type and returns the new value
        /// </summary>
        public static T Include<T>(this Enum value, T append)
        {
            var type = value.GetType();

            //determine the values
            object result = value;
            var parsed = new Value(append, type);
            if (parsed.Signed != null)
            {
                result = Convert.ToInt64(value) | (long) parsed.Signed;
            }
            else if (parsed.Unsigned != null)
            {
                result = Convert.ToUInt64(value) | (ulong) parsed.Unsigned;
            }

            //return the final value
            return (T) Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Removes an enumerated type and returns the new value
        /// </summary>
        public static T Remove<T>(this Enum value, T remove)
        {
            var type = value.GetType();

            //determine the values
            object result = value;
            var parsed = new Value(remove, type);
            if (parsed.Signed is long l)
            {
                result = Convert.ToInt64(value) & ~l;
            }
            else if (parsed.Unsigned != null)
            {
                result = Convert.ToUInt64(value) & ~(ulong) parsed.Unsigned;
            }

            //return the final value
            return (T) Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Checks if an enumerated type contains a value
        /// </summary>
        public static bool Has<T>(this Enum value, T check)
        {
            var type = value.GetType();

            //determine the values
            var parsed = new Value(check, type);
            if (parsed.Signed is long l)
            {
                return (Convert.ToInt64(value) & l) == l;
            }
            if (parsed.Unsigned is ulong l2)
            {
                return (Convert.ToUInt64(value) &
                        l2) == l2;
            }
            return false;
        }

        /// <summary>
        /// Checks if an enumerated type is missing a value
        /// </summary>
        public static bool Missing<T>(this Enum obj, T value)
        {
            return !obj.Has(value);
        }

        //class to simplfy narrowing values between 
        //a ulong and long since either value should
        //cover any lesser value
        private class Value
        {
            //cached comparisons for tye to use
            private static readonly Type UInt64 = typeof(ulong);

            private static readonly Type UInt32 = typeof(long);

            public readonly long? Signed;
            public readonly ulong? Unsigned;

            public Value(object value, Type type)
            {
                //make sure it is even an enum to work with
                if (!type.IsEnum)
                {
                    throw new
                        ArgumentException("Value provided is not an enumerated type!");
                }

                //then check for the enumerated value
                var compare = Enum.GetUnderlyingType(type);

                //if this is an unsigned long then the only
                //value that can hold it would be a ulong
                if (compare == UInt32 || compare == UInt64)
                {
                    this.Unsigned = Convert.ToUInt64(value);
                }
                //otherwise, a long should cover anything else
                else
                {
                    this.Signed = Convert.ToInt64(value);
                }
            }
        }
    }
}