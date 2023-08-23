using System;

namespace DemonCastle.ProjectFiles.Exceptions {
	public partial class UnknownSpriteFileFormatException : Exception {
		public string File { get; }

		public UnknownSpriteFileFormatException(string file)
			: base($"Unknown format for sprite file '{file}'.") {
			File = file;
		}
	}
}