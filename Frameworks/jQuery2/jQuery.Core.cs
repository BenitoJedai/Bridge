using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections.Generic;

namespace Bridge.jQuery2
{    
    public partial class jQuery
    {
        [Template("$()")]
        public jQuery()
        {
        }

        [Template("$({0})")]
        public jQuery(object obj)
        {
        }
        
        [Template("$({0}, {1})")]
        public jQuery(object obj, object context)
        {
        }

        /// <summary>
        /// Accepts a string containing a CSS selector which is then used to match a set of elements.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        [Template("$({0})")]
        public jQuery(string selector)
        {
        }

        /// <summary>
        /// Accepts a string containing a CSS selector which is then used to match a set of elements.
        /// </summary>
        /// <param name="selector">A string containing a selector expression</param>
        /// <param name="context">A DOM Element, Document, or jQuery to use as context</param>
        [Template("$({0}, {1})")]
        public jQuery(string selector, DocumentInstance context)
        {
        }

        [Template("$({0})")]
        public jQuery(Element element)
        {
        }

        [Template("$()")]
        public static jQuery Fn()
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Fn(object obj)
        {
            return null;
        }

        [Template("$({0}, {1})")]
        public static jQuery Fn(object obj, object context)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Element(Element element)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Element(IEnumerable<Element> elements)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Element(WindowInstance window)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Element(DocumentInstance document)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Element(jQuery element)
        {
            return null;
        }

        /// TODO: Conflict with non-static jQuery Html(string htmlString) Method
        //[Template("$({0})")]
        //public static jQuery Html(string html)
        //{
        //    return null;
        //}

        [Template("$({0}, {1})")]
        public static jQuery Html(string html, DocumentInstance ownerDocument)
        {
            return null;
        }

        [Template("$({0}, {1})")]
        public static jQuery Html(string html, object attributes)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Object(object obj)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Ready(Action callback)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery OnDocumentReady(Action callback)
        {
            return null;
        }

        [Template("$({0})")]
        public static jQuery Select(string selector)
        {
            return null;
        }

        [Template("$({0}, {1})")]
        public static jQuery Select(string selector, DocumentInstance context)
        {
            return null;
        }

        [Template("$({0}, {1})")]
        public static jQuery Select(string selector, Element context)
        {
            return null;
        }

        [Template("$({0}, {1})")]
        public static jQuery Select(string selector, jQuery context)
        {
            return null;
        }

        /// <summary>
        /// Holds or releases the execution of jQuery's ready event.
        /// </summary>
        /// <param name="hold">Indicates whether the ready hold is being requested or released</param>
        public static void HoldReady(bool hold)
        {
        }

        /// <summary>
        /// Relinquish jQuery's control of the $ variable.
        /// </summary>
        public static void NoConflict()
        {
        }

        /// <summary>
        /// Relinquish jQuery's control of the $ variable.
        /// </summary>
        /// <param name="removeAll">A Boolean indicating whether to remove all jQuery variables from the global scope (including jQuery itself).</param>
        public static void NoConflict(bool removeAll)
        {
        }

        //TODO: jQuery.when
    }
}
