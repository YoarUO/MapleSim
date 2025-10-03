using System;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Items;
using MapleSim.Scripts.Mobiles;
using MapleSim.Scripts.Skills;
using MapleSim.Sim;
using MapleSim.Sim.Economy;
using MapleSim.Sim.Scrolling;

namespace MapleSim.Sim
{
	class Program
	{
		private static MessageLogger m_Logger;

		static void Main( string[] args )
		{
			SkillHelper.Initialize(); // TODO: Call dynamically

			Player pl = new Player();

			pl.MessageHandler = m_Logger = new MessageLogger();

			Script_Blazyy( pl );

			Script_Damage( pl );

			//Script_Scroll( pl );

			Console.Read();
		}

		private static void Script_Blazyy( Mobile m )
		{
			m.Name = "Blazyy";
			m.Level = 175;
			m.HitsMaxSeed = 2506;
			m.ManaMaxSeed = 22271;
			m.StrRaw = 4;
			m.DexRaw = 4;
			m.IntRaw = 893;
			m.LukRaw = 4;
			m.Skills[(int)SkillName.ElementalAmplification] = 30;
			m.Skills[(int)SkillName.MeteorShower] = 30;
			m.Skills[(int)SkillName.FireDemon] = 30;

			// TODO: Instantiate and equip individual equips
			BaseEquipment ringForEquips = new DummyEquipment();
			ringForEquips.Name = "Blazyy's Equips";
			ringForEquips.Layer = Layer.Ring;
			ringForEquips.Attributes[AttributeName.MaxHP] = 1882;
			ringForEquips.Attributes[AttributeName.MaxMP] = 7729;
			ringForEquips.Attributes[AttributeName.Str] = 70;
			ringForEquips.Attributes[AttributeName.Dex] = 69;
			ringForEquips.Attributes[AttributeName.Int] = 138;
			ringForEquips.Attributes[AttributeName.Luk] = 72;
			ringForEquips.Attributes[AttributeName.Magic] = 190;
			m.Equip( ringForEquips );

			// TODO: Program buff skills
			BaseEquipment ringForBuffs = new DummyEquipment();
			ringForBuffs.Name = "Blazyy's Buffs";
			ringForBuffs.Layer = Layer.Ring;
			ringForBuffs.Attributes[AttributeName.Int] = m.IntRaw / 10; // Maple Warrior
			ringForBuffs.Attributes[AttributeName.Magic] = 20; // Meditation
			m.Equip( ringForBuffs );

			BaseEquipment wand = new ElementalWand5();
			m.Equip( wand );
		}

		private static void Script_Damage( Mobile m )
		{
			Console.WriteLine( "Int: {0}", m.GetAttribute( AttributeName.Int ) );
			Console.WriteLine( "TMA: {0}", m.GetAttribute( AttributeName.Magic ) );
			Console.WriteLine( "Elemental Amplification: {0}", m.Skills[(int)SkillName.ElementalAmplification] );
			Console.WriteLine( "Meteor Shower: {0}", m.Skills[(int)SkillName.MeteorShower] );
			Console.WriteLine( "Fire Demon: {0}", m.Skills[(int)SkillName.FireDemon] );

			BaseMonster monster;

			monster = new MiniGoldMartialArtist();
			AttackSpell.Target = monster;
			m.UseSkill( (int)SkillName.MeteorShower );
			m_Logger.Flush();

			monster = new Vikerola();
			AttackSpell.Target = monster;
			m.UseSkill( (int)SkillName.FireDemon );
			m_Logger.Flush();
		}

		private static void Script_Scroll( Mobile m )
		{
			Console.WriteLine( "Overall Int" );
			ScrollSim( m,
				typeof( Bathrobe ),
				new ScrollStrategy(
					new PassObjective( typeof( ScrollForOverallForInt10 ), 1, 1 ),
					new PassObjective( typeof( ScrollForOverallForInt10 ), 1, 2 ),
					new FillObjective( typeof( ScrollForOverallForInt60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Int ) );
			m_Logger.Flush();

			Console.WriteLine( "Work Gloves Attack" );
			ScrollSim( m,
				typeof( WorkGloves ),
				new ScrollStrategy(
					new PassObjective( typeof( ScrollForGlovesForAttack10 ), 1, 1 ),
					new PassObjective( typeof( ScrollForGlovesForAttack10 ), 1, 2 ),
					new FillObjective( typeof( ScrollForGlovesForAttack60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Attack ) );
			m_Logger.Flush();

			Console.WriteLine( "Yellow Marker Attack" );
			ScrollSim( m,
				typeof( YellowMarker ),
				new ScrollStrategy(
					new FillObjective( typeof( ScrollForGlovesForAttack60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Attack ) );
			m_Logger.Flush();
		}

		private static void ScrollSim( Mobile m, Type equipType, ScrollStrategy strategy, OwlItemEvaluator evaluator )
		{
			const int trials = 10000;

			long totalCost = 0;
			long totalValue = 0;

			for ( int i = 0; i < trials; i++ )
			{
				// TODO: Ensure the equipment has max stats
				BaseEquipment equip = Construct<BaseEquipment>( equipType );

				totalCost += EconomyData.GetValue( equip );

				strategy.Perform( m, equip );

				foreach ( BaseScroll scroll in strategy.Scrolls )
					totalCost += EconomyData.GetValue( scroll );

				strategy.Reset();

				if ( !equip.Deleted )
					totalValue += EconomyData.GetValue( equip, evaluator );
			}

			Console.WriteLine( "Expected cost: {0}", FormatValue( (double)totalCost / trials * 1000.0 ) );
			Console.WriteLine( "Expected value: {0}", FormatValue( (double)totalValue / trials * 1000.0 ) );
		}

		private static T Construct<T>( Type type )
		{
			if ( !typeof( T ).IsAssignableFrom( type ) )
				return default( T );

			T t;

			try
			{
				t = (T)Activator.CreateInstance( type );
			}
			catch
			{
				t = default( T );
			}

			return t;
		}

		private static string FormatValue( double value )
		{
			return value.ToString( "#,#0.###############" );
		}
	}
}
