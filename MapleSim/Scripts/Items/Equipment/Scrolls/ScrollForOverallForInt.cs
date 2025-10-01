using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class ScrollForOverallForInt10 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForOverallForInt10()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 5;
		}

		public override string DefaultName { get { return "Scroll for Overall for Int 10%"; } }
		public override Layer EquipmentLayer { get { return Layer.Overall; } }
		public override int SuccessPerc { get { return 10; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForOverallForInt10()
		{
		}
	}

	public class ScrollForOverallForInt60 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForOverallForInt60()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 2;
		}

		public override string DefaultName { get { return "Scroll for Overall for Int 60%"; } }
		public override Layer EquipmentLayer { get { return Layer.Overall; } }
		public override int SuccessPerc { get { return 60; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForOverallForInt60()
		{
		}
	}

	public class DarkScrollForOverallForInt30 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForOverallForInt30()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 5;
		}

		public override string DefaultName { get { return "Dark Scroll for Overall for Int 30%"; } }
		public override Layer EquipmentLayer { get { return Layer.Overall; } }
		public override int SuccessPerc { get { return 30; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForOverallForInt30()
		{
		}
	}

	public class DarkScrollForOverallForInt70 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForOverallForInt70()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 2;
		}

		public override string DefaultName { get { return "Dark Scroll for Overall for Int 70%"; } }
		public override Layer EquipmentLayer { get { return Layer.Overall; } }
		public override int SuccessPerc { get { return 70; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForOverallForInt70()
		{
		}
	}
}
