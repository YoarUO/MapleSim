using System;
using System.Collections.Generic;
using MapleSim.Core;
using MapleSim.Scripts;
using MapleSim.Scripts.Items;

namespace MapleSim.Sim.Scrolling
{
	public class ScrollStrategy
	{
		private ScrollObjective[] m_Objectives;
		private List<BaseScroll> m_Scrolls;

		public ScrollObjective[] Objectives { get { return m_Objectives; } }
		public List<BaseScroll> Scrolls { get { return m_Scrolls; } }

		public ScrollStrategy( params ScrollObjective[] objectives )
		{
			m_Objectives = objectives;
			m_Scrolls = new List<BaseScroll>();
		}

		public void Reset()
		{
			m_Scrolls.Clear();
		}

		public bool Perform( Mobile from, BaseEquipment equip )
		{
			for ( int i = 0; i < m_Objectives.Length; i++ )
			{
				if ( !m_Objectives[i].Perform( this, from, equip ) )
					return false;
			}

			return true;
		}

		public bool UseScroll( Mobile from, BaseEquipment equip, Type scrollType )
		{
			BaseScroll scroll = GetScroll( scrollType );

			if ( scroll == null )
				return false;

			int curPassed = equip.ScrollsPassed;

			scroll.OnDropTo( from, equip );

			return ( equip.ScrollsPassed != curPassed );
		}

		public BaseScroll GetScroll( Type scrollType )
		{
			BaseScroll scroll = ConstructScroll( scrollType );

			if ( scroll != null )
				m_Scrolls.Add( scroll );

			return scroll;
		}

		protected static BaseScroll ConstructScroll( Type scrollType )
		{
			BaseScroll scroll = null;

			try
			{
				scroll = (BaseScroll)Activator.CreateInstance( scrollType );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( "Error while constructing scroll of type '{0}': {1}", scrollType.Name, ex );
			}

			return scroll;
		}
	}

	public abstract class ScrollObjective
	{
		public ScrollObjective()
		{
		}

		public abstract bool Perform( ScrollStrategy strat, Mobile from, BaseEquipment equip );
	}

	public class FillObjective : ScrollObjective
	{
		private Type m_ScrollType;

		public Type ScrollType { get { return m_ScrollType; } }

		public FillObjective( Type scrollType )
		{
			m_ScrollType = scrollType;
		}

		public override bool Perform( ScrollStrategy strat, Mobile from, BaseEquipment equip )
		{
			int count = equip.ScrollSlots;

			for ( int i = 0; i < count && !equip.Deleted; i++ )
				strat.UseScroll( from, equip, m_ScrollType );

			return ( !equip.Deleted );
		}
	}

	// TODO: Option to stop when we've passed 'm_ReqPass' scrolls?
	public class PassObjective : ScrollObjective
	{
		private Type m_ScrollType;
		private int m_ReqPass;
		private int m_MaxSlots;

		public Type ScrollType { get { return m_ScrollType; } }
		public int ReqPass { get { return m_ReqPass; } }
		public int MaxSlots { get { return m_MaxSlots; } }

		public PassObjective( Type scrollType, int reqPass, int maxSlots )
		{
			m_ScrollType = scrollType;
			m_ReqPass = reqPass;
			m_MaxSlots = maxSlots;
		}

		public override bool Perform( ScrollStrategy strat, Mobile from, BaseEquipment equip )
		{
			int passed = 0;
			int slots = 0;

			while ( passed < m_ReqPass && slots < m_MaxSlots && !equip.Deleted && equip.ScrollSlots >= m_ReqPass - passed )
			{
				if ( strat.UseScroll( from, equip, m_ScrollType ) )
					passed++;

				slots++;
			}

			return ( passed >= m_ReqPass && !equip.Deleted );
		}
	}
}
