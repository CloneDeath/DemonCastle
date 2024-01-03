using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files.BaseEntity;

[JsonConverter(typeof(StringEnumConverter))]
public enum SelfAction {
	Despawn
}