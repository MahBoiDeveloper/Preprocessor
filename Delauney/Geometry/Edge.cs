// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Edge.cs" company="">
//   
// </copyright>
// <summary>
//   Edge made from two point indexes
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DelauneyPaulBourke.Geometry
{
    using System;

    /// <summary>
    /// Edge made from two point indexes
    /// </summary>
    public class Edge : IEquatable<Edge>
    {
        /// <summary>
        /// Start of edge index
        /// </summary>
        public int p1;

        /// <summary>
        /// End of edge index
        /// </summary>
        public int p2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class. 
        /// Initializes a new edge instance
        /// </summary>
        /// <param name="point1">
        /// Start edge vertex index
        /// </param>
        /// <param name="point2">
        /// End edge vertex index
        /// </param>
        public Edge(int point1, int point2)
        {
            this.p1 = point1;
            this.p2 = point2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class. 
        /// Initializes a new edge instance with start/end indexes of '0'
        /// </summary>
        public Edge()
            : this(0, 0)
        {
        }

        #region IEquatable<Edge> Members

        /// <summary>
        /// Checks whether two edges are equal disregarding the direction of the edges
        /// </summary>
        /// <param name="other">
        /// </param>
        /// <returns>
        /// The equals.
        /// </returns>
        public bool Equals(Edge other)
        {
            return ((this.p1 == other.p2) && (this.p2 == other.p1)) || ((this.p1 == other.p1) && (this.p2 == other.p2));
        }

        #endregion
    }
}