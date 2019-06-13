using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpBrowser.TreeNodes
{
	public class BmpHeader : DataNode
	{
		public IReadable Source;

		public BmpHeader() : base(false)
		{
		}

		public override void Reload()
		{
		}

		public override object GetProperties()
		{
			using (var reader = new BinaryReader(Source.GetStream()))
			{
				reader.BaseStream.Position = 0;

				var signature = Encoding.ASCII.GetString(reader.ReadBytes(2));
				var length = reader.ReadUInt32();
				var reserved = reader.ReadBytes(4);
				var dataOffset = reader.ReadUInt32();

				return new
				{
					Signature = signature,
					Length = length,
					Reserved = reserved,
					DataOffset = dataOffset
				};
			}
		}
	}
}

