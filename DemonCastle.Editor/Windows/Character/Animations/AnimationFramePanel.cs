using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationFramePanel : PanelContainer {
		protected VBoxContainer Items { get; }
		public AnimationFramePanel(FrameInfo frameInfo) {
			RectMinSize = new Vector2(50, 50);
			
			AddChild(Items = new VBoxContainer {
				AnchorRight = 1,
				AnchorBottom = 1,
				MarginRight = 0,
				MarginBottom = 0,
			});
			
			Items.AddChild(new Label {
				Text = frameInfo.Index.ToString()
			});

			var texture = frameInfo.TextureRect;
			Items.AddChild(texture);
			texture.StretchMode = TextureRect.StretchModeEnum.KeepCentered;
			
			Items.AddChild(new Label {
				Text = $"{frameInfo.Duration}s"
			});
		}
	}
}