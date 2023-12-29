using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventActionInfo : BaseInfo<SceneEventActionData> {
	public SceneEventActionInfo(IFileNavigator file, SceneEventActionData data) : base(file, data) {
		Scene = new SceneChangeActionInfo(file, data);
	}

	private void Clear() {
		Data.Scene = null;
		Data.SetCharacter = null;
		Data.SetLevel = null;
	}

	public SceneChangeActionInfo Scene { get; }

	public string? SetCharacter {
		get => Data.SetCharacter;
		set {
			Clear();
			Data.SetCharacter = value;
			Save();
			OnPropertyChanged();
		}
	}

	public string? SetLevel {
		get => Data.SetLevel;
		set {
			Clear();
			Data.SetLevel = value;
			Save();
			OnPropertyChanged();
		}
	}

	public void TriggerAction(IGameState game) {
		Scene.TriggerAction(game);
		if (SetCharacter != null) game.SetCharacter(File.GetCharacter(SetCharacter));
		if (SetLevel != null) game.SetLevel(File.GetLevel(SetLevel));
	}
}

public class SceneChangeActionInfo : BaseInfo<SceneEventActionData> {
	public SceneChangeActionInfo(IFileNavigator file, SceneEventActionData data) : base(file, data) { }

	public bool IsSet {
		get => Data.Scene != null;
		set {
			Data.Scene = value ? Data.Scene ?? new SceneChangeActionData() : null;
			Save();
			OnPropertyChanged();
		}
	}

	public string? Set {
		get => Data.Scene?.Set;
		set {
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
		if (Push != null) gameState.PushScene(File.GetScene(Push));
		if (Pop != null) gameState.PopScene(Pop.Value);
	}
}