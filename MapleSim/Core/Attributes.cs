using System;
using System.Collections;
using System.Collections.Generic;

namespace MapleSim.Core
{
	[Flags]
	public enum AttributeName : ulong
	{
		MaxHP			= 0x1,
		MaxMP			= 0x2,
		Str				= 0x4,
		Dex				= 0x8,
		Int				= 0x10,
		Luk				= 0x20,
		Attack			= 0x40,
		WeaponDef		= 0x80,
		Magic			= 0x100,
		MagicDef		= 0x200,
		Accuracy		= 0x400,
		Avoidability	= 0x800,
		CritRate		= 0x1000,
		CritDamage		= 0x2000,
		Speed			= 0x4000,
		Jump			= 0x8000
	}

	public interface IAttributes
	{
		Attributes Attributes { get; }
	}

	public class Attributes : IEnumerable<AttributeValue>
	{
		private static readonly int[] m_EmptyValues = new int[0];

		private AttributeName m_Flags;
		private int[] m_Values;

		public AttributeName Flags { get { return m_Flags; } }
		public int[] Values { get { return m_Values; } }

		public int this[AttributeName attr]
		{
			get { return GetValue( attr ); }
			set { SetValue( attr, value ); }
		}

		public Attributes()
		{
			m_Values = m_EmptyValues;
		}

		public Attributes( Attributes copy )
		{
			m_Flags = copy.m_Flags;
			m_Values = new int[copy.m_Values.Length];

			Array.Copy( copy.m_Values, m_Values, m_Values.Length );
		}

		#region Attribute-value constructors

		public Attributes( AttributeName attr, int value )
			: this( new AttributeValue[] { new AttributeValue( attr, value ) } )
		{
		}

		public Attributes( AttributeName attr1, int value1, AttributeName attr2, int value2 )
			: this( new AttributeValue[] { new AttributeValue( attr1, value1 ), new AttributeValue( attr2, value2 ) } )
		{
		}

		public Attributes( AttributeName attr1, int value1, AttributeName attr2, int value2, AttributeName attr3, int value3 )
			: this( new AttributeValue[] { new AttributeValue( attr1, value1 ), new AttributeValue( attr2, value2 ), new AttributeValue( attr3, value3 ) } )
		{
		}

		public Attributes( params AttributeValue[] attrVals )
			: this()
		{
			for ( int i = 0; i < attrVals.Length; i++ )
				SetValue( attrVals[i].Attribute, attrVals[i].Value );
		}

		#endregion

		#region GetValue, SetValue

		private int GetValue( AttributeName attr )
		{
			if ( ( m_Flags & attr ) == 0 )
				return 0;

			int index = GetIndex( attr );

			if ( index >= 0 && index < m_Values.Length )
				return m_Values[index];

			return 0;
		}

		private void SetValue( AttributeName attr, int value )
		{
			if ( value != 0 )
			{
				int index = GetIndex( attr );

				if ( ( m_Flags & attr ) != 0 )
				{
					m_Values[index] = value;
				}
				else
				{
					m_Flags |= attr;

					EnsureSize( index + 1 );

					Insert( m_Values, index, value );
				}
			}
			else
			{
				m_Flags &= ~attr;
			}
		}

		private int GetIndex( AttributeName attr )
		{
			ulong curr = 0x1;

			int index = 0;

			for ( int i = 0; i < 64; i++, curr <<= 1 )
			{
				if ( curr == (ulong)attr )
					return index;

				if ( ( (ulong)m_Flags & curr ) != 0 )
					index++;
			}

			return -1;
		}

		private void EnsureSize( int size )
		{
			if ( size < 0 || size > 64 )
				throw new InvalidOperationException( "Invalid array size" );

			if ( m_Values.Length >= size )
				return;

			int length = 1;

			for ( int k = 0; length < size && k < 6; k++ )
				length *= 2;

			int[] temp = m_Values;

			m_Values = new int[length];

			Array.Copy( temp, m_Values, temp.Length );
		}

		private static void Insert( int[] array, int index, int value )
		{
			if ( index < 0 || index >= array.Length )
				throw new IndexOutOfRangeException();

			for ( int i = index + 1; i < array.Length; i++ )
				array[i] = array[i - 1];

			array[index] = value;
		}

		#endregion

		public void Add( Attributes attrs )
		{
			foreach ( AttributeValue attrVal in attrs )
				this[attrVal.Attribute] += attrVal.Value;
		}

		public void Subtract( Attributes attrs )
		{
			foreach ( AttributeValue attrVal in attrs )
				this[attrVal.Attribute] -= attrVal.Value;
		}

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<AttributeValue> GetEnumerator()
		{
			return new InternalEnumerator( this );
		}

		public class InternalEnumerator : IEnumerator<AttributeValue>
		{
			private Attributes m_Attributes;
			private ulong m_Mask;
			private ulong m_Curr;

			public InternalEnumerator( Attributes attrs )
			{
				m_Attributes = attrs;
				m_Mask = (ulong)m_Attributes.m_Flags;
				m_Curr = 0;
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public AttributeValue Current
			{
				get { return new AttributeValue( (AttributeName)m_Curr, m_Attributes[(AttributeName)m_Curr] ); }
			}

			public bool MoveNext()
			{
				if ( m_Curr == 0 )
				{
					m_Curr = 0x1;

					if ( ( m_Mask & 0x1 ) != 0 )
						return true;
				}

				while ( m_Mask != 0 )
				{
					m_Mask >>= 1;
					m_Curr <<= 1;

					if ( ( m_Mask & 0x1 ) != 0 )
						return true;
				}

				return false;
			}

			public void Reset()
			{
				m_Mask = (ulong)m_Attributes.m_Flags;
				m_Curr = 0;
			}

			public void Dispose()
			{
				m_Attributes = null;
				m_Mask = 0;
				m_Curr = 0;
			}
		}

		#endregion
	}

	public struct AttributeValue
	{
		public readonly AttributeName Attribute;
		public readonly int Value;

		public AttributeValue( AttributeName attr, int value )
		{
			Attribute = attr;
			Value = value;
		}
	}
}
