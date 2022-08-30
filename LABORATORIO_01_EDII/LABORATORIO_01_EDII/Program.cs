using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LABORATORIO_01_EDII.Model;
using Arbol;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;

namespace LABORATORIO_01_EDII
{
    class Program
    {
        //private static string _path = @"C:\Users\Usuario\Documents\Trabajador.json";
        public static AVLTree<Trabajador> trabajador = new AVLTree<Trabajador>();
        static void Main(string[] args)
        {
            int con = 0;
            do
            {
                int opcion = 0;
                Console.WriteLine("Seleccione la opción que quiera realizar");
                Console.WriteLine("1. Subir Archivo");
                Console.WriteLine("2. Agregar Manual");
                Console.WriteLine("3. Editar Manual");
                Console.WriteLine("4. Eliminar Manual");
                Console.WriteLine("5. Busqueda");
                Console.WriteLine("A la hora de ingresar datos siga el siguiente formato "+'"'+'"'+"dato"+'"'+'"');
                opcion = Convert.ToInt32(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        string ruta = "";
                        Console.WriteLine("Ingrese la ruta del archivo: ");
                        ruta = Console.ReadLine();
                        var reader = new StreamReader(File.OpenRead(ruta));
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var value = line.Split(';');
                            if (value[0] == "INSERT")
                            {
                                var data = JsonConvert.DeserializeObject<Trabajador>(value[1]);
                                Trabajador trabajar = data;
                                trabajador.insert(trabajar, ComparacioDPI);

                            }
                            else if (value[0] == "PATCH")
                            {
                                var data = JsonConvert.DeserializeObject<Trabajador>(value[1]);
                                Trabajador trabajar = data;
                                if (trabajador.Search(trabajar, ComparacioDPI).name == trabajar.name)
                                {
                                    if (trabajar.dateBirth != null)
                                    {
                                        trabajador.Search(trabajar, ComparacioDPI).dateBirth = trabajar.dateBirth;
                                    }
                                    if(trabajar.address != null)
                                    {
                                        trabajador.Search(trabajar, ComparacioDPI).address = trabajar.address;
                                    }
                    
                                }
                            }
                            else if (value[0] == "DELETE")
                            {
                                var data = JsonConvert.DeserializeObject<Trabajador>(value[1]);
                                Trabajador trabajar = data;
                                List<Trabajador> trabajo = trabajador.getAll();
                                int cant = trabajo.Count();
                                for (int i = 0; i < trabajo.Count; i++)
                                {
                                    if (trabajo[i].name == trabajar.name && trabajo[i].dpi == trabajar.dpi)
                                    {
                                        trabajo.RemoveAt(i);
                                    }
                                }
                                trabajador = new AVLTree<Trabajador>();
                                int cant2 = trabajo.Count();
                                for (int j = 0; j < trabajo.Count; j++)
                                {
                                    trabajador.insert(trabajo[j], ComparacioDPI);
                                }
                            }
                        }
                        Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                        con = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 2:
                        Trabajador trabaja = new Trabajador();
                        Console.WriteLine("Ingrese el nombre del solicitante");
                        trabaja.name = Console.ReadLine();
                        Console.WriteLine("Ingrese el dpi del solicitante");
                        trabaja.dpi = Console.ReadLine();
                        Console.WriteLine("Ingrese la fecha de nacimiento del solicitante");
                        trabaja.dateBirth = Console.ReadLine();
                        Console.WriteLine("Ingrese la dirección del solicitante");
                        trabaja.address = Console.ReadLine();
                        trabajador.insert(trabaja, ComparacionNombre);
                        Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                        con = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 3:
                        Trabajador trabaja2 = new Trabajador();
                        trabajador.Sort(ComparacioDPI);
                        string nombre;
                        string editar;
                        Console.WriteLine("Ingrese el nombre del solicitante");
                        nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el dpi del solicitante");
                        trabaja2.dpi = Console.ReadLine();
                        Console.WriteLine("¿Que desea editar?");
                        editar = Console.ReadLine();
                        if(editar == "Nacimiento") 
                        {
                            Console.WriteLine("Ingrese la fecha de nacimiento del solicitante");
                            if (trabajador.Search(trabaja2, ComparacioDPI).name == nombre)
                            {
                                trabajador.Search(trabaja2, ComparacioDPI).dateBirth = Console.ReadLine();
                            }
                            else 
                            {
                                Console.WriteLine("Solictante no encontrado");
                                Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                                con = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                break;
                            }
                        }
                        else if(editar == "Direccion") 
                        {
                            Console.WriteLine("Ingrese direccion del solicitante");
                            if (trabajador.Search(trabaja2, ComparacioDPI).name == nombre)
                            {
                                trabajador.Search(trabaja2, ComparacioDPI).address = Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Solictante no encontrado");
                                Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                                con = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                break;
                            }
                        }
                        else 
                        {
                            Console.WriteLine("Atributo no valido");
                            Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                            con = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            break;
                        }
                        Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                        con = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 4:
                        string nombre2;
                        Trabajador solic = new Trabajador();
                        Console.WriteLine("Ingrese el nombre del solicitante");
                        nombre2 = Console.ReadLine();
                        Console.WriteLine("Ingrese el dpi del solicitante");
                        solic.dpi= Console.ReadLine();
                        if(trabajador.Search(solic, ComparacioDPI)== null || trabajador.Search(solic, ComparacioDPI).name != nombre2) 
                        {
                            Console.WriteLine("Solictante no encontrado");
                            Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                            con = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            break;
                        }
                        List<Trabajador> soli = trabajador.getAll();
                        for (int i = 0; i < soli.Count; i++) 
                        {
                            if(soli[i].name == nombre2 && soli[i].dpi == solic.dpi) 
                            {
                                soli.RemoveAt(i);
                            }
                        }
                        trabajador = new AVLTree<Trabajador>();
                        for(int j = 0; j < soli.Count; j++) 
                        {
                            trabajador.insert(soli[j], ComparacioDPI);
                        }
                        Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                        con = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        break;
                    case 5:
                        string ident;
                        string path;
                        Console.WriteLine("Ingrese el nombre a buscar");
                        ident = Console.ReadLine();
                        trabajador.Sort(ComparacioDPI);
                        List<Trabajador> total = trabajador.getAll();
                        List<Trabajador> result = new List<Trabajador>();
                        int tot = total.Count;
                        for(int i = 0; i < tot; i++) 
                        {
                            if(total[i].name == ident) 
                            {
                                result.Add(total[i]);
                            }
                        }
                        if(result.Count <= 0) 
                        {
                            Console.WriteLine("No hay coincidencias");
                            Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                            con = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        Console.WriteLine("Ingrese la ruta y el nombre de como se guardar como .json");
                        path = Console.ReadLine();
                        Serializacion(result,path);
                        Console.WriteLine("Desea volver al menu No = 0, Si = 1");
                        con = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        break;
                }
            } while (con == 1);
        }
        public static void Serializacion(List<Trabajador> Lista, string path) 
        {
            string solictanteJson = JsonConvert.SerializeObject(Lista.ToArray(),Formatting.Indented);
            File.WriteAllText(path, solictanteJson);
        }
        public static bool ComparacionNombre(Trabajador paciente, string operador, Trabajador paciente2)
        {
            int Comparacion = String.Compare(paciente.name, paciente2.name);
            if (operador == "<")
            {
                return Comparacion < 0;
            }
            else if (operador == ">")
            {
                return Comparacion > 0;
            }
            else if (operador == "==")
            {
                return Comparacion == 0;
            }
            else return false;
        }
        public static bool ComparacioDPI(Trabajador paciente, string operador, Trabajador paciente2)
        {
            int Comparacion = String.Compare(paciente.dpi, paciente2.dpi);
            if (operador == "<")
            {
                return Comparacion < 0;
            }
            else if (operador == ">")
            {
                return Comparacion > 0;
            }
            else if (operador == "==")
            {
                return Comparacion == 0;
            }
            else return false;
        }
    }
}
