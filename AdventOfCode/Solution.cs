namespace AdventOfCode
{
	public abstract class ProblemBase
	{

		public override string ToString()
		{
			var (set, name) = ParseProblemName(GetType().FullName);
			return $"{set}.{name}";
		}

		public abstract void Solve(TextReader input, TextWriter output);


		public static (string, string) ParseProblemName(string typeName)
		{
			var parts = typeName.Split('.');
			return (parts[^3], parts[^2]);
		}

	}
}
