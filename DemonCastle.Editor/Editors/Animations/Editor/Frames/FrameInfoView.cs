using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using Godot;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public partial class FrameInfoView : ControlView<SpriteDefinitionView> {
	private IFrameInfo? _info;

	private PositionTarget OriginTarget { get; }

	public FrameInfoView() {
		Inner.AddChild(OriginTarget = new PositionTarget());
	}

	public override void _ExitTree() {
		base._ExitTree();

		if (_info is not null) {
			_info.PropertyChanged -= Info_OnPropertyChanged;
		}
	}

	public void Load(IFrameInfo info) {
		if (_info is not null) {
			_info.PropertyChanged -= Info_OnPropertyChanged;
		}
		_info = info;
		_info.PropertyChanged += Info_OnPropertyChanged;
		Inner.Load(info.SpriteDefinition);
		OriginTarget.Target = _info.Origin;
	}

	private void Info_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (_info == null) return;
		if (e.PropertyName == nameof(IFrameInfo.SpriteDefinition)) {
			Inner.Load(_info.SpriteDefinition);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		OriginTarget.Target = _info?.Origin ?? Vector2.Zero;
	}
}