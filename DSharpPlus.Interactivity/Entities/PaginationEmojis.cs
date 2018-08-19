using DSharpPlus.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlus.Interactivity
{
    public class PaginationEmojis : IEnumerable<DiscordEmoji>
    {
		public DiscordEmoji Left { get; set; }
		public DiscordEmoji Right { get; set; }
		public DiscordEmoji Stop { get; set; }
		public DiscordEmoji SkipLeft { get; set; }
		public DiscordEmoji SkipRight { get; set; }
	    
	    public DiscordEmoji Close { get; set; }

		public PaginationEmojis(BaseDiscordClient client)
		{
			Left = DiscordEmoji.FromUnicode(client, "\u25C0"); // ◀
			Right = DiscordEmoji.FromUnicode(client, "\u25B6"); // ▶
			SkipLeft = DiscordEmoji.FromUnicode(client, "\u23EE"); // ⏮
			SkipRight = DiscordEmoji.FromUnicode(client, "\u23ED"); // ⏭
			Stop = DiscordEmoji.FromUnicode(client, "\u23F9"); // ⏹
		}

	    public IEnumerator<DiscordEmoji> GetEnumerator()
	    {
		    yield return Left;
		    yield return Right;
		    yield return Stop;
		    yield return SkipLeft;
		    yield return SkipRight;
		    yield return Close;
	    }

	    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
