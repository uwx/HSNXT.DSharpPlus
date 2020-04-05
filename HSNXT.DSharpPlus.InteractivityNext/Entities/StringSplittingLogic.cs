namespace HSNXT.DSharpPlus.InteractivityNext
{
    // TODO this never got off the planning board
    public enum StringSplittingLogic : byte
    {
        /// <summary>
        /// Cut every string at the length limit (slightly under 2000 characters). No fuss, no smart logic.
        /// </summary>
        Verbatim,
        /// <summary>
        /// Cut every string at the length limit, but if the cut intersects a line, the entire line is moved to the next
        /// page. If the line itself is over the length limit, it's split as per <see cref="Words"/>.
        /// </summary>
        Lines,
        /// <summary>
        /// Cut every string at the length limit, but if the cut intersects a word (one or more characters surrounded by
        /// whitespace or the start/end of the string), the entire word is moved to the next page. If the word itself is
        /// over the length limit, it's split as per <see cref="Verbatim"/>.
        /// </summary>
        Words
    }
}