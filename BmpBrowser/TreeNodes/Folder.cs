using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpBrowser.TreeNodes
{
	public class Folder : DataNode
	{
		public string Path;

		public Folder() : base(true)
		{
		}

		public override void Reload()
		{
			Nodes.Clear();

			foreach (var folder in Directory.GetDirectories(Path))
				Nodes.Add(new Folder { Text = System.IO.Path.GetFileName(folder), Path = folder });

			foreach (var file in Directory.GetFiles(Path, "*.bmp"))
				Nodes.Add(new BmpFile { Text = System.IO.Path.GetFileName(file), Path = file });
		}

		public override object GetProperties()
		{
			return new { Path };
		}
	}
}
