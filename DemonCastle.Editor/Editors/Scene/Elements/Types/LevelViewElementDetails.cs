using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class LevelViewElementDetails : ElementDetails {
	public LevelViewElementDetails(LevelViewElementInfo element) : base(element) {
		Name = nameof(LevelViewElementDetails);
	}
}