using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public record Estoque
    {
        public required string Id { get; set; }
        public required string IdCliente { get; set; }
        public double Quantidade { get; set; } = default!;
        public DateTime Data { get; set; } = default!;
    }
}
