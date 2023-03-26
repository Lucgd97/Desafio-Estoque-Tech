using Models;
using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoEstoque.Servicos
{
    public class ProdutoServico
    {
        public IPersistencia<Produto> Persistencia;

        public ProdutoServico(IPersistencia<Produto> persistencia)
        {
            this.Persistencia = persistencia;
        }
    }
}
