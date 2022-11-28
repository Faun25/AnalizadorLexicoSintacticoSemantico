using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalizadorSintacticoSemantico
{
    public partial class Form1 : Form
    {
        #region -VARIABLES-
        DataTable dtABC = new DataTable();
        DataTable dtNumeros = new DataTable();
        DataTable dtSignosPuntuacion = new DataTable();
        DataTable dtComentarios = new DataTable();
        DataTable dtOperadoresAritmeticos = new DataTable();
        DataTable dtOperadoresAsigDel = new DataTable();
        DataTable dtOperadoresLogicos = new DataTable();
        DataTable dtCadena = new DataTable();
        DataTable dtDelimitadores = new DataTable();
        DataTable dtReservadas = new DataTable();
        DataTable dtPosicion = new DataTable();

        DataTable dtResultado = new DataTable();

        string componenteIdentLogicReser = "Identificador";
        string componenteNumero = "Numero Entero";
        string componenteAsigRel = "Operador Relacional";

        string cadena = "";
        string tokenABC = "";
        string tokenNum = "";
        string tokenComent = "";
        string tokenCadena = "";
        string tokenOperAsig = "";
        string tokenDelimitador = "";

        string semantica = "";

        char letra;
        char letraABC;
        char letraNum;
        char letraPunto;
        char letraComent;
        char letraCadena;
        char letraOperAsig;
        char letraDelimitador;

        int posicionLetra1 = 0;
        int estado = 0;

        ArrayList Componentes = new ArrayList();
        ArrayList Tokens = new ArrayList();
        ArrayList Estados = new ArrayList();
        ArrayList Posicion = new ArrayList();
        ArrayList Tokens_V2 = new ArrayList();
        ArrayList Componente_V2 = new ArrayList();

        LlenarTablas Llenar = new LlenarTablas();

        bool EnUso = false;
        #endregion

        #region -COSTRUCTORES-
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region -METODOS
        private void LlenarTablas()
        {
            dtABC = Llenar.ABC();
            dtNumeros = Llenar.Numeros();
            dtSignosPuntuacion = Llenar.SignosPuntuacion();
            dtComentarios = Llenar.Comentarios();
            dtOperadoresAritmeticos = Llenar.OperadoresAritmeticos();
            dtOperadoresAsigDel = Llenar.OperadoresAsignacionRelacionales();
            dtOperadoresLogicos = Llenar.OperadoresLogicos();
            dtCadena = Llenar.Cadenas();
            dtDelimitadores = Llenar.Delimitadores();
            dtReservadas = Llenar.Reservadas();
            dtPosicion = Llenar.Posicion();

            dtResultado = Llenar.Resultado();
        }

        private void comparaCadena()
        {
            cadena = txtCadena.Text;
            for (int posicionLetra = 0; posicionLetra < cadena.Length; posicionLetra++)
            {
                letra = Convert.ToChar(cadena[posicionLetra]);
                if (letra != ' ')
                {
                    /*Comparación para letreros.*/
                    foreach (DataRow renglon in dtABC.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && EnUso != true)
                        {
                            entrarLetreros(cadena);
                            cadena = cadena.Substring(tokenABC.Length);
                            EnUso = true;
                            break;
                        }
                    }

                    /*Comparación para numeros.*/
                    //foreach (DataRow renglon in dtNumeros.Rows)
                    //{
                    //    if (letra == Convert.ToChar(renglon["Componente"]))
                    //    {
                    //        entrarNumeros(cadena);
                    //        cadena = cadena.Substring(tokenNum.Length);
                    //        break;
                    //    }
                    //}

                    /*Comparación para decimal al inicio.*/
                    foreach (DataRow renglon in dtSignosPuntuacion.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && EnUso != true)
                        {
                            entrarDecimalV2(0);
                            try
                            {
                                cadena = cadena.Substring(tokenNum.Length);
                            }
                            catch { }
                            EnUso = true;
                            break;
                        }
                    }

                    /*Comparación para comentarios.*/
                    foreach (DataRow renglon in dtComentarios.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && letra != Convert.ToChar(dtComentarios.Rows[1]["Componente"]))
                        {
                            entrarComentarios();
                            cadena = cadena.Substring(tokenComent.Length);
                        }
                        if (letra == Convert.ToChar(dtComentarios.Rows[1]["Componente"]))
                        {
                            Tokens.Add(letra);
                            Componentes.Add("Comentario");
                            cadena = cadena.Substring(1);
                            break;
                        }
                    }

                    /*Comparación para Operadores Asignación o Relacionales.*/
                    foreach (DataRow renglon in dtOperadoresAsigDel.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && EnUso != true)
                        {
                            entrarOperadoresAsigRel(cadena);
                            cadena = cadena.Substring(tokenOperAsig.Length);
                            EnUso = true;
                            break;
                        }
                    }

                    /*Comparación para Operadores Aritméticos.*/
                    foreach (DataRow renglon in dtOperadoresAritmeticos.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && EnUso != true)
                        {
                            Tokens.Add(letra);
                            Componentes.Add("Operador Aritmético");
                            cadena = cadena.Substring(1);
                            EnUso = true;
                            break;
                        }
                    }

                    /*Comparación para cadenas.*/
                    foreach (DataRow renglon in dtCadena.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && EnUso != true)
                        {
                            entrarCadenas();
                            cadena = cadena.Substring(tokenCadena.Length);
                            EnUso = true;
                            break;
                        }
                    }

                    /*Comparacion para Delimitadores.*/
                    foreach (DataRow renglon in dtDelimitadores.Rows)
                    {
                        if (letra == Convert.ToChar(renglon["Componente"]) && letra != Convert.ToChar(dtDelimitadores.Rows[1]["Componente"]) && letra != Convert.ToChar(dtDelimitadores.Rows[3]["Componente"]) && EnUso != true)
                        {
                            entrarDelimitadores();
                            cadena = cadena.Substring(tokenDelimitador.Length);
                        }
                        if (letra == Convert.ToChar(dtDelimitadores.Rows[1]["Componente"]))
                        {
                            Tokens.Add(letra);
                            Componentes.Add("Delimitador");
                            cadena = cadena.Substring(1);
                            break;
                        }
                        if (letra == Convert.ToChar(dtDelimitadores.Rows[3]["Componente"]))
                        {
                            Tokens.Add(letra);
                            Componentes.Add("Delimitador");
                            cadena = cadena.Substring(1);
                            break;
                        }
                    }
                    posicionLetra = -1;
                    EnUso = false;
                }else
                {
                    cadena = cadena.Substring(1);
                    posicionLetra = -1;
                }
            }
            LlenarEstado();
            dgvDatos.DataSource = dtResultado;
        }

        private void entrarLetreros(string cadena)
        {
            tokenABC = "";
            foreach (var obj in cadena)
            {
                letraABC = Convert.ToChar(obj);

                foreach (DataRow renglon in dtABC.Rows)
                {
                    if (letraABC == Convert.ToChar(renglon["Componente"]) && estado == 0)
                    {
                        estado = 1;
                        break;
                    }
                    else
                    {
                        estado = 0;
                    }
                }

                if (estado == 0)
                {
                    break;
                }
                tokenABC = tokenABC + "" + letraABC;
                posicionLetra1 = posicionLetra1 + 1;
                estado = 0;
            }
            Tokens.Add(tokenABC);
            Componentes.Add("Identificador");
        }

        private void entrarNumeros(string cadena)
        {
            int contador = 0;
            tokenNum = "";
            componenteIdentLogicReser = "Numero Entero";
            componenteNumero = "Numero Entero";
            foreach (var obj in cadena)
            {
                letraNum = Convert.ToChar(obj);
                /*Comparación para números.*/
                foreach (DataRow renglon in dtNumeros.Rows)
                {
                    if (letraNum == Convert.ToChar(renglon["Componente"]))
                    {
                        estado = 1;
                        break;
                    }
                    else
                    {
                        bool edo = false;

                        edo = entrarDecimal(contador);
                        if (edo == false)
                        {
                            estado = 0;
                        }
                        else
                        {
                            componenteIdentLogicReser = "Numero Real";
                            componenteNumero = "Numero Real";
                            estado = 1;
                            break;
                        }
                    }
                    
                }

                if (estado == 0)
                {
                    break;
                }
                tokenNum = tokenNum + "" + letraNum;
                posicionLetra1 = posicionLetra1 + 1;
                contador = contador + 1;
            }
            Tokens.Add(tokenNum);
            Componentes.Add(componenteNumero);
        }

        private bool entrarDecimal(int contador)
        {
            bool edo = false;
            foreach (DataRow renglon in dtSignosPuntuacion.Rows)
            {
                if (letraNum == Convert.ToChar(renglon["Componente"]) && contador != 0)
                {
                    edo = true;
                    break;
                }
                else
                {
                    edo = false;
                }

            }
            return edo;
        }

        private void entrarDecimalV2(int contador)
        {
            tokenNum = "";
            componenteNumero = "Numero Real";
            if (cadena.Length > 1)
            {
                foreach (var obj in cadena)
                {
                    letraPunto = Convert.ToChar(obj);

                    foreach (DataRow renglon in dtSignosPuntuacion.Rows)
                    {
                        if (letraPunto == Convert.ToChar(renglon["Componente"]))
                        {
                            estado = 1;
                            break;
                        }
                        else
                        {
                            foreach (DataRow renglon2 in dtNumeros.Rows)
                            {
                                if (letraPunto == Convert.ToChar(renglon2["Componente"]))
                                {
                                    estado = 1;
                                    break;
                                }
                                else
                                {
                                    estado = 0;
                                }
                            }
                        }
                    }

                    if (estado == 0)
                    {
                        break;
                    }
                    tokenNum = tokenNum + "" + letraPunto;
                    posicionLetra1 = posicionLetra1 + 1;
                    contador = contador + 1;
                }
            }
            //else
            //{
            //    tokenNum = ".";
            //    componenteNumero = "Delimitador";
            //}
            Tokens.Add(tokenNum);
            Componentes.Add(componenteNumero);
            
            //dtResultado.Rows.Add(tokenNum, componenteNumero);
        }

        private void entrarComentarios()
        {
            tokenComent = "";
            foreach (var obj in cadena)
            {
                letraComent = Convert.ToChar(obj);
                if (letraComent != Convert.ToChar(dtComentarios.Rows[1]["Componente"]))
                {
                    estado = 1;
                }else
                {
                    estado = 0;
                }

                tokenComent = tokenComent + "" + letraComent;
                posicionLetra1 = posicionLetra1 + 1;
                if (estado == 0)
                {
                    break;
                }
            }
            //dtResultado.Rows.Add(tokenComent, "Comentario");
            Tokens.Add(tokenComent);
            Componentes.Add("Comentario");
        }

        private void entrarCadenas()
        {
            int numComillas = 0;
            tokenCadena = "";
            foreach (var obj in cadena)
            {
                letraCadena = Convert.ToChar(obj);
                if (letraCadena != Convert.ToChar(dtCadena.Rows[0]["Componente"]))
                {
                    estado = 1;
                }
                else
                {
                    numComillas = numComillas + 1;
                    estado = 0;
                }

                tokenCadena = tokenCadena + "" + letraCadena;
                posicionLetra1 = posicionLetra1 + 1;
                if (numComillas == 2)
                {
                    break;
                }
            }
            //dtResultado.Rows.Add(tokenCadena, "Cadena");
            Tokens.Add(tokenCadena);
            Componentes.Add("Cadena");
        }

        private void entrarOperadoresAsigRel(string cadena)
        {
            tokenOperAsig = "";
            componenteAsigRel = "Operador Relacional";
            int contador = 0;
            foreach (var obj in cadena)
            {
                letraOperAsig = Convert.ToChar(obj);

                foreach (DataRow renglon in dtOperadoresAsigDel.Rows)
                {
                    if (letraOperAsig == Convert.ToChar(renglon["Componente"]))
                    {
                        estado = 1;
                        contador = contador + 1;
                        if (contador > 2)
                        {
                            estado = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        estado = 0;
                    }
                }

                if (estado == 0)
                {
                    break;
                }
                tokenOperAsig = tokenOperAsig + "" + letraOperAsig;
            }
            if (tokenOperAsig == "=")
            {
                componenteAsigRel = "Operador de Asignación";
            }
            Tokens.Add(tokenOperAsig);
            Componentes.Add(componenteAsigRel);
        }

        private void entrarDelimitadores()
        {
            tokenDelimitador = "";
            foreach (var obj in cadena)
            {
                letraDelimitador = Convert.ToChar(obj);

                if (letraDelimitador != Convert.ToChar(dtDelimitadores.Rows[1]["Componente"]) && letraDelimitador != Convert.ToChar(dtDelimitadores.Rows[3]["Componente"]) && tokenDelimitador != dtDelimitadores.Rows[5]["Componente"].ToString())
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }

                tokenDelimitador = tokenDelimitador + "" + letraDelimitador;
                posicionLetra1 = posicionLetra1 + 1;
                if (estado == 0)
                {
                    break;
                }
            }
            //dtResultado.Rows.Add(tokenDelimitador, "Delimitador");
            Tokens.Add(tokenDelimitador);
            Componentes.Add("Delimitador");
        }


        private void LlenarEstado()
        {
            int numElementos = Tokens.Count;

            for (int c = 0; c < numElementos; c++)
            {
                string op = Componentes[c].ToString();
                string token = Tokens[c].ToString();
                string estado = "";

                switch (op)
                {
                    case ("Identificador"):
                        {
                            componenteIdentLogicReser = "Identificador";
                            estado = ValidacionIdentificador(token);
                            Componentes[c] = componenteIdentLogicReser;
                            Estados.Add(estado);
                            break;
                        }
                    case ("Numero Entero"):
                        {
                            estado = ValidacionNumeroEntero(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Numero Real"):
                        {
                            estado = ValidacionNumeroReal(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Comentario"):
                        {
                            estado = ValidacionComentario(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Cadena"):
                        {
                            estado = ValidacionCadena(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Operador de Asignación"):
                        {
                            estado = ValidacionOperadorAsignacion(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Operador Relacional"):
                        {
                            estado = ValidacionOperadorRelacional(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Operador Aritmético"):
                        {
                            estado = ValidacionOperadorAritmetico(token);
                            Estados.Add(estado);
                            break;
                        }
                    case ("Delimitador"):
                        {
                            estado = ValidacionDelimitador(token);
                            Estados.Add(estado);
                            break;
                        }
                }
            }

            for (int c1 = 0; c1 < numElementos; c1++)
            {
                string token = Tokens[c1].ToString();
                string componente = Componentes[c1].ToString();
                string estado = Estados[c1].ToString();

                dtResultado.Rows.Add(token, componente, estado);
            }
        }


        private string ValidacionIdentificador(string token)
        {
            string valCont = "Cadena Válida";
            string validacion = "Cadena Válida";
            int numGuiones = 0;

            for (int contador = 0; contador < token.Length; contador++)
            {
                if (token[contador] == '-' || token[contador] == '_')
                {
                    numGuiones = numGuiones + 1;
                    if(numGuiones == 2)
                    {
                        validacion = "Cadena Inválida Error 501 no puede tener 2 guiones seguidos.";
                    }
                }else
                {
                    numGuiones = 0;
                }
            }

            if (validacion == "Cadena Válida")
            {
                foreach (DataRow renglon in dtABC.Rows)
                {
                    char primeraPosicion = Convert.ToChar(token.Substring(0, 1));
                    if (primeraPosicion == Convert.ToChar(renglon["Componente"]) && renglon["Token"].ToString() == "Identificador" && !char.IsDigit(primeraPosicion))
                    {
                        componenteIdentLogicReser = "Identificador";
                        validacion = "Cadena Válida";
                        valCont = "Cadena Válida";
                        break;
                    }else
                    {
                        componenteIdentLogicReser = "Identificador";
                        validacion = "Cadena Inválida Error 501 debe empezar con una letra.";
                        valCont = "Cadena Válida";
                    }
                }

                if (valCont == "Cadena Válida")
                {
                    foreach (DataRow renglon in dtABC.Rows)
                    {
                        char ultimaPosicion = Convert.ToChar(token.Substring(token.Length - 1, 1));
                        if (ultimaPosicion == Convert.ToChar(renglon["Componente"]) && renglon["Token"].ToString() != "IdentificadorG")
                        {
                            componenteIdentLogicReser = "Identificador";
                            validacion = "Cadena Válida";
                            break;
                        }
                        else
                        {
                            componenteIdentLogicReser = "Identificador";
                            validacion = "Cadena Inválida Error 501 no debe terminar con guión.";
                        }
                    }
                }
            }
            
            foreach (DataRow renglon in dtOperadoresLogicos.Rows)
            {
                string operador = renglon["Componente"].ToString();
                if (token.ToLower() == operador.ToLower())
                {
                    componenteIdentLogicReser = "Operador Lógico";
                    validacion = "Cadena Válida";
                }
            }

            if (validacion == "Cadena Válida")
            {
                foreach (DataRow renglon in dtReservadas.Rows)
                {
                    string reservada = renglon["Componente"].ToString();
                    if (token.ToLower() == reservada.ToLower())
                    {
                        componenteIdentLogicReser = "Palabra Reservada";
                        validacion = "Cadena Válida";
                    }
                }
            }

            if(validacion == "Cadena Válida")
            {
                try
                {

                    Convert.ToDecimal(token);
                    if (token.Substring(token.Length - 1) == "." || token.Substring(token.Length - 1) == ",")
                    {
                        componenteIdentLogicReser = "Numero Real";
                        validacion = "Cadena Inválida Error 501 debe contener al menos 1 dígito a la derecha del punto o coma.";
                    }
                    else
                    {
                        if (token[0] == '.' || token[0] == ',')
                        {
                            componenteIdentLogicReser = "Numero Real";
                            validacion = "Cadena Inválida Error 501 debe contener al menos 1 dígito a la izquierda del punto o coma.";
                        } else
                        {
                            entrarNumeros(token);
                        }
                    }
                }
                catch
                {
                    //Para buscar la cantidad de puntos en una cadena.
                    string Palabra = "";
                    int totalPuntos = 0;
                    for (int i = 0; i < token.Length; i++)
                    {
                        char caracter = token[i];
                        Palabra = Palabra + caracter;

                        if (Palabra.Contains("."))
                        {
                            totalPuntos++;
                            Palabra = "";
                        }
                    }

                    if (totalPuntos > 1)
                    {
                        componenteIdentLogicReser = "Numero Real";
                        validacion = "Cadena Inválida Error 501 no debe contener más de 1 punto o más de 1 coma.";
                    }
                    else
                    {
                        if (token == "." || token == ",")
                        {
                            componenteIdentLogicReser = "Delimitador";
                            validacion = "Cadena Válida";
                        } else if (token.Contains(".") || token.Contains(","))
                        {
                            componenteIdentLogicReser = "Identificador";
                            validacion = "Cadena Inválida Error 501 no debe contener puntos o comas.";
                        }
                        else
                        {
                            foreach (char obj in token)
                            {
                                if (char.IsDigit(token[0]))
                                {
                                    if (char.IsLetter(obj))
                                    {
                                        validacion = "Cadena Inválida Error 501 debe empezar con una letra.";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return validacion;
        }

        private string ValidacionNumeroEntero(string token)
        {
            return "Cadena Válida";
        }

        private string ValidacionNumeroReal(string token)
        {
            string validacion = "Cadena Válida";
            int numPuntos = 0;
            char posicionInicial = ' ';
            char posicionFinal = ' '; 
            try
            {
                posicionInicial = Convert.ToChar(token.Substring(0, 1));
                posicionFinal = Convert.ToChar(token.Substring(token.Length - 1, 1));
            }
            catch { }

            for (int contador = 0; contador < token.Length; contador++)
            {
                if (token[contador] == '.')
                {
                    numPuntos++;
                    if(numPuntos == 2)
                    {
                        validacion = "Cadena Inválida Error 500 no puede tener más de 1 punto.";
                    }
                }
            }

            if (validacion == "Cadena Válida") {
                foreach (DataRow renglon in dtNumeros.Rows)
                {
                    if (posicionInicial != Convert.ToChar(renglon["Componente"]))
                    {
                        validacion = "Cadena Inválida Error 500 debe empezar con un dígito.";
                    } else
                    {
                        validacion = "Cadena Válida";
                        break;
                    }
                }
            }

            if (validacion == "Cadena Válida")
            {
                foreach (DataRow renglon in dtNumeros.Rows)
                {
                    foreach (DataRow renglon1 in dtNumeros.Rows)
                    {
                        if (posicionFinal != Convert.ToChar(renglon1["Componente"]))
                        {
                            validacion = "Cadena Inválida Error 500 debe terminar con un dígito.";
                        }
                        else
                        {
                            validacion = "Cadena Válida";
                            break;
                        }
                    }
                }
            }

            return validacion;
        }

        private string ValidacionComentario(string token)
        {
            string Validacion = "Cadena Válida";
            char posicionInicial = Convert.ToChar(token.Substring(0, 1));
            char posicionFinal = Convert.ToChar(token.Substring(token.Length - 1, 1));

            foreach (DataRow renglon in dtComentarios.Rows)
            {
                if (posicionInicial != Convert.ToChar(dtComentarios.Rows[0]["Componente"]))
                {
                    Validacion = "Cadena Inválida Error 502 debe iniciar con un {";
                }
                if(posicionFinal != Convert.ToChar(dtComentarios.Rows[1]["Componente"]))
                {
                    Validacion = "Cadena Inválida Error 502 debe cerrar el comentario con un }.";
                }
            }

            return Validacion;
        }

        private string ValidacionCadena(string token)
        {
            string Validacion = "Cadena Válida";
            char posicionInicial = Convert.ToChar(token.Substring(0, 1));
            char posicionFinal = Convert.ToChar(token.Substring(token.Length - 1, 1));

            foreach (DataRow renglon in dtCadena.Rows)
            {
                if (posicionInicial != Convert.ToChar(dtCadena.Rows[0]["Componente"]) || posicionFinal != Convert.ToChar(dtCadena.Rows[0]["Componente"]))
                {
                    Validacion = "Cadena Inválida Error 103 debe cerrar con comillas.";
                }
            }

            return Validacion;
        }

        private string ValidacionOperadorAsignacion(string token)
        {
            string Validacion = "Cadena Válida";

            return Validacion;
        }

        private string ValidacionOperadorRelacional(string token)
        {
            string Validacion = "Cadena Válida";
            char posicionInicial = Convert.ToChar(token.Substring(0, 1));
            char posicionFinal = Convert.ToChar(token.Substring(token.Length - 1, 1));

            if (token.Length != 1)
            {
                if (posicionInicial == Convert.ToChar(dtOperadoresAsigDel.Rows[1]["Componente"]))
                {
                    if(posicionFinal != Convert.ToChar(dtOperadoresAsigDel.Rows[1]["Componente"]))
                    {
                        Validacion = "Cadena Inválida Error 505 debe terminar con =";
                    }
                }

                if (posicionInicial == Convert.ToChar(dtOperadoresAsigDel.Rows[2]["Componente"]) || posicionInicial == Convert.ToChar(dtOperadoresAsigDel.Rows[0]["Componente"]))
                {
                    if (posicionFinal == Convert.ToChar(dtOperadoresAsigDel.Rows[0]["Componente"]))
                    {
                        Validacion = "Cadena Inválida Error 505 debe terminar con =";
                    }
                }
            }

            return Validacion;
        }

        private string ValidacionOperadorAritmetico(string token)
        {
            string Validacion = "Cadena Válida";

            return Validacion;
        }

        private string ValidacionDelimitador(string token)
        {
            string Validacion = "Cadena Válida";

            char posicionInicial = ' ';
            char posicionFinal = ' ';

            try
            {
                posicionInicial = Convert.ToChar(token.Substring(0, 1));
                posicionFinal = Convert.ToChar(token.Substring(token.Length - 1, 1));
            }
            catch { }

            foreach (DataRow renglon in dtDelimitadores.Rows)
            {
                if (posicionInicial == Convert.ToChar(dtDelimitadores.Rows[0]["Componente"]))
                {
                    if (posicionFinal == Convert.ToChar(dtDelimitadores.Rows[1]["Componente"]))
                    {
                        Validacion = "Cadena Válida";
                    }
                    else
                    {
                        Validacion = "Cadena Inválida Error 506 debe terminar con )";
                    }
                }else
                {
                    if (posicionInicial == Convert.ToChar(dtDelimitadores.Rows[2]["Componente"]))
                    {
                        if (posicionFinal == Convert.ToChar(dtDelimitadores.Rows[3]["Componente"]))
                        {
                            Validacion = "Cadena Válida";
                        }
                        else
                        {
                            Validacion = "Cadena Inválida Error 506 debe terminar con ]";
                        }
                    }
                    else
                    {
                        if(posicionInicial == Convert.ToChar(dtDelimitadores.Rows[4]["Componente"]) || posicionInicial == Convert.ToChar(dtDelimitadores.Rows[5]["Componente"]) || 
                            posicionInicial == Convert.ToChar(dtDelimitadores.Rows[6]["Componente"]) || posicionInicial == '.')
                        {
                            Validacion = "Cadena Válida";
                        }
                    }
                }
            }
            return Validacion;
        }


        private void MostrarSemanticaCorrecta()
        {
            AsignarNumeroToken();
            OrdenarTokens();

            for (int cont = 0; cont < Posicion.Count; cont++)
            {
                if (Componente_V2[cont] == "Identificador" || Componente_V2[cont] == "Numero Entero" || Componente_V2[cont] == "Numero Real")
                {
                    OrdenarIdentificadorNumeros(Tokens_V2[cont].ToString(), Componente_V2[cont].ToString());//Dentro del método se agrega el token ordenado.
                }
                else
                {
                    semantica = semantica + Tokens_V2[cont] + " ";
                }
            }

            txtSemanticaCorrecta.Text = semantica;
        }

        private void AsignarNumeroToken()
        {
            DataTable dtTemporal = dgvDatos.DataSource as DataTable;

            //En este bucle se asigna una posición a cada token para saber como ordenarlo.
            foreach (DataRow row in dtTemporal.Rows)
            {
                string token = row["Token"].ToString();
                string componente = row["Componente"].ToString();
                string estado = row["Estado"].ToString();

                if (estado == "Cadena Válida")
                {
                    foreach (DataRow renglon in dtPosicion.Rows)
                    {
                        if (renglon["Token"].ToString() == componente)
                        {
                            Posicion.Add(Convert.ToInt32(renglon["Numero"]));
                            Tokens_V2.Add(token);
                            Componente_V2.Add(componente);
                        }
                    }
                }
            }
        }

        private void OrdenarTokens()
        {
            //Ordenamiento Burbuja.
            int cantidadTokens = Posicion.Count;

            for (int cont = 0; cont < cantidadTokens; cont++)
            {
                for (int cont2 = cont + 1; cont2 < cantidadTokens; cont2++)
                {
                    int aux1;
                    string aux2;
                    string aux3;

                    if (Convert.ToInt32(Posicion[cont]) > Convert.ToInt32(Posicion[cont2]))
                    {
                        aux1 = Convert.ToInt32(Posicion[cont]);
                        aux2 = Tokens_V2[cont].ToString();
                        aux3 = Componente_V2[cont].ToString();

                        Posicion[cont] = Posicion[cont2];
                        Tokens_V2[cont] = Tokens_V2[cont2];
                        Componente_V2[cont] = Componente_V2[cont2];

                        Posicion[cont2] = aux1;
                        Tokens_V2[cont2] = aux2;
                        Componente_V2[cont2] = aux3;
                    }
                }
            }
        }

        private void OrdenarIdentificadorNumeros(string token, string componente)
        {
            switch (componente)
            {
                case ("Identificador"):
                    {
                        string aux;
                        int aux2;
                        ArrayList Letras = new ArrayList();
                        ArrayList Letras2 = new ArrayList();
                        //Agregamos el identificador a un arreglo.
                        for (int cont0 = 0; cont0 < token.Length; cont0++)
                        {
                            try
                            {
                                Letras.Add(Convert.ToInt32(token[cont0]));//Se agrega letra en código ASCII.
                                Letras2.Add(token.Substring(cont0, 1));//Se agrega Letra.
                            }
                            catch { }
                        }
                        //Lo ordenamos por el método de burbuja.
                        for (int cont1 = 0; cont1 < token.Length; cont1++)
                        {
                            for (int cont2 = cont1 + 1; cont2 < token.Length; cont2++)
                            {
                                if (Convert.ToInt32(Letras[cont1]) < Convert.ToInt32(Letras[cont2]))
                                {
                                    try
                                    {
                                        //Mover las letras.
                                        aux = token.Substring(cont1, 1);
                                        Letras2[cont1] = token.Substring(cont2, 1);
                                        Letras2[cont2] = aux;

                                        //Mover letras en código ASCII.
                                        aux2 = Convert.ToInt32(Letras[cont1]);
                                        Letras[cont1] = Letras[cont2];
                                        Letras[cont2] = aux2;

                                        token = "";
                                        foreach (var digito in Letras2)
                                        {
                                            token = token + digito;
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        //Juntamos los números de nuevo pero ya ordenados.
                        string identificadorOrdenado = "";
                        foreach (var digito in Letras2)
                        {
                            identificadorOrdenado = identificadorOrdenado + digito;
                        }

                        int cont = 0;
                        string numeros = "";
                        string textoMinus = "";
                        string textoMayus = "";
                        string otrosCaracteres = "";
                        //Para separar números, letras minúsculas, letras mayúsculas y guiones.
                        foreach (var letra in identificadorOrdenado)
                        {
                            if (char.IsDigit(letra))
                            {
                                numeros = numeros + identificadorOrdenado.Substring(0, 1);
                                identificadorOrdenado = identificadorOrdenado.Substring(1);
                                cont = -1;
                            }else if (char.IsLower(letra))
                            {
                                textoMinus = textoMinus + identificadorOrdenado.Substring(0, 1);
                                identificadorOrdenado = identificadorOrdenado.Substring(1);
                                cont = -1;
                            }
                            else if(char.IsUpper(letra))
                            {
                                textoMayus = textoMayus + identificadorOrdenado.Substring(0, 1);
                                identificadorOrdenado = identificadorOrdenado.Substring(1);
                                cont = -1;
                            }
                            else
                            {
                                otrosCaracteres = otrosCaracteres + identificadorOrdenado.Substring(0, 1);
                                identificadorOrdenado = identificadorOrdenado.Substring(1);
                                cont = -1;
                            }
                            cont++;
                        }

                        ArrayList LetrasMayus = new ArrayList();
                        ArrayList LetrasMayus2 = new ArrayList();
                        //Agregamos las letras mayusculas a un arreglo.
                        for (int cont0 = 0; cont0 < textoMayus.Length; cont0++)
                        {
                            try
                            {
                                LetrasMayus.Add(Convert.ToInt32(textoMayus[cont0]));//Se agrega letra en código ASCII.
                                LetrasMayus2.Add(textoMayus.Substring(cont0, 1));//Se agrega Letra.
                            }
                            catch { }
                        }

                        //Ordenamos por el método de burbuja las letras mayusculas.
                        for (int cont1 = 0; cont1 < textoMayus.Length; cont1++)
                        {
                            for (int cont2 = cont1 + 1; cont2 < textoMayus.Length; cont2++)
                            {
                                if (Convert.ToInt32(LetrasMayus[cont1]) > Convert.ToInt32(LetrasMayus[cont2]))
                                {
                                    try
                                    {
                                        //Mover las letras.
                                        aux = textoMayus.Substring(cont1, 1);
                                        LetrasMayus2[cont1] = textoMayus.Substring(cont2, 1);
                                        LetrasMayus2[cont2] = aux;

                                        //Mover letras en código ASCII.
                                        aux2 = Convert.ToInt32(LetrasMayus[cont1]);
                                        LetrasMayus[cont1] = LetrasMayus[cont2];
                                        LetrasMayus[cont2] = aux2;

                                        textoMayus = "";
                                        foreach (var digito in LetrasMayus2)
                                        {
                                            textoMayus = textoMayus + digito;
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }

                        ArrayList LetrasMinus = new ArrayList();
                        ArrayList LetrasMinus2 = new ArrayList();
                        //Agregamos las letras minusculas a un arreglo.
                        for (int cont0 = 0; cont0 < token.Length; cont0++)
                        {
                            try
                            {
                                LetrasMinus.Add(Convert.ToInt32(textoMinus[cont0]));//Se agrega letra en código ASCII.
                                LetrasMinus2.Add(textoMinus.Substring(cont0, 1));//Se agrega Letra.
                            }
                            catch { }
                        }
                        //Ordenamos por el método de burbuja las letras minusculas.
                        for (int cont1 = 0; cont1 < textoMinus.Length; cont1++)
                        {
                            for (int cont2 = cont1 + 1; cont2 < textoMinus.Length; cont2++)
                            {
                                if (Convert.ToInt32(LetrasMinus[cont1]) > Convert.ToInt32(LetrasMinus[cont2]))
                                {
                                    try
                                    {
                                        //Mover las letras.
                                        aux = textoMinus.Substring(cont1, 1);
                                        LetrasMinus2[cont1] = textoMinus.Substring(cont2, 1);
                                        LetrasMinus2[cont2] = aux;

                                        //Mover letras en código ASCII.
                                        aux2 = Convert.ToInt32(LetrasMinus[cont1]);
                                        LetrasMinus[cont1] = LetrasMinus[cont2];
                                        LetrasMinus[cont2] = aux2;

                                        textoMinus = "";
                                        foreach (var digito in LetrasMinus2)
                                        {
                                            textoMinus = textoMinus + digito;
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }

                        string cadenaOrdena = textoMinus + textoMayus + numeros + otrosCaracteres;
                        semantica = semantica + cadenaOrdena + " ";
                        break;
                    }
                case ("Numero Entero"):
                    {
                        int aux;
                        string dig = "";
                        ArrayList Numero = new ArrayList();
                        //Agregamos el número a un arreglo.
                        for (int cont0 = 0; cont0 < token.Length; cont0++)
                        {
                            try
                            {
                                dig = token.Substring(cont0, 1);
                            }
                            catch { }
                            Numero.Add(dig);
                        }
                        //Lo ordenamos por el método de burbuja.
                        for (int cont = 0; cont < token.Length; cont++)
                        {
                            for (int cont2 = cont + 1; cont2 < token.Length; cont2++)
                            {
                                if (Convert.ToInt32(Numero[cont]) < Convert.ToInt32(Numero[cont2]))
                                {
                                    aux = Convert.ToInt32(Numero[cont]);
                                    Numero[cont] = Numero[cont2];
                                    Numero[cont2] = aux;
                                }
                            }
                        }
                        //Juntamos los números de nuevo pero ya ordenados.
                        string numeroOrdenado = "";
                        foreach(var digito in Numero)
                        {
                            numeroOrdenado = numeroOrdenado + digito;
                        }
                        semantica = semantica + numeroOrdenado + " ";
                        break;
                    }
                case ("Numero Real"):
                    {
                        int aux;
                        string dig = "";
                        ArrayList Numero = new ArrayList();
                        //Agregamos el número a un arreglo.
                        for (int cont0 = 0; cont0 < token.Length; cont0++)
                        {
                            try
                            {
                                dig = token.Substring(cont0, 1);
                            }
                            catch { }
                            Numero.Add(dig);
                        }
                        //Lo ordenamos por el método de burbuja.
                        for (int cont = 0; cont < token.Length; cont++)
                        {
                            for (int cont2 = cont + 1; cont2 < token.Length; cont2++)
                            {
                                try
                                {
                                    if (Convert.ToInt32(Numero[cont]) < Convert.ToInt32(Numero[cont2]))
                                    {
                                        aux = Convert.ToInt32(Numero[cont]);
                                        Numero[cont] = Numero[cont2];
                                        Numero[cont2] = aux;
                                    }
                                }
                                catch { }
                            }
                        }
                        //Juntamos los números de nuevo pero ya ordenados.
                        string numeroOrdenado = "";
                        foreach (var digito in Numero)
                        {
                            numeroOrdenado = numeroOrdenado + digito;
                        }
                        semantica = semantica + numeroOrdenado + " ";
                        break;
                    }
            }
        }

        private void Limpiar()
        {
            dtResultado.Clear();

            Componentes.Clear();
            Tokens.Clear();
            Estados.Clear();

            Tokens_V2.Clear();
            Posicion.Clear();
            Componente_V2.Clear();
            semantica = "";
        }
        #endregion

        #region -EVENTOS-
        private void Form1_Load(object sender, EventArgs e)
        {
            LlenarTablas();
        }

        private void btnAnaliza_Click(object sender, EventArgs e)
        {
            Limpiar();
            comparaCadena();
            MostrarSemanticaCorrecta();
        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
