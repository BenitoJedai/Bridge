using Bridge;

namespace Bridge.Html5
{
    public partial class CanvasTypes
    {
        /// <summary>
        /// Indicates the 2D context on CanvaslElement.GetContext().
        /// </summary>
        [Ignore]
        [Enum(Emit.StringNameLowerCase)]
        [Name("String")]
        public enum CanvasContext2DType
        {
            /// <summary>
            /// 2d: Two-dimensional rendering context: CanvasRenderingContext2D
            /// </summary>
            [Name("2d")]
            CanvasRenderingContext2D
        }

        /// <summary>
        /// Indicates the WebGL (OpenGL ES 2.0) context on CanvaslElement.GetContext().
        /// </summary>
        [Ignore]
        [Enum(Emit.StringNameLowerCase)]
        [Name("String")]
        public enum CanvasContextWebGLType
        {
            /// <summary>
            /// WebGL -- available only on browsers that implement WebGL version 1 (OpenGL ES 2.0)
            /// </summary>
            WebGL,

            /// <summary>
            /// Experimental WebGL -- available only on browsers that implement WebGL version 1 (OpenGL ES 2.0)
            /// </summary>
            [Name("experimental-webgl")]
            Experimental_WebGL
        }

        /// <summary>
        /// Indicates the WebGL 2 (OpenGL ES 3.0) context on CanvaslElement.GetContext().
        /// </summary>
        [Ignore]
        [Enum(Emit.StringNameLowerCase)]
        [Name("String")]
        public enum CanvasContextWebGL2Type
        {
            /// <summary>
            /// WebGL2 -- available only on browsers that implement WebGL version 2 (OpenGL ES 3.0)
            /// </summary>
            WebGL2,

            /// <summary>
            /// Experimental WebGL2 -- available only on browsers that implement WebGL version 2 (OpenGL ES 3.0)
            /// </summary>
            [Name("experimental-webgl2")]
            Experimental_WebGL2
        }
    }
}