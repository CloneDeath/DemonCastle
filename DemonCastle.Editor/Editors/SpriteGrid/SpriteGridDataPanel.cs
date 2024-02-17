using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid;

public partial class SpriteGridDataPanel : PanelContainer {
	protected SpriteGridDataInfo SpriteData { get; }
	protected PropertyCollection Properties { get; }
	protected TextureRect Preview { get; }
	protected Button DeleteButton { get; }
	protected DeleteSpriteDataDialog DeleteConfirmation { get; }
	public SpriteGridDataPanel(SpriteGridDataInfo spriteData) {
		SpriteData = spriteData;

		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", spriteData, x => x.Name);
		Properties.AddInteger("X", spriteData, x => x.X);
		Properties.AddInteger("Y", spriteData, x => x.Y);
		Properties.AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);
		Properties.AddBoolean("Flip Vertical", spriteData, x => x.FlipVertical);
		Properties.AddChild(Preview = new SpriteDefinitionView(spriteData) {
			StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered
		});
		Properties.AddChild(DeleteButton = new Button {
			Text = "Delete"
		});
		DeleteButton.Pressed += OnDeleteButtonPressed;

		AddChild(DeleteConfirmation = new DeleteSpriteDataDialog());
		DeleteConfirmation.Confirmed += OnDeleteConfirmed;
	}

	protected void OnDeleteButtonPressed() {
		DeleteConfirmation.Popup();
	}

	protected void OnDeleteConfirmed() {
		SpriteData.Remove();
		QueueFree();
	}
}