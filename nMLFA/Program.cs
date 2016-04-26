using System;
using StringWatch;
using DataTypeSpace;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using StaticClasses;
using System.Threading;


namespace nMLFA
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			double[,] mat = new double[5, 5];
			for (int c = 0; c < 5; c++) {
				for (int c1 = 0; c1 < 5; c1++) {
					Console.Write ($"Enter the element [{c},{c1}] = ");
					mat [c, c1] = double.Parse (Console.ReadLine ()); 
				}
			}
			for (int c = 0; c < 5; c++) {
				for (int c1 = 0; c1 < 5; c1++) {
					Console.Write ($"\t{mat[c,c1]}");
				}
				Console.WriteLine ("");
			}

			Matrix theMat = new Matrix (mat,5, 5); 
			theMat.GaussJordan ();
			Console.WriteLine("After guass elimination = ");
			MatrixOutput.Print (theMat);
	    }     // END MAIN 

		public static void MLFA()
		{
			StringObserver s = new StringObserver();
			string theLine = "";
			while (theLine != "end") {
				theLine = Console.ReadLine ();
				if (theLine != "end") {
					s.setString (theLine);
					try{
						s.Process ();
					}
					catch{
						TheMessageHandler.MessagePrinter.Print ("Invalid Statment");
					}
					try {
						s.Printer ();}
					catch{
					}
				}
			}
			s.PrintEntireExpressionList ();	 
		}

	}
}
