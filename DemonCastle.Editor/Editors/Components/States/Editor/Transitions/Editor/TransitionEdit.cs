using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;

public partial class TransitionEdit : PropertyCollection {
	private readonly TransitionInfoProxy _proxy = new();

	public EntityStateTransitionInfo? Transition {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			WhenEdit.Transition = value;
		}
	}

	private WhenEdit WhenEdit { get; }

	public TransitionEdit(IEnumerableInfo<EntityStateInfo> options) {
		Name = nameof(TransitionEdit);

		AddString("Name", _proxy, t => t.Name);
		AddChild(WhenEdit = new WhenEdit());
		AddStateReference("Transition to", _proxy, t => t.TargetState, options);
	}
}