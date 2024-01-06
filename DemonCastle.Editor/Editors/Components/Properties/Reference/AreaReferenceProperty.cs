using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class AreaReferenceProperty : BaseReferenceProperty<AreaInfo> {
	public AreaReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<AreaInfo> options) : base(binding, options) {
		Name = nameof(AreaReferenceProperty);
	}

	protected override Texture2D? GetTexture(AreaInfo option) => null;
	protected override Guid GetGuid(AreaInfo option) => option.Id;
	protected override string GetName(AreaInfo option) => option.Name;
}