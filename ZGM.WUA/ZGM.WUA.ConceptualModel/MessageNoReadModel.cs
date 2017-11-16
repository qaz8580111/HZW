using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class MessageNoReadModel
    {
        [Key]
        public decimal? ReceiverId { get; set; }
        public decimal? SendId { get; set; }
        public string SendName { get; set; }
        public decimal? Count { get; set; }
    }
}
