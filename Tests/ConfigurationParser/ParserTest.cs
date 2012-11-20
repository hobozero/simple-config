using System;
using NUnit.Framework;
using System.Reflection;
using ConfigurationParser;
using System.IO;

namespace Tests.ConfigurationParser
{
	[TestFixture]
	public class ParserTest : TestBase
	{
	  protected Parser Parser { get; set; }

		[SetUp]
		public void SetUp()
		{
			DeleteFixture("test-config.txt");
			File.Copy(GetPath("valid-config.txt"), GetPath("test-config.txt"));
			Parser = new Parser(GetPath("test-config.txt"));
		}

		[Test]
		public void GetString_should_return_string_value_from_section()
		{
			var headerProject = Parser.GetString("header", "project");
			Assert.AreEqual("Programming Test", headerProject);
		}

		[Test]
		public void GetFloat_should_return_float_value_from_section()
		{
			var headerBudget = Parser.GetFloat("header", "budget");
			Assert.AreEqual(4.5F, headerBudget);
		}

		[Test]
		public void GetInt_should_return_integer_value_from_section()
		{
			var headerAccessed = Parser.GetInt("header", "accessed");
			Assert.AreEqual(205, headerAccessed);
		}

		[Test]
		public void SetString_should_overwrite_key_with_string_value_then_write_file_to_disk()
		{
			Parser.SetString("header", "project", "Hot Ham Sandwich");
			CompareFixtures("test-overwrite-string.txt", "test-config.txt");
		}

		[Test]
		public void SetString_should_add_key_with_string_value_then_write_file_to_disk()
		{
			Parser.SetString("header", "status", "Complete");
			CompareFixtures("test-add-string.txt", "test-config.txt");
		}

		[Test]
		public void SetString_should_do_nothing_if_value_has_not_changed()
		{
			Parser.SetString("header", "project", "Programming Test");
			CompareFixtures("valid-config.txt", "test-config.txt");
		}

		[Test]
		public void SetFloat_should_overwrite_key_with_string_representation_of_float()
		{
			Parser.SetFloat("header", "budget", 5.2F);
			CompareFixtures("test-overwrite-float.txt", "test-config.txt");
		}

		[Test]
		public void SetInt_should_overwrite_key_with_string_representation_of_int()
		{
			Parser.SetInt("header", "accessed", 185);
			CompareFixtures("test-overwrite-int.txt", "test-config.txt");
		}
	}
}

