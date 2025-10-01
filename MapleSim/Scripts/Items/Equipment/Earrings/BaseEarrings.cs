using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseEarrings : BaseEquipment
	{
		public override int DefaultMaxSlots { get { return 5; } }

		public BaseEarrings()
		{
			Layer = Layer.Ears;
		}
	}
}
