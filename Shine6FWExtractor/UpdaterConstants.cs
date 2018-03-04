using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine6FWExtractor
{
	public static class UpdaterConstants
	{
		/* Most of the data here is extracted from sub_404390 which can be found by looking for a
		 * reference to the UTF-16 string "Fail to find firmware information!"
		 *
		 * The loop found in UpdaterReader.ParseMetadataTags is pretty close to the original assembly, and it shouldn't be hard to find
		 * where values were lifted from.
		 */
		public const int MetadataReadOffset = 2760;
		public const int MetadataSize = 0x0B24;
		public const int MetadataOffsetFromEOF = 2852;
		public const int MetadataTagSeparation = 80;

		public const int NumberOfTags = 9;

		// Without any particularly good way to understand the tags, using a minimum size to find firmware seemed effective.
		public const int MinimumSizeForFirmware = 0x5000;


		/* This XOR key acts as a second layer of encryption for the firmware.
		 * (Special thanks to https://github.com/ChaoticConundrum/pok3r_re_firmware for making me realize an xor encryption was being used)
		 *
		 * To find the key I made some assuptions:
		 * 1. 0 is almost always the most common value.
		 * 2. 0 ^ key = key
		 * 3. The xor key was 52-bits
		 *
		 * Using this, I built a key using the most common values, and looked at the result.
		 * There were several places were words were visible, but partially corrupted.
		 * By attempting the 2nd, 3rd, and 4th most common values for those parts of the key, I was able to piece together
		 * all key pieces except for 3.
		 *
		 * From there I noticed a pattern at the start of the file, where "6f 35 00 00" repeated several times, and used that as reference
		 * to correct the last few key pieces.
		 *
		 */
		public static readonly uint[] XorKey = new uint[]
		{
			0xE7C29474,
			0x79084B10,
			0x53D54B0D,
			0xFC1E8F32,
			0x48E81A9B,
			0x773C808E,
			0xB7483552,
			0xD9CB8C76,
			0x2A8C8BC6,
			0x0967ADA8,
			0xD4520F5C,
			0xD0C3279D,
			0xEAC091C5
		};
	}
}
