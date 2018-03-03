using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shine6FWExtractor
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var pathToUpdater = args[0];
			var outputPath = args[1];
			
			var reader = UpdaterReader.FromPath(pathToUpdater);

			// Unfortunately, I was never able to find the correct way to detect the firmware language, but the last one seems to be US.
			var encryptedFirmware = reader.EncryptedFirmwares.Last();

			var firmware = Utilities.DecryptFirmware(encryptedFirmware);

			File.WriteAllBytes(outputPath, firmware);
		}
	}
}
