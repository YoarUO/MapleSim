using System;
using MapleSim.Core;

namespace MapleSim.Scripts.Mobiles
{
	public class MiniGoldMartialArtist : BaseMonster
	{
		public override string DefaultName { get { return "Mini Gold Martial Artist"; } }

		public MiniGoldMartialArtist()
			: base()
		{
			Level = 130;
			HitsMaxSeed = 270000;
			ManaMaxSeed = 550;
			Attributes[AttributeName.Attack] = 550;
			Attributes[AttributeName.WeaponDef] = 1300;
			Attributes[AttributeName.Magic] = 520;
			Attributes[AttributeName.MagicDef] = 1300;
			Attributes[AttributeName.Accuracy] = 200;
			Attributes[AttributeName.Avoidability] = 75;
			Attributes[AttributeName.Speed] = 15;
			Knockback = 1000;
			GiveExperience = 14890;
		}
	}
}
