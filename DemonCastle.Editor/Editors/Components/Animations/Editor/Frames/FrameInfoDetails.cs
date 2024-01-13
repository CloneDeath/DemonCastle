using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations.Editor.Frames;

public partial class FrameInfoDetails : PropertyCollection {
	protected readonly FrameInfoProxy _proxy = new();

	protected FrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	protected PropertyCollection HitBoxes { get; }
	protected PropertyCollection AdditionalProperties { get; }

	public IFrameInfo? FrameInfo {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public FrameInfoDetails(IFileInfo file) : this(file, new FrameInfoView()){

	}

	protected FrameInfoDetails(IFileInfo file, FrameInfoView infoView) {
		Name = nameof(FrameInfoDetails);

		AddFloat("Duration", _proxy, p => p.Duration);
		AddSpriteDefinition(_proxy, file.Directory,
			e => e.SourceFile,
			e => e.SpriteId,
			e => e.SpriteDefinitions);
		AddOrigin("Origin", _proxy, p => p.Anchor, p => p.Offset);
		AddChild(HitBoxes = new PropertyCollection {
			Vertical = false
		});
		HitBoxes.AddNullableRect2I("HitBox", _proxy, p => p.HitBox);
		HitBoxes.AddChild(new VSeparator());
		HitBoxes.AddNullableRect2I("HurtBox", _proxy, p => p.HurtBox);

		AddNullableFile("Audio", _proxy, file.Directory, p => p.Audio, FileType.AudioSources);
		AddChild(AdditionalProperties = new PropertyCollection());

		FrameInfoView = infoView;
		FrameInfoView.SizeFlagsVertical = SizeFlags.ExpandFill;
		AddChild(FrameInfoView);
		FrameInfoView.Load(_proxy);

		AddChild(DeleteButton = new Button {
			Text = "Delete Frame"
		});
		DeleteButton.Pressed += DeleteButton_OnPressed;
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