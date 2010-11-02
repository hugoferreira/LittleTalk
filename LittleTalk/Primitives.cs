using System;

namespace LittleTalk {
	public class NativeValue {
	}
	
	public class NativeString: NativeValue {
		public string Value { get; set; }
	}
	
	public class NativeInteger: NativeValue {
		public int Value { get; set; }
	}
}

