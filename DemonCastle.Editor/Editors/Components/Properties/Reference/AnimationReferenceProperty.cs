using System;
using System.Linq;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.Sprites;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.Properties.Reference;

public partial class AnimationReferenceProperty : BaseReferenceProperty<IAnimationInfo> {
	public AnimationReferenceProperty(IPropertyBinding<Guid> binding, IEnumerableInfo<IAnimationInfo> options) : base(binding, options) {
		Name = nameof(MonsterReferenceProperty);
	}

	protected override Texture2D GetTexture(IAnimationInfo option) {
		var spriteDefinition = option.Frames.FirstOrDefault()?.SpriteDefinition ?? new NullSpriteDefinition();
		return spriteDefinition.ToTexture();
	}

	protected override Guid GetGuid(IAnimationInfo option) => option.Id;
	protected override string GetName(IAnimationInfo option) => option.Name;
}