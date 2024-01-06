using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.List;

public partial class TransitionList : EnumerableInfoList<EntityStateTransitionInfo> {
	public TransitionList(IEnumerableInfo<EntityStateTransitionInfo> data) : base(data) { }
	protected override string GetName(EntityStateTransitionInfo item) => $"[{item.Name}]: -> {item.TargetState}";
}