using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.TileSets;

namespace DemonCastle.Editor.Editors.TileSet;

public partial class TileSetDetails : PropertyCollection {
	public TileSetDetails(TileSetInfo tileSet) {
		Name = nameof(TileSetDetails);

		AddString("Name", tileSet, t => t.Name);
	}
}