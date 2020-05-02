using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.GUI.Util
{
	public class GuiNineSliceGrid : IGuiComponent
	{
		private Texture2D sourceTexture;

		private int cellWidth;
		private int cellHeight;

		private Rectangle topLeft;
		private Rectangle topMiddle;
		private Rectangle topRight;

		private Rectangle centreLeft;
		private Rectangle centreMiddle;
		private Rectangle centreRight;

		private Rectangle bottomLeft;
		private Rectangle bottomMiddle;
		private Rectangle bottomRight;

		public float Layer;
		public Vector2 Position;
		public float Scale;

		public int Width { get; set; }
		public int Height { get; set; }

		public GuiNineSliceGrid(Texture2D texture)
		{
			sourceTexture = texture;

			cellWidth = sourceTexture.Width / 3;
			cellHeight = sourceTexture.Height / 3;

			GenerateSlices();

			Scale = 4f;
			Layer = 0.9f;
		}

		public void Update(float deltaTime)
		{
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			#region Top

			// Top Left
			spriteBatch.Draw(
				sourceTexture,
				Position,
				topLeft,
				Color.White,
				0f,
				Vector2.Zero,
				Scale,
				SpriteEffects.None,
				Layer
			);

			// Top Middle
			for(int xx = 0; xx < Width; xx++)
			{
				spriteBatch.Draw(
					sourceTexture,
					new Vector2(
						Position.X + (cellWidth * 4) * (xx + 1),
						Position.Y + .5f
					),
					topMiddle,
					Color.White,
					0f,
					Vector2.Zero,
					Scale,
					SpriteEffects.None,
					Layer
				);
			}

			// Top Right
			spriteBatch.Draw(
				sourceTexture,
				new Vector2(
					Position.X + (cellWidth * 4) * (Width + 1),
					Position.Y + .5f
				),
				topRight,
				Color.White,
				0f,
				Vector2.Zero,
				Scale,
				SpriteEffects.None,
				Layer
			);

			#endregion

			#region Centre

			for(int yy = 0; yy < Height; yy++)
			{
				// Middle Left
				spriteBatch.Draw(
					sourceTexture,
					new Vector2(
						Position.X,
						Position.Y + ((cellHeight * 4) * yy) + (cellHeight * 4)
					),
					centreLeft,
					Color.White,
					0f,
					Vector2.Zero,
					Scale,
					SpriteEffects.None,
					Layer
				);

				// Middle Centre
				for(int xx = 0; xx < Width; xx++)
				{
					spriteBatch.Draw(
						sourceTexture,
						new Vector2(
							Position.X + ((cellWidth) * 4) * (xx + 1),
							Position.Y + ((cellHeight * 4) * yy + 1) + (cellHeight * 4)
						),
						centreMiddle,
						Color.White,
						0f,
						Vector2.Zero,
						Scale,
						SpriteEffects.None,
						Layer
					);
				}

				// Middle Right
				spriteBatch.Draw(
					sourceTexture,
					new Vector2(
						Position.X + ((cellWidth * 4) * (Width + 1)),
						Position.Y + ((cellHeight * 4) * yy + 1) + (cellHeight * 4)
					),
					centreRight,
					Color.White,
					0f,
					Vector2.Zero,
					Scale,
					SpriteEffects.None,
					Layer
				);
			}

			#endregion

			#region Bottom

			spriteBatch.Draw(
				sourceTexture,
				new Vector2(
					Position.X,
					Position.Y + (cellHeight * 4) * (Height + 1)
				),
				bottomLeft,
				Color.White,
				0f,
				Vector2.Zero,
				Scale,
				SpriteEffects.None,
				Layer
			);

			for(int xx = 0; xx < Width; xx++)
			{
				spriteBatch.Draw(
					sourceTexture,
					new Vector2(
						Position.X + (cellWidth * 4) * (xx + 1),
						Position.Y + (cellHeight * 4) * (Height + 1)
					),
					bottomMiddle,
					Color.White,
					0f,
					Vector2.Zero,
					Scale,
					SpriteEffects.None,
					Layer
				);
			}

			spriteBatch.Draw(
				sourceTexture,
				new Vector2(
					Position.X + (cellWidth * 4) * (Width + 1),
					Position.Y + (cellHeight * 4) * (Height + 1)
				),
				bottomRight,
				Color.White,
				0f,
				Vector2.Zero,
				Scale,
				SpriteEffects.None,
				Layer
			);

			#endregion
		}

		private void GenerateSlices()
		{
			#region Top

			topLeft = new Rectangle(
				0,
				0,
				cellWidth,
				cellHeight
			);

			topMiddle = new Rectangle(
				cellWidth,
				0,
				cellWidth,
				cellHeight
			);

			topRight = new Rectangle(
				cellWidth * 2,
				0,
				cellWidth,
				cellHeight
			);

			#endregion

			#region Centre

			centreLeft = new Rectangle(
				0,
				cellHeight,
				cellWidth,
				cellHeight
			);

			centreMiddle = new Rectangle(
				cellWidth,
				cellHeight,
				cellWidth,
				cellHeight
			);

			centreRight = new Rectangle(
				cellWidth * 2,
				cellHeight,
				cellWidth,
				cellHeight
			);

			#endregion

			#region Bottom

			bottomLeft = new Rectangle(
				0,
				cellHeight * 2,
				cellWidth,
				cellHeight
			);

			bottomMiddle = new Rectangle(
				cellWidth,
				cellHeight * 2,
				cellWidth,
				cellHeight
			);

			bottomRight = new Rectangle(
				cellWidth * 2,
				cellHeight * 2,
				cellWidth,
				cellHeight
			);

			#endregion
		}
	}
}
