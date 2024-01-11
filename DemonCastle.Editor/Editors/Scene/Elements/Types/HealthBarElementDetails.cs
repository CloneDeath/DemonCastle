using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class HealthBarElementDetails : ElementDetails {
	public HealthBarElementDetails(IFileInfo file, HealthBarElementInfo element) : base(element) {
		Name = nameof(HealthBarElementDetails);

		AddFile("Sprite File", element, file.Directory, e => e.SpriteFile, FileType.SpriteSources);
		AddSpriteReference("Sprite", element, e => e.SpriteId, element.SpriteDefinitions);
		AddEnum("Source", element, e => e.Source);
	}
}