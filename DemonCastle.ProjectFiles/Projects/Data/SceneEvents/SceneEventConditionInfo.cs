using DemonCastle.Files.Conditions;
using DemonCastle.Files.Conditions.Events;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Resources;
using DemonCastle.ProjectFiles.State;

namespace DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

public class SceneEventConditionInfo : BaseInfo<SceneEventConditionData> {
	public SceneEventConditionInfo(IFileNavigator file, SceneEventConditionData data) : base(file, data) { }

	/*
	public SceneEventConditionInfoCollection And {
		get => Data.And;
		set {
			Clear();
			Data.And = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneEventConditionInfoCollection? Or {
		get => Data.Or;
		set {
			Clear();
			Data.Or = value;
			Save();
			OnPropertyChanged();
		}
	}

	*/

	public KeyState? AnyInput {
		get => Data.AnyInput;
		set {
			Data.Clear();
			Data.AnyInput = value;
			Save();
			OnPropertyChanged();
		}
	}

	public InputConditionData? Input {
		get => Data.Input;
		set {
			Data.Clear();
			Data.Input = value;
			Save();
			OnPropertyChanged();
		}
	}

	public SceneChangeEvent? ThisScene {
		get => Data.ThisScene;
		set {
			Data.Clear();
			Data.ThisScene = value;
			Save();
			OnPropertyChanged();
		}
	}

	public bool IsConditionMet(IGameState game, SceneState scene) {
		if ((scene.OnEnter || scene.OnExit) && ThisScene == null) {
			return false;
		}
		return (AnyInput != null && game.Input.AnyInputIsInState(AnyInput.Value)) ||
			   (Input != null && game.Input.InputIsInState(Input.Action, Input.State)) ||
			   (ThisScene != null && IsThisSceneConditionMet(ThisScene.Value, scene));
	}

	private static bool IsThisSceneConditionMet(SceneChangeEvent thisScene, SceneState scene) {
		return thisScene switch {
			SceneChangeEvent.Enter => scene.OnEnter,
			SceneChangeEvent.Exit => scene.OnExit,
			_ => throw new InvalidEnumValueException<SceneChangeEvent>(thisScene)
		};
	}
}