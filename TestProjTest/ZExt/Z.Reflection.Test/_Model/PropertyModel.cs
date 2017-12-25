﻿// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.
namespace HSNXT.Z.Reflection.Test
{
    public class PropertyModel<T> : AbstractPropertyModel
    {
        #region Modifier

        public static int StaticProperty { get; set; }
        public override int OverrideProperty { get; set; }
        public virtual int VirtualProperty { get; set; }

        #endregion

        #region Visibility

        internal int InternalProperty { get; set; }
        private int PrivateProperty { get; set; }
        protected int ProtectedProperty { get; set; }
        protected internal int ProtectedInternalProperty { get; set; }
        public int PublicProperty { get; set; }

        public int PublicGetterPrivateSetterProperty { get; private set; }
        public int PrivateGetterPublicSetterProperty { private get;  set; }

        #endregion

        #region Method

        public int GetterProperty
        {
            get { return _helper; }
        }

        public int SetterProperty
        {
            set { _helper = value; }
        }

        #endregion

        #region Type

        public int this[int param1, int param2, int param3]
        {
            get { return param1; }
            set { _helper = value; }
        }

        #endregion

        #region Generic

        public T GenericProperty { get; set; }

        public T this[T param1, int param2]
        {
            get { return param1; }
        }

        #endregion

        private int _helper;
        public override int AbstractProperty { get; set; }
    }

    public abstract class AbstractPropertyModel
    {
        public abstract int AbstractProperty { get; set; }
        public  virtual int OverrideProperty { get; set; }
    }
}