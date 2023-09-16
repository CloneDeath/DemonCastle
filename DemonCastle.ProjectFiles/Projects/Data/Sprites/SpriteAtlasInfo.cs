using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Extensions;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites; 

public class SpriteAtlasInfo : FileInfo<SpriteAtlasFile>, ISpriteSource {
	public IEnumerable<SpriteAtlasDataInfo> SpriteData => Resource.Sprites.Select(s => new SpriteAtlasDataInfo(this, s));

	public string SpriteFile {
		get => Resource.File;
		set { Resource.File = value; Save(); }
	}

	public Color TransparentColor {
		get => Resource.TransparentColor.ToColor();
		set { Resource.TransparentColor = value.ToColorData(); Save(); }
	}

	public Texture2D Texture => File.GetTexture(Resource.File);

	public SpriteAtlasInfo(FileNavigator<SpriteAtlasFile> file) : base(file) { }

	public ISpriteDefinition GetSpriteDefinition(string spriteName) {
		return SpriteData.FirstOrDefault(s => s.Name == spriteName)
			   ?? (ISpriteDefinition) new NullSpriteDefinition();
	}

	public SpriteAtlasDataInfo CreateSprite() {
		var spriteAtlasData = new SpriteAtlasData();
		Resource.Sprites.Add(spriteAtlasData);
		return new SpriteAtlasDataInfo(this, spriteAtlasData);
	}
}