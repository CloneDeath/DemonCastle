using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class SpriteReferenceProperty : BaseReferenceProperty<ISpriteDefinition> {
	public SpriteReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<ISpriteDefinition> options) : base(binding, options) {
		Name = nameof(SpriteReferenceProperty);
	}

	protected override Texture2D GetTexture(ISpriteDefinition option) => option.ToTexture();

	protected override Guid GetGuid(ISpriteDefinition option) => option.Id;
	protected override string GetName(ISpriteDefinition option) => option.Name;
}