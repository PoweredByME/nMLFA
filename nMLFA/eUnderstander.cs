using System;
using System.Collections.Generic;
using StaticClasses;
using DataTypeSpace;



/// <summary>
/// this namespace works for the strings that are passed in for of an equation. i.e with an equality sign.
/// </summary>


namespace equationUnderstander
{
	public class eUnderstander
	{
		List <Expression> theExpressionList;
		Expression result;
		string LHS;
		string RHS;
		string equation;
		bool Processed = false;
		public eUnderstander(){}
		public eUnderstander(List<Expression> theExpList,string theString)
		{
			theExpressionList = theExpList;// remember it is the same one that is in the string watch so if you change this the real one will be damaged.
			equation = new string (theString.ToCharArray ());
			this.Obeserver ();

		}
		public bool isProcessed () => Processed;
		public Expression getResult () => result;
		//________________________________________________________//

		// this observes the equation being given... i.e it sees if is mathematical equation 
		// or a variable declaration.
		// or if the command is invalid

		void Obeserver()      
		{
			Breaker ();
			if (Checker.ifContainOperations (LHS) || LHS.Contains (" ") || string.IsNullOrWhiteSpace(LHS) || Checker.isNumeric (LHS [0])) { 
				// if left hand side is and equation or some thing else than a single variable or if it is and empty
				//string or its first character is a number
					TheMessageHandler.MessagePrinter.Print ("Wrong format LHS cannot contain an expression");
					Processed = false;
			} else {
				if (!Checker.ifContainOperations(RHS) && Checker.isNumberDeclaration (RHS)) {  // if the string is a Numeric Declaration.
					NumberMaker();
				} else if (!Checker.ifContainOperations(RHS) && Checker.isMatrixDeclaration (RHS)) {  // if the string is a Matrix Declaration.
					MatrixMaker();
				} else {
					if (SearchList (RHS)) {    // THIS DEALS WITH THE REASSIGNMENT LIKE A = B 
						Reassignment();
					} else {
						if (Checker.ifContainOperations (RHS)) {
							EquationHead.EquationWatch solution = new EquationHead.EquationWatch (theExpressionList, RHS);
							if (solution.isProcessed()) {
								result = new Expression (solution.getSolution ());
								result.setTag (LHS);
								Processed = true;
							} else {
								Processed = false;
							}
						}
						else{
						Processed = false;
						TheMessageHandler.MessagePrinter.Print ($"No variable '{RHS}' eixsts, currently");
						}
					}   
				}    //if the is no matrix or number declaration	
			}      // else for the reason if there is some thing in the string
		}  // end observer.

		//________________________________________________________//

		void Breaker()   // THIS BREAKS THE STRING INTO LHS AND RHS.
		{
			string[] theStringSlited = equation.Split ("=".ToCharArray (), 2);
			LHS = theStringSlited [0].Trim();
			RHS = theStringSlited [1].Trim();
		}
			
		Expression getExpression(string tag)   // THIS RETURNS THE REQUIRED EXPRESSION FOR THE EXPRESSION LIST 
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

		bool SearchList(string exp)   // THIS SEES IF THE EXPRESSION EXISTS IN THE LIST.
		{
			bool found = false;
			foreach (Expression x in theExpressionList) {
				if (x.getTag () == exp) {
					found = true;
					break;
				}
			}
			return found;
		}

		/// <summary>
		/// THE HELPER FUNCTIONS OF OBSERVE FUNCTION TO MAKE NUMBERS AND MATRICES AND ASSIGN THEM TO THEIR RESPECTIVE NAMES
		/// </summary>

		void NumberMaker()       // this functions deals with the assignment and parsing of the numbers.
		{
			if (!string.IsNullOrWhiteSpace (RHS)) {
				Number newNumber = new Number ();    
				newNumber.setNumber (double.Parse (RHS));
				newNumber.setTag (LHS);
				result = new Expression (newNumber);   
				Processed = true;
			} else {
				TheMessageHandler.MessagePrinter.Print ("No proper input was given to be processed");
				Processed = false;
			}
		}


		void MatrixMaker()   // this function deals with the assigment and making of the matrix
		{
			if (Checker.getOccurance (RHS, ']') == 1 && Checker.getOccurance (RHS, '[') == 1) {  // if there is a correct formatte for the matrix
				if (Checker.OccurAtEnd (RHS, ']') && Checker.OccurAtStart (RHS, '[')) {  // similarly here.
					if (Checker.ifContainInput (RHS)) {
						Matrix theMatrix = new Matrix (HelperClasses.Processes.MatrixMaker (RHS));  // turns the string into matrix
						theMatrix.setTag (LHS);    // sets the tag of the matrix
						result = new Expression (theMatrix);   	
						Processed = true;
					} else {
						TheMessageHandler.MessagePrinter.Print ("No proper input was given to be processed");
						Processed = false;
					}
				} else {
					TheMessageHandler.MessagePrinter.Print ("Wrong formate for matrix entered.");  // wrong formate entered
					Processed = false;
				}
			} else {
				TheMessageHandler.MessagePrinter.Print ("Wrong formate for matrix entered.");   // wrong formate entered
				Processed = false;
			}
		}


		void Reassignment()
		{
			result = new Expression (theExpressionList [theExpressionList.IndexOf (getExpression (RHS))]);
			result.setTag (LHS);
			if (result.getExpType () == 1) {
				result.getMatrix ().setTag (LHS);
			} else if (result.getExpType () == 2) {
				result.getNumber ().setTag (LHS);
			}
			Processed = true;
		}

		///////////////////////////////////////////////////////////////////////////////////////////////

    }//  end class




}  // end namespace
