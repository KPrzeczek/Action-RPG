using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Kinematic.Player.States
{
	public class PlayerIdleState : PlayerState
	{
		public PlayerIdleState(Player player) : base(player)
		{
		}

		public override void Enter()
		{
			player.AnimationManager.Restart();
			player.AnimationManager.Stop();
		}

		public override void Exit()
		{
		}

		public override void Update(float deltaTime)
		{
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
		}
	}
}
