using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.World.Tiles.Forest;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.World.Tiles
{
	public class TileMap
	{
		private Texture2D atlas;
		private List<Tile> tiles;

		public List<Tile> Tiles
		{
			get => tiles;
		}

		public int Width { get; private set; }
		public int Height { get; private set; }

		public TileMap(Texture2D tileAtlas)
		{
			atlas = tileAtlas;
			this.tiles = new List<Tile>();
		}

		public void Generate(Vector2[,] map)
		{
			for(int yy = 0; yy < map.GetLength(0); yy++)
			{
				for(int xx = 0; xx < map.GetLength(1); xx++)
				{
					var tileID = map[yy, xx];

					tiles.Add(new ForestFloorTile(atlas, 3, 1)
					{
						Position = new Vector2(xx * 16, yy * 16),
						AtlasPositionX = (int)tileID.X,
						AtlasPositionY = (int)tileID.Y
					});
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach(var tile in tiles)
			{
				tile.Draw(spriteBatch);
			}
		}
	}
}
