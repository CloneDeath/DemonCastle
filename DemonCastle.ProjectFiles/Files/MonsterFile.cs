using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;
using DemonCastle.ProjectFiles.Files.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files;

public class MonsterFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;

	public int Health { get; set; } = 1;
	public int Experience { get; set; } = 1;
	public int Attack { get; set; } = 1;
	public int PhysicalDefense { get; set; }
	public int MagicalDefense { get; set; }

	public float MoveSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 3;
	public float Gravity { get; set; } = 100;
	public Size Size { get; set; } = new();
	public Guid InitialState { get; set; } = Guid.Empty;

	public List<AnimationData> Animations { get; set; } = new();
	public List<MonsterStateData> States { get; set; } = new();
}

public class MonsterStateData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid Animation { get; set; } = Guid.Empty;
	public List<MonsterStateTransitionData> Transitions { get; set; } = new();

	public List<MonsterStateActionData> OnEnter { get; set; } = new();
	public List<MonsterStateActionData> OnUpdate { get; set; } = new();
	public List<MonsterStateActionData> OnExit { get; set; } = new();
}

public class MonsterStateTransitionData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public Guid TargetState { get; set; } = Guid.Empty;
	public MonsterStateTransitionEvent When { get; set; } = new();
}

public class MonsterStateTransitionEvent {
	public SelfEvents? Self { get; set; }
	public AnimationEvent? Animation { get; set; }
	public RandomTimerExpires? RandomTimerExpires { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfEvents {
	Killed
}

[JsonConverter(typeof(StringEnumConverter))]
public enum AnimationEvent {
	Complete
}

public class RandomTimerExpires {
	public Duration Start { get; set; } = new();
	public Duration End { get; set; } = new();
}

public class Duration {
	public float Seconds { get; set; }
}

public class MonsterStateActionData {
	public FaceAction? Face { get; set; }
	public MoveAction? Move { get; set; }
	public SelfAction? Self { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum FaceAction {
	TowardsClosestPlayer,
	AwayFromClosestPlayer,
	Left,
	Right
}

[JsonConverter(typeof(StringEnumConverter))]
public enum MoveAction {
	Forward,
	Backward,
	Left,
	Right
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfAction {
	Despawn
}