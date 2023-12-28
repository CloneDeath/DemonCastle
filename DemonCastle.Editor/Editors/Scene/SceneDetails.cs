using DemonCastle.Editor.Editors.Properties;
using DemonCastle.Editor.Editors.Properties.Color;
using DemonCastle.ProjectFiles.Projects.Data;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneDetails : PropertyCollection {
	public SceneDetails(SceneInfo scene) {
		AddString("Name", scene, s => s.Name);
		AddVector2I("Size", scene, s => s.Size);
		AddColor("Background Color", scene, s => s.BackgroundColor, new ColorPropertyOptions { EditAlpha = true });
	}
}