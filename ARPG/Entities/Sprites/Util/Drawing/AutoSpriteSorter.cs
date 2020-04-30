using ARPG.Entities.Sprites.Static;
using System;
using System.Collections.Generic;

namespace ARPG.Entities.Sprites.Util.Drawing
{
	public class AutoSpriteSorter
	{
		private Sprite parent;

		private bool once = true;

		public int YOffset;
		public bool Continuous;

		public AutoSpriteSorter(Sprite parent)
		{
			this.parent = parent;
			SortBasedOnY();
		}

		public void Update(float deltaTime)
		{
			if(once)
			{
				once = false;
				SortBasedOnY();
			}

			if(Continuous)
				SortBasedOnY();
		}

		private void SortBasedOnY()
		{
			var bottom = parent.Rectangle.Bottom + YOffset;
			Console.WriteLine(parent.GetType() + " :: " + bottom);
			parent.Layer = (int)(bottom);
		}
	}
}
