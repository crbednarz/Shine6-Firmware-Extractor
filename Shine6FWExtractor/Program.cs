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
			if (args.Length != 2)
			{
				Console.WriteLine(
					"Shine6FWExtractor\n" +
					"Usage: Shine6FWExtractor.exe [FWUpdate.exe path] [Output path]\n" +
					"Example: Shine6FWExtractor.exe FWUpdate.exe firmware.bin");
				return;
			}

			var pathToUpdater = args[0];
			var outputPath = args[1];

			if (!File.Exists(pathToUpdater))
			{
				Console.WriteLine("Failed to find FWUpdate.exe file. Path \"{0}\" not found.", pathToUpdater);
				return;
			}
			
			var reader = UpdaterReader.FromPath(pathToUpdater);

			// Unfortunately, I was never able to find the correct way to detect the firmware language, but the last one seems to be US.
			var encryptedFirmware = reader.EncryptedFirmwares.Last();

			var firmware = Utilities.DecryptFirmware(encryptedFirmware);

			File.WriteAllBytes(outputPath, firmware);

			Console.WriteLine("Extraction complete!");
		}
	}
}
