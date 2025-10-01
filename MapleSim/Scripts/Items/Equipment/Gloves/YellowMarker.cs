using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class YellowMarker : BaseGloves
	{
		public override string DefaultName { get { return "Yellow Marker"; } }
		public override int DefaultMaxSlots { get { return 7; } }

		public YellowMarker()
		{
			// TODO: stats?
		}
	}
}
