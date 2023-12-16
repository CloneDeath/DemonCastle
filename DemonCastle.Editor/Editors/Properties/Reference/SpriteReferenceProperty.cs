using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using Godot;

namespace DemonCastle.Editor.Editors.Properties.Reference;

public partial class SpriteReferenceProperty : BaseReferenceProperty<ISpriteDefinition> {
	public SpriteReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<ISpriteDefinition> options) : base(binding, options) {
		Name = nameof(SpriteReferenceProperty);
	}

	protected override Texture2D GetTexture(ISpriteDefinition option) {
		return new AtlasTexture {
			Atlas = option.Texture,
			Region = option.Region,
			FilterClip = true
		};
	}

	protected override Guid GetGuid(ISpriteDefinition option) => option.Id;
	protected override string GetName(ISpriteDefinition option) => option.Name;
}