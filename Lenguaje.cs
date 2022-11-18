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
namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        string nombreProyecto;
        public Lenguaje()
        {
            nombreProyecto = "";
        }
        public Lenguaje(string nombre):base(nombre)
        { 
            nombreProyecto = "";
        }
        public void Dispose()
        {
            cerrar();
        }
        private void Programa(string espacioProyecto,string produccionPrincipal)
        {
            programa.WriteLine("using System;");
            programa.WriteLine("using System.IO;");
            programa.WriteLine();
            programa.WriteLine("namespace "+espacioProyecto);
            programa.WriteLine("{");
            programa.WriteLine("\tpublic class Program");
            programa.WriteLine("\t{");
            programa.WriteLine("\t\tstatic void Main(string[] args)");
            programa.WriteLine("\t\t{");
            programa.WriteLine();
            programa.WriteLine("\t\t\tusing (Lenguaje a = new Lenguaje())");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\ttry");
            programa.WriteLine("\t\t\t\t{");
            programa.WriteLine("\t\t\t\ta."+produccionPrincipal.ToString()+"();");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t\tcatch (Exception e)");
            programa.WriteLine("\t\t\t{");
            programa.WriteLine("\t\t\t\tConsole.WriteLine(e.Message);");
            programa.WriteLine("\t\t\t}");
            programa.WriteLine("\t\t}");
            programa.WriteLine("\t\tConsole.ReadLine();");
            programa.WriteLine("\t}");
            programa.WriteLine("}");
            programa.WriteLine("}");
        }


        public void Gramatica()
        {
            Cabecera();
            Programa(nombreProyecto,"programa");
            CabeceraLenguaje(nombreProyecto);
            ListaProducciones();
            programa.WriteLine("\t}");
            programa.WriteLine("}");
        }
        private void Cabecera()
        {
            match("Gramatica");
            match(":");
            nombreProyecto = getContenido();
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }
        private void CabeceraLenguaje(string espacioProyecto)
        {
            lenguaje.WriteLine("using System;");
            lenguaje.WriteLine("using System.Collections.Generic;");
            lenguaje.WriteLine("using System.ComponentModel.DataAnnotations;");
            lenguaje.WriteLine("using System.Diagnostics;");
            lenguaje.WriteLine("using System.Diagnostics.SymbolStore;");
            lenguaje.WriteLine("using System.Text;");
            lenguaje.WriteLine("using System.Text.RegularExpressions;");
            lenguaje.WriteLine("namespace "+espacioProyecto);
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("public class Lenguaje : Sintaxis, IDisposable");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("string nombreProyecto;");
            lenguaje.WriteLine("public Lenguaje()");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("nombreProyecto = \"\";");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("public Lenguaje(string nombre):base(nombre))");
            lenguaje.WriteLine("{ ");
            lenguaje.WriteLine("nombreProyecto = \"\";)");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("public void Dispose()");
            lenguaje.WriteLine("        {");
            lenguaje.WriteLine("            cerrar();");
            lenguaje.WriteLine("        }");
            lenguaje.WriteLine("        private void Programa(string espacioProyecto,string produccionPrincipal)");
            lenguaje.WriteLine("        {");
            lenguaje.WriteLine("programa.WriteLine(\"using System;\");");
            lenguaje.WriteLine("programa.WriteLine(\"using System.IO;\");");
            lenguaje.WriteLine("programa.WriteLine();");
            lenguaje.WriteLine("           programa.WriteLine(\"namespace \"+espacioProyecto);");
            lenguaje.WriteLine("programa.WriteLine(\"{\");");
            lenguaje.WriteLine("programa.WriteLine(\"\tpublic class Program\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t{\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\tstatic void Main(string[] args)\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t{\");");
            lenguaje.WriteLine("programa.WriteLine();");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\tusing (Lenguaje a = new Lenguaje())\");");
            programa.WriteLine("\t\t\t{");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t\ttry\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t\t{\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t\ta.\"+produccionPrincipal.ToString()+\"();\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t}\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\tcatch (Exception e)\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t{\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t\tConsole.WriteLine(e.Message);\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t\t}\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\t}\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t\tConsole.ReadLine();\");");
            lenguaje.WriteLine("programa.WriteLine(\"\t}\");");
            lenguaje.WriteLine("programa.WriteLine(\"}\");");
            lenguaje.WriteLine("programa.WriteLine(\"}\");");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("public void Gramatica()");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("Cabecera();");
            lenguaje.WriteLine("Programa(nombreProyecto,\"programa\");");
            lenguaje.WriteLine("ListaProducciones();");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("private void Cabecera()");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("match(\"Gramatica\");");
            lenguaje.WriteLine("match(\":\");");
            lenguaje.WriteLine("nombreProyecto = getContenido();");
            lenguaje.WriteLine("match(Tipos.SNT);");
            lenguaje.WriteLine("match(Tipos.FinProduccion);");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("private void ListaProducciones()");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("match(Tipos.SNT);");
            lenguaje.WriteLine("match(Tipos.Produce);");
            lenguaje.WriteLine("match(Tipos.FinProduccion);");
            lenguaje.WriteLine("if(!FinArchivo())");
            lenguaje.WriteLine("{");
            lenguaje.WriteLine("ListaProducciones();");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("}");
        }
        private void ListaProducciones()
        {
            lenguaje.WriteLine("private void "+getContenido()+"()");
            lenguaje.WriteLine("{");       
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            lenguaje.WriteLine("}");  
            if(!FinArchivo())
            {
                ListaProducciones();
            }
        }
    }
}