using System;
using StaticClasses;
using System.Threading;

namespace DataTypeSpace
{
	public class Matrix  //matrix class starts
	{
		protected double[,] theMatrix;      //the basic matrix;
		protected int mRows, mCols;
		private OperationsClass theOperations =  new OperationsClass();
		private static OperationsClass staticOperations =  new OperationsClass();
		private string tag;

		public Matrix(Matrix theOther)
		{
			mRows = theOther.mRows;
			mCols = theOther.mCols;
			theMatrix = new double[mRows,mCols];
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					theMatrix [c, c1] = theOther.getElement (c+1, c1+1);
				}
			}
			tag = theOther.tag;
		}

		public Matrix(string thetag, double[,] matrix,int row = 1, int column = 1 )
		{
			mRows = row;mCols = column;
			theMatrix = matrix;
			tag = thetag;
		}

		public Matrix(int rows = 01, int columns = 01)
		{
			mRows = rows; mCols = columns;
			theMatrix = new double[mRows, mCols];
			tag = " ";
		}

		public Matrix(double[,] mat, int rows , int cols)
		{
			mRows = rows; mCols = cols;
			theMatrix = mat;
			tag = " ";
		}

		public void Zero()
		{
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					theMatrix [c, c1] = 0;
				}
			}
		}

		public void Identity()
		{
			for (int c = 0; c < mRows; c++) {
				for (int c1 = 0; c1 < mCols; c1++) {
					if (c == c1)
						theMatrix [c, c1] = 1;
					else
						theMatrix [c, c1] = 0;
				}
			}
		}

		// Helper functions and function for guass Jordan.

		private void Swap(string rowOrColumn , int row1 , int row2)   //function for swaping for rows and columns.
		{
			if (rowOrColumn == "row") {   //start if case for row swapping.
				if (row1 > mRows || row1 < 0 || row2 > mRows || row2 < 0)
				{
					Console.WriteLine("Sorry, the row swapping ain't possible");
					return;
				}
				int column = this.mCols;
				double temp = 0;
				for (int i = 0; i<column; i++)
				{
					temp = theMatrix[row1 - 1,i];
				    theMatrix[row1 - 1,i] = theMatrix[row2 - 1,i];
					theMatrix[row2 - 1,i] = temp;
				} 
			}   //end else case for row swaping.
			else {    //start case for column swaping.
				if (row1 > mCols || row1 < 0 || row2 > mCols || row2<0)
				{
					Console.WriteLine ("Sorry, the swaping ain't gonna happen"); 
					return;
				}
				double temp = 0;
				int row = this.mRows;
				for (int i = 0; i<row; i++)
				{
					temp = theMatrix[i,row1 - 1];
					theMatrix[i,row1 - 1] = theMatrix[i,row2 - 1];
					theMatrix[i,row2 - 1] = temp;
				}
			}   // end else case.
		}    //end swap function .

		private void rowaddition(int row1, int row2, double factor)  // function for row addition with a factor.
		{
			for (int i = 0; i < mCols; i++)
			{
				theMatrix[row2,i] += factor*(theMatrix[row1,i]);
			}
		}    //end row addition.
			
		private void columnaddition(int column1, int column2, double factor) //function for column addtion for factors.
		{
			for (int i = 0; i < mRows; i++)
			{
				theMatrix[i,column2] += factor*(theMatrix[i,column1]);
			}
		}    // end column addition.

		private void divisionofrows(double factor, int rownum)  // function for division of rows
		{
			if (factor == 0)
				return;
			for (int i = 0; i < mCols; i++)
			{
				theMatrix[rownum,i] /= factor;
			}
		}   // end division of rows with a factor

		private void divisionofcolumns(double factor, int colnum)  // function for the division of the columns with a factor
		{
			if (factor == 0)
				return;
			for (int i = 0; i < mRows; i++)
			{
				theMatrix[i,colnum] /= factor;
			}
		}     // end function of division of the columns

		public void GaussElimination()     // method for the guass Elimination. // the echelon form.
		{
			int j, i;
			bool calculation = false;
			for (j = 0; j < mCols; j++)
			{
				for (i = 0; i < mRows; i++)
				{
					if (theMatrix[i,j] == 0)continue;
					calculation = true;
					divisionofrows(theMatrix[i,j], i);
					for (int k = i + 1; k < mRows; k++)
					{
						rowaddition(i, k, -1 * theMatrix[k,j]);
						theMatrix[k,j] = 0;
					}
					Swap("row", 1, i + 1);
					int h = 1;
					for (j = j + 1; j < mCols; j++, h++)
					{
						for (i = h; i < mRows; i++)
						{
							if (theMatrix[i,j] == 0)continue;
							divisionofrows(theMatrix[i,j], i);
							for (int k = i + 1; k < mRows; k++)
							{
								rowaddition(i, k, -1 * theMatrix[k,j]);
								theMatrix[k,j] = 0;
							}
							Swap("row", h + 1, i + 1);
						}
					}
				}
			}
	    }    // end method of guass Elimination.

		public void GaussJordan()
		{
			int j, i;
			bool calculation = false;
			for (j = 0; j < mCols; j++)
			{
				for (i = 0; i < mRows; i++)
				{
					if (theMatrix[i,j] == 0)continue;
					calculation = true;
					divisionofrows(theMatrix[i,j], i);
					for (int k = i+1; k < mRows; k++)
					{
						rowaddition(i, k, -1*theMatrix[k,j]);
						theMatrix[k,j] = 0;
					}
					Swap("row", 1, i + 1);
					int h = 1;
					for ( j = j + 1; j < mCols; j++, h++)
					{
						for ( i = h; i < mRows; i++)
						{
							if (theMatrix[i,j] == 0)continue;
							divisionofrows(theMatrix[i,j], i);
							for (int k = 0; k < mRows; k++)
							{
								if (k == i)continue;
								rowaddition(i, k, -1 * theMatrix[k,j]);
								theMatrix[k,j] = 0;
							}
							Swap("row", h + 1, i + 1);
						}
					}
					return;
				}
			}

		}

		//helper functions and functinos for guass jordan.

		public double this [int aRow, int aCol] => theMatrix[aRow, aCol];

		public void setMatrixArray (double[,] mat)
		{
			theMatrix = mat;
		}

		public string getTag ()
		{
			return tag;
		}

		public Matrix getAdjoint ()
		{
			return theOperations.getAdjoint (this);
		}

		public Matrix getTranspose ()
		{
			return theOperations.getTranspose (this);
		}

		public double determinant ()
		{
			return theOperations.getdetreminant (this);
		}

		public Matrix addTo (Matrix otherMat)
		{
			return theOperations.addMatrix (this, otherMat);
		}

		public Matrix subtractFrom (Matrix otherMat)
		{
			return theOperations.subtractMatrix (otherMat, this);
		}

		public int getRows ()
		{
			return mRows;
		}

		public int getColumns ()
		{
			return mCols;
		}

		public double  getElement (int row, int col)
		{
			return theMatrix [row - 1, col - 1];
		}

		public double[,] getMatrixArray ()=>theMatrix;

		public void setTag (string theMatTag)
		{
			tag = theMatTag;
		}

		public void setElement (double number, int rows, int col)
		{
			theMatrix [rows - 1, col - 1] = number;
		}

		static public Matrix operator+ (Matrix lhs, Matrix rhs)
		{
			return staticOperations.addMatrix (lhs, rhs);
		}

		static public Matrix operator- (Matrix lhs, Matrix rhs)
		{
			return staticOperations.subtractMatrix (lhs, rhs);
		}

		static public Matrix operator* (Matrix lhs, Matrix rhs)
		{
			return staticOperations.multiplyMatrix (lhs, rhs);
		}

		static public Matrix operator* (Matrix lhs, double num)
		{
			return staticOperations.mConst (lhs, num);
		}

		static public Matrix operator* (double num, Matrix rhs)
		{
			return staticOperations.mConst (rhs, num);
		}

		static public Matrix operator/ (Matrix lhs, double num)
		{
			return staticOperations.mDivide (lhs, num);
		}


	}
	//the basic matrix class ends
		

	class OperationsClass     //start operations class
	{

		public Matrix mDivide (Matrix mat, double num)
		{
			int r = mat.getRows ();
			int c = mat.getColumns ();
			double[,] m = mat.getMatrixArray ();
			for (int c1 = 0; c1 < r; c1++) {
				for (int c2 = 0; c2 < c; c2++) {
					m [c1, c2] /= num;
				}
			}
			Matrix ans = new Matrix (mat.getTag (), m, r, c);
			return ans;
		}

		public Matrix mConst (Matrix mat, double num)
		{
			int r = mat.getRows ();
			int c = mat.getColumns ();
			double[,] m = mat.getMatrixArray ();
			for (int c1 = 0; c1 < r; c1++) {
				for (int c2 = 0; c2 < c; c2++) {
					m [c1, c2] *= num;
				}
			}
			Matrix ans = new Matrix (mat.getTag (), m, r, c);
			return ans;
		}


		public Matrix addMatrix (Matrix mat, Matrix mat1)    //Matrix Addition
		{ 
			if (mat.getRows () == mat1.getRows () && mat.getColumns () == mat1.getColumns ()) {
				Matrix result = new Matrix (mat.getRows (), mat.getColumns ());
				for (int c = 1; c <= mat.getRows (); c++) {
					for (int c1 = 1; c1 <= mat1.getColumns (); c1++) {
						result.setElement (mat.getElement (c, c1) + mat1.getElement (c, c1), c, c1);
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);
				result.setElement (0, 1, 1);
				Console.WriteLine ("The Matrices are not similar... cannot be added.Sorry.");
				return result;
			}
		}
		//Matrix Addition Ends

		public Matrix subtractMatrix (Matrix mat, Matrix mat1)   //Matrix Subtraction
		{
			if (mat.getRows () == mat1.getRows () && mat.getColumns () == mat1.getColumns ()) {
				Matrix result = new Matrix (mat.getRows (), mat.getColumns ());
				for (int c = 1; c <= mat.getRows (); c++) {
					for (int c1 = 01; c1 <= mat1.getColumns (); c1++) {
						result.setElement (mat.getElement (c, c1) - mat1.getElement (c, c1), c, c1);
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);
				result.setElement (0, 1, 01);
				Console.WriteLine ("The Matrices are not similar... cannot be subtracted.Sorry.");
				return result;
			}
		}
		//End Matrix Subtration

		public Matrix multiplyMatrix (Matrix lhs, Matrix rhs)
		{
			if (lhs.getColumns () == rhs.getRows ()) {
				Matrix result = new Matrix (lhs.getRows (), rhs.getColumns ());
				for (int c = 1; c <= lhs.getRows (); c++) {
					for (int c1 = 1; c1 <= rhs.getColumns (); c1++) {
						for (int c2 = 1; c2 <= lhs.getColumns (); c2++) {
							if (c2 == 0)
								result.setElement (lhs.getElement (c, c2) * rhs.getElement (c2, c1), c, c1);
							else
								result.setElement (result.getElement (c, c1) + (lhs.getElement (c, c2) * rhs.getElement (c2, c1)), c, c1);
						}
					}
				}
				return result;
			} else {
				Matrix result = new Matrix (1, 1);
				result.setElement (0, 1, 1);
				Console.WriteLine ("The matrices cannot be multiplied.Sorry.");
				return result;
			}
		}

		public double getdetreminant (Matrix theMatrix)    //getdeterminant interface
		{
			double[,] matrix = theMatrix.getMatrixArray ();
			int mRows = theMatrix.getRows ();
			int mCols = theMatrix.getColumns ();
			return determinant (matrix, mRows, mCols);
		}
		//getdetermiant ends here.

		private double determinant (double[,] matrix, int rows, int cols)   //determinant implementation 
		{
			if (rows == cols) {
				if (rows == 1 && cols == 1) {
					return matrix [0, 0];
				} else if (rows == 2 && cols == 2) {
					return matrix [0, 0] * matrix [1, 1] - matrix [0, 1] * matrix [1, 0];
				} else {
					double det = 0;
					double[] store = new double[cols];
					int newRows = rows - 1;
					int newCols = cols - 1;
					double[,] newMatrix = new double[newRows, newCols];

					for (int ncolumn = 0; ncolumn < cols; ncolumn++) {
						int newrow = 0;
						int newcolumn = 0;
						for (int row = 0; row < rows; row++) {
							for (int column = 0; column < cols; column++) {
								if (row != 0) {
									if (column != ncolumn) {
										newMatrix [newrow, newcolumn] = matrix [row, column];
										newcolumn++;
										if (newcolumn == newCols) {
											newcolumn = 0;
											newrow++;
										}//end
									}//end
								}//end
							}//end for
						}//end for
						int sign = (int)Math.Pow (-1, ncolumn);
						store [ncolumn] = sign * determinant (newMatrix, newRows, newCols);
					}//end for
					det = 0;
					for (int c = 0; c < cols; c++) {
						det += (matrix [0, c] * store [c]);
					}
					return det;
				}//end else

			} else {
				Console.WriteLine ("The determinant of the matrix doesnot exist.Sorry.");
				return 0;
			}
		}
		//end determinant measurement function ends here

		public Matrix getTranspose (Matrix mat)
		{
			double[,] matrix = mat.getMatrixArray ();
			int rows = mat.getRows ();
			int cols = mat.getColumns ();
			return Transpose (matrix, rows, cols);
		}

		private Matrix Transpose (double[,] matrix, int rows, int cols)
		{
			Matrix t = new Matrix (cols, rows);
			for (int c = 01; c <= rows; c++) {
				for (int c1 = 01; c1 <= cols; c1++) {
					t.setElement (matrix [c - 1, c1 - 1], c1, c);
				}
			}
			return t;
		}

		public Matrix getAdjoint (Matrix matrix)
		{
			double[,] mat = matrix.getMatrixArray ();
			int rows = matrix.getRows ();
			int cols = matrix.getColumns ();
			return Adjoint (mat, rows, cols);
		}

		private Matrix Adjoint (double[,] matrix, int rows, int cols)   //function for adjoint
		{
			if (rows == cols) {
				if (rows == 1 && cols == 1) {
					Matrix result = new Matrix (1, 1);
					result.setElement (matrix [0, 0], 1, 1);
					return result;
				} else {
					double[,] newMatrix = new double[rows, cols];
					double[,] tobeAdjoint = new double[rows, cols];
					int newCol = cols - 1;
					int newRow = rows - 1;
					for (int nrow = 0; nrow < rows; nrow++) {
						for (int ncol = 0; ncol < cols; ncol++) {
							int newrow = 0;
							int newcol = 0;
							for (int c = 0; c < rows; c++) {
								for (int c1 = 0; c1 < cols; c1++) {
									if (nrow != c) {
										if (ncol != c1) {
											newMatrix [newrow, newcol] = matrix [c, c1];
											newcol++;
											if (newcol == newCol) {
												newcol = 0;
												newrow++;
											}
										}
									}
								}
							}
							int sign = 0;
							int sum = ncol + nrow;
							sign = (int)Math.Pow (-1, sum);
							tobeAdjoint [nrow, ncol] = sign * determinant (newMatrix, newRow, newCol);
						}
					}
					return Transpose (tobeAdjoint, rows, cols);
				}
			} else {
				Console.WriteLine ("The Adjoint of this matrix doesnot exist. Sorry.");
				Matrix result = new Matrix (1, 1);
				result.Zero ();
				return result;
			}
		}
		//end function for adjoint.


	}
	//end operations class.
		

	public class Number
	{
		// the class for numeric numbers
		private double number;
		private string tag;

		public double getNumber ()
		{
			return number;
		}

		public string getTag ()
		{
			return tag;
		}

		public void setTag (string theTag)
		{
			tag = theTag;
		}

		public void setNumber (double theNumber)
		{
			number = theNumber;
		}

		public Number ()
		{
			number = 0;
			tag = "";
		}

		public Number (Number obj)
		{
			number = obj.number;
			tag = obj.tag;
		}
			
	}
	//end class for numbers;


	//Expression class Stores all the datatypes and manages them in the list and manage their printing.

	public class Expression
	{
		Matrix theMatrix;
		Number theNumber;
		string theTag = "Exp";
		int expType = 0;

		public Expression ()
		{
		}

		public Expression (Matrix mat)
		{
			theMatrix = mat;
			theTag = theMatrix.getTag ();
			expType = 1;
		}

		public Expression (Number num)
		{
			theNumber = num;
			theTag = theNumber.getTag ();
			expType = 2;
		}

		public Expression (Expression exp)
		{

			expType = exp.expType;
			theTag = exp.theTag;
			if (expType == 1) {
				theMatrix = exp.theMatrix;
			}
			if (expType == 2) {
				theNumber = exp.theNumber;
			}
		}
		public void setTag (string tag)
		{
			theTag = tag;
		}
		public void setEntireTag(string tag)
		{
			theTag = tag;
			if (this.expType == 1) {
				this.theMatrix.setTag (tag);
			} else if (this.expType == 2) {
				this.theNumber.setTag (tag);
			}
		}

		public Matrix getMatrix () => theMatrix;

		public Number getNumber () => theNumber;

		public string getTag () => theTag;

		public int getExpType () => expType;

		public void setExpType (int type) => expType = type;
	}

	//   This class is responsible of printing the Expression.
	public class ExpressionPrinter
	{
		Expression theExpression;

		public ExpressionPrinter ()
		{
		}

		public ExpressionPrinter (Expression exp)
		{
			theExpression = exp;
		}

		public void Print ()
		{
			int expType = theExpression.getExpType ();
			if (expType == 1) {
				Console.WriteLine ($"{theExpression.getTag()} = ");
				MatrixOutput.Print (theExpression.getMatrix ());
			} else if (expType == 2) {
				Console.WriteLine ($"{theExpression.getTag()} = {theExpression.getNumber().getNumber()}");
			}
		}

	}



}