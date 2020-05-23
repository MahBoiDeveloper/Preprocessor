// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Triangle.cs" company="">
//   
// </copyright>
// <summary>
//   Triangle made from three point indexes
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DelauneyPaulBourke.Geometry
{
    /// <summary>
    /// Triangle made from three point indexes
    /// </summary>
    public struct Triangle
    {
        /// <summary>
        /// First vertex index in triangle
        /// </summary>
        public int p1;

        /// <summary>
        /// Second vertex index in triangle
        /// </summary>
        public int p2;

        /// <summary>
        /// Third vertex index in triangle
        /// </summary>
        public int p3;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> struct. 
        /// Initializes a new instance of a triangle
        /// </summary>
        /// <param name="point1">
        /// Vertex 1
        /// </param>
        /// <param name="point2">
        /// Vertex 2
        /// </param>
        /// <param name="point3">
        /// Vertex 3
        /// </param>
        public Triangle(int point1, int point2, int point3)
        {
            this.p1 = point1;
            this.p2 = point2;
            this.p3 = point3;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The equals.
        /// </returns>
        public override bool Equals(object obj)
        {
            var another = (Triangle)obj;
            if (((another.p1 == this.p1) || (another.p1 == this.p2) || (another.p1 == this.p3)) &&
                ((another.p2 == this.p1) || (another.p2 == this.p2) || (another.p2 == this.p3)) &&
                ((another.p3 == this.p1) || (another.p3 == this.p2) || (another.p3 == this.p3)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}