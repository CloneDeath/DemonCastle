using System;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class EntityActionInfo : BaseInfo<EntityActionData>, IListableInfo {
	public EntityActionInfo(IFileNavigator file, EntityActionData data) : base(file, data) {
		SpawnItem = new ActionSpawnItemInfo(file, data);
		SpawnMonster = new ActionSpawnMonsterInfo(file, data);
	}

	public string ListLabel =>
		Face != null ? $"Face {Face}"
		: Move != null ? $"Move {Move}"
		: Self != null ? $"Self {Self}"
		: SpawnItem.IsSet ? SpawnItem.ListLabel
		: SpawnMonster.IsSet ? SpawnMonster.ListLabel
		: "<Empty>";

	public FaceAction? Face {
		get => Data.Face;
		set {
			Data.Clear();
			if (SaveField(ref Data.Face, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public MoveAction? Move {
		get => Data.Move;
		set {
			Data.Clear();
			if (SaveField(ref Data.Move, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public SelfAction? Self {
		get => Data.Self;
		set {
			Data.Clear();
			if (SaveField(ref Data.Self, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public ActionSpawnItemInfo SpawnItem { get; }
	public ActionSpawnMonsterInfo SpawnMonster { get; }

	public void Execute(IGameState game, IEntityState entity) {
		if (Face != null) {
			var face = Face switch {
				FaceAction.Left => -1,
				FaceAction.Right => 1,
				FaceAction.TowardsClosestPlayer => game.Player.Position.X < entity.AreaPosition.X ? -1 : 1,
				FaceAction.AwayFromClosestPlayer => game.Player.Position.X < entity.AreaPosition.X ? 1 : -1,
				_ => throw new NotSupportedException()
			};
			entity.SetFacing(face);
		} else if (Move != null) {
			throw new NotImplementedException();
		} else if (Self != null) {
			throw new NotImplementedException();
		} else if (SpawnItem.IsSet) {
			throw new NotImplementedException();
		} else if (SpawnMonster.IsSet) {
			throw new NotImplementedException();
		}
	}
}