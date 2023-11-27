using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files;

public class MonsterFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;

	public float MoveSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 3;
	public float Gravity { get; set; } = 100;
	public Size Size { get; set; } = new();
	public Guid InitialState { get; set; } = Guid.Empty;

	public List<MonsterAnimationData> Animations { get; set; } = new();
	public List<MonsterStateData> States { get; set; } = new();
}

public class Size {
	public int Width { get; set; } = 16;
	public int Height { get; set; } = 16;
}

public class MonsterAnimationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<MonsterFrameData> Frames { get; set; } = new();
}

public class MonsterFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
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
	public AnimationEvent? Animation { get; set; }
	public RandomTimerExpires? RandomTimerExpires { get; set; }
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
	public FaceTowardsAction? FaceTowards { get; set; }
	public MoveAction? Move { get; set; }
	public SelfAction? Self { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum FaceTowardsAction {
	ClosestPlayer
}

[JsonConverter(typeof(StringEnumConverter))]
public enum MoveAction {
	Forward
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfAction {
	Despawn
}