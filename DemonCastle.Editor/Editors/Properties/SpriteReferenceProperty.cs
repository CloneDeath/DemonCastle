using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Properties;

public partial class SpriteReferenceProperty : BaseProperty {
	private readonly List<ISpriteDefinition> _options;
	protected IPropertyBinding<Guid> Binding { get; }
	protected OptionButton OptionButton { get; }

	public Guid PropertyValue {
		get => OptionButton.Selected < 0 ? Guid.Empty : _options[OptionButton.Selected].Id;
		set {
			var option = _options.FirstOrDefault(o => o.Id == value);
			OptionButton.Selected = option == null ? -1 : _options.IndexOf(option);
		}
	}

	public SpriteReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<ISpriteDefinition> options) {
		_options = options.ToList();
		Name = nameof(SpriteReferenceProperty);
		Binding = binding;

		AddChild(OptionButton = new OptionButton {
			CustomMinimumSize = new Vector2(20, 20),
			SizeFlagsHorizontal = SizeFlags.ExpandFill
		});
		for (var i = 0; i < _options.Count; i++) {
			var option = _options[i];
			var texture = new AtlasTexture {
				Atlas = option.Texture,
				Region = option.Region,
				FilterClip = true
			};
			OptionButton.AddIconItem(texture, option.Name, i);
		}
		PropertyValue = binding.Get();

		OptionButton.ItemSelected += OnItemSelected;
	}

	private void OnItemSelected(long index) {
		Binding.Set(index < 0 ? Guid.Empty : _options[(int)index].Id);
	}

	public override void Enable() {
		base.Enable();
		OptionButton.Disabled = false;
	}

	public override void Disable() {
		base.Disable();
		OptionButton.Disabled = true;
	}
}