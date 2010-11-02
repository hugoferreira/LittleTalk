using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleTalk {
	public class Process {
		
		// interpreter
		
		public virtual void Activate() {
		}
	}
	
	public class DriverProcess: Process {
		private ProcessManager processManager;
		
		public DriverProcess(ProcessManager pm) {
			this.processManager = pm;
		}
		
		public override void Activate () {
			Console.Write(ToString() + "> ");
			var input = Console.ReadLine().Trim();
			if (input.Count() > 0) Console.WriteLine(Parse(input));
			Console.WriteLine();
		}
		
		private Object Parse(string code) {	
			int i;
			if (code.First() == '#') return new Literal(code.Skip(1).ToString());
			if (int.TryParse(code, out i)) return new NativeInteger(i);
			
			return processManager.nil;
		}
		
		public override string ToString () {
			return "Driver";
		}
	}	
	
	public class ProcessManager {
		public Object nil;
		public List<Process> Processes { get; private set; }
		
		public ProcessManager() {
			this.Processes = new List<Process>();	
			this.Bootstrap();
		}
		
		private void Bootstrap() {
			this.nil = new Nil();			
		}
				
		public void Start() {
			while(true) {
				var p = Processes.First();
				p.Activate();
			}
		}
	}
}

