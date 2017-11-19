using System;

namespace HSNXT.ComLib.Automation
{  
    /// <summary>
    /// Attribute used to define a command class used in automation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class CommandParameterAttribute : ExtensionArgAttribute
    {
    }
}
