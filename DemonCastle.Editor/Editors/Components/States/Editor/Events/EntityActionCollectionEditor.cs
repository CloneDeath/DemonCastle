using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : HSplitContainer {
	private readonly ProjectInfo _project;
	private Control Left { get; }
	private Control Right { get; }

	public IBaseEntityInfo? Entity { get; set; }

	public EntityActionCollectionEditor(ProjectInfo project) {
		_project = project;
		Name = nameof(EntityActionCollectionEditor);

		AddChild(Left = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AddChild(Right = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Load(EntityActionInfoCollection actionSet) {
		Clear();

		var list = new InfoCollectionEditor<EntityActionInfo>(actionSet);
		Left.AddChild(list);
		list.SetAnchorsPreset(LayoutPreset.FullRect);
		list.ItemSelected += List_OnItemSelected;
	}

	public void Clear() {
		foreach (var child in Left.GetChildren().Concat(Right.GetChildren())) {
			child.QueueFree();
		}
	}

	private void List_OnItemSelected(EntityActionInfo? obj) {
		foreach (var child in Right.GetChildren()) {
			child.QueueFree();
		}

		if (obj == null) return;
		if (Entity == null) return;

		var editor = new EntityActionEditor(_project, Entity, obj);
		Right.AddChild(editor);
		editor.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}