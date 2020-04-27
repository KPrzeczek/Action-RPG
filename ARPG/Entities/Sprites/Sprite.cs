using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ARPG.Util.Collisions;
using ARPG.Util.Collisions.Colliders;

namespace ARPG.Entities.Sprites
{
	public class Sprite : Entity
	{
		protected Texture2D texture;

		protected float layer { get; set; }
		protected Vector2 origin { get; set; }

		protected Vector2 position { get; set; }
		protected float rotation { get; set; }
		protected float scale { get; set; }

		public Color Colour { get; set; }
		public readonly Color[] TextureData;

		public float Layer
		{
			get => layer;
			set => layer = value;
		}

		public Vector2 Origin
		{
			get => origin;
			set => origin = value;
		}

		public Vector2 Position
		{
			get => position;
			set => position = value;
		}

		public float Rotation
		{
			get => rotation;
			set => rotation = value;
		}

		public float Scale
		{
			get => scale;
			set => scale = value;
		}

		public Rectangle Rectangle
		{
			get
			{
				if(texture != null)
				{
					return new Rectangle(
						((int)Position.X - (int)Origin.X) * (int)Scale, // TODO: this might be a bit buggy, maybe only origin should be multiplied by scale(?)
						((int)Position.Y - (int)Origin.Y) * (int)Scale,
						(int)texture.Width * (int)Scale,
						(int)texture.Height * (int)scale
					);
				}

				throw new Exception("Failed to find texture.");
			}
		}

		public ICollider Collider
		{ 
			get
			{
				return new BoxCollider()
				{
					Parent = this,
					Rectangle = this.Rectangle,
					Position = this.Position
				};
			} 
		}

		public Sprite(Texture2D tex)
		{
			texture = tex;
			Origin = new Vector2(texture.Width / 2, texture.Height);

			Scale = 1f;

			Colour = Color.White;
			TextureData = new Color[texture.Width * texture.Height];
			texture.GetData(TextureData);
		}

		public override void Update(float deltaTime)
		{
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			if(texture != null)
			{
				spriteBatch.Draw(texture, Position, null, Colour, Rotation, Origin, Scale, SpriteEffects.None, Layer);
			}
		}

		public new object Clone()
		{
			var sprite = this.MemberwiseClone() as Sprite;
			return sprite ?? throw new Exception("Failed to return Clone.");
		}
	}
}
