using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Conditions;

namespace DemonCastle.Files.Events;

public class EntityEventData {
	public string Name { get; set; } = string.Empty;
	public EntityEventConditionData When { get; set; } = new();
	public List<EntityActionData> Then { get; set; } = new();
}