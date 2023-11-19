using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameListEditor : VBoxContainer {
	private WeaponAnimationInfo? _current;

	private Button AddFrameButton { get; }

	public WeaponFrameListEditor() {
		AddChild(AddFrameButton = new Button { Text = "Add Frame" });
		AddFrameButton.Pressed += AddFrameButton_OnPressed;
		AddChild(new HFlowContainer {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}

	public void Load(WeaponAnimationInfo animation) {
		_current = animation;
	}

	private void AddFrameButton_OnPressed() {
		_current?.AddFrame();
	}
}