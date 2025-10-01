using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class ScrollForEarringsForInt10 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForEarringsForInt10()
		{
			// TODO: confirm
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 2;
			m_BonusAttributes[AttributeName.Magic] = 3;
		}

		public override string DefaultName { get { return "Scroll for Earrings for Int 10%"; } }
		public override Layer EquipmentLayer { get { return Layer.Ears; } }
		public override int SuccessPerc { get { return 10; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForEarringsForInt10()
		{
		}
	}

	public class ScrollForEarringsForInt60 : BaseScroll
	{
		private static Attributes m_BonusAttributes;

		static ScrollForEarringsForInt60()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 1;
			m_BonusAttributes[AttributeName.Magic] = 2;
		}

		public override string DefaultName { get { return "Scroll for Earrings for Int 60%"; } }
		public override Layer EquipmentLayer { get { return Layer.Ears; } }
		public override int SuccessPerc { get { return 60; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public ScrollForEarringsForInt60()
		{
		}
	}

	public class DarkScrollForEarringsForInt30 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForEarringsForInt30()
		{
			// TODO: confirm
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 2;
			m_BonusAttributes[AttributeName.Magic] = 3;
		}

		public override string DefaultName { get { return "Dark Scroll for Earrings for Int 30%"; } }
		public override Layer EquipmentLayer { get { return Layer.Ears; } }
		public override int SuccessPerc { get { return 30; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForEarringsForInt30()
		{
		}
	}

	public class DarkScrollForEarringsForInt70 : BaseDarkScroll
	{
		private static Attributes m_BonusAttributes;

		static DarkScrollForEarringsForInt70()
		{
			m_BonusAttributes = new Attributes();
			m_BonusAttributes[AttributeName.Int] = 1;
			m_BonusAttributes[AttributeName.Magic] = 2;
		}

		public override string DefaultName { get { return "Dark Scroll for Earrings for Int 70%"; } }
		public override Layer EquipmentLayer { get { return Layer.Ears; } }
		public override int SuccessPerc { get { return 70; } }
		public override Attributes BonusAttributes { get { return m_BonusAttributes; } }

		public DarkScrollForEarringsForInt70()
		{
		}
	}
}
