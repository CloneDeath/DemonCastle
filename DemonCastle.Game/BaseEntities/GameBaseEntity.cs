using System;
using DemonCastle.Game.Animations;
using DemonCastle.Game.DebugNodes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Game.BaseEntities;

public abstract partial class GameBaseEntity : PlayerEntityCommon, IEntityState {
	private readonly BaseEntityVariables _variables;
	private readonly GameAnimation _animation;
	private readonly EntityStateMachine _stateMachine;

	protected GameBaseEntity(IGameState game, IBaseEntityInfo entity, IGameLogger logger, DebugState debug)
		: base(game, logger, debug) {
		Name = nameof(GameBaseEntity);

		CollisionShape.Position = new Vector2(0, -(float)Math.Floor(entity.Size.Y / 2d));
		CollisionShape.Shape = new RectangleShape2D {
			Size = entity.Size
		};

		AddChild(_animation = new GameAnimation(this, debug));
		_animation.SetAnimation(entity.Animations);

		AddChild(_stateMachine = new EntityStateMachine(game, this, entity.InitialState, entity.States));
		_variables = new BaseEntityVariables(entity);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Velocity = _moveDirection * MoveSpeed;
		StopMoving();
		MoveAndSlide();

		_animation.Scale = new Vector2(Facing, 1);
	}

	public virtual void Reset() {
		_variables.Reset();
		_stateMachine.Reset();
	}

	#region IEntityState
	public void SetFacing(int facing) => Facing = facing;
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