using System.Runtime.CompilerServices;

namespace Maptage.Core.Utils;

public static class SpanSortHelper<T>
{
    private static void SwapIfGreater(Span<T> keys, IComparer<T> comparer, int i, int j)
    {
        if (comparer.Compare(keys[i], keys[j]) <= 0)
            return;
        (keys[i], keys[j]) = (keys[j], keys[i]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Swap(Span<T> a, int i, int j)
        => (a[i], a[j]) = (a[j], a[i]);
    public static void IntroSort(Span<T> keys, int depthLimit, IComparer<T> comparer)
    {
        int num1;
        for (var length1 = keys.Length; length1 > 1; length1 = num1)
        {
            if (length1 <= 16)
            {
                if (length1 == 2)
                {
                    SwapIfGreater(keys, comparer, 0, 1);
                    break;
                }

                if (length1 == 3)
                {
                    SwapIfGreater(keys, comparer, 0, 1);
                    SwapIfGreater(keys, comparer, 0, 2);
                    SwapIfGreater(keys, comparer, 1, 2);
                    break;
                }

                InsertionSort(keys[..length1], comparer);
                break;
            }

            if (depthLimit == 0)
            {
                HeapSort(keys[..length1], comparer);
                break;
            }

            --depthLimit;
            num1 = PickPivotAndPartition(keys[..length1], comparer);
            ref var local = ref keys;
            var num2 = num1 + 1;
            var start = num2;
            var length2 = length1 - num2;
            IntroSort(local.Slice(start, length2), depthLimit, comparer);
        }
    }
    private static int PickPivotAndPartition(Span<T> keys, IComparer<T> comparer)
    {
        var j1 = keys.Length - 1;
        var num = j1 >> 1;
        SwapIfGreater(keys, comparer, 0, num);
        SwapIfGreater(keys, comparer, 0, j1);
        SwapIfGreater(keys, comparer, num, j1);
        var obj = keys[num];
        Swap(keys, num, j1 - 1);
        var i = 0;
        var j2 = j1 - 1;
        while (i < j2)
        {
            do
            {}
            while (comparer.Compare(keys[++i], obj) < 0);
            do
            {}
            while (comparer.Compare(obj, keys[--j2]) < 0);
            if (i < j2)
                Swap(keys, i, j2);
            else
                break;
        }

        if (i != j1 - 1)
            Swap(keys, i, j1 - 1);
        return i;
    }
    private static void HeapSort(Span<T> keys, IComparer<T> comparer)
    {
        var length = keys.Length;
        for (var i = length >> 1; i >= 1; --i)
            DownHeap(keys, i, length, comparer);
        for (var index = length; index > 1; --index)
        {
            Swap(keys, 0, index - 1);
            DownHeap(keys, 1, index - 1, comparer);
        }
    }
    private static void DownHeap(Span<T> keys, int i, int n, IComparer<T> comparer)
    {
        var x = keys[i - 1];
        int index;
        for (; i <= n >> 1; i = index)
        {
            index = 2 * i;
            if (index < n && comparer.Compare(keys[index - 1], keys[index]) < 0)
                ++index;
            if (comparer.Compare(x, keys[index - 1]) < 0)
                keys[i - 1] = keys[index - 1];
            else
                break;
        }

        keys[i - 1] = x;
    }
    private static void InsertionSort(Span<T> keys, IComparer<T> comparer)
    {
        for (var index1 = 0; index1 < keys.Length - 1; ++index1)
        {
            var x = keys[index1 + 1];
            int index2;
            for (index2 = index1; index2 >= 0 && comparer.Compare(x, keys[index2]) < 0; --index2)
                keys[index2 + 1] = keys[index2];
            keys[index2 + 1] = x;
        }
    }
}
