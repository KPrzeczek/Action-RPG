using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.States
{
	public interface IStateManaged
	{
		void RequestState(IState requestedState);
	}
}
