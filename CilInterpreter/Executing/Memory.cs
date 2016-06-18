using CilInterpreter.Assemblies.cili.Types;
using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts;
using System;
using System.Collections.Generic;

namespace CilInterpreter.Executing
{
    class Memory
    {
        const int heapSize = 1024 * 1024;
        const int localSize = 1024 * 1024;

        byte[] heap = new byte[heapSize];
        byte[] local = new byte[localSize];
        int heapPointer = 0;
        public int LocalPointer { get { return _localPointer + heapSize; } }
        private int _localPointer { get; set; } = 0;

        public byte this[int position]
        {
            get
            {
                if (position < heapSize)
                    return heap[position];
                else if (position < heapSize + localSize)
                    return local[position - heapSize];
                else
                    throw new CiliRuntimeException("Address is too big");
            }
            set
            {
                if (position < heapSize)
                    heap[position] = value;
                else if (position < heapSize + localSize)
                    local[position - heapSize] = value;
                else
                    throw new CiliRuntimeException("Address is too big");
            }
        }

        public void AddLocal(int count)
        {
            _localPointer += count;
            if (_localPointer >= local.Length)
                throw new CiliRuntimeException("Method's local memory is overloaded.");
        }

        public void RemoveLocal(int count)
        {
            _localPointer -= count;
            if (_localPointer < 0)
                throw new CiliRuntimeException("Local memory pointer is set to a negative value.");
        }

        public byte[] this[int startPosition, int endPosition]
        {
            get
            {
                int size = endPosition - startPosition;
                byte[] result = new byte[size];
                if (startPosition >= heapSize)
                {
                    startPosition -= heapSize;
                    endPosition -= heapSize;
                    for (int i = 0; i < size; i++)
                        result[i] = local[startPosition + i];
                }
                else
                    for (int i = 0; i < size; i++)
                        result[i] = heap[startPosition + i];
                return result;
            }
            set
            {
                int size = endPosition - startPosition;
                byte[] result = new byte[size];
                if (startPosition >= heapSize)
                {
                    startPosition -= heapSize;
                    endPosition -= heapSize;
                    for (int i = 0; i < size; i++)
                        local[startPosition + i] = value[i];
                }
                else
                    for (int i = 0; i < size; i++)
                        heap[startPosition + i] = value[i];
            }
        }



        public int LoadObject(int typeIndex)
        {
            int result = heapPointer;
            var index = BitConverter.GetBytes(typeIndex);
            for (int i = 0; i < 4; i++)
                heap[heapPointer + i] = index[i];
            int size = TypeMap[typeIndex].FullSize;
            if (heapPointer + size > heap.Length)
                GarbageCollection();                
            heapPointer += size;
            return result;
        }

        public int LoadArray(int typeIndex, int size, bool isReferenced)
        {
            int result = heapPointer;
            var index = BitConverter.GetBytes(typeIndex);
            for (int i = 0; i < 4; i++)
                heap[heapPointer + i] = index[i];
            ArrayType array = TypeMap[typeIndex] as ArrayType;
            int fullSize = 8;
            if (isReferenced)
                fullSize += 4 * size;
            else
                fullSize += array.InternalType.FullSize * size;
            if (heapPointer + size > heap.Length)
                GarbageCollection();
            byte[] sizeBytes = BitConverter.GetBytes(size);
            for (int i = 0; i < 4; i++)
            {
                heap[heapPointer + 4 + i] = sizeBytes[i];
            }
            heapPointer += fullSize;
            return result;
        }

        public void GarbageCollection() { }

        public List<CilType> TypeMap = new List<CilType>();
    }
}
