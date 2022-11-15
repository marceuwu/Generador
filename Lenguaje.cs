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
        public void Dispose()
        {
            cerrar();
        }
    }
}