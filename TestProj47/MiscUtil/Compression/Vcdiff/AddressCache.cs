using System;
using System.IO;

namespace HSNXT.MiscUtil.Compression.Vcdiff
{
	/// <summary>
	/// Cache used for encoding/decoding addresses.
	/// </summary>
	internal sealed class AddressCache
	{
		private const byte SelfMode = 0;
		private const byte HereMode = 1;

		private int nearSize;
		private int sameSize;
		private int[] near;
		private int nextNearSlot;
		private int[] same;

		private Stream addressStream;

		internal AddressCache(int nearSize, int sameSize)
		{
			this.nearSize = nearSize;
			this.sameSize = sameSize;
			near = new int[nearSize];
			same = new int[sameSize*256];
		}

		internal void Reset(byte[] addresses)
		{
			nextNearSlot = 0;
			Array.Clear(near, 0, near.Length);
			Array.Clear(same, 0, same.Length);

			addressStream = new MemoryStream(addresses, false);
		}

		internal int DecodeAddress (int here, byte mode)
		{
			int ret;
			if (mode==SelfMode)
			{
				ret = IOHelper.ReadBigEndian7BitEncodedInt(addressStream);
			}
			else if (mode==HereMode)
			{
				ret = here - IOHelper.ReadBigEndian7BitEncodedInt(addressStream);
			}
			else if (mode-2 < nearSize) // Near cache
			{
				ret = near[mode-2] + IOHelper.ReadBigEndian7BitEncodedInt(addressStream);
			}
			else // Same cache
			{
				int m = mode-(2+nearSize);
				ret = same[(m*256)+IOHelper.CheckedReadByte(addressStream)];
			}

			Update (ret);
			return ret;
		}

		private void Update (int address)
		{
			if (nearSize > 0)
			{
				near[nextNearSlot] = address;
				nextNearSlot=(nextNearSlot+1)%nearSize;
			}
			if (sameSize > 0)
			{
				same[address%(sameSize*256)] = address;
			}
		}
	}
}
