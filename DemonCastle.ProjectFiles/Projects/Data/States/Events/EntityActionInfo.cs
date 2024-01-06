using DemonCastle.Files.Actions;
using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.ProjectFiles.Projects.Resources;

namespace DemonCastle.ProjectFiles.Projects.Data.States.Events;

public class EntityActionInfo : BaseInfo<EntityActionData>, IListableInfo {
	public EntityActionInfo(IFileNavigator file, EntityActionData data) : base(file, data) { }

	public string ListLabel {
		get {
			if (Face != null) return $"Face {Face}";
			if (Move != null) return $"Move {Move}";
			if (Self != null) return $"Self {Self}";
			return "<Empty>";
		}
	}

	public FaceAction? Face {
		get => Data.Face;
		set {
			if (SaveField(ref Data.Face, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public MoveAction? Move {
		get => Data.Move;
		set {
			if (SaveField(ref Data.Move, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}

	public SelfAction? Self {
		get => Data.Self;
		set {
			if (SaveField(ref Data.Self, value)) {
				OnPropertyChanged(nameof(ListLabel));
			}
		}
	}
}