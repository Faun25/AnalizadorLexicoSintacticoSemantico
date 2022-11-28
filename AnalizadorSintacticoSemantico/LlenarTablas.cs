using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintacticoSemantico
{



    class LlenarTablas
    {
        #region Variables
        DataTable dtABC = new DataTable();
        DataTable dtNumeros = new DataTable();
        DataTable dtSignosPuntuacion = new DataTable();
        DataTable dtComentarios = new DataTable();
        DataTable dtOperadoresAsigRel = new DataTable();
        DataTable dtOperadoresAritmetico = new DataTable();
        DataTable dtOperadoresLogico = new DataTable();
        DataTable dtCadena = new DataTable();
        DataTable dtDelimitadores = new DataTable();
        DataTable dtReservadas = new DataTable();
        DataTable dtPosicion = new DataTable();

        DataTable dtResultado = new DataTable();
        #endregion

        public DataTable ABC()
        {
            if (dtABC.Columns.Count == 0)
            {
                dtABC.Columns.Add("Componente", typeof(System.String));
                dtABC.Columns.Add("Token", typeof(System.String));
                dtABC.Columns.Add("Posicion", typeof(System.String));
            }

            if (dtABC.Rows.Count == 0)
            {
                dtABC.Rows.Add("a", "Identificador", "0");
                dtABC.Rows.Add("b", "Identificador", "1");
                dtABC.Rows.Add("c", "Identificador", "2");
                dtABC.Rows.Add("d", "Identificador", "3");
                dtABC.Rows.Add("e", "Identificador", "4");
                dtABC.Rows.Add("f", "Identificador", "5");
                dtABC.Rows.Add("g", "Identificador", "6");
                dtABC.Rows.Add("h", "Identificador", "7");
                dtABC.Rows.Add("i", "Identificador", "8");
                dtABC.Rows.Add("j", "Identificador", "9");
                dtABC.Rows.Add("k", "Identificador", "10");
                dtABC.Rows.Add("l", "Identificador", "11");
                dtABC.Rows.Add("m", "Identificador", "12");
                dtABC.Rows.Add("n", "Identificador", "13");
                dtABC.Rows.Add("ñ", "Identificador", "14");
                dtABC.Rows.Add("o", "Identificador", "15");
                dtABC.Rows.Add("p", "Identificador", "16");
                dtABC.Rows.Add("q", "Identificador", "17");
                dtABC.Rows.Add("r", "Identificador", "18");
                dtABC.Rows.Add("s", "Identificador", "19");
                dtABC.Rows.Add("t", "Identificador", "20");
                dtABC.Rows.Add("u", "Identificador", "21");
                dtABC.Rows.Add("v", "Identificador", "22");
                dtABC.Rows.Add("w", "Identificador", "23");
                dtABC.Rows.Add("x", "Identificador", "24");
                dtABC.Rows.Add("y", "Identificador", "25");
                dtABC.Rows.Add("z", "Identificador", "26");

                dtABC.Rows.Add("A", "Identificador", "27");
                dtABC.Rows.Add("B", "Identificador", "28");
                dtABC.Rows.Add("C", "Identificador", "29");
                dtABC.Rows.Add("D", "Identificador", "30");
                dtABC.Rows.Add("E", "Identificador", "31");
                dtABC.Rows.Add("F", "Identificador", "32");
                dtABC.Rows.Add("G", "Identificador", "33");
                dtABC.Rows.Add("H", "Identificador", "34");
                dtABC.Rows.Add("I", "Identificador", "35");
                dtABC.Rows.Add("J", "Identificador", "36");
                dtABC.Rows.Add("K", "Identificador", "37");
                dtABC.Rows.Add("L", "Identificador", "38");
                dtABC.Rows.Add("M", "Identificador", "39");
                dtABC.Rows.Add("N", "Identificador", "40");
                dtABC.Rows.Add("Ñ", "Identificador", "41");
                dtABC.Rows.Add("O", "Identificador", "42");
                dtABC.Rows.Add("P", "Identificador", "43");
                dtABC.Rows.Add("Q", "Identificador", "44");
                dtABC.Rows.Add("R", "Identificador", "45");
                dtABC.Rows.Add("S", "Identificador", "46");
                dtABC.Rows.Add("T", "Identificador", "47");
                dtABC.Rows.Add("U", "Identificador", "48");
                dtABC.Rows.Add("V", "Identificador", "49");
                dtABC.Rows.Add("W", "Identificador", "50");
                dtABC.Rows.Add("X", "Identificador", "51");
                dtABC.Rows.Add("Y", "Identificador", "52");
                dtABC.Rows.Add("Z", "Identificador", "53");

                dtABC.Rows.Add("_", "IdentificadorG", "54");
                dtABC.Rows.Add("-", "IdentificadorG", "55");

                dtABC.Rows.Add("0", "Identificador", "56");
                dtABC.Rows.Add("1", "Identificador", "57");
                dtABC.Rows.Add("2", "Identificador", "58");
                dtABC.Rows.Add("3", "Identificador", "59");
                dtABC.Rows.Add("4", "Identificador", "60");
                dtABC.Rows.Add("5", "Identificador", "61");
                dtABC.Rows.Add("6", "Identificador", "62");
                dtABC.Rows.Add("7", "Identificador", "63");
                dtABC.Rows.Add("8", "Identificador", "64");
                dtABC.Rows.Add("9", "Identificador", "65");

                dtABC.Rows.Add(".", "Identificador", "66");
                dtABC.Rows.Add(",", "Identificador", "67");
            }
            return dtABC;
        }

        public DataTable Numeros()
        {
            if (dtNumeros.Columns.Count == 0)
            {
                dtNumeros.Columns.Add("Componente", typeof(System.String));
                dtNumeros.Columns.Add("Token", typeof(System.String));
            }

            if (dtNumeros.Rows.Count == 0)
            {
                dtNumeros.Rows.Add("1", "Literal Numérica");
                dtNumeros.Rows.Add("2", "Literal Numérica");
                dtNumeros.Rows.Add("3", "Literal Numérica");
                dtNumeros.Rows.Add("4", "Literal Numérica");
                dtNumeros.Rows.Add("5", "Literal Numérica");
                dtNumeros.Rows.Add("6", "Literal Numérica");
                dtNumeros.Rows.Add("7", "Literal Numérica");
                dtNumeros.Rows.Add("8", "Literal Numérica");
                dtNumeros.Rows.Add("9", "Literal Numérica");
                dtNumeros.Rows.Add("0", "Literal Numérica");
            }
            return dtNumeros;
        }

        public DataTable SignosPuntuacion()
        {
            if (dtSignosPuntuacion.Columns.Count == 0)
            {
                dtSignosPuntuacion.Columns.Add("Componente", typeof(System.String));
                dtSignosPuntuacion.Columns.Add("Token", typeof(System.String));
            }

            if (dtSignosPuntuacion.Rows.Count == 0)
            {
                dtSignosPuntuacion.Rows.Add(".", "Puntuacion");
            }
            return dtSignosPuntuacion;
        }

        public DataTable Comentarios()
        {
            if (dtComentarios.Columns.Count == 0)
            {
                dtComentarios.Columns.Add("Componente", typeof(System.String));
                dtComentarios.Columns.Add("Token", typeof(System.String));
            }

            if (dtComentarios.Rows.Count == 0)
            {
                dtComentarios.Rows.Add("{", "Literal Numérica");
                dtComentarios.Rows.Add("}", "Literal Numérica");
            }
            return dtComentarios;
        }

        public DataTable OperadoresAsignacionRelacionales()
        {
            if (dtOperadoresAsigRel.Columns.Count == 0)
            {
                dtOperadoresAsigRel.Columns.Add("Componente", typeof(System.String));
                dtOperadoresAsigRel.Columns.Add("Token", typeof(System.String));
            }

            if (dtOperadoresAsigRel.Rows.Count == 0)
            {
                dtOperadoresAsigRel.Rows.Add("<", "Operador Relacional");
                dtOperadoresAsigRel.Rows.Add("=", "Operador Relacional");
                dtOperadoresAsigRel.Rows.Add(">", "Operador Relacional");
            }
            return dtOperadoresAsigRel;
        }

        public DataTable OperadoresLogicos()
        {
            if (dtOperadoresLogico.Columns.Count == 0)
            {
                dtOperadoresLogico.Columns.Add("Componente", typeof(System.String));
                dtOperadoresLogico.Columns.Add("Token", typeof(System.String));
            }

            if (dtOperadoresLogico.Rows.Count == 0)
            {
                dtOperadoresLogico.Rows.Add("or", "Operador Lógico");
                dtOperadoresLogico.Rows.Add("and", "Operador Lógico");
                dtOperadoresLogico.Rows.Add("not", "Operador Lógico");
            }
            return dtOperadoresLogico;
        }

        public DataTable OperadoresAritmeticos()
        {
            if (dtOperadoresAritmetico.Columns.Count == 0)
            {
                dtOperadoresAritmetico.Columns.Add("Componente", typeof(System.String));
                dtOperadoresAritmetico.Columns.Add("Token", typeof(System.String));
            }

            if (dtOperadoresAritmetico.Rows.Count == 0)
            {
                dtOperadoresAritmetico.Rows.Add("+", "Operador Aritmético");
                dtOperadoresAritmetico.Rows.Add("-", "Operador Aritmético");
                dtOperadoresAritmetico.Rows.Add("*", "Operador Aritmético");
                dtOperadoresAritmetico.Rows.Add("/", "Operador Aritmético");
            }
            return dtOperadoresAritmetico;
        }

        public DataTable Cadenas()
        {
            if (dtCadena.Columns.Count == 0)
            {
                dtCadena.Columns.Add("Componente", typeof(System.String));
                dtCadena.Columns.Add("Token", typeof(System.String));
            }

            if (dtCadena.Rows.Count == 0)
            {
                dtCadena.Rows.Add('"', "Cadena");
            }
            return dtCadena;
        }

        public DataTable Delimitadores()
        {
            if (dtDelimitadores.Columns.Count == 0)
            {
                dtDelimitadores.Columns.Add("Componente", typeof(System.String));
                dtDelimitadores.Columns.Add("Token", typeof(System.String));
            }

            if (dtDelimitadores.Rows.Count == 0)
            {
                dtDelimitadores.Rows.Add("(", "Delimitadores");
                dtDelimitadores.Rows.Add(")", "Delimitadores");
                dtDelimitadores.Rows.Add("[", "Delimitadores");
                dtDelimitadores.Rows.Add("]", "Delimitadores");
                dtDelimitadores.Rows.Add(";", "Delimitadores");
                dtDelimitadores.Rows.Add(":", "Delimitadores");
                dtDelimitadores.Rows.Add(",", "Delimitadores");
            }
            return dtDelimitadores;
        }

        public DataTable Reservadas()
        {
            if (dtReservadas.Columns.Count == 0)
            {
                dtReservadas.Columns.Add("Componente", typeof(System.String));
                dtReservadas.Columns.Add("Token", typeof(System.String));
            }

            if (dtReservadas.Rows.Count == 0)
            {
                dtReservadas.Rows.Add("Array", "Palabras Reservadas");
                dtReservadas.Rows.Add("Begin", "Palabras Reservadas");
                dtReservadas.Rows.Add("Case", "Palabras Reservadas");
                dtReservadas.Rows.Add("Const", "Palabras Reservadas");
                dtReservadas.Rows.Add("Do", "Palabras Reservadas");
                dtReservadas.Rows.Add("Else", "Palabras Reservadas");
                dtReservadas.Rows.Add("WriteIn", "Palabras Reservadas");
                dtReservadas.Rows.Add("ReadIn", "Palabras Reservadas");
                dtReservadas.Rows.Add("ElseIf", "Palabras Reservadas");
                dtReservadas.Rows.Add("End", "Palabras Reservadas");
                dtReservadas.Rows.Add("For", "Palabras Reservadas");
                dtReservadas.Rows.Add("If", "Palabras Reservadas");
                dtReservadas.Rows.Add("Loop", "Palabras Reservadas");
                dtReservadas.Rows.Add("Module", "Palabras Reservadas");
                dtReservadas.Rows.Add("Function", "Palabras Reservadas");
                dtReservadas.Rows.Add("Exit", "Palabras Reservadas");
                dtReservadas.Rows.Add("Not", "Palabras Reservadas");
                dtReservadas.Rows.Add("Of", "Palabras Reservadas");
                dtReservadas.Rows.Add("Mod", "Palabras Reservadas");
                dtReservadas.Rows.Add("Record", "Palabras Reservadas");
                dtReservadas.Rows.Add("Repeat", "Palabras Reservadas");
                dtReservadas.Rows.Add("Return", "Palabras Reservadas");
                dtReservadas.Rows.Add("Procedure", "Palabras Reservadas");
                dtReservadas.Rows.Add("By", "Palabras Reservadas");
                dtReservadas.Rows.Add("Then", "Palabras Reservadas");
                dtReservadas.Rows.Add("To", "Palabras Reservadas");
                dtReservadas.Rows.Add("Until", "Palabras Reservadas");
                dtReservadas.Rows.Add("Var", "Palabras Reservadas");
                dtReservadas.Rows.Add("While", "Palabras Reservadas");
                dtReservadas.Rows.Add("With", "Palabras Reservadas");
                dtReservadas.Rows.Add("True", "Palabras Reservadas");
                dtReservadas.Rows.Add("False", "Palabras Reservadas");
                dtReservadas.Rows.Add("Div", "Palabras Reservadas");
                dtReservadas.Rows.Add("Integer", "Palabras Reservadas");
                dtReservadas.Rows.Add("Int", "Palabras Reservadas");
                dtReservadas.Rows.Add("Real", "Palabras Reservadas");
                dtReservadas.Rows.Add("Char", "Palabras Reservadas");
                dtReservadas.Rows.Add("String", "Palabras Reservadas");
                dtReservadas.Rows.Add("Byte", "Palabras Reservadas");
                dtReservadas.Rows.Add("Boolean", "Palabras Reservadas");
            }
            return dtReservadas;
        }

        public DataTable Resultado()
        {
            if (dtResultado.Columns.Count == 0)
            {
                dtResultado.Columns.Add("Token", typeof(System.String));
                dtResultado.Columns.Add("Componente", typeof(System.String));
                dtResultado.Columns.Add("Estado", typeof(System.String));
            }
            return dtResultado;
        }

        public DataTable Posicion()
        {
            if (dtPosicion.Columns.Count == 0)
            {
                dtPosicion.Columns.Add("Numero", typeof(System.String));
                dtPosicion.Columns.Add("Token", typeof(System.String));
            }

            if (dtPosicion.Rows.Count == 0)
            {
                dtPosicion.Rows.Add("0", "Identificador");
                dtPosicion.Rows.Add("1", "Delimitador");
                dtPosicion.Rows.Add("2", "Operador Relacional");
                dtPosicion.Rows.Add("3", "Operador Lógico");
                dtPosicion.Rows.Add("4", "Cadena");
                dtPosicion.Rows.Add("5", "Numero Entero");
                dtPosicion.Rows.Add("6", "Numero Real");
                dtPosicion.Rows.Add("7", "Palabra Reservada");
                dtPosicion.Rows.Add("8", "Operador Aritmético");
                dtPosicion.Rows.Add("9", "Operador de Asignación");
                dtPosicion.Rows.Add("10", "Comentario");
                
            }
            return dtPosicion;
        }
    }



}
