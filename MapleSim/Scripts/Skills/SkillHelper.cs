using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Skills
{
	public enum SkillName
	{
		ElementalAmplification,
		MeteorShower,
		FireDemon
	}

	public static class SkillHelper
	{
		public static void Initialize()
		{
			Register( new ElementalAmplification() );
			Register( new MeteorShower() );
			Register( new FireDemon() );
		}

		private static void Register( Skill skill )
		{
			Skill.Register( skill );
		}
	}
}
