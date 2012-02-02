namespace LibGBasm
{
	public class GBASM
	{
		/// <summary>
		/// Given a binary file and the appropriate offsets, constructs a GBInstructionUnit containing information about the instruction and its arguments.
		/// </summary>
		/// <param name="BinaryFile">The file to read from.</param>
		/// <param name="OrgOffset">The origin address of the file.</param>
		/// <param name="BinaryOffset">The offset into the file to read from.</param>
		/// <param name="output">The GBInstructionUnit to write the information to.</param>
		/// <returns>The success of the operation. Returns false if the information fetching went wrong for any reason.</returns>
		public static bool GetGBInstruction(byte[] BinaryFile, int OrgOffset, int BinaryOffset, ref GBInstructionUnit output)
		{
			output = new GBInstructionUnit();
			if (BinaryFile == null || BinaryOffset > BinaryFile.Length - 1) return false;
			byte inst = BinaryFile[BinaryOffset];
			if (inst == 0xCB)
			{
				if (BinaryOffset > BinaryFile.Length - 2) return false;
				output = GBInstructions.CBInstructionUnitTable[BinaryFile[BinaryOffset]];
			}
			else output = GBInstructions.InstructionUnitTable[BinaryFile[BinaryOffset]];
			output.BinaryOffset = BinaryOffset;
			output.OrgOffset = BinaryOffset + OrgOffset;
			if (output.BinaryOffset + output.InstSize > BinaryFile.Length - 1) return false;
			if (output.InstSize == 2)
			{
				if (output.Arg1.ArgType == GBArgumentType.Byte)
				{
					output.Arg1.NumberArg = BinaryFile[BinaryOffset + 1];
				}
				else if (output.ArgCount == 2 && output.Arg2.ArgType == GBArgumentType.Byte)
				{
					output.Arg2.NumberArg = BinaryFile[BinaryOffset + 1];
				}
			}
			else if (output.InstSize == 3)
			{
				if (output.Arg1.ArgType == GBArgumentType.Word)
				{
					output.Arg1.NumberArg = System.BitConverter.ToUInt16(BinaryFile, BinaryOffset + 1);
				}
				else if (output.ArgCount == 2 && output.Arg2.ArgType == GBArgumentType.Word)
				{
					output.Arg2.NumberArg = System.BitConverter.ToUInt16(BinaryFile, BinaryOffset + 1);
				}
			}
			return true;
		}
	}
}