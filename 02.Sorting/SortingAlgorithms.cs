
using System;

namespace SortingAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 10, 7, 8, 9, 1, 5 };
            int n = arr.Length;

            MergeSort(arr, 0, n - 1);

            Console.WriteLine(String.Join(" ", arr));

        }

        static int[] InsertionSort(int[] array)
        {
            int index = 0;

            for (int i = 1; i < array.Length; i++)
            {
                index = i;

                while (index > 0 && array[index] < array[index - 1])
                {
                    int temp = array[index - 1];
                    array[index - 1] = array[index];
                    array[index] = temp;
                    index--;
                }
            }

            return array;
        }


        static int[] SelectionSort(int[] array)
        {
            int elementToCompare = 0;

            for(int i = 0; i < array.Length - 1; i++)
            {
                elementToCompare = array[i];

                for(int k = i + 1; k < array.Length; k++)
                {
                    if(elementToCompare > array[k])
                    {
                        int temp = elementToCompare;
                        array[i] = array[k];
                        array[k] = temp;
                        elementToCompare = array[i];
                    }
                }
            }

            return array;
        }
static int[] BubbleSort(int[] array)
        {
            for(int i = 0; i < array.Length - 1; i++)
            {
                for(int k = 0; k < array.Length - 1; k++)
                {
                    if (array[k] > array[k + 1])
                    {
                        int temp = array[k];
                        array[k] = array[k + 1];
                        array[k + 1] = temp;
                    }
                }
            }

            return array;
        }




        static void Swap(int[] array, int i, int k)
        {
            int temp = array[i];
            array[i] = array[k];
            array[k] = temp;
        }

        static int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;

            for(int k = low; k < array.Length - 1; k++)
            {
                if (array[k] < pivot)
                {
                    i++;
                    Swap(array, i, k);
                }
            }

            Swap(array, i + 1, high);
            return i + 1;

        }


        static void QuickSort(int[] array, int low, int high)
        {
            if(low < high)
            {
                int pi = Partition(array, low, high);
                QuickSort(array, low, pi - 1);
                QuickSort(array, pi + 1, high);
            }    
        }
 static void Merge(int[] arr, int l, int m, int r)
        {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            var L = new int[n1];

            for(i = 0; i < n1; i++)
            {
                L[i] = arr[l + i];

            }

            var R = new int[n2];

            for(j = 0; j < n2; j++)
            {
                R[j] = arr[m + 1 + j];
            }

            i = j = 0;
            k = l;

            while(i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i++];
                }
                else
                {
                    arr[k] = R[j++];    
                }

                k++;
            }

            while(i < n1)
            {
                arr[k++] = L[i++];
            }

            while(j < n2)
            {
                arr[k++] = R[j++];
            }

        }


        static void MergeSort(int[] arr, int l, int r)
        {
            if(l < r)
            {
                int m = l + (r - l) / 2;

                MergeSort(arr, l, m);
                MergeSort(arr, m + 1, r);

                Merge(arr, l, m, r);
            }
        }
    }
}

