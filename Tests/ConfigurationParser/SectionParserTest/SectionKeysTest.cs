using System;
using NUnit.Framework;
using ConfigurationParser;

namespace Tests.ConfigurationParser.SectionParserTest 
{
    [TestFixture]
    public class SectionKeysTest : TestBase
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void No_valid_keys_in_section_should_throw_ArgumentException()
        {
            //header section has no keys starting in column 0
            var reader = new FileReader(GetPath("section-no-keys.txt"));
            var parser = new SectionParser(reader);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Section_should_not_contain_duplicate_keys()
        {
            var reader = new FileReader(GetPath("duplicate-section-keys.txt"));
            var parser = new SectionParser(reader);
        }

        [Test]
        public void The_same_key_should_be_usable_in_different_sections()
        {
            var reader = new FileReader(GetPath("sections-with-same-keys.txt"));
            var parser = new SectionParser(reader);

            Assert.AreEqual("value", parser.Sections["section-one"]["key"]);
            Assert.AreEqual("value", parser.Sections["section-two"]["key"]);
        }

        [Test]
        public void Keys_should_be_able_to_contain_empty_values()
        {
            var reader = new FileReader(GetPath("key-with-empty-value.txt"));
            var parser = new SectionParser(reader);

            Assert.AreEqual(string.Empty, parser.Sections["header"]["project"]);
            Assert.AreEqual("4.5", parser.Sections["header"]["budget"]);
        }
    }
}

