using System.ComponentModel;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Animations.Editor.Frames;

public partial class FrameInfoView : ControlView<SpriteDefinitionView> {
	protected readonly FrameInfoProxy Proxy = new();

	protected IFrameInfo? Frame {
		get => Proxy.Proxy;
		set => Proxy.Proxy = value;
	}

	private PositionTarget OriginTarget { get; }

	public FrameInfoView() {
		Toolbar.Zoom = MainControl.Zoom = 8;
		Inner.AddChild(OriginTarget = new PositionTarget());
		Inner.AddChild(new NullableRect2IView(new PropertyBinding<FrameInfoProxy,Rect2I?>(Proxy, p => p.HurtBox)) {
			Color = Colors.Orange
		});
		Inner.AddChild(new NullableRect2IView(new PropertyBinding<FrameInfoProxy,Rect2I?>(Proxy, p => p.HitBox)) {
			Color = Colors.Blue
		});
	}

	public override void _ExitTree() {
		base._ExitTree();

		if (Frame is not null) {
			Frame.PropertyChanged -= Frame_OnPropertyChanged;
		}
	}

	public void Load(IFrameInfo info) {
		if (Frame is not null) {
			Frame.PropertyChanged -= Frame_OnPropertyChanged;
		}
		Frame = info;
		Frame.PropertyChanged += Frame_OnPropertyChanged;
		Inner.Load(info.SpriteDefinition);
		OriginTarget.Target = Frame.Origin;
	}

	private void Frame_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (Frame == null) return;
		if (e.PropertyName == nameof(IFrameInfo.SpriteDefinition)) {
			Inner.Load(Frame.SpriteDefinition);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		OriginTarget.Target = Frame?.Origin ?? Vector2.Zero;
	}
}