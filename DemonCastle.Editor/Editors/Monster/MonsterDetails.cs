using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterDetails : PropertyCollection {
	public MonsterDetails(MonsterInfo monster) {
		Name = nameof(MonsterDetails);

		AddString("Name", monster, m => m.Name);

		AddInteger("Health", monster, m => m.Health);
		AddInteger("Experience", monster, m => m.Experience);
		AddInteger("Attack", monster, m => m.Attack);
		AddInteger("PhysicalDefense", monster, m => m.PhysicalDefense);
		AddInteger("MagicalDefense", monster, m => m.MagicalDefense);

		AddFloat("Move Speed", monster, m => m.MoveSpeed);
		AddFloat("Jump Height", monster, m => m.JumpHeight);
		AddFloat("Gravity", monster, m => m.Gravity);
		AddVector2I("Size", monster, m => m.Size);
		AddStateReference("Initial State", monster, m => m.InitialState, monster.States);
	}
}