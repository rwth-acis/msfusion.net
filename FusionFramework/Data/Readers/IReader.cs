using FusionFramework.Data.Segmentators;

/// <summary>
/// Data reading from different sources
/// </summary>
namespace FusionFramework.Core.Data.Reader
{
    /// <summary>
    /// Delegate to be triggered when the reader finishes reading.
    /// </summary>
    /// <param name="Output">Results of the reading</param>
    public delegate void ReadFinished(dynamic Output);

    /// <summary>
    /// Abstract class that should be extended by every data reader
    /// </summary>
    public abstract class IReader : Transformable
    {
        /// <summary>
        /// Set or get reading finish event.
        /// </summary>
        public ReadFinished OnReadFinished;

        /// <summary>
        /// Gets or sets path to the reading source.
        /// </summary>
        protected string Path;

        /// <summary>
        /// Start reading
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stop reading
        /// </summary>
        public abstract void Stop();

    }
}
