using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor.Types;

public partial class SpriteElementDetails : ElementDetails {
	public SpriteElementDetails(IFileInfo file, SpriteElementInfo element) : base(element) {
		Name = nameof(SpriteElementDetails);

		AddFile("Sprite File", element, file.Directory, e => e.SpriteFile, FileType.SpriteSources);
		AddSpriteReference("Sprite", element, e => e.SpriteId, element.SpriteDefinitions);
	}
}