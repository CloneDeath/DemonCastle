using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations.Editor.Frames;

public partial class FrameInfoDetails : PropertyCollection {
	protected readonly FrameInfoProxy Proxy = new();

	protected FrameInfoView FrameInfoView { get; }
	protected Button DeleteButton { get; }

	protected PropertyCollection AdditionalProperties { get; }

	public IFrameInfo? FrameInfo {
		get => Proxy.Proxy;
		set => Proxy.Proxy = value;
	}

	public FrameInfoDetails(IFileInfo file) : this(file, new FrameInfoView()){

	}

	protected FrameInfoDetails(IFileInfo file, FrameInfoView infoView) {
		Name = nameof(FrameInfoDetails);

		AddFloat("Duration", Proxy, p => p.Duration);
		AddSpriteDefinition(Proxy, file.Directory,
			e => e.SourceFile,
			e => e.SpriteId,
			e => e.SpriteDefinitions);
		AddOrigin("Origin", Proxy, p => p.Anchor, p => p.Offset);

		AddChild(new HSeparator());
		AddNullableRect2I("HitBox", Proxy, p => p.HitBox);
		AddNullableRect2I("HurtBox", Proxy, p => p.HurtBox);
		AddChild(new HSeparator());

		AddNullableFile("Audio", Proxy, file.Directory, p => p.Audio, FileType.AudioSources);
		AddChild(AdditionalProperties = new PropertyCollection());

		FrameInfoView = infoView;
		FrameInfoView.SizeFlagsVertical = SizeFlags.ExpandFill;
		AddChild(FrameInfoView);
		FrameInfoView.Load(Proxy);

		AddChild(DeleteButton = new Button {
			Text = "Delete Frame"
		});
		DeleteButton.Pressed += DeleteButton_OnPressed;
	}

	private void DeleteButton_OnPressed() {
		Proxy.Delete();
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (FrameInfo == null) {Disable();}
		else Enable();
		DeleteButton.Disabled = FrameInfo == null;
	}
}