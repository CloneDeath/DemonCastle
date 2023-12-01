using DemonCastle.Editor.Editors.Animations.Editor;
using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor;

public partial class CharacterAnimationDetails : PropertyCollection {
	private readonly AnimationInfoProxy _proxy = new();

	public IAnimationInfo? CharacterAnimation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public CharacterAnimationDetails() {
		Name = nameof(CharacterAnimationDetails);

		AddString("Name", _proxy, w => w.Name);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (CharacterAnimation == null) Disable();
		else Enable();
	}
}