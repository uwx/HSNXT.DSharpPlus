﻿//////////////////////////////////////////////////////////////////////
// BCLExtensions is (c) 2010 Solutions Design. All rights reserved.
// http://www.sd.nl
//////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c) 2010 Solutions Design. All rights reserved.
// 
// The BCLExtensions library sourcecode and its accompanying tools, tests and support code
// are released under the following license: (BSD2)
// ----------------------------------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////

using System;
using System.Linq;
using System.Reflection;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether the type this method is called on is a nullable type of type Nullable(Of T)
        /// </summary>
        /// <param name="toCheck">The type to check.</param>
        /// <returns>true if toCheck is a Nullable(Of T) type, otherwise false</returns>
        public static bool IsNullableValueType(this Type toCheck)
        {
            if (toCheck == null || !toCheck.IsValueType())
            {
                return false;
            }
            return toCheck.IsGenericType() && toCheck.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


        /// <summary>
        /// Gets the full type name, of the format: Type.Fullname, assembly name. 
        /// If the assembly is signed, the full assembly name is added, otherwise just the assembly name, not the version, public key token or culture.
        /// </summary>
        /// <param name="type">The type of which the full name should be obtained.</param>
        /// <returns>full type name. If the type is a .NET system type (e.g. is located in mscorlib or namespace starts with Microsoft. or System.) the 
        /// FullTypeName is equal to the FullName of the type.</returns>
        /// <remarks>Use this method if you need to store the type's full name in a string for re-instantiation later on with Activator.CreateInstance.</remarks>
        public static string GetFullTypeName(this Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            if (type.IsNetSystemType())
            {
                return type.FullName;
            }
            return type.GetAssembly().GetName().GetPublicKeyToken().Length <= 0
                ? $"{type.FullName}, {type.GetAssembly().GetName().Name}"
                : type.AssemblyQualifiedName;
            // Signed assembly, get the full name, including version and public key token.
        }


        /// <summary>
        /// Determines whether the type specified is a system type of .NET. System types are types in mscorlib, assemblies which start with 'Microsoft.', 'System.'
        /// or the System assembly itself. 
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsNetSystemType(this Type type)
        {
            if (type == null)
            {
                return false;
            }
            var nameToCheck = type.GetAssembly().GetName();
            var exceptions = new[] {"Microsoft.SqlServer.Types"};
            return new[] {"System", "mscorlib", "System.", "Microsoft."}
                   .Any(s => s.EndsWith(".") && nameToCheck.Name.StartsWith(s) || nameToCheck.Name == s) &&
                   !exceptions.Any(s => nameToCheck.Name.StartsWith(s));
        }


        /// <summary>
        /// Gets the default value for the type, e.g. 0 for int, empty guid for guid.
        /// </summary>
        /// <param name="typeToCreateValueFor">The type to create value for.</param>
        /// <param name="safeDefaults">if set to true, the routine will return string.Empty for string and empty byte array for byte[], otherwise null</param>
        /// <returns>
        /// the default value for the type. It returns string.Empty for string, empty byte array for a byte array,
        /// if safeDefaults is set to true
        /// </returns>
        public static object GetDefaultValue(this Type typeToCreateValueFor, bool safeDefaults)
        {
            var sourceType = typeToCreateValueFor;
            if (typeToCreateValueFor.IsNullableValueType())
            {
                sourceType = typeToCreateValueFor.GetGenericArgument(0);
            }
            object toReturn = null;
            if (sourceType.IsValueType())
            {
                // produce default value for value type. 
                toReturn = Array.CreateInstance(sourceType, 1).GetValue(0);
            }
            else
            {
                if (!safeDefaults) return null;
                switch (sourceType.Name)
                {
                    case "String":
                        toReturn = string.Empty;
                        break;
                    case "Byte[]":
                        toReturn = new byte[0];
                        break;
                }
            }
            return toReturn;
        }


        /// <summary>
        /// Helper method to work around the stupid breaking change in .NET Core regarding Type properties/methods
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static Type GetGenericArgument(this Type type, int index)
        {
#if (NETSTANDARD1_6)
			return type.GetTypeInfo().GetGenericArguments()[index];
#else
            return type.GetGenericArguments()[index];
#endif
        }


        /// <summary>
        /// Helper method to work around the stupid breaking change in .NET Core regarding Type properties/methods
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsValueType(this Type type)
        {
#if (NETSTANDARD1_6)
			return type.GetTypeInfo().IsValueType;
#else
            return type.IsValueType;
#endif
        }

        /// <summary>
        /// Helper method to work around the stupid breaking change in .NET Core regarding Type properties/methods
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsGenericType(this Type type)
        {
#if (NETSTANDARD1_6)
			return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        }

        /// <summary>
        /// Helper method to work around the stupid breaking change in .NET Core regarding Type properties/methods
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static Assembly GetAssembly(this Type type)
        {
#if (NETSTANDARD1_6)
			return type.GetTypeInfo().Assembly;
#else
            return type.Assembly;
#endif
        }
    }
}