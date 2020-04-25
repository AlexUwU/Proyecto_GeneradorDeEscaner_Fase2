using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1Lenguajes
{
    public partial class frmMostrarTabla : Form
    {
        string[,] tabla;
        string proceso;
        Scanner scanner;

        public frmMostrarTabla(string[,] tabla, string proceso, Scanner obj)
        {
            InitializeComponent();
            this.tabla = tabla;
            this.proceso = proceso;
            scanner = obj;
        }

        private void frmMostrarTabla_Load(object sender, EventArgs e)
        {
            dgvAutomata.RowCount = tabla.GetLength(0) - 1;
            dgvAutomata.ColumnCount = tabla.GetLength(1) - 1;

            for (int i = 1; i < tabla.GetLength(0); i++)
            {
                dgvAutomata.Rows[i - 1].HeaderCell.Value = tabla[i, 0];
                dgvAutomata.Rows[i - 1].HeaderCell.Style.Font = new Font(dgvAutomata.Font, FontStyle.Bold);
                dgvAutomata.Rows[i - 1].HeaderCell.Style.ForeColor = Color.Green;
            }            

            for (int i = 1; i < tabla.GetLength(1); i++)
            {
                dgvAutomata.Columns[i - 1].HeaderCell.Value = tabla[0, i];
                dgvAutomata.Columns[i - 1].HeaderCell.Style.Font = new Font(dgvAutomata.Font, FontStyle.Bold);                
            }

            for (int i = 1; i < tabla.GetLength(0); i++)
            {
                for (int j = 1; j < tabla.GetLength(1); j++)
                {
                    dgvAutomata.Rows[i - 1].Cells[j - 1].Value = tabla[i, j];
                }
            }            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMostrarProceso_Click(object sender, EventArgs e)
        {
            frmProceso verProceso = new frmProceso(proceso);
            verProceso.Show();
        }

        string rutaArchivo;
        private void btnGenerarApp_Click(object sender, EventArgs e)
        {
            string codigo = escribirGeneral();

            FolderBrowserDialog Explorador = new FolderBrowserDialog();
            Explorador.ShowDialog();
            rutaArchivo = Explorador.SelectedPath + "\\programa.cs";

            if (rutaArchivo == "")
            {
                MessageBox.Show("No ha seleccionado ningun archivo");
            }
            else
            {
                Escritor.escribir(codigo, rutaArchivo);
                CompileExecutable(rutaArchivo);
            }            
        }

        string escribirGeneral()
        {
            string codigoC = "";

            codigoC += "using System;" + Environment.NewLine + "using System.IO;" + Environment.NewLine + "using System.Collections.Generic;" + Environment.NewLine + "using System.Linq;" + Environment.NewLine;
            codigoC += "class Program{" + Environment.NewLine;            
            codigoC += "static void Main(string[] args){" + Environment.NewLine;
            codigoC += "try{" + Environment.NewLine;
            codigoC += escribirCrearListas();
            codigoC += "bool salir = false;" + Environment.NewLine + "while(!salir){" + Environment.NewLine;
            codigoC += "Console.WriteLine(\"Ingrese la ruta del archivo a evaluar\");" + Environment.NewLine + "string ruta = Console.ReadLine();" + Environment.NewLine;
            codigoC += "string contenido = leerArchivo(ruta);" + Environment.NewLine;
            codigoC += "contenido = contenido.Replace('\\t', ' ');" + Environment.NewLine;
            codigoC += "contenido = contenido.Replace('\\n', ' ');" + Environment.NewLine;
            codigoC += "string[] elementosCadena = contenido.Split(' ');" + Environment.NewLine;
            codigoC += "string[] elementosSalida = new string[elementosCadena.Length];" + Environment.NewLine;
            codigoC += escribirEliminarReservadas();
            codigoC += escribirSwitch();
            codigoC += "if (Error)" + Environment.NewLine +
            "{" + Environment.NewLine +
                "Console.WriteLine(\"La cadena ingresada no es correcta\");" + Environment.NewLine +
            "}" + Environment.NewLine +
            "else" + Environment.NewLine +
            "{" + Environment.NewLine +
                "Console.WriteLine(\"La cadena ingresada es correcta\");" + Environment.NewLine;
            //Escribir archivo de salida
            codigoC += "Console.WriteLine(\"Ingrese la ruta del archivo de salida\");";
            codigoC += "string rutaSalida = Console.ReadLine();";
            codigoC += "escribir(elementosSalida, rutaSalida);";
            codigoC += "}" + Environment.NewLine;

            codigoC += "Console.WriteLine(\"¿Desea evaluar otro archivo? (SI O NO)\");" + Environment.NewLine + "if (Console.ReadLine().ToUpper() == \"NO\"){" + Environment.NewLine + "salir = true;" + Environment.NewLine + "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;
            codigoC += "catch (Exception e){" + Environment.NewLine;
            codigoC += "Console.WriteLine(\"Error al verificar el archivo\");" + Environment.NewLine; ;
            codigoC += "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;            
            codigoC += escribirLeerArchivo();
            codigoC += escribirEscribirArchivo();

            codigoC += "}" + Environment.NewLine;

            return codigoC;
        }        

        string escribirCrearListas()
        {
            string codigoC = "";            

            codigoC += "Dictionary<string, string> actions = new Dictionary<string, string>();" + Environment.NewLine;
            for (int i = 0; i < scanner.dicAction.Count; i++)
            {
                codigoC += "actions.Add(\"" + scanner.dicAction.Keys.ElementAt(i) + "\",\"" + scanner.dicAction[scanner.dicAction.Keys.ElementAt(i)].getString().Trim() + "\");" + Environment.NewLine;
            }

            codigoC += "Dictionary<string, string> tokens = new Dictionary<string, string>();" + Environment.NewLine;
            for (int i = 0; i < scanner.dicTokens.Count; i++)
            {
                codigoC += "tokens.Add(\"" + scanner.dicTokens.Keys.ElementAt(i) + "\",\"" + scanner.dicTokens[scanner.dicTokens.Keys.ElementAt(i)].listaToString() + "\");" + Environment.NewLine;
            }

            codigoC += "Dictionary<string, string> sets = new Dictionary<string, string>();" + Environment.NewLine;
            for (int i = 0; i < scanner.dicSet.Count; i++)
            {
                codigoC += "sets.Add(\"" + scanner.dicSet.Keys.ElementAt(i) + "\",\"" + scanner.dicSet[scanner.dicSet.Keys.ElementAt(i)].listaToString() + "\");" + Environment.NewLine;
            }

            return codigoC;
        }

        string escribirLeerArchivo()
        {
            string codigoC = "";

            codigoC += "static string leerArchivo(string archivo){" + Environment.NewLine +
                "StreamReader lector = new StreamReader(archivo);" + Environment.NewLine +
                "string contenido = \"\";" + Environment.NewLine +
                "string linea;" + Environment.NewLine +
                "while ((linea = lector.ReadLine()) != null){" + Environment.NewLine +
                "contenido += linea;" + Environment.NewLine +
                "}" + Environment.NewLine +
                "lector.Close();" + Environment.NewLine +
                "return contenido;" + Environment.NewLine +
                "}" + Environment.NewLine;

            return codigoC;
        }

        string escribirEscribirArchivo()
        {
            string codigoC = "";            
            codigoC += "static void escribir(string[] contenido, string ruta){" + Environment.NewLine +"FileInfo archivo = new FileInfo(ruta);if (archivo.Exists){archivo.Delete();}" + Environment.NewLine +
            "StreamWriter escritor = new StreamWriter(ruta, false);for (int i = 0; i < contenido.Length; i++){escritor.WriteLine(contenido[i]);}escritor.Close();}";

            return codigoC;
        }
        
        string escribirEliminarReservadas()
        {
            string codigoC = "";
            
            codigoC += "for (int i = 0; i < elementosCadena.Length; i++){" + Environment.NewLine +
                "if (actions.ContainsValue(elementosCadena[i].Trim())){" + Environment.NewLine +
                "string key = \"\";" + Environment.NewLine +
                "for (int j = 0; j < actions.Count; j++){" + Environment.NewLine +
                "if (actions[actions.Keys.ElementAt(j)].Equals(elementosCadena[i].Trim())){" + Environment.NewLine +
                "key = actions.Keys.ElementAt(j);" + Environment.NewLine +
                "break;" + Environment.NewLine +
                "}" + Environment.NewLine +
                "}" + Environment.NewLine +
                "elementosSalida[i] = elementosCadena[i] + \" = \" + key;" + Environment.NewLine +
                "elementosCadena[i] = \"\";" + Environment.NewLine +
                "}" + Environment.NewLine +
                "}" + Environment.NewLine;
            codigoC += "bool Error = false;" + Environment.NewLine;

            return codigoC;
        }
        
        string escribirSwitch()
        {
            string codigoC = "";

            codigoC += "for (int h = 0; h < elementosCadena.Length; h++)" + Environment.NewLine;
            codigoC += "{" + Environment.NewLine;
            codigoC += "string Estado = \"Estado 1\";" + Environment.NewLine;
            codigoC += "Error = false;" + Environment.NewLine;

            codigoC += "if (elementosCadena[h] != \"\")" + Environment.NewLine;
            codigoC += "{" + Environment.NewLine;
            codigoC += "for (int z = 0; z < elementosCadena[h].Length; z++)" + Environment.NewLine;
            codigoC += "{" + Environment.NewLine;
            codigoC += "char car = elementosCadena[h].ElementAt(z);" + Environment.NewLine;

                        codigoC += "switch(Estado){" + Environment.NewLine;

                        for (int i = 0; i < scanner.dicTransiciones.Count; i++)
                        {
                            codigoC += "case \"" + scanner.dicTransiciones.Keys.ElementAt(i).getNombre() + "\":" + Environment.NewLine;

                            int contadorIfsHechos = 0;
                            for (int j = 0; j < scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(0)].Count; j++)
                            {
                                if (scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)][scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j)] != null)
                                {
                                    if (contadorIfsHechos != 0)
                                        codigoC += "else ";

                                    if (scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j).StartsWith("'")
                                    && scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j).EndsWith("'"))
                                    {
                                        if (scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j) == "'''")
                                        {
                                            codigoC += "if(car == " + "'\\''" + "){" + Environment.NewLine;
                                        }
                                        else if (scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j) == "'\"'")
                                        {   
                                            codigoC += "if(car == " + "'\"'" + "){" + Environment.NewLine;
                                        }
                                        else
                                        {
                                            codigoC += "if(car == " + scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j) + "){" + Environment.NewLine;
                                        }                                
                                    }
                                    else
                                    {
                                        codigoC += "if(";
                                        String nombreSet = "";
                                        for (int k = 0; k < scanner.dicSet.Count; k++)
                                        {
                                            if (scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j).Equals(scanner.dicSet.Keys.ElementAt(k)))
                                            {
                                                nombreSet = scanner.dicSet.Keys.ElementAt(k);
                                                break;
                                            }
                                        }

                                        for (int k = 0; k < scanner.dicSet[nombreSet].getElementos().Count; k++)
                                        {
                                            codigoC += "car == " + "'" + scanner.dicSet[nombreSet].getElementos().ElementAt(k) + "' ";

                                            if (k != scanner.dicSet[nombreSet].getElementos().Count - 1)
                                                codigoC += "|| ";
                                        }

                                        codigoC += "){" + Environment.NewLine;
                                    }

                                    codigoC += "Estado = " + "\"" +
                                            scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)][scanner.dicTransiciones[scanner.dicTransiciones.Keys.ElementAt(i)].Keys.ElementAt(j)].getNombre() + "\";";
                                    codigoC += Environment.NewLine + "}" + Environment.NewLine;

                                    contadorIfsHechos++;
                                }
                            }

                            if (contadorIfsHechos != 0)
                                codigoC += "else" + Environment.NewLine + "{" + Environment.NewLine;

                            codigoC += "Error = true;" + Environment.NewLine;

                            if (contadorIfsHechos != 0)
                                codigoC += "}" + Environment.NewLine;

                            codigoC += "break;" + Environment.NewLine;
                        }

                        codigoC += "}" + Environment.NewLine;

            //termina
            codigoC += "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;

            //Escribir switch para tokens
            codigoC += "if(Error){" + Environment.NewLine + 
                "break;" + "} else {" + Environment.NewLine;
            codigoC += "if(elementosCadena[h] != \"\"){" + Environment.NewLine;
            codigoC += "string[] separatingStrings = { \"$°$\"};for (int y = 0; y < tokens.Count; y++){bool encontrado = false;string[] elementosToken = tokens[tokens.Keys.ElementAt(y)].Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);string a = \"\";a += elementosCadena[h].ElementAt(0);if (sets.ContainsKey(elementosToken.ElementAt(0))){string[] elementosSet = sets[elementosToken.ElementAt(0)].Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);for (int t = 0; t < elementosSet.Length; t++){if (elementosSet[t] == a){elementosSalida[h] = elementosCadena[h] + \" = \" + tokens.Keys.ElementAt(y);encontrado = true;break;}}}else{if (a == elementosToken[0]){elementosSalida[h] = elementosCadena[h] + \" = \" + tokens.Keys.ElementAt(y);encontrado = true;}}if (encontrado){break;}}";
            codigoC += "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;
            codigoC += "}" + Environment.NewLine;

            return codigoC;
        }

        private static bool CompileExecutable(string sourceName)
        {
            FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOk = false;

            // Select the code provider based on the input file extension. 
            if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".VB")
            {
                provider = CodeDomProvider.CreateProvider("VisualBasic");
            }
            else
            {
                MessageBox.Show("Source file must have a .cs or .vb extension");
            }

            if (provider != null)
            {

                // Format the executable file name. 
                // Build the output assembly path using the current directory 
                // and <source>_cs.exe or <source>_vb.exe.

                String exeName = String.Format(@"{0}\{1}.exe",
                    System.Environment.CurrentDirectory,
                    sourceFile.Name.Replace(".", "_"));

                CompilerParameters cp = new CompilerParameters();

                // Generate an executable instead of  
                // a class library.
                cp.GenerateExecutable = true;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = exeName;

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                cp.ReferencedAssemblies.Add("System.Core.dll");

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceName);

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    MessageBox.Show("Errors building" + sourceName + "into" + cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        MessageBox.Show(ce.ToString());                        
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    MessageBox.Show("Source" + sourceName + "built into" + cr.PathToAssembly + "successfully.");                        
                }

                // Return the results of the compilation. 
                if (cr.Errors.Count > 0)
                {
                    compileOk = false;
                }
                else
                {
                    compileOk = true;
                }
            }
            return compileOk;
        }

        private void btnCompilar_Click(object sender, EventArgs e)
        {
            OpenFileDialog Explorador = new OpenFileDialog();
            Explorador.ShowDialog();
            CompileExecutable(Explorador.FileName);
        }
    }
}
