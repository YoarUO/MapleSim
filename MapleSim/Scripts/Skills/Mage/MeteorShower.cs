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
			SpellAttackBase = 320;
			SpellAttackMult = 10;
			MasteryBase = 10; // TODO
			Elemental = ElementalName.Fire;
		}

		public override int GetSpellAttack( int level )
		{
			// Meteor Shower uses a different multiplier from level 20 onwards
			if ( level <= 20 )
				return base.GetSpellAttack( level );
			else
				return SpellAttackBase + 20 * SpellAttackMult + ( level - 20 ) * 5;
		}
	}
}
