using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WcfServiceLibraryGame
{
    [Serializable()]
    [XmlRoot("Parametros", IsNullable = false)]
    public class Parametros
    {
        public String StringConnection { get; set; }
    }

    public class LoadParametros
    {
        public LoadParametros()
        {
            Load();
        }

        public Parametros parametros = new Parametros();

        public void Load()
        {
            String sArquivo = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) +@"\Parametros.xml";

            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(Parametros));

            try
            {                 
                using (StreamReader oStreamReader = new StreamReader(sArquivo))
                {
                    parametros = (Parametros)oXmlSerializer.Deserialize(oStreamReader);

                }

            }
            catch (Exception e1)
            {
                Parametros parametros = new Parametros();
            }           

        }
    }
}
