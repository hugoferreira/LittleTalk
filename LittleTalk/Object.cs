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
		public List<Object> Variables { get; private set; }
		
		public Object() {
			this.Variables = new List<Object>();
		}
	}
	
	public class ObjTrue: Object {
	}
	
	public class ObjFalse: Object {
	}
}

