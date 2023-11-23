using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameInfoDetails : PropertyCollection {
	private readonly CharacterFrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }
	protected CharacterFrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	public CharacterFrameInfo? CharacterFrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public CharacterFrameInfoDetails(CharacterInfo character) {
		Name = nameof(CharacterFrameInfoDetails);

		AddFloat("Duration", _proxy, f => f.Duration);
		AddFile("Source", _proxy, character.Directory, f => f.SourceFile, FileType.SpriteSources);
		SpriteReference = AddSpriteReference("Sprite", _proxy, f => f.SpriteId, _proxy.SpriteDefinitions);

		AddBoolean("Weapon Enabled", _proxy, f => f.WeaponEnabled);
		AddString("Weapon Animation", _proxy, f => f.WeaponAnimation);
		AddVector2I("Weapon Position", _proxy, f => f.WeaponPosition);

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
		_proxy.DeleteFrame();
	}
}