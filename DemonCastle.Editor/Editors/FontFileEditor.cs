using Godot;

namespace DemonCastle.Editor.Editors;

public partial class FontFileEditor : Control {
	protected ScrollContainer ScrollContainer { get; }
	protected VBoxContainer Lines { get; }

	public FontFileEditor(Font font) {
		Name = nameof(ImageEditor);

		AddChild(ScrollContainer = new ScrollContainer());
		ScrollContainer.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);

		ScrollContainer.AddChild(Lines = new VBoxContainer());

		const string previewText = "The Quick Brown Fox Jumped Over the Lazy Dog";
		var sizes = new[] { 72, 36, 24, 12, 10, 8, 6 };

		foreach (var size in sizes) {
			var labelSettings = new LabelSettings {
				Font = font,
				FontSize = size
			};
			var textLine = $"{labelSettings.FontSize}: {previewText}";
			Lines.AddChild(new Label {
				Text = textLine,
				LabelSettings = labelSettings
			});
			Lines.AddChild(new Label {
				Text = textLine.ToUpper(),
				LabelSettings = labelSettings
			});
			Lines.AddChild(new Label {
				Text = textLine.ToLower(),
				LabelSettings = labelSettings
			});
		}
	}
}