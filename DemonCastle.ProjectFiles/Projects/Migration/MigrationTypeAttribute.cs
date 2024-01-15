using System;

namespace DemonCastle.ProjectFiles.Projects.Migration;

[AttributeUsage(AttributeTargets.Class)]
public class MigrationTypeAttribute : Attribute {
	public Type Type { get; }
	public MigrationTypeAttribute(Type type) {
		Type = type;
	}
}