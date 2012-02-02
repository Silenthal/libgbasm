namespace LibGBasm
{
	public class GBASM
	{
		public static bool GetGBInstruction(byte[] binaryFile, int virtualOffset, int fileOffset, ref GBInstructionUnit output)
		{
			output = new GBInstructionUnit();
			if (binaryFile == null || fileOffset > binaryFile.Length - 1) return false;
			byte inst = binaryFile[fileOffset];
			if (inst == 0xCB)
			{
				if (fileOffset >= binaryFile.Length - 2) return false;
				output = GBInstructions.CBInstructionUnitTable[binaryFile[fileOffset]];
			}
			else output = GBInstructions.InstructionUnitTable[binaryFile[fileOffset]];
			output.BinaryOffset = fileOffset;
			output.OrgOffset = fileOffset + virtualOffset;
			switch (output.InstSize)
			{
				case 2:
					switch (output.Arg1.ArgType)
					{
						case ArgumentType.Byte:
							output.Arg1.NumberArg = binaryFile[fileOffset + 1];
							break;
						default:
							break;
					}
					if (output.ArgCount == 2)
					{
						switch (output.Arg2.ArgType)
						{
							case ArgumentType.Byte:
								output.Arg2.NumberArg = binaryFile[fileOffset + 1];
								break;
							default:
								break;
						}
					}
					break;
				case 3:
					if (output.Arg1.ArgType == ArgumentType.Word)
					{
						output.Arg1.NumberArg = System.BitConverter.ToUInt16(binaryFile, fileOffset + 1);
					}
					else if (output.ArgCount == 2 && output.Arg2.ArgType == ArgumentType.Word)
					{
						output.Arg2.NumberArg = System.BitConverter.ToUInt16(binaryFile, fileOffset + 1);
					}
					break;
				default:
					break;
			}
			return true;
		}
	}
}