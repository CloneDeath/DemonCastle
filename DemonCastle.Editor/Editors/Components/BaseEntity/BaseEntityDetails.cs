using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Monster;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public partial class BaseEntityDetails : PropertyCollection {
	public BaseEntityDetails(IBaseEntityInfo item) {
		Name = nameof(MonsterDetails);

		AddString("Name", item, m => m.Name, InternalMode.Front);
		AddStateReference("Initial State", item, m => m.InitialState, item.States, InternalMode.Back);
	}
}