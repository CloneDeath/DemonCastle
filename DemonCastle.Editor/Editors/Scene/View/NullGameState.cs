using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.State;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View;

public class NullGameState : IGameState {
	public void SetCharacter(CharacterInfo character) {	}
	public void SetLevel(LevelInfo level) {	}
	public void PushScene(SceneInfo scene) { }
	public void PopScene(int number) {	}
	public void SetScene(SceneInfo scene) {	}

	public IInputState Input => new NullInputState();
	public Texture2D LevelView => new GradientTexture2D {
		Gradient = new Gradient {
			Colors = new[] {
				Colors.Silver,
				Colors.LightGray
			},
			Offsets = new[] {
				0f, 1f
			}
		}
	};
}

public class NullInputState : IInputState {
	public bool AnyInputIsInState(KeyState state) => false;
	public bool InputIsInState(PlayerAction action, KeyState state) => false;
}