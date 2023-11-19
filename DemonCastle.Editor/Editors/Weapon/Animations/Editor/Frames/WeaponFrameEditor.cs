using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Weapon.Animations.Editor.Frames;

public partial class WeaponFrameEditor : VBoxContainer {
	public WeaponFrameEditor(WeaponFrameInfo frame) {
		AddChild(new Label {
			Text = $"{frame.Index} - {frame.Duration}s"
		});
	}
}