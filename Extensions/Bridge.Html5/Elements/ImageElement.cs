using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLImageElement interface provides special properties and methods (beyond the regular HTMLElement interface it also has available to it by inheritance) for manipulating the layout and presentation of <img> elements.
    /// </summary>
    [Ignore]
    [Name("HTMLImageElement")]
    public class ImageElement : Element
    {
        [Template("document.createElement('img')")]
        public ImageElement()
        {
        }

        /// <summary>
        /// Reflects the alt HTML attribute, indicating fallback context for the image.
        /// </summary>
        public string Alt;

        /// <summary>
        /// Width of the border around the image.
        /// </summary>
        public int Border;

        /// <summary>
        /// True if the browser has fetched the image, and it is in a supported image type that was decoded without errors.
        /// </summary>
        public bool Complete;

        /// <summary>
        /// The CORS setting for this image element. See CORS settings attributes for details.
        /// </summary>
        public string CrossOrigin;

        /// <summary>
        /// Reflects the height HTML attribute, indicating the rendered height of the image in CSS pixels.
        /// </summary>
        public int Height;

        /// <summary>
        /// Space to the left and right of the image.
        /// </summary>
        [Name("hspace")]
        public int HSpace;

        /// <summary>
        /// Reflects the ismap HTML attribute, indicating that the image is part of a server-side image map.
        /// </summary>
        public bool IsMap;

        /// <summary>
        /// URI of a long description of the image.
        /// </summary>
        public string LongDesc;

        /// <summary>
        /// A reference to a low-quality (but faster to load) copy of the image.
        /// </summary>
        public string LowSrc;

        /// <summary>
        /// Reflects the name HTML attribute, containing a name by which to refer to the <img>.
        /// </summary>
        public string Name;

        /// <summary>
        /// Intrinsic height of the image in CSS pixels, if it is available; otherwise, 0.
        /// </summary>
        public int NaturalHeight;

        /// <summary>
        /// Intrinsic width of the image in CSS pixels, if it is available; otherwise, 0.
        /// </summary>
        public int NaturalWidth;

        /// <summary>
        /// Reflects the src HTML attribute, containing the full URL of the image including base URI.
        /// </summary>
        public string Src;

        /// <summary>
        /// The srcset attribute allows authors to provide a set of images to handle graphical displays of varying dimensions and pixel densities.
        /// </summary>
        public string SrcSet;

        /// <summary>
        /// Reflects the usemap HTML attribute, containing a partial URL of a map element.
        /// </summary>
        public string UseMap;

        /// <summary>
        /// Space above and below the image.
        /// </summary>
        [Name("vspace")]
        public int VSpace;

        /// <summary>
        /// Reflects the width HTML attribute, indicating the rendered width of the image in CSS pixels.
        /// </summary>
        public int Width;

        /// <summary>
        /// The horizontal offset from the nearest layer. (Mimic an old Netscape 4 behavior)
        /// </summary>
        public int X;

        /// <summary>
        /// The vertical offset from the nearest layer. (Mimic an old Netscape 4 behavior)
        /// </summary>
        public int Y;
    }
}