using System;
using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public class MonsterStateData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid Animation { get; set; } = Guid.Empty;
	public List<MonsterStateTransitionData> Transitions { get; set; } = new();

	public List<MonsterStateActionData> OnEnter { get; set; } = new();
	public List<MonsterStateActionData> OnUpdate { get; set; } = new();
	public List<MonsterStateActionData> OnExit { get; set; } = new();
}