using System;
using DataTypeSpace;

/// <summary>
/// this namespace have static functions that prints a matrix or that apply the checks for the code.
/// </summary>


namespace StaticClasses
{
	public static class Checker
	{
		//static string alphabets = "";
		static string numbers = "0123456789.";
		static string operators = "()*+-/^";
	//	static string Alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMONPQRSTUVWXYZ_";
		public static bool isEquation (string exp) => exp.Contains("=");
		public static int getOccurance (string exp, char character)  // returns the occuracences of a specific character.
		{ 
			int occur = 0;
			foreach (char c in exp) {
				if (c == character)
					occur++;
			}
			return occur;
		}
		// tells if the stirng is a mathematical one.
		public static bool ifContainOperations (string exp) => (exp.Contains("(")||exp.Contains(")")||exp.Contains("+")||exp.Contains("-")||exp.Contains("*")||exp.Contains("/")||exp.Contains("^"));
		public static bool isOperation (char character)=> operators.Contains(character.ToString());
		public static bool isMatrixDeclaration (string exp) => (exp.Contains("[") || exp.Contains("]"));
		public static bool isBasicOperator (string exp) => (exp.Contains("+")||exp.Contains("-")||exp.Contains("*")||exp.Contains("/") || exp.Contains(")") || exp.Contains("^"));  // tells wehter the operator is a '+ - * /'  function made specifically for the Operator Manager function of the equation head
		public static bool isNumberDeclaration (string exp)
		{ 
			bool numeric = true;
			foreach (char x in exp) {
				if (!isNumeric (x)) {
					numeric = false;
					break;
				}
			}
			return numeric;
		}
		public static bool OccurAtStart(string exp, char character) 
		{
			return (exp.IndexOf (character) == 0);
		}
		public static bool OccurAtEnd(string exp, char character)
		{
			return (exp.IndexOf (character) == (exp.Length - 1));
		}
		public static bool isNumeric(char character) => numbers.Contains(character.ToString());

		public static bool ifContainInput(string exp)
		{
			string newNumbers = "0123456789";
			bool nummeric = false;
			foreach (char c in exp) {
				if (newNumbers.Contains (c.ToString())) {
					nummeric = true;
					break;
				}
			}
			return nummeric;
		}

		public static bool MatrixEquivalent (Matrix lhs, Matrix rhs) => (lhs.getRows()== rhs.getRows() && lhs.getColumns() == rhs.getColumns());
		public static bool MatrixLegalForMultiplication (Matrix lhs, Matrix rhs) => (lhs.getColumns() == rhs.getRows());

	}    //end Checker class

	// This class is Responsible for matrix printing.
	public static class MatrixOutput
	{
		public static void Print(Matrix mat)
		{
			for (int c = 0; c < mat.getRows (); c++) {
				for (int c1 = 0; c1 < mat.getColumns (); c1++) {
					Console.Write ($"\t{mat.getElement(c+1,c1+1)}");
				}
				Console.WriteLine ("");
			}
		}
	}
}

