using System;
using MapleSim.Core;

namespace MapleSim.Scripts.Mobiles
{
	public abstract class BaseMonster : Mobile
	{
		private Attributes m_Attributes;
		private int m_Knockback;
		private int m_GiveExperience;

		public Attributes Attributes { get { return m_Attributes; } }
		public int Knockback { get { return m_Knockback; } set { m_Knockback = value; } }
		public int GiveExperience { get { return m_GiveExperience; } set { m_GiveExperience = value; } }

		public BaseMonster()
			: base()
		{
			m_Attributes = new Attributes();
		}

		public override int GetAttribute( AttributeName attr )
		{
			int value = base.GetAttribute( attr );

			value += m_Attributes[attr];

			return value;
		}
	}
}
