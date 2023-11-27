using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameInfoDetails : PropertyCollection {
	private  readonly WeaponFrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }
	protected WeaponFrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	public WeaponFrameInfo? WeaponFrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public WeaponFrameInfoDetails(WeaponInfo weapon) {
		Name = nameof(WeaponFrameInfoDetails);

		AddFloat("Duration", _proxy, p => p.Duration);
		AddFile("Source", _proxy, weapon.Directory, p => p.SourceFile, FileType.SpriteSources);
		SpriteReference = AddSpriteReference("Sprite", _proxy, p => p.SpriteId, _proxy.SpriteDefinitions);

		AddVector2I("Anchor", _proxy, p => p.Anchor);
		AddVector2I("Offset", _proxy, p => p.Offset);

		AddChild(FrameInfoView = new WeaponFrameInfoView {
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