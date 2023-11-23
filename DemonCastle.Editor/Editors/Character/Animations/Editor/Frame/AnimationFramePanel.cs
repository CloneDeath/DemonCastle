using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class AnimationFramePanel : PanelContainer {
	protected CharacterFrameInfo FrameInfo { get; }

	protected VBoxContainer Items { get; }
	protected Label FrameInfoLabel { get; }
	protected Label DurationLabel { get; }
	protected Button EditButton { get; }
	protected Button DeleteButton { get; }
	protected SpriteDefinitionView SpriteDefinitionView { get; }

	protected EditFrameWindow EditWindow { get; }

	public AnimationFramePanel(CharacterFrameInfo frameInfo) {
		FrameInfo = frameInfo;

		Name = nameof(AnimationFramePanel);
		CustomMinimumSize = new Vector2(50, 50);

		AddChild(Items = new VBoxContainer());
		Items.SetAnchorsPreset(LayoutPreset.FullRect);

		Items.AddChild(FrameInfoLabel = new Label());

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

		AddChild(EditWindow = new EditFrameWindow(frameInfo));
		EditWindow.Confirmed += OnEditWindowClosed;

		LoadFrameInfo();
	}

	protected void LoadFrameInfo() {
		FrameInfoLabel.Text = $"{FrameInfo.Index} - {FrameInfo.SpriteDefinition.Name}";
		DurationLabel.Text = $"{FrameInfo.Duration}s";
		SpriteDefinitionView.Load(FrameInfo.SpriteDefinition);
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