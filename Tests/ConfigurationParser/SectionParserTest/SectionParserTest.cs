using System;
using NUnit.Framework;
using ConfigurationParser;
using System.Collections.Generic;

namespace Tests.ConfigurationParser.SectionParserTest 
{
    [TestFixture]
    public class SectionParserTest : TestBase
    {
        protected SectionParser Parser { get; set; }

        [SetUp]
        public virtual void SetUp ()
        {
            Parser = new SectionParser(new FileReader(GetPath("valid-config.txt")));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_should_throw_ArgumentException_if_first_non_blank_line_is_not_a_section_heading()
        {
            var parser = new SectionParser(new FileReader(GetPath("invalid-firstline.txt")));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_should_throw_ArgumentException_if_file_has_non_unique_section_headers ()
        {
            var parser = new SectionParser(new FileReader(GetPath("duplicate-headings.txt")));
        }

        [Test]
        public void Sections_should_be_a_Dictionary_of_Sections_with_correct_number_of_sections_from_text()
        {
            Assert.IsInstanceOf<Dictionary<string, Section>>(Parser.Sections);
            Assert.AreEqual(3, Parser.Sections.Count);
        }

        [Test]
        public void Sections_should_contain_keys_for_every_section_in_file()
        {
            Assert.IsTrue(Parser.Sections.ContainsKey("header"));
            Assert.IsTrue(Parser.Sections.ContainsKey("meta data"));
            Assert.IsTrue(Parser.Sections.ContainsKey("trailer"));
        }

        [Test]
        public void A_key_in_sections_should_point_to_a_Section_object_containing_key_value_pairs()
        {
            var header = Parser.Sections["header"];
            Assert.AreEqual("Programming Test", header["project"]);
            Assert.AreEqual("4.5", header["budget"]);
            Assert.AreEqual("205", header["accessed"]);
        }

        [Test]
        public void Newlines_indented_with_whitespace_should_be_appended_to_a_sections_value()
        {
            var meta = Parser.Sections["meta data"];
            var line = "This is a tediously long description of the Art & Logic  programming test that you are taking. Tedious isn't the right word, but  it's the first word that comes to mind.";
            Assert.AreEqual(line, meta["description"]);
        }

        [Test]
        public void Parsed_section_should_not_include_empty_lines()
        {
            Assert.AreEqual(2, Parser.Sections["meta data"].Count);
        }

        [Test]
        public void Parsed_section_should_allow_keys_with_spaces()
        {
            var meta = Parser.Sections["meta data"];
            var correction = meta["correction text"];
            Assert.AreEqual("I meant 'moderately,' not 'tediously,' above.", correction);
        }
    }
}

