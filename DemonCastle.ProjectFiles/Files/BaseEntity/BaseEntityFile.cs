using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

public abstract class BaseEntityFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;


	public Guid InitialState { get; set; } = Guid.Empty;

	public List<AnimationData> Animations { get; set; } = new();
	public List<MonsterStateData> States { get; set; } = new();
}