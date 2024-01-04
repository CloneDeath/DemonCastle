using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Actions;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public class EntityStateData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid Animation { get; set; } = Guid.Empty;
	public List<EntityStateTransitionData> Transitions { get; set; } = new();

	public List<EntityActionData> OnEnter { get; set; } = new();
	public List<EntityActionData> OnUpdate { get; set; } = new();
	public List<EntityActionData> OnExit { get; set; } = new();
}