using DemonCastle.Editor.Extensions;
using DemonCastle.Game;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaCell : Node2D {
	private readonly AreaInfo _areaInfo;
	private readonly LevelInfo _levelInfo;

	public AreaCell(AreaInfo areaInfo) {
		_areaInfo = areaInfo;
		_levelInfo = areaInfo.LevelInfo;
		Position = areaInfo.AreaPosition * _levelInfo.AreaSize;

		const int borderWidth = 2;
		AddChild(new ColorRect {
			Color = Colors.LightBlue,
			Size = areaInfo.Size * _levelInfo.AreaSize
		});
		AddChild(new ColorRect {
			Position = new Vector2(1, 1) * borderWidth,
			Color = Colors.Blue,
			Size = areaInfo.Size * _levelInfo.AreaSize - new Vector2I(2, 2) * borderWidth
		});
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _areaInfo.AreaPosition * _levelInfo.AreaSize;

		if (Input.IsActionJustPressed(InputActions.EditorClick) && MouseWithinBounds()) {
			var window = new Area.AreaEditor(_areaInfo);
			var container = this.GetEditArea();
			container.ShowEditor(window);
		}
	}

	private bool MouseWithinBounds() {
		var mousePosition = GetViewport().GetMousePosition();
		var myPosition = GlobalPosition;
		var delta = mousePosition - myPosition;
		var size = _areaInfo.Size * _levelInfo.AreaSize;
		return delta is { X: >= 0, Y: >= 0 }
			   && delta.X < size.X
			   && delta.Y < size.Y;
	}
}