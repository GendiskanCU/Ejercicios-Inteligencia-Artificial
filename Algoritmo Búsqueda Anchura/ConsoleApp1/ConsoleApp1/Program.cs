using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleIA_01
{

    public class Nodo
    {
        //Array bidimensional que representa las 9 casillas del puzzle
        int[,] casillas = new int[3, 3];

        /// <summary>
        /// Constructor por defecto, que inciliza el nodo con las piezas en el orden correcto
        /// </summary>
        public Nodo()
        {
            int valor = 0;
            for (int fila = 0; fila < 3; fila++)
                for (int columna = 0; columna < 3; columna++)
                    casillas[fila, columna] = valor++;
        }


        /// <summary>
        /// Constructor que recibe individualmente el valor de la pieza en cada casilla del nodo
        /// </summary>
        /// <param name="fila0columna0"></param>
        /// <param name="fila0columna1"></param>
        /// <param name="fila0columna2"></param>
        /// <param name="fila1columna0"></param>
        /// <param name="fila1columna1"></param>
        /// <param name="fila1columna2"></param>
        /// <param name="fila2columna0"></param>
        /// <param name="fila2columna1"></param>
        /// <param name="fila2columna2"></param>
        public Nodo(int fila0columna0, int fila0columna1, int fila0columna2, int fila1columna0,
            int fila1columna1, int fila1columna2, int fila2columna0, int fila2columna1, int fila2columna2)
        {
            casillas[0, 0] = fila0columna0;
            casillas[0, 1] = fila0columna1;
            casillas[0, 2] = fila0columna2;
            casillas[1, 0] = fila1columna0;
            casillas[1, 1] = fila1columna1;
            casillas[1, 2] = fila1columna2;
            casillas[2, 0] = fila2columna0;
            casillas[2, 1] = fila2columna1;
            casillas[2, 2] = fila2columna2;
        }

        /// <summary>
        /// Representa el nodo en forma de cadena de caracteres
        /// </summary>
        /// <returns>un strign con la representación del nodo</returns> <summary>
        public string Dibujar()
        {
            string nodoDibujado = "";

            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    nodoDibujado += casillas[fila, columna] + ",";
                }
                nodoDibujado += "\n";
            }

            return nodoDibujado;
        }


        /// <summary>
        /// Desordena aleatoriamente el nodo
        /// </summary>
        public void Desordenar()
        {
            int filaAleatoria, columnaAleatoria, temporal;
            Random numeroAleatorio = new Random();


            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    filaAleatoria = numeroAleatorio.Next(0, 3);
                    columnaAleatoria = numeroAleatorio.Next(0, 3);
                    temporal = casillas[fila, columna];
                    casillas[fila, columna] = casillas[filaAleatoria, columnaAleatoria];
                    casillas[filaAleatoria, columnaAleatoria] = temporal;
                }
            }
        }


        /// <summary>
        /// Comprueba si el nodo tiene la condición de nodo Meta, o solucionado
        /// </summary>
        /// <returns>true/false si es/ no es nodo Meta o solucionado</returns>
        public bool EsMeta()
        {
            int valor = 0;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (valor != casillas[fila, columna])
                        return false;
                    valor++;
                }
            }
            return true;
        }

        /// <summary>
        /// Compara el nodo con otro nodo
        /// </summary>
        /// <param name="otroNodo"></param>
        /// <returns>true/false si los dos nodos son iguales/distintos</returns> <summary>    
        public bool EsIgualA(Nodo otroNodo)
        {
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (casillas[fila, columna] != otroNodo.casillas[fila, columna])
                        return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Copia el estado del nodo original
        /// </summary>
        /// <param name="nodoOriginal"></param>
        public void CopiaEstado(Nodo nodoOriginal)
        {
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    casillas[fila, columna] = nodoOriginal.casillas[fila, columna];
                }
            }
        }


        public (int, int) PosicionHueco()
        {
            int huecoEnFila = 0, huecoEnColumna = 0;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (casillas[fila, columna] == 0)
                    {
                        huecoEnFila = fila;
                        huecoEnColumna = columna;
                    }
                }
            }

            return (huecoEnFila, huecoEnColumna);
        }


        /// <summary>
        /// Crea un nodo sucesor a partir del actual, en el que el hueco o valor cero es movido
        /// desde la fila/columna actual a la fila/columna nueva intercambiándose con el valor
        /// o pieza que había en esta última
        /// </summary>
        /// <param name="filaActual">Fila actual del hueco</param>
        /// <param name="columnaActual">Columna actual del hueco</param>
        /// <param name="filaNueva">Fila a la que se moverá el hueco</param>
        /// <param name="columnaNueva">Columna a la que se moverá el hueco</param>
        /// <returns></returns>
        public Nodo Sucesor(int filaActual, int columnaActual, int filaNueva, int columnaNueva)
        {
            //Crea un nuevo nodo temporal para trabajar con él sin modificar el original
            Nodo nodoTemporal = new Nodo();
            //Copia el estado del nodo original al temporal
            nodoTemporal.CopiaEstado(this);

            //Guarda temporalmente el valor que hay en la casilla donde el hueco se va a mover
            int temporal = nodoTemporal.casillas[filaNueva, columnaNueva];
            //Coloca el hueco en su nueva posición
            nodoTemporal.casillas[filaNueva, columnaNueva] = 0;
            //Se intercambia por el valor que había en ella
            nodoTemporal.casillas[filaActual, columnaActual] = temporal;

            return nodoTemporal;
        }

        /// <summary>
        /// Obtiene nodos a partir del actual en los que el hueco se habrá movido a las
        /// posiciones posibles en función de la que tiene en este momento
        /// </summary>
        /// <returns>Lista de Nodos sucesores</returns>
        public List<Nodo> ObtenerSucesores()
        {
            //Lista de sucesores a obtener y devolver
            List<Nodo> sucesores = new List<Nodo>();

            //En función de la posición actual del Hueco se determina hacia dónde podrá moverse
            //Obteniéndose el sucesor que corresponda a cada caso y añadiéndolos a la lista de sucesores
            var (filaActualHueco, columnaActualHueco) = this.PosicionHueco();
            int filaNuevaHueco, columnaNuevaHueco;

            //El hueco podrá moverse hacia abajo si está en la fila 0 ó 1
            if (filaActualHueco < 2)
            {
                //Se crea un sucesor en el que el hueco se mueve a la fila de abajo
                //manteniéndose en la misma columna
                filaNuevaHueco = filaActualHueco + 1;
                columnaNuevaHueco = columnaActualHueco;

                Nodo sucesorAbajo = new Nodo();
                sucesorAbajo.CopiaEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));

                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorAbajo);
            }


            //El hueco podrá moverse hacia arriba si está en la fila 1 ó 2
            if (filaActualHueco > 0)
            {
                //Se crea un sucesor en el que el hueco se mueve a la fila de arriba
                //manteniéndose en la misma columna
                filaNuevaHueco = filaActualHueco - 1;
                columnaNuevaHueco = columnaActualHueco;

                Nodo sucesorArriba = new Nodo();
                sucesorArriba.CopiaEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));

                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorArriba);
            }


            //El hueco podrá moverse hacia la derecha si está en la columna 0 ó 1
            if (columnaActualHueco < 2)
            {
                //Se crea un sucesor en el que el hueco se mueve a la columna de la derecha
                //manteniéndose en la misma fila
                filaNuevaHueco = filaActualHueco;
                columnaNuevaHueco = columnaActualHueco + 1;

                Nodo sucesorDerecha = new Nodo();
                sucesorDerecha.CopiaEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));

                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorDerecha);
            }


            //El hueco podrá moverse hacia la izquierda si está en la columna 1 ó 2
            if (columnaActualHueco > 0)
            {
                //Se crea un sucesor en el que el hueco se mueve a la columna de la izquierda
                //manteniéndose en la misma fila
                filaNuevaHueco = filaActualHueco;
                columnaNuevaHueco = columnaActualHueco - 1;

                Nodo sucesorIzquierda = new Nodo();
                sucesorIzquierda.CopiaEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));

                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorIzquierda);
            }

            return sucesores;
        }
    }





    class Programa
    {
        static void Main(string[] args)
        {
            Nodo nodoInicial = new Nodo(6, 5, 4,
                                        1, 3, 2,
                                        8, 0, 7);

            Console.WriteLine("NODO INICIAL:\n" + nodoInicial.Dibujar());
            

            
            Nodo solucion = BusquedaEnAnchura(nodoInicial);

            if(solucion != null )
            {
                Console.WriteLine("SE HA ENCONTRADO EL NODO META:\n" + solucion.Dibujar());
            }
            else
            {
                Console.WriteLine("No se ha encontrado la solución");
            }
           
            Console.WriteLine("Ejecución finalizada");
            Console.ReadKey();
            
        }





        static Nodo BusquedaEnAnchura(Nodo nodoInicial)
        {
            List<Nodo> nodosAbiertos = new List<Nodo>();
            List<Nodo> nodosCerrados = new List<Nodo>();
            Nodo nodoActual = new Nodo();

            StreamWriter ficheroPruebas = new StreamWriter("comprobaciones.txt");

            Console.WriteLine("Buscando en anchura...");

            if(nodoInicial.EsMeta())
            {
                ficheroPruebas.Close();
                return nodoInicial;
            }


            nodosAbiertos.Add(nodoInicial);
            nodosCerrados.Add(nodoInicial);

            int numeroPasadas = 0, nodosCreados = 1;
            while (nodosAbiertos.Count > 0)
            {
                //Console.WriteLine("Nodos abiertos al principio: " + nodosAbiertos.Count);
                //Copia en actual el primer nodo Abierto
                nodoActual.CopiaEstado(nodosAbiertos[0]);

                ficheroPruebas.WriteLine("Paso " + ++numeroPasadas + ". Num. abiertos: " + nodosAbiertos.Count + ". Num cerrados: " + nodosCerrados.Count +
                    ". Nodos creados: " + nodosCreados + ". Nodo actual:\n" + nodoActual.Dibujar());

                /*Console.WriteLine("Nodo actual:\n" + nodoActual.Dibujar());
                Console.ReadKey();*/

                //Elimina el primer nodo de la lista de Abiertos
                nodosAbiertos.RemoveAt(0);
                //Console.WriteLine("Nodos abiertos después de quitar el primero: " + nodosAbiertos.Count);
                /*Console.WriteLine("Nodos abiertos:");
                foreach (Nodo n in nodosAbiertos)
                {
                    Console.WriteLine(n.Dibujar());
                }
                Console.ReadKey();*/

                //Añade el nodo actual a la lista de Cerrados
                Nodo nodoACerrar = new Nodo();
                nodoACerrar.CopiaEstado(nodoActual);
                nodosCerrados.Add(nodoACerrar);
                /*Console.WriteLine("Nodos cerrados:");
                foreach (Nodo n in nodosCerrados)
                {
                    Console.WriteLine(n.Dibujar());
                }
                Console.ReadKey();*/

                //Si el nodo actual es el META finaliza con éxito
                if (nodoActual.EsMeta())
                {
                    ficheroPruebas.Close();
                    return nodoActual;
                }
                else //Si todavía no se ha llegado al nodo meta
                {
                    //Se obtienen todos los posibles sucesores del nodo actual
                    List<Nodo> posiblesSucesores = new List<Nodo>();
                    posiblesSucesores = nodoActual.ObtenerSucesores();

                    nodosCreados += posiblesSucesores.Count;
                    /*Console.WriteLine("Posibles sucesores del nodo actual:");
                    foreach(Nodo n in posiblesSucesores)
                    {
                        Console.WriteLine(n.Dibujar());
                    }
                    Console.ReadKey();*/

                    //Se añaden al final de la lista de Abiertos todos los posibles sucesores que no
                    //estén ya ni en la lista de Abiertos ni en la de Cerrados
                    foreach (Nodo n in posiblesSucesores)
                    {
                        //Si algún sucesor ya es el META, se finaliza con éxito
                        if (n.EsMeta())
                        {
                            ficheroPruebas.Close();
                            return n;
                        }


                        if (!EstaEnLaLista(n, nodosCerrados) && !EstaEnLaLista(n, nodosAbiertos))
                        {
                            nodosAbiertos.Add(n);
                            //Console.Write("\nNodo añadido a Abiertos:" + n.Dibujar());
                        }
                        //Console.ReadKey();
                    }

                    /*Console.WriteLine("Nodos abiertos al final del proceso:");
                    foreach (Nodo n in nodosAbiertos)
                    {
                        Console.WriteLine(n.Dibujar());
                    }
                    Console.ReadKey();*/

                }
            }

            ficheroPruebas.Close();
            //Se llega al final sin encontrar la solución
            return null;
        }



        static bool EstaEnLaLista(Nodo nodoAComprobar, List<Nodo> listaDeNodos)
        {
            foreach (Nodo n in listaDeNodos)
            {
                if (nodoAComprobar.EsIgualA(n))
                    return true;
            }
            return false;
        }
    }
}