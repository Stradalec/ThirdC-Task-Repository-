using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdC_Task {

  public class InvalidDepartmentException : System.Exception {
    public InvalidDepartmentException()
    : base() { }
    public InvalidDepartmentException(string message)
    : base(message) { }
    public InvalidDepartmentException
    (string message, System.Exception inner)
    : base(message, inner) { }

  }
  class Matrix {
    protected int[,] matrix;
    protected int rowAndLineCount;
    public Matrix() {
      Random random = new Random();
      rowAndLineCount = random.Next(2, 5);
      matrix = new int[rowAndLineCount, rowAndLineCount];

    }
    public void SetMatrix() {
      Random random = new Random();
      for (int lineIndex = 0; lineIndex < rowAndLineCount; ++lineIndex) {
        for (int rowIndex = 0; rowIndex < rowAndLineCount; ++rowIndex) {
          matrix[lineIndex, rowIndex] = random.Next(0, 100);
        }
      }
    }
  }

  class CloneMatrix : Matrix, ICloneable, IComparable {

    public static CloneMatrix operator +(CloneMatrix first, CloneMatrix second) {
      CloneMatrix resultMatrix = new CloneMatrix();
      try {
        for (int lineIndex = 0; lineIndex < second.rowAndLineCount; ++lineIndex) {
          for (int rowIndex = 0; rowIndex < second.rowAndLineCount; ++rowIndex) {
            resultMatrix.matrix[lineIndex, rowIndex] = first.matrix[lineIndex, rowIndex] + second.matrix[lineIndex, rowIndex];
          }
        }
      } catch (IndexOutOfRangeException exception) {
        Console.WriteLine(exception.Message);
      }
      resultMatrix.ShowMatrix();
      return resultMatrix;
    }

    public static CloneMatrix operator -(CloneMatrix first, CloneMatrix second) {
      CloneMatrix resultMatrix = new CloneMatrix();
      for (int lineIndex = 0; lineIndex < first.rowAndLineCount; ++lineIndex) {
        for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
          resultMatrix.matrix[lineIndex, rowIndex] = first.matrix[lineIndex, rowIndex] - second.matrix[lineIndex, rowIndex];
        }
      }
      resultMatrix.ShowMatrix();
      return resultMatrix;
    }

    public static CloneMatrix operator *(CloneMatrix first, CloneMatrix second) {
      CloneMatrix resultMatrix = new CloneMatrix();
      for (int lineIndex = 0; lineIndex < first.rowAndLineCount; ++lineIndex) {
        Console.Write("\n");
        for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
          resultMatrix.matrix[lineIndex, rowIndex] = first.matrix[lineIndex, rowIndex] * second.matrix[lineIndex, rowIndex];
        }
      }
      resultMatrix.ShowMatrix();
      return resultMatrix;
    }

    public static CloneMatrix operator +(CloneMatrix inputMatrix) {
      CloneMatrix resultMatrix = new CloneMatrix();
      try {
        for (int lineIndex = 0; lineIndex < inputMatrix.rowAndLineCount; ++lineIndex) {
          for (int rowIndex = 0; rowIndex < inputMatrix.rowAndLineCount; ++rowIndex) {
            resultMatrix.matrix[lineIndex, rowIndex] = inputMatrix.matrix[rowIndex, lineIndex];
          }
        }
      } catch (IndexOutOfRangeException exception) {
        Console.WriteLine(exception.Message);
      }
      resultMatrix.ShowMatrix();
      return resultMatrix;
    }
    public static int operator !(CloneMatrix inputMatrix) {
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

    public static bool operator ==(CloneMatrix first, CloneMatrix second) {
      byte check = 0;
      for (int lineIndex = 0; lineIndex < first.rowAndLineCount; ++lineIndex) {
        for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
          if (first.matrix[lineIndex, rowIndex] != second.matrix[lineIndex, rowIndex]) {
            ++check;
          };
        }
      }
      if (check != 0) {
        return false;
      } else {
        return true;
      }
    }

    public static bool operator !=(CloneMatrix first, CloneMatrix second) {
      byte check = 0;
      for (int lineIndex = 0; lineIndex < first.rowAndLineCount; ++lineIndex) {
        for (int rowIndex = 0; rowIndex < first.rowAndLineCount; ++rowIndex) {
          if (first.matrix[lineIndex, rowIndex] != second.matrix[lineIndex, rowIndex]) {
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
    public override bool Equals(object myObject) {
      bool result = false;
      if (myObject is CloneMatrix) {
        var parameter = myObject as CloneMatrix;
        if (parameter.rowAndLineCount == this.rowAndLineCount) {
          result = true;
        }
      }
      return result;
    }
    public override int GetHashCode() {
      return (int)this.rowAndLineCount;
    }

    int IComparable.CompareTo(object myObject) {
      if (myObject is CloneMatrix) {
        var parameter = myObject as CloneMatrix;
        if (parameter.rowAndLineCount > this.rowAndLineCount) {
          return -1;
        }
        if (parameter.rowAndLineCount == this.rowAndLineCount) {
          return 0;
        }
        if (parameter.rowAndLineCount < this.rowAndLineCount) {
          return 1;
        }
      }
      return -1;
    }

    public int ChangeOrShowRowAndLineCount {
      get { return rowAndLineCount; }
      set { rowAndLineCount = value; }
    }


    public void ShowMatrix() {
      for (int lineIndex = 0; lineIndex < rowAndLineCount; ++lineIndex) {
        Console.Write("\n");
        for (int rowIndex = 0; rowIndex < rowAndLineCount; ++rowIndex) {
          Console.Write(matrix[lineIndex, rowIndex].ToString() + " ");
        }
      }
      Console.Write("\n");
    }
    public object Clone() {
      CloneMatrix clonedRowCount = new CloneMatrix();
      clonedRowCount.rowAndLineCount = this.rowAndLineCount;
      return clonedRowCount;
    }
  }
  internal class Program {

    static void Main(string[] args) {
      char select = ' ';
      CloneMatrix myMatrix = new CloneMatrix();
      CloneMatrix myCloneMatrix = (CloneMatrix)myMatrix.Clone();
      myMatrix.SetMatrix();
      myMatrix.ShowMatrix();
      myCloneMatrix.SetMatrix();
      myCloneMatrix.ShowMatrix();
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
          Console.WriteLine((+myMatrix).ToString() + "\n");
          break;
        case 'f':
          Console.WriteLine((!myMatrix).ToString() + "\n");
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
