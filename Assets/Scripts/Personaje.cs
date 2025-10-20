using UnityEngine;

public class Personaje : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string nombre { get; set; }
    int nivel { get; set; }
    int vidaActual { get; set; }
    int vidaMax { get; set; }
    float ataqueBase { get; set; }
    float DefensaBase { get; set; }
    float Velocidad { get; set; }
    float speed { get; set; }

    

    public void recibirDaño(int cantidad)
    {
        Debug.Log("el personaje recibe " + cantidad + "de daño");
    }

    // Update is called once per frame
    
    public void movement(int speed)
    { 
        this.speed = speed; 
        if (speed == 0)
        {
            
        }
    }


    void EstaVivo()
    {

    }
    //void ElegirAccion(Accion Contexto)
}
