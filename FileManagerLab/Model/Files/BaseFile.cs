using System.IO;

namespace FileManagerLab.Model;

public abstract class BaseFile
{
    protected FileInfo Path;

    public abstract void OpenFile();
}