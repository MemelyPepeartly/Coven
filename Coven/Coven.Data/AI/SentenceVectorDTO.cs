using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.AI
{
    /// <summary>
    /// Class used for associating a sentence with a vector internally. Implimented namely in the API Pinecone Service.
    /// </summary>
    public class SentenceVectorDTO
    {
        public string Sentence { get; set; }
        public List<float> Vector { get; set; }
    }
}
