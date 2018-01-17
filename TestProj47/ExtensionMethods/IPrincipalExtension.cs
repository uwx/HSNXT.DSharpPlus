// Decompiled with JetBrains decompiler
// Type: ExtensionMethods.IPrincipalExtension
// Assembly: ExtensionMethods, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A78E21D-808B-4A21-BFAA-D781C27D2CD5
// Assembly location: ...\bin\Debug\ExtensionMethods.dll

using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>Returns the user name without a domain name.</summary>
        /// <param name="user">IPrincipal</param>
        /// <returns>user name.</returns>
        public static string GetUserNameOnly(this IPrincipal user)
        {
            if (string.IsNullOrWhiteSpace(user.Identity.Name))
                return user.Identity.Name;
            var str = user.Identity.Name;
            var num = str.IndexOf('\\');
            if (num > -1)
                str = str.Substring(num + 1);
            return str;
        }

        /// <summary>
        /// Determines if a IPrincipal belongs to at least one of the specified roles.
        /// </summary>
        /// <param name="user">IPrincipal.</param>
        /// <param name="roles">An array of roles.</param>
        /// <returns>true if the IPrincipal is at least in one of the specified roles; otherwise, false.</returns>
        public static bool IsInAnyRole(this IPrincipal user, params string[] roles)
        {
            return user.IsInAnyRole((IEnumerable<string>) roles);
        }

        private static bool IsInAnyRole(this IPrincipal user, IEnumerable<string> roles)
        {
            return roles.Any(user.IsInRole);
        }

        /// <summary>
        /// Determines if a IPrincipal belongs to all the specified roles.
        /// </summary>
        /// <param name="user">IPrincipal.</param>
        /// <param name="roles">An array of roles.</param>
        /// <returns>true if the IPrincipal is in all the specified roles; otherwise, false.</returns>
        public static bool IsInAllRoles(this IPrincipal user, params string[] roles)
        {
            return user.IsInAllRoles((IEnumerable<string>) roles);
        }

        private static bool IsInAllRoles(this IPrincipal user, IEnumerable<string> roles)
        {
            return roles.All(role => user.IsInRole(role));
        }
    }
}