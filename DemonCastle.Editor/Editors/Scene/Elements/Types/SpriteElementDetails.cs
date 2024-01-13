using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class SpriteElementDetails : ElementDetails {
	public SpriteElementDetails(IFileInfo file, SpriteElementInfo element) : base(element) {
		Name = nameof(SpriteElementDetails);

		AddSpriteDefinition(element, file.Directory,
			e => e.SpriteFile,
			e => e.SpriteId,
			e => e.SpriteDefinitions);
	}
}