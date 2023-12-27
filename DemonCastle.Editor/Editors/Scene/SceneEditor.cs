using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneEditor : BaseEditor {
	public override Texture2D TabIcon => EditorFileType.Scene.Icon;
	public override string TabText { get; }

	public SceneEditor(SceneInfo scene) {
		TabText = scene.FileName;
	}
}