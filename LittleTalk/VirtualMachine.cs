using System;
using System.Collections.Generic;

namespace LittleTalk {
	public class Context {
		// receiver
		// arguments
		// temps
	}

	public class Interpreter {
		public Interpreter sender, creator;
		public Object receiver, context, literals;
		public Stack<object> stack;
		public List<Op> ops;
		public int opPointer = 0;
			
		public Interpreter(Interpreter sender) {
			this.sender = sender;
			this.stack = new Stack<object>();
			this.ops = new List<Op>();
		}
		
		public void Resume() {
			while(true) {
				ops[opPointer++].Execute(this);
			}
		}
	}
	
	public abstract class Op {
		public virtual void Execute(Interpreter ctx) { }
	}
	
	public class LoadObjectVar: Op {
		public int varIndex = 0;
		
		public override void Execute (Interpreter ctx) {
			ctx.stack.Push(ctx.receiver.Variables[varIndex]);
		}
	}
	
	public class StoreObjectVar: Op {
	}
	
	public class LoadLocalVar: Op {
		public int varIndex = 0;
		
		public override void Execute (Interpreter ctx) {
			ctx.stack.Push(ctx.context.Variables[varIndex]);
		}
	}

	public class StoreLocalVar: Op {
	}
	
	public class LoadLiteral: Op {
		public int varIndex = 0;
		
		public override void Execute (Interpreter ctx) {
			ctx.stack.Push(ctx.literals.Variables[varIndex]);
		}
	}
	
	public class LoadClass: Op {
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
	}
	
	public class Return: Op {
	}
	
	public class InnerReturn: Op {
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
	}
}

