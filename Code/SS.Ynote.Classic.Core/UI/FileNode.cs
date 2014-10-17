using System.IO;
using System.Collections.Generic;

namespace SS.Ynote.Classic.Core.UI
{
        public class Node : IEnumerable<Node>
        {
            public string Title { get; set; }
            public List<Node> Childs { get; private set; }

            public Node(string title = null)
            {
                Title = title;
                Childs = new List<Node>();
            }

            public IEnumerator<Node> GetEnumerator()
            {
                return Childs.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return Childs.GetEnumerator();
            }

            public override string ToString()
            {
                return Title;
            }
        }

    /// <summary>
    /// A File Node
    /// From : FastTree.Tester
    /// </summary>
    public class FileNode : IEnumerable<FileNode>
    {
        public string Path { get; private set; }
        public bool IsDir { get; private set; }

        public FileNode(string path, bool isDir)
        {
            this.Path = path;
            this.IsDir = isDir;
        }

        public string Name
        {
            get
            {
                var name = System.IO.Path.GetFileName(Path);
                if (string.IsNullOrEmpty(name))
                    return Path;
                return name;
            }
        }

        public bool HasChildren
        {
            get { return true; }
        }

        public IEnumerator<FileNode> GetEnumerator()
        {
            if (!IsDir)
                yield break;

            string[] dirs, files;
            try
            {
                dirs = Directory.GetDirectories(Path);
                files = Directory.GetFiles(Path);
            }
            catch
            {
                yield break;//UnauthorizedAccessException
            }

            foreach (var path in dirs)
                yield return new FileNode(path, true);

            foreach (var path in files)
                yield return new FileNode(path, false);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Path.Equals((obj as FileNode).Path);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
