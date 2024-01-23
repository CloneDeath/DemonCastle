using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public partial class BaseEntityDetails : PropertyCollection {
	public BaseEntityDetails(IBaseEntityInfo entity) {
		Name = nameof(MonsterDetails);

		AddString("Name", entity, m => m.Name, InternalMode.Front);
		AddStateReference("Initial State", entity, m => m.InitialState, entity.States, InternalMode.Back);
	}
}