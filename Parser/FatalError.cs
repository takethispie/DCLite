using System;

namespace DCLite 
{
	public class FatalError: Exception {
		public FatalError(string m): base(m) {}
	}
}