using System;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IEntityState {
	void SetFacing(int facing);
	Vector2 AreaPosition { get; }
	void SetAnimation(Guid animationId);
}