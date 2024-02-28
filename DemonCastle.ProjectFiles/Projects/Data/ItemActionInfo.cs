using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class ItemActionInfo : BaseInfo<ItemActionData>, IListableInfo {
	public ItemActionInfo(IFileNavigator file, ItemActionData data) : base(file, data) {
		Player = new PlayerActionInfo(file, data);
	}

	public string ListLabel => Player.IsSet ? Player.ListLabel
								   : "<Empty>";

	public PlayerActionInfo Player { get; }

	public void Execute(IGameState game) {
		if (Player.IsSet) Player.Execute(game);
		else throw new IncompleteDataException(File.FilePath);
	}
}

public class PlayerActionInfo : BaseInfo<ItemActionData>, IListableInfo {
	public PlayerActionInfo(IFileNavigator file, ItemActionData data) : base(file, data) { }
	public string ListLabel => RecoverHp.HasValue ? $"Player Recover {RecoverHp} Hp"
							   : RecoverMp.HasValue ? $"Player Recover {RecoverMp} Mp"
							   : "<Empty>";

	public bool IsSet {
		get => Data.Player != null;
		set {
			if (value) {
				var player = Data.Player ?? new PlayerActionData();
				Data.Clear();
				Data.Player = player;
			} else {
				Data.Player = null;
			}
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
			OnPropertyChanged(nameof(RecoverHp));
			OnPropertyChanged(nameof(RecoverMp));
		}
	}

	public int? RecoverHp {
		get => Data.Player?.RecoverHp;
		set {
			Data.Clear();
			Data.Player = new PlayerActionData {
				RecoverHp = value
			};
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public int? RecoverMp {
		get => Data.Player?.RecoverMp;
		set {
			Data.Clear();
			Data.Player = new PlayerActionData {
				RecoverMp = value
			};
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(ListLabel));
		}
	}

	public void Execute(IGameState game) {
		if (RecoverHp.HasValue) game.Player.RecoverHp(RecoverHp.Value);
		else if (RecoverMp.HasValue) game.Player.RecoverMp(RecoverMp.Value);
		else throw new IncompleteDataException(File.FilePath);
	}
}