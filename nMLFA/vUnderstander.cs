using System;
using DataTypeSpace;
using System.Collections.Generic;
using StaticClasses;

///this namespace deals in expression with no equality signs i.e it sees commands and mathematical expressions and variable assigment.

namespace virtualUnderstander
{
	public class vUnderstander
	{
		List <Expression> theExpressionList;
		private string givenExpression;
		Expression theResult;
		bool Processed = false;
		public bool isProcessed () => Processed;
		public vUnderstander (){}
		public vUnderstander (List<Expression> theExpList,string exp)
		{
			givenExpression = new string (exp.ToCharArray ()); 
			theExpressionList = theExpList; // remember it is the same one that is in the string watch so if you change this the real one will be damaged.
			this.Observer();
		}

		public Expression getResult ()=> theResult;

		void Observer()
		{
			if (Checker.isNumberDeclaration (givenExpression) && !Checker.ifContainOperations(givenExpression)) {
				MakeNumber ();
			} else if (Checker.isMatrixDeclaration (givenExpression) && !Checker.ifContainOperations(givenExpression)) {
				MakeMatrix ();
			} else {
				if(Checker.ifContainOperations(givenExpression))    // if the expression is a mathematical one.
				{
					EquationHead.EquationWatch theEquationSolution = new EquationHead.EquationWatch (theExpressionList,givenExpression);
					if (theEquationSolution.isProcessed ()) {
						theResult = new Expression (theEquationSolution.getSolution ());
						if (theResult.getExpType () == 1) {
							theResult.setTag (autoMatrixNamer ());
						} else if (theResult.getExpType () == 2) {
							theResult.setTag (autoNumberNamer ());
						}
						Processed = true;
					} else {
						Processed = false;
					}
				}
				else if (SearchList (givenExpression))   // for searching a quried variable
				{
					Processed = false;
				} 
				else 
				{
					TheMessageHandler.MessagePrinter.Print ("Does not exit");
					Processed = false;
				}
			}
				
		}

		/// <summary>
		/// these two functions auto name the variable.
		/// </summary>
		static int SNACount=0;
		string autoNumberNamer()
		{
			string theName = "n" + SNACount;
			while (doesExist (theName)) {
				SNACount++;
				theName = "n" + SNACount;
			}
			SNACount++;
			return theName;
		}

		static int SMACount=0;
		string autoMatrixNamer()
		{
			string theName = "M" + SNACount;
			while (doesExist (theName)) {
				SMACount++;
				theName = "M" + SMACount;
			}
			SMACount++;
			return theName;
		}

		//______________________________________________________________________//

		bool doesExist(string name)     // this function is like search list but helps out the autoNumberNamer and autoMatrixNamer
		{
			bool exists = false;
			foreach (Expression x in theExpressionList) {
				if (name == x.getTag ()) {
					exists = true;
					break;
				}
			}
			return exists;
		}

		/// <summary>
		/// Makes the number.
		/// Makes the Matrix
		/// </summary>

		void MakeNumber()
		{
			if (!string.IsNullOrWhiteSpace (givenExpression)) {
				Number num = new Number ();
				num.setNumber (double.Parse (givenExpression));
				num.setTag (autoNumberNamer ());
				theResult = new Expression (num);
				Processed = true;
			} else {
				TheMessageHandler.MessagePrinter.Print ("No proper input was given to be processed");
				Processed = false;
			}
		}

		void MakeMatrix()
		{
			if (Checker.getOccurance (givenExpression, ']') == 1 && Checker.getOccurance (givenExpression, '[') == 1) {  // if there is a correct formatte for the matrix
				if (Checker.OccurAtEnd (givenExpression, ']') && Checker.OccurAtStart (givenExpression, '[')) {  // similarly here.
					if (Checker.ifContainInput (givenExpression)) {
						Matrix theMatrix = new Matrix (HelperClasses.Processes.MatrixMaker (givenExpression));
						theMatrix.setTag (autoMatrixNamer ());
						theResult = new Expression (theMatrix);
						Processed = true;
					} else {
						TheMessageHandler.MessagePrinter.Print ("No proper input was given to be processed");
						Processed = false;
					}
				} else {
					TheMessageHandler.MessagePrinter.Print ("Wrong formate of the matrix is entered");
					Processed = false;
				}
			}else {
				TheMessageHandler.MessagePrinter.Print ("Wrong formate of the matrix is entered");
				Processed = false;
			}
		}


		//_________________________________________________________________________________//


		Expression getExpression(string tag)
		{
			Expression fished = null;
			foreach (Expression x in theExpressionList) {
				if(x.getTag () == tag) {
					fished = x;
					break;
				}
			}
			return fished;
		}

		bool SearchList(string exp)   // searches the list for queried variable and returns a bool if found = true and 
		{		                              // false if not found
			bool found = false;
			foreach (Expression x in theExpressionList) {
				if (x.getTag () == exp) {
					ExpressionPrinter p = new ExpressionPrinter (x);
					p.Print ();
					found = true;
					break;
				}
			}
			return found;
		}


	}  //end class
}   //end namspace 

