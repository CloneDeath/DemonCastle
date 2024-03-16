using System;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public partial class EntityStateMachine : Node {
	private readonly IGameState _game;
	private readonly IEntityState _entity;
	private readonly Guid _initialState;
	private readonly IEntityStateInfoCollection _states;
	private Guid _currentStateId;

	private EntityStateInfo? CurrentState => _states.Get(_currentStateId);

	public EntityStateMachine(IGameState game, IEntityState entity, Guid initialState, IEntityStateInfoCollection states) {
		_game = game;
		_entity = entity;
		_initialState = initialState;
		_states = states;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (CurrentState == null) return;

		CurrentState.OnUpdate.Execute(_game, _entity);
		CurrentState.Transitions.CheckAndTriggerTransitions(_game, _entity);
	}

	private bool ProcessingShouldBeDisabled => CurrentState == null || !CurrentState.OnUpdate.Any() && !CurrentState.Transitions.Any();

	public void Reset() {
		_currentStateId = Guid.Empty;
        ChangeState(_initialState);
	}

	public void ChangeState(Guid stateId) {
		CurrentState?.OnExit.Execute(_game, _entity);
		_currentStateId = stateId;
		CurrentState?.OnEnter.Execute(_game, _entity);
		_entity.SetAnimation(CurrentState?.Animation ?? Guid.Empty);

		ProcessMode = ProcessingShouldBeDisabled ? ProcessModeEnum.Disabled : ProcessModeEnum.Inherit;
	}
}