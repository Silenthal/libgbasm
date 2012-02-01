namespace LibGBasm
{
	public class LibGBasm
	{
		public static bool GetZ80Instruction(byte[] binaryFile, int virtualOffset, int fileOffset, ref InstructionUnit output)
		{
			output = new InstructionUnit();
			if (binaryFile == null || fileOffset > binaryFile.Length - 1) return false;
			byte inst = binaryFile[fileOffset];
			if (inst == 0xCB)
			{
				if (fileOffset >= binaryFile.Length - 2) return false;
				output = Z80Instructions.CBInstructionUnitTable[binaryFile[fileOffset]];
			}
			else output = Z80Instructions.InstructionUnitTable[binaryFile[fileOffset]];
			output.fileOffset = fileOffset;
			output.virtualOffset = fileOffset + virtualOffset;
			return true;
		}
	}
}