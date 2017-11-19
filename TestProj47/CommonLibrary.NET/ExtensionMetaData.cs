﻿/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace HSNXT.ComLib
{
    /// <summary>
    /// This class contains information about an extension.
    /// </summary>
    public class ExtensionMetaData
    {
        /// <summary>
        /// Extension id.
        /// </summary>
        public string Id;


        /// <summary>
        /// Extension instance.
        /// </summary>
        public object Instance;       


        /// <summary>
        /// Extension attribute.
        /// </summary>
        public ExtensionAttribute Attribute;


        /// <summary>
        /// List of additional attributes.
        /// </summary>
        public List<object> AdditionalAttributes = new List<object>();


        /// <summary>
        /// Data type.
        /// </summary>
        public Type DataType;


        /// <summary>
        /// Lamda associated with extension.
        /// </summary>
        public object Lamda;
    }
}
