using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceLibraryGame
{
    [DataContract]
    public class Player
    {      

        [DataMember]
        public long playerId { get; set; }
             
        
        [DataMember]
        public long balance { get; set; }

        [DataMember]
        public DateTime lastUpdateDate { get; set; }
    }
}
