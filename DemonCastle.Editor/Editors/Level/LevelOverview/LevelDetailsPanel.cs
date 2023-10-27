using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview;

public partial class LevelDetailsPanel : VBoxContainer {
	protected LevelInfo LevelInfo { get; }

	protected PropertyCollection Properties { get; }
	protected Button AddAreaButton { get; }

	public LevelDetailsPanel(LevelInfo levelInfo) {
		LevelInfo = levelInfo;

		AddChild(Properties = new PropertyCollection());
		Properties.AddString("Name", levelInfo, x => x.Name);
		Properties.AddVector2I("Tile Size", levelInfo, x => x.TileSize);
		Properties.AddVector2I("Area Size", levelInfo, x => x.AreaSize);

		AddChild(AddAreaButton = new Button { Text = "Add Area" });
		AddAreaButton.Pressed += AddAreaButtonOnPressed;
	}

	private void AddAreaButtonOnPressed() {
		LevelInfo.CreateArea();
	}
}