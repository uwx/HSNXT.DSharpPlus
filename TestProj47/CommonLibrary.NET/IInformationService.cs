using System;

namespace HSNXT.ComLib.Information
{
    /// <summary>
    /// Information Service
    /// </summary>
    public interface IInformationService : IExtensionService<InfoAttribute, IInformation>
    {
        /// <summary>
        /// Gets the information task after validating that the user has access to it.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="authenticate"></param>
        /// <param name="authenticator"></param>
        /// <returns></returns>
        BoolMessageItem<KeyValue<InfoAttribute, IInformation>> GetInfoTask(string name, bool authenticate, Func<string, bool> authenticator);
    }
}
