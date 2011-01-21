using System;
using System.Collections.Generic;
using System.Text;

namespace DelimitedFileViewer
{
    public enum FileStatus
    {
        FileDoesNotExist,
        IncorrectDelimiter,
        IncorrectEncoding,
        Valid,
    }
}
