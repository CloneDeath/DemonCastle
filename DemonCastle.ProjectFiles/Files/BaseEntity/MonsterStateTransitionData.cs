using System;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public class MonsterStateTransitionData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid TargetState { get; set; } = Guid.Empty;
	public MonsterStateTransitionEvent When { get; set; } = new();
}