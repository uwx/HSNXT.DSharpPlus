// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright � ZZZ Projects Inc. 2014 - 2016. All rights reserved.
using System;
using System.Reflection;

public static partial class Extensions
{
    /// <summary>
    ///     Retrieves an array of the custom attributes applied to a module. Parameters specify the module, and the type
    ///     of the custom attribute to search for.
    /// </summary>
    /// <param name="element">An object derived from the  class that describes a portable executable file.</param>
    /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
    /// <returns>
    ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
    ///     attributes exist.
    /// </returns>
    public static Attribute[] GetCustomAttributes(this Module element, Type attributeType)
    {
        return Attribute.GetCustomAttributes(element, attributeType);
    }

    /// <summary>
    ///     Retrieves an array of the custom attributes applied to a module. A parameter specifies the module.
    /// </summary>
    /// <param name="element">An object derived from the  class that describes a portable executable file.</param>
    /// <returns>
    ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
    ///     exist.
    /// </returns>
    public static Attribute[] GetCustomAttributes(this Module element)
    {
        return Attribute.GetCustomAttributes(element);
    }

    /// <summary>
    ///     Retrieves an array of the custom attributes applied to a module. Parameters specify the module, and an
    ///     ignored search option.
    /// </summary>
    /// <param name="element">An object derived from the  class that describes a portable executable file.</param>
    /// <param name="inherit">This parameter is ignored, and does not affect the operation of this method.</param>
    /// <returns>
    ///     An  array that contains the custom attributes applied to , or an empty array if no such custom attributes
    ///     exist.
    /// </returns>
    public static Attribute[] GetCustomAttributes(this Module element, Boolean inherit)
    {
        return Attribute.GetCustomAttributes(element, inherit);
    }

    /// <summary>
    ///     Retrieves an array of the custom attributes applied to a module. Parameters specify the module, the type of
    ///     the custom attribute to search for, and an ignored search option.
    /// </summary>
    /// <param name="element">An object derived from the  class that describes a portable executable file.</param>
    /// <param name="attributeType">The type, or a base type, of the custom attribute to search for.</param>
    /// <param name="inherit">This parameter is ignored, and does not affect the operation of this method.</param>
    /// <returns>
    ///     An  array that contains the custom attributes of type  applied to , or an empty array if no such custom
    ///     attributes exist.
    /// </returns>
    public static Attribute[] GetCustomAttributes(this Module element, Type attributeType, Boolean inherit)
    {
        return Attribute.GetCustomAttributes(element, attributeType, inherit);
    }
}