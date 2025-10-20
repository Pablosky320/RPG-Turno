using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Inventario
{
    int[] i;
    public List<Item> items = new List<Item>();
    public void añadirItem(Item item)
    {
        items.Add(item);
    }

    public void removerItem(Item item) 
    {
        items.Remove(item);
    }
    
    public void BuscarPorNombre(string nombre)
    {
        foreach (var item in items)
        {
            if (item.name == nombre)
                Debug.Log("Objeto" + nombre + "encontrado en el inventario");
        }
    }
}
