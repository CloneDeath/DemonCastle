using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.SpriteAtlas.Details;

public partial class SpriteAtlasDetails : PropertyCollection {
	public SpriteAtlasDetails(SpriteAtlasInfo spriteAtlas) {
		AddFile("File", spriteAtlas, spriteAtlas.Directory, x => x.SpriteFile, FileType.RawTextureFiles);
		AddColor("Transparent Color", spriteAtlas, x => x.TransparentColor);
	}
}