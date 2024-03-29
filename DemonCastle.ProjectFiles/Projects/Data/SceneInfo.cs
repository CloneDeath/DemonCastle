using DemonCastle.Files;
using DemonCastle.Files.Common;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;
using Godot;
using SceneState = DemonCastle.ProjectFiles.State.SceneState;

namespace DemonCastle.ProjectFiles.Projects.Data;

public class SceneInfo : FileInfo<SceneFile> {
	public SceneInfo(FileNavigator<SceneFile> file) : base(file) {
		Elements = new ElementInfoCollection(file, Resource.Elements);
		Events = new SceneEventInfoCollection(file, Resource.Events);
	}

	public string Name {
		get => Resource.Name;
		set {
			Resource.Name = value;
			Save();
			OnPropertyChanged();
		}
	}

	public Vector2I Size {
		get => Resource.Size.ToVector2I();
		set {
			Resource.Size = value.ToSize();
			Save();
			OnPropertyChanged();
		}
	}

	public Color BackgroundColor {
		get => Resource.BackgroundColor.ToColor();
		set {
			Resource.BackgroundColor = value.ToColorAlphaData();
			Save();
			OnPropertyChanged();
		}
	}

	public ElementInfoCollection Elements { get; }
	public SceneEventInfoCollection Events { get; }

	public void TriggerEvents(IGameState gameState, SceneState sceneState) {
		foreach (var sceneEvent in Events) {
			sceneEvent.TriggerEvent(gameState, sceneState);
		}
	}
}