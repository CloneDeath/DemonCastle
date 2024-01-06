using System.Linq;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Properties;
using DemonCastle.Game.Scenes;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor.Types;

public partial class LabelElementDetails : ElementDetails {
	public LabelElementDetails(IFileInfo file, LabelElementInfo element) : base(element) {
		Name = nameof(LabelElementDetails);

		AddChild(new TextProperty(new PropertyBinding<LabelElementInfo, string>(element, e => e.Text)));
		AddNullableFile("Font", element, file.Directory, e => e.FontFile, FileType.FontFiles);
		AddInteger("Font Size", element, e => e.FontSize);
		AddColor("Color", element, e => e.Color);
		AddEnum("Text Transform", element, e => e.TextTransform);
	}
}

public partial class TextProperty : VBoxContainer, IBaseProperty {
	private IPropertyBinding<string> Binding { get; }

	protected Label Label { get; }
	protected MenuButton Variables { get; }
	protected TextEdit TextEdit { get; }

	public string PropertyValue {
		get => TextEdit.Text;
		set => TextEdit.Text = value;
	}

	public TextProperty(IPropertyBinding<string> binding) {
		Name = nameof(TextProperty);
		Binding = binding;

		HBoxContainer header;
		AddChild(header = new HBoxContainer());

		header.AddChild(Label = new Label { Text = "Text" });
		header.AddSpacer(false);
		header.AddChild(Variables = new MenuButton {
			Text = "Variables..."
		});
		LoadVariables();

		AddChild(TextEdit = new TextEdit {
			CustomMinimumSize = new Vector2(100, 150),
			Text = binding.Get()
		});
		TextEdit.TextChanged += TextEdit_OnTextChanged;
	}

	private void LoadVariables() {
		var popup = Variables.GetPopup();
		var integerValuesKeys = TextFinalizer.IntegerValues.Keys.ToArray();
		for (var i = 0; i < integerValuesKeys.Length; i++) {
			var value = integerValuesKeys[i];
			popup.AddItem(value, i);
		}
		popup.IdPressed += Popup_OnIdPressed;
	}

	private void Popup_OnIdPressed(long id) {
		var integerValuesKeys = TextFinalizer.IntegerValues.Keys.ToArray();
		TextEdit.InsertTextAtCaret($"{{{integerValuesKeys[(int)id]}}}");
	}

	private void TextEdit_OnTextChanged() {
		Binding.Set(TextEdit.Text);
	}

	public override void _EnterTree() {
		base._EnterTree();
		Binding.Changed += Binding_OnChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		Binding.Changed -= Binding_OnChanged;
	}

	private void Binding_OnChanged(string value) {
		if (PropertyValue == value) return;
		PropertyValue = value;
	}

	public string DisplayName {
		get => Label.Text;
		set => Label.Text = value;
	}

	public void Enable() {
		TextEdit.Editable = true;
	}

	public void Disable() {
		TextEdit.Editable = false;
	}
}