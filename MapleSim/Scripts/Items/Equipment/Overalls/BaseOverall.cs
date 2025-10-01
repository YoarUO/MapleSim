using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseOverall : BaseEquipment
	{
		public override int DefaultMaxSlots { get { return 10; } }

		public BaseOverall()
		{
			Layer = Layer.Overall;
		}
	}
}
