using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.World.Tiles
{
	public class Tile
	{
		private Rectangle rectangle;

		protected int tileSizeX;
		protected int tileSizeY;

		protected Texture2D atlas;

		public Vector2 Position { get; set; }

		public int AtlasPositionX = 0;
		public int AtlasPositionY = 0;

		public int TileSizeX
		{
			get => tileSizeX;
		}

		public int TileSizeY
		{
			get => tileSizeY;
		}

		public Rectangle Rectangle
		{
			get => rectangle;
			protected set => rectangle = value;
		}

		public Tile(Texture2D texture, int tilesX, int tilesY)
		{
			atlas = texture;

			rectangle.Width = atlas.Width;
			rectangle.Height = atlas.Height;

			tileSizeX = atlas.Width / tilesX;
			tileSizeY = atlas.Height / tilesY;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				atlas,
				Position,
				new Rectangle(
					rectangle.X + (tileSizeX * AtlasPositionX),
					rectangle.Y + (tileSizeY * AtlasPositionY),
					tileSizeX,
					tileSizeY
				),
				Color.White
			);
		}
	}
}
