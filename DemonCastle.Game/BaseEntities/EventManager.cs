using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public partial class EventManager : Node {
	private readonly IGameState _game;
	private readonly IEntityState _entityState;
	private readonly IBaseEntityInfo _entity;

	public EventManager(IGameState game, IEntityState entityState, IBaseEntityInfo entity) {
		_game = game;
		_entityState = entityState;
		_entity = entity;
		Name = nameof(EventManager);
	}

	public void OnPlayerEnter() {
		_entity.Events.CheckAndTriggerEvents(_game, _entityState, new EventDetails {
			OnPlayerEnter = true
		});
	}

	public void OnPlayerExit() {
		_entity.Events.CheckAndTriggerEvents(_game, _entityState, new EventDetails {
			OnPlayerExit = true
		});
	}
}