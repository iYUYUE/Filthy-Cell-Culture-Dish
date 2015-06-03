#if !IgnoreRectLib
//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using AssemblyCSharp;
namespace Gamelogic.Grids
{
	public partial struct RectPoint
	{
		#region Constants
		/**
			The zero point (0, 0).
		*/
		public static readonly RectPoint Zero = new RectPoint(0, 0);
		#endregion

		#region Fields
		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;
		#endregion

		#region Properties
		/** 
			The x-coordinate of this point.
		*/
		public int X
		{
			get
			{
				return x;
			}
		}

		/**
			The y-coordinate of this point.
		*/
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/**	
			A Uniform point's base point is simply the point itself.
			Makes it easier to implement generic algorithms.
			
			@since 1.1
		*/
		public RectPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction
		/**
			Constructs a new RectPoint with the given coordinates.
		*/
		public RectPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}
	
		/**
			Constructs a new RectPoint with the same coordinates as the given VectorPoint.
		*/
		private RectPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
		}
		#endregion

		#region Distance
		/**
			The lattice distance from this point to the other.
		*/
		public int DistanceFrom(RectPoint other)
		{
			return Subtract(other).Magnitude();
		}
		#endregion

		#region Equality
		public bool Equals(RectPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(RectPoint))
			{
				return false;
			}

			var point = (RectPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic
		/**
			This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
			`p1.DistanceFrom(p2)`.
		*/
		public RectPoint Translate(RectPoint translation)
		{
			return new RectPoint(x + translation.X, y + translation.Y);
		}

		public RectPoint Negate()
		{
			return new RectPoint(-x, -y);
		}

		public RectPoint ScaleDown(int r)
		{
			return new RectPoint(Mathi.Div(x, r), Mathi.Div(y, r));
		}

		public RectPoint ScaleUp(int r)
		{
			return new RectPoint(x * r, y * r);
		}

		/**
			Subtracts the other point from this point, and returns the result.
		*/
		public RectPoint Subtract(RectPoint other)
		{
			return new RectPoint(x - other.X, y - other.Y);
		}

		public RectPoint MoveBy(RectPoint translation)
		{
			return Translate(translation);
		}

		public RectPoint MoveBackBy(RectPoint translation)
		{
			return Translate(translation.Negate());
		}

		/**
			@version1_7
		*/
		public int Dot(RectPoint other)
		{
			return x * other.X + y * other.Y;
		}

		/**
			@version1_7
		*/
		public int PerpDot(RectPoint other)
		{
			return x * other.Y - y * other.x;
		}

		/**
			@version1_10
		*/
		public RectPoint Perp()
		{
			return new RectPoint(-y, x);
		}

		/**
			Gives a new point that represents the 
			reminder when the first point is divided
			by the second point	component-wise. The
			division is integer division.

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public RectPoint Mod(RectPoint otherPoint)
		{
			var x = Mathi.Mod(X, otherPoint.X);
			var y = Mathi.Mod(Y, otherPoint.Y);

			return new RectPoint(x, y);
		}

		/**
			Gives a new point that represents the 
			first point divided by the second point
			component-wise. The division is integer
			division.

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public RectPoint Div(RectPoint otherPoint)
		{
			var x = Mathi.Div(X, otherPoint.X);
			var y = Mathi.Div(Y, otherPoint.Y);

			return new RectPoint(x, y);
		}

		/**
			Gives a new point that represents the 
			first point multiplied by the second point
			component-wise. 

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public RectPoint Mul(RectPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new RectPoint(x, y);
		}
		#endregion 

		#region Utility
		override public string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(RectPoint point1, RectPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(RectPoint point1, RectPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static RectPoint operator +(RectPoint point)
		{
			return point;
		}

		public static RectPoint operator -(RectPoint point)
		{
			return point.Negate();
		}

		public static RectPoint operator +(RectPoint point1, RectPoint point2)
		{
			return point1.Translate(point2);
		}

		public static RectPoint operator -(RectPoint point1, RectPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static RectPoint operator *(RectPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static RectPoint operator /(RectPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static RectPoint operator *(RectPoint point1, RectPoint point2)
		{
			return point1.Mul(point2);
		}

		public static RectPoint operator /(RectPoint point1, RectPoint point2)
		{
			return point1.Div(point2);
		}

		public static RectPoint operator %(RectPoint point1, RectPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings
		/**
			Gives a coloring of the grid such that 
			if a point p has color k, then all points
			p + m[ux, 0] + n[vx, vy] have the same color
			for any integers a and b.

			More information anout grid colorings:
			http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/

			@since 1.7
		*/
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new RectPoint(ux, 0);
			var v = new RectPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathi.FloorToInt(a);
			int n = Mathi.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = Mathi.Mod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathi.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/**
			Gives a coloring of the grid such that 
			if a point p has color k, then all points
			p + m[ux, 0] + n[vx, vy] have the same color
			for any integers a and b.

			More information anout grid colorings:
			http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/

			@since 1.7
		*/
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathi.FloorToInt(a);
			int n = Mathi.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = Mathi.Mod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathi.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}
		#endregion
	}

	#region Wrappers
	/**
		Wraps points both horizontally and vertically.
		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class RectParallelogramWrapper : IPointWrapper<RectPoint>
	{
		readonly int width;
		readonly int height;

		public RectParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(Mathi.Mod(point.X, width), Mathi.Mod(point.Y, height));
		}
	}

	/**
		Wraps points horizontally.
		
		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class RectHorizontalWrapper : IPointWrapper<RectPoint>
	{
		readonly int width;

		public RectHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(Mathi.Mod(point.X, width), point.Y);
		}
	}

	/**
		Wraps points vertically.

		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class RectVerticalWrapper : IPointWrapper<RectPoint>
	{
		readonly int height;

		public RectVerticalWrapper(int height)
		{
			this.height = height;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(point.X, Mathi.Mod(point.Y, height));
		}
	}

	#endregion 
	public partial struct DiamondPoint
	{
		#region Constants
		/**
			The zero point (0, 0).
		*/
		public static readonly DiamondPoint Zero = new DiamondPoint(0, 0);
		#endregion

		#region Fields
		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;
		#endregion

		#region Properties
		/** 
			The x-coordinate of this point.
		*/
		public int X
		{
			get
			{
				return x;
			}
		}

		/**
			The y-coordinate of this point.
		*/
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/**	
			A Uniform point's base point is simply the point itself.
			Makes it easier to implement generic algorithms.
			
			@since 1.1
		*/
		public DiamondPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction
		/**
			Constructs a new DiamondPoint with the given coordinates.
		*/
		public DiamondPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}
	
		/**
			Constructs a new DiamondPoint with the same coordinates as the given VectorPoint.
		*/
		private DiamondPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
			cell = null;
		}
		#endregion

		#region Distance
		/**
			The lattice distance from this point to the other.
		*/
		public int DistanceFrom(DiamondPoint other)
		{
			return Subtract(other).Magnitude();
		}
		#endregion

		#region Equality
		public bool Equals(DiamondPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(DiamondPoint))
			{
				return false;
			}

			var point = (DiamondPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic
		/**
			This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
			`p1.DistanceFrom(p2)`.
		*/
		public DiamondPoint Translate(DiamondPoint translation)
		{
			return new DiamondPoint(x + translation.X, y + translation.Y);
		}

		public DiamondPoint Negate()
		{
			return new DiamondPoint(-x, -y);
		}

		public DiamondPoint ScaleDown(int r)
		{
			return new DiamondPoint(Mathi.Div(x, r), Mathi.Div(y, r));
		}

		public DiamondPoint ScaleUp(int r)
		{
			return new DiamondPoint(x * r, y * r);
		}

		/**
			Subtracts the other point from this point, and returns the result.
		*/
		public DiamondPoint Subtract(DiamondPoint other)
		{
			return new DiamondPoint(x - other.X, y - other.Y);
		}

		public DiamondPoint MoveBy(DiamondPoint translation)
		{
			return Translate(translation);
		}

		public DiamondPoint MoveBackBy(DiamondPoint translation)
		{
			return Translate(translation.Negate());
		}

		/**
			@version1_7
		*/
		public int Dot(DiamondPoint other)
		{
			return x * other.X + y * other.Y;
		}

		/**
			@version1_7
		*/
		public int PerpDot(DiamondPoint other)
		{
			return x * other.Y - y * other.x;
		}

		/**
			@version1_10
		*/
		public DiamondPoint Perp()
		{
			return new DiamondPoint(-y, x);
		}

		/**
			Gives a new point that represents the 
			reminder when the first point is divided
			by the second point	component-wise. The
			division is integer division.

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public DiamondPoint Mod(DiamondPoint otherPoint)
		{
			var x = Mathi.Mod(X, otherPoint.X);
			var y = Mathi.Mod(Y, otherPoint.Y);

			return new DiamondPoint(x, y);
		}

		/**
			Gives a new point that represents the 
			first point divided by the second point
			component-wise. The division is integer
			division.

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public DiamondPoint Div(DiamondPoint otherPoint)
		{
			var x = Mathi.Div(X, otherPoint.X);
			var y = Mathi.Div(Y, otherPoint.Y);

			return new DiamondPoint(x, y);
		}

		/**
			Gives a new point that represents the 
			first point multiplied by the second point
			component-wise. 

			@since 1.6 (Rect)
			@since 1.7 (other)
		*/
		public DiamondPoint Mul(DiamondPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new DiamondPoint(x, y);
		}
		#endregion 

		#region Utility
		override public string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(DiamondPoint point1, DiamondPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static DiamondPoint operator +(DiamondPoint point)
		{
			return point;
		}

		public static DiamondPoint operator -(DiamondPoint point)
		{
			return point.Negate();
		}

		public static DiamondPoint operator +(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Translate(point2);
		}

		public static DiamondPoint operator -(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static DiamondPoint operator *(DiamondPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static DiamondPoint operator /(DiamondPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static DiamondPoint operator *(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Mul(point2);
		}

		public static DiamondPoint operator /(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Div(point2);
		}

		public static DiamondPoint operator %(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings
		/**
			Gives a coloring of the grid such that 
			if a point p has color k, then all points
			p + m[ux, 0] + n[vx, vy] have the same color
			for any integers a and b.

			More information anout grid colorings:
			http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/

			@since 1.7
		*/
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new DiamondPoint(ux, 0);
			var v = new DiamondPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathi.FloorToInt(a);
			int n = Mathi.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = Mathi.Mod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathi.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/**
			Gives a coloring of the grid such that 
			if a point p has color k, then all points
			p + m[ux, 0] + n[vx, vy] have the same color
			for any integers a and b.

			More information anout grid colorings:
			http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/

			@since 1.7
		*/
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathi.FloorToInt(a);
			int n = Mathi.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = Mathi.Mod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathi.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}
		#endregion
	}

	#region Wrappers
	/**
		Wraps points both horizontally and vertically.
		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class DiamondParallelogramWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int width;
		readonly int height;

		public DiamondParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(Mathi.Mod(point.X, width), Mathi.Mod(point.Y, height));
		}
	}

	/**
		Wraps points horizontally.
		
		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class DiamondHorizontalWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int width;

		public DiamondHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(Mathi.Mod(point.X, width), point.Y);
		}
	}

	/**
		Wraps points vertically.

		@since 1.7
		@ingroup Scaffolding
	*/
	[Experimental]
	public class DiamondVerticalWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int height;

		public DiamondVerticalWrapper(int height)
		{
			this.height = height;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(point.X, Mathi.Mod(point.Y, height));
		}
	}

	#endregion 
}
#endif
