using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level;

public partial class MinimapView : ScrollContainer {
	private readonly LevelInfo _levelInfo;
	public Node2D Root;

	public MinimapView(LevelInfo levelInfo) {
		Name = nameof(MinimapView);
		_levelInfo = levelInfo;

		var control = new Control {
			Size = new Vector2(500, 500)
		};
		AddChild(control);

		control.AddChild(new GridControl {
			Size = new Vector2I(500, 500),
			GridSize = levelInfo.AreaSize
		});
		control.AddChild(Root = new Node2D());
		LoadLevel(levelInfo);
	}

	public void Reload() => LoadLevel(_levelInfo);

	private void LoadLevel(LevelInfo levelInfo) {
		foreach (var child in Root.GetChildren()) {
			child.QueueFree();
		}
		foreach (var area in levelInfo.Areas) {
			Root.AddChild(new AreaCell(area));
		}
	}
}