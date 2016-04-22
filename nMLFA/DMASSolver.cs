using System;
using DataTypeSpace;
using System.Collections.Generic;
using allSolverInterface;
/// <summary>
/// DMAS solver.
/// THis deals with the mathematical equations (arithematic) in 
/// DMAS.
/// </summary>
namespace EquationSolver
{
	public class DMASSolver : Solver
	{
		List<Expression> theExpressionList;
		List<string> theBatch;
		private bool Processed = true;
		Expression Solution;

		public bool isProcessed () => Processed;

		public Expression getSolution() => Solution;
		public DMASSolver (List<Expression> theExpressionList, List<string>theBatch)
		{
			this.theExpressionList = theExpressionList;
			this.theBatch = theBatch;
			Solve ();
		}

		void Solve()
		{
			if (theBatch.Count == 1) {    // if case for the expression type of "-a"
				string s = theBatch [0].Trim ();
				Expression x = getExpression (theBatch [0].TrimStart ("-".ToCharArray ()));
				if (s [0] == '-') {
					if (x.getExpType () == 1) { // if the expression is a matrix.
						Solution = new Expression ((-1 * x.getMatrix ()));
					}
					if (x.getExpType () == 2) {
						Number num = x.getNumber ();
						num.setNumber (-1 * num.getNumber ());
						Solution = new Expression (num);
					}
				} else if (s [0] == '+') {
					if (x.getExpType () == 1) { // if the expression is a matrix.
						Solution = new Expression (x.getMatrix ());
					}
					if (x.getExpType () == 2) {
						Number num = x.getNumber ();
						num.setNumber (num.getNumber ());
						Solution = new Expression (num);
					}	
				}
				Solution = x;
			}  // end if case for expression of the type of "-a"

			else {     //if there is a mathematical expression like a+ a
				if(Division()){
					if (Multiply ()) {
						if (Add ()) {
							Solution = theExpressionList [theExpressionList.IndexOf(getExpression(theBatch[0]))];
						}
					}
				} else {}
			}

		}


		// this function deals with the addition of the variables.
		bool Add()
		{
			bool solved = true;
			int counter = 0;
			while (counter < theBatch.Count) {
				string x = theBatch [counter];
				if (x == "+") {
					string LHS = theBatch [counter - 1];
					string RHS = theBatch [counter + 1];
					Expression LEXP = new Expression (getExpression (LHS.TrimStart ("-".ToCharArray())));
					Expression REXP = new Expression (getExpression (RHS.TrimStart ("-".ToCharArray())));
					if (LEXP.getExpType () == REXP.getExpType ()) {
						if (LEXP.getExpType () == 1) {
							if (StaticClasses.Checker.MatrixEquivalent (LEXP.getMatrix(), REXP.getMatrix())) {
								Matrix lmat = new Matrix (LEXP.getMatrix());
								Matrix rmat = new Matrix (REXP.getMatrix());
								if (LHS.Contains ("-")) {
									lmat = -1 * lmat;
									theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
								}
								if (RHS.Contains ("-")) {
									rmat = -1 * rmat;
									theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
								}
								Matrix sum = lmat + rmat;
								sum.setTag (LHS.TrimStart ("-".ToCharArray ()));
								Expression result = new Expression (sum);
								theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result; 
							} else {
								TheMessageHandler.MessagePrinter.Print ("Matrices are not similar");
								Processed = false;
								solved = false; 
								break;
							}
						} else if (LEXP.getExpType () == 2) {
							Number lnum = new Number(LEXP.getNumber ());
							Number rnum = new Number(REXP.getNumber ());
							if (LHS.Contains ("-")) {
								lnum.setNumber (-1 * lnum.getNumber ());
								theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
							}
							if (RHS.Contains ("-")) {
								rnum.setNumber (-1 * rnum.getNumber ()); 
								theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
							}
							Number sum = new Number ();
							sum.setNumber (lnum.getNumber () + rnum.getNumber ());
							sum.setTag (LHS.TrimStart ("-".ToCharArray ()));
							Expression result = new Expression (sum);
							theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result;
						}
					} else {
						TheMessageHandler.MessagePrinter.Print ("Cannot add variable with different datatypes");
						Processed = false;
						solved = false;
						break;
					}
					theBatch.RemoveRange (counter, 2);
					counter = 0;
				}
				counter++;
			}
			return  solved;
		}    //end function for addition.


