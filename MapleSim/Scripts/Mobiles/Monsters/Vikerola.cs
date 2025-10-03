using System;
using MapleSim.Core;

namespace MapleSim.Scripts.Mobiles
{
	public class Vikerola : BaseMonster
	{
		public override string DefaultName { get { return "Vikerola"; } }

		public Vikerola()
			: base()
		{
			Level = 87;
			HitsMaxSeed = 36000;
			ManaMaxSeed = 150;
			Attributes[AttributeName.Attack] = 388;
			Attributes[AttributeName.WeaponDef] = 820;
			Attributes[AttributeName.Magic] = 430;
			Attributes[AttributeName.MagicDef] = 465;
			Attributes[AttributeName.Accuracy] = 160;
			Attributes[AttributeName.Avoidability] = 26;
			Attributes[AttributeName.Speed] = 10;
			Knockback = 3700;
			GiveExperience = 2000;
			ElementalWeakness = ElementalName.Fire | ElementalName.Lightning;
		}
	}
}
