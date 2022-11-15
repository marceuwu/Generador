//DIAZ GUERRERO MARCELA
using System.IO;
using System.Runtime.CompilerServices;
//Sacar tokens
namespace Generador
{
    public class Lexico : Token
    {
        protected StreamReader archivo;
        protected StreamWriter log;
        const int F = -1;
        const int E = -2;
        protected int linea;
        protected int contCaracteres = 0;
        int[,] TRAND = new int[,]
        {
            
        };

        public Lexico()
        {
            linea = 1;
            string path = "C:\\Mis archivos\\Quinto semestre\\LyA\\Generador\\c.gram";
            bool existencia = File.Exists(path);
            log = new StreamWriter("C:\\Mis archivos\\Quinto semestre\\LyA\\Semantica\\prueba.Log"); 
            log.AutoFlush = true;

            log.WriteLine("Archivo: c.gram");
            log.WriteLine(DateTime.Now);

            if (existencia == true)
            {
                archivo = new StreamReader(path);
            }
            else
            {
                throw new Error("Error: El archivo c.gram no existe", log);
            }
        }
        public Lexico(string nombre)
        {
            linea = 1;

            string pathLog = Path.ChangeExtension(nombre, ".log");
            log = new StreamWriter(pathLog); 
            log.AutoFlush = true;
            log.WriteLine("Archivo: "+nombre);
            log.WriteLine("Fecha: " + DateTime.Now);

            if (File.Exists(nombre))
            {
                archivo = new StreamReader(nombre);
            }
            else
            {
                throw new Error("Error: El archivo " +Path.GetFileName(nombre)+ " no existe ", log);
            }
        }
        public int getContCaracter()
        {
            return contCaracteres;
        }
        public void setContCaracter(int cont)
        {
            contCaracteres = cont;
        }
        public void cerrar()
        {
            archivo.Close();
            log.WriteLine("Fin de compilacion");
            Console.WriteLine("\n\nFin de compilacion ");
            log.Close();
        }       

        private void clasifica(int estado)
        {
        }
        private int columna(char c)
        {
            return 0;
        }
        public string NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;
            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = TRAND[estado,columna(c)];
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    contCaracteres++;
                    if(c == '\n')
                    {
                        linea++;
                    }
                    if (estado >0)
                    {
                        buffer += c;
                    }
                    else
                    {
                        buffer = "";
                    }
                }
            }
            setContenido(buffer); 
            if(estado == E)
            {
                throw new Error("Error lexico: No definido en linea: "+linea, log); 
            }
            return getContenido();
        }

        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}