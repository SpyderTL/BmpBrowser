using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpBrowser.TreeNodes
{
	public class BmpData : DataNode
	{
		public IReadable Source;

		public BmpData() : base(false)
		{
		}

		public override void Reload()
		{
		}

		public override object GetProperties()
		{
			using (var reader = new BinaryReader(Source.GetStream()))
			{
				reader.BaseStream.Position = 10;

				var dataOffset = reader.ReadUInt32();
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

				reader.BaseStream.Position = dataOffset;

				var flip = false;

				if (height < 0)
				{
					height = -height;
					flip = true;
				}

				var pixels = new Color[width * height];

				for (var y = 0; y < height; y++)
				{
					for (var x = 0; x < width; x++)
					{
						Color pixel;

						switch (bitsPerPixel)
						{
							case 24:
								var values = reader.ReadBytes(3);

								pixel = Color.FromArgb(values[2], values[1], values[0]);
								break;

							default:
								pixel = Color.Black;
								break;
						}

						if (flip)
							pixels[(y * width) + x] = pixel;
						else
							pixels[((height - y - 1) * width) + x] = pixel;
					}
				}

				return new
				{
					Pixels = pixels
				};
			}
		}
	}
}

