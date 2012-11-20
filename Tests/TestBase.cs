using System;
using System.IO;
using NUnit.Framework;

namespace Tests
{
	abstract public class TestBase
	{
		protected string FixturesPath = Path.GetFullPath("Fixtures");

		protected string GetPath(string pathFromFixtures, string pathPrefix = "")
		{
			return String.Format("{0}{1}{2}{3}", pathPrefix, FixturesPath, Path.DirectorySeparatorChar, pathFromFixtures);
		}

		protected void CreateFixture(string name)
		{
			var newPath = FixturesPath + Path.DirectorySeparatorChar + name;
			File.Create(newPath).Close();
		}

		protected void DeleteFixture(string name)
		{
			var path = GetPath(name);
			if(File.Exists(path))
				File.Delete(path);
		}

		protected void CompareFixtures(string fix1, string fix2)
		{
			var contents1 = new StreamReader(GetPath(fix1)).ReadToEnd().Trim();
			var contents2 = new StreamReader(GetPath(fix2)).ReadToEnd().Trim();
			Assert.AreEqual(contents1, contents2);
		}
	}
}

