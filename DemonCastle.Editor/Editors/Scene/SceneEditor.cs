using System.Collections.Generic;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Scene.View;
using DemonCastle.Editor.Icons;
using DemonCastle.Files;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	private HSplitContainer Split { get; }

	private VBoxContainer Left { get; }
	private TabContainer LeftTabs { get; }
	private InfoCollectionEditorByEnum<IElementInfo, ElementType> ElementList { get; }
	private InfoCollectionEditor<SceneEventInfo> EventsList { get; }

	private HSplitContainer Right { get; }
	private SceneItemEditor SceneItemEditor { get; }

	private static readonly Dictionary<ElementType, Texture2D> ElementTypeIconMap = new() {
		{ ElementType.ColorRect, IconTextures.ColorRectElementIcon },
		{ ElementType.HealthBar, IconTextures.HealthBarElementIcon },
		{ ElementType.Label, IconTextures.LabelElementIcon },
		{ ElementType.LevelView, IconTextures.LevelViewElementIcon },
		{ ElementType.Sprite, IconTextures.SpriteElementIcon },
		{ ElementType.OptionList, IconTextures.OptionListElementIcon }
	};

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
			LeftTabs.TabSelected += LeftTabs_OnTabSelected;

			LeftTabs.AddChild(ElementList = new InfoCollectionEditorByEnum<IElementInfo, ElementType>(scene.Elements,
								  ElementTypeIconMap, i => i.Type));
			ElementList.ItemSelected += ElementList_OnElementSelected;
			LeftTabs.SetTabTitle(0, "Elements");

			LeftTabs.AddChild(EventsList = new InfoCollectionEditor<SceneEventInfo>(scene.Events));
			EventsList.ItemSelected += EventList_OnEventSelected;
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

	private void LeftTabs_OnTabSelected(long tab) {
		ElementList.ClearSelection();
		EventsList.ClearSelection();
		SceneItemEditor.Clear();
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