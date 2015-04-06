// The documentation for this class (on <summary> tags) was extracted from:
// https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D

using Bridge;

namespace Bridge.Html5
{
    /// <summary>
    /// The CanvasRenderingContext2D interface provides the 2D rendering context for the drawing surface of a
    /// <canvas> element.
    /// To get an object of this interface, call getContext() on a <canvas>, supplying
    /// "CanvasContext2DType.CanvasRenderingContext2D" as the argument.
    /// </summary>
    public class CanvasRenderingContext2D
    {
        /// <summary>
        /// There are three methods that immediately draw rectangles to the bitmap.
        /// </summary>
        #region Drawing Rectangles

        // TODO: Review summary to include parameters' info. From this point down to createPattern (~390)
        /// <summary>
        /// Sets all pixels in the rectangle defined by starting point (x, y) and size (width, height) to transparent black, erasing any previously drawn content.
        /// </summary>
        public virtual void ClearRect(uint x, uint y, uint width, uint height)
        {
            return;
        }

        /// <summary>
        /// Sets all pixels in the rectangle defined by starting point (x, y) and size (width, height) to transparent black, erasing any previously drawn content.
        /// </summary>
        public virtual void ClearRect(int x, int y, int width, int height)
        {
            return;
        }

        /// <summary>
        /// Draws a filled rectangle at (x, y) position whose size is determined by width and height.
        /// </summary>
        public virtual void FillRect(uint x, uint y, uint width, uint height)
        {
            return;
        }

        /// <summary>
        /// Draws a filled rectangle at (x, y) position whose size is determined by width and height.
        /// </summary>
        public virtual void FillRect(int x, int y, int width, int height)
        {
            return;
        }

        /// <summary>
        /// Paints a rectangle which has a starting point at (x, y) and has a w width and an h height onto the canvas, using the current stroke style.
        /// </summary>
        public virtual void StrokeRect(uint x, uint y, uint width, uint height)
        {
            return;
        }

        /// <summary>
        /// Paints a rectangle which has a starting point at (x, y) and has a w width and an h height onto the canvas, using the current stroke style.
        /// </summary>
        public virtual void StrokeRect(int x, int y, int width, int height)
        {
            return;
        }

        #endregion

        /// <summary>
        /// The following methods are provided for drawing text. See also the TextMetrics object for text properties.
        /// </summary>
        #region Drawing Text

        /// <summary>
        /// Draws (fills) a given text at the given (x,y) position.
        /// </summary>
        public virtual void FillText(string text, uint x, uint y, uint? maxWidth = null)
        {
            return;
        }

        /// <summary>
        /// Draws (fills) a given text at the given (x,y) position.
        /// </summary>
        public virtual void FillText(string text, int x, int y, int? maxWidth = null)
        {
            return;
        }

        /// <summary>
        /// Draws (strokes) a given text at the given (x, y) position.
        /// </summary>
        public virtual void StrokeText(string text, uint x, uint y, uint? maxWidth = null)
        {
            return;
        }

        /// <summary>
        /// Draws (strokes) a given text at the given (x, y) position.
        /// </summary>
        public virtual void StrokeText(string text, int x, int y, int? maxWidth = null)
        {
            return;
        }

        /// <summary>
        /// Returns a TextMetrics object.
        /// </summary>
        public virtual TextMetrics MeasureText(string text)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// The following methods and properties control how lines are drawn.
        /// </summary>
        #region Line Styles

        /// <summary>
        /// Width of lines. Default 1.0
        /// </summary>
        public double LineWidth;

        /// <summary>
        /// Type of endings on the end of lines. Possible values: butt (default), round, square.
        /// </summary>
        public CanvasTypes.CanvasLineCapType LineCap;

        /// <summary>
        /// Defines the type of corners where two lines meet. Possible values: round, bevel, miter (default).
        /// </summary>
        public CanvasTypes.CanvasLineJoinType LineJoin;

        /// <summary>
        /// Miter limit ratio. Default 10.
        /// </summary>
        public double MiterLimit;

        /// <summary>
        /// Returns the current line dash pattern array containing an even number of non-negative numbers.
        /// If the number, when setting the elements, was odd, the elements of the array get copied and
        /// concatenated. For example, setting the line dash to [5, 15, 25] will result in getting back
        /// [5, 15, 25, 5, 15, 25].
        /// </summary>
        public virtual double[] GetLineDash()
        {
            return null;
        }

