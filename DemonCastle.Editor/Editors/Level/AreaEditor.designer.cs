using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level; 

public partial class AreaEditor {
	private readonly LevelInfo _levelInfo;
	public Node2D Root;
	
	public AreaEditor(LevelInfo levelInfo) {
		Name = nameof(AreaEditor);
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
}