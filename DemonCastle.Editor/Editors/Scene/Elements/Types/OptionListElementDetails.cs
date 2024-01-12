using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Scene.Events;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class OptionListElementDetails : ElementDetails {
	private readonly IFileInfo _file;
	private readonly EnumerableInfoList<OptionInfo> _options;
	private OptionDetails? _details;

	public OptionListElementDetails(IFileInfo file, OptionListElementInfo element) : base(element) {
		_file = file;
		Name = nameof(LabelElementDetails);

		AddNullableFile("Font", element, file.Directory, e => e.FontFile, FileType.FontFiles);
		AddInteger("Font Size", element, e => e.FontSize);
		AddColor("Color", element, e => e.Color);
		AddEnum("Text Transform", element, e => e.TextTransform);

		AddChild(_options = new EnumerableInfoList<OptionInfo>(element.Options) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		_options.ItemSelected += Options_OnItemSelected;
	}

	private void Options_OnItemSelected(OptionInfo? obj) {
		_details?.QueueFree();
		if (obj == null) return;

		_details = new OptionDetails(_file, obj) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		};
		AddChild(_details);
	}
}

public partial class OptionDetails : PropertyCollection {
	public OptionDetails(IFileInfo file, OptionInfo option) {
		AddString("Text", option, o => o.Text);
		AddChild(new SceneEventActionCollectionEditor(file, option.OnSelect) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}
}