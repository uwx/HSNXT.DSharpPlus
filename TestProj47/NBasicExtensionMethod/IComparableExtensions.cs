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
        public static bool IsLessThan<T>(this T c, T o) where T : IComparable
        {
            return c.CompareTo(o) < 0;
        }

        public static bool IsLessThanOrEqualTo<T>(this T c, T o) where T : IComparable
        {
            return c.CompareTo(o) <= 0;
        }

        public static bool IsGreaterThan<T>(this T c, T o) where T : IComparable
        {
            return c.CompareTo(o) > 0;
        }

        public static bool IsGreaterThanOrEqualTo<T>(this T c, T o) where T : IComparable
        {
            return c.CompareTo(o) >= 0;
        }

        public static bool Is<T>(this T c, T o) where T : IComparable
        {
            return c.ValueEquals(o);
        }

        public static bool ValueEquals<T>(this T c, T o) where T : IComparable
        {
            return c.CompareTo(o) == 0;
        }

        public static bool IsBetween<T>(this T value, T lowerBound, T upperBound) where T : IComparable
        {
            return value.CompareTo(lowerBound) > 0 && value.CompareTo(upperBound) < 0;
        }

        public static bool IsBetweenInclusive<T>(this T value, T lowerBound, T upperBound) where T : IComparable
        {
            return value.CompareTo(lowerBound) >= 0 && value.CompareTo(upperBound) <= 0;
        }
    }
}

// ReSharper restore InconsistentNaming