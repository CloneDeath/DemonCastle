using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.States;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public partial class StateDetails : PropertyCollection {
	private readonly StateInfoProxy _proxy = new();

	public StateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	public StateDetails() {
		Name = nameof(StateDetails);

		AddString("Name", _proxy, p => p.Name);
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (State == null) Disable();
		else Enable();
	}
}