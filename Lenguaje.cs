//DIAZ GUERRERO MARCELA
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Text;
using System.Text.RegularExpressions;
//Requerimiento 1.- Construir un metodo para escribir en el archivo Lenguaje.cs indentando el condigo
//                  "{" incrementa un tabulador, "}"decrementa un tabulador
//Requerimiento 2.- Declarar un atributo primerProduccion de tipo de String y actualizarlo con la primera produccion de la gramatica 
//Requerimiento 3.- La primera podrucción es publica y el resto privada 
//Requerimiento 4.- El constructor lexico parametrizado debe validar que la extension del archivo a compilar sea .gen 
//                  si no es .gen debe llamar una excepcion
//Requerimiento 5.- Resolver la ambiguedad de ST y SNT
//                  Recorrer linea por linea el archivo .gram para extraer
//Requerimiento 6.- Agregar el parentesis izq y der escapados en la matriz de ttransiciones 
//Requerimiento 7.- Implementar el or y la cerradura epsilon | ?
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        List<string> listaSNT;
        string sLenguaje;
        string sPrograma;
        public Lenguaje(string nombre) : base(nombre)
        {
            sLenguaje = "";
            sPrograma = "";
            listaSNT = new List<string>();
        }
        public Lenguaje()
        {
            sLenguaje = "";
            sPrograma = "";
            listaSNT = new List<string>();
        }
        public void Dispose()
        {
            cerrar();
        }
        private bool esSNT(string contenido)
        {
            return listaSNT.Contains(contenido);
            //return true;
        }
        private void agregaSNT(string contenido)
        {
            //Requerimiento 5
            listaSNT.Add(contenido);
        }
        public string tabular(string cadena)
        {
            string sNuevaCadena = "";
            string sTabulador = "";
            int contTab = 0;
            for(int i = 0; i < cadena.Length; i++)
            {
                sTabulador = "";
                if (cadena[i] == '{')
                {
                    contTab++;
                }
                else if (cadena[i] == '}')
                {
                    contTab--;
                }                  
                for (int j = 0; j < contTab; j++)
                {
                    sTabulador += "\t";
                }
                if(cadena[i] == '\n')
                {
                    sNuevaCadena += cadena[i] + sTabulador;
                }
                else if(cadena[i] == '}' ||cadena[i] == '{' || cadena[i] == ';')
                {
                    sNuevaCadena += cadena[i] + "\n" + sTabulador;
                }
                else
                {
                    sNuevaCadena += cadena[i];
                }
            }
            return sNuevaCadena;
        }
        private void Programa(string produccionPrincipal)
        {
            agregaSNT("Programa");
            agregaSNT("Librerias");
            agregaSNT("Variables");
            agregaSNT("ListaIdentificadores");
            sPrograma = sPrograma +"\nusing System;";
            sPrograma = sPrograma +"\nnamespace Generico";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\n    public class Program";
            sPrograma = sPrograma +"\n    {";
            sPrograma = sPrograma +"\n        static void Main(string[] args)";
            sPrograma = sPrograma +"\n        {";
            sPrograma = sPrograma +"\n            try";
            sPrograma = sPrograma +"\n            {";
            sPrograma = sPrograma +"\n                using (Lenguaje a = new Lenguaje())";
            sPrograma = sPrograma +"\n                {";
            sPrograma = sPrograma +"\n                    a." + produccionPrincipal + "();";
            sPrograma = sPrograma +"\n                }";
            sPrograma = sPrograma +"\n            }";
            sPrograma = sPrograma +"\n            catch (Exception e)";
            sPrograma = sPrograma +"\n            {";
            sPrograma = sPrograma +"\n                Console.WriteLine(e.Message);";
            sPrograma = sPrograma +"\n            }";
            sPrograma = sPrograma +"\n        }";
            sPrograma = sPrograma +"\n    }";
            sPrograma = sPrograma +"\n}";
        }
        public void gramatica()
        {
            cabecera();
            Programa("Programa");
            cabeceraLenguaje();
            listaDeProducciones();
            sLenguaje = sLenguaje +"\n}";
            sLenguaje = sLenguaje +"\n}";
            lenguaje.Write(tabular(sLenguaje));
            programa.Write(sPrograma);
        }
        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.ST);
            match(Tipos.FinProduccion);
        }
        private void cabeceraLenguaje()
        {
            sLenguaje = sLenguaje +"\nusing System;";
            sLenguaje = sLenguaje +"\nusing System.Collections.Generic;";
            sLenguaje = sLenguaje +"\nusing System.Linq;";
            sLenguaje = sLenguaje +"\nusing System.Threading.Tasks;";
            sLenguaje = sLenguaje +"\n";
            sLenguaje = sLenguaje +"\nnamespace Generico";
            sLenguaje = sLenguaje +"\n{";
            sLenguaje = sLenguaje +"\npublic class Lenguaje : Sintaxis, IDisposable";
            sLenguaje = sLenguaje +"\n{";
            sLenguaje = sLenguaje +"\npublic Lenguaje(string nombre) : base(nombre)";
            sLenguaje = sLenguaje +"\n{";
            sLenguaje = sLenguaje +"\n}";
            sLenguaje = sLenguaje +"\npublic Lenguaje()";
            sLenguaje = sLenguaje +"\n{";
            sLenguaje = sLenguaje +"\n}";
            sLenguaje = sLenguaje +"\npublic void Dispose()";
            sLenguaje = sLenguaje +"\n{";
            sLenguaje = sLenguaje +"\ncerrar();";
            sLenguaje = sLenguaje +"\n}";
        }
        private void listaDeProducciones()
        {
            sLenguaje = sLenguaje +"private void " + getContenido() + "()";
            sLenguaje = sLenguaje +"{";
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            sLenguaje = sLenguaje +"}";
            if (!FinArchivo())
            {
                Console.WriteLine(getContenido());
                listaDeProducciones();
            }
        }
        private void simbolos()
        {
            if (esTipo(getContenido()))
            {
                sLenguaje = sLenguaje +"match(Tipos." + getContenido() + ");";
                match(Tipos.ST);
            }
            else if (esSNT(getContenido()))
            {
                sLenguaje = sLenguaje + getContenido() + "();";
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                sLenguaje = sLenguaje +"match(\"" + getContenido() + "\");";
                match(Tipos.ST);
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                simbolos();
            }
        }
        private bool esTipo(string clasificacion)
        {
            switch (clasificacion)
            {
                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacion":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                return true;
            }
            return false;
        }
    }
}