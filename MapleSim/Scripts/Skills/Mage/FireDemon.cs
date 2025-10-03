using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Skills
{
	public class FireDemon : AttackSpell
	{
		public override bool UseMLDamageFormula { get { return true; } }

		public FireDemon()
			: base( (int)SkillName.FireDemon )
		{
			Name = "Fire Demon";
			SpellAttackBase = 82;
			SpellAttackStep = 2;
			MasteryBase = 15;
			MasteryStep = 5;
			LevelsPerMasteryStep = 3;
			Elemental = ElementalName.Fire;
		}
	}
}
