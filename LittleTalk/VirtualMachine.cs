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
		public Stack<Object> stack;
		public List<Op> ops;
			
		public Interpreter(Interpreter sender) {
			this.sender = sender;
			this.context = new Object();
			this.stack = new Stack<Object>();
			this.ops = new List<Op>();
		}
		
		public void Resume() {
			ops.ForEach(op => op.Execute(this));
		}
	}
	
	public abstract class Op {
		public Action<Interpreter> action;
		public virtual void Execute(Interpreter ctx) { action(ctx); }
	}
		
	public class LoadInstanceVar: Op {
		public LoadInstanceVar(string token){ 
			action = (ctx => ctx.stack.Push(ctx.receiver.Variables[token]));
		}
	}
	
	public class LoadConstant: Op {
		public LoadConstant(int k) {
			action = (ctx => ctx.stack.Push(new NativeInteger(k)));			
		}
	}
		
	public class StoreInstanceVar: Op {
		public StoreInstanceVar(string token) { 
			action = (ctx => ctx.receiver.Variables[token] = ctx.stack.Pop());
		}		
	}
	
	public class LoadLocalVar: Op {
		public LoadLocalVar(string token) { 
			action = (ctx => ctx.stack.Push(ctx.context.Variables[token]));
		}
	}

	public class StoreLocalVar: Op {
		public StoreLocalVar(string token) { 
			action = (ctx => ctx.context.Variables[token] = ctx.stack.Pop());
		}
	}
	
	public class LoadLiteral: Op {
		public LoadLiteral(string token) { 
			action = (ctx => ctx.stack.Push(ctx.literals.Variables[token]));
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

