using DemonCastle.Editor.Editors.BaseEntity;
using DemonCastle.ProjectFiles.Files;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Monster;

public partial class MonsterDetails : BaseEntityDetails<MonsterInfo, MonsterFile> {
	public MonsterDetails(MonsterInfo monster) : base(monster) {
		Name = nameof(MonsterDetails);

		CustomProperties.AddInteger("Health", monster, m => m.Health);
		CustomProperties.AddInteger("Experience", monster, m => m.Experience);
		CustomProperties.AddInteger("Attack", monster, m => m.Attack);
		CustomProperties.AddInteger("PhysicalDefense", monster, m => m.PhysicalDefense);
		CustomProperties.AddInteger("MagicalDefense", monster, m => m.MagicalDefense);

		CustomProperties.AddFloat("Move Speed", monster, m => m.MoveSpeed);
		CustomProperties.AddFloat("Jump Height", monster, m => m.JumpHeight);
		CustomProperties.AddFloat("Gravity", monster, m => m.Gravity);
		CustomProperties.AddVector2I("Size", monster, m => m.Size);
	}
}