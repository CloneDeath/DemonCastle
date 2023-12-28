using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View;

public partial class SceneView : ControlView<ColorRect> {
	private readonly SceneInfo _scene;
	private ColorRect Background => Inner;

	public SceneView(SceneInfo scene) {
		_scene = scene;
		Name = nameof(SceneView);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Background.Color = _scene.BackgroundColor;
		Background.Size = _scene.Size;
	}
}