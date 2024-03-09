using System;
using System.Collections.Generic;
using DemonCastle.Files.Animations;
using DemonCastle.Files.Common;
using DemonCastle.Files.Events;
using DemonCastle.Files.Variables;
using Newtonsoft.Json;

namespace DemonCastle.Files.BaseEntity;

public abstract class BaseEntityFile {
	[JsonProperty(Order = -3)]
	public Guid Id { get; set; } = Guid.NewGuid();

	[JsonProperty(Order = -2)]
	public string Name = string.Empty;

	public Size Size = new();

	public Guid InitialState = Guid.Empty;

	public List<AnimationData> Animations { get; set; } = new();
	public List<EntityStateData> States { get; set; } = new();
	public List<VariableDeclarationData> Variables { get; set; } = new();
	public List<EntityEventData> Events { get; set; } = new();
}