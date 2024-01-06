using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : HSplitContainer {
	private Control Left { get; }
	private Control Right { get; }

	public EntityActionCollectionEditor() {
		Name = nameof(EntityActionCollectionEditor);

		AddChild(Left = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AddChild(Right = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Load(EntityActionInfoCollection actionSet) {
		foreach (var child in Left.GetChildren().Concat(Right.GetChildren())) {
			child.QueueFree();
		}

		var list = new EnumerableInfoList<EntityActionInfo>(actionSet);
		Left.AddChild(list);
		list.SetAnchorsPreset(LayoutPreset.FullRect);
		list.ItemSelected += List_OnItemSelected;
	}

	private void List_OnItemSelected(EntityActionInfo? obj) {
		foreach (var child in Right.GetChildren()) {
			child.QueueFree();
		}

		if (obj == null) return;

		var editor = new EntityActionEditor(obj);
		Right.AddChild(editor);
		editor.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}