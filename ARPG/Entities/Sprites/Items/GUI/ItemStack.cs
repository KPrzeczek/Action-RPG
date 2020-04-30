using ARPG.Entities.Sprites.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Items.Gui
{
	public class ItemStack
	{
		public Item Item { get; set; }
		public int ItemCount { get; set; }
	}
}
