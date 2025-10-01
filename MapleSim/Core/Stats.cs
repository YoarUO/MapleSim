using System;

namespace MapleSim.Core
{
	public enum StatName
	{
		Str,
		Dex,
		Int,
		Luk
	}

	public class Stats
	{
		private int m_Str;
		private int m_Dex;
		private int m_Int;
		private int m_Luk;

		public int Str { get { return m_Str; } set { m_Str = value; } }
		public int Dex { get { return m_Dex; } set { m_Dex = value; } }
		public int Int { get { return m_Int; } set { m_Int = value; } }
		public int Luk { get { return m_Luk; } set { m_Luk = value; } }

		public int this[StatName stat]
		{
			get { return GetStat( stat ); }
			set { SetStat( stat, value ); }
		}

		public Stats()
		{
		}

		public Stats( int str, int dex, int intel, int luk, int maxHP, int maxMP )
			: this()
		{
			m_Str = str;
			m_Dex = dex;
			m_Int = intel;
			m_Luk = luk;
		}

		private int GetStat( StatName stat )
		{
			switch ( stat )
			{
				case StatName.Str: return m_Str;
				case StatName.Dex: return m_Dex;
				case StatName.Int: return m_Int;
				case StatName.Luk: return m_Luk;
			}

			return 0;
		}

		private void SetStat( StatName stat, int value )
		{
			switch ( stat )
			{
				case StatName.Str: m_Str = value; break;
				case StatName.Dex: m_Dex = value; break;
				case StatName.Int: m_Int = value; break;
				case StatName.Luk: m_Luk = value; break;
			}
		}
	}
}
