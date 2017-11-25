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
using System.Collections.Generic;
using System.Linq;

namespace HSNXT
{
    public static partial class Extensions
    {
        public static bool HasExactlyOne<TType, TCollection>(this TType t, Func<TType, IEnumerable<TCollection>> func)
            where TType : Type
        {
            return Has(t, 1, func);
        }

        public static bool HasNo<TType, TCollection>(this TType t, Func<TType, IEnumerable<TCollection>> func)
            where TType : Type
        {
            return Has(t, 0, func);
        }

        public static bool Has<TType, TCollection>(this TType t, int n, Func<TType, IEnumerable<TCollection>> func)
            where TType : Type
        {
            return func(t).Count() == n;
        }

        public static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}