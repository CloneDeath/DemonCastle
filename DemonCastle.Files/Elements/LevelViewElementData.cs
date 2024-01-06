using DemonCastle.Files.Common;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.LevelView)]
public class LevelViewElementData : ElementData {
	public LevelViewElementData() {
		Name = "Level View";
		Type = ElementType.LevelView;
		Region = new Region2I(0, 0, 64, 64);
	}
}