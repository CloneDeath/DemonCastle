using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;

namespace DemonCastle.Editor.Editors.Components.States.Editor;

public partial class StateDetails : PropertyCollection {
	private readonly StateInfoProxy _proxy = new();

	public StateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public StateDetails(IEnumerableInfo<IAnimationInfo> options) {
		Name = nameof(StateDetails);

		AddString("Name", _proxy, p => p.Name);
		AddAnimationReference("Animation", _proxy, p => p.Animation, options);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (State == null) Disable();
		else Enable();
	}
}