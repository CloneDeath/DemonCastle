using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class OptionListElementDetails : ElementDetails {
	public OptionListElementDetails(IFileInfo file, OptionListElementInfo element) : base(element) {
		Name = nameof(LabelElementDetails);

		AddNullableFile("Font", element, file.Directory, e => e.FontFile, FileType.FontFiles);
		AddInteger("Font Size", element, e => e.FontSize);
		AddColor("Color", element, e => e.Color);
		AddEnum("Text Transform", element, e => e.TextTransform);

	}
}