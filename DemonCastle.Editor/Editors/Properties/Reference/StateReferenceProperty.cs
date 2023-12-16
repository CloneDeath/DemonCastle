using System;
using System.Collections.Generic;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Properties.Reference;

public partial class StateReferenceProperty : BaseReferenceProperty<StateInfo> {
	public StateReferenceProperty(IPropertyBinding<Guid> binding, IEnumerable<StateInfo> options) : base(binding, options) {
		Name = nameof(StateReferenceProperty);
	}

	protected override Texture2D? GetTexture(StateInfo option) {
		return null;
	}

	protected override Guid GetGuid(StateInfo option) => option.Id;
	protected override string GetName(StateInfo option) => option.Name;
}