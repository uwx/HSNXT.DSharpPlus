#if NetFX
using System.Web.UI.WebControls;

namespace HSNXT
{
    /// <summary>
    /// Webcontrol TextBox Extensions
    /// </summary>
    public static partial class Extensions
    {
        #region FormValue extension

        /// <summary>
        /// The form value for this textbox.
        /// </summary>
        /// <param name="textbox">The textbox.</param>
        /// <returns></returns>
        public static string FormValue(this TextBox textbox)
        {
            return FormValue(textbox, string.Empty);
        }

        public static string FormValue(this TextBox textbox, string defaultvalue)
        {
            string value = null;
            value = textbox.Page.Request.Form[textbox.UniqueID];
            return value ?? (!string.IsNullOrEmpty(textbox.Text) ? textbox.Text : defaultvalue);
        }

        //public static int FormIntValue(this TextBox textbox, int defaultvalue)
        //{
        //    string value = null;
        //    if (textbox.UniqueID != null) value = textbox.Page.Request.Form[textbox.UniqueID];
        //    return value ?? ((textbox.Text.ToInteger() == 0) ? defaultvalue: textbox.Text.ToInteger());
        //}

        #endregion
    }
}
#endif