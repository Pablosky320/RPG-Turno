using UnityEngine;

public class Jugador : MonoBehaviour
{
    int experiencia { get; set; }
    int recursoActual;
    int recursoMax;
    ClaseJugador claseJugador;
    private int experienciaSiguienteNivel;

    public int GetExperiencia() { return experiencia; }

    void ganarExperiencia(int xp) { }
    void SubirNivel() { }
}
