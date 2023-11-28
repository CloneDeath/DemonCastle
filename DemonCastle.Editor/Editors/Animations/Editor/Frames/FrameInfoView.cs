using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Animations;

namespace DemonCastle.Editor.Editors.Animations.Editor.Frames;

public partial class FrameInfoView : ControlView<SpriteDefinitionView> {
	private IFrameInfo? _info;

	public override void _ExitTree() {
		base._ExitTree();

		if (_info is INotifyPropertyChanged notify) {
			notify.PropertyChanged -= Info_OnPropertyChanged;
		}
	}

	public void Load(IFrameInfo info) {
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
		if (_info != null && e.PropertyName == nameof(IFrameInfo.SpriteDefinition)) {
			Inner.Load(_info.SpriteDefinition);
		}
	}
}