using System;

namespace MapleSim.Core
{
	public delegate int ExperienceFormula( int level, int prevExperience );

	public class LevelInfo
	{
		public const int MaxLevel = 200;

		private static LevelInfo m_Definition;

		public static LevelInfo Definition { get { return m_Definition; } set { m_Definition = value; } }

		public static int GetMaxExperience( int level )
		{
			if ( m_Definition == null )
				return 0;

			return m_Definition.GetExperience( level );
		}

		private ExperienceFormula m_Formula;
		private int[] m_Levels;

		public LevelInfo( ExperienceFormula formula )
		{
			m_Formula = formula;
			m_Levels = new int[MaxLevel];
		}

		public int GetExperience( int level )
		{
			return GetExperience( level, false );
		}

		private int GetExperience( int level, bool recurse )
		{
			if ( level <= 0 || level >= MaxLevel )
				return 0;

			int experience = m_Levels[level];

			if ( experience == 0 )
			{
				experience = m_Formula( level, GetExperience( level - 1, true ) );

				if ( experience == 0 )
					Console.WriteLine( "Warning: Bad experience value for level {0}", level );
				else
					m_Levels[level] = experience;
			}

			return experience;
		}
	}
}
