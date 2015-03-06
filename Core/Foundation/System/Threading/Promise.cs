using Bridge.Foundation;
namespace System.Threading.Tasks 
{
	/// <summary>
	/// CommonJS Promise/A interface
    /// http://wiki.commonjs.org/wiki/Promises/A
	/// </summary>
	[Ignore]
	public interface IPromise 
    {
		/// <summary>
        /// Adds a fulfilledHandler to be called for completion of a promise.
		/// </summary>
        /// <param name="fulfilledHandler">The fulfilledHandler is called when the promise is fulfilled</param>
        void Then(Delegate fulfilledHandler);

        /// <summary>
        /// Adds a fulfilledHandler, errorHandler to be called for completion of a promise.
        /// </summary>
        /// <param name="fulfilledHandler">The fulfilledHandler is called when the promise is fulfilled</param>
        /// <param name="errorHandler">The errorHandler is called when a promise fails.</param>
		void Then(Delegate fulfilledHandler, Delegate errorHandler);
	}
}
