using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.SpriteGrid.View;

public partial class SpriteGridTextureView : TextureView {
	private SpriteGridDataInfo? _selectedSprite;

	protected SpriteGridInfo SpriteGridInfo { get; }
	protected Outline Outline { get; }

	public SpriteGridTextureView(SpriteGridInfo spriteGridInfo) {
		SpriteGridInfo = spriteGridInfo;
		Inner.AddChild(Outline = new Outline {
			Visible = false
		});
		Texture = SpriteGridInfo.Texture;
	}

	public override void _EnterTree() {
		base._EnterTree();
		SpriteGridInfo.PropertyChanged += SpriteGridInfo_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		SpriteGridInfo.PropertyChanged -= SpriteGridInfo_OnPropertyChanged;
		if (_selectedSprite != null) _selectedSprite.PropertyChanged -= SelectedSprite_OnPropertyChanged;
	}

	private void SpriteGridInfo_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Texture = SpriteGridInfo.Texture;
	}

	public SpriteGridDataInfo? SelectedSprite {
		get => _selectedSprite;
		set {
			if (_selectedSprite != null) _selectedSprite.PropertyChanged -= SelectedSprite_OnPropertyChanged;
			_selectedSprite = value;
			if (_selectedSprite != null) _selectedSprite.PropertyChanged += SelectedSprite_OnPropertyChanged;
			Outline.Visible = value != null;
			Outline.Size = value?.Region.Size ?? Vector2.Zero;
			Outline.Position = value?.Region.Position ?? Vector2.Zero;
		}
	}

	private void SelectedSprite_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (_selectedSprite == null) return;
		Outline.Size = _selectedSprite.Region.Size;
		Outline.Position = _selectedSprite.Region.Position;
	}
}