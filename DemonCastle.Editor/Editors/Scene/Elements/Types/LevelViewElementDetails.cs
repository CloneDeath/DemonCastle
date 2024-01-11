using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class LevelViewElementDetails : ElementDetails {
	protected readonly LevelViewElementInfo _element;

	public LevelViewElementDetails(LevelViewElementInfo element) : base(element) {
		_element = element;
		Name = nameof(LevelViewElementDetails);
	}
}