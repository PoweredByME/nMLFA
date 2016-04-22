using System;

namespace allSolverInterface
{
	public interface Solver
	{
	    bool isProcessed();
		DataTypeSpace.Expression getSolution(); 
	}
}

