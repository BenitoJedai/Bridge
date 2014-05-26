using System;
namespace Bridge.CLR.Html
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("document")]
    public static class Document
    {
        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(string type, Action listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(EventType type, Action listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(string type, Action<Event> listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(EventType type, Action<Event> listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(string type, Action listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(EventType type, Action listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(string type, Action<Event> listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(EventType type, Action<Event> listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(string type, IEventListener listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        public static void AddEventListener(EventType type, IEventListener listener)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(string type, IEventListener listener, bool useCapture)
        {
        }

        /// <summary>
        /// The method registers the specified listener on the EventTarget it's called on. The event target may be an Element in a document, the Document itself, a Window, or any other object that supports events.
        /// </summary>
        /// <param name="type">A string representing the event type to listen for.</param>
        /// <param name="listener">The object that receives a notification when an event of the specified type occurs. This must be an object implementing the EventListener interface, or simply a JavaScript function.</param>
        /// <param name="useCapture"></param>
        public static void AddEventListener(EventType type, IEventListener listener, bool useCapture)
        {
        }

        /// Dispatches the specified event to the current element.
        /// To create an event object use the createEvent method in Firefox, Opera, Google Chrome, Safari and Internet Explorer from version 9. After the new event is created, initialize it first (for details, see the page for the createEvent method). When the event is initialized, it is ready for dispatching.
        /// </summary>
        /// <param name="e">Required. Reference to an event object to be dispatched.</param>
        /// <returns>Boolean that indicates whether the default action of the event was not canceled.</returns>
        public static bool DispatchEvent(Event e)
        {
            return false;
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(EventType type, IEventListener listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(EventType type, IEventListener listener, bool capture)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(string type, IEventListener listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(string type, IEventListener listener, bool capture)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(EventType type, Action listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(EventType type, Action listener, bool capture)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(string type, Action listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(string type, Action listener, bool capture)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(EventType type, Action<Event> listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(EventType type, Action<Event> listener, bool capture)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        public static void RemoveEventListener(string type, Action<Event> listener)
        {
        }

        /// <summary>
        /// Removes the event listener previously registered with EventTarget.addEventListener.
        /// </summary>
        /// <param name="type">A string representing the event type being removed.</param>
        /// <param name="listener">The listener parameter indicates the EventListener function to be removed.</param>
        /// <param name="capture">Specifies whether the EventListener being removed was registered as a capturing listener or not. If not specified, useCapture defaults to false.</param>
        public static void RemoveEventListener(string type, Action<Event> listener, bool capture)
        {
        }


        /// <summary>
        /// Returns a DOMString representing the base URL. The concept of base URL changes from one language to another; in HTML, it corresponds to the protocol, the domain name and the directory structure, that is all until the last '/'.
        /// </summary>
        public static readonly string BaseURI;

        /// <summary>
        /// Returns a live NodeList containing all the children of this node. NodeList being live means that if the children of the Node change, the NodeList object is automatically updated.
        /// </summary>
        public static readonly ClientRectList ChildNodes;

        /// <summary>
        /// Returns a Node representing the first direct child node of the node, or null if the node has no child.
        /// </summary>
        public static readonly Node FirstChild;

        /// <summary>
        /// Returns a Node representing the last direct child node of the node, or null if the node has no child.
        /// </summary>
        public static readonly Node LastChild;

        /// <summary>
        /// Returns a Node representing the next node in the tree, or null if there isn't such node.
        /// </summary>
        public static readonly Node NextSibling;

        /// <summary>
        /// Returns a DOMString containing the name of the Node. The structure of the name will differ with the name type. E.g. An HTMLElement will contain the name of the corresponding tag, like 'audio' for an HTMLAudioElement, a Text node will have the '#text' string, or a Document node will have the '#document' string.
        /// </summary>
        public static readonly string NodeName;

        /// <summary>
        /// Returns an unsigned short representing the type of the node.
        /// </summary>
        public static readonly NodeType NodeType;

        /// <summary>
        /// Is a DOMString representing the value of an object. For most Node type, this returns null and any set operation is ignored. For nodes of type TEXT_NODE (Text objects), COMMENT_NODE (Comment objects), and PROCESSING_INSTRUCTION_NODE (ProcessingInstruction objects), the value corresponds to the text data contained in the object.
        /// </summary>
        public static string NodeValue;

        /// <summary>
        /// Returns the Document that this node belongs to. If no document is associated with it, returns null.
        /// </summary>
        public static readonly DocumentInstance OwnerDocument;

        /// <summary>
        /// Returns a Node that is the parent of this node. If there is no such node, like if this node is the top of the tree or if doesn't participate in a tree, this property returns null.
        /// </summary>
        public static readonly Node ParentNode;

        /// <summary>
        /// Returns an Element that is the parent of this node. If the node has no parent, or if that parent is not an Element, this property returns null.
        /// </summary>
        public static readonly Element ParentElement;

        /// <summary>
        /// Returns a Node representing the previous node in the tree, or null if there isn't such node.
        /// </summary>
        public static readonly Element PreviousSibling;

        /// <summary>
        /// Is a DOMString representing the textual content of an element and all its descendants.
        /// </summary>
        public static readonly string TextContent;

        /// <summary>
        /// Adds a node to the end of the list of children of a specified parent node. If the node already exists it is removed from current parent node, then added to new parent node.
        /// </summary>
        /// <param name="child">child is the node to append underneath element. Also returned.</param>
        /// <returns></returns>
        public static Node AppendChild(Node child)
        {
            return null;
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <returns>The new node that will be a clone of this node</returns>
        public static Node CloneNode()
        {
            return null;
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The new node that will be a clone of this node</returns>
        public static Node CloneNode(bool deep)
        {
            return null;
        }

        /// <summary>
        /// Compares the position of the current node against another node in any other document.
        /// </summary>
        /// <param name="node">is the node that's being compared against.</param>
        /// <returns>The return value is computed as the relationship that otherNode has with node.</returns>
        public static DocumentPosition CompareDocumentPosition(Node node)
        {
            return default(DocumentPosition);
        }

        /// <summary>
        /// Indicates whether a node is a descendant of a given node.
        /// </summary>
        /// <param name="node">is the node that's being compared against.</param>
        /// <returns>The return value is true if otherNode is a descendant of node, or node itself. Otherwise the return value is false.</returns>
        public static bool Contains(Node node)
        {
            return false;
        }

        /// <summary>
        ///  returns a Boolean value indicating whether the current Node has child nodes or not.
        /// </summary>
        /// <returns>Boolean value indicating whether the current Node has child nodes or not</returns>
        public static bool HasChildNodes()
        {
            return false;
        }

        /// <summary>
        /// Inserts the specified node before a reference element as a child of the current node.
        /// </summary>
        /// <param name="newElement">The node to insert.</param>
        /// <param name="referenceElement">The node before which newElement is inserted.</param>
        /// <returns>The node being inserted, that is newElement</returns>
        public static Node InsertBefore(Node newElement, Node referenceElement)
        {
            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">string representing the namespace against which the element will be checked.</param>
        /// <returns>holds the return value true or false.</returns>
        public static bool IsDefaultNamespace(string namespaceURI)
        {
            return false;
        }

        /// <summary>
        /// Tests whether two nodes are equal.
        /// </summary>
        /// <param name="node">The node to compare equality with.</param>
        /// <returns></returns>
        public static bool IsEqualNode(Node node)
        {
            return false;
        }

        /// <summary>
        /// Clean up all the text nodes under this element (merge adjacent, remove empty).
        /// </summary>
        public static void Normalize()
        {
        }

        /// <summary>
        /// Removes a child node from the DOM. Returns removed node.
        /// </summary>
        /// <param name="child">child node to be removed from the DOM.</param>
        /// <returns>Reference to the removed child node</returns>
        public static Node RemoveChild(Node child)
        {
            return null;
        }

        /// <summary>
        /// Replaces one child node of the specified element with another.
        /// </summary>
        /// <param name="newChild">new node to replace oldChild. If it already exists in the DOM, it is first removed.</param>
        /// <param name="oldChild">the existing child to be replaced.</param>
        /// <returns>the replaced node. This is the same node as oldChild.</returns>
        public static Node ReplaceChild(Node newChild, Node oldChild)
        {
            return null;
        }

        /// <summary>
        /// Returns the Element immediately prior to this ChildNode in its parent's children list, or null if there is no Element in the list prior to this ChildNode.
        /// </summary>
        public static readonly Element PreviousElementSibling;

        /// <summary>
        /// Returns the Element immediately following this ChildNode in its parent's children list, or null if there is no Element in the list following this ChildNode.
        /// </summary>
        public static readonly Element NextElementSibling;

        /// <summary>
        /// Collection of all attribute nodes registered to the specified node.
        /// </summary>
        public static readonly NamedNodeMap Attributes;

        /// <summary>
        /// The number of child nodes that are elements.
        /// </summary>
        public static readonly int ChildElementCount;

        /// <summary>
        /// All child elements of an element as a collection.
        /// </summary>
        public static readonly HTMLCollection Children;

        /// <summary>
        /// Token list of class attribute
        /// </summary>
        public static readonly TokenList ClassList;

        /// <summary>
        /// gets and sets the value of the class attribute of the specified element.
        /// </summary>
        public static string ClassName;

        /// <summary>
        /// The Element.clientHeight read-only property returns the inner height of an element in pixels, including padding but not the horizontal scrollbar height, border, or margin.
        /// </summary>
        public static readonly int ClientHeight;

        /// <summary>
        /// The width of the left border of an element in pixels. It includes the width of the vertical scrollbar if the text direction of the element is right–to–left and if there is an overflow causing a left vertical scrollbar to be rendered. clientLeft does not include the left margin or the left padding. clientLeft is read-only.
        /// </summary>
        public static readonly int ClientLeft;

        /// <summary>
        /// The width of the top border of an element in pixels. It does not include the top margin or padding. clientTop is read-only.
        /// </summary>
        public static readonly int ClientTop;

        /// <summary>
        /// The Element.clientWidth property is the inner width of an element in pixels. It includes padding but not the vertical scrollbar (if present, if rendered), border or margin.
        /// </summary>
        public static readonly int ClientWidth;

        /// <summary>
        /// The ParentNode.firstElementChild read-only property returns the object's first child Element, or null if there are no child elements.
        /// </summary>
        public static readonly Element FirstElementChild;

        /// <summary>
        /// Gets or sets the element's identifier (attribute id).
        /// </summary>
        public static string Id;

        /// <summary>
        /// The innerHTML sets or gets the HTML syntax describing the element's descendants.    
        /// </summary>
        public static string InnerHTML;

        /// <summary>
        /// The ParentNode.lastElementChild read-only method returns the object's last child Element or null if there are no child elements.
        /// </summary>
        public static readonly Element LastElementChild;

        /// <summary>
        /// The outerHTML attribute of the element DOM interface gets the serialized HTML fragment describing the element including its descendants. It can be set to replace the element with nodes parsed from the given string.
        /// </summary>
        public static readonly string OuterHTML;

        /// <summary>
        /// The Element.scrollHeight read-only attribute is a measurement of the height of an element's content including content not visible on the screen due to overflow. The scrollHeight value is equal to the minimum clientHeight the element would require in order to fit all the content in the viewpoint without using a vertical scrollbar. It includes the element padding but not its margin.
        /// </summary>
        public static readonly int ScrollHeight;

        /// <summary>
        /// The Element.scrollLeft property gets or sets the number of pixels that an element's content is scrolled to the left.
        /// </summary>
        public static readonly int ScrollLeft;

        /// <summary>
        /// The Element.scrollTop property gets or sets the number of pixels that the content of an element is scrolled upward. An element's scrollTop is a measurement of the distance of an element's top to its topmost visible content. When an element content does not generate a vertical scrollbar, then its scrollTop value defaults to 0.
        /// </summary>
        public static readonly int ScrollTop;

        /// <summary>
        /// The Element.scrollWidth read–only property returns either the width in pixels of the content of an element or the width of the element itself, whichever is greater. If the element is wider than its content area (for example, if there are scroll bars for scrolling through the content), the scrollWidth is larger than the clientWidth.
        /// </summary>
        public static readonly int ScrollWidth;

        /// <summary>
        /// Returns the name of the element.
        /// </summary>
        public static readonly string TagName;

        /// <summary>
        /// The oncopy property returns the onCopy event handler code on the current element.
        /// </summary>
        [Bridge.CLR.Name("oncopy")]
        public static Delegate OnCopy;

        /// <summary>
        /// Returns the event handling code for the cut event.
        /// </summary>
        [Bridge.CLR.Name("oncut")]
        public static Delegate OnCut;

        /// <summary>
        /// Returns the event handling code for the paste event.
        /// </summary>
        [Bridge.CLR.Name("onpaste")]
        public static Delegate OnPaste;

        /// <summary>
        /// Returns the event handling code for the wheel event.
        /// </summary>
        [Bridge.CLR.Name("onwheel")]
        public static Delegate OnWheel;

        /// <summary>
        /// Returns the value of a specified attribute on the element. If the given attribute does not exist, the value returned will either be null or "" (the empty string)
        /// </summary>
        /// <param name="attributeName">name of the attribute whose value you want to get.</param>
        /// <returns>string containing the value of attributeName.</returns>
        /// 
        public static string GetAttribute(string attributeName)
        {
            return null;
        }

        /// <summary>
        ///  returns the string value of the attribute with the specified namespace and name. If the named attribute does not exist, the value returned will either be null or "" (the empty string); 
        /// </summary>
        /// <param name="namespace">The namespace in which to look for the specified attribute.</param>
        /// <param name="attributeName"></param>
        /// <returns>The string value of the specified attribute. If the attribute doesn't exist, the result is null.</returns>
        public static string GetAttributeNS(string @namespace, string attributeName)
        {
            return null;
        }

        /// <summary>
        /// The Element.getBoundingClientRect() method returns a text rectangle object that encloses a group of text rectangles.
        /// </summary>
        /// <returns></returns>
        public static ClientRect GetBoundingClientRect()
        {
            return null;
        }

        /// <summary>
        /// The Element.getClientRects() method returns a collection of rectangles that indicate the bounding rectangles for each box in a client.
        /// </summary>
        /// <returns></returns>
        public static ClientRectList GetClientRects()
        {
            return null;
        }

        /// <summary>
        /// Returns an array-like object of all child elements which have all of the given class names.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HTMLCollection GetElementsByClassName(string name)
        {
            return null;
        }

        /// <summary>
        /// Retrieve a set of all descendant elements, of a particular tag name, from the current element.
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public static HTMLCollection GetElementsByTagName(string tagName)
        {
            return null;
        }

        /// <summary>
        /// Retrieve a set of all descendant elements, of a particular tag name and namespace, from the current element.
        /// </summary>
        /// <param name="namespaceURI">namespace URI of elements to look for</param>
        /// <param name="localName">local name of elements to look for or the special value "*", which matches all elements </param>
        /// <returns></returns>
        public static HTMLCollection GetElementsByTagNameNS(string namespaceURI, string localName)
        {
            return null;
        }

        /// <summary>
        /// Check if the element has the specified attribute, or not.
        /// </summary>
        /// <param name="attName">string representing the name of the attribute.</param>
        /// <returns>holds the return value true or false.</returns>
        public static bool HasAttribute(string attName)
        {
            return false;
        }

        /// <summary>
        /// Check if the element has the specified attribute, in the specified namespace, or not.
        /// </summary>
        /// <param name="namespace">string specifying the namespace of the attribute.</param>
        /// <param name="localName">name of the attribute.</param>
        /// <returns></returns>
        public static bool HasAttributeNS(string @namespace, string localName)
        {
            return false;
        }

        /// <summary>
        /// Returns the first element that is a descendant of the element on which it is invoked that matches the specified group of selectors. 
        /// </summary>
        /// <param name="selectors">selectors is a group of selectors to match on.</param>
        /// <returns></returns>
        public static Node QuerySelector(string selectors)
        {
            return null;
        }

        /// <summary>
        /// Returns a non-live NodeList of all elements descended from the element on which it is invoked that match the specified group of CSS selectors.
        /// </summary>
        /// <param name="selectors">selectors is a group of selectors to match on.</param>
        /// <returns></returns>
        public static NodeList QuerySelectorAll(string selectors)
        {
            return null;
        }

        /// <summary>
        /// The ChildNode.remove method removes the object from the tree it belongs to.
        /// </summary>
        public static void Remove()
        {
        }

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="attrName">String that names the attribute to be removed from element.</param>
        public static void RemoveAttribute(string attrName)
        {
        }

        /// <summary>
        /// Remove the attribute with the specified name and namespace, from the current node.
        /// </summary>
        /// <param name="namespaceURI">String that contains the namespace of the attribute.</param>
        /// <param name="attrName">String that names the attribute to be removed from the current node.</param>
        public static void RemoveAttributeNS(string namespaceURI, string attrName)
        {
        }

        /// <summary>
        /// Scrolls the page until the element gets into the view.
        /// </summary>
        /// <param name="alignWithTop">If true, the scrolled element is aligned with the top of the scroll area. If false, it is aligned with the bottom.</param>
        public static void ScrollIntoView(bool alignWithTop)
        {
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="name">the name of the attribute as a string.</param>
        /// <param name="value">the desired new value of the attribute.</param>
        public static void SetAttribute(string name, string value)
        {
        }

        /// <summary>
        /// Adds a new attribute or changes the value of an attribute with the given namespace and name.
        /// </summary>
        /// <param name="namespaceURI">String specifying the namespace of the attribute.</param>
        /// <param name="name">string identifying the attribute to be set.</param>
        /// <param name="value">the desired string value of the new attribute.</param>
        public static void SetAttributeNS(string namespaceURI, string name, string value)
        {
        }

        /// <summary>
        /// Call this method during the handling of a mousedown event to retarget all mouse events to this element until the mouse button is released or document.releaseCapture() is called.
        /// </summary>
        public static void SetCapture()
        {
        }

        /// <summary>
        /// Call this method during the handling of a mousedown event to retarget all mouse events to this element until the mouse button is released or document.releaseCapture() is called.
        /// </summary>
        /// <param name="retargetToElement">If true, all events are targeted directly to this element; if false, events can also fire at descendants of this element.</param>
        public static void SetCapture(bool retargetToElement)
        {
        }

        /// <summary>
        /// Parses the specified text as HTML or XML and inserts the resulting nodes into the DOM tree at a specified position. It does not reparse the element it is being used on and thus it does not corrupt the existing elements inside the element. This, and avoiding the extra step of serialization make it much faster than direct innerHTML manipulation.
        /// </summary>
        /// <param name="position">The position relative to the element</param>
        /// <param name="text">String to be parsed as HTML or XML and inserted into the tree.</param>
        public static void InsertAdjacentHTML(InsertPosition position, string text)
        {
        }

        /// <summary>
        /// The access key assigned to the element.
        /// </summary>
        public static readonly string AccessKey;

        /// <summary>
        /// A string that represents the element's assigned access key.
        /// </summary>
        public static readonly string AccessKeyLabel;

        /// <summary>
        /// Gets/sets whether or not the element is editable.
        /// </summary>
        public static ContentEditable ContentEditable;

        /// <summary>
        /// Indicates whether or not the content of the element can be edited.
        /// </summary>
        public static readonly bool IsContentEditable;

        /// <summary>
        /// Allows access to read and write custom data attributes (data-*) of the element.
        /// </summary>
        public static readonly DOMStringMap Dataset;

        /// <summary>
        /// The HTMLElement.dir attribute gets or sets the text writing directionality of the content of the current element.
        /// </summary>
        public static TextDirection Dir;

        /// <summary>
        /// Gets/sets the language of an element's attributes, text, and element contents.
        /// </summary>
        public static string Lang;

        /// <summary>
        /// The height of an element, relative to the layout.
        /// </summary>
        public static readonly int OffsetHeight;

        /// <summary>
        /// The distance from this element's left border to its offsetParent's left border.
        /// </summary>
        public static readonly int OffsetLeft;

        /// <summary>
        /// The element from which all offset calculations are currently computed.
        /// </summary>
        public static readonly Element OffsetParent;

        /// <summary>
        /// The distance from this element's top border to its offsetParent's top border.
        /// </summary>
        public static readonly int OffsetTop;

        /// <summary>
        /// The width of an element, relative to the layout.
        /// </summary>
        public static readonly int OffsetWidth;

        /// <summary>
        /// An object representing the declarations of an element's style attributes.
        /// </summary>
        public static readonly CSSStyleDeclaration Style;

        /// <summary>
        /// Gets/sets the position of the element in the tabbing order.
        /// </summary>
        public static int TabIndex;

        /// <summary>
        /// A string that appears in a popup box when mouse is over the element.
        /// </summary>
        public static string Title;

        /// <summary>
        ///  EventHandler representing the code to be called when the abort event is raised.
        /// </summary>
        [Bridge.CLR.Name("onabort")]
        public static Action<Event> OnAbort;

        /// <summary>
        /// EventHandler representing the code to be called when the blur event is raised.
        /// </summary>
        [Bridge.CLR.Name("onblur")]
        public static Action<Event> OnBlur;

        /// <summary>
        /// OnErrorEventHandler representing the code to be called when the error event is raised.
        /// </summary>
        [Bridge.CLR.Name("onerror")]
        public static ErrorEventHandler OnError;

        /// <summary>
        /// EventHandler representing the code to be called when the focus event is raised.
        /// </summary>
        [Bridge.CLR.Name("onfocus")]
        public static Action<Event> OnFocus;

        /// <summary>
        /// EventHandler representing the code to be called when the cancel event is raised.
        /// </summary>
        [Bridge.CLR.Name("oncancel")]
        public static Action<Event> OnCancel;

        /// <summary>
        /// EventHandler representing the code to be called when the canplay event is raised
        /// </summary>
        [Bridge.CLR.Name("oncanplay")]
        public static Action<Event> OnCanPlay;

        /// <summary>
        /// EventHandler representing the code to be called when the canplaythrough event is raised.
        /// </summary>
        [Bridge.CLR.Name("oncanplaythrough")]
        public static Action<Event> OnCanPlayThrough;

        /// <summary>
        /// EventHandler representing the code to be called when the change event is raised.
        /// </summary>
        [Bridge.CLR.Name("onchange")]
        public static Action<Event> OnChange;

        /// <summary>
        /// EventHandler representing the code to be called when the click event is raised.
        /// </summary>
        [Bridge.CLR.Name("onclick")]
        public static Action<Event> OnClick;

        /// <summary>
        /// EventHandler representing the code to be called when the close event is raised.
        /// </summary>
        [Bridge.CLR.Name("onclose")]
        public static Action<Event> OnClose;

        /// <summary>
        /// EventHandler representing the code to be called when the contextmenu event is raised.
        /// </summary>
        [Bridge.CLR.Name("oncontextmenu")]
        public static Action<Event> OnContextMenu;

        /// <summary>
        /// EventHandler representing the code to be called when the cuechange event is raised.
        /// </summary>
        [Bridge.CLR.Name("oncuechange")]
        public static Action<Event> OnCueChange;

        /// <summary>
        /// EventHandler representing the code to be called when the dblclick event is raised.
        /// </summary>
        [Bridge.CLR.Name("ondblclick")]
        public static Action<Event> OnDblClick;

        /// <summary>
        /// EventHandler representing the code to be called when the drag event is raised.
        /// </summary>
        [Bridge.CLR.Name("ondrag")]
        public static Action<Event> OnDrag;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragend event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragend")]
        public static Action<Event> OnDragEnd;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragenter event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragenter")]
        public static Action<Event> OnDragEnter;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragexit event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragexit")]
        public static Action<Event> OnDragExit;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragleave event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragleave")]
        public static Action<Event> OnDragLeave;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragover event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragover")]
        public static Action<Event> OnDragOver;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the dragstart event is raised
        /// </summary>
        [Bridge.CLR.Name("ondragstart")]
        public static Action<Event> OnDragStart;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the drop event is raised
        /// </summary>
        [Bridge.CLR.Name("ondrop")]
        public static Action<Event> OnDrop;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the durationchange event is raised
        /// </summary>
        [Bridge.CLR.Name("ondurationchange")]
        public static Action<Event> OnDurationChange;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the emptied event is raised
        /// </summary>
        [Bridge.CLR.Name("onemptied")]
        public static Action<Event> OnEmptied;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the ended event is raised
        /// </summary>
        [Bridge.CLR.Name("onended")]
        public static Action<Event> OnEnded;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the input event is raised
        /// </summary>
        [Bridge.CLR.Name("oninput")]
        public static Action<Event> OnInput;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the invalid event is raised
        /// </summary>
        [Bridge.CLR.Name("oninvalid")]
        public static Action<Event> OnInvalid;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the keydown event is raised
        /// </summary>
        [Bridge.CLR.Name("onkeydown")]
        public static Action<Event> OnKeyDown;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the keypress event is raised
        /// </summary>
        [Bridge.CLR.Name("onkeypress")]
        public static Action<Event> OnKeyPress;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the keyup event is raised
        /// </summary>
        [Bridge.CLR.Name("onkeyup")]
        public static Action<Event> OnKeyUp;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the load event is raised
        /// </summary>
        [Bridge.CLR.Name("onload")]
        public static Action<Event> OnLoad;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the loadeddata event is raised
        /// </summary>
        [Bridge.CLR.Name("onloadeddata")]
        public static Action<Event> OnLoadedData;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the loadedmetadata event is raised
        /// </summary>
        [Bridge.CLR.Name("onloadedmetadata")]
        public static Action<Event> OnLoadedMetaData;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the loadstart event is raised
        /// </summary>
        [Bridge.CLR.Name("onloadstart")]
        public static Action<Event> OnLoadStart;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mousedown event is raised
        /// </summary>
        [Bridge.CLR.Name("onmousedown")]
        public static Action<Event> OnMouseDown;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mouseenter event is raised
        /// </summary>
        [Bridge.CLR.Name("onmouseenter")]
        public static Action<Event> OnMouseEnter;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mouseleave event is raised
        /// </summary>
        [Bridge.CLR.Name("onmouseleave")]
        public static Action<Event> OnMouseLeave;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mousemove event is raised
        /// </summary>
        [Bridge.CLR.Name("onmousemove")]
        public static Action<Event> OnMouseMove;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mouseout event is raised
        /// </summary>
        [Bridge.CLR.Name("onmouseout")]
        public static Action<Event> OnMouseOut;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mouseover event is raised
        /// </summary>
        [Bridge.CLR.Name("onmouseover")]
        public static Action<Event> OnMouseOver;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mouseup event is raised
        /// </summary>
        [Bridge.CLR.Name("onmouseup")]
        public static Action<Event> OnMouseUp;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the mousewheel event is raised
        /// </summary>
        [Bridge.CLR.Name("onmousewheel")]
        public static Action<Event> OnMouseWheel;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the pause event is raised
        /// </summary>
        [Bridge.CLR.Name("onpause")]
        public static Action<Event> OnPause;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the play event is raised
        /// </summary>
        [Bridge.CLR.Name("onplay")]
        public static Action<Event> OnPlay;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the playing event is raised
        /// </summary>
        [Bridge.CLR.Name("onplaying")]
        public static Action<Event> OnPlaying;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the progress event is raised
        /// </summary>
        [Bridge.CLR.Name("onprogress")]
        public static Action<Event> OnProgress;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the ratechange event is raised
        /// </summary>
        [Bridge.CLR.Name("onratechange")]
        public static Action<Event> OnRateChange;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the reset event is raised
        /// </summary>
        [Bridge.CLR.Name("onreset")]
        public static Action<Event> OnReset;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the scroll event is raised
        /// </summary>
        [Bridge.CLR.Name("onscroll")]
        public static Action<Event> OnScroll;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the seeked event is raised
        /// </summary>
        [Bridge.CLR.Name("onseeked")]
        public static Action<Event> OnSeeked;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the seeking event is raised
        /// </summary>
        [Bridge.CLR.Name("onseeking")]
        public static Action<Event> OnSeeking;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the select event is raised
        /// </summary>
        [Bridge.CLR.Name("onselect")]
        public static Action<Event> OnSelect;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the show event is raised
        /// </summary>
        [Bridge.CLR.Name("onshow")]
        public static Action<Event> OnShow;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the sort event is raised
        /// </summary>
        [Bridge.CLR.Name("onsort")]
        public static Action<Event> OnSort;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the stalled event is raised
        /// </summary>
        [Bridge.CLR.Name("onstalled")]
        public static Action<Event> OnStalled;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the submit event is raised
        /// </summary>
        [Bridge.CLR.Name("onsubmit")]
        public static Action<Event> OnSubmit;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the suspend event is raised
        /// </summary>
        [Bridge.CLR.Name("onsuspend")]
        public static Action<Event> OnSuspend;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the timeupdate event is raised
        /// </summary>
        [Bridge.CLR.Name("ontimeupdate")]
        public static Action<Event> OnTimeUpdate;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the volumechange event is raised
        /// </summary>
        [Bridge.CLR.Name("onvolumechange")]
        public static Action<Event> OnVolumeChange;

        /// <summary>
        /// Is an EventHandler representing the code to be called when the waiting event is raised
        /// </summary>
        [Bridge.CLR.Name("onwaiting")]
        public static Action<Event> OnWaiting;

        /// <summary>
        /// Returns the event handling code for the touchstart event.
        /// </summary>
        [Bridge.CLR.Name("onTouchStart")]
        public static Action<Event> OnTouchStart;

        /// <summary>
        /// Returns the event handling code for the touchend event.
        /// </summary>
        [Bridge.CLR.Name("onTouchEnd")]
        public static Action<Event> OnTouchEnd;

        /// <summary>
        /// Returns the event handling code for the touchmove event.
        /// </summary>
        [Bridge.CLR.Name("onTouchMove")]
        public static Action<Event> OnTouchMove;

        /// <summary>
        /// Returns the event handling code for the touchenter event.
        /// </summary>
        [Bridge.CLR.Name("onTouchEnter")]
        public static Action<Event> OnTouchEnter;

        /// <summary>
        /// Returns the event handling code for the touchleave event.
        /// </summary>
        [Bridge.CLR.Name("onTouchLeave")]
        public static Action<Event> OnTouchLeave;

        /// <summary>
        /// Returns the event handling code for the touchcancel event.
        /// </summary>
        [Bridge.CLR.Name("onTouchCancel")]
        public static Action<Event> OnTouchCancel;

        /// <summary>
        /// Removes keyboard focus from the currently focused element.
        /// </summary>
        public static void Blur()
        {
        }

        /// <summary>
        /// Sends a mouse click event to the element.
        /// </summary>
        public static void Click()
        {
        }

        /// <summary>
        /// Makes the element the current keyboard focus.
        /// </summary>
        public static void Focus()
        {
        }
    }

    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("HTMLDocument")]
    public class DocumentInstance: Element
    {
    }
}
    