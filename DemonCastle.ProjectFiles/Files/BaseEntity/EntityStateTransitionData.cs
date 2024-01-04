using System;
using DemonCastle.ProjectFiles.Files.Conditions;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public class EntityStateTransitionData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid TargetState { get; set; } = Guid.Empty;
	public EntityStateTransitionEvent When { get; set; } = new();
}