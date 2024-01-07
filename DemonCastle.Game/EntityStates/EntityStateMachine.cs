using System;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.Game.EntityStates;

public class EntityStateMachine {
	private readonly IGameState _game;
	private readonly IEntityState _entity;
	private readonly Guid _initialState;
	private readonly EntityStateInfoCollection _states;
	private Guid _currentStateId;

	private EntityStateInfo? CurrentState => _states.Get(_currentStateId);

	public EntityStateMachine(IGameState game, IEntityState entity, Guid initialState, EntityStateInfoCollection states) {
		_game = game;
		_entity = entity;
		_initialState = initialState;
		_states = states;
		_currentStateId = initialState;
	}

	public void Reset() {
		_currentStateId = Guid.Empty;
        ChangeState(_initialState);
	}

	private void ChangeState(Guid stateId) {
		CurrentState?.OnExit.Execute(_game, _entity);
		_currentStateId = stateId;
		CurrentState?.OnEnter.Execute(_game, _entity);
		_entity.SetAnimation(CurrentState?.Animation ?? Guid.Empty);
	}
}