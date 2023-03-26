using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models

{
    public record Produto 
    {
        public required string Id { get; set; } = default!;

        public string Nome { get; set; } = default!;
        public string Codigo { get; set; } = default!;
        public string Fornecedor { get; set; } = default!;

    }
}
