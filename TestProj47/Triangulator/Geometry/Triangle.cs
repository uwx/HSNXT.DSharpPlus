namespace HSNXT.Triangulator.Geometry
{
    /// <summary>
    /// Triangle made from three point indexes
    /// </summary>
    public struct Triangle
    {
        /// <summary>
        /// First vertex index in triangle
        /// </summary>
        public int P1;

        /// <summary>
        /// Second vertex index in triangle
        /// </summary>
        public int P2;

        /// <summary>
        /// Third vertex index in triangle
        /// </summary>
        public int P3;

        /// <summary>
        /// Initializes a new instance of a triangle
        /// </summary>
        /// <param name="point1">Vertex 1</param>
        /// <param name="point2">Vertex 2</param>
        /// <param name="point3">Vertex 3</param>
        public Triangle(int point1, int point2, int point3)
        {
            P1 = point1;
            P2 = point2;
            P3 = point3;
        }
    }
}