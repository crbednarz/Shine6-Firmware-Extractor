using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine6FWExtractor
{
	/// <summary>
	/// Responsible for reading/parsing the updater file. (Usually FWUpdate.exe)
	/// </summary>
	public class UpdaterReader
	{
		public IEnumerable<byte[]> EncryptedFirmwares => _encryptedFirmwares;
		private readonly byte[] _rawFile;

		private readonly List<byte[]> _encryptedFirmwares = new List<byte[]>();

		private UpdaterReader(byte[] updaterRawData)
		{
			_rawFile = updaterRawData;

			ParseFileData();
		}

		public static UpdaterReader FromPath(string path)
		{
			return new UpdaterReader(File.ReadAllBytes(path));
		}

		private void ParseFileData()
		{
			var metadata = ExtractMetadata();
			ParseMetadataTags(metadata);
		}

		private byte[] ExtractMetadata()
		{
			var metadata = new byte[UpdaterConstants.MetadataSize];

			for (int i = 0; i < metadata.Length; i++)
			{
				int srcIndex = i + _rawFile.Length - UpdaterConstants.MetadataOffsetFromEOF;
				metadata[i] = _rawFile[srcIndex];
			}

			Utilities.DecryptShuffledData(metadata);

			return metadata;
		}

		private void ParseMetadataTags(byte[] metadata)
		{
			int metadataIndex = UpdaterConstants.MetadataReadOffset;
			int fileIndex = _rawFile.Length - UpdaterConstants.MetadataOffsetFromEOF;

			for (int tagIndex = 0; tagIndex < UpdaterConstants.NumberOfTags; tagIndex++)
			{
				int valueLength = BitConverter.ToInt32(metadata, metadataIndex);
				int nameLength = BitConverter.ToInt32(metadata, metadataIndex + 4);

				fileIndex -= nameLength;

				// While we don't actually do anything with the name here, I've left the read code in for the sake of completeness
				if (nameLength != 0)
				{
					byte[] name = new byte[nameLength];

					for (int i = 0; i < nameLength; i++)
						name[i] = _rawFile[fileIndex + i];

					Utilities.DecryptShuffledData(name);
				}
				
				fileIndex -= valueLength;

				if (valueLength != 0)
				{
					byte[] value = new byte[valueLength];

					for (int i = 0; i < valueLength; i++)
						value[i] = _rawFile[fileIndex + i];

					Utilities.DecryptShuffledData(value);

					if (valueLength >= UpdaterConstants.MinimumSizeForFirmware)
						_encryptedFirmwares.Add(value);

				}
				
				metadataIndex -= UpdaterConstants.MetadataTagSeparation;
			}
		}
	}
}
