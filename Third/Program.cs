using System;
using System.Collections.Generic;

namespace Third
{
	internal static class Program
	{
		private delegate uint CallbackFoo(uint value, IReadOnlyList<byte> sub);
		private static readonly byte[] Sub =
		{
			14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7
		};

		public static void Main()
		{
			CallbackFoo callbackFoo = Substitute;
			Core(callbackFoo);
		}

		private static void Core(CallbackFoo callbackFoo) =>
			Console.WriteLine(callbackFoo(8, Sub));
		
		private static uint Substitute(uint value, IReadOnlyList<byte> sub)
		{
			uint result = 0;
			for (var i = 0; i < 4; i++)
				result |= (uint)sub[(byte)(value >> (4 * i) & 0x0f)] << (4 * i);
			return result;
		}
	}
}