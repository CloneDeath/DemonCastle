using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class HealthBarElementDetails : ElementDetails {
	public HealthBarElementDetails(IFileInfo file, HealthBarElementInfo element) : base(element) {
		Name = nameof(HealthBarElementDetails);

		AddSpriteDefinition(element, file.Directory,
			e => e.SpriteFile,
			e => e.SpriteId,
			e => e.SpriteDefinitions);
		AddEnum("Source", element, e => e.Source);
	}
}