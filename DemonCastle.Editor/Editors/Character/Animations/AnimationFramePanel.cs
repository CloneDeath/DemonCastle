using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations;

public partial class AnimationFramePanel : PanelContainer {
	protected FrameInfo FrameInfo { get; }
	protected Label DurationLabel { get; }
	protected VBoxContainer Items { get; }
	protected Button EditButton { get; }
	protected Button DeleteButton { get; }
	protected SpriteDefinitionView SpriteDefinitionView { get; }

	protected Frame.EditFrameWindow EditWindow { get; }

	public AnimationFramePanel(FrameInfo frameInfo) {
		FrameInfo = frameInfo;
		CustomMinimumSize = new Vector2(50, 50);

		AddChild(Items = new VBoxContainer {
			AnchorRight = 1,
			AnchorBottom = 1,
			OffsetRight = 0,
			OffsetBottom = 0
		});

		Items.AddChild(new Label {
			Text = $"{FrameInfo.Index} - {frameInfo.Sprite.Name}"
		});

		Items.AddChild(SpriteDefinitionView = new SpriteDefinitionView(FrameInfo.SpriteDefinition));

		Items.AddChild(DurationLabel = new Label());

		Items.AddChild(EditButton = new Button {
			Text = "Edit"
		});
		EditButton.Pressed += OnEditButtonClicked;

		Items.AddChild(DeleteButton = new Button {
			Text = "Delete"
		});
		DeleteButton.Pressed += OnDeleteButtonClicked;

		AddChild(EditWindow = new Frame.EditFrameWindow(frameInfo));
		EditWindow.Confirmed += OnEditWindowClosed;

		LoadFrameInfo();
	}

	protected void LoadFrameInfo() {
		DurationLabel.Text = $"{FrameInfo.Duration}s";
		SpriteDefinitionView.Reload();
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