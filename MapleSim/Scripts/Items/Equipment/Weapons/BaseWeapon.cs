using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseWeapon : BaseEquipment
	{
		public override int DefaultMaxSlots { get { return 7; } }

		public BaseWeapon()
			: base()
		{
		}
	}
}
