using System;
using System.Collections.Generic;

namespace Second
{
	internal static class Program
	{
		private static readonly byte[] Sub =
		{
			14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7
		};

		public static void Main()
		{
			Console.WriteLine(Substitute(8, Sub)); 
			  //dec: 8 -> bin: 0000_0000_0000_1000 ->
			 //ind: 0, 0, 0, 8 -> Sub[ind]: 14, 14, 14, 3 ->
			//bin: 1110_1110_1110_0011 -> dec: 61155
		}
		
		private static uint Substitute(uint value, IReadOnlyList<byte> sub)
		{
			uint result = 0;
			for (var i = 0; i < 4; i++)
				result |= (uint)sub[(byte)(value >> (4 * i) & 0x0f)] << (4 * i);
			return result;
		}
	}
}