﻿namespace LibGBasm
{
	/// <summary>
	/// Represents the type of instruction.
	/// </summary>
	public enum InstructionType { invalid, adc, add, and, bit, call, ccf, cb, cp, cpl, ei, daa, dec, di, halt, inc, jp, jr, ld, ldi, ldd, ldhl, nop, or, pop, push, res, ret, reti, rl, rla, rlc, rlca, rot, rr, rra, rrc, rrca, rst, sbc, scf, set, sla, sra, srl, stop, sub, swap, xor }

	/// <summary>
	/// The type of a GBArgument. Depending on the type of argument, the values within are interpreted differently.
	/// </summary>
	public enum ArgumentType { None, Byte, Word, Conditional, RegisterSingle, RegisterDouble }

	/// <summary>
	/// Represents a single register.
	/// </summary>
	public enum RegisterSingle { a, b, c, d, e, h, l }

	//Represents a register pair.
	public enum RegisterDouble { af, bc, de, hl, sp }

	/// <summary>
	/// Represents a conditional statement.
	/// </summary>
	public enum Conditional { nz, z, nc, c }

	/// <summary>
	/// Represents a single GB instruction.
	/// </summary>
	public struct GBInstructionUnit
	{
		/// <summary>
		/// The actual offset if the instruction in the binary.
		/// </summary>
		public int BinaryOffset;
		/// <summary>
		/// The address assigned to the beginning of the binary where the instruction originated.
		/// </summary>
		public int OrgOffset;
		/// <summary>
		/// The size of the argument, in bytes.
		/// </summary>
		public int InstSize;
		/// <summary>
		/// The type of instruction.
		/// </summary>
		public InstructionType InstType;
		/// <summary>
		/// The number of arguments to this instruction.
		/// </summary>
		public int ArgCount;
		/// <summary>
		/// The first argument of the instruction.
		/// </summary>
		public GBArgument Arg1;
		/// <summary>
		/// The second argument of the instruction.
		/// </summary>
		public GBArgument Arg2;
	}

	/// <summary>
	/// Represents an argument to a GB instruction.
	/// </summary>
	public struct GBArgument
	{
		/// <summary>
		/// The type of argument.
		/// </summary>
		public ArgumentType ArgType;
		/// <summary>
		/// The number value of the argument, if it has one.
		/// </summary>
		public int NumberArg;
		/// <summary>
		/// Represents whether the argument is referring to a location in the memory map.
		/// </summary>
		public bool IsMemMap;
		/// <summary>
		/// Represents whether the number argument is being interpreted as a location in the $FF00-$FFFF range.
		/// </summary>
		public bool IsFFNNInst;
		/// <summary>
		/// The RegisterSingle value of the argument, if it has one.
		/// </summary>
		public RegisterSingle RegSingleArg;
		/// <summary>
		/// The RegisterDouble value of the argument, if it has one.
		/// </summary>
		public RegisterDouble RegDoubleArg;
		/// <summary>
		/// The Conditional value of the argument, if it has one.
		/// </summary>
		public Conditional CondArg;
	}
}