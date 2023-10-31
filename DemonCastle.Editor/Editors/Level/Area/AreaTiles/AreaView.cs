using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaView : SelectableControl {
	protected AreaInfo Area { get; }

	protected Outline Outline { get; }
	protected AreaTilesView Root { get; }

	private static readonly Color SelectedColor = new(Colors.White, 0.75f);
	private static readonly Color DeselectedColor = new(Colors.White, 0.3f);

	public AreaView(AreaInfo area) {
		Area = area;

		Name = nameof(AreaView);

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = DeselectedColor
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);
		AddChild(Root = new AreaTilesView(area));
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = Area.PositionOfArea.ToLevelPositionInPixels();
		Size = Area.SizeOfArea.ToPixelSize();
		Outline.Color = IsSelected ? SelectedColor : DeselectedColor;
		Outline.Thickness = IsSelected ? 2 : 1;
	}

	protected override void OnSelected() {
		base.OnSelected();
		DeselectSiblings();
	}
}