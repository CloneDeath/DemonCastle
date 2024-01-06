using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventEditor : PropertyCollection {
	public SceneEventEditor(IFileInfo file, SceneEventInfo sceneEvent) {
		Name = nameof(SceneEventEditor);

		AddString("Name", sceneEvent, e => e.Name);
		AddChild(new SceneEventConditionEditor(sceneEvent.When) {
			//SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		AddChild(new SceneEventActionCollectionEditor(file, sceneEvent.Then) {
			//SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
	}
}