﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraThemeModel
    {
        [Key]
        public decimal ThemeId { get; set; }
        public string CameraName { get; set; }
        public decimal? ParentId { get; set; }
        public decimal? Isline { get; set; }
        public string Note { get; set; }
    }
}
