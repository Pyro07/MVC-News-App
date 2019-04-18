using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCHaberV2.Models;

namespace MVCHaberV2.Models
{
    public class IndexModel
    {
        public List<Kategori> IMKategoriler { get; set; }
        public List<Haber> IMHaberler { get; set; }
        public List<Yorum> IMYorumlar { get; set; }
    }
}