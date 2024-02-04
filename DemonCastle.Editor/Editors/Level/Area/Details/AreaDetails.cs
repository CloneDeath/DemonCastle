using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;

namespace DemonCastle.Editor.Editors.Level.Area.Details;

public partial class AreaDetails : PropertyCollection {
	private readonly AreaProxy _areaProxy = new();

	public AreaInfo? Area {
		get => _areaProxy.Proxy;
		set {
			_areaProxy.Proxy = value;
			if (value == null) {
				Disable();
			} else {
				Enable();
			}
		}
	}

	public AreaDetails() {
		Name = nameof(AreaDetails);

		AddString("Name", _areaProxy, x => x.Name);
		AddVector2I("Position", _areaProxy, x => x.AreaPosition);
		AddVector2I("Size", _areaProxy, x => x.Size);

		Disable();
	}
}