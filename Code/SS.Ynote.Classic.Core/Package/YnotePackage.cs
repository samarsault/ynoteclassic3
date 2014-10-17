using System.IO;

namespace SS.Ynote.Classic.Core.Package
{
    /// <summary>
    /// A Ynote Package
    /// </summary>
    public class YnotePackage
    {
        private string dataDir;
        private string packageFile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="package">The Package Zip/Package Directory</param>
        public YnotePackage(string package)
        {
            this.packageFile = package;
        }
        /// <summary>
        /// Install the Package
        /// </summary>
        public void Install()
        {
            using (var zip = ZipStorer.Open(packageFile, FileAccess.Read))
            {
                var entries = zip.ReadCentralDir();
                string packageFileName = Path.GetFileNameWithoutExtension(packageFile);
                foreach (var entry in entries)
                {
                    zip.ExtractFile(entry, Path.Combine(dataDir, packageFileName));
                }
            }
        }

        /// <summary>
        /// Uninstall the Package
        /// </summary>
        public void Uninstall(LocalPackageMetadata metadata)
        {
            foreach (string file in metadata.Files)
            {
                File.Delete(file);
            }
        }
    }
}
