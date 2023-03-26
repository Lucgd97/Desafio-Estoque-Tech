using Models;
using ProdutoEstoque.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoEstoque.Servicos
{
    public class EstoqueServico
    {
        public IPersistencia<Estoque> Persistencia;

        public EstoqueServico(IPersistencia<Estoque> persistencia)
        {
            this.Persistencia = persistencia;
        }

        public async Task<List<Estoque>> ExtratoProduto(string idProduto)
        {
            var estoqueProduto = (await this.Persistencia.Todos()).FindAll(ep => ep.IdProduto == idProduto);
            if (estoqueProduto.Count == 0) return new List<Estoque>();

            return estoqueProduto;
        }

        public async Task<int> SaldoProduto(string idProduto, List<Estoque>? estoqueProduto = null)
        {
            if (estoqueProduto == null)
                estoqueProduto = await ExtratoProduto(idProduto);

            if (estoqueProduto.Count == 0 ) return 0;

            return Convert.ToInt32(estoqueProduto.Sum(ep => ep.Quantidade));
        }
    }
}
