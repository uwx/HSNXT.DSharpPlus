using System;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Globalization;
using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Data.Entity.Design.PluralizationServices;
using System.Security;
using System.Xml.Linq;
using System.Xml;
using System.Collections.ObjectModel;
using System.Data.Common;
//using System.Data.SqlServerCe;
using System.Drawing.Drawing2D;
using System.Security.AccessControl;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Web.UI;
using System.Windows.Forms;
// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.Linq;
//using System.Reflection;
//using System.Text;

namespace TestProj47
{
    public static partial class Extensions
    {
        public static string GetDeclaraction(this ConstructorInfo @this)
        {
            // Example: [Visibility] [Modifier] [Type] [Name] [<GenericArguments] ([Parameters])
            var sb = new StringBuilder();

            // Visibility
            if (@this.IsPublic)
            {
                sb.Append("public ");
            }
            else if (@this.IsFamily)
            {
                sb.Append("protected ");
            }
            else if (@this.IsAssembly)
            {
                sb.Append("internal ");
            }
            else if (@this.IsPrivate)
            {
                sb.Append("private ");
            }
            else
            {
                sb.Append("protected internal ");
            }

            // Name
            sb.Append(@this.ReflectedType.IsGenericType
                ? @this.ReflectedType.Name.Substring(0, @this.ReflectedType.Name.IndexOf('`'))
                : @this.ReflectedType.Name);

            // Parameters
            ParameterInfo[] parameters = @this.GetParameters();
            sb.Append("(");
            sb.Append(string.Join(", ", parameters.Select(x => x.GetDeclaraction())));
            sb.Append(")");

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        public static string GetDeclaraction(this EventInfo @this)
        {
            return null;
        }
    }

