using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Animations.Editor;

public partial class AnimationDetails : PropertyCollection {
	private readonly AnimationInfoProxy _proxy = new();

	private StringProperty AnimationName { get; }

	public IAnimationInfo? Animation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public AnimationDetails() {
		Name = nameof(AnimationDetails);

		AnimationName = AddString("Name", _proxy, w => w.Name);
	}

	public void Disable() => AnimationName.Disable();
	public void Enable() => AnimationName.Enable();

	public override void _Process(double delta) {
		base._Process(delta);
		if (Animation == null) Disable();
		else Enable();
	}
}