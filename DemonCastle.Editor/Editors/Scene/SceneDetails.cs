using DemonCastle.Editor.Editors.Components.Properties.Color;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Scene;

public partial class SceneDetails : PropertyCollection {
	public SceneDetails(SceneInfo scene) {
		AddString("Name", scene, s => s.Name);
		AddVector2I("Size", scene, s => s.Size);
		AddColor("Background Color", scene, s => s.BackgroundColor, new ColorPropertyOptions { EditAlpha = true });
	}
}