using System.Collections.Generic;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Sprites.SpriteDefinition;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
	public class SpriteAtlasInfo : FileInfo<SpriteAtlasFile>, ISpriteSource {
		public List<SpriteAtlasDataInfo> SpriteData { get; }

		public string SpriteFile => Resource.File;

		public Color TransparentColor => Color.Color8(
			(byte) Resource.TransparentColor.Red,
			(byte) Resource.TransparentColor.Green,
			(byte) Resource.TransparentColor.Blue);

		public Texture Texture => File.GetTexture(Resource.File);

		public SpriteAtlasInfo(FileNavigator<SpriteAtlasFile> file) : base(file) {
			SpriteData = Resource.Sprites.Select(s => new SpriteAtlasDataInfo(this, s)).ToList();
		}

		public ISpriteDefinition GetSpriteDefinition(string spriteName) {
			return SpriteData.FirstOrDefault(s => s.Name == spriteName)
				?? (ISpriteDefinition) new NullSpriteDefinition();
		}
	}
}