using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;
using TileData = DemonCastle.Files.TileData;

namespace DemonCastle.ProjectFiles.Projects.Data.Levels.Tiles;

public class StairInfo : BaseInfo<TileData> {
	public StairInfo(IFileNavigator file, TileData tileData) : base(file, tileData) {
	}

	public bool Enabled {
		get => Data.Stairs != null;
		set {
			if (value) {
				Data.Stairs ??= new StairData {
					Start = Vector2.Down,
					End = Vector2.Right
				};
			} else {
				Data.Stairs = null;
			}
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Start));
			OnPropertyChanged(nameof(End));
		}
	}

	public Vector2 Start {
		get => Data.Stairs?.Start ?? Vector2.Down;
		set {
			Data.Stairs ??= new StairData();
			Data.Stairs.Start = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Enabled));
			OnPropertyChanged(nameof(End));
		}
	}

	public Vector2 End {
		get => Data.Stairs?.End ?? Vector2.Right;
		set {
			Data.Stairs ??= new StairData();
			Data.Stairs.End = value;
			Save();
			OnPropertyChanged();
			OnPropertyChanged(nameof(Enabled));
			OnPropertyChanged(nameof(Start));
		}
	}
}