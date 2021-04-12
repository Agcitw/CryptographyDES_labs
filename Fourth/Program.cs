using System;
using System.Collections.Generic;

namespace Fourth
{
	internal static class Program
	{
		public static void Main()
		{
			var test = new TestFeistelNet(100, 200, 7);
			test.Show();
			test.Encrypt();
			test.Show();
			test.Decrypt();
			test.Show();
		}
	}
	
	public abstract class FeistelNet
	{
		protected abstract int Left { get; set; }
		protected abstract int Right { get; set; }
		protected abstract int Rounds { get; set; }

		private readonly List<int> _keys = new List<int>();

		private int GenerateKey(int val1, int val2)
		{
			var key = SecretFunc(val1, val2);
			_keys.Add(key);
			return key;
		}
		
		private Func<int, int, int> SecretFunc { get; } 
			= (i, j) => (i + j) % 256;
		
		public void Encrypt()
		{
			var key = 1;
			for (var i = 0; i < Rounds; i++)
			{
				if (i < Rounds - 1)
				{
					var temp = Left;
					Left = Right ^ GenerateKey(Left, key);
					Right = temp;
				}
				else
				{
					Right ^= GenerateKey(Left, key);
				}
				key += 1;
			}
		}
		public void Decrypt()
		{
			for (var i = 0; i < Rounds; i++)
			{
				if (i < Rounds - 1)
				{
					var temp = Left;
					Left = Right ^ _keys[i];
					Right = temp;
				}
				else
				{
					Right ^= _keys[i];
				}
			}
		}
		public void Show()
		{
			Console.WriteLine(Left + " " + Right);
		}
	}

	internal sealed class TestFeistelNet : FeistelNet
	{
		protected override int Left { get; set; }
		protected override int Right { get; set; }
		protected override int Rounds { get; set; }
		
		public TestFeistelNet(int left, int right, int rounds)
		{
			Left = left;
			Right = right;
			Rounds = rounds;
		}
	}
}