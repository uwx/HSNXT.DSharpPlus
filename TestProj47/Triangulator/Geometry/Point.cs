using System.Diagnostics.CodeAnalysis;

namespace HSNXT.Triangulator.Geometry
{
    /// <summary>
    /// 2D Point with double precision
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Initializes a new instance of a point
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the X component of the point
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y component of the point
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Makes a planar checks for if the points is spatially equal to another point.
        /// </summary>
        /// <param name="other">Point to check against</param>
        /// <returns>True if X and Y values are the same</returns>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool Equals2D(Point other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}