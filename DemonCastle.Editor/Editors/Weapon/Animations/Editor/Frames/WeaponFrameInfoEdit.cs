using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameInfoEdit : PropertyCollection {
	private  readonly WeaponFrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }
	protected Button DeleteButton { get; }

	public WeaponFrameInfo? WeaponFrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponFrameInfoEdit(WeaponInfo weapon) {
		Name = nameof(WeaponFrameInfoEdit);

		AddFloat("Duration", _proxy, p => p.Duration);
		AddFile("Source", _proxy, weapon.Directory, p => p.SourceFile, FileType.SpriteSources);
		SpriteReference = AddSpriteReference("Sprite", _proxy, p => p.SpriteId, _proxy.SpriteDefinitions);
		AddVector2I("Origin", _proxy, p => p.Origin);
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