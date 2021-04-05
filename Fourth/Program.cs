using System;

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

	internal abstract class FeistelNet
	{
		public abstract int Left { get; set; }
		public abstract int Right { get; set; }
		public abstract int Rounds { get; set; }
		public abstract Func<int, int, int> SecretFunc { get; set; }
		public abstract void Encrypt();
		public abstract void Decrypt();
	}

	internal sealed class TestFeistelNet : FeistelNet
	{
		public override int Left { get; set; }
		public override int Right { get; set; }
		public override int Rounds { get; set; }
		public override Func<int, int, int> SecretFunc { get; set; } 
			= (i, j) => (i + j) % 256;

		public TestFeistelNet(int left, int right, int rounds)
		{
			Left = left;
			Right = right;
			Rounds = rounds;
		}

		public override void Encrypt()
		{
			var round = 1;
			for (var i = 0; i < Rounds; i++)
			{
				if (i < Rounds - 1)
				{
					var temp = Left;
					Left = Right ^ SecretFunc(Left, round);
					Right = temp;
				}
				else
				{
					Right ^= SecretFunc(Left, round);
				}
				round += 1;
			}
		}
		public override void Decrypt()
		{
			var round = Rounds;
			for (var i = 0; i < Rounds; i++)
			{
				if (i < Rounds - 1)
				{
					var temp = Left;
					Left = Right ^ SecretFunc(Left, round);
					Right = temp;
				}
				else
				{
					Right ^= SecretFunc(Left, round);
				}
				round -= 1;
			}
		}
		public void Show()
		{
			Console.WriteLine(Left + " " + Right);
		}
	}
}