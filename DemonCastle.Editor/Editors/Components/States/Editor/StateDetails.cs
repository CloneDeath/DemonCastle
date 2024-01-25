using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;

namespace DemonCastle.Editor.Editors.Components.States.Editor;

public partial class StateDetails : PropertyCollection {
	private readonly StateInfoProxy _state = new();
	private readonly EnumerableInfoProxy<IAnimationInfo> _animations = new();

	public IEnumerableInfo<IAnimationInfo>? Animations {
		get => _animations.Proxy;
		set => _animations.Proxy = value;
	}

	public EntityStateInfo? State {
		get => _state.Proxy;
		set => _state.Proxy = value;
	}

	public StateDetails() {
		Name = nameof(StateDetails);

		AddString("Name", _state, p => p.Name);
		AddAnimationReference("Animation", _state, p => p.Animation, _animations);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (State == null) Disable();
		else Enable();
	}
}