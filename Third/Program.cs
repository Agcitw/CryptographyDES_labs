// 2. Реализовать функцию substitute, принимающую целочисленное
// 	значение n и правило замены группы из 4 бит на другую группу из 4 бит
// 	(в виде массива sub, где sub[i] есть результат замены значения i).
// 	Результатом функции должно быть значение n, в которой каждая группа
// 	из 4 бит (0-3 биты, 4-7 биты, …) заменена в соответствии с переданным
// правилом.
// 3. Модифицируйте код из задания 2 таким образом, чтобы правило
// 	замены передавалось на вход функции в виде функции обратного
// вызова.

using System;

namespace Third
{
	internal static class Program
	{
		private static readonly Func<byte, byte> CallBackFunc = i => Sub[i];
		private static readonly byte[] Sub = 
			{ 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 };

		public static void Main()
		{
			Console.WriteLine(Substitute(8, CallBackFunc));
		}
		
		private static uint Substitute(uint value, Func<byte, byte> callback)
		{
			uint result = 0;
			for (var i = 0; i < 4; i++)
				result |= (uint)callback((byte)(value >> (4 * i) & 0x0f)) << (4 * i);
			return result;
		}
	}
}