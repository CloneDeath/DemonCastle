using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaEditor {
	public Node2D Root; 
	public AreaEditor(LevelInfo levelInfo) {
		Name = nameof(AreaEditor);
		
		var control = new Control {
			Size = new Vector2(500, 500)
		};
		AddChild(control);

		control.AddChild(Root = new Node2D());
		LoadLevel(levelInfo);
	}
}