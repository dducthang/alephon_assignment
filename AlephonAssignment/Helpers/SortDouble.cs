﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlephonAssignment.Helpers
{
    public class SortDouble
    {
        public void HeapSort(double[] arr)
        {
            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                double temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;

                Heapify(arr, i, 0);
            }
        }

        public void Heapify(double[] arr, int n, int i)
        {
            int largest = i;
            int leftChild = 2 * i + 1;
            int rightChild = 2 * i + 2;

            if (leftChild < n && arr[leftChild] > arr[largest])
                largest = leftChild;

            if (rightChild < n && arr[rightChild] > arr[largest])
                largest = rightChild;

            if (largest != i)
            {
                double temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;

                Heapify(arr, n, largest);
            }
        }
    }
}
