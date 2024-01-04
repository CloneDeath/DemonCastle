using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files.Conditions;

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfEvent {
	Killed
}