// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Point.cs" company="">
//   
// </copyright>
// <summary>
//   2D Point with double precision
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DelauneyPaulBourke.Geometry
{
    /// <summary>
    /// 2D Point with double precision
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X component of point
        /// </summary>
        protected double _X;

        /// <summary>
        /// Y component of point
        /// </summary>
        protected double _Y;

        /// <summary>
        /// Point number
        /// </summary>
        protected int number;

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class. 
        /// Initializes a new instance of a point
        /// </summary>
        /// <param name="x">
        /// </param>
        /// <param name="y">
        /// </param>
        /// <param name="n">
        /// The n.
        /// </param>
        public Point(double x, double y, int n)
        {
            this._X = x;
            this._Y = y;
            this.number = n;
        }

        /// <summary>
        /// Gets or sets the X component of the point
        /// </summary>
        public double X
        {
            get
            {
                return this._X;
            }

            set
            {
                this._X = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y component of the point
        /// </summary>
        public double Y
        {
            get
            {
                return this._Y;
            }

            set
            {
                this._Y = value;
            }
        }

        /// <summary>
        /// Gets or sets Number.
        /// </summary>
        public int Number
        {
            get
            {
                return this.number;
            }

            set
            {
                this.number = value;
            }
        }

        /// <summary>
        /// Makes a planar checks for if the points is spatially equal to another point.
        /// </summary>
        /// <param name="other">
        /// Point to check against
        /// </param>
        /// <returns>
        /// True if X and Y values are the same
        /// </returns>
        public bool Equals2D(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
    }
}