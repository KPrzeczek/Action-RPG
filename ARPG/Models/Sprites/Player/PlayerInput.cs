using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Models.Sprites.Player
{
	public class PlayerInput
	{
		public Keys MoveUp { get; set; }
		public Keys MoveDown { get; set; }
		public Keys MoveLeft { get; set; }
		public Keys MoveRight { get; set; }

		public Keys Run { get; set; }
	}
}
