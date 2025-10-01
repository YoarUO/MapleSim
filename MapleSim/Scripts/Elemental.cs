using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts
{
	public enum ElementalName : byte
	{
		None		= 0x00,
		Fire		= 0x01,
		Ice			= 0x02,
		Poison		= 0x04,
		Lightning	= 0x08,
		Holy		= 0x10,
		Dark		= 0x20
	}
}
