using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseScroll : Item
	{
		public abstract Layer EquipmentLayer { get; }
		public abstract int SuccessPerc { get; }
		public abstract Attributes BonusAttributes { get; } // TODO: make a field?

		public BaseScroll()
		{
		}

		public override void OnDropTo( Mobile from, Item target )
		{
			BaseEquipment equip = target as BaseEquipment;

			if ( equip == null )
			{
				from.SendMessage( "You cannot use a scroll on that!" );
				return;
			}
			else if ( equip.Layer != EquipmentLayer )
			{
				from.SendMessage( "This scroll doesn't work on that equipment layer." );
				return;
			}
			else if ( equip.ScrollSlots <= 0 )
			{
				from.SendMessage( "The item has no available slots." );
				return;
			}

			// TODO: the item must be equipped

			if ( Dice.Random( 100 ) < SuccessPerc )
			{
				from.SendMessage( "The power of the scroll is transferred to the item." ); // TODO

				OnSuccess( from, equip );

				equip.ScrollsPassed++;
			}
			else
			{
				from.SendMessage( "The scroll evaporates and nothing happened." ); // TODO

				OnFail( from, equip );
			}

			equip.ScrollSlots--;

			Delete();
		}

		public virtual void OnSuccess( Mobile m, BaseEquipment equip )
		{
			equip.Attributes.Add( BonusAttributes );
		}

		public virtual void OnFail( Mobile m, BaseEquipment equip )
		{
		}
	}
}
