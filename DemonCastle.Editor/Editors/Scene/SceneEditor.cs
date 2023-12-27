using DemonCastle.Editor.Editors.Scene.Elements.List;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	private HSplitContainer Split { get; }

	private VBoxContainer Left { get; }
	private HSplitContainer Right { get; }

	public SceneEditor(SceneInfo scene) {
		TabText = scene.FileName;

		AddChild(Split = new HSplitContainer());
		Split.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		Split.AddChild(Left = new VBoxContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		{
			Left.AddChild(new SceneDetails(scene));
			Left.AddChild(new ElementList(scene.Elements) {
				SizeFlagsVertical = SizeFlags.ExpandFill
			});
		}

		Split.AddChild(Right = new HSplitContainer());
		{
			Right.AddChild(new VBoxContainer {
				CustomMinimumSize = new Vector2(300, 300)
			});
			Right.AddChild(new ColorRect {
				Color = Colors.Red,
				CustomMinimumSize = new Vector2(300, 300)
			});
		}
	}
}