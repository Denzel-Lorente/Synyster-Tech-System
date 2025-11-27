using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Synyster_Tech_System
{
    internal class Program
    {
        /*public struct Servicio
        {
            public string Cliente;
            public string TipoServicio;
            public string Descripcion;
            public DateTime Fecha;
        }*/
        static List<string> listaClientes = new List<string>();
        static string[,] Clientes = new string[100, 5];
        static string[,] Servicios = new string[50, 4];
        static string[] Productos = new string[50];
        static double[] Precios = new double[50];
        static int ContadorProductos = 0;
        static int ContadorClientes = 0;
        static int ContadorServicios = 0;
        static string[] ListaServiciosDisponibles = 
        {
        "Mantenimiento Preventivo",
        "Reparación de PC / Laptop",
        "instalación de Cámaras",
        "Soporte Técnico Remoto",
        "Optimización / Instalación de Software"
        };
        

        static void Main(string[] args)
        {
            Console.Title = "Bienvenido a Synyster Tech System";
            Console.Clear();
            MenuPrincipal();
            /*RegistrarCliente();
            VerClientes();
            BuscarCliente();*/
        }

        static void MenuPrincipal()
        {
            String Opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n========= SYNYSTER TECH SYSTEM =========");
                Console.WriteLine("\n\n----- Clientes -----\n");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Ver lista de clientes");
                Console.WriteLine("3. Buscar cliente mediante cédula");
                Console.WriteLine("\n\n----- Servicios -----\n");
                Console.WriteLine("4. Registrar servicio");
                Console.WriteLine("5. Gestión de servicios");
                Console.WriteLine("\n\n----- Productos -----\n");
                Console.WriteLine("6. Gestión de productos");
                Console.WriteLine("7. Salir");
                Console.WriteLine("\n========================================");
                Console.Write("\nIngrese una opción: ");

                Opcion = Console.ReadLine().ToUpper();

                switch (Opcion)
                {
                    case "1": RegistrarCliente(); break;
                    case "2": VerClientes(); break;
                    case "3": BuscarCliente(); break;
                    case "4": RegistrarServicio(); break;
                    case "5": MenuServicios(); break;
                    case "6": MenuProductos(); break;
                    case "7": Console.WriteLine("Salindo del sistema... ¡Gracias por usar Synyster Tech System!");
                        Environment.Exit(0);
                        break;
                    default: Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
            while (true);
        }

        static void RegistrarCliente()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar Cliente ===");

            if (ContadorClientes >= Clientes.GetLength(0))
            {
                Console.WriteLine("No se pueden registrar más clientes (ímite alcanzado).");
                Console.ReadKey();
                return;
            }

            string Nombres;
            do
            {
                Console.Write("Nombres: ");
                Nombres = Console.ReadLine();
                if (string.IsNullOrEmpty(Nombres))
                {
                    Console.WriteLine("El nombre no puede estar vacío.");
                }
            }
            while (string.IsNullOrEmpty(Nombres));

            string Apellidos;
            do
            {
                Console.Write("Apellidos: ");
                Apellidos = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(Apellidos))
                {
                    Console.WriteLine("El apellido no puede estar vacío.");
                }
            } while (string.IsNullOrEmpty(Apellidos));

            string NumCedula;
            do
            {
                Console.Write("Número de cédula (sin espacios y agregue la letra del final): ");
                NumCedula = Console.ReadLine()?.Trim().ToUpper();
                if (string.IsNullOrEmpty(NumCedula))
                {
                    Console.WriteLine("El número de cédula no puede estar vacío.");
                    continue;
                }
                if (!EsCedulaValida(NumCedula))
                {
                    Console.WriteLine("Formato de cédula inválido (debe tener 13 números y una letra al final.)");
                    continue;
                }
                if (CedulaDuplicada(NumCedula))
                {
                    Console.WriteLine("Ya existe un cliente registrado con este número de cédula.");
                    NumCedula = null;
                    continue;
                }
                break;
            }
            while (true);

            string Telefono;
            do
            {
                Console.Write("Teléfono: ");
                Telefono = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(Telefono))
                {
                    Console.WriteLine("El número de teléfono no puede estar vacío.");
                    continue;
                }
                if (!EsTelefonoValido(Telefono))
                {
                    Console.WriteLine("Teléfono inválido. Debe contener sólo números, entre 8 y 15 dígitos");
                    continue;
                }
                break;
            }
            while (true);

            string Correo;
            do
            {
                Console.Write("Correo electrónico: ");
                Correo = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(Correo))
                {
                    Console.WriteLine("El correo no puede estar vacío.");
                    continue;
                }
                if (!EsCorreoValido(Correo))
                {
                    Console.WriteLine("El correo ingresado no tiene un formato válido.");
                    continue;
                }
                break;
            }
            while (true);

            Clientes[ContadorClientes, 0] = Nombres;
            Clientes[ContadorClientes, 1] = Apellidos;
            Clientes[ContadorClientes, 2] = NumCedula;
            Clientes[ContadorClientes, 3] = Telefono;
            Clientes[ContadorClientes, 4] = Correo;

            ContadorClientes++;

            Console.WriteLine("\n Cliente registrado exitosamente.");
            Console.WriteLine($"Nombre: {Nombres} {Apellidos}");
            Console.WriteLine($"Número de Cédula: {NumCedula}");
            Console.WriteLine($"Teléfono: {Telefono}");
            Console.WriteLine("\nPresione una tecla para continuar");
            Console.ReadKey();
        }

        static void VerClientes()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Clientes ===");

            if (ContadorClientes == 0)
            {
                Console.WriteLine("No hay clientes registrados.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < ContadorClientes; i++)
            {
                //Console.WriteLine($"{Clientes[i, 0]} {Clientes[i, 1]} | Cédula: {Clientes[i, 2]} | Teléfono: {Clientes[i, 3]} | Correo: {Clientes[i, 4]}");
                Console.WriteLine($"[{i + 1}] {Clientes[i, 0]} {Clientes[i, 1]} | Cédula: {Clientes[i, 2]} | Teléfono: {Clientes[i, 3]} | Correo: {Clientes[i, 4]}");
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }

        static void BuscarCliente()
        {
            Console.Clear();
            Console.WriteLine("=== Buscar Cliente Mediante Cédula ===");
            Console.Write("Ingrese el número de cédula: ");
            string NumCedula = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(NumCedula))
            {
                Console.WriteLine("Cédula vacía. Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            int index = -1;
            for (int i = 0; i < ContadorClientes; i++)
            {
                if (Clientes[i, 2] == NumCedula)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine("Cliente no encontrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCliente encontrado:");
            Console.WriteLine($"Nombre: {Clientes[index, 0]} {Clientes[index, 1]}");
            Console.WriteLine($"Cédula: {Clientes[index, 2]}");
            Console.WriteLine($"Teléfono: {Clientes[index, 3]}");
            Console.WriteLine($"Correo: {Clientes[index, 4]}");

            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("1. Editar cliente");
            Console.WriteLine("2. Eliminar cliente");
            Console.WriteLine("3. Regresar al menú principal");
            Console.WriteLine("Opción: ");

            string Opcion = Console.ReadLine();

            switch (Opcion)
            {
                case "1":
                    EditarCliente(index);
                    break;
                case "2":
                    EliminarCliente(index);
                    break;
                default:
                    return;
            }
        }
        static void EditarCliente(int i)
        {
            Console.Clear();
            Console.WriteLine("=== Editar Cliente ===");

            Console.WriteLine($"1. Nombres ({Clientes[i, 0]})");
            Console.WriteLine($"2. Apellidos ({Clientes[i, 1]})");
            Console.WriteLine($"3. Cédula ({Clientes[i, 2]})");
            Console.WriteLine($"4. Teléfono ({Clientes[i, 3]})");
            Console.WriteLine($"5. Correo ({Clientes[i, 4]})");
            Console.WriteLine($"6. Cancelar");

            Console.Write("\nSeleccione el campo a editar: ");
            string Opcion = Console.ReadLine();

            switch (Opcion)
            {
                case "1":
                    Console.Write("Nuevo nombre: ");
                    Clientes[i, 0] = Console.ReadLine()?.Trim();
                    break;
                case "2":
                    Console.Write("Nuevo apellido: ");
                    Clientes[i, 1] = Console.ReadLine()?.Trim();
                    break;
                case "3":
                    Console.Write("Nueva cédula: ");
                    string NuevaCedula = Console.ReadLine()?.Trim().ToUpper();
                    if (!EsCedulaValida(NuevaCedula))
                    {
                        Console.WriteLine("Cédula inválida.");
                        Console.ReadKey();
                        return;
                    }
                    Clientes[i, 2] = NuevaCedula;
                    break;
                case "4":
                    Console.Write("Nuevo teléfono: ");
                    Clientes[i, 3] = Console.ReadLine()?.Trim();
                    break;
                case "5":
                    Console.Write("Nuevo correo: ");
                    string NuevoCorreo = Console.ReadLine()?.Trim();
                    if (!EsCorreoValido(NuevoCorreo))
                    {
                        Console.WriteLine("Correo inválido.");
                        Console.ReadKey();
                        return;
                    }
                    Clientes[i, 4] = NuevoCorreo;
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            Console.WriteLine("\n Cliente actualizado.");
            Console.ReadKey();
        }

        static void EliminarCliente(int index)
        {
            Console.Write($"\n¿Seguro que desea eliminar este cliente? (S/N): ");
            string Confirmar = Console.ReadLine();
            Confirmar = (Confirmar == null ? "" : Confirmar.Trim().ToUpper());

            if (Confirmar != "S") return;

            for (int i = index; i < ContadorClientes - 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Clientes[i, j] = Clientes[i + 1, j];
                }
            }
            ContadorClientes--;
            Console.WriteLine("\n Cliente eliminado correctamente.");
            Console.ReadKey();
        }

        //===== SERVICIOS =====

        static void MenuServicios()
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("===== MENPU DE SERVICIOS =====\n");
                Console.WriteLine("1. Registrar nuevo servicio");
                Console.WriteLine("2. Ver lista de servicios registrados");
                Console.WriteLine("3. Editar servicio");
                Console.WriteLine("4. Eliminar servicio");
                Console.WriteLine("5. Cambiar estado de servicio");
                Console.WriteLine("6. Volver al menú principal\n");

                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        RegistrarServicio();
                        break;
                    case 2:
                        VerServicios();
                        break;
                    case 3:
                        EditarServicio();
                        break;
                    case 4:
                        EliminarServicio();
                        break;
                    case 5:
                        CambiarEstadoServicio();
                        break;
                    case 6:
                        Console.WriteLine("\nRegresando...");
                        break;
                    default:
                        Console.WriteLine("\nOpción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }

            while (opcion != 6);
        }

        static void RegistrarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== REGISTRAR NUEVO SERVICIO =====");

            string cliente = SeleccionarCliente();
            if (cliente == "No registrado") return;

            string TipoServicio = SeleccionarTipoServicio();

            Console.Write("\nDescripción breve (opcional): ");
            string descripcion = Console.ReadLine();

            Servicios[ContadorServicios, 0] = cliente;
            Servicios[ContadorServicios, 1] = TipoServicio;
            Servicios[ContadorServicios, 2] = descripcion;
            Servicios[ContadorServicios, 3] = "Pendiente";

            ContadorServicios++;

            Console.WriteLine("\n Servicio registrado exitosamente.");
        }

        static void VerServicios()
        {
            Console.Clear();
            Console.WriteLine("===== LISTA DE SERVICIOS =====\n");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }

            for (int i = 0;  i < ContadorServicios; i++)
            {
                Console.WriteLine($"{i + 1}. Cliente: {Servicios[i, 0]} | Servicio: {Servicios[i, 1]} | Estado: {Servicios[i, 3]}");
            }
        }

        static void EditarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== EDITAR SERVICIO =====\n");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }

            VerServicios();

            Console.Write("\nSeleccione el número del servicio a editar: ");
            int indice = Convert.ToInt32(Console.ReadLine());

            if (indice < 0 || indice > ContadorServicios)
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine($"== Editando servicio de: {Clientes[indice, 0]} ==\n");
                Console.WriteLine($"1. Cambiar cliente actual ({Clientes[indice, 0]})");
                Console.WriteLine($"2. Cambiar tipo de servicio ({Servicios[indice, 1]})");
                Console.WriteLine($"3. Editar descripción");
                Console.WriteLine("4. Salir\n");

                Console.Write("Seleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        Servicios[indice, 0] = SeleccionarCliente();
                        break;
                    case 2:
                        Servicios[indice, 1] = SeleccionarTipoServicio();
                        break;
                    case 3:
                        Console.Write("\nNueva descripción: ");
                        Servicios[indice, 2] = Console.ReadLine();
                        break;
                }
            }
            while (opcion != 4);

            Console.WriteLine("\n Cambio guardado.");
        }

        static void EliminarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== ELIMINAR SERVICIO =====\n");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }

            VerServicios();

            Console.Write("\nSeleccione el número del servicio a eliminar: ");
            int index = Convert.ToInt32(Console.ReadLine()) -1;

            if (index < 0 || index >= ContadorServicios)
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            for (int i = index; i < ContadorServicios - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Servicios[i, j] = Servicios[i + 1, j];
                }
            }

            ContadorServicios--;

            Console.WriteLine("\n Servicio eliminado correctamente.");
        }

        static void CambiarEstadoServicio()
        {
            Console.Clear();
            Console.WriteLine("===== CAMBIAR ESTADI DEL SERVICIO =====\n");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }

            VerServicios();

            Console.Write("\nSeleccione el número del servicio: ");
            int index = Convert.ToInt32(Console.ReadLine()) -1;

            if (index <0 || index >= ContadorServicios)
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            Console.WriteLine("\nSeleccione el nuevo estado: ");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. En proceso");
            Console.WriteLine("3. Finalizado");
            Console.WriteLine("4. Cancelado");
            Console.Write("\nOpción: ");

            int estado = Convert.ToInt32(Console.ReadLine());

            switch (estado)
            {
                case 1: Servicios[index, 3] = "Pendiente"; break;
                case 2: Servicios[index, 3] = "En proceso"; break;
                case 3: Servicios[index, 3] = "Finalizado"; break;
                case 4: Servicios[index, 3] = "Cancelado"; break;
                default:
                    Console.WriteLine("\nOpción inválida.");
                    return;
            }

            Console.WriteLine("\nEstado actualizado.");
        }

        //Seleccion de cliente existente o registrar nuevo

        static string SeleccionarCliente()
        {
            Console.Clear();
            Console.WriteLine("\n=== Seleccionar Cliente ===");

            if (ContadorClientes == 0)
            {
                Console.WriteLine("No hay clientes registrados");
                return "No registrado";
            }

            for (int i = 0; i < ContadorClientes; i++)
            {
                Console.WriteLine($"{i + 1}. {Clientes[i, 0]} {Clientes[i, 1]}");
            }

            Console.Write("\nSeleccione un cliente: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;

            if (index < 0 || index >= ContadorClientes)
            {
                return "No registrado";
            }
            return $"{Clientes[index, 0]} {Clientes[index, 1]}";
        }

        static string SeleccionarTipoServicio()
        {
            Console.Clear();
            Console.WriteLine("===== LISTA DE SERVICIOS DISPONIBLES =====");

            for (int i = 0; i < ListaServiciosDisponibles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {ListaServiciosDisponibles[i]}");
            }

            Console.Write("\nSeleccione una opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine()) - 1;

            if (opcion < 0 || opcion >= ListaServiciosDisponibles.Length)
            {
                return "Servicio desconocido";
            }
            return ListaServiciosDisponibles[opcion];
        }

      /*  static void RegistrarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== REGISTRAR SERIVICIO =====");

            if (ContadorServicios >= 50)
            {
                Console.WriteLine("No hay espacio para más servicios.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Ingrese el nombre del Cliente: ");
            string NombreCliente = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(NombreCliente))
            {
                Console.WriteLine("El nombre del cliente es obligatorio.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nSeleccione el tipo de servicio:");
            Console.WriteLine("1. Mantenimiento preventivo");
            Console.WriteLine("2. Reparación de PC/Laptop");
            Console.WriteLine("3. Instalación de cámaras");
            Console.WriteLine("4. Soporte técnico remoto");
            Console.WriteLine("5. Optimización / Instalación de Software");
            Console.Write("Opción: ");

            int OpcionServicio;
            if (!int.TryParse(Console.ReadLine(), out OpcionServicio) || OpcionServicio < 1 || OpcionServicio > 5)
            {
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                return;
            }

            string TipoServicio = "";
            switch (OpcionServicio)
            {
                case 1: TipoServicio = "Mantenimiento preventivo";
                    break;
                case 2: TipoServicio = "Repacación de PC/Laptop";
                    break;
                case 3: TipoServicio = "Instalación de cámaras";
                    break;
                case 4: TipoServicio = "Soporte técnico remoto";
                    break;
                case 5: TipoServicio = "Optimización / Instalación de Software";
                    break;
                default:
                    TipoServicio = "Desconocido";
                    break;
            }

            Servicios[ContadorServicios, 0] = NombreCliente;
            Servicios[ContadorServicios, 1] = TipoServicio;
            Servicios[ContadorServicios, 2] = "Pendiente";
            ContadorServicios++;

            Console.WriteLine("\n Servicio registrado correctamente.");
            Console.ReadKey();
        }

        static void MenuServicios()
        {
            int Opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTIÓN DE SERVICIOS =====");
                Console.WriteLine("1. Ver servicios");
                Console.WriteLine("2. Editar servicio");
                Console.WriteLine("3. Eliminar servicio");
                Console.WriteLine("4. Cambiar estado de servicio");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("\nIngrese una opción: ");

                if (!int.TryParse(Console.ReadLine(), out Opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    continue;
                }

                switch (Opcion)
                {
                    case 1: VerServicios(); break;
                    case 2: EditarServicio(); break;
                    case 3: EliminarServicio(); break;
                    case 4: CambiarEstadoServicio(); break;
                    case 5: return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }
            while (true);
        }

        static void VerServicios()
        {
            Console.Clear();
            Console.WriteLine("===== LISTA DE SERVICIOS =====");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados");
                //Console.ReadKey();
                return;
            }

            for (int i = 0; i < ContadorServicios; i++)
            {
                Console.WriteLine($"\nServicio #{i + 1}:");
                Console.WriteLine($"Cliente: {Servicios[i, 0]}");
                Console.WriteLine($"Descripción: {Servicios[i, 1]}");
                Console.WriteLine($"Estado: {Servicios[i, 2]}");
                Console.WriteLine("----------------------------");
            }

            Console.ReadKey();
        }

        static void EditarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== EDITAR SERVICIO =====");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados");
                Console.ReadKey();
                return;
            }

            VerServicios();
            Console.Write("\nSeleccione número de servicio a editar: ");

            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > ContadorServicios)
            {
                Console.WriteLine(" Opción inválida.");
                Console.ReadKey();
                return;
            }

            index--;

            Console.Write("\nNuevo nombre del cliente (dejar vacío para no cambiar): ");
            string NuevoCliente = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(NuevoCliente))
            {
                Servicios[index, 0] = NuevoCliente;
            }

            Console.Write("Nueva descripcion del servicio (Dejar vacío para no cambiar): ");
            string NuevaDescripcion = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(NuevaDescripcion))
            {
                Servicios[index, 1] = NuevaDescripcion;
            }

            Console.WriteLine("\nServicio actualizado correctamente.");
            Console.ReadKey();
        }

        static void EliminarServicio()
        {
            Console.Clear();
            Console.WriteLine("===== ELIMINAR SERVICIO =====");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                Console.ReadKey();
                return;
            }

            VerServicios();
            Console.WriteLine("\nSeleccione número de servicio a eliminar: ");

            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > ContadorServicios)
            {
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                return;
            }

            index--;

            Console.Write($"\n¿Seguro que quiere eliminar este servicio? (S/N): ");
            string Confirmacion = Console.ReadLine()?.Trim().ToUpper();

            if (Confirmacion != "S")
            {
                Console.WriteLine("Operación cancelada.");
                Console.ReadKey();
                return;
            }

            for (int i = index; i < ContadorServicios - 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Servicios[i, j] = Servicios[i + 1, j];
                }
            }

            ContadorServicios--;
            Console.WriteLine("\n Servicio eliminado exitosamente.");
            Console.ReadLine();
        }

        static void CambiarEstadoServicio()
        {
            Console.Clear();
            Console.WriteLine("===== CAMBIAR ESTADO DE SERVICIO =====");

            if (ContadorServicios == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                Console.ReadKey();
                return;
            }

            VerServicios();
            Console.Write("\nSeleccione número de servicio: ");
            int index;

            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > ContadorServicios)
            {
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                return;
            }

            index--;

            Console.WriteLine("\nSeleccione el nuevo estado:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. En proceso");
            Console.WriteLine("3. Finalizado");
            Console.WriteLine("4. Cancelado");
            Console.Write("Opción: ");

            int Estado;
            if (!int.TryParse(Console.ReadLine(), out Estado) || Estado < 1 || Estado > 4)
            {
                Console.WriteLine("Estado inválido.");
                Console.ReadKey();
                return;
            }

            string NuevoEstado = "";

            switch (Estado)
            {
                case 1: NuevoEstado = "Pendiente";
                    break;
                case 2: NuevoEstado = "En proceso";
                    break;
                case 3: NuevoEstado = "Finalizado";
                    break;
                case 4: NuevoEstado = "Cancelado";
                    break;
                default:
                    Console.WriteLine("Estado inválido.");
                    Console.ReadKey();
                    return;
            }
            Servicios[index, 2] = NuevoEstado;

            Console.WriteLine("\n Estado del servicio actualizado correctamente");
            Console.ReadLine();
        }*/

        //Validacion de Datos:
        static bool EsCorreoValido(string Correo)
        {
            if (string.IsNullOrEmpty(Correo)) return false;
            string PatronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(Correo, PatronCorreo);
        }

        static bool EsTelefonoValido(string Telefono)
        {
            if (string.IsNullOrEmpty(Telefono)) return false;
            //Acepta sólo números y una longitud de hasta 15 dígitos:
            return Regex.IsMatch(Telefono, @"^\d{7,15}$");
        }

        static bool EsCedulaValida(string NumCedula)
        {
            if (string.IsNullOrWhiteSpace(NumCedula)) return false;
            string SinGuiones = NumCedula.Replace("-", "").Trim();

            if (SinGuiones.Length != 14) return false;

            for (int i = 0; i < 13; i++)
            {
                if (!char.IsDigit(SinGuiones[i])) return false;
            }

            char Ultimo = SinGuiones[13];
            if (!char.IsLetter(Ultimo)) return false;

            return true;
        }

        static bool CedulaDuplicada(string NumCedula)
        {
            for (int i = 0; i < ContadorClientes; i++)
            {
                if (Clientes[i, 2].ToUpper() == NumCedula.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        static void MenuProductos()
        {
            int Opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTIÓN DE PRODUCTOS =====");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Ver lista de productos");
                Console.WriteLine("3. Editar producto");
                Console.WriteLine("4. Eliminar producto");
                Console.WriteLine("5. Calcular promedio de precios");
                Console.WriteLine("6. Ver producto más caro");
                Console.WriteLine("7. Volver al menú principal");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out Opcion))
                {
                    Console.WriteLine("Ingrese una opción válida.");
                    Console.ReadKey();
                    continue;
                }
                switch (Opcion)
                {
                    case 1:
                        AgregarProducto();
                        break;
                    case 2:
                        VerProductos();
                        break;
                    case 3:
                        EditarProducto();
                        break;
                    case 4:
                        EliminarProducto();
                        break;
                    case 5:
                        CalcularPromedioPrecios();
                        break;
                    case 6:
                        ProductoMasCaro();
                        break;
                }
            }
            while (Opcion != 7);
        }

        static void AgregarProducto()
        {
            Console.Clear();
            Console.WriteLine("===== AGREGAR PRODUCTO =====");

            if (ContadorProductos >= 50)
            {
                Console.WriteLine("No se pueden registrar más productos.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre del producto: ");
            string NombreProducto = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(NombreProducto))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                Console.ReadKey();
                return;
            }

            Console.Write("Precio: $");
            double Precio;
            if (!double.TryParse(Console.ReadLine(), out Precio) || Precio <= 0)
            {
                Console.WriteLine("Precio inválido.");
                Console.ReadKey();
                return;
            }

            Productos[ContadorProductos] = NombreProducto;
            Precios[ContadorProductos] = Precio;
            ContadorProductos++;

            Console.WriteLine("Producto agregado correctamente.");
            Console.ReadKey();
        }

        static void VerProductos()
        {
            Console.Clear();
            Console.WriteLine("===== LISTA DE PRODUCTOS =====");

            if (ContadorProductos == 0)
            {
                Console.WriteLine("No hay productos registrados.");
            }
            else
            {
                for (int i = 0; i < ContadorProductos; i++)
                {
                    Console.WriteLine($"{i + 1}. {Productos[i]} - ${Precios[i]: 0.00}");
                }
            }
            Console.ReadKey();
        }
        static void CalcularPromedioPrecios()
        {
            Console.Clear();
            Console.WriteLine("===== PROMEDIO DE PRECIOS =====");

            if (ContadorProductos == 0)
            {
                Console.WriteLine("No hay productos registrados.");
            }
            else
            {
                double suma = 0;
                for (int i = 0; i < ContadorProductos; i++)
                    suma += Precios[i];

                double promedio = suma / ContadorProductos;
                Console.WriteLine($"El precio promedio es: ${promedio: 0.00}");
            }

            Console.ReadKey();
        }

        static void ProductoMasCaro()
        {
            Console.Clear();
            Console.WriteLine("===== PRODUCTO MÁS CARO =====");

            if (ContadorProductos == 0)
            {
                Console.WriteLine("No hay productos registrados.");
            }
            else
            {
                double maxPrecio = Precios[0];
                int indexMax = 0;

                for (int i = 1; i < ContadorProductos; ++i)
                {
                    if (Precios[i] > maxPrecio)
                    {
                        maxPrecio = Precios[i];
                        indexMax = i;
                    }
                }
                Console.WriteLine($"El producto más caro es: {Productos[indexMax]} - ${maxPrecio: 0.00}");
            }

            Console.ReadKey();
        }

        static void EditarProducto()
        {
            Console.Clear();
            Console.WriteLine("===== EDITAR PRODUCTO =====");

            if (ContadorProductos == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                Console.ReadKey();
                return;
            }

            VerProductos();
            Console.Write("\nSeleccione el número del producto a editar: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > ContadorProductos)
            {
                Console.WriteLine("Opción inválida.");
                //Console.ReadKey();
                return;
            }

            index--;

            Console.WriteLine($"\nProducto actual: {Productos[index]} - ${Precios[index]: 0.00}");

            Console.Write("\nNuevo nombre (dejar vacío para mantener): ");
            string NuevoNombre = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(NuevoNombre))
            {
                Productos[index] = NuevoNombre;
            }

            Console.Write("Nuevo precio (dejar vacío para mantener): $");
            string nuevoPrecioInput = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(nuevoPrecioInput))
            {
                double nuevoPrecio;
                if(double.TryParse(nuevoPrecioInput, out nuevoPrecio) && nuevoPrecio >0)
                {
                    Precios[index] = nuevoPrecio;
                }
                else
                {
                    Console.WriteLine("Precio inváldo, no se realizaron los cambios en el precio.");
                }
            }

            Console.WriteLine("\nProducto actualizado correctamente.");
            Console.ReadKey();
        }

        static void EliminarProducto()
        {
            Console.Clear();
            Console.WriteLine("===== ELIMINAR PRODUCTO =====");

            if (ContadorProductos == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                Console.ReadKey();
                return;
            }

            VerProductos();
            Console.Write("\nSeleccione el número del producto a eliminar: ");
            int index;
            if (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > ContadorProductos)
            {
                Console.WriteLine("Opción inválida.");
                Console.ReadKey();
                return;
            }

            index--;

            Console.Write($"¿Está seguro de eliminar '{Productos[index]}'? (S/N): ");
            string confirm = Console.ReadLine().Trim().ToUpper();

            if ( confirm != "S")
            {
                Console.WriteLine("Operación cancelada.");
                Console.ReadKey();
                return;
            }

            for (int i = index; i < ContadorProductos -1; i++)
            {
                Productos[i] = Productos[i + 1];
                Precios[i] = Precios[i + 1];
            }

            ContadorProductos--;
            Console.WriteLine("\n Producto eliminado correctamente.");
            Console.ReadKey();
        }
    }
}
