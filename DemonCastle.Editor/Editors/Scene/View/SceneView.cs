using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View;

public partial class SceneView : ControlView<ColorRect> {
	private readonly SceneInfo _scene;
	private ColorRect Background => Inner;
	private ElementsView Elements { get; }

	public SceneView(SceneInfo scene) {
		_scene = scene;
		Name = nameof(SceneView);

		Inner.AddChild(Elements = new ElementsView(scene));
		Elements.SetAnchorsPreset(LayoutPreset.FullRect);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Background.Color = _scene.BackgroundColor;
		Background.Size = _scene.Size;
	}
}