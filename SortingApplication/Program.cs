using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApplication
{
    class Program
    {
        public static bool HasTwoNodesSum(int[] array, int arrayLen, int sum)
        {
            return HasTwoNodesSum(array, 0, arrayLen - 1, sum);
        }

        public static bool HasTwoNodesSum(int[] sortedArray, int index1, int index2, int sum)
        {
            if (index1 < index2)
            {
                if (sortedArray[index1] + sortedArray[index2] == sum)
                    return true;

                if (sortedArray[index1] + sortedArray[index2] < sum)
                    return HasTwoNodesSum(sortedArray, index1+1, index2, sum);

                return HasTwoNodesSum(sortedArray, index1, index2-1, sum);
            }
            return false;
        }

        //better way is to reverse all the chars in the string and then reverse the characters in each word
        public static string Reverse(string query)
        {
            var strList = query.Split(' ');
            var result = "";
            int i;
            for ( i = strList.Length - 1; i > 0; i--)
            {
                result += (strList[i] + " ");
            }
            result += strList[i];

            return result;
        }


        static void Main(string[] args)
        {
            var array = new[] {5, 4, 3, 10, 15, 50, 11};
            var sortedArray = MergeSort(array.ToList());
            //QuickSort(array, 0, array.Length - 1);
            Console.WriteLine("Sorted Array: " + string.Join(", ", sortedArray));
            Console.WriteLine(HasTwoNodesSum(array, array.Length, 25));
            Console.WriteLine(HasTwoNodesSum(array, array.Length, 15));
            Console.WriteLine(HasTwoNodesSum(array, array.Length, 50));

            Console.WriteLine(Reverse("I am a student."));

            Console.ReadLine();
        }

        public static List<int> MergeSort(List<int> array)
        {
            int n = array.Count;

            if (n == 1)
                return array;

            var midPoint = n/2;

            var leftSide = MergeSort(array.Take(midPoint).ToList());
            var rightSide = MergeSort(array.Skip(midPoint).ToList());

            return Merge(leftSide, rightSide);

        }

        public static List<int> Merge(List<int> a, List<int> b)
        {
            var mergedArray = new List<int>();

            while (a.Count > 0 && b.Count > 0)
            {
                if (a[0] < b[0])
                {
                    mergedArray.Add(a[0]);
                    a.RemoveAt(0);
                }
                else
                {
                    mergedArray.Add(b[0]);
                    b.RemoveAt(0);
                }
            }

            while (a.Count > 0)
            {
                mergedArray.Add(a[0]);
                a.RemoveAt(0);
            }

            while (b.Count > 0)
            {
                mergedArray.Add(b[0]);
                b.RemoveAt(0);
            }

            return mergedArray;
        } 

        public static void QuickSort(int[] array, int low, int high)
        {
            if (low >= high)
                return;

            var p = Partition(array, low, high);
            QuickSort(array, low, p-1);
            QuickSort(array, p+1, high);

        }

        public static void Swap(int[] array, int index1, int index2)
        {
            if (index1 != index2)
            {
                array[index1] = array[index1] ^ array[index2];
                array[index2] = array[index1] ^ array[index2];
                array[index1] = array[index1] ^ array[index2];
            }
        }

        public static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low;
            for (int j = low; j < high; j++)
            {
                if (array[j] <= pivot)
                {
                    Swap(array, i, j);
                    i++;
                }
            }

            Swap(array, i, high);
            
            return i;
        }

        
    }
}
