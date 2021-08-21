using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites {
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
			Shader = ResourceLoader.Load<Shader>($"res://DemonCastle.ProjectFiles/Projects/Data/Sprites/{nameof(TransparentColorSpriteShader)}.shader");
		}
	}
}