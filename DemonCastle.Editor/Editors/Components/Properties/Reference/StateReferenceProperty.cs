using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class StateReferenceProperty : BaseReferenceProperty<EntityStateInfo> {
	public StateReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<EntityStateInfo> options) : base(binding, options) {
		Name = nameof(StateReferenceProperty);
	}

	protected override Texture2D? GetTexture(EntityStateInfo option) {
		return null;
	}

	protected override Guid GetGuid(EntityStateInfo option) => option.Id;
	protected override string GetName(EntityStateInfo option) => option.Name;
}