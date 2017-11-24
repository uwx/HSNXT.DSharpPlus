namespace HSNXT.Triangulator.Geometry
{
    /// <inheritdoc />
    /// <summary>
    /// A point with an attribute value of type 'T'
    /// </summary>
    public class Point<T> : Point
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the point
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        /// <param name="attribute">Attribute</param>
        public Point(double x, double y, T attribute)
            : base(x, y)
        {
            Attribute = attribute;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the point and sets the attribute to its default value
        /// </summary>
        /// <param name="x">X component</param>
        /// <param name="y">Y component</param>
        public Point(double x, double y)
            : this(x, y, default)
        {
        }

        /// <summary>
        /// Gets or sets the attribute component of the point
        /// </summary>
        public T Attribute { get; set; }
    }
}