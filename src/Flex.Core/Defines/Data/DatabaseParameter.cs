using System.Data;

namespace Flex.Core.Defines.Data
{
    public class DatabaseParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
        public int? Size { get; set; }
        public ParameterDirection Direction { get; set; }
    }
}