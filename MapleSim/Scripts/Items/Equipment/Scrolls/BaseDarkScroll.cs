using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseDarkScroll : BaseScroll
	{
		public virtual int DestroyChance { get { return 50; } }

		public BaseDarkScroll()
		{
		}

		public override void OnFail( Mobile m, BaseEquipment equip )
		{
			if ( Dice.Random( 100 ) < DestroyChance )
			{
				m.SendMessage( "The item is destroyed due to the overwhelming power of the scroll." ); // TODO

				equip.Delete();
			}
		}
	}
}
