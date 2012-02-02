namespace LibGBasm
{
	public class GBASM
	{
		/// <summary>
		/// Given a binary file and the appropriate offsets, constructs a GBInstructionUnit containing information about the instruction and its arguments.
		/// </summary>
		/// <param name="BinaryFile">The file to read from.</param>
		/// <param name="OrgOffset">The origin address of the file.</param>
		/// <param name="RealOffset">The offset into the file to read from.</param>
		/// <param name="output">The GBInstructionUnit to write the information to.</param>
		/// <returns>The success of the operation. Returns false if the information fetching went wrong for any reason.</returns>
		public static bool GetGBInstruction(byte[] BinaryFile, int OrgOffset, int RealOffset, ref GBInstructionUnit output)
		{
			output = new GBInstructionUnit();
			if (BinaryFile == null || RealOffset > BinaryFile.Length - 1) return false;
			byte inst = BinaryFile[RealOffset];
			if (inst == 0xCB)
			{
				if (RealOffset > BinaryFile.Length - 2) return false;
				output = GBInstructions.CBInstructionUnitTable[BinaryFile[RealOffset]];
			}
			else output = GBInstructions.InstructionUnitTable[BinaryFile[RealOffset]];
			output.Offset = RealOffset;
			output.Address = RealOffset + OrgOffset;
			if (output.Offset + output.InstSize > BinaryFile.Length - 1) return false;
			if (output.InstSize == 2)
			{
				if (output.Arg1.ArgType == GBArgumentType.Byte)
				{
					output.Arg1.NumberArg = BinaryFile[RealOffset + 1];
				}
				else if (output.ArgCount == 2 && output.Arg2.ArgType == GBArgumentType.Byte)
				{
					output.Arg2.NumberArg = BinaryFile[RealOffset + 1];
				}
			}
			else if (output.InstSize == 3)
			{
				if (output.Arg1.ArgType == GBArgumentType.Word)
				{
					output.Arg1.NumberArg = System.BitConverter.ToUInt16(BinaryFile, RealOffset + 1);
				}
				else if (output.ArgCount == 2 && output.Arg2.ArgType == GBArgumentType.Word)
				{
					output.Arg2.NumberArg = System.BitConverter.ToUInt16(BinaryFile, RealOffset + 1);
				}
			}
			return true;
		}
	}
}