using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class LevelView : VSplitContainer {
	private readonly MinimapView _minimap;
	public LevelView(LevelInfo levelInfo) {
		Name = nameof(LevelView);

		AddChild(_minimap = new MinimapView(levelInfo) {
			CustomMinimumSize = new Vector2(0, 100)
		});
		AddChild(new Panel());
	}

	public void Reload() {
		_minimap.Reload();
	}
}