using System;


namespace ThirdCTask {

  public class InvalidDepartmentException : System.Exception {
    public InvalidDepartmentException()
    : base() { }
    public InvalidDepartmentException(string message)
    : base(message) { }
    public InvalidDepartmentException
    (string message, System.Exception inner)
    : base(message, inner) { }

  }
  class Matrix : IComparable {
    protected int[,] matrix;
    protected int rowAndLineCount;
    public Matrix() {
      Random random = new Random();
      rowAndLineCount = random.Next(2, 5);
      matrix = new int[rowAndLineCount, rowAndLineCount];
      for (int rowIndex = 0; rowIndex < rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < rowAndLineCount; ++columnIndex) {
          matrix[rowIndex, columnIndex] = random.Next(0, 100);
        }
      }
    }
    public object Clone() {
      Matrix clonedRowCount = new Matrix();
      clonedRowCount.rowAndLineCount = this.rowAndLineCount;
      for (int rowIndex = 0; rowIndex < this.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < rowAndLineCount; ++columnIndex) {
          clonedRowCount.matrix[rowIndex, columnIndex] = this.matrix[rowIndex, columnIndex];
        }
      }
      this.matrix[0, 0] = 0;
      return clonedRowCount;
    }
    public static Matrix operator +(Matrix first, Matrix second) {
      Matrix resultMatrix = (Matrix)first.Clone();
      try {
        for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
          for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
            resultMatrix.matrix[rowIndex, columnIndex] = first.matrix[rowIndex, columnIndex] + second.matrix[rowIndex, columnIndex];
          }
        }
      } catch (IndexOutOfRangeException exception) {
        Console.WriteLine(exception.Message);
      }
      resultMatrix.ShowMatrix(resultMatrix);
      return resultMatrix;
    }

    public static Matrix operator -(Matrix first, Matrix second) {
      Matrix resultMatrix = (Matrix)first.Clone();
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          resultMatrix.matrix[rowIndex, columnIndex] = first.matrix[rowIndex, columnIndex] - second.matrix[rowIndex, columnIndex];
        }
      }
      resultMatrix.ShowMatrix(resultMatrix);
      return resultMatrix;
    }

    public static Matrix operator *(Matrix first, Matrix second) {
      Matrix resultMatrix = (Matrix)first.Clone();
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        Console.Write("\n");
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          for (int innerIndex = 0; innerIndex < first.rowAndLineCount; ++innerIndex) {
            resultMatrix.matrix[rowIndex, columnIndex] += first.matrix[rowIndex, innerIndex] * second.matrix[innerIndex, columnIndex];
          }
          Console.Write(resultMatrix.matrix[rowIndex, columnIndex] + " ");
          resultMatrix.matrix[rowIndex, columnIndex] = 0;
        }
      }
      return resultMatrix;
    }

    public static Matrix operator +(Matrix inputMatrix) {
      Matrix resultMatrix = (Matrix)inputMatrix.Clone();
      try {
        for (int rowIndex = 0; rowIndex < inputMatrix.rowAndLineCount; ++rowIndex) {
          Console.Write("\n");
          for (int columnIndex = 0; columnIndex < inputMatrix.rowAndLineCount; ++columnIndex) {
            Console.Write(inputMatrix.matrix[columnIndex, rowIndex] + " ");
          }
        }
      } catch (IndexOutOfRangeException exception) {
        Console.WriteLine(exception.Message);
      }
      return resultMatrix;
    }
    public void ShowMatrix(Matrix first) {
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        Console.Write("\n");
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          Console.Write(matrix[rowIndex, columnIndex].ToString() + " ");
        }
      }
      Console.Write("\n");
    }
    public static int operator !(Matrix inputMatrix) {
      int determinant = 0;
      switch (inputMatrix.rowAndLineCount) {
        case 2:
          determinant = inputMatrix.matrix[0, 0] * inputMatrix.matrix[1, 1] - inputMatrix.matrix[0, 1] * inputMatrix.matrix[1, 0];
          break;
        case 3:
          determinant = (inputMatrix.matrix[0, 0] * inputMatrix.matrix[1, 1] * inputMatrix.matrix[2, 2] +
                        inputMatrix.matrix[0, 2] * inputMatrix.matrix[1, 0] * inputMatrix.matrix[2, 1] +
                        inputMatrix.matrix[0, 1] * inputMatrix.matrix[1, 2] * inputMatrix.matrix[2, 0]) -
                        (inputMatrix.matrix[0, 2] * inputMatrix.matrix[1, 1] * inputMatrix.matrix[2, 0] +
                        inputMatrix.matrix[0, 1] * inputMatrix.matrix[1, 0] * inputMatrix.matrix[2, 2] +
                        inputMatrix.matrix[0, 0] * inputMatrix.matrix[1, 2] * inputMatrix.matrix[2, 1]);
          break;
        default:
          Console.WriteLine("Can not find determinant");
          break;
      }
      return determinant;
    }

    public static bool operator ==(Matrix first, Matrix second) {
      byte check = 0;
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          if (first.matrix[rowIndex, columnIndex] != second.matrix[rowIndex, columnIndex]) {
            ++check;
          };
        }
      }
      if (check == 0) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator >(Matrix first, Matrix second) {
      int result = 0;
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          result = result + first.matrix[rowIndex, columnIndex] * first.matrix[rowIndex, columnIndex] - second.matrix[rowIndex, columnIndex] * second.matrix[rowIndex, columnIndex];
        }
      }
      return result > 0;
    }

    public static bool operator <(Matrix first, Matrix second) {
      int result = 0;
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          result = result + first.matrix[rowIndex, columnIndex] * first.matrix[rowIndex, columnIndex] - second.matrix[rowIndex, columnIndex] * second.matrix[rowIndex, columnIndex];
        }
      }
      return result < 0;
    }
    public static bool operator !=(Matrix first, Matrix second) {
      byte check = 0;
      for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
        for (int columnIndex = 0; columnIndex < first.rowAndLineCount; ++columnIndex) {
          if (first.matrix[rowIndex, columnIndex] != second.matrix[rowIndex, columnIndex]) {
            ++check;
          };
        }
      }
      if (check != 0) {
        return true;
      } else {
        return false;
      }
    }
    public static bool operator >=(Matrix first, Matrix second) {
      return !(first < second);
    }
    public static bool operator <=(Matrix first, Matrix second) {
      return !(first > second);
    }
    public override bool Equals(object myObject) {
      bool result = false;
      if (myObject is Matrix) {
        var parameter = myObject as Matrix;
        if (parameter.rowAndLineCount == this.rowAndLineCount) {
          result = true;
        }
      }
      return result;
    }
    public override int GetHashCode() {
      return (int)this.rowAndLineCount;
    }
    public static implicit operator int[,](Matrix inputMatrix) {
      return inputMatrix.matrix;
    }
    public static explicit operator int(Matrix inputMatrix) {
      return inputMatrix.rowAndLineCount;
    }
    public static bool operator true(Matrix inputMatrix) {
      return inputMatrix.rowAndLineCount != 1;
    }
    public static bool operator false(Matrix inputMatrix) {
      return inputMatrix.rowAndLineCount == 1;
    }
    int IComparable.CompareTo(object myObject) {
      if (myObject is Matrix) {
        var parameter = myObject as Matrix;
        if (parameter.rowAndLineCount > this.rowAndLineCount) {
          Console.WriteLine("1 case");
          return -1;
        }
        if (parameter.rowAndLineCount == this.rowAndLineCount) {
          Console.WriteLine("2 case");
          return 0;
        }
        if (parameter.rowAndLineCount < this.rowAndLineCount) {
          Console.WriteLine("3 case");
          return 1;
        }
      }
      Console.WriteLine("4 case");
      return -1;
    }

    public int ChangeOrShowRowAndLineCount {
      get { return rowAndLineCount; }
      set { rowAndLineCount = value; }
    }
  }

  internal class Program {

    static void Main(string[] args) {
      char select = ' ';
      Matrix myMatrix = new Matrix();
      Matrix myCloneMatrix = (Matrix)myMatrix.Clone();
      myMatrix.ShowMatrix(myMatrix);
      myCloneMatrix.ShowMatrix(myCloneMatrix);
      Console.WriteLine("Select your operation: s = summ, d = difference, c = compare, m = multiplication, " +
                        "r = reverse matrix, f = determinant.");
      select = Convert.ToChar(Console.ReadLine());
      switch (select) {
        case 's':
          Console.WriteLine((myMatrix + myCloneMatrix).ToString() + "\n");
          break;
        case 'd':
          Console.WriteLine((myMatrix - myCloneMatrix).ToString() + "\n");
          break;
        case 'm':
          Console.WriteLine((myMatrix * myCloneMatrix).ToString() + "\n");
          break;
        case 'r':
          Console.WriteLine((+myCloneMatrix).ToString() + "\n");
          break;
        case 'f':
          Console.WriteLine((!myCloneMatrix).ToString() + "\n");
          break;
        case 'c':
          Console.WriteLine((myMatrix == myCloneMatrix).ToString() + "\n");
          break;
        default:
          Console.WriteLine("Wrong file type");
          break;
      }

      Console.ReadKey();
    }

  }

}
