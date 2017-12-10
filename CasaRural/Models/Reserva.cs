using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CasaRural.Models;

namespace CasaRural.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }
        [Display(Name = "Data d'entrada")]
        public DateTime DataEntrada { get; set; }
        [Display(Name = "Data de sortida")]
        public DateTime? DataSortida { get; set; }


        [Display(Name = "Llogater")]
        public string llogaterId { get; set; }
        public Llogater llogater { get; set; }
    }
}