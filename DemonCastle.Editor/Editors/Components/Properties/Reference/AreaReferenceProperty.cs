using System;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class AreaReferenceProperty : BaseReferenceProperty<AreaInfo> {
	public AreaReferenceProperty(IPropertyBinding<Guid> binding, IEnumerableInfo<AreaInfo> options) : base(binding, options) {
		Name = nameof(AreaReferenceProperty);
	}

	protected override Texture2D? GetTexture(AreaInfo option) => null;
	protected override Guid GetGuid(AreaInfo option) => option.Id;
	protected override string GetName(AreaInfo option) => option.Name;
}