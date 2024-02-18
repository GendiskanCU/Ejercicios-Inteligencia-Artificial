using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PuzzleIA_01
{

    public class Nodo
    {
        //Array bidimensional que representa las 9 casillas del puzzle
        int[,] casillas = new int[3, 3];

        //Nodo padre (necesario para guardar el camino de resolución del puzzle)
        Nodo nodoPadre;

        /// <summary>
        /// Constructor por defecto, que inicializa el nodo con las piezas en el orden correcto
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
        /// Guarda el nodo padre del nodo actual
        /// </summary>
        /// <param name="padre"></param>
        public void EstableceNodoPadre(Nodo padre)
        {
            nodoPadre = new Nodo();
            nodoPadre.CopiaEstado(padre);
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
                    if(casillas[fila, columna] != 0)
                        nodoDibujado += " " + casillas[fila, columna] + " ";
                    else
                        nodoDibujado += "-" + casillas[fila, columna] + "-";
                }
                nodoDibujado += "\n";
            }

            return nodoDibujado;
        }

        /// <summary>
        /// Devuelve el nodo padre del nodo actual. Si no tiene nodo padre devuelve el propio nodo
        /// </summary>
        /// <returns></returns>
        public Nodo Padre()
        {
            return nodoPadre;
            //return (nodoPadre == null) ? this : nodoPadre;
        }


        /// <summary>
        /// Desordena aleatoriamente el nodo (no se utiliza en la versión actual)
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
        /// Comprueba si un nodo tiene la condición de nodo Meta
        /// </summary>
        /// <returns>true/false si es/no es nodo Meta</returns>
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
        /// <returns>true/false si los dos nodos son iguales/distintos</returns>
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
        /// Copia el estado del nodo original (hace una copia del mismo)
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


        /// <summary>
        /// Devuelve la posición en la que está el hueco
        /// </summary>
        /// <returns>Posición del hueco en el array bidimensional</returns>
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
            Nodo nodoInicial = new Nodo(1, 2, 3,
                                        6, 4, 5,
                                        7, 0, 8);

            Console.WriteLine("NODO INICIAL:\n" + nodoInicial.Dibujar());



            if(BusquedaEnAnchura(nodoInicial))
            {
                Console.WriteLine("\n¡¡¡Puzzle resuelto con éxito!!!\n");
            }
            else
            {
                Console.WriteLine("\nNo se ha encontrado solución al puzzle\n");
            }

            Console.WriteLine("Ejecución finalizada");
            Console.ReadKey();
        }



        /// <summary>
        /// Realiza una búsqueda en anchura de la solución
        /// </summary>
        /// <param name="nodoInicial"></param>
        /// <returns>true si la búsqueda ha obtenido la solución / false si no es así</returns>
        static bool BusquedaEnAnchura(Nodo nodoInicial)
        {
            List<Nodo> nodosAbiertos = new List<Nodo>();
            List<Nodo> nodosCerrados = new List<Nodo>();
            Nodo nodoActual = new Nodo();

            //Fichero interno donde se irá guardando el proceso de búsqueda
            StreamWriter ficheroPruebas = new StreamWriter("comprobaciones.txt");

            Console.WriteLine("Buscando la solución por el método de Búsqueda en anchura.\n" + 
                "El proceso puede tardar unos minutos, ten paciencia...\n");            


            nodosAbiertos.Add(nodoInicial);
            nodosCerrados.Add(nodoInicial);

            int numeroPasadas = 0, nodosCreados = 1; //Variables para escribir en el fichero de control interno

            while (nodosAbiertos.Count > 0)
            {                
                //Copia en actual el primer nodo Abierto
                nodoActual.CopiaEstado(nodosAbiertos[0]);

                if(nodosAbiertos[0].Padre() != null)//Guarda el padre del nodo actual para conservar el camino
                    nodoActual.EstableceNodoPadre(nodosAbiertos[0].Padre());
                //Guarda los valores de control en el fichero interno
                if(nodoActual.Padre() != null)
                    ficheroPruebas.WriteLine("Paso " + ++numeroPasadas + ". Num. abiertos: " + nodosAbiertos.Count + ". Num cerrados: " + nodosCerrados.Count +
                    ". Nodos creados: " + nodosCreados + ". Nodo actual:\n" + nodoActual.Dibujar() + "Nodo padre del actual:\n" + nodoActual.Padre().Dibujar() + "\n");
                
                //Elimina el primer nodo de la lista de Abiertos
                nodosAbiertos.RemoveAt(0);                

                //Añade el nodo actual a la lista de Cerrados
                Nodo nodoACerrar = new Nodo();
                nodoACerrar.CopiaEstado(nodoActual);
                if (nodoActual.Padre() != null)
                    nodoACerrar.EstableceNodoPadre(nodoActual.Padre());
                nodosCerrados.Add(nodoACerrar);
                

                //Si el nodo actual es el META se muestra la solución obtenida (camino) y devuelve confirmación del éxito
                if (nodoActual.EsMeta())
                {
                    RecorreCamino(nodosCerrados, nodoActual);
                    return true;
                }
                else //Si todavía no se ha llegado al nodo meta
                {
                    //Se obtienen todos los posibles sucesores del nodo actual
                    List<Nodo> posiblesSucesores = new List<Nodo>();
                    posiblesSucesores = nodoActual.ObtenerSucesores();

                    nodosCreados += posiblesSucesores.Count;//Para el fichero de control interno                    

                    //Se añaden al final de la lista de Abiertos todos los posibles sucesores que no
                    //estén ya ni en la lista de Abiertos ni en la de Cerrados
                    foreach (Nodo n in posiblesSucesores)
                    {
                        n.EstableceNodoPadre(nodoActual);//Guarda antes el nodo padre para poder recuperar el camino
                       

                        if (!EstaEnLaLista(n, nodosCerrados) && !EstaEnLaLista(n, nodosAbiertos))
                        {                            
                            nodosAbiertos.Add(n);                            
                        }                        
                    }
                }
            }

            ficheroPruebas.Close();//Se cierra el fichero de control interno

            //Se llega aquí sin obtener solución
            return false;            
        }



        /// <summary>
        /// Comprueba si un nodo está en una lista de nodos
        /// </summary>
        /// <param name="nodoAComprobar"></param>
        /// <param name="listaDeNodos"></param>
        /// <returns></returns>
        static bool EstaEnLaLista(Nodo nodoAComprobar, List<Nodo> listaDeNodos)
        {
            foreach (Nodo n in listaDeNodos)
            {
                if (nodoAComprobar.EsIgualA(n))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Busca el camino recorrido hasta llegar al nodo final y lo muestra por pantalla
        /// </summary>
        /// <param name="nodosVisitados">Lista de nodos cerrados (los visitados durante la búsqueda)</param>
        /// <param name="nodoFinal">Nodo final (será el de partida para la obtención del camino)</param>
        static void RecorreCamino(List<Nodo> nodosVisitados, Nodo nodoFinal)
        {
            List<Nodo> caminoInverso = new List<Nodo>();//El camino se creará en orden inverso al que se recorre

            Nodo nodoPadre = new Nodo();//Nodo padre

            Nodo nuevoNodo = new Nodo();//Nodo temporal

            caminoInverso.Add(nodoFinal);//Añade al camino el último de los nodos del mismo

            if (nodoFinal.Padre() != null)
            {
               //Añade al camino su nodo padre 
                nodoPadre.CopiaEstado(nodoFinal.Padre());
                nuevoNodo.CopiaEstado(nodoPadre);
                caminoInverso.Add(nuevoNodo);
                //Console.WriteLine("Entra aquí, y añade a: " + nodoFinal.Padre().Dibujar());
            }            

            //Console.WriteLine("Número de nodos visitados: " + nodosVisitados.Count);

            while (nodoPadre != null)//Mientras haya algún nodo padre del último que se añadió al camino
            {
                //Console.WriteLine("Buscando a:\n" + nodoPadre.Dibujar());

                foreach (Nodo n in nodosVisitados)//Recorre todos los nodos en busca actual
                {                    
                    if (nodoPadre != null && n.EsIgualA(nodoPadre))
                    {
                        //if(n.Padre() != null)
                        //  Console.WriteLine("Se ha localizado:\n" + nodoPadre.Dibujar() + "Su padre es:\n" + n.Padre().Dibujar());

                        //Si lo encuentra, comprueba si a su vez éste tiene un padre, en cuyo caso añade este último al camino
                        if (n.Padre() != null)
                        {  
                            //Console.WriteLine("Añadiendo al camino a:\n" + n.Padre().Dibujar());
                            Nodo nodoCamino = new Nodo();
                            nodoCamino.CopiaEstado(n.Padre());
                            caminoInverso.Add(nodoCamino);
                                                     
                            //Cambia el nodo actual por su padre para seguir comprobando
                            nodoPadre.CopiaEstado(n.Padre());
                        }
                        else //Cuando el nodo actual no tenga padre, será porque se ha llegado al último del camino inverso
                        {
                            nodoPadre = null; //Se asigna el valor nulo, lo que provocará a su vez la salida del bucle
                        }
                    }
                }
            }

            //Console.WriteLine("Número de nodos en el camino: " + caminoInverso.Count);           

            //Creado el camino, se recorre en orden contrario para mostrar la solución al puzzle

            Console.WriteLine("\nSe ha encontrado una solución. Pulsa una tecla para verla");
            Console.ReadKey();            

            for(int i = caminoInverso.Count - 1; i >= 0; i--)
            {                
                Thread.Sleep(1500); //Hace una pausa                
                Console.Clear();
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("\n=========\n" + caminoInverso[i].Dibujar() + "=========");                
            }            
        }
    }
}