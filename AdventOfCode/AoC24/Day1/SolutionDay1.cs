using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.AoC24.Day1
{
	public class Solution : ProblemBase
	{
		public override void Solve(TextReader input, TextWriter output)
		{
			var lefts = new List<int>();
			var rights = new List<int>();

			foreach (var line in input.ReadIntegerLines())
			{
				lefts.Add(line[0]);
				rights.Add(line[1]);
			}

			var total = 0;
			foreach (var pair in lefts.Order().Zip(rights.Order()))
			{
				var d = Math.Abs(pair.First - pair.Second);
				total += d;
			}
			output.WriteLine(total);
		}
	}
}
