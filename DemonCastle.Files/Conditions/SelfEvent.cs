using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Conditions;

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfEvent {
	Killed
}