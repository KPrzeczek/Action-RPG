using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Items.GUI
{
	public class InventoryUISettings
	{
		public InventorySlot InventorySlotPrefab { get; set; }
		
		public Texture2D ItemHoverInfoNineSlice { get; set; }
		public SpriteFont ItemHoverInfoFont { get; set; }
	}
}
