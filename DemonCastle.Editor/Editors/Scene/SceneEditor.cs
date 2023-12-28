using DemonCastle.Editor.Editors.Scene.Events;
using DemonCastle.Editor.Editors.Scene.View;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;
using ElementList = DemonCastle.Editor.Editors.Scene.Elements.ElementList;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	private HSplitContainer Split { get; }

	private VBoxContainer Left { get; }
	private TabContainer LeftTabs { get; }
	private ElementList ElementList { get; }
	private SceneEventsList EventsList { get; }

	private HSplitContainer Right { get; }
	private SceneItemEditor SceneItemEditor { get; }

	public SceneEditor(SceneInfo scene) {
		TabText = scene.FileName;

		AddChild(Split = new HSplitContainer());
		Split.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		Split.AddChild(Left = new VBoxContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		{
			Left.AddChild(new SceneDetails(scene));

			Left.AddChild(LeftTabs = new TabContainer {
				SizeFlagsVertical = SizeFlags.ExpandFill
			});

			LeftTabs.AddChild(ElementList = new ElementList(scene.Elements));
			ElementList.ElementSelected += ElementList_OnElementSelected;
			LeftTabs.SetTabTitle(0, "Elements");

			LeftTabs.AddChild(EventsList = new SceneEventsList(scene.Events));
			EventsList.SceneEventSelected += EventList_OnEventSelected;
			LeftTabs.SetTabTitle(1, "Events");
		}

		Split.AddChild(Right = new HSplitContainer());
		{
			Right.AddChild(SceneItemEditor = new SceneItemEditor(scene) {
				CustomMinimumSize = new Vector2(425, 300)
			});
			Right.AddChild(new SceneView(scene) {
				CustomMinimumSize = new Vector2(300, 300)
			});
		}
	}

	private void EventList_OnEventSelected(SceneEventInfo? obj) {
		if (obj == null) {
			SceneItemEditor.Clear();
		} else {
			SceneItemEditor.LoadEvent(obj);
		}
	}

	private void ElementList_OnElementSelected(IElementInfo? obj) {
		if (obj == null) {
			SceneItemEditor.Clear();
		} else {
			SceneItemEditor.LoadElement(obj);
		}
	}
}