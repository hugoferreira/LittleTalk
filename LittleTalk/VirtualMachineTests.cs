using System;
using System.Linq;
using System.Linq.Expressions;
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
		public void TestInterpreterChain ()
		{
			var p = new MockProcess ();
			var i1 = new Interpreter (p, null);
			var i2 = new Interpreter (p, i1);
			
			i2.ops.Add (new LoadConstant (anInteger));
			i2.ops.Add (new Return ());
			i2.Resume ();
			
			Assert.AreEqual (i1, p.i);
			
			var peek = i1.stack.Peek (); 
			Assert.AreEqual (typeof(NativeInteger), peek.GetType ()); 
			Assert.AreEqual (anInteger, ((NativeInteger)peek).Value);
		}
		
		[Test()]
		public void TestInterpreterSumLambda ()
		{
			var p = new MockProcess ();
			var i = new Interpreter (p, null);

			for (int x = 0; x < 1000; x++) {
				i.ops.Add (new LoadConstant (2));
				i.ops.Add (new LoadConstant (3));
				i.ops.Add (new SumLambda ());
				i.Resume ();
				i.stack.Pop ();
			}

			var peek = i.stack.Peek (); 
			Assert.AreEqual (typeof(NativeInteger), peek.GetType ()); 
			Assert.AreEqual (5, ((NativeInteger)peek).Value);
		}

		class C {	
			public string A{ get; set; }
			public string B{ get; set; }
		}
		
		
		[Test()]
		public void TestExpressionManipulation ()
		{
			var arg0 = Expression.Parameter (typeof(C), "arg");
			var arg1 = Expression.Parameter (typeof(int), "x");
			
			var body = Expression.Equal (Expression.Property (arg0, "A"), Expression.Constant ("One"));
			body = Expression.And (Expression.Equal (Expression.Property (arg0, "B"), Expression.Constant ("Two")), body);
			var lambda = Expression.Lambda<Func<C, bool>> (body, arg0).Compile ();

			Expression<Func<int, bool>> lambda3 = (x => x > 10);
			var lambda2 = Expression.Lambda<Func<int, bool>> (lambda3.Body, lambda3.Parameters.First ()).Compile ();
			
			Assert.IsTrue (lambda (new C (){A="One", B="Two"}));
			Assert.IsTrue (lambda2 (15));
		}
	}
}