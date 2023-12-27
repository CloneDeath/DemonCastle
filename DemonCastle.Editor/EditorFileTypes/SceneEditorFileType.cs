using System.Collections.Generic;
using DemonCastle.Editor.Editors;
using DemonCastle.Editor.Editors.Scene;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.FileTypes;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.EditorFileTypes;

public class SceneEditorFileType : SceneFileType, IEditorFileType {
	public Texture2D Icon => IconTextures.SceneIcon;
	public object CreateFileInstance() => new SceneFile {
		Name = "scene",
		Elements = new List<Element> {
			new LabelElement {
				Text = "Hello World",
				Color = Colors.White,
				Region = new Rect2I(Vector2I.Zero, Vector2I.One)
			}
		}
	};

	public BaseEditor GetEditor(ProjectInfo project, FileNavigator file) => new SceneEditor(file.ToSceneInfo());
}