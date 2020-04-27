using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ARPG.Util.Collisions;
using ARPG.Util.Collisions.Colliders;
using ARPG.Models.Sprites.Util;
using System.Collections.Generic;
using ARPG.Managers.Sprites;
using System.Linq;

namespace ARPG.Entities.Sprites
{
	public class Sprite : Entity
	{
		protected Dictionary<string, Animation> animations;
		protected AnimationManager animationManager;

		protected Texture2D texture;

		protected float layer { get; set; }
		protected Vector2 origin { get; set; }

		protected Vector2 position { get; set; }
		protected float rotation { get; set; }
		protected float scale { get; set; }

		public Color Colour { get; set; }
		public readonly Color[] TextureData;

		/// <summary>
		/// Normally Sprites sort via their Origin.Y Point, however this offsets that
		/// meaning sprites can sort at different areas without actually affecting the
		/// actual drawing position of the sprite
		/// </summary>
		public int YSortOffset { get; set; }

		public AnimationManager AnimationManager => animationManager;

		public float Layer
		{
			get => layer;
			set
			{
				layer = value;
				if(AnimationManager != null)
					AnimationManager.Layer = value;
			}
		}

		public Vector2 Origin
		{
			get => origin;
			set
			{
				origin = value;
				if(AnimationManager != null)
					AnimationManager.Origin = value;
			}
		}

		public Vector2 Position
		{
			get => position;
			set
			{
				position = value;
				if(AnimationManager != null)
					AnimationManager.Position = value;
			}
		}

		public float Rotation
		{
			get => rotation;
			set
			{
				rotation = value;
				if(AnimationManager != null)
					AnimationManager.Rotation = value;
			}
		}

		public float Scale
		{
			get => scale;
			set
			{
				scale = value;
				if(AnimationManager != null)
					AnimationManager.Scale = value;
			}
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

				if(animationManager != null)
				{
					var anim = animations.FirstOrDefault().Value;
					return new Rectangle(
						((int)Position.X - (int)Origin.X) * (int)animationManager.Scale, // TODO: this might be a bit buggy, maybe only origin should be multiplied by scale(?)
						((int)Position.Y - (int)Origin.Y) * (int)animationManager.Scale,
						(int)anim.FrameWidth * (int)animationManager.Scale,
						(int)anim.FrameHeight * (int)animationManager.Scale
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

		public Sprite(Dictionary<string, Animation> anims)
		{
			animations = anims;
			Animation animation = animations.FirstOrDefault().Value;
			animationManager = new AnimationManager(animation);

			texture = null;
			Colour = Color.White;
			TextureData = new Color[animation.Texture.Width * animation.Texture.Height];
			animation.Texture.GetData(TextureData);

			Scale = 1f;
			Origin = new Vector2((float)animation.FrameWidth / 2, (float)animation.FrameHeight / 2);
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
			else
			{
				animationManager?.Draw(spriteBatch);
			}
		}

		public new object Clone()
		{
			var sprite = this.MemberwiseClone() as Sprite;

			if(animations != null && sprite != null)
			{
				sprite.animations = animations.ToDictionary(c => c.Key, v => v.Value.Clone() as Animation);
				sprite.animationManager = sprite.animationManager.Clone() as AnimationManager;
			}

			return sprite ?? throw new Exception("Failed to return Clone.");
		}
	}
}
