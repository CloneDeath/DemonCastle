using System;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public partial class EntityDetectionArea : Area2D {
	private readonly CollisionShape2D _collisionShape;
	private readonly RectangleShape2D _shape;

	public event Action? PlayerEnter;
	public event Action? PlayerExit;

	public Vector2 Size {
		get => _shape.Size;
		set => _shape.Size = value;
	}

	public Vector2 Center {
		get => _collisionShape.Position;
		set => _collisionShape.Position = value;
	}

	public EntityDetectionArea(DebugState debug) {
		Name = nameof(EntityDetectionArea);
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.Player;
		Monitoring = true;

		AddChild(_collisionShape = new CollisionShape2D {
			Shape = _shape = new RectangleShape2D(),
			DebugColor = new Color(Colors.Brown, 0.5f),
			Visible = debug.ShowCollisions
		});

		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body) {
		if (body is GamePlayer) PlayerEnter?.Invoke();
	}

	private void OnBodyExited(Node2D body) {
		if (body is GamePlayer) PlayerExit?.Invoke();
	}
}