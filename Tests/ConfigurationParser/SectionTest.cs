using System;
using NUnit.Framework;
using ConfigurationParser;
using System.Collections.Generic;

namespace Tests.ConfigurationParser 
{
    [TestFixture]
    public class SectionTest
    {
        protected Section Section { get; set; }

        [SetUp]
        public void SetUp()
        {
            Section = new Section("header");
            Section["string"] = "hello world";
            Section["int"] = "205";
            Section["float"] = "4.5";
        }

        [Test]
        public void Section_should_be_instance_of_Dictionary_with_string_keys_and_values()
        {
            Assert.IsInstanceOf<Dictionary<string, string>>(Section);
        }

        [Test]
        public void GetString_should_return_value_as_string()
        {
            Assert.AreEqual("hello world", Section.GetString("string"));
        }

        [Test]
        public void GetInt_should_return_value_as_int()
        {
            Assert.AreEqual(205, Section.GetInt("int"));
        }

        [Test]
        public void GetFloat_should_return_value_as_float()
        {
            Assert.AreEqual(4.5F, Section.GetFloat("float"));
        }

        [Test]
        public void GetInt_should_return_0_if_it_cant_be_parsed()
        {
            Assert.AreEqual(0, Section.GetInt("string"));
        }

        [Test]
        public void GetFloat_should_return_0_if_it_cant_be_parsed()
        {
            Assert.AreEqual(0F, Section.GetFloat("string"));
        }

        [Test]
        public void SetString_should_reset_existing_key_to_string_value()
        {
            Section.SetString("string", "goodbye");
            Assert.AreEqual("goodbye", Section["string"]);
        }

        [Test]
        public void SetString_should_add_new_key_and_value_if_key_is_new()
        {
            Section.SetString("string2", "new string!");
            Assert.AreEqual("new string!", Section["string2"]);
        }

        [Test]
        public void SetInt_should_set_value_to_string_representation_of_int()
        {
            Section.SetInt("int", 57);
            Assert.AreEqual("57", Section["int"]);
        }

        [Test]
        public void SetFloat_should_set_value_to_string_representation_of_float()
        {
            Section.SetFloat("float", 72.7F);
            Assert.AreEqual("72.7", Section["float"]);
        }

        [Test]
        public void ToString_should_return_string_in_valid_config_format()
        {
            var str = "[header]\r\nstring: hello world\r\nint: 205\r\nfloat: 4.5\r\n";
            Assert.AreEqual(str, Section.ToString());
        }

        [Test]
        public void ToString_should_convert_null_values_to_empty_strings()
        {
            Section["int"] = null;
            var str = "[header]\r\nstring: hello world\r\nint: \r\nfloat: 4.5\r\n";
            Assert.AreEqual(str, Section.ToString());
        }
    }
}

