using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class EditFrameWindow : WindowDialog {
		protected PropertyCollection Properties { get; }
		
		public EditFrameWindow(FrameInfo frameInfo) {
			Name = nameof(EditFrameWindow);
			WindowTitle = "Edit Frame";
			RectMinSize = new Vector2(200, 200);
			PopupExclusive = true;

			AddChild(Properties = new PropertyCollection());
			
			Properties.AddFile("Source", frameInfo, frameInfo.Directory, f => f.SourceFile);
			Properties.AddString("Sprite", frameInfo, f => f.SpriteName);
			Properties.AddFloat("Duration", frameInfo, f => f.Duration);
		}
	}
}