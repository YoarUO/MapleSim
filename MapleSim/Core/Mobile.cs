using System;

namespace MapleSim.Core
{
	public class Mobile : BaseEntity
	{
		public const int MaxRings = 4;

		private int m_Level;
		private int m_Experience;
		private int m_Hits, m_HitsMax;
		private int m_Mana, m_ManaMax;
		private Stats m_Stats;
		private Skills m_Skills;
		private Item m_Weapon;
		private Item[] m_Rings;

		#region Level, Experience

		public int Level
		{
			get { return m_Level; }
			set
			{
				if ( value < 0 )
					value = 0;
				else if ( value > LevelInfo.MaxLevel )
					value = LevelInfo.MaxLevel;

				if ( m_Level != value )
				{
					int oldLevel = m_Level;

					m_Level = value;

					OnLevelChanged( oldLevel );
				}
			}
		}

		public virtual void OnLevelChanged( int oldLevel )
		{
		}

		public int Experience
		{
			get { return m_Experience; }
			set
			{
				if ( value < 0 )
				{
					value = 0;
				}
				else
				{
					int experienceMax = LevelInfo.GetMaxExperience( m_Level );

					if ( value > experienceMax )
						value = experienceMax;
				}

				if ( m_Experience != value )
				{
					int oldExperience = m_Experience;

					m_Experience = value;

					OnExperienceChanged( oldExperience );
				}
			}
		}

		public virtual void OnExperienceChanged( int oldExperience )
		{
		}

		#endregion

		#region Hits, Mana

		public int Hits
		{
			get { return m_Hits; }
			set
			{
				if ( value < 0 )
				{
					value = 0;
				}
				else
				{
					int hitsMax = HitsMax;

					if ( value > hitsMax )
						value = hitsMax;
				}

				if ( m_Hits != value )
				{
					int oldHits = m_Hits;

					m_Hits = value;

					OnHitsChanged( oldHits );
				}
			}
		}

		public virtual void OnHitsChanged( int oldHits )
		{
		}

		public int HitsMaxSeed
		{
			get { return m_HitsMax; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_HitsMax != value )
				{
					m_HitsMax = value;

					int hitsMax = HitsMax;

					if ( m_Hits > hitsMax )
						m_Hits = hitsMax;
				}
			}
		}

		public int HitsMax { get { return GetAttribute( AttributeName.MaxHP ); } }

		public int Mana
		{
			get { return m_Mana; }
			set
			{
				if ( value < 0 )
				{
					value = 0;
				}
				else
				{
					int manaMax = ManaMax;

					if ( value > manaMax )
						value = manaMax;
				}

				if ( m_Mana != value )
				{
					int oldMane = m_Mana;

					m_Mana = value;

					OnManaChanged( oldMane );
				}
			}
		}

		public virtual void OnManaChanged( int oldHits )
		{
		}

