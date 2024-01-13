using DemonCastle.Files.Actions;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents.Types;

public class SceneChangeActionInfo : BaseInfo<SceneActionData> {
	public SceneChangeActionInfo(IFileNavigator file, SceneActionData data) : base(file, data) { }

	public bool IsSet {
		get => Data.Scene != null;
		set {
			Data.Clear();
			Data.Scene = value ? Data.Scene ?? new SceneChangeActionData() : null;
			Save();
			OnPropertyChanged();
		}
	}

	public string? Set {
		get => Data.Scene?.Set;
		set {
			Data.Clear();
			Data.Scene = new SceneChangeActionData {
				Set = value
			};
			Save();
			OnPropertyChanged();
		}
	}

	public string? Push {
		get => Data.Scene?.Push;
		set {
			Data.Clear();
			Data.Scene = new SceneChangeActionData {
				Push = value
			};
			Save();
			OnPropertyChanged();
		}
	}

	public int? Pop {
		get => Data.Scene?.Pop;
		set {
			Data.Clear();
			Data.Scene = new SceneChangeActionData {
				Pop = value
			};
			Save();
			OnPropertyChanged();
		}
	}

	public void TriggerAction(IGameState gameState) {
		if (!IsSet) return;
		if (Set != null) gameState.SetScene(File.GetScene(Set));
		else if (Push != null) gameState.PushScene(File.GetScene(Push));
		else if (Pop != null) gameState.PopScene(Pop.Value);
		else throw new IncompleteDataException(File.FilePath);
	}

	public override string ToString() {
		return !IsSet ? "<null>"
			   : Set != null ? $"Set: {Set}"
			   : Push != null ? $"Push: {Push}"
			   : Pop != null ? $"Pop: {Pop}"
			   : "Invalid Action";
	}
}