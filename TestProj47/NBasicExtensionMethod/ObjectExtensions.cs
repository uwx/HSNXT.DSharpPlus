// Copyright 2012, Ben Aston (ben@bj.ma).
// 
// This file is part of NBasicExtensionMethod.
// 
// NBasicExtensionMethod is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// NBasicExtensionMethod is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with NBasicExtensionMethod. If not, see <http://www.gnu.org/licenses/>.

using System;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool IsNot<T>(this T o1, T o2) where T : struct
        {
            return !o1.Equals(o2);
        }

        public static T EnsuringThat<T, TException>(this T o, Func<T, bool> assertionAboutO, string message)
            where TException : Exception, new()
        {
            if (assertionAboutO(o))
            {
                return o;
            }

            var e = new TException();
            e.Data.Add("Details", message);
            throw e;
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type.
        /// </summary>
        public static bool Is<T>(this T o, Func<T, bool> a) where T : class
        {
            return a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to perform a calculation on a reference type.
        /// </summary>
        public static T2 Calculate<T1, T2>(this T1 o, Func<T1, T2> a) where T1 : class
        {
            return a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type, *and then invert it*.
        /// </summary>
        public static bool IsNot<T>(this T o, Func<T, bool> a) where T : class
        {
            return !a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type.
        /// </summary>
        public static bool Has<T>(this T o, Func<T, bool> a) where T : class
        {
            return a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type, *and then invert it*.
        /// </summary>
        public static bool HasNot<T>(this T o, Func<T, bool> a) where T : class
        {
            return !a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type.
        /// </summary>
        public static bool Does<T>(this T o, Func<T, bool> a) where T : class
        {
            return a(o);
        }

        /// <summary>
        /// 	Responsible for providing a fluent method to apply boolean logic against a reference type, *and then invert it*.
        /// </summary>
        public static bool DoesNot<T>(this T o, Func<T, bool> a) where T : class
        {
            return !a(o);
        }
    }
}