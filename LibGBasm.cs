//Copyright © 2012 Silenthal

//This file is part of LibGBasm.
//LibGBasm is free software: you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//LibGBasm is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.

//You should have received a copy of the GNU Lesser General Public License
//along with LibGBasm.  If not, see <http://www.gnu.org/licenses/>.
namespace LibGBasm
{
	public class GBASM
	{
		/// <summary>
		/// Given a binary file and the appropriate offsets, constructs a GBInstructionUnit containing information about the instruction and its arguments.
		/// </summary>
		/// <param name="baseFile">The file to read from.</param>
		/// <param name="org">The origin address of the file.</param>
		/// <param name="offset">The offset into the file to read from.</param>
		/// <param name="outputInstruction">The GBInstruction to write the information to.</param>
		/// <returns>The success of the operation. Returns false if the information fetching went wrong for any reason
		/// </returns>
		public static bool GetGBInstruction(byte[] baseFile, int org, int offset, ref GBInstruction outputInstruction)
		{
			outputInstruction = new GBInstruction();
			if (baseFile == null || offset > baseFile.Length - 1) return false;
			byte inst = baseFile[offset];
			if (inst == 0xCB)
			{
				if (offset > baseFile.Length - 2) return false;
				outputInstruction = GBInstructions.CBInstructionUnitTable[baseFile[offset + 1]];
			}
			else outputInstruction = GBInstructions.InstructionUnitTable[baseFile[offset]];
			outputInstruction.Offset = offset;
			outputInstruction.Address = offset + org;
			if (outputInstruction.Offset + outputInstruction.InstSize > baseFile.Length - 1) return false;
			if (outputInstruction.InstSize == 1 && outputInstruction.InstType == InstructionType.db)
			{
				outputInstruction.ArgCount = 1;
				outputInstruction.Arg1.ArgType = GBArgumentType.Byte;
				outputInstruction.Arg1.NumberArg = baseFile[offset];
			}
			else if (outputInstruction.InstSize == 2)
			{
				if (outputInstruction.ArgCount == 1)
				{
					if (outputInstruction.Arg1.ArgType == GBArgumentType.Byte ||
						outputInstruction.Arg1.ArgType == GBArgumentType.MemMapByte)
					{
						outputInstruction.Arg1.NumberArg = baseFile[offset + 1];
					}
				}
				else if (outputInstruction.ArgCount == 2)
				{
					if (outputInstruction.Arg2.ArgType == GBArgumentType.Byte ||
						outputInstruction.Arg2.ArgType == GBArgumentType.MemMapByte)
					{
						outputInstruction.Arg2.NumberArg = baseFile[offset + 1];
					}
				}
			}
			else if (outputInstruction.InstSize == 3)
			{
				if (outputInstruction.ArgCount == 1)
				{
					if (outputInstruction.Arg1.ArgType == GBArgumentType.Word ||
						outputInstruction.Arg1.ArgType == GBArgumentType.MemMapWord)
					{
						outputInstruction.Arg1.NumberArg = System.BitConverter.ToUInt16(baseFile, offset + 1);
					}
				}
				else if (outputInstruction.ArgCount == 2)
				{
					if (outputInstruction.Arg2.ArgType == GBArgumentType.Word ||
						outputInstruction.Arg2.ArgType == GBArgumentType.MemMapWord)
					{
						outputInstruction.Arg2.NumberArg = System.BitConverter.ToUInt16(baseFile, offset + 1);
					}
				}
			}
			return true;
		}
	}
}