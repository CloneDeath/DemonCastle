using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Components.Animations.Editor;

public partial class AnimationDetails : PropertyCollection {
	private readonly AnimationInfoProxy _proxy = new();

	public IAnimationInfo? Animation {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public AnimationDetails() {
		Name = nameof(AnimationDetails);

		AddString("Name", _proxy, w => w.Name);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Animation == null) Disable();
		else Enable();
	}
}