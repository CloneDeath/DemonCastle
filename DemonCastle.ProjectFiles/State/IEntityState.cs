using System;
using Godot;

namespace DemonCastle.ProjectFiles.State;

public interface IEntityState {
	void SetFacing(int facing);
	Vector2 AreaPosition { get; }
	bool WasKilled { get; }
	IVariables Variables { get; }
	void SetAnimation(Guid animationId);
	void ChangeStateTo(Guid stateId);
	void Despawn();

	void MoveLeft();
	void MoveRight();
	void MoveForward();
	void MoveBackward();
	void EnableWorldCollisions();
	void DisableWorldCollisions();
}

public interface IVariables {
	Guid GetGuid(Guid variableId);
	Vector2I GetVector2I(Guid variableId);
	bool HasBoolean(Guid variableId);
	bool GetBoolean(Guid variableId);
	void SetBoolean(Guid variableId, bool value);
	bool? TryGetBoolean(Guid variableId) => HasBoolean(variableId) ? GetBoolean(variableId) : null;
}