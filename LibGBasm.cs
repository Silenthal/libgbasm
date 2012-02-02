namespace LibGBasm
{
	public class GBASM
	{
		public static bool GetGBInstruction(byte[] binaryFile, int virtualOffset, int fileOffset, ref InstructionUnit output)
		{
			output = new InstructionUnit();
			if (binaryFile == null || fileOffset > binaryFile.Length - 1) return false;
			byte inst = binaryFile[fileOffset];
			if (inst == 0xCB)
			{
				if (fileOffset >= binaryFile.Length - 2) return false;
				output = GBInstructions.CBInstructionUnitTable[binaryFile[fileOffset]];
			}
			else output = GBInstructions.InstructionUnitTable[binaryFile[fileOffset]];
			output.fileOffset = fileOffset;
			output.virtualOffset = fileOffset + virtualOffset;
			switch (output.instSize)
			{
				case 2:
					switch (output.arg1.argType)
					{
						case ArgumentType.Byte:
							output.arg1.numberArg = binaryFile[fileOffset + 1];
							break;
						default:
							break;
					}
					if (output.ArgCount == 2)
					{
						switch (output.arg2.argType)
						{
							case ArgumentType.Byte:
								output.arg2.numberArg = binaryFile[fileOffset + 1];
								break;
							default:
								break;
						}
					}
					break;
				case 3:
					if (output.arg1.argType == ArgumentType.Word)
					{
						output.arg1.numberArg = System.BitConverter.ToUInt16(binaryFile, fileOffset + 1);
					}
					else if (output.ArgCount == 2 && output.arg2.argType == ArgumentType.Word)
					{
						output.arg2.numberArg = System.BitConverter.ToUInt16(binaryFile, fileOffset + 1);
					}
					break;
				default:
					break;
			}
			return true;
		}
	}
}