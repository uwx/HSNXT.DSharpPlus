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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static IDictionary ToDictionary<TEnumValueType>(this Enum e)
        {
            if (typeof(TEnumValueType).FullName != Enum.GetUnderlyingType(e.GetType()).FullName)
            {
                throw new ArgumentException("Invalid type specified.");
            }

            return Enum.GetValues(e.GetType())
                .Cast<object>()
                .ToDictionary(key => Enum.GetName(e.GetType(), key),
                    value => (TEnumValueType) value);
        }

        public static IEnumerable<T> All<T>(this T e) where T : struct, IConvertible
        {
            return Enum.GetValues(e.GetType()).Cast<T>();
        }
    }
}