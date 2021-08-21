using Godot;
using Godot.LocalResources;

namespace DemonCastle.Projects.Data.Sprites {
	public class TransparentColorSpriteShader : ShaderMaterial {
		public float Threshold {
			get => (float)GetShaderParam("threshold");
			set => SetShaderParam("threshold", value);
		}

		public Color TransparentColor {
			get => (Color) GetShaderParam("transparent_color");
			set => SetShaderParam("transparent_color", value);
		}
		
		public TransparentColorSpriteShader() {
			Shader = LocalResource<TransparentColorSpriteShader>.Load<Shader>($"{nameof(TransparentColorSpriteShader)}.shader");
		}
	}
}