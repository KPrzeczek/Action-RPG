using ARPG.Models.Sprites.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ARPG.Util.States;
using ARPG.Entities.Sprites.Kinematic.Player.States;
using ARPG.Models.Sprites.Player;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Kinematic.Player
{
	public class Player : Sprite, ICollidable
	{
		#region Fields

		private StateMachine stateMachine;

		private bool isMoving;

		private const float MOVE_SPEED = 75f;
		private float DIAG_SPEED = (float)(MOVE_SPEED * (Math.Sqrt(2f) / 2f));
		private float currentSpeed = 0f;

		private Vector2 velocity;
		private Vector2 movement;

		#endregion

		#region Properties

		public PlayerInput Input { get; set; }
		public PlayerStats Stats { get; set; }

		#endregion

		#region Methods

		public Player(Dictionary<string, Animation> anims) : base(anims)
		{
			Game1.NativeCamera.TargetSprite = this;

			stateMachine = new StateMachine(new PlayerIdleState(this));

			Stats = new PlayerStats();
			Input = new PlayerInput()
			{
				MoveUp = Keys.W,
				MoveDown = Keys.S,
				MoveLeft = Keys.A,
				MoveRight = Keys.D,

				Run = Keys.LeftShift
			};

			AutoSpriteSorter.Continuous = true;
		}

		public override void Update(float deltaTime)
		{
			#region Input

			int inputX = 0;
			int inputY = 0;

			bool isKeyDown(Keys k) => Keyboard.GetState().IsKeyDown(k);

			if(isKeyDown(Input.MoveUp))
				inputY -= 1;
			if(isKeyDown(Input.MoveDown))
				inputY += 1;

			if(isKeyDown(Input.MoveLeft))
				inputX -= 1;
			if(isKeyDown(Input.MoveRight))
				inputX += 1;

			movement.X = inputX;
			movement.Y = inputY;

			#endregion

			velocity = movement * (currentSpeed * Stats.SpeedModifier) * deltaTime;

			#region Diagonal Speed

			if(velocity.X != 0 && velocity.Y != 0)
				currentSpeed = DIAG_SPEED;
			else
				currentSpeed = MOVE_SPEED;

			#endregion

			#region State Switching

			if(velocity != Vector2.Zero)
				isMoving = true;
			else
				isMoving = false;

			if(!isMoving)
				RequestState(new PlayerIdleState(this));
			else if(isMoving && !(stateMachine.CurrentState is PlayerRunningState))
				RequestState(new PlayerWalkState(this));

			#endregion

			if(movement.X > 0)
			{
				FlipHorizontal = false;
			}
			else if(movement.X < 0)
			{
				FlipHorizontal = true;
			}

			Position += velocity;

			#region Misc

			stateMachine.Update(deltaTime);
			AnimationManager.Update(deltaTime);
			AutoSpriteSorter.Update(deltaTime);

			#endregion
		}

		public void OnCollide(Sprite other)
		{
		}

		#endregion

		#region State Management

		public void RequestState(IState requestedState)
		{
			if(requestedState is PlayerIdleState)
			{
				//if(movementStateMachine.CurrentState is PlayerWalkState)
				stateMachine.ChangeState(requestedState);
			}

			if(requestedState is PlayerWalkState)
			{
				if(stateMachine.CurrentState is PlayerIdleState || stateMachine.CurrentState is PlayerRunningState)
					stateMachine.ChangeState(requestedState);
			}

			if(requestedState is PlayerRunningState)
			{
				if(stateMachine.CurrentState is PlayerWalkState)
					stateMachine.ChangeState(requestedState);
			}
		}

		#endregion
	}
}
