using System;
using System.Collections.Generic;
using System.Linq;
	
namespace LittleTalk {
	class MainClass {
		public static void Main (string[] args) {
			Console.Clear();
			Console.WriteLine ("Welcome to LittleTalk v0.1a");
			var pm = new ProcessManager();
			pm.Processes.Add(new DriverProcess());
			pm.Start();
		}
	}
}
