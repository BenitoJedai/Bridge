using System;
using Bridge.CLR;

namespace Bridge.Html5
{
    /// <summary>
    /// The HTMLAudioElement interface provides access to the properties of <audio> elements, as well as methods to manipulate them. It derives from the HTMLMediaElement interface.
    /// </summary>
    [Ignore]
    [Name("HTMLAudioElement")]
    [Constructor("Audio")]
    public class AudioElement : Element
    {
        /// <summary>
        /// Constructor for audio elements. The preload property of the returned object is set to auto and the src property is set to the argument value. The browser begins asynchronously selecting the resource before returning the object.
        /// </summary>
        public AudioElement()
        {
        }

        /// <summary>
        /// Constructor for audio elements. The preload property of the returned object is set to auto and the src property is set to the argument value. The browser begins asynchronously selecting the resource before returning the object.
        /// </summary>
        /// <param name="urlString">The src property of the constructed HTMLAudioElement.</param>
        public AudioElement(string urlString)
        {
        }
    }
}