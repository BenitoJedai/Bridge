using Bridge.CLR;

namespace Bridge.Html5
{
	/// <summary>
	/// An ArrayBuffer is a useful object for representing an arbitrary chunk of data. In many cases, such data will be read from disk or from the network, and will not follow the alignment restrictions that are imposed on the Typed Array Views described earlier. In addition, the data will often be heterogeneous in nature and have a defined byte order.
    /// The DataView view provides a low-level interface for reading such data from and writing it to an ArrayBuffer.
	/// </summary>
    [Ignore]
    [Name("DataView")]
	public class DataView : ArrayBufferView 
    {
		/// <summary>
        /// Returns a new DataView object using the passed ArrayBuffer for its storage.
		/// </summary>
        /// <param name="buffer">An existing ArrayBuffer to use as the storage for the new DataView object.</param>
        public DataView(ArrayBuffer buffer) 
        {
		}

        /// <summary>
        /// Returns a new DataView object using the passed ArrayBuffer for its storage.
        /// </summary>
        /// <param name="buffer">An existing ArrayBuffer to use as the storage for the new DataView object.</param>
        /// <param name="byteOffset">The offset, in bytes, to the first byte in the specified buffer for the new view to reference. If not specified, the view of the buffer will start with the first byte.</param>
		public DataView(ArrayBuffer buffer, long byteOffset) 
        {
		}

        /// <summary>
        /// Returns a new DataView object using the passed ArrayBuffer for its storage.
        /// </summary>
        /// <param name="buffer">An existing ArrayBuffer to use as the storage for the new DataView object.</param>
        /// <param name="byteOffset">The offset, in bytes, to the first byte in the specified buffer for the new view to reference. If not specified, the view of the buffer will start with the first byte.</param>
        /// <param name="byteLength">The number of elements in the byte array. If unspecified, length of the view will match the buffer's length.</param>
		public DataView(ArrayBuffer buffer, long byteOffset, long byteLength) 
        {
		}

        /// <summary>
        /// Gets a signed 8-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public sbyte GetInt8(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an unsigned 8-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public byte GetUint8(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets a signed 16-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public short GetInt16(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets a signed 16-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <param name="littleEndian">Indicates whether the 16-bit int is stored in little- or big-endian format. If false or undefined, a big-endian value is read.</param>
        /// <returns></returns>
		public short GetInt16(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an unsigned 16-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public ushort GetUint16(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an unsigned 16-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <param name="littleEndian">Indicates whether the 16-bit int is stored in little- or big-endian format. If false or undefined, a big-endian value is read.</param>
        /// <returns></returns>
		public ushort GetUint16(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an signed 32-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public int GetInt32(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an signed 32-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <param name="littleEndian">Indicates whether the 32-bit int is stored in little- or big-endian format. If false or undefined, a big-endian value is read.</param>
        /// <returns></returns>
		public int GetInt32(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an unsigned 32-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <returns></returns>
		public uint GetUint32(long byteOffset) 
        {
			return 0;
		}

        /// <summary>
        /// Gets an unsigned 32-bit integer at the specified byte offset from the start of the view.
        /// </summary>
        /// <param name="byteOffset">The offset, in byte, from the start of the view where to read the data.</param>
        /// <param name="littleEndian">Indicates whether the 32-bit int is stored in little- or big-endian format. If false or undefined, a big-endian value is read.</param>
        /// <returns></returns>
		public uint GetUint32(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

		public float GetFloat32(long byteOffset) 
        {
			return 0;
		}

		public float GetFloat32(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

		public double GetFloat64(long byteOffset) 
        {
			return 0;
		}

		public double GetFloat64(long byteOffset, bool littleEndian) 
        {
			return 0;
		}

		public void SetInt8(long byteOffset, sbyte value) 
        {
		}

		public void SetUint8(long byteOffset, byte value) 
        {
		}

		public void SetInt16(long byteOffset, short value) 
        {
		}

		public void SetInt16(long byteOffset, short value, bool littleEndian) 
        {
		}

		public void SetUint16(long byteOffset, ushort value) 
        {
		}

		public void SetUint16(ulong byteOffset, ushort value, bool littleEndian) 
        {
		}

		public void SetInt32(long byteOffset, int value) 
        {
		}

		public void SetInt32(long byteOffset, int value, bool littleEndian) 
        {
		}

		public void SetUint32(long byteOffset, uint value) 
        {
		}

		public void SetUint32(ulong byteOffset, uint value, bool littleEndian) 
        {
		}

		public void SetFloat32(long byteOffset, float value) 
        {
		}

		public void SetFloat32(long byteOffset, float value, bool littleEndian) 
        {
		}

		public void SetFloat64(long byteOffset, double value) 
        {
		}

		public void SetFloat64(long byteOffset, double value, bool littleEndian) 
        {
		}
	}
}
