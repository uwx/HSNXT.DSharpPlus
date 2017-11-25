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
        public static bool IsGreaterThanZero(this TimeSpan t)
        {
            return t > TimeSpan.Zero;
        }

        public static DateTime Hence(this TimeSpan t)
        {
            return DateTime.Now + t;
        }

        public static DateTime Before(this TimeSpan t, DateTime d)
        {
            return d - t;
        }

        public static DateTime After(this TimeSpan t, DateTime d)
        {
            return d + t;
        }

        public static TimeSpan ShorterThan(this TimeSpan t1, TimeSpan t2)
        {
            return t2 - t1;
        }

        public static TimeSpan LongerThan(this TimeSpan t1, TimeSpan t2)
        {
            return t2 + t1;
        }
    }
}