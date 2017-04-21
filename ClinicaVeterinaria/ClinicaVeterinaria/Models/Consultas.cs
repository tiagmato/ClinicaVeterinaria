using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaVeterinaria.Models
{
    public class Consultas
    {
        [Key] // Força o atributo a ser chave primaria
        public int ConsultaID { get; set; }

        [Column(TypeName = "date")] //só regista 'datas', não 'horas'
        public DateTime DataConsulta { get; set; }


        [ForeignKey("Veterinario")]
        public int VeterinarioFK { get; set; }
        public virtual Veterinarios Veterinario { get; set; }


        [ForeignKey("Animal")]
        public int AnimalFK { get; set; }
        public virtual Animais Animal { get; set; }

    }
}