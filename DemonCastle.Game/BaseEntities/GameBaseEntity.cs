using System;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public abstract partial class GameBaseEntity : CharacterBody2D, IEntityState, IDamageable {
	private readonly BaseEntityVariables _variables;
	private readonly GameAnimation _animation;
	private readonly EntityStateMachine _stateMachine;

	public Guid Id { get; } = Guid.NewGuid();

	protected GameBaseEntity(IGameState game, IBaseEntityInfo entity, DebugState debug) {
		Name = nameof(GameBaseEntity);

		AddChild(new CollisionShape2D {
			Position = new Vector2(0, -(float)Math.Floor(entity.Size.Y/2d)),
			Shape = new RectangleShape2D {
				Size = entity.Size
			},
			DebugColor = new Color(Colors.Green, 0.5f),
			Visible = debug.ShowCollisions
		});
		CollisionLayer = (uint) CollisionLayers.Player;
		CollisionMask = (uint) CollisionLayers.World;


		AddChild(_animation = new GameAnimation(this, debug));
		_animation.SetAnimation(entity.Animations);
		AddChild(new DebugPosition2D(debug));

		AddChild(_stateMachine = new EntityStateMachine(game, this, entity.InitialState, entity.States));
		_variables = new BaseEntityVariables(entity);
	}

	public virtual void TakeDamage(int amount) {

	}

	public virtual void Reset() {
		_stateMachine.Reset();
		_variables.Reset();
	}

	#region IEntityState
	public void SetFacing(int facing) {
		throw new NotImplementedException();
	}

	public Vector2 AreaPosition => Position;
	public abstract bool WasKilled { get; }
	public IVariables Variables => _variables;

	public void SetAnimation(Guid animationId) => _animation.Play(animationId);
	public void ChangeStateTo(Guid stateId) => _stateMachine.ChangeState(stateId);
	public void Despawn() {
		_animation.PlayNone();
		_stateMachine.ChangeState(Guid.Empty);
	}
	#endregion
}