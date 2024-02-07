using DemonCastle.ProjectFiles.Projects.Data.Elements;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class LevelViewElementDetails : ElementDetails {
	public LevelViewElementDetails(IElementInfo element) : base(element) {
		Name = nameof(LevelViewElementDetails);
	}
}