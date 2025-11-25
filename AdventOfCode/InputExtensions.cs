using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
	public static class InputExtensions
	{

		public static IEnumerable<string> ReadLines(this TextReader reader)
		{
			while (reader.ReadLine() is { } line)
			{
				yield return line;
			}
		}

		public static IEnumerable<int[]> ReadIntegerLines(this TextReader reader)
		{
			foreach (var line in reader.ReadLines())
			{
				var parts = line.Split(new[]
				{
					' ', '\t'
				}, StringSplitOptions.RemoveEmptyEntries);
				yield return parts.Select(int.Parse).ToArray();
			}
		}

	}
}
