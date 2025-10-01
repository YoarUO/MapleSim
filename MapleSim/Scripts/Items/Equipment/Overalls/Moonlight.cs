using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class Moonlight : BaseOverall // TODO: color variants, female only
	{
		public override string DefaultName { get { return "Moonlight"; } }

		public Moonlight()
		{
			// TODO: randomized stats
			Attributes[AttributeName.Int] = 5;
			Attributes[AttributeName.Luk] = 2;
		}
	}
}
