using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelTiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _areaInfo;
	private Outline Outline { get; }

	public AreaTilesView(AreaInfo areaInfo) {
		Name = nameof(areaInfo);

		_areaInfo = areaInfo;
		AddChild(Outline = new Outline {
			MouseFilter = MouseFilterEnum.Ignore,
			Color = new Color(Colors.White, 0.5f)
		});
		Outline.SetAnchorsPreset(LayoutPreset.FullRect, true);

		Size = areaInfo.TileSize * areaInfo.AreaSize * areaInfo.Size;
	}
}