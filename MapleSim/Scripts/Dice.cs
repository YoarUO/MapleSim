using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts
{
	public static class Dice
	{
		private static Random m_Random;

		static Dice()
		{
			m_Random = new Random();
		}

		public static int Random( int count )
		{
			return m_Random.Next( count );
		}

		public static double RandomDouble()
		{
			return m_Random.NextDouble();
		}
	}
}
