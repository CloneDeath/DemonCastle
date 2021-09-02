using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class SpriteGridTextureView : TextureView {
		protected Texture Texture => SpriteGridInfo.Texture;
		protected int TextureWidth => Texture.GetWidth();
		protected int TextureHeight => Texture.GetHeight();
		protected int StartX => SpriteGridInfo.XOffset;
		protected int StartY => SpriteGridInfo.YOffset;
		protected int DeltaX => SpriteGridInfo.Width + SpriteGridInfo.XSeparation;
		protected int DeltaY => SpriteGridInfo.Height + SpriteGridInfo.YSeparation;
		public override void _Draw() {
			base._Draw();

			for (var x = StartX; x <= TextureWidth; x += DeltaX) {
				for (var y = StartY; y <= TextureHeight; y += DeltaY) {
					DrawRect(new Rect2(x + 1, y, SpriteGridInfo.Width, SpriteGridInfo.Height),
						Colors.White, false);
				}
			}
		}
	}
}