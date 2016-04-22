using System;
using DataTypeSpace;
using System.Collections.Generic;

namespace HelperClasses
{
	/////////////////////////////////////////////////////////////////////////////
	///        AXILLARY CLASSES   ///////////////////////////////////////////////

	 /* these classes help in understanding the string and the stuff like a matrix making etc */


	static class Processes
	{
		static Matrix resultMatrix;
		static string theString;
		static List<string> theList;
		public static Matrix MatrixMaker(string exp)
		{
			theList = new List<string> ();
			theString = new string (exp.ToCharArray());
			MatrixProcessor ();
			return resultMatrix;
		}

		//_________________________________________________________________________________________________________________//
		static void MatrixProcessor()     // function that makes the matrix
		{
			string dumy = "";
			foreach (char c in theString) {
				if (c == ';') {
					if (!string.IsNullOrWhiteSpace (dumy)) {
						dumy = dumy.Trim ();
						theList.Add (dumy);
						dumy = "";
					} else {
						dumy = "";
					}
					theList.Add (c.ToString ());
				}
				if (c == ' ') {
					if (!string.IsNullOrWhiteSpace (dumy)) {
						dumy = dumy.Trim ();
						theList.Add (dumy);
						dumy = "";
					} else {
						dumy = "";
					}
				}
				else if(StaticClasses.Checker.isNumeric(c)) {
					dumy += c.ToString();
				}
			}
			if (!string.IsNullOrWhiteSpace (dumy)) {
				dumy = dumy.Trim ();
				theList.Add (dumy);
				dumy = "";
			}
				

			bool alreadyPrinted = false;
			int rows = 0, columns = 0; 
			int greatest = 0;
			foreach (string num in theList) {  //foreach loop to set the number of rows and columns
				if (num == ";") {
					if (greatest < columns) {
						greatest = columns;
						if (!alreadyPrinted) {
							TheMessageHandler.MessagePrinter.Print( "Alert!!! There were some postions in the matrix that you left undefined. So, I have set them to zero.");  
							alreadyPrinted = true;
						}
					} 
					columns = 0;
					rows++;
				} else
					columns++;
			}// end foreach loop.
			if (columns > greatest)
				greatest = columns;
			if (greatest == 0)
				greatest = columns;  // end of logic to set the matrix number of rows and columns

			Matrix theMatrix = new Matrix (rows+1, greatest); //creation of the matrix.
			theMatrix.Zero ();   //make the matrix zero


			rows = 0; columns = 0;    //reinitialize the rows and columns after seting.
			foreach(string num in theList){   //sets the matrix values
				if (num == ";") {
					rows++;
					columns = 0;
				} else {
					theMatrix.setElement (double.Parse (num), rows+1, columns+1);
					columns++;
				}
			}    // ends the matrix values setter

			resultMatrix = theMatrix;

		}    //end MatrixProcessor
		 
		//_________________________________________________________________________________________________________________//


	}     //end processess  class

}    //end namespace

