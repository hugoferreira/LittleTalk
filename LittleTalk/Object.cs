using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleTalk {
	public class Courier {
	
	}
	
	public class Class {
		public Object Name { get; set; }
		public Object Super { get; set; }
		public Object Vars { get; set; }
		public Object Methods { get; set; }	
	}
	
	public class Object {
		public int ReferenceCount { get; set; }
		public Class Class { get; set; }
		public Object Super { get; set; }
		public Dictionary<string, Object> Variables { get; private set; }
		
		public Object() {
			this.Variables = new Dictionary<string, Object>();
		}
	}
		
	public class ObjTrue: Object {
	}
	
	public class ObjFalse: Object {
	}
	
	public class Nil: Object {
	}
}