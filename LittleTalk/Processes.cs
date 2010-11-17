using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleTalk {
	public enum ProcessState { Suspended, Blocked, Terminated, Running }
	
	public class Process {
		private Interpreter currentInterpreter;
		private ProcessManager processManager;
		public ProcessState ProcessState { get; set; }
		
		public Process(ProcessManager pm): this(pm, new DriverInterpreter()) {
			
		}
		
		public Process(ProcessManager pm, Interpreter initialIntepreter) {
			this.processManager = pm;
			this.currentInterpreter = initialIntepreter;
		}
		
		public virtual void Activate() {
			System.Diagnostics.Debug.WriteLine("Activating Interpreter of Process " + processManager.ToString());
			this.ProcessState = ProcessState.Running;
			currentInterpreter.Resume();
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

