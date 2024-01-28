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

	public IBaseEntityInfo? Entity {
		get => WhenEdit.Entity;
		set => WhenEdit.Entity = value;
	}

	private WhenEdit WhenEdit { get; }

	public TransitionEdit(ProjectInfo project, IEnumerableInfo<EntityStateInfo> options) {
		Name = nameof(TransitionEdit);

		AddString("Name", _proxy, t => t.Name);
		AddChild(WhenEdit = new WhenEdit(project));
		AddStateReference("Transition to", _proxy, t => t.TargetState, options);
	}
}