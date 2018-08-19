using System.Collections.Concurrent;
using System.Collections.Generic;
using ConcurrentCollections;
using DSharpPlus.Entities;

namespace DSharpPlus.Interactivity
{
    public class ReactionCollectionContext
    {
        public ConcurrentDictionary<DiscordEmoji, int> Reactions { get; } = new ConcurrentDictionary<DiscordEmoji, int>();

        internal readonly ConcurrentHashSet<ulong> VotingMembers = new ConcurrentHashSet<ulong>();

        public InteractivityExtension Interactivity { get; }

        public DiscordClient Client => Interactivity.Client;

        public ReactionCollectionContext(InteractivityExtension interactivity)
        {
            Interactivity = interactivity;
        }

        internal void AddReaction(DiscordEmoji reaction)
        {
            if (Reactions.ContainsKey(reaction))
                Reactions[reaction]++;
            else
                Reactions.TryAdd(reaction, 1);
        }

        internal void AddReaction(DiscordEmoji reaction, ulong member)
        {
            if (VotingMembers.Contains(member)) return;
            
            if (Reactions.ContainsKey(reaction))
                Reactions[reaction]++;
            else
                Reactions.TryAdd(reaction, 1);
            VotingMembers.Add(member);
        }

        internal void RemoveReaction(DiscordEmoji reaction)
        {
            if (!Reactions.ContainsKey(reaction)) return;
            
            if (--Reactions[reaction] <= 0)
                Reactions.TryRemove(reaction, out _);
        }

        internal void RemoveReaction(DiscordEmoji reaction, ulong member)
        {
            if (!Reactions.ContainsKey(reaction) || !VotingMembers.Contains(member)) return;
            
            if (--Reactions[reaction] <= 0)
                Reactions.TryRemove(reaction, out _);
            VotingMembers.TryRemove(member);
        }

        internal void ClearReactions()
        {
            Reactions.Clear();
            VotingMembers.Clear();
        }
    }
}
