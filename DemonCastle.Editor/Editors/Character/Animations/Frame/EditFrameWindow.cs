using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Frame;

public partial class EditFrameWindow : AcceptDialog {

	public EditFrameWindow(FrameInfo frame) {
		Name = nameof(EditFrameWindow);
		Title = "Edit Frame";
		MinSize = new Vector2I(200, 200);
		Exclusive = true;

		AddChild(new CharacterFrameDetails(frame));
	}
}