using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    T[] myItems;
    int myCurrentItemCount;

    public Heap(int aMaxHeapSize)
    {
        myItems = new T[aMaxHeapSize];
    }

    public void Add(T anItem)
    {
        anItem.myHeapIndex = myCurrentItemCount;
        myItems[myCurrentItemCount] = anItem;
        SortUp(anItem);
        myCurrentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = myItems[0];
        myCurrentItemCount--;
        myItems[0] = myItems[myCurrentItemCount];
        myItems[0].myHeapIndex = 0;
        SortDown(myItems[0]);

        return firstItem;
    }

    public bool Contains(T anItem)
    {
        return Equals(myItems[anItem.myHeapIndex], anItem);
    }

    public int Count
    {
        get
        {
            return myCurrentItemCount;
        }
    }

    public void UpdateItem(T anItem)
    {
        SortUp(anItem);
    }

    void SortDown(T anItem)
    {
        while (true)
        {
            int childIndexLeft = anItem.myHeapIndex * 2 + 1;
            int childIndexRight = anItem.myHeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < myCurrentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < myCurrentItemCount)
                {
                    if (myItems[childIndexLeft].CompareTo(myItems[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (anItem.CompareTo(myItems[swapIndex]) < 0)
                {
                    Swap(anItem, myItems[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void SortUp(T anItem)
    {
        int parentIndex = (anItem.myHeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = myItems[parentIndex];
            if (anItem.CompareTo(parentItem) > 0)
            {
                Swap(anItem, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (anItem.myHeapIndex - 1) / 2;
        }
    }

    void Swap(T anItemA, T anItemB)
    {
        myItems[anItemA.myHeapIndex] = anItemB;
        myItems[anItemB.myHeapIndex] = anItemA;
        int itemAHeapIndex = anItemA.myHeapIndex;
        anItemA.myHeapIndex = anItemB.myHeapIndex;
        anItemB.myHeapIndex = itemAHeapIndex;
    }

}

public interface IHeapItem<T> : IComparable<T>
{
    public int myHeapIndex { get; set; }
}
