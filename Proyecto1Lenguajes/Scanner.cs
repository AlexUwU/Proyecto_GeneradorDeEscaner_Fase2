using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Lenguajes
{
    public class Scanner
    {
        static string proceso;
        bool sets, tokens, actions, error;

        public Dictionary<string, Token> dicTokens = new Dictionary<string, Token>();
        public Dictionary<string, Set> dicSet = new Dictionary<string, Set>();
        public Dictionary<string, Action> dicAction = new Dictionary<string, Action>();
        public Dictionary<Estado, Dictionary<string, Estado>> dicTransiciones;

        List<char> letrasMayusculas = new List<char>();
        List<char> letrasMinusculas = new List<char>();
        List<char> numeros = new List<char>();

        public static int contadorEncabezados;

        public Scanner()
        {
            contadorEncabezados = 0;
            proceso = "";
            agregarLineaProceso("Verificando archivo...");
            sets = false;
            tokens = false;
            actions = false;
            error = false;

            numeros.Add('0');
            numeros.Add('1');
            numeros.Add('2');
            numeros.Add('3');
            numeros.Add('4');
            numeros.Add('5');
            numeros.Add('6');
            numeros.Add('7');
            numeros.Add('8');
            numeros.Add('9');

            letrasMayusculas.Add('A');
            letrasMinusculas.Add('a');
            letrasMayusculas.Add('B');
            letrasMinusculas.Add('b');
            letrasMayusculas.Add('C');
            letrasMinusculas.Add('c');
            letrasMayusculas.Add('D');
            letrasMinusculas.Add('d');
            letrasMayusculas.Add('E');
            letrasMinusculas.Add('e');
            letrasMayusculas.Add('F');
            letrasMinusculas.Add('f');
            letrasMayusculas.Add('G');
            letrasMinusculas.Add('g');
            letrasMayusculas.Add('H');
            letrasMinusculas.Add('h');
            letrasMayusculas.Add('I');
            letrasMinusculas.Add('i');
            letrasMayusculas.Add('J');
            letrasMinusculas.Add('j');
            letrasMayusculas.Add('K');
            letrasMinusculas.Add('k');
            letrasMayusculas.Add('L');
            letrasMinusculas.Add('l');
            letrasMayusculas.Add('M');
            letrasMinusculas.Add('m');
            letrasMayusculas.Add('N');
            letrasMinusculas.Add('n');
            letrasMayusculas.Add('Ñ');
            letrasMinusculas.Add('ñ');
            letrasMayusculas.Add('O');
            letrasMinusculas.Add('o');
            letrasMayusculas.Add('P');
            letrasMinusculas.Add('p');
            letrasMayusculas.Add('Q');
            letrasMinusculas.Add('q');
            letrasMayusculas.Add('R');
            letrasMinusculas.Add('r');
            letrasMayusculas.Add('S');
            letrasMinusculas.Add('s');
            letrasMayusculas.Add('T');
            letrasMinusculas.Add('t');
            letrasMayusculas.Add('U');
            letrasMinusculas.Add('u');
            letrasMayusculas.Add('V');
            letrasMinusculas.Add('v');
            letrasMayusculas.Add('W');
            letrasMinusculas.Add('w');
            letrasMayusculas.Add('X');
            letrasMinusculas.Add('x');
            letrasMayusculas.Add('Y');
            letrasMinusculas.Add('y');
            letrasMayusculas.Add('Z');
            letrasMinusculas.Add('z');
        }

        /// <summary>
        /// Verifica la linea enviada por el lector
        /// </summary>
        /// <param name="numlinea">numero de linea actual</param>
        /// <param name="linea">string con la linea leida</param>
        /// <returns>True si no encuentra ningun error. False lo contrario</returns>
        public bool Verificar(int numlinea, string linea)
        {
            try
            {
                if (numlinea != 1)
                {
                    bool resultado = false;

                    if (sets)
                    {
                        resultado = verificarSet(linea);
                    }
                    else if (tokens)
                    {
                        resultado = verificarToken(linea);
                    }
                    else if (actions)
                    {
                        resultado = verificarAction(linea);
                    }
                    else if (error)
                    {
                        resultado = verificarError(linea);
                    }

                    return resultado;
                }
                else
                {
                    return verificarEncabezadoSets(linea);
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Verifica el encabezado del set
        bool verificarEncabezadoSets(string linea)
        {
            string tempLinea = linea.Trim();

            if (tempLinea.Length == 4 && tempLinea.ToUpper() == "SETS")
            {
                sets = true;
                contadorEncabezados++;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Verifica el set
        bool verificarSet(string linea)
        {
            if (linea.Trim() == "")
                return true;

            if (!linea.Trim().ToUpper().Contains("TOKENS"))
            {
                string lineaTemp = linea.TrimStart();
                lineaTemp = lineaTemp.TrimEnd();
                string nombreSet = "";

                int contador = 0;
                while (lineaTemp.ElementAt(contador) != '=')
                {
                    if (contador == (lineaTemp.Length - 1))
                    {
                        return false;
                    }

                    nombreSet += lineaTemp.ElementAt(contador);
                    contador++;

                }

                nombreSet = nombreSet.Trim();

                if (contador == (lineaTemp.Length - 1))
                {
                    return false;
                }

                contador++;

                Set set = new Set();
                dicSet.Add(nombreSet, set);

                lineaTemp = lineaTemp.Substring(contador);
                lineaTemp = lineaTemp.TrimStart();

                if (!reemplazarChars('+', ref lineaTemp, "*"))
                {
                    return false;
                }

                if (!reemplazarChars('.', ref lineaTemp, "|"))
                {
                    return false;
                }

                if (!reemplazarChars('*', ref lineaTemp, "+"))
                {
                    return false;
                }

                if (!reemplazarChars('|', ref lineaTemp, "."))
                {
                    return false;
                }

                if (lineaTemp == "")
                {
                    return false;
                }

                string[] sets = lineaTemp.Split('+');

                bool dentroComillas = false;
                bool comillasJuntas = false;
                string elemento = "";
                for (int i = 0; i < sets.Length; i++)
                {
                    if (sets[i] == "")
                    {
                        return false;
                    }

                    sets[i] = sets[i].TrimStart();
                    sets[i] = sets[i].TrimEnd();

                    if (sets[i].Contains(".."))
                    {
                        string[] rango = sets[i].Split(new string[] { ".." }, StringSplitOptions.None);

                        if (rango.Length != 2)
                        {
                            return false;
                        }

                        int inicio = 0, fin = 0;
                        int conjunto = 0;
                        for (int k = 0; k < 2; k++)
                        {
                            rango[k] = rango[k].TrimStart();
                            rango[k] = rango[k].TrimEnd();
                            for (int l = 0; l < rango[k].Length; l++)
                            {
                                switch (rango[k].ElementAt(l))
                                {
                                    case ' ':
                                        if (dentroComillas)
                                            return false;
                                        break;
                                    case '\'':
                                        if (dentroComillas)
                                        {
                                            dentroComillas = false;

                                            if (k == 1)
                                            {
                                                if (conjunto == 0)
                                                {
                                                    return false;
                                                }

                                                if (inicio >= fin)
                                                {
                                                    return false;
                                                }
                                                switch (conjunto)
                                                {
                                                    case 1:
                                                        for (int m = inicio; m <= fin; m++)
                                                        {
                                                            string letra = "";
                                                            letra += letrasMayusculas.ElementAt(m);
                                                            dicSet[nombreSet].AgregarElemento(letra);
                                                        }
                                                        break;
                                                    case 2:
                                                        for (int m = inicio; m <= fin; m++)
                                                        {
                                                            string letra = "";
                                                            letra += letrasMinusculas.ElementAt(m);
                                                            dicSet[nombreSet].AgregarElemento(letra);
                                                        }
                                                        break;
                                                    case 3:
                                                        for (int m = inicio; m <= fin; m++)
                                                        {
                                                            string letra = "";
                                                            letra += numeros.ElementAt(m);
                                                            dicSet[nombreSet].AgregarElemento(letra);
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            dentroComillas = true;
                                        }
                                        break;
                                    case 'C':
                                        if ((l + 1) == rango[k].Length)
                                        {
                                            return false;
                                        }

                                        if (rango[k].ElementAt(l + 1) != 'H')
                                        {
                                            return false;
                                        }

                                        if ((l + 2) == rango[k].Length)
                                        {
                                            return false;
                                        }

                                        if (rango[k].ElementAt(l + 2) != 'R')
                                        {
                                            return false;
                                        }

                                        if ((l + 3) == rango[k].Length)
                                        {
                                            return false;
                                        }

                                        if (rango[k].ElementAt(l + 3) != '(')
                                        {
                                            return false;
                                        }

                                        if ((l + 4) == rango[k].Length)
                                        {
                                            return false;
                                        }

                                        l = l + 4;
                                        string numero = "";
                                        while (rango[k].ElementAt(l) != ')')
                                        {
                                            if (l == (rango[k].Length - 1))
                                            {
                                                return false;
                                            }

                                            numero += rango[k].ElementAt(l);
                                            l++;
                                        }

                                        int num = 0;
                                        int.TryParse(numero, out num);

                                        if (num == 0)
                                        {
                                            return false;
                                        }

                                        if (l != rango[k].Length - 1)
                                        {
                                            return false;
                                        }


                                        if (k == 0)
                                        {
                                            inicio = num;
                                        }
                                        else
                                        {
                                            fin = num;

                                            if (inicio >= fin)
                                            {
                                                return false;
                                            }

                                            for (int m = inicio; m <= fin; m++)
                                            {
                                                string caracter = "" + (char)m;
                                                dicSet[nombreSet].AgregarElemento(caracter);
                                            }
                                        }
                                        break;
                                    default:
                                        if (!dentroComillas)
                                        {
                                            return false;
                                        }

                                        if (rango[k].ElementAt(l + 1) != '\'')
                                        {
                                            return false;
                                        }

                                        if (!letrasMayusculas.Contains(rango[k].ElementAt(l)) && !letrasMinusculas.Contains(rango[k].ElementAt(l)) && !numeros.Contains(rango[k].ElementAt(l)))
                                        {
                                            return false;
                                        }

                                        if (letrasMayusculas.Contains(rango[k].ElementAt(l)))
                                        {
                                            if (k == 0)
                                            {
                                                inicio = letrasMayusculas.IndexOf(rango[k].ElementAt(l));
                                                conjunto = 1;
                                            }
                                            else
                                            {
                                                if (conjunto != 1)
                                                {
                                                    return false;
                                                }

                                                fin = letrasMayusculas.IndexOf(rango[k].ElementAt(l));
                                            }
                                        }
                                        else if (letrasMinusculas.Contains(rango[k].ElementAt(l)))
                                        {
                                            if (k == 0)
                                            {
                                                inicio = letrasMinusculas.IndexOf(rango[k].ElementAt(l));
                                                conjunto = 2;
                                            }
                                            else
                                            {
                                                if (conjunto != 2)
                                                {
                                                    return false;
                                                }

                                                fin = letrasMinusculas.IndexOf(rango[k].ElementAt(l));
                                            }
                                        }
                                        else if (numeros.Contains(rango[k].ElementAt(l)))
                                        {
                                            if (k == 0)
                                            {
                                                inicio = numeros.IndexOf(rango[k].ElementAt(l));
                                                conjunto = 3;
                                            }
                                            else
                                            {
                                                if (conjunto != 3)
                                                {
                                                    return false;
                                                }

                                                fin = numeros.IndexOf(rango[k].ElementAt(l));
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int j = 0; j < sets[i].Length; j++)
                        {
                            switch (sets[i].ElementAt(j))
                            {
                                case '\'':
                                    if (dentroComillas)
                                    {
                                        if (comillasJuntas)
                                        {
                                            elemento += '\'';
                                            comillasJuntas = false;
                                        }
                                        else
                                        {
                                            if (elemento == "")
                                                return false;

                                            dentroComillas = false;
                                            dicSet[nombreSet].AgregarElemento(elemento);
                                            elemento = "";
                                        }
                                    }
                                    else
                                    {
                                        string lineaSinEspacios = lineaTemp.Substring(i).Trim();
                                        if (lineaSinEspacios.ElementAt(1) == '\'' && lineaSinEspacios.ElementAt(2) == '\'')
                                            comillasJuntas = true;

                                        dentroComillas = true;
                                    }
                                    break;
                                case ' ':
                                    if (dentroComillas)
                                        elemento += ' ';
                                    break;
                                case 'C':
                                    if ((j + 1) == sets[i].Length)
                                    {
                                        return false;
                                    }

                                    if (sets[i].ElementAt(j + 1) != 'H')
                                    {
                                        return false;
                                    }

                                    if ((j + 2) == sets[i].Length)
                                    {
                                        return false;
                                    }

                                    if (sets[i].ElementAt(j + 2) != 'R')
                                    {
                                        return false;
                                    }

                                    if ((j + 3) == sets[i].Length)
                                    {
                                        return false;
                                    }

                                    if (sets[i].ElementAt(j + 3) != '(')
                                    {
                                        return false;
                                    }

                                    if ((j + 4) == sets[i].Length)
                                    {
                                        return false;
                                    }

                                    j = j + 4;
                                    string numero = "";
                                    while (sets[i].ElementAt(j) != ')')
                                    {
                                        if (j == (sets[i].Length - 1))
                                        {
                                            return false;
                                        }

                                        numero += sets[i].ElementAt(j);
                                        j++;
                                    }

                                    int num = 0;
                                    int.TryParse(numero, out num);

                                    if (num == 0)
                                    {
                                        return false;
                                    }

                                    if (j != sets[i].Length - 1)
                                    {
                                        return false;
                                    }

                                    string caracter = "";
                                    caracter += (char)num;
                                    dicSet[nombreSet].AgregarElemento(caracter);

                                    break;
                                default:
                                    if (!dentroComillas)
                                    {
                                        return false;
                                    }

                                    if (dentroComillas)
                                    {
                                        elemento += sets[i].ElementAt(j);
                                    }
                                    break;
                            }

                            if (dentroComillas && (j == (sets[i].Length - 1)))
                            {
                                return false;
                            }
                        }
                    }
                }

                agregarLineaProceso("Set guardado");
                return true;
            }
            else
            {
                sets = false;
                return verificarEncabezadoTokens(linea);
            }
        }

        //Verifica el encabezado del token
        bool verificarEncabezadoTokens(string linea)
        {
            string tempLinea = linea.Trim();

            if (tempLinea.Length == 6 && tempLinea.ToUpper() == "TOKENS")
            {
                tokens = true;
                contadorEncabezados++;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Verifica el token
        bool verificarToken(string linea)
        {
            if (linea.Trim() == "")
                return true;

            if (!linea.Trim().ToUpper().Contains("ACTIONS"))
            {
                string lineaTemp = linea.TrimStart();
                lineaTemp = lineaTemp.TrimEnd();
                string nombreToken = "";

                int contador = 0;
                while (lineaTemp.ElementAt(contador) != '=')
                {
                    if (contador == (lineaTemp.Length - 1))
                    {
                        return false;
                    }

                    nombreToken += lineaTemp.ElementAt(contador);
                    contador++;

                }

                nombreToken = nombreToken.Trim();

                if (contador == (lineaTemp.Length - 1))
                {
                    return false;
                }

                contador++;

                Token token = new Token();
                dicTokens.Add(nombreToken, token);

                lineaTemp = lineaTemp.Substring(contador);
                lineaTemp = lineaTemp.TrimStart();

                if (lineaTemp == "")
                {
                    return false;
                }

                string elemento = "";
                bool dentroComillas = false;
                bool comillasJuntas = false;
                int contadorParentesisAbierto = 0;
                int contadorParentesisCerrado = 0;
                for (int i = 0; i < lineaTemp.Length; i++)
                {
                    switch (lineaTemp.ElementAt(i))
                    {
                        case '\'':
                            if (dentroComillas)
                            {
                                if (comillasJuntas)
                                {
                                    elemento += '\'';
                                    comillasJuntas = false;
                                }
                                else
                                {
                                    int noEncuentra = 0;
                                    for (int m = 0; m < dicSet.Count; m++)
                                    {
                                        if (!dicSet.ElementAt(m).Value.getElementos().Contains(elemento))
                                            noEncuentra++;

                                    }

                                    if (noEncuentra == dicSet.Count)
                                        return false;

                                    token.agregarExpresion('\'' + elemento + '\'');
                                    dentroComillas = false;
                                    elemento = "";
                                }
                            }
                            else
                            {
                                string lineaSinEspacios = lineaTemp.Substring(i).Trim();
                                if (lineaSinEspacios.ElementAt(1) == '\'' && lineaSinEspacios.ElementAt(2) == '\'')
                                    comillasJuntas = true;

                                dentroComillas = true;
                            }
                            break;
                        default:
                            if (dentroComillas)
                            {
                                elemento += lineaTemp.ElementAt(i);
                            }
                            else
                            {
                                if (lineaTemp.ElementAt(i) == ' ')
                                    break;

                                if (lineaTemp.ElementAt(i) != '?' && lineaTemp.ElementAt(i) != '*' && lineaTemp.ElementAt(i) != '|'
                                    && lineaTemp.ElementAt(i) != '(' && lineaTemp.ElementAt(i) != ')' && lineaTemp.ElementAt(i) != '+')
                                {
                                    string nombreSet = "";
                                    nombreSet += lineaTemp.ElementAt(i);

                                    while (!dicSet.Keys.Contains(nombreSet))
                                    {
                                        if (i == lineaTemp.Length - 1)
                                        {
                                            return false;
                                        }

                                        if (lineaTemp.ElementAt(i + 1) == ' ' || lineaTemp.ElementAt(i + 1) == '(' || lineaTemp.ElementAt(i + 1) == ')' ||
                                            lineaTemp.ElementAt(i + 1) == '|' || lineaTemp.ElementAt(i + 1) == '*' || lineaTemp.ElementAt(i + 1) == '?' || lineaTemp.ElementAt(i + 1) == '+')
                                        {
                                            return false;
                                        }

                                        i++;
                                        nombreSet += lineaTemp.ElementAt(i);
                                    }

                                    token.agregarExpresion(nombreSet);
                                }
                                else
                                {
                                    if (i == lineaTemp.Length - 1 && lineaTemp.ElementAt(i) != '?' && lineaTemp.ElementAt(i) != '*' && lineaTemp.ElementAt(i) != ')' && lineaTemp.ElementAt(i) != '+')
                                        return false;

                                    int x = i;

                                    if (i != lineaTemp.Length - 1)
                                    {
                                        x = x + 1;
                                        while (lineaTemp.ElementAt(x) == ' ')
                                        {
                                            x++;
                                        }
                                    }

                                    switch (lineaTemp.ElementAt(i))
                                    {
                                        case '(':
                                            if (lineaTemp.ElementAt(i) == lineaTemp.Length - 1)
                                                return false;

                                            if (lineaTemp.ElementAt(x) == '|' || lineaTemp.ElementAt(x) == '*' || lineaTemp.ElementAt(x) == '?'
                                                || lineaTemp.ElementAt(x) == ')' || lineaTemp.ElementAt(x) == '+')
                                                return false;

                                            contadorParentesisAbierto++;
                                            break;
                                        case ')':
                                            contadorParentesisCerrado++;
                                            break;
                                        case '|':
                                            if (lineaTemp.ElementAt(x) == '*' || lineaTemp.ElementAt(x) == '?' || lineaTemp.ElementAt(x) == '|'
                                                || lineaTemp.ElementAt(x) == ')' || lineaTemp.ElementAt(x) == '+')
                                            {
                                                return false;
                                            }
                                            break;
                                        case '*':
                                            if (x != i)
                                            {
                                                if (lineaTemp.ElementAt(x) == '?' || lineaTemp.ElementAt(x) == '*' || lineaTemp.ElementAt(x) == '+')
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                        case '?':
                                            if (x != i)
                                            {
                                                if (lineaTemp.ElementAt(x) == '*' || lineaTemp.ElementAt(x) == '?' || lineaTemp.ElementAt(x) == '+')
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                        case '+':
                                            if (x != i)
                                            {
                                                if (lineaTemp.ElementAt(x) == '*' || lineaTemp.ElementAt(x) == '?' || lineaTemp.ElementAt(x) == '+')
                                                {
                                                    return false;
                                                }
                                            }
                                            break;
                                    }

                                    string temp = "";
                                    temp += lineaTemp.ElementAt(i);
                                    token.agregarExpresion(temp);
                                }
                            }
                            break;
                    }

                    if (dentroComillas && i == lineaTemp.Length - 1)
                        return false;

                }

                if (contadorParentesisAbierto != contadorParentesisCerrado)
                    return false;

                agregarLineaProceso("Token guardado");
                return true;
            }
            else
            {
                tokens = false;
                return verificarEncabezadoActions(linea);
            }
        }

        //Verifica el encabezado de las actions
        bool verificarEncabezadoActions(string linea)
        {
            string tempLinea = linea.Trim();

            if (tempLinea.Length == 7 && tempLinea.ToUpper() == "ACTIONS")
            {
                actions = true;
                contadorEncabezados++;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Verifica las actions
        bool empiezaBien = false;
        bool dentroCorchetes = false;
        bool verificarAction(string linea)
        {
            if (linea.Trim() == "")
                return true;

            if (!linea.Contains("ERROR"))
            {
                if (linea.Trim().Equals("RESERVADAS()"))
                {
                    empiezaBien = true;
                    return true;
                }

                if (empiezaBien == false && !linea.Trim().Equals("RESERVADAS()"))
                    return false;

                if (linea.Trim().Equals("{"))
                {
                    dentroCorchetes = true;
                    return true;
                }

                if (dentroCorchetes == false)
                    return false;

                if (linea.Trim().Equals("}"))
                {
                    if (dentroCorchetes == false)
                        return false;

                    dentroCorchetes = false;
                }

                if (dentroCorchetes)
                {
                    linea = linea.Trim();
                    string nombreAction = "";

                    int contador = 0;
                    while (linea.ElementAt(contador) != '=')
                    {
                        if (contador == (linea.Length - 1))
                        {
                            return false;
                        }

                        nombreAction += linea.ElementAt(contador);
                        contador++;

                    }

                    nombreAction = nombreAction.Trim();

                    if (contador == (linea.Length - 1))
                    {
                        return false;
                    }

                    contador++;

                    Action action = new Action(linea.Substring(contador).TrimStart().Replace("'", String.Empty));
                    dicAction.Add(nombreAction, action);
                }                

                return true;
            }
            else
            {
                actions = false;
                return verificarEncabezadoError(linea);
            }
        }

        //Verifica el error
        bool verificarEncabezadoError(string linea)
        {
            if (linea.Trim() == "ERROR = 54")
            {
                contadorEncabezados++;
                return true;                
            }
            else
            {
                return false;
            }
        }

        bool verificarError(string linea)
        {
            return true;
        }

        bool reemplazarChars(char caracter, ref string linea, string reemplazo)
        {
            try
            {
                for (int i = 0; i < linea.Length; i++)
                {
                    if (linea.ElementAt(i) == '\'')
                    {
                        i++;
                        while (linea.ElementAt(i) != '\'')
                        {
                            if (linea.ElementAt(i) == caracter)
                            {
                                string linea1 = linea.Substring(0, i);
                                string linea2 = "";

                                if (i + 1 < linea.Length)
                                {
                                    linea2 = linea.Substring(i + 1);
                                }

                                linea = linea1 + reemplazo + linea2;
                            }

                            i++;
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void generarAutomata()
        {
            agregarLineaProceso("Generando autótamata...");
            List<string> expresionRegular = agregarConcatenacion();

            string expresion = "";
            for (int i = 0; i < expresionRegular.Count; i++)
            {
                expresion += expresionRegular.ElementAt(i);
            }
            agregarLineaProceso("Expresión generada: " + expresion);
            
            expresionRegular = convertirPostfijo(expresionRegular);

            expresion = "";
            for (int i = 0; i < expresionRegular.Count; i++)
            {
                expresion += expresionRegular.ElementAt(i);
            }
            agregarLineaProceso("Expresión en postfijo: " + expresion);
            
            List<Nodo> nodos = GenerarNodos(expresionRegular);

            Dictionary<int, List<int>> follows = operarNodos(ref nodos);

            agregarLineaProceso("FOLLOWS");
            for (int i = 0; i < follows.Count; i++)
            {
                string valores = follows.Keys.ElementAt(i) + ": ";
                for (int j = 0; j < follows.ElementAt(i).Value.Count; j++)
                {
                    valores += follows.ElementAt(i).Value.ElementAt(j);

                    if (j == follows.ElementAt(i).Value.Count - 1)
                        valores += ",";
                }

                agregarLineaProceso(valores);
            }

            List<Nodo> terminales = new List<Nodo>();
            List<Nodo> nodosHoja = new List<Nodo>();

            for (int i = 0; i < nodos.Count; i++)
            {
                if (nodos.ElementAt(i).getNumero() != 0)
                {
                    int iguales = 0;
                    for (int j = 0; j < terminales.Count; j++)
                    {
                        if (terminales.ElementAt(j).getElemento().Equals(nodos.ElementAt(i).getElemento()))
                            iguales++;
                    }

                    if(iguales == 0)
                        terminales.Add(nodos.ElementAt(i));

                    nodosHoja.Add(nodos.ElementAt(i));
                }                    
            }

            for (int i = 0; i < follows.Count; i++)
            {
                follows[nodosHoja.ElementAt(i).getNumero()].Sort();
            }

            agregarLineaProceso("Realizando transiciones...");
            dicTransiciones = realizarTransiciones(terminales, nodosHoja, follows, nodos.ElementAt(nodos.Count - 1));

            string[,] tabla = generarTabla(dicTransiciones);

            frmMostrarTabla mostrar = new frmMostrarTabla(tabla, proceso, this);
            mostrar.Show();
        }

        //Junta todos los tokens para formar una sola expresion regular y añade los simbolos de concatenacion °
        List<string> agregarConcatenacion()
        {
            List<string> expresion = new List<string>();
            expresion.Add("(");

            for (int i = 0; i < dicTokens.Count; i++)
            {
                int tamañoExpresion = dicTokens[dicTokens.Keys.ElementAt(i)].getExpresion().Count;
                List<string> lista = dicTokens[dicTokens.Keys.ElementAt(i)].getExpresion();

                expresion.Add("(");
                for (int j = 0; j < tamañoExpresion; j++)
                {                    
                    expresion.Add(lista.ElementAt(j));

                    if(j != tamañoExpresion - 1)
                    {
                        if (lista.ElementAt(j + 1) != "|" && lista.ElementAt(j) != "|" && lista.ElementAt(j) != "(" && lista.ElementAt(j+1) != ")" && lista.ElementAt(j + 1) != "*"
                            && lista.ElementAt(j + 1) != "+" && lista.ElementAt(j + 1) != "?") 
                        {
                            expresion.Add("°");
                        }
                    }                    
                }
                expresion.Add(")");

                if(i != dicTokens.Count - 1)
                    expresion.Add("|");
            }

            expresion.Add(")");
            expresion.Add("°");
            expresion.Add("#");
            
            return expresion;
        }        

        //Genera el listado con los nodos
        List<Nodo> GenerarNodos(List<string> lista)
        {
            List<Nodo> nodos = new List<Nodo>();

            int contadorHojas = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i) != "|" && lista.ElementAt(i) != "*" && lista.ElementAt(i) != "?" && lista.ElementAt(i) != "+"
                    && lista.ElementAt(i) != "°")
                {
                    contadorHojas++;
                    Nodo nodo = new Nodo(lista.ElementAt(i), contadorHojas, contadorHojas, contadorHojas, false);
                    nodos.Add(nodo);
                    agregarLineaProceso("Hoja agregada: " + nodo.getElemento() + " Numero: " + nodo.getNumero());
                }
                else
                {
                    Nodo nodo = new Nodo(lista.ElementAt(i));

                    if (lista.ElementAt(i) == "*" || lista.ElementAt(i) == "?")
                        nodo.setNulabilidad(true);

                    nodos.Add(nodo);
                    agregarLineaProceso("Nodo de operador agregado: " + nodo.getElemento());
                }
            }

            return nodos;
        }

        //Opera los nodos, sus first, last y la tabla de follows
        Dictionary<int, List<int>> operarNodos(ref List<Nodo> nodos)
        {
            Dictionary<int, List<int>> follows = new Dictionary<int, List<int>>();
            Stack<Nodo> pila = new Stack<Nodo>();

            for (int i = 0; i < nodos.Count; i++)
            {
                if(nodos.ElementAt(i).getNumero() != 0)
                {
                    pila.Push(nodos.ElementAt(i));
                }
                else
                {
                    switch (nodos.ElementAt(i).getElemento())
                    {
                        case "*": case "+": case "?":
                            Nodo hijo = pila.Pop();
                            
                            for (int j = 0; j < hijo.getFirst().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getFirst().Contains(hijo.getFirst().ElementAt(j)))
                                    nodos.ElementAt(i).agregarFirst(hijo.getFirst().ElementAt(j));                                
                            }
                            
                            for (int j = 0; j < hijo.getLast().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getLast().Contains(hijo.getLast().ElementAt(j)))
                                    nodos.ElementAt(i).agregarLast(hijo.getLast().ElementAt(j));
                            }
                            
                            for (int j = 0; j < hijo.getLast().Count; j++)
                            {
                                for (int k = 0; k < hijo.getFirst().Count; k++)
                                {
                                    if (!follows.ContainsKey(hijo.getLast().ElementAt(j)))
                                        follows.Add(hijo.getLast().ElementAt(j), new List<int>());

                                    if (!follows[hijo.getLast().ElementAt(j)].Contains(hijo.getFirst().ElementAt(k)))
                                    {
                                        follows[hijo.getLast().ElementAt(j)].Add(hijo.getFirst().ElementAt(k));
                                        agregarLineaProceso("Follow actualizado: " + hijo.getLast().ElementAt(j) + ": " + hijo.getFirst().ElementAt(k));
                                    }                                        
                                }                                
                            }

                            if(nodos.ElementAt(i).getElemento() == "+")
                            {
                                if (hijo.getNulabilidad())
                                    nodos.ElementAt(i).setNulabilidad(true);
                            }

                            pila.Push(nodos.ElementAt(i));
                            break;                                                   
                        case "|":
                            Nodo hijo2 = pila.Pop();
                            Nodo hijo1 = pila.Pop();

                            if (hijo1.getNulabilidad() || hijo2.getNulabilidad())
                                nodos.ElementAt(i).setNulabilidad(true);


                            for (int j = 0; j < hijo1.getFirst().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getFirst().Contains(hijo1.getFirst().ElementAt(j)))
                                    nodos.ElementAt(i).agregarFirst(hijo1.getFirst().ElementAt(j));
                            }

                            for (int j = 0; j < hijo2.getFirst().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getFirst().Contains(hijo2.getFirst().ElementAt(j)))
                                    nodos.ElementAt(i).agregarFirst(hijo2.getFirst().ElementAt(j));
                            }



                            for (int j = 0; j < hijo1.getLast().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getLast().Contains(hijo1.getLast().ElementAt(j)))
                                    nodos.ElementAt(i).agregarLast(hijo1.getLast().ElementAt(j));
                            }

                            for (int j = 0; j < hijo2.getLast().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getLast().Contains(hijo2.getLast().ElementAt(j)))
                                    nodos.ElementAt(i).agregarLast(hijo2.getLast().ElementAt(j));
                            }

                            pila.Push(nodos.ElementAt(i));
                            break;
                        case "°":
                            Nodo hijo2a = pila.Pop();
                            Nodo hijo1a = pila.Pop();

                            if (hijo1a.getNulabilidad() && hijo2a.getNulabilidad())
                                nodos.ElementAt(i).setNulabilidad(true);

                            for (int j = 0; j < hijo1a.getFirst().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getFirst().Contains(hijo1a.getFirst().ElementAt(j)))
                                    nodos.ElementAt(i).agregarFirst(hijo1a.getFirst().ElementAt(j));
                            }

                            if (hijo1a.getNulabilidad())
                            {
                                for (int j = 0; j < hijo2a.getFirst().Count; j++)
                                {
                                    if (!nodos.ElementAt(i).getFirst().Contains(hijo2a.getFirst().ElementAt(j)))
                                        nodos.ElementAt(i).agregarFirst(hijo2a.getFirst().ElementAt(j));
                                }
                            }

                            if (hijo2a.getNulabilidad())
                            {
                                for (int j = 0; j < hijo1a.getLast().Count; j++)
                                {
                                    if (!nodos.ElementAt(i).getLast().Contains(hijo1a.getLast().ElementAt(j)))
                                        nodos.ElementAt(i).agregarLast(hijo1a.getLast().ElementAt(j));
                                }
                            }

                            for (int j = 0; j < hijo2a.getLast().Count; j++)
                            {
                                if (!nodos.ElementAt(i).getLast().Contains(hijo2a.getLast().ElementAt(j)))
                                    nodos.ElementAt(i).agregarLast(hijo2a.getLast().ElementAt(j));
                            }

                            for (int j = 0; j < hijo1a.getLast().Count; j++)
                            {
                                for (int k = 0; k < hijo2a.getFirst().Count; k++)
                                {
                                    if (!follows.ContainsKey(hijo1a.getLast().ElementAt(j)))
                                        follows.Add(hijo1a.getLast().ElementAt(j), new List<int>());

                                    if (!follows[hijo1a.getLast().ElementAt(j)].Contains(hijo2a.getFirst().ElementAt(k)))
                                    {
                                        follows[hijo1a.getLast().ElementAt(j)].Add(hijo2a.getFirst().ElementAt(k));
                                        agregarLineaProceso("Follow actualizado: " + hijo1a.getLast().ElementAt(j) + ": " + hijo2a.getFirst().ElementAt(k));
                                    }                                        
                                }
                            }

                            pila.Push(nodos.ElementAt(i));
                            break;                        
                    }
                }
            }            

            return follows;
        }

        //Convierte la expresion a postfijo
        List<string> convertirPostfijo(List<string> lista)
        {
            Stack<string> operadores = new Stack<string>();
            List<string> salida = new List<string>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i) != "|" && lista.ElementAt(i) != "*" && lista.ElementAt(i) != "?" && lista.ElementAt(i) != "+"
                    && lista.ElementAt(i) != "°" && lista.ElementAt(i) != "(" && lista.ElementAt(i) != ")")
                {
                    salida.Add(lista.ElementAt(i));
                }
                else if (lista.ElementAt(i) == "(")
                {
                    operadores.Push(lista.ElementAt(i));
                }
                else if (lista.ElementAt(i) == ")")
                {
                    while (operadores.Peek() != "(" && operadores.Count != 0)
                    {
                        salida.Add(operadores.Pop());
                    }

                    if (operadores.Peek() == "(")
                    {
                        operadores.Pop();
                    }
                }
                else
                {
                    if (operadores.Count != 0)
                    {
                        while (operadores.Peek() != "(" && operadores.Count != 0)
                        {
                            if (VerJerarquia(lista.ElementAt(i)) <= VerJerarquia(operadores.Peek()))
                                salida.Add(operadores.Pop());
                            else
                                break;
                        }
                    }

                    operadores.Push(lista.ElementAt(i));
                }
            }

            while (operadores.Count != 0)
            {
                salida.Add(operadores.Pop());
            }

            return salida;
        }

        int VerJerarquia(string elemento)
        {
            switch (elemento)
            {
                case "*":
                    return 2;
                case "+":
                    return 2;
                case "?":
                    return 2;
                case "|":
                    return 1;
                case "°":
                    return 1;
                default:
                    return 0;
            }
        }

        Dictionary<Estado, Dictionary<string, Estado>> realizarTransiciones(List<Nodo> terminales, List<Nodo> nodosHoja, Dictionary<int, List<int>> follows, Nodo raiz)
        {
            Dictionary<Estado, Dictionary<string, Estado>> transiciones = new Dictionary<Estado, Dictionary<string, Estado>>();
            
            Estado estadoActual = new Estado(raiz.getFirst(), "Estado 1");
            agregarLineaProceso("Estado inicial: Estado 1(" + estadoActual.getElementosString() + ")");
            if (estadoActual.getLista().Contains(nodosHoja.ElementAt(nodosHoja.Count - 1).getNumero()))
                estadoActual.setEstadoAceptacion(true);

            List<Estado> estados = new List<Estado>();
            estados.Add(estadoActual);
            int contadorEstados = 0;

            bool terminado = false;
            while (!terminado)
            {
                transiciones.Add(estadoActual, new Dictionary<string, Estado>());

                for (int i = 0; i < terminales.Count - 1; i++)
                {
                    List<int> listaTemporal = new List<int>();

                    for (int j = 0; j < estadoActual.getLista().Count; j++)
                    {
                        if (terminales.ElementAt(i).getElemento() == nodosHoja.ElementAt(estadoActual.getLista().ElementAt(j) - 1).getElemento())
                        {
                            if (!transiciones[estadoActual].ContainsKey(terminales.ElementAt(i).getElemento()))
                            {
                                transiciones[estadoActual].Add(terminales.ElementAt(i).getElemento(), new Estado());
                            }

                            
                            for (int k = 0; k < follows[nodosHoja.ElementAt(estadoActual.getLista().ElementAt(j) - 1).getNumero()].Count; k++)
                            {
                                if(!listaTemporal.Contains(follows[nodosHoja.ElementAt(estadoActual.getLista().ElementAt(j) - 1).getNumero()].ElementAt(k)))
                                {
                                    listaTemporal.Add(follows[nodosHoja.ElementAt(estadoActual.getLista().ElementAt(j) - 1).getNumero()].ElementAt(k));
                                }
                            }                            
                        }
                    }

                    if(listaTemporal.Count > 0)
                    {
                        listaTemporal.Sort();
                        string listaNueva = Estado.generarRetornarString(listaTemporal);

                        int contDiferente = 0;
                        Estado estadoNuevo = null;
                        for (int j = 0; j < estados.Count; j++)
                        {
                            if (!estados.ElementAt(j).getElementosString().Equals(listaNueva))
                            {
                                contDiferente++;
                            }
                            else
                            {
                                estadoNuevo = estados.ElementAt(j);
                            }
                        }

                        if (contDiferente == estados.Count)
                        {
                            estadoNuevo = new Estado(listaTemporal, "Estado " + (estados.Count + 1));
                            estados.Add(estadoNuevo);
                            agregarLineaProceso("Estado nuevo: " + estadoNuevo.getNombre() + "(" + estadoNuevo.getElementosString() + ")");
                        }

                        transiciones[estadoActual][terminales.ElementAt(i).getElemento()] = estadoNuevo;
                    }
                    else
                    {
                        transiciones[estadoActual][terminales.ElementAt(i).getElemento()] = null;
                    }                    
                }

                contadorEstados++;
                if (contadorEstados != estados.Count)
                {
                    estadoActual = estados.ElementAt(contadorEstados);
                    if (estadoActual.getLista().Contains(nodosHoja.ElementAt(nodosHoja.Count - 1).getNumero()))
                        estadoActual.setEstadoAceptacion(true);
                }
                else
                {
                    terminado = true;
                }
            }


            return transiciones;
        }    
        
        string[,] generarTabla(Dictionary<Estado, Dictionary<string, Estado>> transiciones)
        {
            int filas = transiciones.Count;
            int columnas = transiciones.ElementAt(0).Value.Count;
            
            string[,] matriz = new string[filas + 1, columnas + 1];            

            for (int i = 0; i < filas + 1; i++)
            {
                if(i == 0)
                {
                    matriz[i, 0] = "Estados";
                }
                else
                {                    
                    matriz[i, 0] = transiciones.ElementAt(i-1).Key.getNombre() + "(" + transiciones.ElementAt(i - 1).Key.getElementosString() + ")";
                    if (transiciones.ElementAt(i - 1).Key.getEstadoAceptacion())
                        matriz[i, 0] += "#";

                    if (i == 1)
                        matriz[i, 0] += "INICIAL";
                }                
            }

            for (int i = 1; i < transiciones.ElementAt(0).Value.Count + 1; i++)
            {
                matriz[0, i] = transiciones.ElementAt(0).Value.ElementAt(i - 1).Key;
            }

            for (int i = 1; i < transiciones.Keys.Count + 1; i++)
            {
                for (int j = 1; j < transiciones.ElementAt(0).Value.Count + 1; j++)
                {                    
                    if(transiciones.ElementAt(i - 1).Value.ElementAt(j - 1).Value != null)
                    {
                        matriz[i, j] = transiciones.ElementAt(i - 1).Value.ElementAt(j - 1).Value.getNombre();
                    }
                    else
                    {
                        matriz[i, j] = "---";
                    }
                    
                }
            }

            return matriz;
        }

        void agregarLineaProceso(string linea)
        {            
            proceso += linea + Environment.NewLine;
            
        }
    }
}
