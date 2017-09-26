// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

using System.Diagnostics.CodeAnalysis;

[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object)", Scope = "member",
        Target = "ExtensionOverflow.StringExtensions.#FormatWith(System.String,System.Object)")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object,System.Object)", Scope = "member",
        Target = "ExtensionOverflow.StringExtensions.#FormatWith(System.String,System.Object,System.Object)")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)", Scope = "member",
        Target =
            "ExtensionOverflow.StringExtensions.#FormatWith(System.String,System.Object,System.Object,System.Object)")]
[assembly:
    SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider",
        MessageId = "System.String.Format(System.String,System.Object[])", Scope = "member",
        Target = "ExtensionOverflow.StringExtensions.#FormatWith(System.String,System.Object[])")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserialise",
        Scope = "member", Target = "ExtensionOverflow.StringExtensions.#XmlDeserialise`1(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Serialise",
        Scope = "member", Target = "ExtensionOverflow.StringExtensions.#XmlSerialise`1(!!0)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member",
        Target = "ExtensionOverflow.StringExtensions.#XmlDeserialise`1(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member",
        Target = "ExtensionOverflow.StringExtensions.#XmlDeserialize`1(System.String)")]
[assembly:
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Serialise",
        Scope = "member", Target = "ExtensionOverflow.StringExtensions.#XmlSerialize`1(!!0)")]