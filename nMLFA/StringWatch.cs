using System;
using System.Collections.Generic;
using DataTypeSpace;
using StaticClasses;
using equationUnderstander;
using TheMessageHandler;
using virtualUnderstander;

/// <summary>
/// String observer.
/// This Class and namespace is the brain of the total program it observes the string given by the user to the computer.
/// It senses the string as an equation or an expression.
/// If the given string is an equation it sends the string to the equationUnderstander namespace via eUnderstander.
/// If the given string is an expression it sends the string to the virtual Understander namespace
/// to under stand if the string is a command for variable or a mathermatical expression.
/// 
/// This namespace also deals with the assignment operators. 
/// If the variable with the same name exists it overrides it. 
/// Else it makes the new variable.
/// </summary>

namespace StringWatch
{	
	public class StringObserver
	{
		List<Expression> theExpressionList = new List<Expression>();
		string givenExpression;
		bool printExp = false;
		public void setString(string exp)=> givenExpression = exp.Trim();
		public void Process() => Observe();
		public StringObserver(){}
		public StringObserver(string theString)
		{
			theString = theString.Trim ();
			givenExpression = new string (theString.ToCharArray ());
		}
	
		void Observe()
		{
			if (string.IsNullOrWhiteSpace (givenExpression)) {
				MessagePrinter.Print ("Nothing is given to me");
			}
			else if (Checker.isEquation (givenExpression)) {  // if the string is an equation. i.e. with an equality symbol 
				if (Checker.getOccurance (givenExpression, '=') == 1) {  // if there is a correct formate(1 equality only)
					eUnderstander equation = new eUnderstander (theExpressionList,givenExpression);   // send the string to eUnderstander for further processing 
					if (equation.isProcessed ()) {    // if the string processes with no error or exception handling.
						Expression theResult = new Expression (equation.getResult ());
						if (DoesNotAlreadyExist (theResult)) {    //checks if the variable name already exists or not.
							theExpressionList.Add (theResult);
							printExp = true;
						} else {
							printExp = false;
						}
					}
				} else {
					MessagePrinter.Print ("Wrong formate equation must contain only one equality operator");
				}
			} else {
				vUnderstander expression = new vUnderstander (theExpressionList, givenExpression);    // sends the equation  to the virtual Understanding classes to manage it.
				if (expression.isProcessed ()) {
					Expression theResult = new Expression (expression.getResult ());
					theExpressionList.Add (theResult);
					printExp = true;
				} else
					printExp = false;
			}
		}

		/// ///////////////////////////////////////////////////////////////////////////

		bool DoesNotAlreadyExist(Expression theResult)
		{
			bool notfound = true;
			foreach (Expression x in theExpressionList) {
				if (x.getTag () == theResult.getTag ()) {
					theExpressionList [theExpressionList.IndexOf (x)] = theResult;
					notfound = false;
					break;
				}
			}
			if (notfound == false) {
				TheMessageHandler.MessagePrinter.Print ("A variable with the same name existed so I have overriden the data.");
				ExpressionPrinter exp = new ExpressionPrinter (theResult);
				exp.Print ();
			}
			return notfound;
		}


		/// ///////////////////////////////////////////////////////////////////////////


		private static int counter = 0;   
	    public void Printer() // printes the expressions of the Expression List 
		{
			if (printExp) {
				ExpressionPrinter x = new ExpressionPrinter (theExpressionList [counter]);
				x.Print ();
				counter++;
			} 
			autoDestroyer ();
		}

		void autoDestroyer()
		{
			if (counter != 0 && counter % 50 == 0) {
				theExpressionList.RemoveRange (0, 15);
				counter -= 15;
			}
		}

		public void PrintEntireExpressionList()
		{
			foreach (Expression x in theExpressionList) {
				ExpressionPrinter c = new ExpressionPrinter (x);
				c.Print ();
			}
		}


	}
}

