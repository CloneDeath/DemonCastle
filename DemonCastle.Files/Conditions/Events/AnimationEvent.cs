using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Conditions.Events;

[JsonConverter(typeof(StringEnumConverter))]
public enum AnimationEvent {
	Complete
}