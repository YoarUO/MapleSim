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

			Script_Meteor( pl );

			//Script_Scroll( pl );

			Console.Read();
		}

		private static void Script_Blazyy( Player pl )
		{
			pl.Name = "Blazyy";
			pl.Level = 175;
			pl.HitsMaxSeed = 2506;
			pl.ManaMaxSeed = 22271;
			pl.StrRaw = 4;
			pl.DexRaw = 4;
			pl.IntRaw = 893;
			pl.LukRaw = 4;
			pl.Skills[(int)SkillName.ElementalAmplification] = 30;
			pl.Skills[(int)SkillName.MeteorShower] = 30;

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
			pl.Equip( ringForEquips );

			// TODO: Program buff skills
			BaseEquipment ringForBuffs = new DummyEquipment();
			ringForBuffs.Name = "Blazyy's Buffs";
			ringForBuffs.Layer = Layer.Ring;
			ringForBuffs.Attributes[AttributeName.Int] = pl.IntRaw / 10; // Maple Warrior
			ringForBuffs.Attributes[AttributeName.Magic] = 20; // Meditation
			pl.Equip( ringForBuffs );

			BaseEquipment wand = new ElementalWand5();
			pl.Equip( wand );
		}

		private static void Script_Meteor( Player pl )
		{
			Console.WriteLine( "Int: {0}", pl.GetAttribute( AttributeName.Int ) );
			Console.WriteLine( "TMA: {0}", pl.GetAttribute( AttributeName.Magic ) );
			Console.WriteLine( "Elemental Amplification: {0}", pl.Skills[(int)SkillName.ElementalAmplification] );
			Console.WriteLine( "Meteor Shower: {0}", pl.Skills[(int)SkillName.MeteorShower] );

			BaseMonster monster = new MiniGoldMartialArtist();
			AttackSpell.Target = monster;
			pl.UseSkill( (int)SkillName.MeteorShower );
			m_Logger.Flush();
		}

		private static void Script_Scroll( Player pl )
		{
			Console.WriteLine( "Overall Int" );
			ScrollSim( pl,
				typeof( Bathrobe ),
				new ScrollStrategy(
					new PassObjective( typeof( ScrollForOverallForInt10 ), 1, 1 ),
					new PassObjective( typeof( ScrollForOverallForInt10 ), 1, 2 ),
					new FillObjective( typeof( ScrollForOverallForInt60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Int ) );
			m_Logger.Flush();

			Console.WriteLine( "Work Gloves Attack" );
			ScrollSim( pl,
				typeof( WorkGloves ),
				new ScrollStrategy(
					new PassObjective( typeof( ScrollForGlovesForAttack10 ), 1, 1 ),
					new PassObjective( typeof( ScrollForGlovesForAttack10 ), 1, 2 ),
					new FillObjective( typeof( ScrollForGlovesForAttack60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Attack ) );
			m_Logger.Flush();

			Console.WriteLine( "Yellow Marker Attack" );
			ScrollSim( pl,
				typeof( YellowMarker ),
				new ScrollStrategy(
					new FillObjective( typeof( ScrollForGlovesForAttack60 ) ) ),
				new SimpleAttributeEvaluator( AttributeName.Attack ) );
			m_Logger.Flush();
		}

		private static void ScrollSim( Player pl, Type equipType, ScrollStrategy strategy, OwlItemEvaluator evaluator )
		{
			const int trials = 10000;

			long totalCost = 0;
			long totalValue = 0;

			for ( int i = 0; i < trials; i++ )
			{
				// TODO: Ensure the equipment has max stats
				BaseEquipment equip = Construct<BaseEquipment>( equipType );

				totalCost += EconomyData.GetValue( equip );

				strategy.Perform( pl, equip );

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
