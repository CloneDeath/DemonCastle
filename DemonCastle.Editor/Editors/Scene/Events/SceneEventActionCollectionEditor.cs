using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;

namespace DemonCastle.Editor.Editors.Scene.Events;

public partial class SceneEventActionCollectionEditor : ActionCollectionEditor<SceneEventActionInfo> {
	private readonly IFileInfo _file;
	private readonly ProjectInfo _project;

	public SceneEventActionCollectionEditor(IFileInfo file, ProjectInfo project, SceneEventActionInfoCollection then) {
		_file = file;
		_project = project;
		Name = nameof(SceneEventActionCollectionEditor);

		_actionSet = then;
	}

	protected override void AddAction(SceneEventActionInfo action) {
		if (_actionSet == null) return;
		Actions.AddChild(new SceneEventActionEditor(_file, _project, action, _actionSet));
	}
}