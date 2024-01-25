using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class ItemReferenceProperty : BaseReferenceProperty<ItemInfo> {
	public ItemReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<ItemInfo> options) : base(binding, options) {
		Name = nameof(ItemReferenceProperty);
	}

	protected override Texture2D GetTexture(ItemInfo option) => option.PreviewTexture;
	protected override Guid GetGuid(ItemInfo option) => option.Id;
	protected override string GetName(ItemInfo option) => option.Name;
}