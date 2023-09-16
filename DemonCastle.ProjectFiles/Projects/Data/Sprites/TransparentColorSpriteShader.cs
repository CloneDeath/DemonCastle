using Godot;

namespace DemonCastle.ProjectFiles.Projects.Data.Sprites; 

public partial class TransparentColorSpriteShader : ShaderMaterial {
	public float Threshold {
		get => (float)GetShaderParameter("threshold");
		set => SetShaderParameter("threshold", value);
	}

	public Color TransparentColor {
		get => (Color) GetShaderParameter("transparent_color");
		set => SetShaderParameter("transparent_color", value);
	}
		
	public TransparentColorSpriteShader() {
		Shader = ResourceLoader.Load<Shader>($"res://DemonCastle.ProjectFiles/Projects/Data/Sprites/{nameof(TransparentColorSpriteShader)}.gdshader");
	}
}