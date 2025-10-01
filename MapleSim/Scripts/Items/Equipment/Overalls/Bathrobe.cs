using System;
using MapleSim.Core;
using MapleSim.Scripts;

namespace MapleSim.Scripts.Items
{
	public class Bathrobe : BaseOverall // TODO: for male/female
	{
		public override string DefaultName { get { return "Bathrobe"; } }

		public Bathrobe()
		{
			Attributes[AttributeName.WeaponDef] = 20;
			Attributes[AttributeName.Speed] = 10;
		}
	}
}
