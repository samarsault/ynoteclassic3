namespace SS.Ynote.Classic.Core.Package
{
    /// <summary>
    /// A Package Metadata
    /// </summary>
    public class PackageMetadata
    {
        /// <summary>
        /// Name of the Package
        /// </summary>
        public string Name;
        /// <summary>
        /// Version of the Package
        /// </summary>
        public double Version;
        /// <summary>
        /// Description of the Package
        /// </summary>
        public string Description;
        /// <summary>
        /// Author of the Package
        /// </summary>
        public string Author;
    }

    public class LocalPackageMetadata : PackageMetadata
    {
        /// <summary>
        /// The Local Files
        /// </summary>
        public string[] Files;
    }
}
