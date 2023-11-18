using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Frame;

public partial class EditFrameWindow : AcceptDialog {
	private FrameInfo Frame { get; }

	protected PropertyCollection Properties { get; }
	protected SpriteReferenceProperty SpriteReference { get; }

	public EditFrameWindow(FrameInfo frame) {
		Frame = frame;

		Name = nameof(EditFrameWindow);
		Title = "Edit Frame";
		MinSize = new Vector2I(200, 200);
		Exclusive = true;

		AddChild(Properties = new PropertyCollection());

		var source = Properties.AddFile("Source", frame, frame.Directory, f => f.SourceFile, FileType.SpriteSources);
		source.FileSelected += Source_OnFileSelected;
		SpriteReference = Properties.AddSpriteReference("Sprite", frame, f => f.SpriteId, frame.SpriteDefinitions);
		Properties.AddFloat("Duration", frame, f => f.Duration);
	}

	private void Source_OnFileSelected(string file) {
		SpriteReference.LoadOptions(Frame.SpriteDefinitions);
	}
}