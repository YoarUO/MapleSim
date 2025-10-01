using System;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Items;

namespace MapleSim.Scripts.Skills
{
	public abstract class AttackSpell : Skill
	{
		public static Mobile Target; // Temporary

		private int m_SpellAttackBase;
		private int m_SpellAttackMult;
		private int m_MasteryBase;
		private int m_MasteryMult;
		private ElementalName m_Elemental;

		public int SpellAttackBase { get { return m_SpellAttackBase; } set { m_SpellAttackBase = value; } }
		public int SpellAttackMult { get { return m_SpellAttackMult; } set { m_SpellAttackMult = value; } }
		public int MasteryBase { get { return m_MasteryBase; } set { m_MasteryBase = value; } }
		public int MasteryMult { get { return m_MasteryMult; } set { m_MasteryMult = value; } }
		public ElementalName Elemental { get { return m_Elemental; } set { m_Elemental = value; } }

		public AttackSpell( int skillID )
			: base( skillID )
		{
		}

		public override void Use( Mobile caster )
		{
			Mobile target = Target;

			if ( target == null )
			{
				caster.SendMessage( "You use {0} but there's nothing to hit.", Name );
				return;
			}

			int minDamage, maxDamage;

			CalculateDamage( caster, target, out minDamage, out maxDamage );

			caster.SendMessage( "You use {0} and hit {1} for [{2}-{3}] damage.", Name, target, minDamage, maxDamage );
		}

		public virtual void CalculateDamage( Mobile caster, Mobile target, out int minDamage, out int maxDamage )
		{
			int intel = caster.GetAttribute( AttributeName.Int );
			int magic = caster.GetAttribute( AttributeName.Magic );
			int level = caster.Skills[(int)SkillName.MeteorShower];
			int spellAttack = GetSpellAttack( level );
			int mastery = GetMastery( level );

			minDamage = (int)( ( ( magic * magic / 1000.0 + magic * ( mastery / 100.0 ) * 0.9 ) / 30.0 + intel / 200.0 ) * spellAttack );
			maxDamage = (int)( ( ( magic * magic / 1000.0 + magic ) / 30.0 + intel / 200.0 ) * spellAttack );

			int amplification = caster.Skills[(int)SkillName.ElementalAmplification];

			if ( amplification > 0 )
			{
				int amplificationMult = 110 + amplification;

				minDamage = amplificationMult * minDamage / 100;
				maxDamage = amplificationMult * maxDamage / 100;
			}

			BaseElementalWand elementalWand = caster.Weapon as BaseElementalWand;

			if ( elementalWand != null )
			{
				int elementalMult;

				if ( m_Elemental == elementalWand.Primary )
					elementalMult = 125;
				else if ( m_Elemental == elementalWand.Secondary )
					elementalMult = 110;
				else
					elementalMult = 100;

				minDamage = elementalMult * minDamage / 100;
				maxDamage = elementalMult * maxDamage / 100;
			}

			int magicDefense = target.GetAttribute( AttributeName.MagicDef );
			int levelDiff = Math.Max( 0, target.Level - caster.Level );

			minDamage = (int)( minDamage - magicDefense * 0.6 * ( 1.0 + 0.01 * levelDiff ) );
			maxDamage = (int)( maxDamage - magicDefense * 0.5 * ( 1.0 + 0.01 * levelDiff ) );
		}

		public virtual int GetSpellAttack( int level )
		{
			return m_SpellAttackBase + level * m_SpellAttackMult;
		}

		public virtual int GetMastery( int level )
		{
			return m_MasteryBase + level * m_MasteryMult;
		}
	}
}
