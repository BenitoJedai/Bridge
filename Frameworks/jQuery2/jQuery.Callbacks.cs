using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections.Generic;

namespace Bridge.jQuery2
{    
    public partial class jQuery
    {
        /// <summary>
        /// A multi-purpose callbacks list object that provides a powerful way to manage callback lists.
        /// </summary>
        /// <returns></returns>
        public static Callbacks Callbacks()
        {
            return null;
        }

        /// <summary>
        /// A multi-purpose callbacks list object that provides a powerful way to manage callback lists.
        /// </summary>
        /// <param name="flags">An optional list of space-separated flags that change how the callback list behaves. Possible flags: once: Ensures the callback list can only be fired once (like a Deferred). memory: Keeps track of previous values and will call any callback added after the list has been fired right away with the latest "memorized" values (like a Deferred). unique: Ensures a callback can only be added once (so there are no duplicates in the list). stopOnFalse: Interrupts callings when a callback returns false.</param>
        /// <returns></returns>
        public static Callbacks Callbacks(string flags)
        {
            return null;
        }
    }

    /// <summary>
    /// The jQuery.Callbacks() function, introduced in version 1.7, returns a multi-purpose object that provides a powerful way to manage callback lists. It supports adding, removing, firing, and disabling callbacks.
    /// </summary>
    [Ignore]
    [Name("Object")]
    public class Callbacks
    {
        private Callbacks ()
	    {
	    }

        /// <summary>
        /// Add a callback or a collection of callbacks to a callback list.
        /// </summary>
        /// <param name="callback">A function, or array of functions, that are to be added to the callback list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Add(Delegate callback)
        {
            return null;
        }

        /// <summary>
        /// Add a callback or a collection of callbacks to a callback list.
        /// </summary>
        /// <param name="callbacks">A function, or array of functions, that are to be added to the callback list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Add(Delegate[] callbacks)
        {
            return null;
        }

        /// <summary>
        /// Disable a callback list from doing anything more.
        /// </summary>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Disable()
        {
            return null;
        }

        /// <summary>
        /// Determine if the callbacks list has been disabled.
        /// </summary>
        /// <returns></returns>
        public bool Disabled()
        {
            return false;
        }

        /// <summary>
        /// Remove all of the callbacks from a list.
        /// </summary>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Empty()
        {
            return null;
        }

        /// <summary>
        /// Call all of the callbacks with the given arguments
        /// </summary>
        /// <param name="arguments">The argument or list of arguments to pass back to the callback list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Fire(params object[] arguments)
        {
            return null;
        }

        /// <summary>
        /// Determine if the callbacks have already been called at least once.
        /// </summary>
        /// <returns></returns>
        public bool Fired()
        {
            return false;
        }

        /// <summary>
        /// Call all callbacks in a list with the given context and arguments.
        /// </summary>
        /// <param name="context">A reference to the context in which the callbacks in the list should be fired.</param>
        /// <returns></returns>
        public Callbacks FireWith(object context)
        {
            return null;
        }

        /// <summary>
        /// Call all callbacks in a list with the given context and arguments.
        /// </summary>
        /// <param name="context">A reference to the context in which the callbacks in the list should be fired.</param>
        /// <param name="arguments">An argument, or array of arguments, to pass to the callbacks in the list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks FireWith(object context, object[] arguments)
        {
            return null;
        }

        /// <summary>
        /// Determine whether a supplied callback is in a list
        /// </summary>
        /// <param name="callback">The callback to search for.</param>
        /// <returns></returns>
        public bool Has(Delegate callback)
        {
            return false;
        }

        /// <summary>
        /// Lock a callback list in its current state.
        /// </summary>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Lock()
        {
            return null;
        }

        /// <summary>
        /// Determine if the callbacks list has been locked.
        /// </summary>
        /// <returns></returns>
        public bool Locked()
        {
            return false;
        }

        /// <summary>
        /// Remove a callback or a collection of callbacks from a callback list.
        /// </summary>
        /// <param name="callback">A function, or array of functions, that are to be removed from the callback list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Remove(Delegate callback)
        {
            return null;
        }

        /// <summary>
        /// Remove a callback or a collection of callbacks from a callback list.
        /// </summary>
        /// <param name="callback">A function, or array of functions, that are to be removed from the callback list.</param>
        /// <returns>This method returns the Callbacks object onto which it is attached (this).</returns>
        public Callbacks Remove(Delegate[] callbacks)
        {
            return null;
        }
    }
}
