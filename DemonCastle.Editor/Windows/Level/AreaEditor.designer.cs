using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level; 

public partial class AreaEditor {
	public Node2D Root; 
	public AreaEditor(LevelInfo levelInfo) {
		var subViewportContainer = new SubViewportContainer();
		AddChild(subViewportContainer);

		var subViewport = new SubViewport {
			Size = new Vector2I(1000, 1000)
		};
		subViewportContainer.AddChild(subViewport);

		subViewport.AddChild(Root = new Node2D());
		LoadLevel(levelInfo);
	}
}