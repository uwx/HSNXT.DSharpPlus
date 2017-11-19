
/*
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

using System.Security.Principal;

namespace HSNXT.ComLib.Authentication
{
    /// <summary>
    /// Custom prinical class with additional propertes to identity user.
    /// </summary>
    public class UserPrincipal : IPrincipal
    {
        #region Null values
        /// <summary>
        /// Empty/null value.
        /// </summary>
        private static readonly IPrincipal _empty = new UserPrincipal(-1, string.Empty, string.Empty,
                                           new UserIdentity(-1, string.Empty, string.Empty, false));

        /// <summary>
        /// Get the empty value.
        /// </summary>
        public static IPrincipal Empty => _empty;

        #endregion
        

        private int _userId;
        private string _userName;
        private string[] _userRoles;

        /// <summary>
        /// The user identity used for authentication.
        /// </summary>
        protected IIdentity _identity;


        /// <summary>
        /// Create a new default instance.
        /// </summary>
        public UserPrincipal() { }


        /// <summary>
        /// Create new instance using supplied user information.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userRolesDelimitedByComma"></param>
        /// <param name="identity"></param>
        public UserPrincipal(int userId, string userName, string userRolesDelimitedByComma, IIdentity identity)
        {            
            var roles = userRolesDelimitedByComma.Split(';');
            Init(userId, userName, roles, identity);
        }


        /// <summary>
        /// Create new instance using supplied user information.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userRolesDelimitedByComma"></param>
        /// <param name="authType"></param>
        /// <param name="isAuthenicated"></param>
        public UserPrincipal(int userId, string userName, string userRolesDelimitedByComma, string authType, bool isAuthenicated)
        {
            var roles = userRolesDelimitedByComma.Split(';');
            IIdentity identity = new UserIdentity(userId, userName, authType, isAuthenicated);
            Init(userId, userName, roles, identity);
        }


        /// <summary>
        /// Create new instance using supplied user information.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <param name="identity"></param>
        public UserPrincipal(int userId, string userName, string[] roles, IIdentity identity)
        {
            Init(userId, userName, roles, identity);
        }


        /// <summary>
        /// Identity of the principal. 
        /// </summary>
        public IIdentity Identity
        {
            get => _identity;
            set => _identity = value;
        }


        /// <summary>
        /// Id of the user.
        /// </summary>
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }


        /// <summary>
        /// Username.
        /// </summary>
        public string Name
        {
            get => _userName;
            set => _userName = value;
        }


        /// <summary>
        /// Comma delimited string of users roles.
        /// </summary>
        public string[] Roles
        {
            get => _userRoles;
            set => _userRoles = value;
        }


        /// <summary>
        /// Determines if this user is in the role supplied.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            foreach (var userRole in _userRoles)
            {
                if (userRole == role)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Initializes this instance with the specified parameters.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <param name="identity"></param>
        public void Init(int userId, string userName, string[] roles, IIdentity identity)
        {
            _userRoles = roles;
            _identity = identity;
            _userId = userId;
            _userName = userName;
        }
    }

}
