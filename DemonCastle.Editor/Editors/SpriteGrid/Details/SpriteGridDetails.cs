using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;

namespace DemonCastle.Editor.Editors.SpriteGrid.Details;

public partial class SpriteGridDetails : PropertyCollection {
	public SpriteGridDetails(SpriteGridInfo spriteGrid) {
		AddFile("File", spriteGrid, spriteGrid.Directory, x => x.SpriteFile, FileType.RawTextureFiles);
		AddInteger("Width", spriteGrid, x => x.Width);
		AddInteger("Height", spriteGrid, x => x.Height);
		AddInteger("X Offset", spriteGrid, x => x.XOffset);
		AddInteger("Y Offset", spriteGrid, x => x.YOffset);
		AddInteger("X Separation", spriteGrid, x => x.XSeparation);
		AddInteger("Y Separation", spriteGrid, x => x.YSeparation);
	}
}