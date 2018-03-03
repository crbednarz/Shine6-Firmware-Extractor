using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine6FWExtractor
{
	public static class UpdaterConstants
	{
		public const int MetadataReadOffset = 2760;
		public const int MetadataSize = 0x0B24;
		public const int MetadataOffsetFromEOF = 2852;
		public const int MetadataTagSeparation = 80;

		public const int NumberOfTags = 9;

		public const int MinimumSizeForFirmware = 0x5000;
		
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
