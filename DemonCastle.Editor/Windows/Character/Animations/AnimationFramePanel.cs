using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Windows.Character.Animations {
	public class AnimationFramePanel : PanelContainer {
		protected FrameInfo FrameInfo { get; }
		protected Label DurationLabel { get; }
		protected VBoxContainer Items { get; }
		protected Button EditButton { get; }
		protected Button DeleteButton { get; }
		protected CenterContainer TextureContainer { get; }
		
		protected EditFrameWindow EditWindow { get; }
		
		public AnimationFramePanel(FrameInfo frameInfo) {
			FrameInfo = frameInfo;
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

			Items.AddChild(TextureContainer = new CenterContainer());

			Items.AddChild(DurationLabel = new Label());
			
			Items.AddChild(EditButton = new Button {
				Text = "Edit"
			});
			EditButton.Connect("pressed", this, nameof(OnEditButtonClicked));
			
			Items.AddChild(DeleteButton = new Button {
				Text = "Delete"
			});
			DeleteButton.Connect("pressed", this, nameof(OnDeleteButtonClicked));

			AddChild(EditWindow = new EditFrameWindow(frameInfo));
			EditWindow.Connect("confirmed", this, nameof(OnEditWindowClosed));
			
			LoadFrameInfo();
		}

		protected void LoadFrameInfo() {
			DurationLabel.Text = $"{FrameInfo.Duration}s";
			
			foreach (Node child in TextureContainer.GetChildren()) {
				child.QueueFree();
			}
			var texture = FrameInfo.TextureRect;
			TextureContainer.AddChild(texture);
			texture.StretchMode = TextureRect.StretchModeEnum.KeepCentered;
		}

		protected void OnEditWindowClosed() {
			LoadFrameInfo();
		}

		protected void OnEditButtonClicked() {
			EditWindow.PopupCentered();
		}

		protected void OnDeleteButtonClicked() {
			FrameInfo.Delete();
			QueueFree();
		}
	}
}