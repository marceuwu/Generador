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
//                  Recorrer linea por linea el archivo .gram para extraer del nombre de cada produccion
//Requerimiento 6.- Agregar el parentesis izq y der escapados en la matriz de transicion 
//Requerimiento 7.- Implementar la cerradura epsilon ?
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        List<string> listaSNT;
        string sLenguaje;
        string sPrograma;
        string sPrimeraProduccion;
        public Lenguaje(string nombre) : base(nombre)
        {
            sLenguaje = "";
            sPrograma = "";
            sPrimeraProduccion = "";
            listaSNT = new List<string>();
        }
        public Lenguaje()
        {
            sLenguaje = "";
            sPrograma = "";
            sPrimeraProduccion = "";
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
            string[] lineas = System.IO.File.ReadAllLines("c2.gram");
            foreach (string linea in lineas)
            {
                string lineaSinEspacios = linea.Replace(" ", "");
                int index = lineaSinEspacios.IndexOf("-");
                if (index > 0)
                {
                    string snt = lineaSinEspacios.Substring(0, index);
                    if (!listaSNT.Contains(snt))
                        //Console.WriteLine(snt);
                        listaSNT.Add(snt);
                }
            }
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
                if(cadena[i] == '}')
                {
                    sNuevaCadena += sTabulador+ cadena[i];
                }
                else if(cadena[i] == '\n')
                {
                    if(cadena[i+1] == '}')
                    {
                        sNuevaCadena += cadena[i];
                    }
                    else
                    {
                        sNuevaCadena += cadena[i] + sTabulador;
                    }
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
            sPrograma = sPrograma +"\npublic class Program";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\nstatic void Main(string[] args)";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\ntry";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\nusing (Lenguaje a = new Lenguaje())";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\na." + produccionPrincipal + "();";
            sPrograma = sPrograma +"\n}";
            sPrograma = sPrograma +"\n}";
            sPrograma = sPrograma +"\ncatch (Exception e)";
            sPrograma = sPrograma +"\n{";
            sPrograma = sPrograma +"\nConsole.WriteLine(e.Message);";
            sPrograma = sPrograma +"\n}";
            sPrograma = sPrograma +"\n}";
            sPrograma = sPrograma +"\n}";
            sPrograma = sPrograma +"\n}";
        }
        public void gramatica()
        {
            cabecera();
            Programa("Programa");
            cabeceraLenguaje();
            listaDeProducciones(true);
            sLenguaje = sLenguaje +"\n}";
            sLenguaje = sLenguaje +"\n}";
            lenguaje.Write(tabular(sLenguaje));
            programa.Write(tabular(sPrograma));
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
        private void listaDeProducciones(bool primeraProduccion)
        {
            //Requerimiento 2 y 3                                 
            if(primeraProduccion)
            {
                sLenguaje = sLenguaje +"\npublic void " + getContenido()+ "()";
                primeraProduccion = false;
                sPrimeraProduccion = sLenguaje+"\n{}";
            }
            else
            {
               sLenguaje = sLenguaje +"\nprivate void " + getContenido() + "()"; 
            }
            sLenguaje = sLenguaje +"\n{";
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            sLenguaje = sLenguaje +"\n}";
            if (!FinArchivo())
            {
                //Console.WriteLine(getContenido());
                listaDeProducciones(primeraProduccion);
            }
        }
        private void simbolos()
        {
            if(getContenido() == "\\(")
            {
                match("\\(");
                if(esTipo(getContenido()))
                {
                    sLenguaje += "\nif (getClasificacion() == Tipos." + getContenido()+")";
                }
                else
                {
                    sLenguaje += "\nif (getContenido() == \"" + getContenido()+"\")";
                }
                sLenguaje += "\n{";
                simbolos();
                match("\\)");
                sLenguaje += "\n}";
            }
            else if (esTipo(getContenido()))
            {
                sLenguaje = sLenguaje +"\nmatch(Tipos." + getContenido() + ");";
                match(Tipos.ST);
            }
            else if (esSNT(getContenido()))
            {
                sLenguaje = sLenguaje +"\n" + getContenido() + "();";
                match(Tipos.ST);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                sLenguaje = sLenguaje +"\nmatch(\"" + getContenido() + "\");";
                match(Tipos.ST);
            }
            if (getClasificacion() != Tipos.FinProduccion && getContenido() != "\\)")
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