		public int ManaMaxSeed
		{
			get { return m_ManaMax; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_ManaMax != value )
				{
					m_ManaMax = value;

					int manaMax = ManaMax;

					if ( m_Mana > manaMax )
						m_Mana = manaMax;
				}
			}
		}

		public int ManaMax { get { return GetAttribute( AttributeName.MaxMP ); } }

		#endregion

		#region Stats

		public Stats Stats { get { return m_Stats; } }

		public int StrRaw
		{
			get { return m_Stats.Str; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_Stats.Str != value )
				{
					m_Stats.Str = value;

					InvalidateAttribute( AttributeName.Str );
				}
			}
		}

		public int Str { get { return GetAttribute( AttributeName.Str ); } }

		public int DexRaw
		{
			get { return m_Stats.Dex; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_Stats.Dex != value )
				{
					m_Stats.Dex = value;

					InvalidateAttribute( AttributeName.Dex );
				}
			}
		}

		public int Dex { get { return GetAttribute( AttributeName.Dex ); } }

		public int IntRaw
		{
			get { return m_Stats.Int; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_Stats.Int != value )
				{
					m_Stats.Int = value;

					InvalidateAttribute( AttributeName.Int );
				}
			}
		}

		public int Int { get { return GetAttribute( AttributeName.Int ); } }

		public int LukRaw
		{
			get { return m_Stats.Luk; }
			set
			{
				if ( value < 0 )
					value = 0;

				if ( m_Stats.Luk != value )
				{
					m_Stats.Luk = value;

					InvalidateAttribute( AttributeName.Luk );
				}
			}
		}

		public int Luk { get { return GetAttribute( AttributeName.Luk ); } }

		#endregion

		public Skills Skills { get { return m_Skills; } }

		public Item Weapon
		{
			get
			{
				if ( m_Weapon != null && !m_Weapon.Deleted && m_Weapon.Parent == this )
					return m_Weapon;

				return m_Weapon = GetItem( Layer.Weapon );
			}
		}

		public Item[] Rings { get { return m_Rings; } }

		public Mobile()
		{
			m_Stats = new Stats();
			m_Skills = new Skills();
			m_Rings = new Item[MaxRings];
		}

		public bool Equip( Item item )
		{
			if ( item.Layer == Layer.Invalid )
				return false;

			if ( item.Layer == Layer.Ring )
			{
				bool available = false;

				for ( int i = 0; !available && i < m_Rings.Length; i++ )
				{
					if ( m_Rings[i] == null )
					{
						available = true;
						m_Rings[i] = item;
					}
				}

				if ( !available )
					return false;
			}
			else
			{
				if ( GetItem( item.Layer ) != null )
					return false;
			}

			AddItem( item );

			return true;
		}

		public override void OnItemAdded( Item item )
		{
			if ( item is IAttributes )
			{
				foreach ( AttributeValue attrVal in ( (IAttributes)item ).Attributes )
					InvalidateAttribute( attrVal.Attribute );
			}
		}

		public override void OnItemRemoved( Item item )
		{
			if ( item.Layer == Layer.Ring )
			{
				for ( int i = 0; i < m_Rings.Length; i++ )
				{
					if ( m_Rings[i] == item )
					{
						m_Rings[i] = null;
						break;
					}
				}
			}

			if ( item is IAttributes )
			{
				foreach ( AttributeValue attrVal in ( (IAttributes)item ).Attributes )
					InvalidateAttribute( attrVal.Attribute );
			}
		}

		public virtual void InvalidateAttribute( AttributeName attr )
		{
			switch ( attr )
			{
				case AttributeName.MaxHP: InvalidateHits(); break;
				case AttributeName.MaxMP: InvalidateMana(); break;
			}
		}

		public void InvalidateHits()
		{
			int hitsMax = HitsMax;

			if ( m_Hits > hitsMax )
			{
				int oldHits = m_Hits;

				m_Hits = hitsMax;

				OnHitsChanged( oldHits );
			}
		}

		public void InvalidateMana()
		{
			int manaMax = ManaMax;

			if ( m_Mana > manaMax )
			{
				int oldMana = m_Mana;

				m_Mana = manaMax;

				OnManaChanged( oldMana );
			}
		}

		public virtual int GetAttribute( AttributeName attr )
		{
			int value = 0;

			switch ( attr )
			{
				case AttributeName.MaxHP: value += HitsMaxSeed; break;
				case AttributeName.MaxMP: value += ManaMaxSeed; break;

				case AttributeName.Str: value += StrRaw; break;
				case AttributeName.Dex: value += DexRaw; break;
				case AttributeName.Int: value += IntRaw; break;
				case AttributeName.Luk: value += LukRaw; break;

				case AttributeName.Magic: value += Int; break;
			}

			foreach ( Item item in Items )
			{
				if ( item is IAttributes )
					value += ( (IAttributes)item ).Attributes[attr];
			}

			return value;
		}

		public void UseSkill( int skillID )
		{
			Skill skill = Skill.GetSkill( skillID );

			if ( skill != null && skill.CanUse )
				skill.Use( this );
		}

		public void SendMessage( string format, params object[] args )
		{
			SendMessage( String.Format( format, args ) );
		}

		public virtual void SendMessage( string text )
		{
			Console.WriteLine( "{0}: {1}", Name, text );
		}

		public override string ToString()
		{
			return String.Format( "{0} ({1})", Name, Level );
		}
	}
}
