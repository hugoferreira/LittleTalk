using System;
using System.Linq;
using System.Collections.Generic;

namespace LittleTalk {
	public class Context {
		// receiver
		// arguments
		// temps
	}

	public class Interpreter {
		public Process process;
		public Interpreter sender, creator;
		public Object receiver, context, literals;
		public Stack<Object> stack;
		public List<Op> ops;

		public Interpreter (Process p,Interpreter sender) {
			this.process = p;
			this.sender = sender;
			this.context = new Object ();
			this.stack = new Stack<Object> ();
			this.ops = new List<Op> ();
		}

		public virtual void Resume () {
			ops.ForEach (op => op.Execute (this));
			new Return ().Execute (this);
		}

		public void Return (Object o) {
			if (sender != null) {
				sender.stack.Push (o);
				process.ChangeIntepreter (sender);
			}
		}
	}

	public class DriverInterpreter: Interpreter {
		public DriverInterpreter (Process p): base(p, null) {
		}

		public override void Resume () {
			Console.Write (ToString () + "> ");
			var input = Console.ReadLine ().Trim ();
			if (input.Count () > 0) {
				var interpreter = new Interpreter (process, this);
				try {
					interpreter.ops.AddRange (Parse (input));
					process.ChangeIntepreter (interpreter);
					interpreter.Resume ();
					if (interpreter.stack.Count () > 0)
						Console.WriteLine (interpreter.stack.Peek ());
				} catch (InvalidOperationException) {
					Console.WriteLine ("Syntax Error.");
				}
			}
		}

		private List<Op> Parse (string code) {
			var ops = new List<Op> ();
			int i;

			if (code.First () == '#')
				ops.Add (new LoadLiteral (code.Skip (1).ToString ()));
			else if (int.TryParse (code, out i))
				ops.Add (new LoadConstant (i));
			else
				throw new InvalidOperationException ();
			return ops;
		}
	}	

	public abstract class Op {
		public Action<Interpreter> action;
		protected Op (Action<Interpreter> action)
		{
			this.action = action;
		} 
		
		public virtual void Execute (Interpreter ctx) {
			action (ctx);
		}
	}

	public class LoadInstanceVar: Op {
		public LoadInstanceVar (string token): base(ctx => ctx.stack.Push (ctx.receiver.Variables [token])) {}
	}

	public class LoadConstant: Op {
		public LoadConstant (int k): base(ctx => ctx.stack.Push (new NativeInteger (k))) { }
	}
	
	public class SumLambda: Op {
		public SumLambda (): base(ctx => ctx.stack.Push ((NativeInteger)ctx.stack.Pop () + (NativeInteger)ctx.stack.Pop ())) { }
	}
	
	public class StoreInstanceVar: Op {
		public StoreInstanceVar (string token):base(ctx => ctx.receiver.Variables [token] = ctx.stack.Pop ()) { }		
	}

	public class LoadLocalVar: Op {
		public LoadLocalVar (string token):base(ctx => ctx.stack.Push (ctx.context.Variables [token])) { }
	}

	public class StoreLocalVar: Op {
		public StoreLocalVar (string token):base(ctx => ctx.context.Variables [token] = ctx.stack.Pop ()) { }
	}

	public class LoadLiteral: Op {
		public LoadLiteral (string token):base(ctx => ctx.stack.Push (ctx.literals.Variables [token])) { }
	}

	/* public class LoadClass: Op {
	}

	public class Send: Op {	
	}

	public class SendSuper: Op {
	}

	public class CreateBlock: Op {
	}

	public class Pop: Op {
	}

	public class Dup: Op {
	} */

	public class Return: Op {
		public Return (): base(ctx => ctx.Return (ctx.stack.Peek ())) { }
	}

	/* public class InnerReturn: Op {
	}

	public class ReturnReceiver: Op {
	}

	public class JumpTrueNil: Op {
	}

	public class JumpTrue: Op {
	}

	public class JumpFalseNil: Op {
	}

	public class JumpFalse: Op {
	}

	public class Jump: Op {
	}

	public class Retrocede: Op {
	}

	public class Primitive: Op {
	} */
}