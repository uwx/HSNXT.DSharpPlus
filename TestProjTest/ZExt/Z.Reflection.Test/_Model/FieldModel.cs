﻿// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;

namespace HSNXT.Z.Reflection.Test
{
    public class FieldModel<T>
    {
        #region Modifier

        public const int ConstField = 1;
        public static int StaticField;
        public readonly int ReadOnlyField = 1;
        public volatile int VolatileField = 1;
        public event EventHandler EventField;

        #endregion

        #region Visibility

        internal int InternalField;
        protected internal int ProtectedInternalField;
        private int PrivateField;
        protected int ProtectedField;
        public int PublicField;

        #endregion

        #region Generic

        public T GenericField;

        #endregion
    }
}