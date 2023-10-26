using DemonCastle.Editor.Editors.Level.LevelTiles;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class LevelView : VSplitContainer {
	private readonly MinimapView _minimap;
	public LevelView(LevelInfo levelInfo) {
		Name = nameof(LevelView);

		AddChild(_minimap = new MinimapView(levelInfo) {
			CustomMinimumSize = new Vector2(0, 150)
		});
		AddChild(new LevelTilesView(levelInfo));
	}

	public void Reload() {
		_minimap.Reload();
	}
}