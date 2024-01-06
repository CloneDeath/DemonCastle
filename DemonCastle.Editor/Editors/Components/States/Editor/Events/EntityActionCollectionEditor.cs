using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionCollectionEditor : HSplitContainer {
	public EntityActionCollectionEditor() {
		Name = nameof(EntityActionCollectionEditor);
	}

	public void Load(EntityActionInfoCollection actionSet) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		AddChild(new EnumerableInfoList<EntityActionInfo>(actionSet));
	}
}