using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ARPG.Util.Collisions;
using ARPG.Util.Collisions.Colliders;
using ARPG.Models.Sprites.Util;
using System.Collections.Generic;
using ARPG.Managers.Sprites;
using System.Linq;
using ARPG.Entities.Sprites.Util.Drawing;

namespace ARPG.Entities.Sprites
{
	public class Sprite : Entity
	{
		protected Dictionary<string, Animation> animations;
		protected AnimationManager animationManager;

		protected AutoSpriteSorter autoSpriteSorter;

		protected Texture2D texture;

		protected bool flipHorizontal { get; set; }

		protected float layer { get; set; }
		protected Vector2 origin { get; set; }

		protected Vector2 position { get; set; }
		protected float rotation { get; set; }
		protected float scale { get; set; }

		public bool FlipHorizontal
		{
			get
			{
				return flipHorizontal;
			}
			set
			{
				flipHorizontal = value;
				if(AnimationManager != null)
					AnimationManager.FlipHorizontal = value;
			}
		}

		public Color Colour { get; set; }
		public readonly Color[] TextureData;

		/// <summary>
		/// Normally Sprites sort via their Origin.Y Point, however this offsets that
		/// meaning sprites can sort at different areas without actually affecting the
		/// actual drawing position of the sprite
		/// </summary>

		public AutoSpriteSorter AutoSpriteSorter => autoSpriteSorter;

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
						(int)Position.X - (int)(Origin.X * Scale),
						(int)Position.Y - (int)(Origin.Y * Scale),
						(int)(texture.Width * Scale),
						(int)(texture.Height * Scale)
					);
				}

				if(animationManager != null)
				{
					var anim = animations.FirstOrDefault().Value;

					return new Rectangle(
						(int)Position.X - (int)(Origin.X * animationManager.Scale),
						(int)Position.Y - (int)(Origin.Y * animationManager.Scale),
						(int)(anim.FrameWidth * animationManager.Scale),
						(int)(anim.FrameHeight * animationManager.Scale)
					);
				}

				throw new Exception("Failed to find texture.");
			}
		}

		protected float colliderOffsetX;
		protected float colliderOffsetY;
		protected float colliderOffsetWidth;
		protected float colliderOffsetHeight;

		public Collider Collider
		{
			get
			{
				return new BoxCollider()
				{
					Parent = this,
					Rectangle = new Rectangle(
						(int)(Rectangle.X + colliderOffsetX),
						(int)(Rectangle.Y + colliderOffsetY),
						(int)(Rectangle.Width + colliderOffsetWidth),
						(int)(Rectangle.Height + colliderOffsetHeight)
					)
				};
			}
		}

		public Sprite(Texture2D tex)
		{
			texture = tex;
			Origin = new Vector2(texture.Width / 2, texture.Height);

			autoSpriteSorter = new AutoSpriteSorter(this);
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

			autoSpriteSorter = new AutoSpriteSorter(this);
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
				var spriteEffects = SpriteEffects.None;

				if(FlipHorizontal)
					spriteEffects = SpriteEffects.FlipHorizontally;

				spriteBatch.Draw(texture, Position, null, Colour, Rotation, Origin, Scale, spriteEffects, Layer);
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
