using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class ScrollForGlovesForAttack10 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForGlovesForAttack10()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Attack] = 3;
		}

		public override string DefaultName { get { return "Scroll for Gloves for Attack 10%"; } }
		public override Layer EquipmentLayer { get { return Layer.Gloves; } }
		public override int SuccessPerc { get { return 10; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForGlovesForAttack10()
		{
		}
	}

	public class ScrollForGlovesForAttack60 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForGlovesForAttack60()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Attack] = 2;
		}

		public override string DefaultName { get { return "Scroll for Gloves for Attack 60%"; } }
		public override Layer EquipmentLayer { get { return Layer.Gloves; } }
		public override int SuccessPerc { get { return 60; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForGlovesForAttack60()
		{
		}
	}

	public class DarkScrollForGlovesForAttack30 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForGlovesForAttack30()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Attack] = 3;
		}

		public override string DefaultName { get { return "Dark Scroll for Gloves for Attack 30%"; } }
		public override Layer EquipmentLayer { get { return Layer.Gloves; } }
		public override int SuccessPerc { get { return 30; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForGlovesForAttack30()
		{
		}
	}

	public class DarkScrollForGlovesForAttack70 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForGlovesForAttack70()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Attack] = 2;
		}

		public override string DefaultName { get { return "Dark Scroll for Gloves for Attack 70%"; } }
		public override Layer EquipmentLayer { get { return Layer.Gloves; } }
		public override int SuccessPerc { get { return 70; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForGlovesForAttack70()
		{
		}
	}
}
