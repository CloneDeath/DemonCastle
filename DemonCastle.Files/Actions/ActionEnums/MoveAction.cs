using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Actions.ActionEnums;

[JsonConverter(typeof(StringEnumConverter))]
public enum MoveAction {
	Forward,
	Backward,
	Left,
	Right
}