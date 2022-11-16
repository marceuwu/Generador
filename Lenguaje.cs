//DIAZ GUERRERO MARCELA
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Text;
using System.Text.RegularExpressions;

namespace Generador
{
    public class Lenguaje : Sintaxis, IDisposable
    {
        public Lenguaje()
        {
        }
        public Lenguaje(string nombre):base(nombre)
        { 
        }
        public void Dispose()
        {
            cerrar();
        }
        public void Gramatica()
        {
            Cabecera();
            ListaProducciones();
        }
        private void Cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.SNT);
            match(Tipos.FinProduccion);
        }
        private void ListaProducciones()
        {
            match(Tipos.SNT);
            match(Tipos.Produce);
            match(Tipos.FinProduccion);
            if(!FinArchivo())
            {
                ListaProducciones();
            }
        }
    }
}