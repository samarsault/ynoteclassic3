using SS.Ynote.Classic.Core.Plugins;

namespace SS.Ynote.Classic.Core.API
{
    /// <summary>
    /// The Base of the Ynote API
    /// </summary>
    public interface IAPI
    {
        /// <summary>
        /// Create the API Engine
        /// </summary>
        /// <param name="ynote"></param>
        void CreateEngine(IYnote ynote);
        /// <summary>
        /// Load All the Commands
        /// </summary>
        void LoadCommands();
        /// <summary>
        /// Run The Command
        /// </summary>
        /// <param name="method">The Method to run</param>
        /// <param name="arguments">Arguments</param>
        void CallMethod(string method, dynamic arguments=null);
    }
}
