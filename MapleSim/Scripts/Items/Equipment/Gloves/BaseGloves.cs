using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseGloves : BaseEquipment
	{
		public override int DefaultMaxSlots { get { return 5; } }

		public BaseGloves()
		{
			Layer = Layer.Gloves;
		}
	}
}
