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
			if (input.Count() > 0) {
				var i = new Interpreter(null);
				try {
					i.ops.AddRange(Parse(input));
					i.Resume();
					if (i.stack.Count() > 0) Console.WriteLine(i.stack.Peek());
				} catch(InvalidOperationException) {
					Console.WriteLine("Syntax Error.");
				}
			}
		}
		
		private List<Op> Parse(string code) {
			var ops = new List<Op>();
			int i;
			
			if (code.First() == '#') ops.Add(new LoadLiteral(code.Skip(1).ToString()));
			else if (int.TryParse(code, out i)) ops.Add(new LoadConstant(i));
			else throw new InvalidOperationException();
			return ops;
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

