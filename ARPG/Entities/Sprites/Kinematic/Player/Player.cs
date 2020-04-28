using ARPG.Util.Collisions.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARPG.Entities.Sprites.Kinematic.Player
{
	public class Player : Sprite, ICollidable
	{
		private Vector2 velocity;
		private Vector2 movement;

		public Player(Texture2D tex) : base(tex)
		{
		}

		public override void Update(float deltaTime)
		{
		}

		public void OnCollide(Sprite other)
		{
		}
	}
}
