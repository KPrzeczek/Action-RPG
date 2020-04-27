using ARPG.Util.Collisions.Colliders;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.Collisions
{
	/*
	 * THIS WAS NOT MADE BY ME. MOST OF THE CODE COLLISION DETECTING CODE IS HERE:
	 * https://github.com/OneLoneCoder/olcPixelGameEngine/blob/master/Videos/OneLoneCoder_PGE_PolygonCollisions1.cpp
	 * 
	 * WHILE IT WAS WRITTEN USING C++, IT WAS TRIVIAL TO PORT TO C#
	 */

	public static class CollisionHelper
	{
		#region Intersection Methods
		/// <summary>
		/// Much more complex than AABB collision detection. Works with CONVEX shapes.
		/// WARNING :: DO NOT USE CONCAVE SHAPES
		/// </summary>
		/// <param name="r1"></param>
		/// <param name="r2"></param>
		/// <returns></returns>
		public static bool ShapeOverlap_SAT(PolygonCollider r1, PolygonCollider r2)
		{
			PolygonCollider poly1 = r1;
			PolygonCollider poly2 = r2;

			for(int shape = 0; shape < 2; shape++)
			{
				if(shape == 1)
				{
					poly1 = r2;
					poly2 = r1;
				}

				for(int a = 0; a < poly1.Points.Count; a++)
				{
					int b = (a + 1) % poly1.Points.Count;
					Vector2 axisProj = new Vector2(-(poly1.Points[b].Y - poly1.Points[a].Y), poly1.Points[b].X - poly1.Points[a].X);

					float min_r1 = Single.PositiveInfinity;
					float max_r1 = Single.NegativeInfinity;

					for(int p = 0; p < poly1.Points.Count; p++)
					{
						float q = (poly1.Points[p].X * axisProj.X + poly1.Points[p].Y * axisProj.Y);
						min_r1 = Math.Min(min_r1, q);
						max_r1 = Math.Max(max_r1, q);
					}

					float min_r2 = Single.PositiveInfinity;
					float max_r2 = Single.NegativeInfinity;

					for(int p = 0; p < poly2.Points.Count; p++)
					{
						float q = (poly2.Points[p].X * axisProj.X + poly2.Points[p].Y * axisProj.Y);
						min_r2 = Math.Min(min_r2, q);
						max_r2 = Math.Max(max_r2, q);
					}

					if(!(max_r2 >= min_r1 && max_r1 >= min_r2))
						return false;
				}
			}

			return true;
		}

		public static bool ShapeOverlap_AABB(BoxCollider r1, BoxCollider r2)
		{
			var b1 = r1.Rectangle;
			var b2 = r2.Rectangle;

			if(b1.X < b2.X + b2.Width &&
			    b1.X + b1.Width > b2.X &&
			    b1.Y < b2.Y + b2.Height &&
			    b1.Y + b1.Height > b2.Y)
			{
				return true;
			}

			return false;
		}

		/*
		 * https://github.com/KrossX/misc/blob/master/lwings/src/lwings_game.c#L2112
		 */

		public static bool ShapeOverlap_AABB_STATIC(BoxCollider r1, BoxCollider r2)
		{
			var b1 = r1.Rectangle;
			var b2 = r2.Rectangle;

			if(ShapeOverlap_AABB(r1, r2))
			{
				//======[COLLISION VALUES]======//
				float dl = Math.Abs(b1.Right - b2.Left);
				float dt = Math.Abs(b1.Bottom - b2.Top);
				float dr = Math.Abs(b1.Left - b2.Right);
				float db = Math.Abs(b1.Top - b2.Bottom);

				float dh = (dl < dr) ? dl : dr;
				float dv = (dt < db) ? dt : db;
				//==============================//

				//======[COLLISION RESPONSE]====//
				var pos = r1.Parent.Position;

				if(dh < dv)
				{
					pos.X += (dl < dr) ? -dl : dr;
				}
				else
				{
					pos.Y += (dt < db) ? -dt : db;
				}

				r1.Parent.Position = pos;
				//==============================//
			}

			return false;
		}

		/*
		public static bool ShapeOverlap_SAT_STATIC(PolygonCollider r1, PolygonCollider r2)
		{
			PolygonCollider poly1 = r1;
			PolygonCollider poly2 = r2;

			float overlap = Single.PositiveInfinity;

			for(int shape = 0; shape < 2; shape++)
			{
				if(shape == 1)
				{
					poly1 = r2;
					poly2 = r1;
				}

				for(int a = 0; a < poly1.Points.Count; a++)
				{
					int b = (a + 1) % poly1.Points.Count;

					Vector2 axisProj = new Vector2(
						-(poly1.Points[b].Y - poly1.Points[a].Y),
						poly1.Points[b].X - poly1.Points[a].X
					);

					float d = (float)Math.Sqrt(axisProj.X * axisProj.X + axisProj.Y * axisProj.Y);
					axisProj = new Vector2(axisProj.X / d, axisProj.Y / d);

					float min_r1 = Single.PositiveInfinity;
					float max_r1 = Single.NegativeInfinity;

					for(int p = 0; p < poly1.Points.Count; p++)
					{
						float q = (poly1.Points[p].X * axisProj.X + poly1.Points[p].Y * axisProj.Y);
						min_r1 = Math.Min(min_r1, q);
						max_r1 = Math.Max(max_r1, q);
					}

					float min_r2 = Single.PositiveInfinity;
					float max_r2 = Single.NegativeInfinity;

					for(int p = 0; p < poly2.Points.Count; p++)
					{
						float q = (poly2.Points[p].X * axisProj.X + poly2.Points[p].Y * axisProj.Y);
						min_r2 = Math.Min(min_r2, q);
						max_r2 = Math.Max(max_r2, q);
					}

					overlap = Math.Min(
						Math.Min(max_r1, max_r2) - Math.Max(min_r1, min_r2),
						overlap
					);

					if(!(max_r2 >= min_r1 && max_r1 >= min_r2))
						return false;
				}
			}

			#region Collision Resolution

			Vector2 dist = new Vector2(r2.Position.X - r1.Position.X, r2.Position.Y - r1.Position.Y);
			float s = (float)Math.Sqrt(dist.X * dist.X + dist.Y * dist.Y);

			var pos = r1.Parent.Position;



			r1.Parent.Position = pos;

			#endregion

			return false;
		}

		// THESE FUNCTIONS ARE EXPERIMENTAL AND DONT WORK EXACTLY WELL
		public static bool ShapeOverlap_DIAGS(PolygonCollider r1, PolygonCollider r2)
		{
			PolygonCollider poly1 = r1;
			PolygonCollider poly2 = r2;

			for(int shape = 0; shape < 2; shape++)
			{
				if(shape == 1)
				{
					poly1 = r2;
					poly2 = r1;
				}

				for(int p = 0; p < poly1.Points.Count; p++)
				{
					Vector2 line_r1s = poly1.Position;
					Vector2 line_r1e = poly1.Points[p];

					for(int q = 0; q < poly2.Points.Count; q++)
					{
						Vector2 line_r2s = poly2.Points[q];
						Vector2 line_r2e = poly2.Points[(q + 1) % poly2.Points.Count];

						float h = (line_r2e.X - line_r2s.X) * (line_r1s.Y - line_r1e.Y) - (line_r1s.X - line_r1e.X) * (line_r2e.Y - line_r2s.Y);
						float t1 = ((line_r2s.Y - line_r2e.Y) * (line_r1s.X - line_r2s.X) + (line_r2e.X - line_r2s.X) * (line_r1s.Y - line_r2s.Y)) / h;
						float t2 = ((line_r1s.Y - line_r1e.Y) * (line_r1s.X - line_r2s.X) + (line_r1e.X - line_r1s.X) * (line_r1s.Y - line_r2s.Y)) / h;

						if(t1 >= 0f && t1 < 1f && t2 >= 0f && t2 < 1f)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public static bool ShapeOverlap_DIAGS_STATIC(PolygonCollider r1, PolygonCollider r2)
		{
			PolygonCollider poly1 = r1;
			PolygonCollider poly2 = r2;

			for(int shape = 0; shape < 2; shape++)
			{
				if(shape == 1)
				{
					poly1 = r2;
					poly2 = r1;
				}

				for(int p = 0; p < poly1.Points.Count; p++)
				{
					Vector2 line_r1s = poly1.Position;
					Vector2 line_r1e = poly1.Points[p];

					Vector2 displacement = Vector2.Zero;

					for(int q = 0; q < poly2.Points.Count; q++)
					{
						Vector2 line_r2s = poly2.Points[q];
						Vector2 line_r2e = poly2.Points[(q + 1) % poly2.Points.Count];

						float h = (line_r2e.X - line_r2s.X) * (line_r1s.Y - line_r1e.Y) - (line_r1s.X - line_r1e.X) * (line_r2e.Y - line_r2s.Y);
						float t1 = ((line_r2s.Y - line_r2e.Y) * (line_r1s.X - line_r2s.X) + (line_r2e.X - line_r2s.X) * (line_r1s.Y - line_r2s.Y)) / h;
						float t2 = ((line_r1s.Y - line_r1e.Y) * (line_r1s.X - line_r2s.X) + (line_r1e.X - line_r1s.X) * (line_r1s.Y - line_r2s.Y)) / h;

						if(t1 >= 0f && t1 < 1f && t2 >= 0f && t2 < 1f)
						{
							displacement.X += (1f - t1) * (line_r1e.X - line_r1s.X);
							displacement.Y += (1f - t1) * (line_r1e.Y - line_r1s.Y);
						}
					}

					var pos = r1.Parent.Position;

					pos.X += displacement.X * (shape == 0 ? -1 : +1);
					pos.Y += displacement.Y * (shape == 0 ? -1 : +1);

					r1.Parent.Position = pos;
				}
			}

			return false;
		}
		*/
		#endregion
	}
}