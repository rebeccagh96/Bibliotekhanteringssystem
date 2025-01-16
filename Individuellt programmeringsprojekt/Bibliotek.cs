using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individuellt_programmeringsprojekt
{
    //Här har jag skapat klassen Biliotek för bibliotekets böcker.
    public class Bibliotek
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Författare { get; set; }
        public string Isbn { get; set; }
        public bool Tillgänglig {  get; set; }    
    }
}
