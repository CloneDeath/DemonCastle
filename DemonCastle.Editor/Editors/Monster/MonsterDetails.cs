using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterDetails : BaseEntityDetails {
	public MonsterDetails(MonsterInfo monster) : base(monster) {
		Name = nameof(MonsterDetails);

		AddInteger("Health", monster, m => m.Health);
		AddInteger("Experience", monster, m => m.Experience);
		AddInteger("Attack", monster, m => m.Attack);
		AddInteger("PhysicalDefense", monster, m => m.PhysicalDefense);
		AddInteger("MagicalDefense", monster, m => m.MagicalDefense);

		AddFloat("Move Speed", monster, m => m.MoveSpeed);
		AddFloat("Jump Height", monster, m => m.JumpHeight);
		AddFloat("Gravity", monster, m => m.Gravity);
		AddVector2I("Size", monster, m => m.Size);
		AddBoolean("Despawn on Death", monster, m => m.DespawnOnDeath);
	}
}