    public static partial class Extensions
    {
        /// <summary>A FieldInfo extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this FieldInfo @this)
        {
            // Example: [Visibility] [Modifier] [Type] [Name] [PostModifier];
            var sb = new StringBuilder();

            // Variable
            bool isConstant = false;
            Type[] requiredTypes = @this.GetRequiredCustomModifiers();

            // Visibility
            if (@this.IsPublic)
            {
                sb.Append("public ");
            }
            else if (@this.IsPrivate)
            {
                sb.Append("private ");
            }
            else if (@this.IsFamily)
            {
                sb.Append("protected ");
            }
            else if (@this.IsAssembly)
            {
                sb.Append("internal ");
            }
            else
            {
                sb.Append("protected internal ");
            }

            // Modifier
            if (@this.IsStatic)
            {
                if (@this.IsLiteral)
                {
                    isConstant = true;
                    sb.Append("const ");
                }
                else
                {
                    sb.Append("static ");
                }
            }
            else if (@this.IsInitOnly)
            {
                sb.Append("readonly ");
            }
            if (requiredTypes.Any(x => x == typeof(IsVolatile)))
            {
                sb.Append("volatile ");
            }

            // Type
            sb.Append(@this.FieldType.GetShortDeclaraction());
            sb.Append(" ");

            // Name
            sb.Append(@this.Name);

            // PostModifier
            if (isConstant)
            {
                sb.Append(" = " + @this.GetRawConstantValue());
            }

            // End
            sb.Append(";");

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>A MemberInfo extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this MemberInfo @this)
        {
            switch (@this.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo) @this).GetDeclaraction();
                case MemberTypes.Property:
                    return ((PropertyInfo) @this).GetDeclaraction();
                case MemberTypes.Constructor:
                    return ((ConstructorInfo) @this).GetDeclaraction();
                case MemberTypes.Method:
                    return ((MethodInfo) @this).GetDeclaraction();
                case MemberTypes.TypeInfo:
                    return ((Type) @this).GetDeclaraction();
                case MemberTypes.NestedType:
                    return ((Type) @this).GetDeclaraction();
                case MemberTypes.Event:
                    return ((EventInfo) @this).GetDeclaraction();
                default:
                    return null;
            }
        }
    }

    public static partial class Extensions
    {
        /// <summary>A MethodInfo extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this MethodInfo @this)
        {
            // Example: [Visibility] [Modifier] [Type] [Name] [<GenericArguments] ([Parameters])
            var sb = new StringBuilder();

            // Visibility
            if (@this.IsPublic)
            {
                sb.Append("public ");
            }
            else if (@this.IsFamily)
            {
                sb.Append("protected ");
            }
            else if (@this.IsAssembly)
            {
                sb.Append("internal ");
            }
            else if (@this.IsPrivate)
            {
                sb.Append("private ");
            }
            else
            {
                sb.Append("protected internal ");
            }

            // Modifier
            if (@this.IsAbstract)
            {
                sb.Append("abstract ");
            }
            else if (@this.GetBaseDefinition().DeclaringType != @this.DeclaringType)
            {
                sb.Append("override ");
            }
            else if (@this.IsVirtual)
            {
                sb.Append("virtual ");
            }
            else if (@this.IsStatic)
            {
                sb.Append("static ");
            }

            // Type
            sb.Append(@this.ReturnType.GetShortDeclaraction());
            sb.Append(" ");

            // Name
            sb.Append(@this.Name);

            if (@this.IsGenericMethod)
            {
                sb.Append("<");

                Type[] arguments = @this.GetGenericArguments();

                sb.Append(string.Join(", ", arguments.Select(x => x.GetShortDeclaraction())));

                sb.Append(">");
            }

            // Parameters
            ParameterInfo[] parameters = @this.GetParameters();
            sb.Append("(");
            sb.Append(string.Join(", ", parameters.Select(x => x.GetDeclaraction())));
            sb.Append(")");

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>A ParameterInfo extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this ParameterInfo @this)
        {
            var sb = new StringBuilder();

            @this.GetDeclaraction(sb);
            return sb.ToString();
        }

        internal static void GetDeclaraction(this ParameterInfo @this, StringBuilder sb)
        {
            // retval attribute
            var attributes = new List<string>();

            string typeName;
            Type elementType = @this.ParameterType.GetElementType();

            if (elementType != null)
            {
                typeName = @this.ParameterType.Name.Replace(elementType.Name, elementType.GetShortDeclaraction());
            }
            else
            {
                typeName = @this.ParameterType.GetShortDeclaraction();
            }


            if (Attribute.IsDefined(@this, typeof(ParamArrayAttribute)))
            {
                sb.Append("params ");
            }
            else if (@this.Position == 0 && @this.Member.IsDefined(typeof(ExtensionAttribute)))
            {
                sb.Append("this ");
            }

            if (@this.IsIn)
            {
                attributes.Add("In");
            }
            if (@this.IsOut)
            {
                if (typeName.Contains("&"))
                {
                    typeName = typeName.Replace("&", "");
                    sb.Append("out ");
                }
                else
                {
                    attributes.Add("Out");
                }
            }
            else if (@this.ParameterType.IsByRef)
            {
                typeName = typeName.Replace("&", "");
                sb.Append("ref ");
            }
            sb.Append(typeName);

            sb.Append(" ");
            sb.Append(@this.Name);

            if (@this.IsOptional)
            {
                if (@this.DefaultValue != Missing.Value)
                {
                    sb.Append(" = " + @this.DefaultValue);
                }
                else
                {
                    attributes.Add("Optional");
                }
            }


            string attribute = attributes.Count > 0 ? "[" + string.Join(", ", attributes) + "] " : "";
            sb.Insert(0, attribute);
        }
    }

    public static partial class Extensions
    {
        /// <summary>A PropertyInfo extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this PropertyInfo @this)
        {
            // Example: [Visibility] [Modifier] [Type] [Name | Indexer] { [VisibilityGetter] [Getter]; [VisibilitySetter] [Setter]; }
            var sb = new StringBuilder();

            // Variable
            bool canRead = @this.CanRead;
            bool canWrite = @this.CanWrite;
            int readLevel = 9;
            int writeLevel = 9;
            string readVisibility = "";
            string writeVisibility = "";

            // Variable Method
            bool isAbstract = false;
            bool isOverride = false;
            bool isStatic = false;
            bool isVirtual = false;

            // Set visibility
            if (canRead)
            {
                MethodInfo method = @this.GetGetMethod(true);
                isAbstract = method.IsAbstract;
                isOverride = method.GetBaseDefinition().DeclaringType != method.DeclaringType;
                isStatic = method.IsStatic;
                isVirtual = method.IsVirtual;

                if (method.IsPublic)
                {
                    readLevel = 1;
                    readVisibility = "public ";
                }
                else if (method.IsFamily)
                {
                    readLevel = 2;
                    readVisibility = "protected ";
                }
                else if (method.IsAssembly)
                {
                    readLevel = 3;
                    readVisibility = "internal ";
                }
                else if (method.IsPrivate)
                {
                    readLevel = 5;
                    readVisibility = "private ";
                }
                else
                {
                    readLevel = 4;
                    readVisibility = "protected internal ";
                }
            }
            if (canWrite)
            {
                MethodInfo method = @this.GetSetMethod(true);

                if (readLevel != 9)
                {
                    isAbstract = method.IsAbstract;
                    isOverride = method.GetBaseDefinition().DeclaringType != method.DeclaringType;
                    isStatic = method.IsStatic;
                    isVirtual = method.IsVirtual;
                }

                if (method.IsPublic)
                {
                    writeLevel = 1;
                    writeVisibility = "public ";
                }
                else if (method.IsFamily)
                {
                    writeLevel = 2;
                    writeVisibility = "protected ";
                }
                else if (method.IsAssembly)
                {
                    writeLevel = 3;
                    writeVisibility = "internal ";
                }
                else if (method.IsPrivate)
                {
                    writeLevel = 5;
                    writeVisibility = "private ";
                }
                else
                {
                    writeLevel = 4;
                    writeVisibility = "protected internal ";
                }
            }

            // Visibility
            if (canRead && canWrite)
            {
                sb.Append(readLevel <= writeLevel ? readVisibility : writeVisibility);
            }
            else if (canRead)
            {
                sb.Append(readVisibility);
            }
            else
            {
                sb.Append(writeVisibility);
            }

            // Modifier
            if (isAbstract)
            {
                sb.Append("abstract ");
            }
            else if (isOverride)
            {
                sb.Append("override ");
            }
            else if (isVirtual)
            {
                sb.Append("virtual ");
            }
            else if (isStatic)
            {
                sb.Append("static ");
            }

            // Type
            sb.Append(@this.PropertyType.GetShortDeclaraction());
            sb.Append(" ");

            // [Name | Indexer]
            ParameterInfo[] indexerParameter = @this.GetIndexParameters();
            if (indexerParameter.Length == 0)
            {
                // Name
                sb.Append(@this.Name);
            }
            else
            {
                // Indexer
                sb.Append("this");
                sb.Append("[");
                sb.Append(string.Join(", ",
                    indexerParameter.Select(x => x.ParameterType.GetShortDeclaraction() + " " + x.Name)));
                sb.Append("]");
            }
            sb.Append(" ");

            sb.Append("{ ");
            // Getter
            if (@this.CanRead)
            {
                if (readLevel > writeLevel)
                {
                    sb.Append(readVisibility);
                }
                sb.Append("get; ");
            }

            // Setter
            if (@this.CanWrite)
            {
                if (writeLevel > readLevel)
                {
                    sb.Append(writeVisibility);
                }
                sb.Append("set; ");
            }
            sb.Append("}");

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>A Type extension method that gets a declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The declaraction.</returns>
        public static string GetDeclaraction(this Type @this)
        {
            // Example: [Visibility] [Modifier] [Type] [Name] [<GenericArguments>] [:] [Inherited Class] [Inherited Interface]
            var sb = new StringBuilder();

            // Variable
            bool hasInheritedClass = false;

            // Visibility
            sb.Append(@this.IsPublic ? "public " : "internal ");

            // Modifier
            if (!@this.IsInterface && @this.IsAbstract)
            {
                sb.Append(@this.IsSealed ? "static " : "abstract ");
            }

            // Type
            sb.Append(@this.IsInterface ? "interface " : "class ");

            // Name
            sb.Append(@this.IsGenericType ? @this.Name.Substring(0, @this.Name.IndexOf('`')) : @this.Name);

            List<string> constraintType = new List<string>();

            // GenericArguments
            if (@this.IsGenericType)
            {
                Type[] arguments = @this.GetGenericArguments();
                sb.Append("<");
                sb.Append(string.Join(", ", arguments.Select(x =>
                {
                    GenericParameterAttributes sConstraints = x.GenericParameterAttributes;

                    if (GenericParameterAttributes.None != (sConstraints & GenericParameterAttributes.Contravariant))
                    {
                        sb.Append("in ");
                    }
                    if (GenericParameterAttributes.None != (sConstraints & GenericParameterAttributes.Covariant))
                    {
                        sb.Append("out ");
                    }

                    List<string> parameterConstraint = new List<string>();

                    if (GenericParameterAttributes.None !=
                        (sConstraints & GenericParameterAttributes.ReferenceTypeConstraint))
                    {
                        parameterConstraint.Add("class");
                    }


                    if (GenericParameterAttributes.None !=
                        (sConstraints & GenericParameterAttributes.DefaultConstructorConstraint))
                    {
                        parameterConstraint.Add("new()");
                    }


                    if (parameterConstraint.Count > 0)
                    {
                        constraintType.Add(x.Name + " : " + string.Join(", ", parameterConstraint));
                    }

                    return x.GetShortDeclaraction();
                })));
                sb.Append(">");

                foreach (var argument in arguments)
                {
                    GenericParameterAttributes sConstraints =
                        argument.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
                }
            }

            List<string> constaints = new List<string>();

            // Inherited Class
            if (@this.BaseType != null && @this.BaseType != typeof(object))
            {
                constaints.Add(@this.BaseType.GetShortDeclaraction());
            }

            // Inherited Interface
            Type[] interfaces = @this.GetInterfaces();
            if (interfaces.Length > 0)
            {
                constaints.AddRange(interfaces.Select(x => x.Name));
            }

            if (constaints.Count > 0)
            {
                sb.Append(" : ");
                sb.Append(string.Join(", ", constaints));
            }

            if (constraintType.Count > 0)
            {
                sb.Append(" where ");
                sb.Append(string.Join(", ", constraintType));
            }

            return sb.ToString();
        }
    }

    public static partial class Extensions
    {
        /// <summary>A Type extension method that gets short declaraction.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The short declaraction.</returns>
        internal static string GetShortDeclaraction(this Type @this)
        {
            if (@this == typeof(bool))
            {
                return "bool";
            }
            if (@this == typeof(byte))
            {
                return "byte";
            }
            if (@this == typeof(char))
            {
                return "char";
            }
            if (@this == typeof(decimal))
            {
                return "decimal";
            }
            if (@this == typeof(double))
            {
                return "double";
            }
            if (@this == typeof(Enum))
            {
                return "enum";
            }
            if (@this == typeof(float))
            {
                return "float";
            }
            if (@this == typeof(int))
            {
                return "int";
            }
            if (@this == typeof(long))
            {
                return "long";
            }
            if (@this == typeof(object))
            {
                return "object";
            }
            if (@this == typeof(sbyte))
            {
                return "sbyte";
            }
            if (@this == typeof(short))
            {
                return "short";
            }
            if (@this == typeof(string))
            {
                return "string";
            }
            if (@this == typeof(uint))
            {
                return "uint";
            }
            if (@this == typeof(ulong))
            {
                return "ulong";
            }
            if (@this == typeof(ushort))
            {
                return "ushort";
            }

            return @this.Name;
        }
    }
}