using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClinicaVeterinaria.Models
{
    public class Donos{

        // criar o construtor desta classe
        // e carregar a lista de Animais
        public Donos()
        {
            ListaDeAnimais = new HashSet<Animais>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]// o PK não será AutoNumber
        public int DonoID { get; set; }

        [Required(ErrorMessage ="o {0} é de preenchimento obrigatório")]
        [Display(Name ="Nome do Dono do Animal")]
        [RegularExpression("[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+((-| )((de|da|do|dos) )?[A-ZÍÂÓ][a-záéíóúàèìòùâêîôûãõäëïöüç']+)*",ErrorMessage ="No {0} só são aceites letras. Cada nome deve de começar com letra maiúscula." )]
        public string Nome { set; get; }

        [Required]
        [RegularExpression("[0-9]{9}",ErrorMessage ="Escreva apenas 9 caracteres numéricos")]
        public string NIF { get; set; }

        //********************************************************************************************
        //     criar um atributo para ligar este atributo à BD de autentificação
        //********************************************************************************************

        public string UserName { get; set; } //corresponde ao Login
        //********************************************************************************************
        
        // especificar que um DONO tem muitos ANIMAIS
        public ICollection<Animais> ListaDeAnimais { get; set; }


}
}

 