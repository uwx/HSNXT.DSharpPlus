#if NetFX
using System.Web;
using System.Web.UI;

//using System.Data.SqlServerCe;

// Description: C# Extension Methods Library to enhances the .NET Framework by adding hundreds of new methods. It drastically increases developers productivity and code readability. Support C# and VB.NET
// Website & Documentation: https://github.com/zzzprojects/Z.ExtensionMethods
// Forum: https://github.com/zzzprojects/Z.ExtensionMethods/issues
// License: https://github.com/zzzprojects/Z.ExtensionMethods/blob/master/LICENSE
// More projects: http://www.zzzprojects.com/
// Copyright © ZZZ Projects Inc. 2014 - 2016. All rights reserved.

//using System.Web;

namespace HSNXT
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A HttpResponse extension method that redirects.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="urlFormat">The URL format.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        public static void Redirect(this HttpResponse @this, string urlFormat, params object[] values)
        {
            var url = string.Format(urlFormat, values);
            @this.Redirect(url, true);
        }

        /// <summary>
        ///     A HttpResponse extension method that reloads the given this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void Reload(this HttpResponse @this)
        {
            @this.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }

        /// <summary>
        ///     A HttpResponse extension method that sends an attachment.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fullPathToFile">The full path to file.</param>
        /// <param name="outputFileName">Filename of the output file.</param>
        public static void SendAttachment(this HttpResponse @this, string fullPathToFile, string outputFileName)
        {
            @this.Clear();
            @this.AddHeader("content-disposition", "attachment; filename=" + outputFileName);
            @this.WriteFile(fullPathToFile);
            @this.ContentType = "";
            @this.End();
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the status.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="code">The code.</param>
        public static void SetStatus(this HttpResponse @this, int code)
        {
            @this.StatusCode = code;
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the status.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="code">The code.</param>
        /// <param name="endResponse">true to end response.</param>
        public static void SetStatus(this HttpResponse @this, int code, bool endResponse)
        {
            @this.StatusCode = code;

            if (endResponse)
            {
                @this.End();
            }
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the status.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        public static void SetStatus(this HttpResponse @this, int code, string description)
        {
            @this.StatusCode = code;
            @this.StatusDescription = description;
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the status.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <param name="endResponse">true to end response.</param>
        public static void SetStatus(this HttpResponse @this, int code, string description, bool endResponse)
        {
            @this.StatusCode = code;
            @this.StatusDescription = description;

            if (endResponse)
            {
                @this.End();
            }
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 202 (Accepted.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusAccepted(this HttpResponse @this)
        {
            @this.StatusCode = 202;
            @this.StatusDescription = "Accepted.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 406 (Client browser does not accept the
        ///     MIME type of the requested page.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusClientBrowserDoesNotAcceptMimeType(this HttpResponse @this)
        {
            @this.StatusCode = 406;
            @this.StatusDescription = "Client browser does not accept the MIME type of the requested page.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 100 (Continue.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusContinue(this HttpResponse @this)
        {
            @this.StatusCode = 100;
            @this.StatusDescription = "Continue.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 201 (Created.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusCreated(this HttpResponse @this)
        {
            @this.StatusCode = 201;
            @this.StatusDescription = "Created.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 500 (Internal server error. ).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusInternalServerError(this HttpResponse @this)
        {
            @this.StatusCode = 500;
            @this.StatusDescription = "Internal server error. ";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 501 (Header values specify a
        ///     configuration that is not implemented.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusInvalidHeaderValueConfiguration(this HttpResponse @this)
        {
            @this.StatusCode = 501;
            @this.StatusDescription = "Header values specify a configuration that is not implemented.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 502 (Web server received an invalid
        ///     response while acting as a gateway or proxy. ).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusInvalidResponseWhileGatewayOrProxy(this HttpResponse @this)
        {
            @this.StatusCode = 502;
            @this.StatusDescription = "Web server received an invalid response while acting as a gateway or proxy. ";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 405 (Method Not Allowed.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusMethodNotAllowed(this HttpResponse @this)
        {
            @this.StatusCode = 405;
            @this.StatusDescription = "Method Not Allowed.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 301 (Moved permanently.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusMovedPermanently(this HttpResponse @this)
        {
            @this.StatusCode = 301;
            @this.StatusDescription = "Moved permanently.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 204 (No content.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusNoContent(this HttpResponse @this)
        {
            @this.StatusCode = 204;
            @this.StatusDescription = "No content.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 203 (Nonauthoritative information.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusNonauthoritativeInformation(this HttpResponse @this)
        {
            @this.StatusCode = 203;
            @this.StatusDescription = "Nonauthoritative information.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 404 (Not found. ).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusNotFound(this HttpResponse @this)
        {
            @this.StatusCode = 404;
            @this.StatusDescription = "Not found. ";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 304 (Not modified.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusNotModified(this HttpResponse @this)
        {
            @this.StatusCode = 304;
            @this.StatusDescription = "Not modified.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 302 (Object moved.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusObjectMoved(this HttpResponse @this)
        {
            @this.StatusCode = 302;
            @this.StatusDescription = "Object moved.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 206 (Partial content.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusPartialContent(this HttpResponse @this)
        {
            @this.StatusCode = 206;
            @this.StatusDescription = "Partial content.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 412 (Precondition failed.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusPreconditionFailed(this HttpResponse @this)
        {
            @this.StatusCode = 412;
            @this.StatusDescription = "Precondition failed.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 408 (Request timed out.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusRequestTimedOut(this HttpResponse @this)
        {
            @this.StatusCode = 408;
            @this.StatusDescription = "Request timed out.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 205 (Reset content.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusResetContent(this HttpResponse @this)
        {
            @this.StatusCode = 205;
            @this.StatusDescription = "Reset content.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 503 (Service unavailable. ).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusServiceUnavailable(this HttpResponse @this)
        {
            @this.StatusCode = 503;
            @this.StatusDescription = "Service unavailable. ";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 101 (Switching protocols.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusSwitchingProtocols(this HttpResponse @this)
        {
            @this.StatusCode = 101;
            @this.StatusDescription = "Switching protocols.";
        }

        /// <summary>
        ///     A HttpResponse extension method that sets the response to status code 307 (Temporary redirect.).
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void SetStatusTemporaryRedirect(this HttpResponse @this)
        {
            @this.StatusCode = 307;
            @this.StatusDescription = "Temporary redirect.";
        }

        /// <summary>
        ///     Searches the current naming container for a server control with the specified id parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>The specified control, or a null reference if the specified control does not exist.</returns>
        public static T FindControl<T>(this Control @this, string id) where T : class
        {
            return @this.FindControl(id) as T;
        }

        /// <summary>
        ///     Searches recursively in the container and child container for a server control with the specified id
        ///     parameter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>The specified control, or a null reference if the specified control does not exist.</returns>
        public static Control FindControlRecursive(this Control @this, string id)
        {
            var rControl = @this.FindControl(id);

            if (rControl == null)
            {
                foreach (Control control in @this.Controls)
                {
                    rControl = control.FindControl(id);
                    if (rControl != null)
                    {
                        break;
                    }
                }
            }

            return rControl;
        }

        /// <summary>
        ///     Searches recursively in the container and child container for a server control with the specified id
        ///     parameter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="id">The identifier for the control to be found.</param>
        /// <returns>The specified control, or a null reference if the specified control does not exist.</returns>
        public static T FindControlRecursive<T>(this Control @this, string id) where T : class
        {
            var rControl = @this.FindControl(id);

            if (rControl == null)
            {
                foreach (Control control in @this.Controls)
                {
                    rControl = control.FindControl(id);
                    if (rControl != null)
                    {
                        break;
                    }
                }
            }

            return rControl as T;
        }
    }
}
#endif