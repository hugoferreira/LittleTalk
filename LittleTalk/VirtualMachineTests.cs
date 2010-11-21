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
		const int anInteger = 32;
		
		[Test()]
		public void TestLoadStore () {
			var p = new MockProcess();
			var i = new Interpreter(p, null);
			i.ops.Add(new LoadConstant(anInteger));
			i.ops.Add(new StoreLocalVar("temp"));
			i.ops.Add(new LoadLocalVar("temp"));
			i.Resume();
			
			Assert.AreEqual(typeof(NativeInteger), i.stack.Peek().GetType());
			Assert.AreEqual(anInteger, ((NativeInteger) i.stack.Peek()).Value);
		}
		
		[Test()]
		public void TestInterpreterChain() {
			var p = new MockProcess();
			var i1 = new Interpreter(p, null);
			var i2 = new Interpreter(p, i1);
			
			i2.ops.Add(new LoadConstant(anInteger));
			i2.ops.Add(new Return());
			i2.Resume();
			
			Assert.AreEqual(i1, p.i);
			
			var peek = i1.stack.Peek(); 
			Assert.AreEqual(typeof(NativeInteger), peek.GetType()); 
			Assert.AreEqual(anInteger, ((NativeInteger) peek).Value);
		}
	}
}