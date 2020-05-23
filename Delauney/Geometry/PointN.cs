// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointN.cs" company="">
//   
// </copyright>
// <summary>
//   A point with an attribute value of type 'T'
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DelauneyPaulBourke.Geometry
{
    /// <summary>
    /// A point with an attribute value of type 'T'
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Point<T> : Point
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point{T}"/> class. 
        /// Initializes a new instance of the point
        /// </summary>
        /// <param name="x">
        /// X component
        /// </param>
        /// <param name="y">
        /// Y component
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <param name="attribute">
        /// Attribute
        /// </param>
        public Point(double x, double y, int n, T attribute)
            : base(x, y, n)
        {
            this.Attribute = attribute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point{T}"/> class. 
        /// Initializes a new instance of the point and sets the attribute to its default value
        /// </summary>
        /// <param name="x">
        /// X component
        /// </param>
        /// <param name="y">
        /// Y component
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        public Point(double x, double y, int n)
            : this(x, y, n, default(T))
        {
        }

        /// <summary>
        /// Gets or sets the attribute component of the point
        /// </summary>
        public T Attribute { get; set; }
    }
}