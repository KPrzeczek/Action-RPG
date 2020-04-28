using ARPG.Models.Sprites.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using ARPG.Util.States;
using ARPG.Entities.Sprites.Kinematic.Player.States;
using ARPG.Models.Sprites.Player;

namespace ARPG.Entities.Sprites.Kinematic.Player
{
	public class Player : Sprite, ICollidable
	{
		private StateMachine stateMachine;

		private bool isMoving;

		private const float MOVE_SPEED = 75f;
		private float DIAG_SPEED = (float)(MOVE_SPEED * (Math.Sqrt(2f) / 2f));
		private float currentSpeed = 0f;

		private Vector2 velocity;
		private Vector2 movement;

		public PlayerStats Stats { get; set; }

		public Player(Dictionary<string, Animation> anims) : base(anims)
		{
			stateMachine = new StateMachine(new PlayerIdleState(this));
			Stats = new PlayerStats();

			AutoSpriteSorter.Continuous = true;
		}

		#region Methods

		public override void Update(float deltaTime)
		{
			#region Input

			int inputX = 0;
			int inputY = 0;

			bool isKeyDown(Keys k) => Keyboard.GetState().IsKeyDown(k);

			if(isKeyDown(Keys.W))
				inputY -= 1;
			if(isKeyDown(Keys.S))
				inputY += 1;

			if(isKeyDown(Keys.A))
				inputX -= 1;
			if(isKeyDown(Keys.D))
				inputX += 1;

			movement.X = inputX;
			movement.Y = inputY;

			#endregion

			velocity = movement * currentSpeed * deltaTime;

			#region Diagonal Speed

			if(velocity.X != 0 && velocity.Y != 0)
				currentSpeed = DIAG_SPEED;
			else
				currentSpeed = MOVE_SPEED;

			#endregion

			if(velocity != Vector2.Zero)
				isMoving = true;
			else
				isMoving = false;

			if(!isMoving)
			{
				RequestState(new PlayerIdleState(this));
			}
			else if(isMoving && !(stateMachine.CurrentState is PlayerRunningState))
			{
				RequestState(new PlayerWalkState(this));
			}

			// Update Position
			Position += velocity * Stats.SpeedModifier;

			// Misc
			stateMachine.Update(deltaTime);
			AnimationManager.Update(deltaTime);
			AutoSpriteSorter.Update(deltaTime);
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
