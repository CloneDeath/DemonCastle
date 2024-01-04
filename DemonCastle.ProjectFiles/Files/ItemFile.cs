using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Actions;
using DemonCastle.ProjectFiles.Files.BaseEntity;

namespace DemonCastle.ProjectFiles.Files;

public class ItemFile : BaseEntityFile {
	public Guid InventoryAnimation { get; set; } = Guid.Empty;
	public List<PlayerActionData> OnPickup { get; set; } = new();
}