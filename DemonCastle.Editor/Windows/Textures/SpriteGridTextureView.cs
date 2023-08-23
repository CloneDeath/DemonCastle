using Godot;

namespace DemonCastle.Editor.Windows.Textures {
	public partial class SpriteGridTextureView : TextureView {
		protected int TextureWidth => Texture2D.GetWidth();
		protected int TextureHeight => Texture2D.GetHeight();
		protected int StartX => SpriteGridInfo.XOffset;
		protected int StartY => SpriteGridInfo.YOffset;
		protected int DeltaX => SpriteGridInfo.Width + SpriteGridInfo.XSeparation;
		protected int DeltaY => SpriteGridInfo.Height + SpriteGridInfo.YSeparation;
		public override void _Draw() {
			base._Draw();
			if (Texture2D == null) return;

			for (var x = StartX; x <= TextureWidth; x += DeltaX) {
				for (var y = StartY; y <= TextureHeight; y += DeltaY) {
					DrawRect(new Rect2(x + 1, y, SpriteGridInfo.Width, SpriteGridInfo.Height),
						Colors.White, false);
				}
			}
		}

		public override void _Process(float delta) {
			base._Process(delta);
			Texture = SpriteGridInfo.Texture2D;
			Update();
		}
	}
}