﻿using System;
using System.Collections;

namespace HSNXT.ComLib.EmailSupport
{
    /// <summary>
    /// Helper class for emails.
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// Create the email service settings from the configuration source provided.
        /// </summary>
        /// <param name="config">The Configuration source.</param>
        /// <param name="emailServiceSectionName">The name of the section in the config source
        /// containing the email service settings.</param>
        /// <param name="settingsCreator">Lamda to option create the settings object.</param>
        /// <returns></returns>
        public static IEmailSettings GetSettings(IDictionary config, string emailServiceSectionName, Func<IEmailSettings> settingsCreator = null)
        {
            var settings = settingsCreator == null ? new EmailServiceSettings() : settingsCreator();
            settings.IsAuthenticationRequired = GetOrDefault(config, emailServiceSectionName, "IsAuthenticationRequired", false);
            settings.AuthenticationUserName = GetOrDefault(config, emailServiceSectionName, "AuthenticationUserName", "");
            settings.AuthenticationPassword = GetOrDefault(config, emailServiceSectionName, "AuthenticationPassword", "");
            settings.SmptServer = GetOrDefault(config, emailServiceSectionName, "SmptServer", "");
            settings.UsePort = GetOrDefault(config, emailServiceSectionName, "UsePort", false);
            settings.Port = settings.UsePort ? GetOrDefault(config, emailServiceSectionName, "Port", 25) : 25;
            settings.From = GetOrDefault(config, emailServiceSectionName, "From", "");
            return settings;
        }


        /// <summary>
        /// Get the value from the dictionary using the section/keys specified, and if not available, return the defaultvalue supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static T GetOrDefault<T>(IDictionary config, string sectionName, string key, T defaultValue)
        {
            if (config == null) return defaultValue;
            if (!config.Contains(sectionName)) return defaultValue;
            var section = config[sectionName] as IDictionary;
            if (!section.Contains(key)) return defaultValue;
            var val = section[key];
            if (val == null) return default;
            var converted = Converter.ConvertTo<T>(val);
            return converted;
        }
    }
}
