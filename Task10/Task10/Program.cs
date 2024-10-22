using HeapLibrary;
int[] array = new int[] {10,2,4,1,6};
Heap<int> heap = new Heap<int>(array);
heap.RemoveMax();
heap.Output();
