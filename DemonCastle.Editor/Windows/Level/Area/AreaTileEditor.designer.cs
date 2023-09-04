using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaTileEditor {
	public AreaInfo Area { get; }
	public Node2D Root;
	
	public AreaTileEditor(AreaInfo area) {
		Area = area;
		Name = nameof(AreaEditor);
		
		var control = new Control {
			Size = new Vector2(500, 500)
		};
		AddChild(control);

		control.AddChild(Root = new Node2D());
		control.AddChild(new GridControl {
			TotalSize = area.Size * area.AreaSize * area.TileSize,
			GridSize = area.TileSize
		});
		LoadArea(area);
	}
}