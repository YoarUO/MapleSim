using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseWand : BaseWeapon
	{
		public BaseWand()
			: base()
		{
			Layer = Layer.Weapon;
		}
	}
}
