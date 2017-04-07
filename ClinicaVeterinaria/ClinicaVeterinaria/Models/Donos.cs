using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    public class Donos{

        //definir um Construtor, para carregar o atributo ListaAnimais

        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }

        public int DonosID { get; set; }
        
        public string Nome { get; set; }

        public string NIF { get; set; }

        //indicar o relacionamento entre Donos e Animais
        //um Dono tem muitos Animais

        public virtual ICollection<Animais> ListaDeAnimais { get; set; }
    }
}