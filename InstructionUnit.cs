namespace LibGBasm
{
	public enum InstructionType { invalid, adc, add, and, bit, call, ccf, cb, cp, cpl, ei, daa, dec, di, halt, inc, jp, jr, ld, ldi, ldd, ldhl, nop, or, pop, push, res, ret, reti, rl, rla, rlc, rlca, rot, rr, rra, rrc, rrca, rst, sbc, scf, set, sla, sra, srl, stop, sub, swap, xor }

	public enum ArgumentType { None, MemMap, RegisterDoubleMemMap, RegisterSingleMemMap, Conditional, Register, RegisterPair, Byte, MemMapFFByte, Word, Offset }

	public enum RegisterSingle { a, b, c, d, e, h, l }

	public enum RegisterDouble { af, bc, de, hl, sp }

	public enum Conditional { nz, z, nc, c }

	public struct InstructionUnit
	{
		public int fileOffset;
		public int virtualOffset;
		public int instSize;
		public InstructionType instType;
		public int ArgCount;
		public Argument arg1;
		public Argument arg2;
	}

	public struct Argument
	{
		public ArgumentType argType;
		public byte byteArg;
		public ushort shortArg;
		public RegisterSingle rsArg;
		public RegisterDouble rdArg;
		public Conditional cArg;
	}
}