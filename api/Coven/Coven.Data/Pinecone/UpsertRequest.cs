using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Pinecone
{
    public class UpsertRequest
    {
        public List<Vector> Vectors { get; set; }
        /// <summary>
        /// The name of the associated dataset
        /// </summary>
        public string Namespace { get; set; }
    }
    public class Vector
    {
        public string Id { get; set; }
        public List<float> Values { get; set; }
        public Metadata Metadata { get; set; }
    }
    public class Metadata
    {
        public string Genre { get; set; }
    }
}
