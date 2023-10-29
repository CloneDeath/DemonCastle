using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaView : Control {
	protected Outline Outline { get; }
	protected AreaTilesView Root { get; }

	public AreaView(AreaInfo areaInfo) {
		Name = nameof(AreaView);

		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = new Color(Colors.White, 0.5f)
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);
		AddChild(Root = new AreaTilesView(areaInfo));

		Size = areaInfo.TileSize * areaInfo.AreaSize * areaInfo.Size;
	}
}