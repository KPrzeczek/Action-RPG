using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ARPG.Util.Collisions;

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
			Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) / 4;
		}

		public void OnCollide(Sprite other)
		{
			Console.WriteLine("E");
		}
	}
}
