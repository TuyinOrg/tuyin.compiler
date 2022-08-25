using compute.environment;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace compute.utils
{
    static class MeshTools
    {
        class Pointer
        {
            private GCHandle _handle;

            public IntPtr Address { get; private set; }

            private Pointer()
            {
            }

            public void Free()
            {
                _handle.Free();
            }

            public static Pointer Create<T>(T Object)
            {
                var pointer = new Pointer
                {
                    _handle = GCHandle.Alloc(Object, GCHandleType.Pinned)
                };
                pointer.Address = pointer._handle.AddrOfPinnedObject();
                return pointer;
            }
        }

        class Libaray : EnviromentLibrary
        {
            public Libaray() 
                : base("MeshOptimizer") 
            {
            }

            protected override IEnumerable<string> GetLinuxLibraries()
            {
                yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\gltfpack.so";
            }

            protected override IEnumerable<string> GetOSXLibraries()
            {
                yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\gltfpack.dylib";
            }

            protected override IEnumerable<string> GetWindowsLibraries()
            {
                yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\gltfpack.exe";
            }
        }


        static class MeshOptimizerNative
        {
            private readonly static Libaray sExternLibrary = new Libaray();

            private delegate UInt32 meshopt_generateVertexRemapDelegate0(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize);
            private static readonly meshopt_generateVertexRemapDelegate0 meshopt_generateVertexRemap0 = sExternLibrary.GetStaticProc<meshopt_generateVertexRemapDelegate0>("meshopt_generateVertexRemap");
            private static UInt32 meshopt_generateVertexRemap(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize) { return meshopt_generateVertexRemap0(Destination, Indices, IndexCount, Vertices, VertexCount, VertexSize); }
            private delegate void meshopt_remapIndexBufferDelegate0(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, UInt32[] Remap);
            private static readonly meshopt_remapIndexBufferDelegate0 meshopt_remapIndexBuffer0 = sExternLibrary.GetStaticProc<meshopt_remapIndexBufferDelegate0>("meshopt_remapIndexBuffer");
            private static void meshopt_remapIndexBuffer(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, UInt32[] Remap) { meshopt_remapIndexBuffer0(Destination, Indices, IndexCount, Remap); }
            private delegate void meshopt_remapVertexBufferDelegate0(IntPtr Destination, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize, UInt32[] Remap);
            private static readonly meshopt_remapVertexBufferDelegate0 meshopt_remapVertexBuffer0 = sExternLibrary.GetStaticProc<meshopt_remapVertexBufferDelegate0>("meshopt_remapVertexBuffer");
            private static void meshopt_remapVertexBuffer(IntPtr Destination, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize, UInt32[] Remap) { meshopt_remapVertexBuffer0(Destination, Vertices, VertexCount, VertexSize, Remap); }
            private delegate void meshopt_optimizeVertexCacheDelegate0(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, UIntPtr VertexCount);
            private static readonly meshopt_optimizeVertexCacheDelegate0 meshopt_optimizeVertexCache0 = sExternLibrary.GetStaticProc<meshopt_optimizeVertexCacheDelegate0>("meshopt_optimizeVertexCache");
            private static void meshopt_optimizeVertexCache(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, UIntPtr VertexCount) { meshopt_optimizeVertexCache0(Destination, Indices, IndexCount, VertexCount); }
            private delegate void meshopt_optimizeOverdrawDelegate0(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr VertexPositions, UIntPtr VertexCount, UIntPtr Stride, Single Threshold);
            private static readonly meshopt_optimizeOverdrawDelegate0 meshopt_optimizeOverdraw0 = sExternLibrary.GetStaticProc<meshopt_optimizeOverdrawDelegate0>("meshopt_optimizeOverdraw");
            private static void meshopt_optimizeOverdraw(UInt32[] Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr VertexPositions, UIntPtr VertexCount, UIntPtr Stride, Single Threshold) { meshopt_optimizeOverdraw0(Destination, Indices, IndexCount, VertexPositions, VertexCount, Stride, Threshold); }
            private delegate UInt32 meshopt_optimizeVertexFetchDelegate0(IntPtr Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize);
            private static readonly meshopt_optimizeVertexFetchDelegate0 meshopt_optimizeVertexFetch0 = sExternLibrary.GetStaticProc<meshopt_optimizeVertexFetchDelegate0>("meshopt_optimizeVertexFetch");
            private static UInt32 meshopt_optimizeVertexFetch(IntPtr Destination, UInt32[] Indices, UIntPtr IndexCount, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize) { return meshopt_optimizeVertexFetch0(Destination, Indices, IndexCount, Vertices, VertexCount, VertexSize); }

            public static uint GenerateVertexRemap(uint[] Destination, uint[] Indices, UIntPtr IndexCount, IntPtr Vertices,
              UIntPtr VertexCount, UIntPtr VertexSize)
            {
                return meshopt_generateVertexRemap(Destination, Indices, IndexCount, Vertices, VertexCount, VertexSize);
            }

            public static void RemapIndexBuffer(uint[] Destination, uint[] Indices, UIntPtr IndexCount, uint[] Remap)
            {
                meshopt_remapIndexBuffer(Destination, Indices, IndexCount, Remap);
            }

            public static void RemapVertexBuffer(IntPtr Destination, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize, uint[] Remap)
            {
                meshopt_remapVertexBuffer(Destination, Vertices, VertexCount, VertexSize, Remap);
            }

            public static void OptimizeVertexCache(uint[] Destination, uint[] Indices, UIntPtr IndexCount, UIntPtr VertexCount)
            {
                meshopt_optimizeVertexCache(Destination, Indices, IndexCount, VertexCount);
            }

            public static void OptimizeOverdraw(uint[] Destination, uint[] Indices, UIntPtr IndexCount, IntPtr VertexPositions, UIntPtr VertexCount, UIntPtr Stride, float Threshold)
            {
                meshopt_optimizeOverdraw(Destination, Indices, IndexCount, VertexPositions, VertexCount, Stride, Threshold);
            }

            public static uint OptimizeVertexFetch(IntPtr Destination, uint[] Indices, UIntPtr IndexCount, IntPtr Vertices, UIntPtr VertexCount, UIntPtr VertexSize)
            {
                return meshopt_optimizeVertexFetch(Destination, Indices, IndexCount, Vertices, VertexCount, VertexSize);
            }
        }

        /// <summary>
        /// Executes the "standard" optimizations that are suggested in the meshoptimizer README
        /// See (https://github.com/zeux/meshoptimizer#pipeline)
        /// </summary>
        /// <param name="Vertices">The vertices of the mesh</param>
        /// <param name="Indices">The indices of the mesh</param>
        /// <param name="VertexSize">The size of T in bytes</param>
        /// <typeparam name="T">The vertex type</typeparam>
        /// <returns>A tuple with the new vertices and indices</returns>
        public static Tuple<T[], uint[]> Optimize<T>(T[] Vertices, uint[] Indices, uint VertexSize)
        {
            var results = Reindex(Vertices, Indices, VertexSize);
            var vertices = results.Item1;
            var indices = results.Item2;

            OptimizeCache(indices, vertices.Length);
            OptimizeOverdraw(indices, vertices, VertexSize, 1.05f);
            OptimizeVertexFetch(indices, vertices, VertexSize);
            return Tuple.Create(vertices, indices);
        }

        /// <summary>
        /// Reindex the given mesh. See (https://github.com/zeux/meshoptimizer#indexing)
        /// </summary>
        /// <param name="Vertices">The vertices of the mesh</param>
        /// <param name="Indices">The indices of the mesh</param>
        /// <param name="VertexSize">The size of T in bytes</param>
        /// <typeparam name="T">The vertex type</typeparam>
        /// <returns>A tuple with the new vertices and indices</returns>
        public static Tuple<T[], uint[]> Reindex<T>(T[] Vertices, uint[] Indices, uint VertexSize)
        {
            var remap = new uint[Vertices.Length];
            var vertexPointer = Pointer.Create(Vertices);
            var indexCount = (Indices?.Length ?? Vertices.Length);
            var totalVertices = MeshOptimizerNative.GenerateVertexRemap(
                remap,
                Indices,
                (UIntPtr)indexCount,
                vertexPointer.Address,
                (UIntPtr)Vertices.Length,
                (UIntPtr)VertexSize
            );

            var indices = new uint[indexCount];
            MeshOptimizerNative.RemapIndexBuffer(indices, Indices, (UIntPtr)indexCount, remap);

            var vertices = new T[totalVertices];
            var targetVerticesPointer = Pointer.Create(vertices);
            MeshOptimizerNative.RemapVertexBuffer(targetVerticesPointer.Address, vertexPointer.Address, (UIntPtr)Vertices.Length, (UIntPtr)VertexSize, remap);

            vertexPointer.Free();
            targetVerticesPointer.Free();
            return Tuple.Create(vertices, indices);
        }

        /// <summary>
        /// Optimizes the mesh for the GPU cache. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
        /// </summary>
        /// <param name="Indices">The indices of the mesh</param>
        /// <param name="VertexCount">Total amount of vertices the mesh has</param>
        public static void OptimizeCache(uint[] Indices, int VertexCount)
        {
            MeshOptimizerNative.OptimizeVertexCache(Indices, Indices, (UIntPtr)Indices.Length, (UIntPtr)VertexCount);
        }

        /// <summary>
        /// Optimizes the mesh to reduce overdraw. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
        /// </summary>
        /// <param name="Indices">The indices of the mesh</param>
        /// <param name="Vertices">The vertices of the mesh</param>
        /// <param name="Stride">Space (in bytes) between each vertex</param>
        /// <param name="Threshold">The optimization threshold</param>
        /// <typeparam name="T"></typeparam>
        public static void OptimizeOverdraw<T>(uint[] Indices, T[] Vertices, uint Stride, float Threshold)
        {
            var pointer = Pointer.Create(Vertices);
            MeshOptimizerNative.OptimizeOverdraw(Indices, Indices, (UIntPtr)Indices.Length, pointer.Address, (UIntPtr)Vertices.Length, (UIntPtr)Stride, Threshold);
            pointer.Free();
        }

        /// <summary>
        /// Optimizes vertex fetching. See (https://github.com/zeux/meshoptimizer#vertex-cache-optimization)
        /// </summary>
        /// <param name="Indices">The indices of the mesh</param>
        /// <param name="Vertices">The vertices of the mesh</param>
        /// <param name="VertexSize">The size of T in bytes</param>
        /// <typeparam name="T">The vertex type</typeparam>
        public static void OptimizeVertexFetch<T>(uint[] Indices, T[] Vertices, uint VertexSize)
        {
            var pointer = Pointer.Create(Vertices);
            MeshOptimizerNative.OptimizeVertexFetch(pointer.Address, Indices, (UIntPtr)Indices.Length, pointer.Address, (UIntPtr)Vertices.Length, (UIntPtr)VertexSize);
            pointer.Free();
        }
    }
}
