using System;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using ConfigurationParser;

namespace Tests.ConfigurationParser
{
	[TestFixture]
	public class FileReaderTest : TestBase
	{
		protected string FilePath { get; set; }
		protected FileReader Reader { get; set; }

		[SetUp]
		public void SetUp ()
		{
			FilePath = GetPath("valid-config.txt");
			Reader = new FileReader(FilePath);
		}
 
		[Test]
		public void Constructor_should_set_readonly_FilePath_property()
		{
			Assert.AreEqual(FilePath, Reader.FilePath);
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void Constructor_should_throw_FileNotFoundException_if_file_does_not_exist()
		{
			var path = "Fixtures\\testconfizzzz.txt";
			var reader = new FileReader(path);
			Assert.AreEqual(path, reader.FilePath);
		}
	}
}