		// this function deals with the multiplication
		bool Multiply()
		{
			bool solved =  true;
			int counter = 0;
			while (counter < theBatch.Count) {
				string x = theBatch [counter];
				if (x == "*") {
					string LHS = theBatch [counter - 1];
					string RHS = theBatch [counter + 1];
					Expression LEXP = new Expression (getExpression (LHS.TrimStart ("-".ToCharArray ())));
					Expression REXP = new Expression (getExpression (RHS.TrimStart ("-".ToCharArray ())));
					if (LEXP.getExpType () == 1 && REXP.getExpType () == 1) {
						if (StaticClasses.Checker.MatrixLegalForMultiplication (LEXP.getMatrix (), REXP.getMatrix ())) {
							Matrix lmat = new Matrix (LEXP.getMatrix ());
							Matrix rmat = new Matrix (REXP.getMatrix ());
							if (LHS.Contains ("-")) {
								lmat = -1 * lmat;
								theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
							}
							if (RHS.Contains ("-")) {
								rmat = -1 * rmat;	
								theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
							}
							Matrix product = lmat * rmat;
							product.setTag (LHS.TrimStart ("-".ToCharArray ()));
							Expression result = new Expression (product);	
							theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result; 
						} else {
							solved = false;
							Processed = false;
							TheMessageHandler.MessagePrinter.Print ("The order of the matrices is not correct");
							break;
						}
					} else if (LEXP.getExpType () == 2 && REXP.getExpType () == 2) {
						Number lnum = new Number (LEXP.getNumber ());
						Number rnum = new Number (REXP.getNumber ());
						if (LHS.Contains ("-")) {
							lnum.setNumber ((-1 * lnum.getNumber ()));
							theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
						}
						if (RHS.Contains ("-")) {
							rnum.setNumber ((-1 * rnum.getNumber ()));
							theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
						}
						Number product = new Number ();
						product.setNumber (lnum.getNumber () * rnum.getNumber ());
						product.setTag (LHS.TrimStart ("-".ToCharArray ()));
						Expression result = new Expression (product);
						theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result; 
					} else if ((LEXP.getExpType () == 1 && REXP.getExpType () == 2)) {
						Matrix lhs = new Matrix (LEXP.getMatrix ());
						Number rhs = new Number (REXP.getNumber ());
						if (LHS.Contains ("-")) {
							lhs = -1 * lhs;
							theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
						}
						if (RHS.Contains ("-")) {
							rhs.setNumber (-1 * rhs.getNumber ());
							theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
							}
						Matrix product = lhs * (rhs.getNumber());
						product.setTag (LHS.TrimStart ("-".ToCharArray ()));
						Expression result = new Expression (product);
						theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result; 
					}
					else if ((LEXP.getExpType () == 2 && REXP.getExpType () == 1)) {
						Matrix rhs = new Matrix (REXP.getMatrix ());
						Number lhs = new Number (LEXP.getNumber ());
						if (RHS.Contains ("-")) {
							rhs = -1 * rhs;
							theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
						}
						if (LHS.Contains ("-")) {
							lhs.setNumber (-1 * lhs.getNumber ());
							theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
						}
						Matrix product = rhs * (lhs.getNumber());
						product.setTag (LHS.TrimStart ("-".ToCharArray ()));
						Expression result = new Expression (product);
						theExpressionList [theExpressionList.IndexOf (getExpression (LHS.TrimStart ("-".ToCharArray ())))] = result; 
					}

					theBatch.RemoveRange (counter,2);
					counter = 0;
				}
				counter++;
			}
			return solved;
		}  //end multiplication

		bool Division()
		{
			bool solved = true;
			int counter = 0;
			while (counter < theBatch.Count) {
				string x = theBatch [counter];
				if (x == "/") {
					string lhs = theBatch [counter - 1];
					string rhs = theBatch [counter + 1];
					Expression lexp = new Expression (getExpression (lhs.TrimStart ("-".ToCharArray ())));
					Expression rexp = new Expression (getExpression (rhs.TrimStart ("-".ToCharArray ())));
					if (lexp.getExpType () == 1 && rexp.getExpType () == 2) {
						Number rnum = new Number (rexp.getNumber ());
						Matrix lmat = new Matrix (lexp.getMatrix ());
						if (lhs.Contains ("-")) {
							lmat = -1 * lmat;
							theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
						}
						if (rhs.Contains ("-")) {
							rnum.setNumber (-1 * rnum.getNumber ());
							theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
						}
						Matrix ans = lmat / (rnum.getNumber ());
						ans.setTag (lhs.TrimStart ("-".ToCharArray ()));
						Expression result = new Expression (ans);
						theExpressionList [theExpressionList.IndexOf (getExpression (lhs.TrimStart ("-".ToCharArray ())))] = result; 
					} else if (lexp.getExpType () == 2 && rexp.getExpType () == 2) {
						Number lnum = new Number (lexp.getNumber ());
						Number rnum = new Number (rexp.getNumber ());
						if (lhs.Contains ("-")) {
							lnum.setNumber (-1 * lnum.getNumber ());
							theBatch [counter - 1] = theBatch [counter - 1].TrimStart ("-".ToCharArray ());
						}
						if (rhs.Contains ("-")) {
							rnum.setNumber (-1 * rnum.getNumber ());
							theBatch [counter + 1] = theBatch [counter + 1].TrimStart ("-".ToCharArray ());
						}
						Number ans = new Number ();
						ans.setNumber (lnum.getNumber () / rnum.getNumber ());
						ans.setTag (lhs.TrimStart ("-".ToCharArray ()));
						Expression result = new Expression (ans);
						theExpressionList [theExpressionList.IndexOf (getExpression (lhs.TrimStart ("-".ToCharArray ())))] = result; 
					} else {
						TheMessageHandler.MessagePrinter.Print ("Invalid operation");
						Processed = false;
						solved = false;
						break;
					}
					theBatch.RemoveRange (counter, 2);
					counter = 0;
				}

				counter++;
			}
			return solved;
		}  //end function for division.

		////////////////////////Helper functions

		Expression getExpression(string tag)
		{
			Expression found = new Expression();
			foreach (Expression x in theExpressionList) {
				if (x.getTag () == tag) {
					found = x;
					break;
				}
			}
			return found;
		} 

	}   //end DMAS Solver Class

}     // end EquationSolvingHead namespace

