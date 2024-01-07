using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public class BaseEntityVariables : IVariables {
	private readonly IBaseEntityInfo _entity;

	private readonly Dictionary<Guid, Guid> _guids = new();
	private readonly Dictionary<Guid, Vector2I> _vector2Is = new();

	public BaseEntityVariables(IBaseEntityInfo entity) {
		_entity = entity;
		Reset();
	}

	public void Reset() {
		_guids.Clear();
		foreach (var itemVariable in _entity.Variables.Items) {
			_guids.Add(itemVariable.Id, itemVariable.DefaultValue);
		}
		foreach (var monsterVariable in _entity.Variables.Monsters) {
			_guids.Add(monsterVariable.Id, monsterVariable.DefaultValue);
		}

		_vector2Is.Clear();
		foreach (var vectorVariable in _entity.Variables.Vector2I) {
			_vector2Is.Add(vectorVariable.Id, vectorVariable.DefaultValue);
		}
	}

	public Guid GetGuid(Guid variableId) => _guids[variableId];

	public Vector2I GetVector2I(Guid variableId) => _vector2Is[variableId];
}