        /// <summary>
        /// Sets the current line dash pattern. If the number of elements in the array is odd, the elements
        /// of the array get copied and concatenated. For example, [5, 15, 25] will become
        /// [5, 15, 25, 5, 15, 25].
        /// </summary>
        public virtual void SetLineDash(double[] segments)
        {
            return;
        }

        /// <summary>
        /// Sets the current line dash pattern. If the number of elements in the array is odd, the elements
        /// of the array get copied and concatenated. For example, [5, 15, 25] will become
        /// [5, 15, 25, 5, 15, 25].
        /// </summary>
        public virtual void SetLineDash(uint[] segments)
        {
            return;
        }

        /// <summary>
        /// Sets the current line dash pattern. If the number of elements in the array is odd, the elements
        /// of the array get copied and concatenated. For example, [5, 15, 25] will become
        /// [5, 15, 25, 5, 15, 25].
        /// </summary>
        public virtual void SetLineDash(int[] segments)
        {
            return;
        }

        /// <summary>
        /// Sets the current line dash pattern. If the number of elements in the array is odd, the elements
        /// of the array get copied and concatenated. For example, [5, 15, 25] will become
        /// [5, 15, 25, 5, 15, 25].
        /// Format is: "[ 4.5, 2, 3.4, 8 ]"
        /// </summary>
        /// <remarks>Use this overload ONLY when you know what you're doing!</remarks>
        public virtual void SetLineDash(string segments)
        {
            return;
        }

        /// <summary>
        /// Specifies where to start a dash array on a line.
        /// </summary>
        public float LineDashOffset;

        #endregion

        /// <summary>
        /// The following properties control how text is laid out.
        /// </summary>
        #region Text Styles

        /// <summary>
        /// Font setting. Default value 10px sans-serif.
        /// </summary>
        public string Font;

        /// <summary>
        /// Text alignment setting. Possible values: start (default), end, left, right or center.
        /// </summary>
        public CanvasTypes.CanvasTextAlign TextAlign;

        /// <summary>
        /// Baseline alignment setting. Possible values: top, hanging, middle, alphabetic (default), ideographic, bottom.
        /// </summary>
        public CanvasTypes.CanvasTextBaselineAlign TextBaseline;

        /// <summary>
        /// Directionality. Possible values: ltr, rtl, inherit (default).
        /// </summary>
        public CanvasTypes.CanvasTextDirection Direction;

        #endregion

        /// <summary>
        /// Fill styling is used for colors and styles inside shapes and stroke styling is used for the lines around shapes.
        /// </summary>
        #region Fill and Stroke Styles

        /// <summary>
        /// Color or style to use inside shapes. Default #000 (black).
        /// </summary>
        public string FillStyle;

        /// <summary>
        /// Color or style to use for the lines around shapes. Default #000 (black).
        /// </summary>
        public string StrokeStyle;

        #endregion

        #region Gradients and Patterns
        /// <summary>
        /// Creates a linear gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateLinearGradient(uint x0, uint y0, uint x1, uint y1)
        {
            return null;
        }

        /// <summary>
        /// Creates a linear gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateLinearGradient(int x0, int y0, int x1, int y1)
        {
            return null;
        }

        /// <summary>
        /// Creates a linear gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateLinearGradient(double x0, double y0, double x1, double y1)
        {
            return null;
        }

        /// <summary>
        /// Creates a radial gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateRadialGradient(uint x0, uint y0, uint r0, uint x1, uint y1, uint r1)
        {
            return null;
        }

        /// <summary>
        /// Creates a radial gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateRadialGradient(int x0, int y0, int r0, int x1, int y1, int r1)
        {
            return null;
        }

