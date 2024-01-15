using System;

namespace DemonCastle.ProjectFiles.Projects.Migration;

[AttributeUsage(AttributeTargets.Method)]
public class ToVersionAttribute : Attribute {
	public int Version { get; }
	public ToVersionAttribute(int version) {
		Version = version;
	}
}