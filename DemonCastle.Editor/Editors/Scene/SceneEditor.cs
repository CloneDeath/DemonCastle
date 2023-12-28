using DemonCastle.Editor.Editors.Scene.Elements.Editor;
using DemonCastle.Editor.Editors.Scene.Elements.List;
using DemonCastle.Editor.Editors.Scene.View;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	private HSplitContainer Split { get; }

	private VBoxContainer Left { get; }
	private ElementList ElementList { get; }

	private HSplitContainer Right { get; }
	private ElementEditor ElementEditor { get; }

	public SceneEditor(SceneInfo scene) {
		TabText = scene.FileName;

		AddChild(Split = new HSplitContainer());
		Split.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		Split.AddChild(Left = new VBoxContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		{
			Left.AddChild(new SceneDetails(scene));
			Left.AddChild(ElementList = new ElementList(scene.Elements) {
				SizeFlagsVertical = SizeFlags.ExpandFill
			});
			ElementList.ElementSelected += ElementList_OnElementSelected;
		}

		Split.AddChild(Right = new HSplitContainer());
		{
			Right.AddChild(ElementEditor = new ElementEditor(scene) {
				CustomMinimumSize = new Vector2(425, 300)
			});
			Right.AddChild(new SceneView(scene) {
				CustomMinimumSize = new Vector2(300, 300)
			});
		}
	}

	private void ElementList_OnElementSelected(IElementInfo obj) {
		ElementEditor.LoadElement(obj);
	}
}