using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class TextureView {
		protected TextureRect TextureRect { get; }

		public Texture2D Texture2D {
			get => TextureRect.Texture2D;
			set => TextureRect.Texture2D = value;
		}

		public TextureView() {
			Name = nameof(TextureView);
			
			AddChild(TextureRect = new TextureRect {
				Name = nameof(TextureRect)
			});
		}
	}
}