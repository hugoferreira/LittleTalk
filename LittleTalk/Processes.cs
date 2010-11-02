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
		public override void Activate () {
			Console.Write(ToString() + "> ");
			Console.ReadLine();
		}
		
		public override string ToString () {
			return "Driver";
		}
	}	
	
	public class ProcessManager {
		public List<Process> Processes { get; private set; }
		
		public ProcessManager() {
			this.Processes = new List<Process>();
		}
				
		public void Start() {
			while(true) {
				var p = Processes.First();
				p.Activate();
			}
		}
	}
}

