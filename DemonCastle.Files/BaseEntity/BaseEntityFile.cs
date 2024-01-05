using System;
using System.Collections.Generic;
using DemonCastle.Files.Animations;
using Newtonsoft.Json;

namespace DemonCastle.Files.BaseEntity;

public abstract class BaseEntityFile {
	[JsonProperty(Order = -3)]
	public Guid Id { get; set; } = Guid.NewGuid();

	[JsonProperty(Order = -2)]
	public string Name { get; set; } = string.Empty;

	public Guid InitialState { get; set; } = Guid.Empty;

	public List<AnimationData> Animations { get; set; } = new();
	public List<EntityStateData> States { get; set; } = new();
}