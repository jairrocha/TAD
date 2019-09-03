using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TAD_Treinamento_a_distancia.Models
{
    public class Pessoa
    {
        [Required(ErrorMessage="O campo é obrigatório")]
        public string Nome { get; set; }

        public Usuario Usuario { get; set; }

    }
}