using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor.Types;

public partial class LabelElementDetails : ElementDetails {
	public LabelElementDetails(IFileInfo file, LabelElementInfo element) : base(element) {
		Name = nameof(LabelElementDetails);

		AddString("Text", element, e => e.Text);
		AddNullableFile("Font", element, file.Directory, e => e.FontFile, FileType.FontFiles);
		AddInteger("Font Size", element, e => e.FontSize);
		AddColor("Color", element, e => e.Color);
		AddEnum("Text Transform", element, e => e.TextTransform);
	}
}