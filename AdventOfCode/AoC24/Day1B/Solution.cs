using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.AoC24.Day1B
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

			var count = rights.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
			var total = 0;
			foreach (var left in lefts)
			{
				if (count.TryGetValue(left, out var c))
				{
					total += left * c;
				}
			}
			output.WriteLine(total);
		}
	}
}
