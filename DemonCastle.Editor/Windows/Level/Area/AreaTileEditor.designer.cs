using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaTileEditor {
	public Node2D Root; 
	public AreaTileEditor(AreaInfo areaInfo) {
		Name = nameof(AreaEditor);
		
		var control = new Control {
			Size = new Vector2(500, 500)
		};
		AddChild(control);

		control.AddChild(Root = new Node2D());
		LoadArea(areaInfo);
	}
}