using System;

namespace DemonCastle.Files.Actions.DataValues;

/// <summary>
/// References a Monster, Item, etc...
/// </summary>
public class ReferenceData {
	public Guid? Id { get; set; }
	public Guid? Variable { get; set; }
}