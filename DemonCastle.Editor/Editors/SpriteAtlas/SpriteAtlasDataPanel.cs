using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteAtlas;

public partial class SpriteAtlasDataPanel : PanelContainer {
	private readonly SpriteAtlasDataInfo _spriteData;
	protected PropertyCollection Properties { get; }
	protected CenterContainer TextureContainer { get; }

	public SpriteAtlasDataPanel(SpriteAtlasDataInfo spriteData) {
		_spriteData = spriteData;
		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", spriteData, x => x.Name);
		Properties.AddRect2I("Region", spriteData, x => x.Region);
		Properties.AddBoolean("Flip Horizontal", spriteData, x => x.FlipHorizontal);

		Properties.AddChild(TextureContainer = new CenterContainer());
		TextureContainer.AddChild(new SpriteDefinitionTextureRect(spriteData));

		spriteData.PropertyChanged += SpriteData_OnPropertyChanged;
	}

	private void SpriteData_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		foreach (var child in TextureContainer.GetChildren()) {
			child.QueueFree();
		}
		TextureContainer.AddChild(new SpriteDefinitionTextureRect(_spriteData));
	}
}