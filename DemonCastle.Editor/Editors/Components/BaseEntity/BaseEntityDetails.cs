using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public partial class BaseEntityDetails : PropertyCollection {
	public BaseEntityDetails(IBaseEntityInfo entity) {
		Name = nameof(MonsterDetails);

		AddString("Name", entity, e => e.Name, InternalMode.Front);
		AddStateReference("Initial State", entity, e => e.InitialState, entity.States, InternalMode.Back);
	}
}