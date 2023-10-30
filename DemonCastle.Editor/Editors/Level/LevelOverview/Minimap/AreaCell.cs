using System.Linq;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview.Minimap;

public partial class AreaCell : SelectableControl {
	public AreaInfo Area { get; }
	private LevelInfo Level => Area.LevelInfo;

	private ColorRect AreaColor { get; }
	private ColorRect OutlineColor { get; }

	public AreaCell(AreaInfo area) {
		Area = area;

		Name = nameof(AreaCell);

		SelectedCursorShape = CursorShape.Arrow;
		DefaultCursorShape = CursorShape.PointingHand;

		const int borderWidth = 2;
		AddChild(AreaColor = new ColorRect {
			Color = Colors.LightBlue,
			MouseFilter = MouseFilterEnum.Ignore
		});
		AreaColor.SetAnchorsPreset(LayoutPreset.FullRect);
		AddChild(OutlineColor = new ColorRect {
			Color = Colors.Blue,
			MouseFilter = MouseFilterEnum.Ignore
		});
		OutlineColor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: borderWidth);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = Area.AreaPosition * Level.AreaSize;
		Size = Area.Size * Level.AreaSize;
		AreaColor.Color = IsSelected ? Colors.MediumBlue : Colors.LightBlue;
	}

	protected override void OnSelected() {
		base.OnSelected();

		var siblings = GetParent().GetChildren().Where(c => c is AreaCell && c != this).Cast<AreaCell>();
		foreach (var sibling in siblings) {
			sibling.IsSelected = false;
		}
	}
}