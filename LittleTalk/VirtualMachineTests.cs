using System;
using NUnit.Framework;

namespace LittleTalk {
	public class MockProcess: Process {
		public Interpreter i;
		
		public MockProcess() { }
		
		public override void ChangeIntepreter(Interpreter i) {
			this.i = i;
		}
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
		
		[Test()]
		public void Test() {
			var p = new MockProcess();
			var i1 = new Interpreter(p, null);
			var i2 = new Interpreter(p, i1);
			var val = new NativeInteger(32);
			// i2.ops.Add();
			i2.ops.Add(new Return());
			i2.Resume();
			Assert.AreEqual(val, i1.stack.Peek()); 
		}
	}
}