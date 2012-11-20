Simple Config Parser
====================

This is a simple configuration parser written in C# and built using Mono.

File Format
-----------
A configuration file has the following format:

[Section Heading]
key: value
key2: value
multiline: This is a longer configuration
  that is broken into multiple lines by having
  each line after the first be indented one or
  more spaces

[ Section Two ]
key: keys cant be duplicated in a section
  but different sections can have the same key

Config values can be fetched as strings, floats, and ints, and can be set as such.

Methods
-------

A parser is created like so:

```c#
var parser = new Parser("/path/to/config.txt")
```

Getting values is easy:

```c#
var stringValue = parser.GetString("Section Heading", "key");
var floatValue = parser.GetFloat("Section Heading", "float key");
var intValue = parser.GetInt("Section Heading 2", "int key");
```

Setting values is easy as well:

```c#
//add a new field or update an existing
parser.SetString("Section Heading", "new key", "new string value");
parser.SetFloat("Section Heading", "float key", 5.2F);
parser.SetInt("Section Heading 2", "int key", 5);
```

Please note that calling a set method immediately writes the value back to the config file.

Running The Tests
-----------------
The simple config tests were written using the NUnit bundled with Monodevelop. If building in a different environment, an appropriate nunit.dll must be included.
