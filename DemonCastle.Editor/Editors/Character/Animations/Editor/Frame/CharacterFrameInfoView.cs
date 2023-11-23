using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Character.Animations.Editor.Frame;

public partial class CharacterFrameInfoView : ControlView<SpriteDefinitionView> {
	private ICharacterFrameInfo? _info;

	public override void _ExitTree() {
		base._ExitTree();

		if (_info is INotifyPropertyChanged notify) {
			notify.PropertyChanged -= Info_OnPropertyChanged;
		}
	}

	public void Load(ICharacterFrameInfo info) {
		if (_info is INotifyPropertyChanged notify) {
			notify.PropertyChanged -= Info_OnPropertyChanged;
		}
		_info = info;
		if (_info is INotifyPropertyChanged newNotify) {
			newNotify.PropertyChanged += Info_OnPropertyChanged;
		}
		Inner.Load(info.SpriteDefinition);
	}

	private void Info_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (_info != null && e.PropertyName == nameof(ICharacterFrameInfo.SpriteDefinition)) {
			Inner.Load(_info.SpriteDefinition);
		}
	}
}