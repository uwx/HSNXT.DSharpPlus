using System;

namespace HSNXT.Triangulator.Geometry
{
    /// <inheritdoc />
    /// <summary>
    /// Edge made from two point indexes
    /// </summary>
    public class Edge : IEquatable<Edge>
    {
        /// <summary>
        /// Start of edge index
        /// </summary>
        public int P1;

        /// <summary>
        /// End of edge index
        /// </summary>
        public int P2;

        /// <summary>
        /// Initializes a new edge instance
        /// </summary>
        /// <param name="point1">Start edge vertex index</param>
        /// <param name="point2">End edge vertex index</param>
        public Edge(int point1, int point2)
        {
            P1 = point1;
            P2 = point2;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new edge instance with start/end indexes of '0'
        /// </summary>
        public Edge()
            : this(0, 0)
        {
        }

        #region IEquatable<dEdge> Members

        /// <inheritdoc />
        /// <summary>
        /// Checks whether two edges are equal disregarding the direction of the edges
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Edge other)
        {
            return other != null && (
                       this.P1 == other.P2 && this.P2 == other.P1 ||
                       this.P1 == other.P1 && this.P2 == other.P2);
        }

        #endregion
    }
}