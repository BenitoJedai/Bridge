using System;

using Bridge.CLR;

namespace Bridge.Html5
{
    [Ignore]
    [Name("DocumentFragment")]
    public class DocumentFragment : Node
    {
        /// <summary>
        /// Returns a live HTMLCollection containing all objects of type Node that are children of the DocumentFragment object.
        /// </summary>
        public readonly HTMLCollection Children;

        /// <summary>
        /// Returns the Element that is the first child of the DocumentFragment object, or null if there is none.
        /// </summary>
        public readonly Element FirstElementChild;

        /// <summary>
        /// Returns the Element that is the last child of the DocumentFragment object, or null if there is none.
        /// </summary>
        public readonly Element LastElementChild;

        /// <summary>
        /// Returns an unsigned long giving the amount of children that the DocumentFragment has.
        /// </summary>
        public readonly int ChildElementCount;

        /// <summary>
        /// Returns the first Element node within the DocumentFragment, in document order, that matches the specified selectors.
        /// </summary>
        /// <param name="selectors">String containing one or more CSS selectors separated by commas.</param>
        /// <returns></returns>
        public Element QuerySelector(string selectors)
        {
            return null;
        }

        /// <summary>
        /// Returns a NodeList of all the Element nodes within the DocumentFragment that match the specified selectors.
        /// </summary>
        /// <param name="selectors">String containing one or more CSS selectors separated by commas.</param>
        /// <returns></returns>
        public NodeList QuerySelectorAll(string selectors)
        {
            return null;
        }

        /// <summary>
        /// Returns the first Element node within the DocumentFragment, in document order, that matches the specified ID.
        /// </summary>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public Element GetElementById(string elementId)
        {
            return null;
        }
    }
}
