using DemonCastle.Editor.Windows.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public partial class EditFrameWindow : AcceptDialog {
		protected PropertyCollection Properties { get; }
		
		public EditFrameWindow(FrameInfo frameInfo) {
			Name = nameof(EditFrameWindow);
			Title = "Edit Frame";
			MinSize = new Vector2I(200, 200);
			Exclusive = true;

			AddChild(Properties = new PropertyCollection());
			
			Properties.AddFile("Source", frameInfo, frameInfo.Directory, f => f.SourceFile);
			Properties.AddString("Sprite2D", frameInfo, f => f.SpriteName);
			Properties.AddFloat("Duration", frameInfo, f => f.Duration);
		}
	}
}