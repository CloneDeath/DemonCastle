using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : HSplitContainer {
	private readonly ProjectInfo _project;
	private readonly IBaseEntityInfo _entity;
	private Control Left { get; }
	private Control Right { get; }

	public EntityActionCollectionEditor(ProjectInfo project, IBaseEntityInfo entity) {
		_project = project;
		_entity = entity;
		Name = nameof(EntityActionCollectionEditor);

		AddChild(Left = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
		AddChild(Right = new MarginContainer {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Clear() {
		foreach (var child in Left.GetChildren().Concat(Right.GetChildren())) {
			child.QueueFree();
		}
	}

	public void Load(EntityActionInfoCollection actionSet) {
		Clear();

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

		var editor = new EntityActionEditor(_project, _entity, obj);
		Right.AddChild(editor);
		editor.SetAnchorsPreset(LayoutPreset.FullRect);
	}
}