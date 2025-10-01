using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Skills
{
	public class ElementalAmplification : PassiveSkill
	{
		public ElementalAmplification()
			: base( (int)SkillName.ElementalAmplification )
		{
			Name = "Elemental Amplification";
		}
	}
}
