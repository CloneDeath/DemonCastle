using System;

namespace DemonCastle.ProjectFiles.Exceptions;

public class IncompleteDataException : Exception {
	public string File { get; }

	public IncompleteDataException(string file)
		: base($"Incomplete data found in file '{file}'.") {
		File = file;
	}
}