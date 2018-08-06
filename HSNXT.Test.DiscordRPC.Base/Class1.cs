using System;
using System.Runtime.InteropServices;
using System.Text;

namespace HSNXT.DiscordRPC.Base
{
    public class DiscordRpc
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadyCallback();
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DisconnectedCallback(int errorCode, string message);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorCallback(int errorCode, string message);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void JoinCallback(string secret);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SpectateCallback(string secret);
    
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RequestCallback(JoinRequest request);
    
        [StructLayout(LayoutKind.Sequential)]
        public struct EventHandlers
        {
            public ReadyCallback readyCallback;
            public DisconnectedCallback disconnectedCallback;
            public ErrorCallback errorCallback;
            public JoinCallback joinCallback;
            public SpectateCallback spectateCallback;
            public RequestCallback requestCallback;
        }
    
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct RichPresenceInternal
        {
            public IntPtr state; /* max 128 bytes */
            public IntPtr details; /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public IntPtr largeImageKey; /* max 32 bytes */
            public IntPtr largeImageText; /* max 128 bytes */
            public IntPtr smallImageKey; /* max 32 bytes */
            public IntPtr smallImageText; /* max 128 bytes */
            public IntPtr partyId; /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public IntPtr matchSecret; /* max 128 bytes */
            public IntPtr joinSecret; /* max 128 bytes */
            public IntPtr spectateSecret; /* max 128 bytes */
            public bool instance;
        }
        
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RichPresence
        {
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string state; /* max 128 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string details; /* max 128 bytes */
            public long startTimestamp;
            public long endTimestamp;
            public string largeImageKey; /* max 32 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string largeImageText; /* max 128 bytes */
            public string smallImageKey; /* max 32 bytes */
            [MarshalAs(UnmanagedType.LPUTF8Str)] public string smallImageText; /* max 128 bytes */
            public string partyId; /* max 128 bytes */
            public int partySize;
            public int partyMax;
            public string matchSecret; /* max 128 bytes */
            public string joinSecret; /* max 128 bytes */
            public string spectateSecret; /* max 128 bytes */
            public bool instance;
        }
    
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct JoinRequest
        {
            public string userId;
            public string username;
            public string avatar;
        }
    
        public enum Reply
        {
            No = 0,
            Yes = 1,
            Ignore = 2
        }
    
        [DllImport("discord-rpc", EntryPoint = "Discord_Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Initialize(string applicationId, ref EventHandlers handlers, bool autoRegister, string optionalSteamId);
    
        [DllImport("discord-rpc", EntryPoint = "Discord_Shutdown", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Shutdown();
    
        [DllImport("discord-rpc", EntryPoint = "Discord_RunCallbacks", CallingConvention = CallingConvention.Cdecl)]
        public static extern void RunCallbacks();
    
        [DllImport("discord-rpc", EntryPoint = "Discord_UpdatePresence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UpdatePresenceInternal(ref RichPresenceInternal presence);
        
        public static void UpdatePresence(ref RichPresence presence)
        {
            var apresence = new RichPresenceInternal
            {
                state = ConvertString(presence.state),
                details = ConvertString(presence.details),
                startTimestamp = presence.startTimestamp,
                endTimestamp = presence.endTimestamp,
                largeImageKey = ConvertString(presence.largeImageKey),
                largeImageText = ConvertString(presence.largeImageText),
                smallImageKey = ConvertString(presence.smallImageKey),
                smallImageText = ConvertString(presence.smallImageText),
                partyId = ConvertString(presence.partyId),
                partySize = presence.partySize,
                partyMax = presence.partyMax,
                matchSecret = ConvertString(presence.matchSecret),
                joinSecret = ConvertString(presence.joinSecret),
                spectateSecret = ConvertString(presence.spectateSecret),
                instance = presence.instance,
            };
            UpdatePresenceInternal(ref apresence);
        }
        
        private static IntPtr ConvertString(string str)
        {
            if (str == null) return IntPtr.Zero;
            
            var retArray = Encoding.UTF8.GetBytes(str);
            var retPtr = Marshal.AllocHGlobal(retArray.Length + 1);
            Marshal.Copy(retArray, 0, retPtr, retArray.Length);
            Marshal.WriteByte(retPtr, retArray.Length, 0);
            return retPtr;
        }

        [DllImport("discord-rpc", EntryPoint = "Discord_Respond", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Respond(string userId, Reply reply);
    }

}