using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpBrowser.TreeNodes
{
	public class DibHeader : DataNode
	{
		public IReadable Source;

		public DibHeader() : base(false)
		{
		}

		public override void Reload()
		{
		}

		public override object GetProperties()
		{
			using (var reader = new BinaryReader(Source.GetStream()))
			{
				reader.BaseStream.Position = 14;

				var length = reader.ReadUInt32();
				var width = reader.ReadInt32();
				var height = reader.ReadInt32();
				var planeCount = reader.ReadUInt16();
				var bitsPerPixel = reader.ReadUInt16();
				var compression = reader.ReadUInt32();
				var dataLength = reader.ReadUInt32();
				var horizontalResolution = reader.ReadUInt32();
				var verticalResolution = reader.ReadUInt32();
				var paletteEntryCount = reader.ReadUInt32();
				var importantColors = reader.ReadUInt32();

				return new
				{
					Length = length,
					Width = width,
					Height = height,
					PlaneCount = planeCount,
					BitsPerPixel = bitsPerPixel,
					Compression = compression,
					DataLength = dataLength,
					HorizontalResolution = horizontalResolution,
					VerticalResolution = verticalResolution,
					PaletteEntryCount = paletteEntryCount,
					ImportantColors = importantColors
				};
			}
		}
	}
}

