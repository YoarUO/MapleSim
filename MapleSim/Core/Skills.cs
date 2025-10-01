using System;
using System.Collections.Generic;

namespace MapleSim.Core
{
	public abstract class Skill
	{
		private static readonly Dictionary<int, Skill> m_Table;

		public static Dictionary<int, Skill> Table { get { return m_Table; } }

		static Skill()
		{
			m_Table = new Dictionary<int, Skill>();
		}

		public static void Register( Skill skill )
		{
			if ( m_Table.ContainsKey( skill.SkillID ) )
			{
				Console.WriteLine( "Warning: Duplicate skill ID '{0}'.", skill.SkillID );
				return;
			}

			m_Table[skill.SkillID] = skill;
		}

		public static Skill GetSkill( int skillID )
		{
			Skill skill;

			if ( m_Table.TryGetValue( skillID, out skill ) )
				return skill;

			return null;
		}

		public virtual bool CanUse { get { return true; } }

		private int m_SkillID;
		private string m_Name;

		public int SkillID { get { return m_SkillID; } }
		public string Name { get { return m_Name; } set { m_Name = value; } }

		public Skill( int skillID )
		{
			m_SkillID = skillID;
		}

		public abstract void Use( Mobile from );
	}

	public abstract class PassiveSkill : Skill
	{
		public override bool CanUse { get { return false; } }

		public PassiveSkill( int skillID )
			: base( skillID )
		{
		}

		public override void Use( Mobile from )
		{
		}
	}

	public class Skills
	{
		private Dictionary<int, int> m_Dict;

		public int this[int skillID]
		{
			get { return GetLevel( skillID ); }
			set { SetLevel( skillID, value ); }
		}

		public Skills()
		{
			m_Dict = new Dictionary<int, int>();
		}

		private int GetLevel( int skillID )
		{
			int points;

			if ( m_Dict.TryGetValue( skillID, out points ) )
				return points;

			return 0;
		}

		private void SetLevel( int skillID, int points )
		{
			m_Dict[skillID] = points;
		}
	}
}
