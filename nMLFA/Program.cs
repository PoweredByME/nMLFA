using System;
using StringWatch;
namespace nMLFA
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			StringObserver s = new StringObserver();
			string theLine = "";
			while (theLine != "end") {
				theLine = Console.ReadLine ();
				if (theLine != "end") {
					s.setString (theLine);
					//try{
						s.Process ();
					//}
					//catch{
					//	TheMessageHandler.MessagePrinter.Print ("Invalid Statment");
					//}
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
