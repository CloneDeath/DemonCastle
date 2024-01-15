using System;

namespace DemonCastle.ProjectFiles.Exceptions;

public class MissingMigratorException : Exception {
	public Type Type { get; }
	public int Version { get; }

	public MissingMigratorException(Type type, int version)
		: base($"Could not find a migrator for {type.Name} version {version}.") {
		Type = type;
		Version = version;
	}
}