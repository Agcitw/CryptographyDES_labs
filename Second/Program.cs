using System;
using System.Collections.Generic;

namespace Second
{
	internal static class Program
	{
		private static readonly int[][] Sub =
		{
			new[] {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
			new[] {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
			new[] {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
			new[] {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
		};

		public static void Main()
		{
			Console.WriteLine(Substitute(0, Sub));
		}
		
		private static uint Substitute(uint value, IReadOnlyList<int[]> sub)
		{
			uint result = 0;
			for (var i = 0; i < 4; i++)
			{
				var index = (byte)(value >> (4 * i) & 0x0f);
				var sBlock = sub[i][index];
				result |= (uint)sBlock << (4 * i);
			}
			return result;
		}
	}
}