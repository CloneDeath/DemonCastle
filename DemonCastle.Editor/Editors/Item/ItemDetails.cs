using DemonCastle.Editor.Editors.Components.BaseEntity;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemDetails : BaseEntityDetails {
	public ItemDetails(ItemInfo item) : base(item) {
		Name = nameof(ItemDetails);

		AddFloat("Move Speed", item, i => i.MoveSpeed);
		AddFloat("Gravity", item, i => i.Gravity);
		AddVector2I("Size", item, i => i.Size);
	}
}