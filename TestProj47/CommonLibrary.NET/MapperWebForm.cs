using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace HSNXT.ComLib.MapperSupport
{
    /// <summary>
    /// Mapper for web forms. This is used to automatically map values from a form into an objects Properties. This built in
    /// UpdateModel method on an MVC Controller doesn't support use of Interfaces for automapping.
    /// </summary>
    public class MapperWebForms
    {

        /// <summary>
        /// Maps form values from a NameValueCollection to an object. This is used specifically for auto mapping Form input values to an Object's properties.
        /// </summary>
        /// <param name="obj">The object to set the values on from the form.</param>
        /// <param name="form">Data from webform.</param>
        /// <param name="prefix">The prefix to use for the property names in the form.</param>
        /// <param name="excludeProps">The props to exclude.</param>
        public static void UpdateModel(object obj, NameValueCollection form, string prefix, IDictionary<string, string> excludeProps)
        {
            // 1. Only process public / settable properties
            var flags = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance;
            var props = obj.GetType().GetProperties(flags);

            foreach(var prop in props)
            {
                try
                {
                    // 2. The name of the form input should be the same as the property name
                    //    Unless a prefix is supplied.
                    //    e.g. "Title" or "<prefix>.Title" as in "Post.Title"
                    var name = prop.Name;
                    var formname = string.IsNullOrEmpty(prefix) ? name : prefix + "." + name;
                    var val = form[formname];

                    // 3. Now check that either 
                    //    1. property being processed is excluded from processing
                    //    2. form input value does exist at all for this property ( val != null ).
                    if (excludeProps == null || !excludeProps.ContainsKey(prop.Name) && val != null)
                    {
                        // 4. Allow the mapper helper to try to convert the value.
                        //    This is better because it uses conversion helpers to accomplish this.
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(obj, val, null);
                        }
                        else if (prop.PropertyType == typeof(bool) && val != null)
                        {
                            if (!string.IsNullOrEmpty(val) && val.Contains(","))
                                val = val.Substring(0, val.IndexOf(","));
                                                        
                            MapperHelper.SetProperty(prop, obj, -1, null, val);
                        }
                        else if (prop.PropertyType == typeof(TimeSpan))
                        {
                            MapperHelper.SetProperty(prop, obj, -1, null, val);
                        }
                        else if (TypeHelper.IsBasicType(prop.PropertyType) && val != null)
                        {
                            MapperHelper.SetProperty(prop, obj, -1, null, val);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
