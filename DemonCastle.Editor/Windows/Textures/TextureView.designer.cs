using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class TextureView {
		protected TextureRect TextureRect { get; }

		public Texture2D Texture {
			get => TextureRect.Texture;
			set => TextureRect.Texture = value;
		}

		public TextureView() {
			Name = nameof(TextureView);
			
			AddChild(TextureRect = new TextureRect {
				Name = nameof(TextureRect)
			});
		}
	}
}