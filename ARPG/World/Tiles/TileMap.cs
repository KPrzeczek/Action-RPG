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
		private Tile tilePrefab;

		private Texture2D atlas;
		private List<Tile> tiles;

		public List<Tile> Tiles
		{
			get => tiles;
		}

		public int Width { get; private set; }
		public int Height { get; private set; }

		public TileMap(Texture2D atlas, Tile tile)
		{
			this.tilePrefab = tile;
			this.atlas = atlas;
			this.tiles = new List<Tile>();
		}

		public void Generate(Vector2[,] map)
		{
			for(int yy = 0; yy < map.GetLength(0); yy++)
			{
				for(int xx = 0; xx < map.GetLength(1); xx++)
				{
					var tileID = map[yy, xx];

					Tile tile = tilePrefab.Clone() as Tile;

					tile.Position = new Vector2(xx * 16, yy * 16);
					tile.AtlasPositionX = (int)tileID.X;
					tile.AtlasPositionY = (int)tileID.Y;

					tiles.Add(tile);
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
