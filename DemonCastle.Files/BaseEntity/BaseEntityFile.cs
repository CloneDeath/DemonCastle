using System;
using System.Collections.Generic;
using DemonCastle.Files.Animations;

namespace DemonCastle.Files.BaseEntity;

public abstract class BaseEntityFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;


	public Guid InitialState { get; set; } = Guid.Empty;

	public List<AnimationData> Animations { get; set; } = new();
	public List<EntityStateData> States { get; set; } = new();
}