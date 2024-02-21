using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components;

public partial class ActionEditor<T> : HBoxContainer {
	protected readonly IEnumerableInfo<T> Collection;
	protected readonly T Action;

	private Button DeleteButton { get; }
	private Button MoveUpButton { get; }
	private Button MoveDownButton { get; }

	public ActionEditor(IEnumerableInfo<T> collection, T action) {
		Collection = collection;
		Action = action;
		Name = nameof(ActionEditor<T>);

		AddChild(DeleteButton = new Button {
			Icon = IconTextures.DeleteIcon,
			TooltipText = "Delete Action"
		}, @internal: InternalMode.Front);
		DeleteButton.Pressed += DeleteButton_OnPressed;

		VBoxContainer moveButtons;
		AddChild(moveButtons = new VBoxContainer(), @internal: InternalMode.Back);

		moveButtons.AddChild(MoveUpButton = new Button {
			Icon = IconTextures.UpIcon,
			TooltipText = "Move Up"
		});
		MoveUpButton.Pressed += MoveUpButton_OnPressed;
		MoveUpButton.Disabled = !Collection.CanMoveUp(Action);

		moveButtons.AddChild(MoveDownButton = new Button {
			Icon = IconTextures.DownIcon,
			TooltipText = "Move Down"
		});
		MoveDownButton.Pressed += MoveDownButton_OnPressed;
		MoveDownButton.Disabled = !Collection.CanMoveDown(Action);
	}

	private void DeleteButton_OnPressed() {
		Collection.Remove(Action);
		QueueFree();
	}

	private void MoveUpButton_OnPressed() => Collection.MoveUp(Action);
	private void MoveDownButton_OnPressed() => Collection.MoveDown(Action);
}