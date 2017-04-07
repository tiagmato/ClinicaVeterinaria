using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    public class Animais
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Raca { get; set; }

        public string Especie { get; set; }

        public double Peso { get; set; }

        public int idade { get; set; }

        //defenir as chaves forasteiras associadas a esta classe

        [ForeignKey("DonoFK")]
        public Donos Dono { get; set; } //relacionar, no C#, o objeto Animal com o objeto Dono

        public int DonoFK { get; set; } //relaciona, no SQLServer, o ANIMAL com o DONO {FK}


    }
}