using System;
using System.Collections.Generic;

namespace MapleSim.Core
{
	public abstract class BaseEntity
	{
		public virtual string DefaultName { get { return null; } }

		private string m_Name;
		private List<Item> m_Items;
		private bool m_Deleted;

		public string Name
		{
			get
			{
				if ( m_Name != null )
					return m_Name;

				return DefaultName;
			}
			set { m_Name = value; }
		}

		public List<Item> Items { get { return m_Items; } }
		public bool Deleted { get { return m_Deleted; } }

		public BaseEntity()
		{
			m_Items = new List<Item>();
		}

		public void AddItem( Item item )
		{
			if ( item.Parent != null )
				item.Parent.RemoveItem( item );

			m_Items.Add( item );

			item.Parent = this;

			OnItemAdded( item );
		}

		public virtual void OnItemAdded( Item item )
		{
		}

		public void RemoveItem( Item item )
		{
			if ( !m_Items.Remove( item ) )
				return;

			item.Parent = null;

			OnItemRemoved( item );
		}

		public virtual void OnItemRemoved( Item item )
		{
		}

		public Item GetItem( Layer layer )
		{
			foreach ( Item item in Items )
			{
				if ( ( item.Layer & layer ) != 0 )
					return item;
			}

			return null;
		}

		public void Delete()
		{
			if ( m_Deleted )
				return;

			OnDelete();

			m_Deleted = true;
		}

		public virtual void OnDelete()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
