using System;
using NUnit.Framework;
using ConfigurationParser;

namespace Tests.ConfigurationParser.SectionParserTest 
{
	public class SectionsWithLeadingWhiteSpaceTest : SectionParserTest
	{
		[SetUp]
		public override void SetUp ()
		{
			Parser = new SectionParser(new FileReader(GetPath("valid-leading-whitespace.txt")));
		}
	}
}

