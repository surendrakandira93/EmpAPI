using System;

namespace File.API.Data
{
    public partial class FileTable
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
