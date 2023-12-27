using DemonCastle.Editor.Editors.Scene.Elements.List;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	private VBoxContainer Left { get; }

	public SceneEditor(SceneInfo scene) {
		TabText = scene.FileName;

		AddChild(Left = new VBoxContainer());
		Left.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		Left.AddChild(new SceneDetails(scene));
		Left.AddChild(new ElementList(scene.Elements) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}
}