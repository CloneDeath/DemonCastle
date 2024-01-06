using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.Animations.Editor.Frames;
using Godot;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameInfoView : FrameInfoView {
	public PositionTarget WeaponPosition { get; }

	public CharacterFrameInfoView() {
		Name = nameof(CharacterFrameInfoView);

		Inner.AddChild(WeaponPosition = new PositionTarget {
			Color = Colors.Orange
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);

		WeaponPosition.Visible = _proxy.WeaponEnabled;
		WeaponPosition.Target = _proxy.WeaponPosition;
	}
}