using System;

namespace LittleTalk {
	public class NativeValue: Object {
	}
	
	public class NativeString: NativeValue {
		public string Value { get; set; }
	}
		
	public class NativeInteger: Object {
		public int Value { get; set; }
		
		public NativeInteger(int i) {
			this.Value = i;
		}		
		
		public override string ToString ()
		{
			return Value.ToString ();
		}
		
		public static NativeInteger operator +(NativeInteger c1, NativeInteger c2){ 
			return new NativeInteger(c1.Value + c2.Value);
		}
	}
	
	public class Literal: Object {
		public string Value { get; set; }
		
		public Literal(string symbol) {
			this.Value = symbol;
		}
	}
}

