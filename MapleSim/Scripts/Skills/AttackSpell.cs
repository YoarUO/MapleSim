using System;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Items;
using MapleSim.Scripts.Mobiles;

namespace MapleSim.Scripts.Skills
{
	public abstract class AttackSpell : Skill
	{
		public static BaseMonster Target; // Temporary

		public virtual bool UseMLDamageFormula { get { return false; } }

		private int m_SpellAttackBase;
		private int m_SpellAttackStep;
		private int m_MasteryBase;
		private int m_MasteryStep;
		private int m_LevelsPerMasteryStep;
		private ElementalName m_Elemental;

		public int SpellAttackBase { get { return m_SpellAttackBase; } set { m_SpellAttackBase = value; } }
		public int SpellAttackStep { get { return m_SpellAttackStep; } set { m_SpellAttackStep = value; } }
		public int MasteryBase { get { return m_MasteryBase; } set { m_MasteryBase = value; } }
		public int MasteryStep { get { return m_MasteryStep; } set { m_MasteryStep = value; } }
		public int LevelsPerMasteryStep { get { return m_LevelsPerMasteryStep; } set { m_LevelsPerMasteryStep = value; } }
		public ElementalName Elemental { get { return m_Elemental; } set { m_Elemental = value; } }

		public AttackSpell( int skillID )
			: base( skillID )
		{
			m_LevelsPerMasteryStep = 1;
		}

		public override void Use( Mobile caster )
		{
			BaseMonster target = Target;

			if ( target == null )
			{
				caster.SendMessage( "You use {0} but there's nothing to hit.", Name );
				return;
			}

			int minDamage, maxDamage;

			CalculateDamage( caster, target, out minDamage, out maxDamage );

			caster.SendMessage( "You use {0} and hit {1} for [{2}-{3}] damage.", Name, target, minDamage, maxDamage );
		}

		public virtual void CalculateDamage( Mobile caster, BaseMonster target, out int minDamage, out int maxDamage )
		{
			int level = caster.Skills[SkillID];

			if ( level <= 0 || ( m_Elemental & target.ElementalImmunity ) != 0 )
			{
				maxDamage = 0;
				minDamage = 0;
				return;
			}

			int magic = caster.GetAttribute( AttributeName.Magic );
			int mastery = GetMastery( level );

			if ( UseMLDamageFormula )
			{
				int baseInt = caster.IntRaw;
				int baseLuk = caster.LukRaw;

				// TODO: Verify mastery component
				maxDamage = (int)( baseInt * ( magic - baseInt - Math.Min( baseLuk, 100 ) * 0.5 ) / 6700.0 + baseInt / 38.0 + ( magic - baseInt - Math.Min( baseLuk, 100 ) * 0.5 ) / 14.0 + Math.Log10( Math.Max( 1, magic ) ) );
				minDamage = (int)( baseInt * ( magic - baseInt - Math.Min( baseLuk, 100 ) * 0.5 ) / 6700.0 + ( baseInt / 38.0 + ( magic - baseInt - Math.Min( baseLuk, 100 ) * 0.5 ) / 14.0 ) * ( mastery / 3.0 + 30 ) * 0.0107 + Math.Log10( Math.Max( 1, magic ) ) );
			}
			else
			{
				int intel = caster.GetAttribute( AttributeName.Int );

				maxDamage = ( magic * magic / 1000 + magic ) / 30 + intel / 200;
				minDamage = ( magic * magic / 1000 + magic * mastery / 100 ) / 30 + intel / 200;
			}

			int spellAttack = GetSpellAttack( level );

			maxDamage *= spellAttack;
			minDamage *= spellAttack;

			if ( ( m_Elemental & target.ElementalWeakness ) != 0 )
			{
				maxDamage = maxDamage * 3 / 2;
				minDamage = minDamage * 3 / 2;
			}
			else if ( ( m_Elemental & target.ElementalStrength ) != 0 )
			{
				maxDamage /= 2;
				minDamage /= 2;
			}

			int amplification = caster.Skills[(int)SkillName.ElementalAmplification];

			if ( amplification > 0 )
			{
				int amplificationMult = 110 + amplification;

				maxDamage = amplificationMult * maxDamage / 100;
				minDamage = amplificationMult * minDamage / 100;
			}

			BaseElementalWand elementalWand = caster.Weapon as BaseElementalWand;

			if ( elementalWand != null )
			{
				int elementalWandMult;

				if ( m_Elemental == elementalWand.Primary )
					elementalWandMult = 125;
				else if ( m_Elemental == elementalWand.Secondary )
					elementalWandMult = 110;
				else
					elementalWandMult = 100;

				maxDamage = elementalWandMult * maxDamage / 100;
				minDamage = elementalWandMult * minDamage / 100;
			}

			int magicDefense = target.GetAttribute( AttributeName.MagicDef );
			int levelDiff = Math.Max( 0, target.Level - caster.Level );

			maxDamage -= 50 * magicDefense * ( 100 + levelDiff ) / 10000;
			minDamage -= 60 * magicDefense * ( 100 + levelDiff ) / 10000;
		}

		public virtual int GetSpellAttack( int level )
		{
			return m_SpellAttackBase + ( level - 1 ) * m_SpellAttackStep;
		}

		public virtual int GetMastery( int level )
		{
			if ( m_LevelsPerMasteryStep <= 0 )
				return 0; // TODO: Warning?

			return m_MasteryBase + ( level - 1 ) / m_LevelsPerMasteryStep * m_MasteryStep;
		}
	}
}
