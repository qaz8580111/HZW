using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public class historyModel
    {
        public decimal? USERID { get; set; }
        public string WFDNAME { get; set; }
        public string USERNAME { get; set; }
        public string TIME { get; set; }
        public string CONTENT { get; set; }
        public string WFSUID { get; set; }
      
        private List<string> _List_Path = new List<string>();
        public List<string> List_Path
        {
            get { return _List_Path; }
            set { _List_Path = value; }
        }
      
    }


    public class RWhistoryModel
    {
        public decimal? USERID { get; set; }
        public string WFDNAME { get; set; }
        public string USERNAME { get; set; }
        public string TIME { get; set; }
        public string CONTENT { get; set; }
        public string WFSUID { get; set; }
        public decimal? nextuserid { get; set; }
        private List<path> _List_Path = new List<path>();
        public List<path> List_Path
        {
            get { return _List_Path; }
            set { _List_Path = value; }
        }

    }

    public class path
    {
        public string l_path { get; set; }
        public string l_name { get; set; }
    }


}
