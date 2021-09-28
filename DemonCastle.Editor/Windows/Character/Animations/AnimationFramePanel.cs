using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationFramePanel : Panel {
		public AnimationFramePanel(FrameInfo frameInfo) {
			RectMinSize = new Vector2(50, 50);
			
			AddChild(new Label {
				Text = frameInfo.Index.ToString()
			});
		}
	}
}