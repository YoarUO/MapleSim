using System;

namespace MapleSim.Core
{
	public class Item : BaseEntity
	{
		private BaseEntity m_Parent;
		private int m_ItemID;
		private Layer m_Layer;

		public BaseEntity Parent { get { return m_Parent; } set { m_Parent = value; } }
		public int ItemID { get { return m_ItemID; } set { m_ItemID = value; } }
		public Layer Layer { get { return m_Layer; } set { m_Layer = value; } }

		public Item()
		{
		}

		public virtual void OnDropTo( Mobile from, Item target )
		{
		}

		public override void OnDelete()
		{
			if ( m_Parent != null )
				m_Parent.RemoveItem( this );
		}
	}
}
