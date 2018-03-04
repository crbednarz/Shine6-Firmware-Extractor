# Ducky Shine 6 Firmware Extractor
Tool & Documentation for the extraction of Duck Shine 6's firmware from its firmware updater.


## Usage
After compiling the tool, you can use the first argument to specify the path, and the second to specify the output.
By default it only extracts the US firmware from the Shine6 FWUpdate.exe for V1.02.10

Example Usage: Shine6FWExtractor.exe FWUpdate.exe firmware.bin

However, the intention of the tool is more to cover how one might approach extracting future firmware updates from Ducky devices.
Comments in UpdaterConstants.cs may offer some insight on approach other versions of the updater.


## License
License information can be found in the LICENSE file in the root directory.
