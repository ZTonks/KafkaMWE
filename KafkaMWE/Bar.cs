// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Test
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using global::Avro;
	using global::Avro.Specific;
	
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("avrogen", "1.12.0+8c27801dc8d42ccc00997f25c0b8f45f8d4a233e")]
	public partial class Bar : global::Avro.Specific.ISpecificRecord
	{
		public static global::Avro.Schema _SCHEMA = global::Avro.Schema.Parse("{\"type\":\"record\",\"name\":\"Bar\",\"namespace\":\"Test\",\"fields\":[{\"name\":\"Id\",\"type\":\"s" +
				"tring\"},{\"name\":\"Bar1\",\"type\":[\"null\",\"string\"]},{\"name\":\"Bar2\",\"type\":\"int\"},{\"" +
				"name\":\"Bar3\",\"type\":{\"type\":\"enum\",\"name\":\"Bar3\",\"namespace\":\"Test\",\"symbols\":[\"" +
				"Value1\",\"Value2\",\"Value3\"],\"default\":\"Value1\"}},{\"name\":\"Bar4\",\"type\":[\"null\",\"i" +
				"nt\"]},{\"name\":\"Bar5\",\"type\":{\"type\":\"array\",\"items\":{\"type\":\"record\",\"name\":\"Bar" +
				"5\",\"namespace\":\"Test\",\"fields\":[{\"name\":\"Id\",\"type\":\"string\"},{\"name\":\"Bar6\",\"ty" +
				"pe\":{\"type\":\"enum\",\"name\":\"Bar6\",\"namespace\":\"Test\",\"symbols\":[\"Value1\",\"Value2\"" +
				",\"Value3\",\"Value4\",\"Value5\",\"Value6\",\"Value7\",\"Value8\"],\"default\":\"Value8\"}},{\"n" +
				"ame\":\"Bar7\",\"type\":[\"null\",\"string\"]},{\"name\":\"Bar8\",\"type\":[\"null\",\"string\"]},{" +
				"\"name\":\"Bar9\",\"type\":{\"type\":\"int\",\"logicalType\":\"date\"}},{\"name\":\"Bar10\",\"type\"" +
				":[\"null\",{\"type\":\"bytes\",\"logicalType\":\"decimal\",\"precision\":22,\"scale\":4}]},{\"n" +
				"ame\":\"Bar11\",\"type\":[\"null\",{\"type\":\"bytes\",\"logicalType\":\"decimal\",\"precision\":" +
				"14,\"scale\":2}]},{\"name\":\"Bar12\",\"type\":[\"null\",{\"type\":\"bytes\",\"logicalType\":\"de" +
				"cimal\",\"precision\":19,\"scale\":7}]},{\"name\":\"Bar13\",\"type\":{\"type\":\"bytes\",\"logic" +
				"alType\":\"decimal\",\"precision\":14,\"scale\":2}},{\"name\":\"Bar14\",\"type\":[\"null\",\"int" +
				"\"]},{\"name\":\"Bar15\",\"type\":[\"null\",{\"type\":\"record\",\"name\":\"Bar15\",\"namespace\":\"" +
				"Test\",\"fields\":[{\"name\":\"Bar16\",\"type\":\"long\"},{\"name\":\"Bar17\",\"type\":[\"null\",\"i" +
				"nt\"]},{\"name\":\"Bar18\",\"type\":[\"null\",\"int\"]},{\"name\":\"Bar19\",\"type\":[\"null\",\"int" +
				"\"]},{\"name\":\"Bar20\",\"type\":[\"null\",\"int\"]},{\"name\":\"Bar21\",\"type\":\"int\"},{\"name\"" +
				":\"Bar22\",\"type\":\"int\"},{\"name\":\"Bar23\",\"type\":{\"type\":\"int\",\"logicalType\":\"date\"" +
				"}},{\"name\":\"Bar24\",\"type\":{\"type\":\"string\",\"logicalType\":\"uuid\"}}]}]}]}}}]}");
		private string _Id;
		private string _Bar1;
		private int _Bar2;
		private Test.Bar3 _Bar3 = Test.Bar3.Value1;
		private System.Nullable<System.Int32> _Bar4;
		private IList<Test.Bar5> _Bar5;
		public virtual global::Avro.Schema Schema
		{
			get
			{
				return Bar._SCHEMA;
			}
		}
		public string Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}
		public string Bar1
		{
			get
			{
				return this._Bar1;
			}
			set
			{
				this._Bar1 = value;
			}
		}
		public int Bar2
		{
			get
			{
				return this._Bar2;
			}
			set
			{
				this._Bar2 = value;
			}
		}
		public Test.Bar3 Bar3
		{
			get
			{
				return this._Bar3;
			}
			set
			{
				this._Bar3 = value;
			}
		}
		public System.Nullable<System.Int32> Bar4
		{
			get
			{
				return this._Bar4;
			}
			set
			{
				this._Bar4 = value;
			}
		}
		public IList<Test.Bar5> Bar5
		{
			get
			{
				return this._Bar5;
			}
			set
			{
				this._Bar5 = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.Id;
			case 1: return this.Bar1;
			case 2: return this.Bar2;
			case 3: return this.Bar3;
			case 4: return this.Bar4;
			case 5: return this.Bar5;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.Id = (System.String)fieldValue; break;
			case 1: this.Bar1 = (System.String)fieldValue; break;
			case 2: this.Bar2 = (System.Int32)fieldValue; break;
			case 3: this.Bar3 = (Test.Bar3)fieldValue; break;
			case 4: this.Bar4 = (System.Nullable<System.Int32>)fieldValue; break;
			case 5: this.Bar5 = (IList<Test.Bar5>)fieldValue; break;
			default: throw new global::Avro.AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
