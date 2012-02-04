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
		/// <param name="BinaryFile">The file to read from.</param>
		/// <param name="Org">The origin address of the file.</param>
		/// <param name="Offset">The offset into the file to read from.</param>
		/// <param name="OutInstruction">The GBInstruction to write the information to.</param>
		/// <returns>The success of the operation. Returns false if the information fetching went wrong for any reason
		/// </returns>
		public static bool GetGBInstruction(byte[] BinaryFile, int Org, int Offset, ref GBInstruction OutInstruction)
		{
			OutInstruction = new GBInstruction();
			if (BinaryFile == null || Offset > BinaryFile.Length - 1) return false;
			byte inst = BinaryFile[Offset];
			if (inst == 0xCB)
			{
				if (Offset > BinaryFile.Length - 2) return false;
				OutInstruction = GBInstructions.CBInstructionUnitTable[BinaryFile[Offset + 1]];
			}
			else OutInstruction = GBInstructions.InstructionUnitTable[BinaryFile[Offset]];
			OutInstruction.Offset = Offset;
			OutInstruction.Address = Offset + Org;
			if (OutInstruction.Offset + OutInstruction.InstSize > BinaryFile.Length - 1) return false;
			if (OutInstruction.InstSize == 2)
			{
				if (OutInstruction.Arg1.ArgType == GBArgumentType.Byte)
				{
					OutInstruction.Arg1.NumberArg = BinaryFile[Offset + 1];
				}
				else if (OutInstruction.ArgCount == 2 && OutInstruction.Arg2.ArgType == GBArgumentType.Byte)
				{
					OutInstruction.Arg2.NumberArg = BinaryFile[Offset + 1];
				}
			}
			else if (OutInstruction.InstSize == 3)
			{
				if (OutInstruction.Arg1.ArgType == GBArgumentType.Word)
				{
					OutInstruction.Arg1.NumberArg = System.BitConverter.ToUInt16(BinaryFile, Offset + 1);
				}
				else if (OutInstruction.ArgCount == 2 && OutInstruction.Arg2.ArgType == GBArgumentType.Word)
				{
					OutInstruction.Arg2.NumberArg = System.BitConverter.ToUInt16(BinaryFile, Offset + 1);
				}
			}
			return true;
		}
	}
}