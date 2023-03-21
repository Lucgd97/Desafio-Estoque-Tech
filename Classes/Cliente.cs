using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Cliente : Base
    {
        public Cliente() { }

        public Cliente(string nome, string telefone, string cpf)
        {
            this.Nome= nome;
            this.Telefone= telefone;
            this.Cpf= cpf;
        }
    }
}
