using System;
using MapleSim.Core;

namespace MapleSim.Scripts.Mobiles
{
	public class WolfSpider : BaseMonster
	{
		public override string DefaultName { get { return "Wolf Spider"; } }

		public WolfSpider()
			: base()
		{
			Level = 130;
			HitsMaxSeed = 28000;
			ManaMaxSeed = 350;
			Attributes[AttributeName.Attack] = 450;
			Attributes[AttributeName.WeaponDef] = 700;
			Attributes[AttributeName.Magic] = 490;
			Attributes[AttributeName.MagicDef] = 650;
			Attributes[AttributeName.Accuracy] = 175;
			Attributes[AttributeName.Avoidability] = 30;
			Attributes[AttributeName.Speed] = -5;
			Knockback = 1000;
			GiveExperience = 1200;
		}
	}
}
