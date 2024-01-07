using System;
using System.Collections.Generic;
using DemonCastle.Files.Actions;

namespace DemonCastle.Files.BaseEntity;

public class EntityStateData {
	public Guid Id = Guid.NewGuid();
	public string Name = "State";
	public Guid Animation = Guid.Empty;
	public List<EntityStateTransitionData> Transitions { get; set; } = new();

	public List<EntityActionData> OnEnter { get; set; } = new();
	public List<EntityActionData> OnUpdate { get; set; } = new();
	public List<EntityActionData> OnExit { get; set; } = new();
}