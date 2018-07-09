using System.Collections.Generic;
using System.Reflection;

namespace DSharpPlus.CommandsNext
{
    public static partial class CommandsNextUtilities
    {
        // GetMethods workaround for .NET Standard 1.1
        internal static IEnumerable<MethodInfo> GetAllMethods(TypeInfo ti)
        {
            var baseDefs = new HashSet<string>();
            while (true)
            {
                foreach (var m in ti.DeclaredMethods)
                {
                    if (!baseDefs.Add(m.ToString()))
                        continue;

                    yield return m;
                }

                var b = ti.BaseType;
                if (b == null) break;
                if (b == typeof(BaseCommandModule)) break;

                ti = b.GetTypeInfo();

                // only allow inheriting commands from abstract classes, since other types of classes will get picked up
                // as regular modules by CNext
                if (!ti.IsAbstract) break;
            }
        }
    }
}