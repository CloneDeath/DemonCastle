using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Actions;

namespace DemonCastle.ProjectFiles.Files;

public class ItemFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;



	public List<PlayerActionData> OnPickup { get; set; } = new();
}