using System;
using MapleSim.Core;

namespace MapleSim.Scripts.Mobiles
{
	public interface IMessageHandler
	{
		void HandleMessage( string text );
	}

	public class Player : Mobile
	{
		private IMessageHandler m_MessageHandler;

		public IMessageHandler MessageHandler { get { return m_MessageHandler; } set { m_MessageHandler = value; } }

		public Player()
			: base()
		{
		}

		public override void SendMessage( string text )
		{
			if ( m_MessageHandler != null )
			{
				m_MessageHandler.HandleMessage( text );
				return;
			}

			base.SendMessage( text );
		}
	}
}
