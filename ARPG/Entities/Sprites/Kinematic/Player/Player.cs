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
			var keyboard = Keyboard.GetState();

			if(keyboard.IsKeyDown(Keys.W))
				movement.Y = -1;
			else if(keyboard.IsKeyDown(Keys.S))
				movement.Y = 1;
			else
				movement.Y = 0;

			if(keyboard.IsKeyDown(Keys.A))
				movement.X = -1;
			else if(keyboard.IsKeyDown(Keys.D))
				movement.X = 1;
			else
				movement.X = 0;

			velocity = movement * 25f * deltaTime;

			Position += velocity;
			
			/*
			Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y) / 4;
			*/
		}

		public void OnCollide(Sprite other)
		{
			Console.WriteLine("E");
		}
	}
}
