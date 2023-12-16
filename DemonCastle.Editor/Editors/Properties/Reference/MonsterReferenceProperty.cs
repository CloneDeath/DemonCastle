using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Properties.Reference;

public partial class MonsterReferenceProperty : BaseReferenceProperty<MonsterInfo> {
	public MonsterReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<MonsterInfo> options) : base(binding, options) {
		Name = nameof(MonsterReferenceProperty);
	}

	protected override Texture2D GetTexture(MonsterInfo option) {
		return option.PreviewTexture;
	}

	protected override Guid GetGuid(MonsterInfo option) => option.Id;
	protected override string GetName(MonsterInfo option) => option.Name;
}