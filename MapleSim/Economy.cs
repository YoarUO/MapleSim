using System;
using System.Collections.Generic;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Items;

namespace MapleSim.Sim.Economy
{
	// All values are given in units of 1,000 (1k)
	public static class EconomyData
	{
		private static List<OwlData> m_OwlData;
		private static Dictionary<Type, long> m_TypeData;

		static EconomyData()
		{
			m_OwlData = new List<OwlData>();
			m_TypeData = new Dictionary<Type, long>();

			// Attack gloves
			AddOwlData( typeof( BaseGloves ),
				// Owled at 2025/09/26
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Attack, 6 ), 0 ), 2400, 3000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Attack, 8 ), 0 ), 2500, 4500 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Attack, 10 ), 0 ), 18000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Attack, 12 ), 0 ), 92000, 95000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Attack, 14 ), 0 ), 435000 ) );

			// Int overalls
			AddOwlData( typeof( BaseOverall ),
				// Owled at 2025/09/26
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 13 ), 0 ), 42000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 14 ), 0 ), 40000, 40000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 15 ), 0 ), 59000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 17 ), 0 ), 74000, 75000, 85000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 19 ), 0 ), 104000 ),
				new OwlDataEntry( new OwlItem( new Attributes( AttributeName.Int, 20 ), 0 ), 130000, 135000 ) );

			// Equipment
			AddTypeData( typeof( YellowMarker ), 20000 );

			// Scrolls
			AddTypeData( typeof( ScrollForGlovesForAttack10 ), 170 );
			AddTypeData( typeof( ScrollForGlovesForAttack60 ), 490 );
			AddTypeData( typeof( DarkScrollForGlovesForAttack30 ), 24100 );
			AddTypeData( typeof( DarkScrollForGlovesForAttack70 ), 7500 );
			AddTypeData( typeof( ScrollForOverallForInt10 ), 2100 );
			AddTypeData( typeof( ScrollForOverallForInt60 ), 3700 );
			AddTypeData( typeof( DarkScrollForOverallForInt30 ), 8500 );
			AddTypeData( typeof( DarkScrollForOverallForInt70 ), 2000 );
		}

		public static void AddOwlData( Type itemType, params OwlDataEntry[] entries )
		{
			m_OwlData.Add( new OwlData( itemType, entries ) );
		}

		public static void AddTypeData( Type itemType, long value )
		{
			m_TypeData[itemType] = value;
		}

		public static long GetValue( Item item )
		{
			return GetValue( item, null );
		}

		public static long GetValue( Item item, OwlItemEvaluator evaluator )
		{
			Type itemType = item.GetType();

			if ( evaluator != null && item is BaseEquipment )
			{
				BaseEquipment equip = (BaseEquipment)item;

				int itemValue = evaluator.Evaluate( new OwlItem( equip ) );

				foreach ( OwlData data in m_OwlData )
				{
					if ( data.RefType.IsAssignableFrom( itemType ) )
					{
						long price;

						if ( data.GetPrice( itemValue, evaluator, out price ) )
							return price;
					}
				}
			}

			long value;

			if ( m_TypeData.TryGetValue( itemType, out value ) )
				return value;

			return 0;
		}
	}

	public class OwlData
	{
		private Type m_RefType;
		private OwlDataEntry[] m_Entries;

		public Type RefType { get { return m_RefType; } }
		public OwlDataEntry[] Entries { get { return m_Entries; } }

		public OwlData( Type refType, OwlDataEntry[] entries )
		{
			m_RefType = refType;
			m_Entries = entries;

			// TODO: sort in ascending order
			//Array.Sort( m_Entries );
		}

		public bool GetPrice( int itemValue, OwlItemEvaluator evaluator, out long price )
		{
			for ( int i = m_Entries.Length - 1; i >= 0; i-- )
			{
				// TODO: linear interpolation?
				if ( itemValue >= evaluator.Evaluate( m_Entries[i].Item ) )
				{
					price = Mean( m_Entries[i].Prices );
					return true;
				}
			}

			price = 0;
			return false;
		}

		private static long Mean( int[] array )
		{
			long total = 0;

			for ( int i = 0; i < array.Length; i++ )
				total += array[i];

			return total / array.Length;
		}
	}

	public class OwlDataEntry
	{
		private OwlItem m_Item;
		private int[] m_Prices;

		public OwlItem Item { get { return m_Item; } }
		public int[] Prices { get { return m_Prices; } }

		public OwlDataEntry( OwlItem item, params int[] prices )
		{
			m_Item = item;
			m_Prices = prices;
		}
	}

	public class OwlItem
	{
		private Attributes m_Attributes;
		private int m_ScrollSlots;

		public Attributes Attributes { get { return m_Attributes; } }
		public int ScrollSlots { get { return m_ScrollSlots; } }

		public OwlItem( Attributes attrs, int scrollSlots )
		{
			m_Attributes = attrs;
			m_ScrollSlots = scrollSlots;
		}

		public OwlItem( BaseEquipment equip )
		{
			m_Attributes = new Attributes( equip.Attributes );
			m_ScrollSlots = equip.ScrollSlots;
		}
	}

	public abstract class OwlItemEvaluator
	{
		public OwlItemEvaluator()
		{
		}

		public abstract int Evaluate( OwlItem item );
	}

	public class SimpleAttributeEvaluator : OwlItemEvaluator
	{
		private AttributeName[] m_Attributes;

		public SimpleAttributeEvaluator( params AttributeName[] attrs )
		{
			m_Attributes = attrs;
		}

		public override int Evaluate( OwlItem item )
		{
			int value = 0;

			for ( int i = 0; i < m_Attributes.Length; i++ )
				value += item.Attributes[m_Attributes[i]];

			return value;
		}
	}
}
