using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public abstract class BaseElementalWand : BaseWand
	{
		public abstract ElementalName Primary { get; }
		public abstract ElementalName Secondary { get; }

		public BaseElementalWand()
		{
		}
	}

	public class ElementalWand5 : BaseElementalWand
	{
		public override ElementalName Primary { get { return ElementalName.Fire; } }
		public override ElementalName Secondary { get { return ElementalName.Poison; } }

		public ElementalWand5()
		{
			// TODO: Attributes
		}
	}

	// TODO: Other elemental wands
}
