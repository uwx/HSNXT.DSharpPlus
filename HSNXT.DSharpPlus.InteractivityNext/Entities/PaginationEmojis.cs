using DSharpPlus.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
	/// <summary>
	/// Holds a set of emojis for pagination reactions. Every emoji can be null to be ignored, but you likely want to
	/// keep at least <see cref="Left"/>, <see cref="Right"/> and <see cref="Stop"/>.
	/// </summary>
    public class PaginationEmojis
    {
	    /// <summary>
	    /// Emoji for going left (back) one page.
	    /// </summary>
		public DiscordEmoji Left { get; set; }
	    
	    /// <summary>
	    /// Emoji for going right (forward) one page.
	    /// </summary>
		public DiscordEmoji Right { get; set; }
	    
	    /// <summary>
	    /// Emoji to end pagination.
	    /// </summary>
		public DiscordEmoji Stop { get; set; }
	    
	    /// <summary>
	    /// Emoji for going back to the first page.
	    /// </summary>
		public DiscordEmoji SkipLeft { get; set; }
	    
	    /// <summary>
	    /// Emoji for going all the way to the last page.
	    /// </summary>
		public DiscordEmoji SkipRight { get; set; }
	    
	    /// <summary>
	    /// Emoji for ending pagination and deleting the message.
	    /// </summary>
	    public DiscordEmoji Close { get; set; }

	    private PaginationEmojis()
	    {
	    }
	    
	    private PaginationEmojis(BaseDiscordClient client)
		{
			Left = DiscordEmoji.FromUnicode(client, "\u25C0"); // ◀
			Right = DiscordEmoji.FromUnicode(client, "\u25B6"); // ▶
			SkipLeft = DiscordEmoji.FromUnicode(client, "\u23EE"); // ⏮
			SkipRight = DiscordEmoji.FromUnicode(client, "\u23ED"); // ⏭
			Stop = DiscordEmoji.FromUnicode(client, "\u23F9"); // ⏹
		}

	    /// <summary>
	    /// Creates an empty set of pagination emojis to be filled in later.
	    /// </summary>
	    /// <returns>An empty set of pagination emojis</returns>
	    public static PaginationEmojis CreateEmpty() => new PaginationEmojis();
	    
	    /// <summary>
	    /// Creates the default set of pagination emojis: left arrow, right arrow, skip left, skip right and stop.
	    /// </summary>
	    /// <returns>The default set of pagination emojis</returns>
	    public static PaginationEmojis CreateDefault(BaseDiscordClient client) => new PaginationEmojis(client);
	    
	    /// <summary>
	    /// Creates the complete set of pagination emojis: left arrow, right arrow, skip left, skip right, stop and
	    /// close.
	    /// </summary>
	    /// <returns>The complete set of pagination emojis</returns>
	    public static PaginationEmojis CreateComplete(BaseDiscordClient client) => new PaginationEmojis(client)
	    {
		    Close = DiscordEmoji.FromUnicode(client, "\uD83C\uDDFD") // REGIONAL INDICATOR SYMBOL LETTER X
	    };

	    // not inheriting IEnumerable since we don't want this publicly accessible
	    internal IEnumerable<DiscordEmoji> Enumerate()
	    {
		    yield return Left;
		    yield return Right;
		    yield return Stop;
		    yield return SkipLeft;
		    yield return SkipRight;
		    yield return Close;
	    }
    }
}
