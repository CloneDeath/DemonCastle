using System.ComponentModel;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;
using SpriteReferenceProperty = DemonCastle.Editor.Editors.Properties.Reference.SpriteReferenceProperty;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public partial class FrameInfoDetails : PropertyCollection {
	protected readonly FrameInfoProxy _proxy = new();

	protected SpriteReferenceProperty SpriteReference { get; }
	protected FrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	protected PropertyCollection HitBoxes { get; }
	protected PropertyCollection AdditionalProperties { get; }

	public IFrameInfo? FrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public FrameInfoDetails(IFileInfo file) {
		Name = nameof(FrameInfoDetails);

		AddFloat("Duration", _proxy, p => p.Duration);
		AddFile("Source", _proxy, file.Directory, p => p.SourceFile, FileType.SpriteSources);
		SpriteReference = AddSpriteReference("Sprite", _proxy, p => p.SpriteId, _proxy.SpriteDefinitions);
		AddOrigin("Origin", _proxy, p => p.Anchor, p => p.Offset);
		AddChild(HitBoxes = new PropertyCollection {
			Vertical = false
		});
		HitBoxes.AddNullableRect2I("HitBox", _proxy, p => p.HitBox);
		HitBoxes.AddNullableRect2I("HurtBox", _proxy, p => p.HurtBox);

		AddChild(AdditionalProperties = new PropertyCollection());

		AddChild(FrameInfoView = new FrameInfoView {
			SizeFlagsVertical = SizeFlags.ExpandFill,
		});
		FrameInfoView.Load(_proxy);

		AddChild(DeleteButton = new Button {
			Text = "Delete Frame"
		});
		DeleteButton.Pressed += DeleteButton_OnPressed;
	}

	public override void _EnterTree() {
		base._EnterTree();
		_proxy.PropertyChanged += Proxy_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_proxy.PropertyChanged -= Proxy_OnPropertyChanged;
	}

	private void Proxy_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		SpriteReference.LoadOptions(_proxy.SpriteDefinitions);
		SpriteReference.PropertyValue = _proxy.SpriteId;
	}

	private void DeleteButton_OnPressed() {
		_proxy.Delete();
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (FrameInfo == null) Disable();
		else Enable();
	}
}