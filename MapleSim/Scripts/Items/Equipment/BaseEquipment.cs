using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseEquipment : Item, IAttributes
	{
		public virtual int DefaultMaxSlots { get { return 0; } }

		private Attributes m_Attributes;
		private int m_ScrollSlots;
		private int m_MaxScrollSlots;
		private int m_ScrollsPassed;

		public Attributes Attributes { get { return m_Attributes; } }
		public int ScrollSlots { get { return m_ScrollSlots; } set { m_ScrollSlots = value; } }
		public int MaxScrollSlots { get { return m_MaxScrollSlots; } set { m_MaxScrollSlots = value; } }
		public int ScrollsPassed { get { return m_ScrollsPassed; } set { m_ScrollsPassed = value; } }

		public BaseEquipment()
			: base()
		{
			m_Attributes = new Attributes();
			m_ScrollSlots = m_MaxScrollSlots = DefaultMaxSlots;
		}

		public override string ToString()
		{
			if ( m_ScrollsPassed > 0 )
				return String.Format( "{0} (+{1})", base.ToString(), m_ScrollsPassed );

			return base.ToString();
		}
	}
}
