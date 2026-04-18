namespace TareaArbolAVL
{
    internal class Program
    {
        class Nodo
        {
            public int valor;
            public Nodo izquierda, derecha;
            public int altura;

            public Nodo(int v)
            {
                valor = v;
                altura = 1;
            }
        }

        class BST
        {
            public Nodo Insertar(Nodo raiz, int valor)
            {
                if (raiz == null)
                    return new Nodo(valor);

                if (valor < raiz.valor)
                    raiz.izquierda = Insertar(raiz.izquierda, valor);
                else
                    raiz.derecha = Insertar(raiz.derecha, valor);

                return raiz;
            }
        }

        class AVL
        {
            int Altura(Nodo n) => n == null ? 0 : n.altura;

            int Balance(Nodo n) => n == null ? 0 : Altura(n.izquierda) - Altura(n.derecha);

            Nodo RotarDerecha(Nodo y)
            {
                Nodo x = y.izquierda;
                Nodo T2 = x.derecha;

                x.derecha = y;
                y.izquierda = T2;

                y.altura = Math.Max(Altura(y.izquierda), Altura(y.derecha)) + 1;
                x.altura = Math.Max(Altura(x.izquierda), Altura(x.derecha)) + 1;

                return x;
            }

            Nodo RotarIzquierda(Nodo x)
            {
                Nodo y = x.derecha;
                Nodo T2 = y.izquierda;

                y.izquierda = x;
                x.derecha = T2;

                x.altura = Math.Max(Altura(x.izquierda), Altura(x.derecha)) + 1;
                y.altura = Math.Max(Altura(y.izquierda), Altura(y.derecha)) + 1;

                return y;
            }

            public Nodo Insertar(Nodo nodo, int valor)
            {
                if (nodo == null)
                    return new Nodo(valor);

                if (valor < nodo.valor)
                    nodo.izquierda = Insertar(nodo.izquierda, valor);
                else if (valor > nodo.valor)
                    nodo.derecha = Insertar(nodo.derecha, valor);
                else
                    return nodo;

                nodo.altura = 1 + Math.Max(Altura(nodo.izquierda), Altura(nodo.derecha));

                int balance = Balance(nodo);

                if (balance > 1 && valor < nodo.izquierda.valor)
                    return RotarDerecha(nodo);

                if (balance < -1 && valor > nodo.derecha.valor)
                    return RotarIzquierda(nodo);

                if (balance > 1 && valor > nodo.izquierda.valor)
                {
                    nodo.izquierda = RotarIzquierda(nodo.izquierda);
                    return RotarDerecha(nodo);
                }

                if (balance < -1 && valor < nodo.derecha.valor)
                {
                    nodo.derecha = RotarDerecha(nodo.derecha);
                    return RotarIzquierda(nodo);
                }

                return nodo;
            }
        }

        class Util
        {
            public static void ImprimirArbol(Nodo raiz, int espacio = 0, int incremento = 5)
            {
                if (raiz == null)
                    return;

                espacio += incremento;

                ImprimirArbol(raiz.derecha, espacio);

                Console.WriteLine();
                for (int i = incremento; i < espacio; i++)
                    Console.Write(" ");
                Console.WriteLine(raiz.valor);

                ImprimirArbol(raiz.izquierda, espacio);
            }
        }
        static void Main(string[] args)
            {
            int cantidad = LeerEnteroPositivo();

            Random rand = new Random();

            BST bst = new BST();
            AVL avl = new AVL();

            Nodo raizBST = null;
            Nodo raizAVL = null;

            Console.WriteLine("\nValores:");

            for (int i = 0; i < cantidad; i++)
            {
                int valor = rand.Next(1, 101);
                Console.Write(valor + " ");

                raizBST = bst.Insertar(raizBST, valor);
                raizAVL = avl.Insertar(raizAVL, valor);
            }

            Console.WriteLine("\n");

            Console.WriteLine("Árbol sin balancear:");
            Util.ImprimirArbol(raizBST);

            Console.WriteLine("\nÁrbol balanceado:");
            Util.ImprimirArbol(raizAVL);

            Console.WriteLine("\nLa Raiz esta a la izquierda y los hijos a la derecha"); //Se intento y asi quedo lol
        }
        static int LeerEnteroPositivo()
        {
            int numero;

            while (true)
            {
                Console.Write("Ingrese la cantidad de valores del árbol: ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out numero) && numero > 0)
                {
                    return numero;
                }

                Console.WriteLine("Error: Solo se aceptan numeros enteros positivos.\n");
            }
        }
    }
}
