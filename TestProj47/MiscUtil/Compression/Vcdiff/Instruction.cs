namespace HSNXT.MiscUtil.Compression.Vcdiff
{
	/// <summary>
	/// Contains the information for a single instruction
	/// </summary>
	internal struct Instruction
	{
		private readonly InstructionType type;
		internal InstructionType Type
		{
			get { return type; }
		}

		private readonly byte size;
		internal byte Size
		{
			get { return size; }
		}

		private readonly byte mode;
		internal byte Mode
		{
			get { return mode; }
		}

		internal Instruction(InstructionType type, byte size, byte mode)
		{
			this.type = type;
			this.size = size;
			this.mode = mode;
		}


	}
}

