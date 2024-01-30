using System;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents.Types;

public class EmptyVariableCollection : IVariables {
	public Guid GetGuid(Guid variableId) => Guid.Empty;

	public Vector2I GetVector2I(Guid variableId) => Vector2I.Zero;

	public bool HasBoolean(Guid variableId) => false;
	public bool GetBoolean(Guid variableId) => false;
	public void SetBoolean(Guid variableId, bool value) { }
}