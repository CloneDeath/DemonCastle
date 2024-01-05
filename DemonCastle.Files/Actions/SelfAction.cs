using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Actions;

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfAction {
	Despawn
}