using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game;

public class VariableCollection : IVariables {
	private readonly VariableDeclarationInfoCollection _variables;

	private readonly Dictionary<Guid, Guid> _guids = new();
	private readonly Dictionary<Guid, Vector2I> _vector2Is = new();
	private readonly Dictionary<Guid, bool> _booleans = new();

	public VariableCollection(VariableDeclarationInfoCollection variables) {
		_variables = variables;
		Reset();
	}

	public void Reset() {
		_guids.Clear();
		foreach (var itemVariable in _variables.Items) {
			_guids.Add(itemVariable.Id, itemVariable.DefaultValue);
		}
		foreach (var monsterVariable in _variables.Monsters) {
			_guids.Add(monsterVariable.Id, monsterVariable.DefaultValue);
		}

		_vector2Is.Clear();
		foreach (var vectorVariable in _variables.Vector2I) {
			_vector2Is.Add(vectorVariable.Id, vectorVariable.DefaultValue);
		}

		_booleans.Clear();
		foreach (var boolVariable in _variables.Boolean) {
			_booleans.Add(boolVariable.Id, boolVariable.DefaultValue);
		}
	}

	public Guid GetGuid(Guid variableId) => _guids[variableId];

	public Vector2I GetVector2I(Guid variableId) => _vector2Is[variableId];

	public bool HasBoolean(Guid variableId) => _booleans.ContainsKey(variableId);
	public bool GetBoolean(Guid variableId) => _booleans[variableId];
	public void SetBoolean(Guid variableId, bool value) => _booleans[variableId] = value;
}