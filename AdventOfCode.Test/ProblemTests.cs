using System;
using System.Collections.Generic;
using System.Text;
using NFluent;

namespace AdventOfCode.Test
{
	public class ProblemTests
	{

		[Test(), MethodDataSource(nameof(AllProblems))]
		public async Task SolveSample(ProblemReference reference)
		{
			using var inputReader = new StreamReader(reference.OpenStream("SampleInput.txt"));
			using var outputReader = new StreamReader(reference.OpenStream("SampleOutput.txt"));
			var writer = new StringWriter();

			var problem = reference.Instantiate();
			try
			{
				problem.Solve(inputReader, writer);
				await writer.FlushAsync();

				var mySolution = new StringReader(writer.ToString().Trim());
				CompareReaders(mySolution, outputReader);

				Console.WriteLine("Pass!");
			}
			catch
			{
				await writer.FlushAsync();
				Console.WriteLine();
				Console.WriteLine("**********************");
				Console.WriteLine("Output before exception");
				Console.WriteLine("**********************");
				Console.WriteLine(writer.ToString());
				throw;
			}
		}

		private void CompareReaders(TextReader actual, TextReader expected)
		{
			var actualLines = actual.ReadLines().ToList();
			var expectedLines = expected.ReadLines().ToList();
			for (var line = 0; line < Math.Max(actualLines.Count, expectedLines.Count); line++)
			{
				if (line >= actualLines.Count)
				{
					throw new InvalidOperationException($"Expected answer has more lines ({expectedLines.Count}) than actual answer ({actualLines.Count})");
				}
				if (line >= expectedLines.Count)
				{
					throw new InvalidOperationException($"Expected answer has less lines ({expectedLines.Count}) than actual answer ({actualLines.Count})");
				}
				try
				{
					Check.That(actualLines[line]).IsEqualIgnoringCase(expectedLines[line]);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException($"Line {line + 1}: " + ex.Message, ex);
				}
			}


		}

		public static IEnumerable<Func<ProblemReference>> AllProblems()
		{
			var assembly = typeof(ProblemBase).Assembly;
			foreach (var type in assembly.ExportedTypes)
			{
				if (type.IsSubclassOf(typeof(ProblemBase)))
				{
					yield return () => new ProblemReference(type);
				}
			}
		}


	}

	public record class ProblemReference(Type Type)
	{
		public override string ToString()
		{
			var (set, name) = ProblemBase.ParseProblemName(Type.FullName);
			return $"{set}.{name}";
		}

		public Stream OpenStream(string name) => Type.Assembly.GetManifestResourceStream($"{Type.Namespace}.{name}") ?? throw new InvalidOperationException($"");

		public ProblemBase Instantiate()
		{
			var constructor = Type.GetConstructor(Type.EmptyTypes);
			Check.That(constructor).IsNotNull();
			return (ProblemBase)constructor!.Invoke(null);
		}

	}
}
