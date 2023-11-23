using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor;

public partial class CharacterAnimationDetails : PropertyCollection {
	private readonly CharacterAnimationInfoProxy _proxy = new();

	private StringProperty AnimationName { get; }

	public CharacterAnimationInfo? CharacterAnimation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public CharacterAnimationDetails() {
		Name = nameof(CharacterAnimationDetails);

		AnimationName = AddString("Name", _proxy, w => w.Name);
	}

	public void Disable() => AnimationName.Disable();
	public void Enable() => AnimationName.Enable();

	public override void _Process(double delta) {
		base._Process(delta);
		if (CharacterAnimation == null) Disable();
		else Enable();
	}
}