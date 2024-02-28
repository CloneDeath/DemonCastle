using System;
using System.Collections.Generic;
using DemonCastle.Files.Actions;
using DemonCastle.Files.BaseEntity;

namespace DemonCastle.Files;

public class ItemFile : BaseEntityFile, IGameFile {
	public int FileVersion => 1;
	public Guid InventoryAnimation { get; set; } = Guid.Empty;
	public List<ItemActionData> OnPickup { get; set; } = new();

	public float MoveSpeed = 1;
	public float Gravity = 100;
}