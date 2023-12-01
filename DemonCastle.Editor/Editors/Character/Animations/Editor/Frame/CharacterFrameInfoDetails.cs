using System.ComponentModel;
using DemonCastle.Editor.Editors.Animations.Editor.Frames;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Editors.Properties.Vector;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameInfoDetails : PropertyCollection {
	private readonly FrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }
	protected CharacterFrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	public IFrameInfo? FrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public CharacterFrameInfoDetails(IFileInfo character) {
		Name = nameof(CharacterFrameInfoDetails);

		AddFloat("Duration", _proxy, p => p.Duration);
		AddFile("Source", _proxy, character.Directory, p => p.SourceFile, FileType.SpriteSources);
		SpriteReference = AddSpriteReference("Sprite", _proxy, p => p.SpriteId, _proxy.SpriteDefinitions);

		AddAnchor("Anchor", _proxy, p => p.Anchor);
		AddVector2I("Offset", _proxy, p => p.Offset, new Vector2IPropertyOptions { AllowNegative = true });

		AddBoolean("Weapon Enabled", _proxy, p => p.WeaponEnabled);
		AddString("Weapon Animation", _proxy, p => p.WeaponAnimation);
		AddVector2I("Weapon Position", _proxy, p => p.WeaponPosition);

		AddChild(FrameInfoView = new CharacterFrameInfoView {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		FrameInfoView.Load(_proxy);

		AddChild(DeleteButton = new Button {
			Text = "Delete Frame"
		});
		DeleteButton.Pressed += DeleteButton_OnPressed;

		_proxy.PropertyChanged += Proxy_OnPropertyChanged;
	}

	private void Proxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		SpriteReference.LoadOptions(_proxy.SpriteDefinitions);
		SpriteReference.PropertyValue = _proxy.SpriteId;
	}

	private void DeleteButton_OnPressed() {
		_proxy.Delete();
	}
}