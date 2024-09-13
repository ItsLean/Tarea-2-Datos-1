public enum SortDirection
{
    Asc,
    Desc
}

public interface IList
{
    void InsertInOrder(int value);
    int DeleteFirst();
    int DeleteLast();
    bool DeleteValue(int value);
    int GetMiddle();
    void MergeSorted(IList listA, IList listB, SortDirection direction);
}

public class Nodo
{
    public int Valor;
    public Nodo Anterior;
    public Nodo Siguiente;

    public Nodo(int valor)
    {
        Valor = valor;
        Anterior = null;
        Siguiente = null;
    }
}

public class ListaDoble : IList
{
    private Nodo cabeza;
    private Nodo cola;
    private Nodo medio;
    private int longitud;

    public ListaDoble()
    {
        cabeza = null;
        cola = null;
        medio = null;
        longitud = 0;
    }

    // Insertar en orden ascendente
    public void InsertInOrder(int value)
    {
        Nodo nuevo = new Nodo(value);
        if (cabeza == null)
        {
            cabeza = cola = medio = nuevo;
            longitud = 1;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual != null && actual.Valor < value)
            {
                actual = actual.Siguiente;
            }

            if (actual == null) // Insertar al final
            {
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
            else if (actual == cabeza) // Insertar al inicio
            {
                nuevo.Siguiente = cabeza;
                cabeza.Anterior = nuevo;
                cabeza = nuevo;
            }
            else // Insertar en medio
            {
                actual.Anterior.Siguiente = nuevo;
                nuevo.Anterior = actual.Anterior;
                nuevo.Siguiente = actual;
                actual.Anterior = nuevo;
            }

            longitud++;
            ActualizarMedio();
        }
    }

    // Borrar el primer elemento
    public int DeleteFirst()
    {
        if (cabeza == null)
        {
            throw new InvalidOperationException("Lista vacía");
        }

        int valor = cabeza.Valor;
        cabeza = cabeza.Siguiente;
        if (cabeza != null)
        {
            cabeza.Anterior = null;
        }
        else
        {
            cola = null;
        }

        longitud--;
        ActualizarMedio();
        return valor;
    }

    // Borrar el último elemento
    public int DeleteLast()
    {
        if (cola == null)
        {
            throw new InvalidOperationException("Lista vacía");
        }

        int valor = cola.Valor;
        cola = cola.Anterior;
        if (cola != null)
        {
            cola.Siguiente = null;
        }
        else
        {
            cabeza = null;
        }

        longitud--;
        ActualizarMedio();
        return valor;
    }

    // Borrar un valor específico
    public bool DeleteValue(int value)
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            if (actual.Valor == value)
            {
                if (actual == cabeza)
                {
                    DeleteFirst();
                }
                else if (actual == cola)
                {
                    DeleteLast();
                }
                else
                {
                    actual.Anterior.Siguiente = actual.Siguiente;
                    actual.Siguiente.Anterior = actual.Anterior;
                    longitud--;
                    ActualizarMedio();
                }
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }

    // Obtener el valor del nodo central
    public int GetMiddle()
    {
        if (medio == null)
        {
            throw new InvalidOperationException("Lista vacía");
        }
        return medio.Valor;
    }

    // Mezclar dos listas en orden
    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        if (listA == null || listB == null)
        {
            throw new InvalidOperationException("Una o ambas listas son nulas");
        }

        // Lógica de mezcla de las listas según la dirección (ascendente o descendente)
        // ...
    }

    // Método auxiliar para actualizar el nodo medio
    private void ActualizarMedio()
    {
        if (longitud == 0)
        {
            medio = null;
        }
        else
        {
            Nodo actual = cabeza;
            for (int i = 0; i < longitud / 2; i++)
            {
                actual = actual.Siguiente;
            }
            medio = actual;
        }
    }
}

public class ListaDobleTests
{
    public object Assert { get; private set; }

    public void InsertInOrder_Test()
    {
        ListaDoble lista = new ListaDoble();
        lista.InsertInOrder(10);
        lista.InsertInOrder(5);
        lista.InsertInOrder(20);

        Assert.AreEqual(5, lista.DeleteFirst());
        Assert.AreEqual(10, lista.GetMiddle());
        Assert.AreEqual(20, lista.DeleteLast());
    }

    // Otras pruebas para DeleteFirst, DeleteLast, GetMiddle, etc.
}
