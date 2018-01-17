using System;
using System.Security.Permissions;

namespace HSNXT
{
#if !NetFX
    public class EnvironmentPermissionAttribute : Attribute
    {
        public EnvironmentPermissionAttribute(SecurityAction demand)
        {
        }

        public bool Unrestricted
        {
            get;
            set;
        }
    }
#endif
}