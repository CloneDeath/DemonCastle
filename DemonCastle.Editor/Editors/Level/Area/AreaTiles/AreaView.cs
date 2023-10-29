using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaView : Control {
	protected AreaInfo Area { get; }

	protected Outline Outline { get; }
	protected AreaTilesView Root { get; }

	public AreaView(AreaInfo area) {
		Area = area;

		Name = nameof(AreaView);

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = new Color(Colors.White, 0.5f)
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);
		AddChild(Root = new AreaTilesView(area));

	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = Area.AreaPosition * Area.AreaSize * Area.TileSize;
		Size = Area.TileSize * Area.AreaSize * Area.Size;
	}
}