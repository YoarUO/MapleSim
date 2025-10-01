using System;

namespace MapleSim.Core
{
	[Flags]
	public enum Layer : uint
	{
		Invalid	= 0x0,
		Hat		= 0x1,
		Face	= 0x2,
		Ring	= 0x4,
		Eyes	= 0x10,
		Ears	= 0x20,
		Cape	= 0x40,
		Clothes	= 0x80,
		Pendant	= 0x100,
		Weapon	= 0x200,
		Shield	= 0x400,
		Gloves	= 0x800,
		Pants	= 0x1000,
		Shoes	= 0x2000,

		Overall = Clothes | Pants,
		TwoHanded = Weapon | Shield
	}
}
