using System;
using NUnit.Framework;

namespace LittleTalk {
	public class MockProcess: Process {
		public MockProcess() { }
	}
	
	[TestFixture()]
	public class VirtualMachineTests {
		[Test()]
		public void TestLoadStore () {
			var p = new MockProcess();
			var i = new Interpreter(p, null);
			i.ops.Add(new LoadConstant(42));
			i.ops.Add(new StoreLocalVar("temp"));
			i.ops.Add(new LoadLocalVar("temp"));
			i.Resume();
			
			Assert.AreEqual(typeof(NativeInteger), i.stack.Peek().GetType());
			Assert.AreEqual(42, ((NativeInteger) i.stack.Peek()).Value);
		}
	}
}