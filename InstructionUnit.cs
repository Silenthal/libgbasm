using System.Collections.Generic;

namespace LibGBasm
{
	public enum InstructionType { adc, add, and, bit, call, ccf, cb, cp, cpl, ei, daa, data, dec, di, halt, inc, jp, jr, ld, ldi, ldd, ldhl, nop, or, pop, push, rla, rlc, rlca, res, ret, reti, rot, rl, rr, rra, rrc, rrca, rst, sbc, scf, set, shft, sla, sra, srl, stop, sub, swap, xor }
	public enum ArgumentType { None, MemMap, RegisterDoubleMemMap, RegisterSingleMemMap, Conditional, Register, RegisterPair, Byte, Word, Offset }
	public enum LabeledArgument { None, First, Second }
	public enum RegisterSingle { a, b, c, d, e, h, l }
	public enum RegisterDouble { af, bc, de, hl, sp }
	public enum Conditional { nz, z, nc, c }

	internal class InstructionTypes
	{
		public static Dictionary<InstructionType, string> instStringTranslationTable = new Dictionary<InstructionType, string>
		{
			{ InstructionType.adc, "adc" }, 
			{ InstructionType.add, "add" }, 
			{ InstructionType.and, "and" }, 
			{ InstructionType.bit, "bit" }, 
			{ InstructionType.call, "call" }, 
			{ InstructionType.cb, "cb" }, 
			{ InstructionType.ccf, "ccf" }, 
			{ InstructionType.cpl, "cpl" }, 
			{ InstructionType.daa, "daa" }, 
			{ InstructionType.dec, "dec" }, 
			{ InstructionType.di, "di" }, 
			{ InstructionType.ei, "ei" }, 
			{ InstructionType.halt, "halt" }, 
			{ InstructionType.inc, "inc" }, 
			{ InstructionType.jp, "jp" }, 
			{ InstructionType.jr, "jr" }, 
			{ InstructionType.ld, "ld" }, 
			{ InstructionType.nop, "nop" }, 
			{ InstructionType.or, "or" }, 
			{ InstructionType.pop, "pop" }, 
			{ InstructionType.push, "push" }, 
			{ InstructionType.res, "res" }, 
			{ InstructionType.ret, "ret" }, 
			{ InstructionType.rot, "rot" }, 
			{ InstructionType.rst, "rst" }, 
			{ InstructionType.sbc, "sbc" }, 
			{ InstructionType.scf, "scf" }, 
			{ InstructionType.set, "set" }, 
			{ InstructionType.shft, "shft" }, 
			{ InstructionType.cp, "special" }, 
			{ InstructionType.stop, "stop" }, 
			{ InstructionType.sub, "sub" }, 
			{ InstructionType.swap, "swap" }, 
			{ InstructionType.xor, "xor" }, 
		};
	}

	public struct InstructionUnit
	{
		public int fileOffset;
		public InstructionType instType;
		public int ArgCount;
		public LabeledArgument labeledArg;
		public Argument arg1;
		public Argument arg2;
		public int dataSize;
	}

	public struct Argument
	{
		public ArgumentType argType;
		public byte byteArg;
		public ushort shortArg;
		public int offsetArg;
		public string stringArg;
		public RegisterSingle rsArg;
		public RegisterDouble rdArg;
		public Conditional cArg;
	}
}
