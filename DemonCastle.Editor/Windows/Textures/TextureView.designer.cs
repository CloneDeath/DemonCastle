using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class TextureView {
		protected TextureRect TextureRect { get; }

		public TextureView(Texture texture) {
			Name = nameof(TextureView);
			
			AddChild(TextureRect = new TextureRect {
				Name = nameof(TextureRect),
				Texture = texture,
			});
		}
	}
}