using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Game.Scenes;

public partial class GameScene : Control {
	public GameScene() {
		Name = nameof(GameScene);
	}

	public void Load(SceneInfo scene) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		AddChild(new ColorRect {
			Size = scene.Size,
			Color = scene.BackgroundColor
		});
		AddChild(new ElementsView(scene));
	}
}