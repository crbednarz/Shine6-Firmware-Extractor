using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine6FWExtractor
{
	public static class Utilities
	{
		private static void SwapByte(ref byte value1, ref byte value2)
		{
			byte temp = value2;
			value2 = value1;
			value1 = temp;
		}

		public static void DecryptShuffledData(byte[] data)
		{
			// As a base-line encryption, bytes and then bits are shuffled.
			// Presumably to stop people from finding the firmware without reverse engineering the code.

			for (int i = 4; i < data.Length; i += 5)
				SwapByte(ref data[i], ref data[i - 4]);
			
			for (int i = 1; i < data.Length; i += 2)
				SwapByte(ref data[i], ref data[i - 1]);

			for (int i = 0; i < data.Length; i++)
			{
				int a = data[i];
				int d = a;
				d -= 7;
				d <<= 4;
				a >>= 4;

				d += a;

				data[i] = (byte)d;
			}
		}

		public static byte[] DecryptFirmware(byte[] data)
		{
			var size = data.Length / sizeof(int);
			var firmwareImage = new uint[size];
			for (var index = 0; index < size; index++)
				firmwareImage[index] = BitConverter.ToUInt32(data, index * sizeof(int));


			uint[] decoded = new uint[firmwareImage.Length];

			for (int i = 0; i < decoded.Length; i++)
				decoded[i] = firmwareImage[i] ^ UpdaterConstants.XorKey[i % 13];
			
			byte[] result = new byte[decoded.Length * sizeof(int)];
			Buffer.BlockCopy(decoded, 0, result, 0, result.Length);

			return result;
		}
	}
}
