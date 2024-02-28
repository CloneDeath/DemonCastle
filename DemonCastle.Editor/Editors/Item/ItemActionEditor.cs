using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Item;

public partial class ItemActionEditor : ActionEditor<ItemActionInfo> {
	public ItemActionEditor(IEnumerableInfo<ItemActionInfo> collection, ItemActionInfo action) : base(collection, action) {
		Name = nameof(ItemActionEditor);

		AddChild(new ChoiceTree {
			{
				nameof(action.Player),
				action.Player.IsSet,
				c => {
					action.Player.IsSet = true;
					c.AddChild(new ChoiceTree {
						{
							"Recover Hp",
							action.Player.RecoverHp != null,
							p => {
								action.Player.RecoverHp ??= 1;
								var binding = new CallbackBinding<int>(
									() => action.Player.RecoverHp ?? 0,
									(value) => action.Player.RecoverHp = value);
								p.AddChild(new IntegerProperty(binding));
							}
						},
						{
							"Recover Mp",
							action.Player.RecoverMp != null,
							p => {
								action.Player.RecoverMp ??= 1;
								var binding = new CallbackBinding<int>(
									() => action.Player.RecoverMp ?? 0,
									(value) => action.Player.RecoverMp = value);
								p.AddChild(new IntegerProperty(binding));
							}
						}
					});
				}
			}
		});
	}
}