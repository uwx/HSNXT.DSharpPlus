using System;
using System.Reflection;
//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System;
//using System.Reflection;

namespace TestProj47
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, and the
        ///     type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
        {
            return Attribute.GetCustomAttribute(element, attributeType);
        }

        /// <summary>
        ///     Retrieves a custom attribute applied to a method parameter. Parameters specify the method parameter, the type
        ///     of the custom attribute to search for, and whether to search ancestors of the method parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     A reference to the single custom attribute of type  that is applied to , or null if there is no such
        ///     attribute.
        /// </returns>
        public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
        {
            return Attribute.GetCustomAttribute(element, attributeType, inherit);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. A parameter specifies the method
        ///     parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <returns>
        ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
        ///     exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this ParameterInfo element)
        {
            return Attribute.GetCustomAttributes(element);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, and the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this ParameterInfo element, Type attributeType)
        {
            return Attribute.GetCustomAttributes(element, attributeType);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, the type of the custom attribute to search for, and whether to search ancestors of the method
        ///     parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
        ///     attributes exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
        {
            return Attribute.GetCustomAttributes(element, attributeType, inherit);
        }

        /// <summary>
        ///     Retrieves an array of the custom attributes applied to a method parameter. Parameters specify the method
        ///     parameter, and whether to search ancestors of the method parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>
        ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
        ///     exist.
        /// </returns>
        public static Attribute[] GetCustomAttributes(this ParameterInfo element, bool inherit)
        {
            return Attribute.GetCustomAttributes(element, inherit);
        }
    }

    public static partial class Extensions
    {
        /// <summary>
        ///     Determines whether any custom attributes are applied to a method parameter. Parameters specify the method
        ///     parameter, and the type of the custom attribute to search for.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static bool IsDefined(this ParameterInfo element, Type attributeType)
        {
            return Attribute.IsDefined(element, attributeType);
        }

        /// <summary>
        ///     Determines whether any custom attributes are applied to a method parameter. Parameters specify the method
        ///     parameter, the type of the custom attribute to search for, and whether to search ancestors of the method
        ///     parameter.
        /// </summary>
        /// <param name="element">An object derived from the  class that describes a parameter of a member of a class.</param>
        /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
        /// <param name="inherit">If true, specifies to also search the ancestors of  for custom attributes.</param>
        /// <returns>true if a custom attribute of type  is applied to ; otherwise, false.</returns>
        public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
        {
            return Attribute.IsDefined(element, attributeType, inherit);
        }
    }
}