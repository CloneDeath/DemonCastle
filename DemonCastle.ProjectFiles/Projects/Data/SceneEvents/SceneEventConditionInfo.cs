using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventConditionInfo : BaseInfo<SceneEventConditionData> {
	public SceneEventConditionInfo(IFileNavigator file, SceneEventConditionData data) : base(file, data) { }

	private void Clear() {
		Data.And = null;
		Data.Or = null;
		Data.AnyInput = null;
		Data.Input = null;
	}

	public SceneEventConditionData[]? And {
		get => Data.And;
		set {
			Clear();
			Data.And = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneEventConditionData[]? Or {
		get => Data.Or;
		set {
			Clear();
			Data.Or = value;
			Save();
			OnPropertyChanged();
		}
	}

	public KeyState? AnyInput {
		get => Data.AnyInput;
		set {
			Clear();
			Data.AnyInput = value;
			Save();
			OnPropertyChanged();
		}
	}

	public InputConditionData? Input {
		get => Data.Input;
		set {
			Clear();
			Data.Input = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneChangeEvent? ThisScene {
		get => Data.ThisScene;
		set {
			Clear();
			Data.ThisScene = value;
			Save();
			OnPropertyChanged();
		}
	}
}