        /// <summary>
        /// Creates a radial gradient along the line given by the coordinates represented by the parameters.
        /// </summary>
        public virtual CanvasGradient CreateRadialGradient(double x0, double y0, double r0, double x1, double y1, double r1)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        public virtual CanvasPattern CreatePattern(ImageElement image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        public virtual CanvasPattern CreatePattern(VideoElement image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        public virtual CanvasPattern CreatePattern(CanvasElement image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        public virtual CanvasPattern CreatePattern(CanvasRenderingContext2D image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        /*
         * TODO: ImageData interface not implemented
        public virtual CanvasPattern CreatePattern(ImageData image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }
        */

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        public virtual CanvasPattern CreatePattern(Blob image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        /// <summary>
        /// Creates a pattern using the specified image (a CanvasImageSource). It repeats the source in the
        /// directions specified by the repetition argument. This method returns a CanvasPattern.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="repetition"></param>
        /// <returns></returns>
        /// <remarks>
        /// At the time of implementation, ImageBitmap had no documentation and Bridge.NET did not have
        /// it defined inside.
        /// </remarks>
        public virtual CanvasPattern CreatePattern(string image, CanvasTypes.CanvasRepetitionTypes repetition)
        {
            return null;
        }

        #endregion

        #region Shadows

        /// <summary>
        /// Specifies the blurring effect. Default 0
        /// </summary>
        public float ShadowBlur;

        /// <summary>
        /// Color of the shadow. Default fully-transparent black.
        /// </summary>
        public string ShadowColor;

        /// <summary>
        /// Horizontal distance the shadow will be offset. Default 0.
        /// </summary>
        public float ShadowOffsetX;

        /// <summary>
        /// Vertical distance the shadow will be offset. Default 0.
        /// </summary>
        public float ShadowOffsetY;

        #endregion

        /// <summary>
        /// The following methods can be used to manipulate paths of objects.
        /// </summary>
        #region Paths

        /// <summary>
        /// Starts a new path by emptying the list of sub-paths. Call this method when you want to create a new path.
        /// </summary>
        public virtual void BeginPath()
        {
            return;
        }

        /// <summary>
        /// Causes the point of the pen to move back to the start of the current sub-path. It tries to draw a straight line from the current point to the start. If the shape has already been closed or has only one point, this function does nothing.
        /// </summary>
        public virtual void ClosePath()
        {
            return;
        }

        /// <summary>
        /// Moves the starting point of a new sub-path to the (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x axis of the point</param>
        /// <param name="y">The y axis of the point</param>
        public virtual void MoveTo(uint x, uint y)
        {
            return;
        }

        /// <summary>
        /// Moves the starting point of a new sub-path to the (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x axis of the point</param>
        /// <param name="y">The y axis of the point</param>
        public virtual void MoveTo(int x, int y)
        {
            return;
        }

        /// <summary>
        /// Moves the starting point of a new sub-path to the (x, y) coordinates.
        /// </summary>
        /// <param name="x">The x axis of the point</param>
        /// <param name="y">The y axis of the point</param>
        public virtual void MoveTo(double x, double y)
        {
            return;
        }

        /// <summary>
        /// Connects the last point in the subpath to the x, y coordinates with a straight line.
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the end of the line.</param>
        /// <param name="y">The y axis of the coordinate for the end of the line.</param>
        public virtual void LineTo(uint x, uint y)
        {
            return;
        }

        /// <summary>
        /// Connects the last point in the subpath to the x, y coordinates with a straight line.
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the end of the line.</param>
        /// <param name="y">The y axis of the coordinate for the end of the line.</param>
        public virtual void LineTo(int x, int y)
        {
            return;
        }

        /// <summary>
        /// Connects the last point in the subpath to the x, y coordinates with a straight line.
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the end of the line.</param>
        /// <param name="y">The y axis of the coordinate for the end of the line.</param>
        public virtual void LineTo(double x, double y)
        {
            return;
        }

        /// <summary>
        /// Adds a cubic Bézier curve to the path. It requires three points. The first two points are control points and the third one is the end point. The starting point is the last point in the current path, which can be changed using moveTo() before creating the Bézier curve.
        /// </summary>
        /// <param name="cp1x">The x axis of the coordinate for the first control point.</param>
        /// <param name="cp1y">The y axis of the coordinate for first control point.</param>
        /// <param name="cp2x">The x axis of the coordinate for the second control point.</param>
        /// <param name="cp2y">The y axis of the coordinate for the second control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void BezierCurveTo(uint cp1x, uint cp1y, uint cp2x, uint cp2y, uint x, uint y)
        {
            return;
        }

        /// <summary>
        /// Adds a cubic Bézier curve to the path. It requires three points. The first two points are control points and the third one is the end point. The starting point is the last point in the current path, which can be changed using moveTo() before creating the Bézier curve.
        /// </summary>
        /// <param name="cp1x">The x axis of the coordinate for the first control point.</param>
        /// <param name="cp1y">The y axis of the coordinate for first control point.</param>
        /// <param name="cp2x">The x axis of the coordinate for the second control point.</param>
        /// <param name="cp2y">The y axis of the coordinate for the second control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void BezierCurveTo(int cp1x, int cp1y, int cp2x, int cp2y, int x, int y)
        {
            return;
        }

        /// <summary>
        /// Adds a cubic Bézier curve to the path. It requires three points. The first two points are control points and the third one is the end point. The starting point is the last point in the current path, which can be changed using moveTo() before creating the Bézier curve.
        /// </summary>
        /// <param name="cp1x">The x axis of the coordinate for the first control point.</param>
        /// <param name="cp1y">The y axis of the coordinate for first control point.</param>
        /// <param name="cp2x">The x axis of the coordinate for the second control point.</param>
        /// <param name="cp2y">The y axis of the coordinate for the second control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void BezierCurveTo(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y)
        {
            return;
        }

        /// <summary>
        /// Adds a quadratic Bézier curve to the current path.
        /// </summary>
        /// <param name="cpx">The x axis of the coordinate for the control point.</param>
        /// <param name="cpy">The y axis of the coordinate for the control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void QuadraticCurveTo(uint cpx, uint cpy, uint x, uint y)
        {
            return;
        }

        /// <summary>
        /// Adds a quadratic Bézier curve to the current path.
        /// </summary>
        /// <param name="cpx">The x axis of the coordinate for the control point.</param>
        /// <param name="cpy">The y axis of the coordinate for the control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void QuadraticCurveTo(int cpx, int cpy, int x, int y)
        {
            return;
        }

        /// <summary>
        /// Adds a quadratic Bézier curve to the current path.
        /// </summary>
        /// <param name="cpx">The x axis of the coordinate for the control point.</param>
        /// <param name="cpy">The y axis of the coordinate for the control point.</param>
        /// <param name="x">The x axis of the coordinate for the end point.</param>
        /// <param name="y">The y axis of the coordinate for the end point.</param>
        public virtual void QuadraticCurveTo(double cpx, double cpy, double x, double y)
        {
            return;
        }
        
        /// <summary>
        /// Adds an arc to the path which is centered at (x, y) position with radius r starting at
        /// startAngle and ending at endAngle going in the given direction by anticlockwise
        /// (defaulting to clockwise).
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the arc's center.</param>
        /// <param name="y">The y axis of the coordinate for the arc's center.</param>
        /// <param name="radius">The arc's radius.</param>
        /// <param name="startAngle">
        /// The starting point, measured from the x axis, from which it will be drawn, expressed in radians.
        /// </param>
        /// <param name="endAngle">The end arc's angle to which it will be drawn, expressed in radians.</param>
        /// <param name="anticlockwise">
        /// An optional Boolean which, if true, draws the arc anticlockwise (counter-clockwise), otherwise
        /// in a clockwise direction.
        /// </param>
        public virtual void Arc(uint x, uint y, uint radius, double startAngle, double endAngle, bool anticlockwise = false)
        {
            return;
        }

        /// <summary>
        /// Adds an arc to the path which is centered at (x, y) position with radius r starting at
        /// startAngle and ending at endAngle going in the given direction by anticlockwise
        /// (defaulting to clockwise).
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the arc's center.</param>
        /// <param name="y">The y axis of the coordinate for the arc's center.</param>
        /// <param name="radius">The arc's radius.</param>
        /// <param name="startAngle">
        /// The starting point, measured from the x axis, from which it will be drawn, expressed in radians.
        /// </param>
        /// <param name="endAngle">The end arc's angle to which it will be drawn, expressed in radians.</param>
        /// <param name="anticlockwise">
        /// An optional Boolean which, if true, draws the arc anticlockwise (counter-clockwise), otherwise
        /// in a clockwise direction.
        /// </param>
        public virtual void Arc(int x, int y, int radius, double startAngle, double endAngle, bool anticlockwise = false)
        {
            return;
        }

        /// <summary>
        /// Adds an arc to the path which is centered at (x, y) position with radius r starting at
        /// startAngle and ending at endAngle going in the given direction by anticlockwise
        /// (defaulting to clockwise).
        /// </summary>
        /// <param name="x">The x axis of the coordinate for the arc's center.</param>
        /// <param name="y">The y axis of the coordinate for the arc's center.</param>
        /// <param name="radius">The arc's radius.</param>
        /// <param name="startAngle">
        /// The starting point, measured from the x axis, from which it will be drawn, expressed in radians.
        /// </param>
        /// <param name="endAngle">The end arc's angle to which it will be drawn, expressed in radians.</param>
        /// <param name="anticlockwise">
        /// An optional Boolean which, if true, draws the arc anticlockwise (counter-clockwise), otherwise
        /// in a clockwise direction.
        /// </param>
        public virtual void Arc(double x, double y, double radius, double startAngle, double endAngle, bool anticlockwise = false)
        {
            return;
        }

        /// <summary>
        /// Adds an arc to the path with the given control points and radius, connected to the previous point by a straight line.
        /// </summary>
        /// <param name="x1">The x axis of the coordinate for the first control point.</param>
        /// <param name="y1">The y axis of the coordinate for the first control point.</param>
        /// <param name="x2">The x axis of the coordinate for the second control point.</param>
        /// <param name="y2">The y axis of the coordinate for the second control point.</param>
        /// <param name="radius">The arc's radius.</param>
        public virtual void ArcTo(uint x1, uint y1, uint x2, uint y2, double radius)
        {
            return;
        }

        /// <summary>
        /// Adds an arc to the path with the given control points and radius, connected to the previous point by a straight line.
        /// </summary>
        /// <param name="x1">The x axis of the coordinate for the first control point.</param>
        /// <param name="y1">The y axis of the coordinate for the first control point.</param>
        /// <param name="x2">The x axis of the coordinate for the second control point.</param>
        /// <param name="y2">The y axis of the coordinate for the second control point.</param>
        /// <param name="radius">The arc's radius.</param>
        public virtual void ArcTo(int x1, int y1, int x2, int y2, double radius)
        {
            return;
        }

        /// <summary>
        /// Adds an arc to the path with the given control points and radius, connected to the previous point by a straight line.
        /// </summary>
        /// <param name="x1">The x axis of the coordinate for the first control point.</param>
        /// <param name="y1">The y axis of the coordinate for the first control point.</param>
        /// <param name="x2">The x axis of the coordinate for the second control point.</param>
        /// <param name="y2">The y axis of the coordinate for the second control point.</param>
        /// <param name="radius">The arc's radius.</param>
        public virtual void ArcTo(double x1, double y1, double x2, double y2, double radius)
        {
            return;
        }

        // TODO: Continue updating from this point
        /// <summary>
        /// Adds an ellipse to the path which is centered at (x, y) position with the radii radiusX and radiusY starting at startAngle and ending at endAngle going in the given direction by anticlockwise (defaulting to clockwise).
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public virtual void Ellipse()
        {
            return;
        }

        /// <summary>
        /// Creates a path for a rectangle at position (x, y) with a size that is determined by width and height.
        /// </summary>
        public virtual void Rect()
        {
            return;
        }

        #endregion

        #region Drawing Paths

        /// <summary>
        /// Fills the subpaths with the current fill style.
        /// </summary>
        public virtual void Fill()
        {
            return;
        }

        /// <summary>
        /// Strokes the subpaths with the current stroke style.
        /// </summary>
        public virtual void Stroke()
        {
            return;
        }

        /// <summary>
        /// If a given element is focused, this method draws a focus ring around the current path.
        /// </summary>
        public virtual void DrawFocusIfNeeded()
        {
            return;
        }

        /// <summary>
        /// Scrolls the current path or a given path into the view.
        /// </summary>
        public virtual void ScrollpathIntoView()
        {
            return;
        }

        /// <summary>
        /// Creates a clipping path from the current sub-paths. Everything drawn after clip() is called appears inside the clipping path only. For an example, see Clipping paths in the Canvas tutorial.
        /// </summary>
        public virtual void Clip()
        {
            return;
        }

        /// <summary>
        /// Reports whether or not the specified point is contained in the current path.
        /// </summary>
        public virtual bool IsPointInPath()
        {
            return false;
        }

        /// <summary>
        /// Reports whether or not the specified point is inside the area contained by the stroking of a path.
        /// </summary>
        public virtual bool IsPointInStroke()
        {
            return false;
        }

        #endregion

        /// <summary>
        /// Objects in the CanvasRenderingContext2D rendering context have a current transformation matrix and methods to manipulate it. The transformation matrix is applied when creating the current default path, painting text, shapes and Path2D objects. The methods listed below remain for historical and compatibility reasons as SVGMatrix objects are used in most parts of the API nowadays and will be used in the future instead.
        /// </summary>
        #region Transformations

        /// <summary>
        /// Current transformation matrix (SVGMatrix object).
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        // TODO: define the SVGMatrix class based on https://developer.mozilla.org/en-US/docs/Web/API/SVGMatrix
        public string CurrentTransform; // TODO: string here should be SVGMatrix

        /// <summary>
        /// Adds a rotation to the transformation matrix. The angle argument represents a clockwise rotation angle and is expressed in radians.
        /// </summary>
        public virtual void Rotate()
        {
            return;
        }

        /// <summary>
        /// Adds a scaling transformation to the canvas units by x horizontally and by y vertically.
        /// </summary>
        public virtual void Scale()
        {
            return;
        }

        /// <summary>
        /// Adds a translation transformation by moving the canvas and its origin x horizontally and y vertically on the grid.
        /// </summary>
        public virtual void Translate(uint x, uint y)
        {
            return;
        }

        /// <summary>
        /// Adds a translation transformation by moving the canvas and its origin x horizontally and y vertically on the grid.
        /// </summary>
        public virtual void Translate(double x, double y)
        {
            return;
        }

        /// <summary>
        /// Multiplies the current transformation matrix with the matrix described by its arguments.
        /// </summary>
        public virtual void Transform()
        {
            return;
        }

        /// <summary>
        /// Resets the current transform to the identity matrix, and then invokes the transform() method with the same arguments.
        /// </summary>
        public virtual void SetTransfrom()
        {
            return;
        }

        /// <summary>
        /// Resets the current transform by the identity matrix.
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public virtual void ResetTransform()
        {
            return;
        }

        #endregion

        #region Composing

        /// <summary>
        /// Alpha value that is applied to shapes and images before they are composited onto the canvas. Default 1.0 (opaque).
        /// </summary>
        public float GlobalAlpha;

        /// <summary>
        /// With globalAlpha applied this sets how shapes and images are drawn onto the existing bitmap.
        /// </summary>
        public CanvasTypes.CanvasCompositeOperationType GlobalCompositeOperation;

        #endregion

        #region Drawing Images

        /// <summary>
        /// Draws the specified image. This method is available in multiple formats, providing a great deal of flexibility in its use.
        /// </summary>
        public virtual void DrawImage()
        {
            return;
        }

        #endregion

        /// <summary>
        /// See also the ImageData object.
        /// </summary>
        #region Pixel Manipulation

        /// <summary>
        /// Creates a new, blank ImageData object with the specified dimensions. All of the pixels in the new object are transparent black.
        /// </summary>
        public virtual void CreateImageData()
        {
            return;
        }

        /// <summary>
        /// Returns an ImageData object representing the underlying pixel data for the area of the canvas denoted by the rectangle which starts at (sx, sy) and has an sw width and sh height.
        /// </summary>
        public virtual void GetImageData()
        {
            return;
        }

        /// <summary>
        /// Paints data from the given ImageData object onto the bitmap. If a dirty rectangle is provided, only the pixels from that rectangle are painted.
        /// </summary>
        public virtual void PutImageData()
        {
            return;
        }

        #endregion

        #region Image Smoothing

        /// <summary>
        /// Image smoothing mode; if disabled, images will not be smoothed if scaled.
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public bool ImageSmoothingEnabled;

        #endregion

        /// <summary>
        /// The CanvasRenderingContext2D rendering context contains a variety of drawing style states (attributes for
        /// line styles, fill styles, shadow styles, text styles). The following methods help you to work with that state
        /// </summary>
        #region Canvas State
        
        /// <summary>
        /// Saves the current drawing style state using a stack so you can revert any change you make to it
        /// using Restore().
        /// </summary>
        public virtual void Save()
        {
            return;
        }

        /// <summary>
        /// Restores the drawing style state to the last element on the 'state stack' saved by save().
        /// </summary>
        public virtual void Restore()
        {
            return;
        }

        /// <summary>
        /// A read-only back-reference to the HTMLCanvasElement.
        /// Might be null if it is not associated with a <canvas> element.
        /// </summary>
        public readonly CanvasElement Canvas;
        #endregion

        #region Hit Regions

        /// <summary>
        /// Adds a hit region to the canvas.
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public virtual void AddHitRegion()
        {
            return;
        }

        /// <summary>
        /// Removes the hit region with the specified id from the canvas.
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public virtual void RemoveHitRegion()
        {
            return;
        }

        /// <summary>
        /// Removes all hit regions from the canvas.
        /// </summary>
        /// <remarks>This is experimental API that should not be used in production code.</remarks>
        public virtual void ClearHitRegions()
        {
            return;
        }

        #endregion

        #region Non-standard APIs
        // Non-standard APIs are not implemented on Bridge.NET.

        /// <summary>
        /// Most of these APIs are deprecated and will be removed in the future.
        /// </summary>
        #endregion
    }
}