using System;
using Fourth;

namespace Fifth
{
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var testDes = new Des(150, 250);
			testDes.Encrypt();
			testDes.Show();
			testDes.Decrypt();
			testDes.Show();
		}
	}

	internal class Des : FeistelNet
	{
		protected sealed override int Left { get; set; }
		protected sealed override int Right { get; set; }
		protected override int Rounds { get; set; } = 16;
		private readonly int[] _roundKeys = {1, 12, 3, 0, 11, 14, 15, 10, 2, 5, 7, 6, 8, 9, 4, 13};

		public Des(int left, int right)
		{
			Left = left;
			Right = right;
		}
	}
}