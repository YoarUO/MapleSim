using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Skills
{
	public class MeteorShower : AttackSpell
	{
		public MeteorShower()
			: base( (int)SkillName.MeteorShower )
		{
			Name = "Meteor Shower";
			SpellAttackBase = 330;
			SpellAttackStep = 10;
			MasteryBase = 15; // TODO
			MasteryStep = 5;
			LevelsPerMasteryStep = 3;
			Elemental = ElementalName.Fire;
		}

		public override int GetSpellAttack( int level )
		{
			// Meteor Shower uses a different multiplier from level 20 onwards
			if ( level <= 20 )
				return base.GetSpellAttack( level );
			else
				return SpellAttackBase + 19 * SpellAttackStep + ( level - 20 ) * 5;
		}
	}
}
