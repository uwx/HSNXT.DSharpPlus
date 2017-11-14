﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17020
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TestProj47 {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class Strings {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    var temp = new ResourceManager("IntXLib.Utils.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alphabet characters must be unique..
        /// </summary>
        internal static string AlphabetRepeatingChars {
            get {
                return ResourceManager.GetString("AlphabetRepeatingChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Alphabet is too small to represent numbers in base {0}..
        /// </summary>
        internal static string AlphabetTooSmall {
            get {
                return ResourceManager.GetString("AlphabetTooSmall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operands can&apos;t be null..
        /// </summary>
        internal static string CantBeNull {
            get {
                return ResourceManager.GetString("CantBeNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t use null in comparsion operations..
        /// </summary>
        internal static string CantBeNullCmp {
            get {
                return ResourceManager.GetString("CantBeNullCmp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operand can&apos;t be null..
        /// </summary>
        internal static string CantBeNullOne {
            get {
                return ResourceManager.GetString("CantBeNullOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t compare with provided argument..
        /// </summary>
        internal static string CantCmp {
            get {
                return ResourceManager.GetString("CantCmp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Digit bytes array length is invalid..
        /// </summary>
        internal static string DigitBytesLengthInvalid {
            get {
                return ResourceManager.GetString("DigitBytesLengthInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FHT multiplication returned invalid result for IntX objects with lengths {0} and {1}..
        /// </summary>
        internal static string FhtMultiplicationError {
            get {
                return ResourceManager.GetString("FhtMultiplicationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to One of the operated big integers is too big..
        /// </summary>
        internal static string IntegerTooBig {
            get {
                return ResourceManager.GetString("IntegerTooBig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Base is invalid..
        /// </summary>
        internal static string ParseBaseInvalid {
            get {
                return ResourceManager.GetString("ParseBaseInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid character in input..
        /// </summary>
        internal static string ParseInvalidChar {
            get {
                return ResourceManager.GetString("ParseInvalidChar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No digits in string..
        /// </summary>
        internal static string ParseNoDigits {
            get {
                return ResourceManager.GetString("ParseNoDigits", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Digit is too big for this base..
        /// </summary>
        internal static string ParseTooBigDigit {
            get {
                return ResourceManager.GetString("ParseTooBigDigit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Base must be between 2 and 65536..
        /// </summary>
        internal static string ToStringSmallBase {
            get {
                return ResourceManager.GetString("ToStringSmallBase", resourceCulture);
            }
        }
    }
}
