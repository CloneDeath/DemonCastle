using System;

namespace DemonCastle.ProjectFiles.Exceptions {
	public class UnknownSpriteFileFormatException : Exception {
		public string File { get; }

		public UnknownSpriteFileFormatException(string file)
			: base($"Unknown format for sprite file '{file}'.") {
			File = file;
		}
	}
}