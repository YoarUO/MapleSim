using System;
using System.Collections.Generic;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Mobiles;

namespace MapleSim.Sim
{
	public class MessageLogger : IMessageHandler
	{
		private Dictionary<string, int> m_Dict;

		public MessageLogger()
		{
			m_Dict = new Dictionary<string, int>();
		}

		public void HandleMessage( string text )
		{
			if ( m_Dict.ContainsKey( text ) )
				m_Dict[text]++;
			else
				m_Dict[text] = 1;
		}

		public bool Contains( string text )
		{
			return Contains( text, false );
		}

		public bool Contains( string text, bool consume )
		{
			if ( !m_Dict.ContainsKey( text ) )
				return false;

			if ( consume )
			{
				if ( m_Dict[text] == 1 )
					m_Dict.Remove( text );
				else
					m_Dict[text] = m_Dict[text] - 1;
			}

			return true;
		}

		public void Flush()
		{
			foreach ( KeyValuePair<string, int> kvp in m_Dict )
				Console.WriteLine( "{0} ({1:N0})", kvp.Key, kvp.Value );

			m_Dict.Clear();
		}
	}
}
