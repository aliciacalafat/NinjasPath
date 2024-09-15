using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Mision : ScriptableObject
{
    //Para notificar el evento una vez completada la mision:
    public static Action<Mision> EventoMisionCompletada;

    [Header("Info")]
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    [Header("Recompensas")]
    public int RecompensaOro;
    public float RecompensaExp;
    public MisionRecompensaItem RecompensaItem;

    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool MisionCompletadaCheck;
    [HideInInspector] public bool MisionAceptada;

    //Para añadir el progreso de las misiones al panel:
    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarMisionCompletado();
    }

    //Para verificar que al añadir cantidad a CantidadActual hemos
    //llegado o no a CantidadObjetivo:
    private void VerificarMisionCompletado()
    {
        if(CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            MisionCompletada();
        }
    }

    //Vamos a resetear valores de las misiones con la siguiente función, 
    //a través del MisionManager (en vez de solo hacerlo aquí como
    //scriptable object) porque allí es donde estamos guardando el array de misiones.
    public void ResetMision()
    {
        MisionCompletadaCheck = false;
        CantidadActual = 0;
    }

    //Para completar la mision una vez que hemos llegado a la CantidadObjetivo.
    //Lanzar el evento notificando que la mision ha sido completada.
    private void MisionCompletada()
    {
        if(MisionCompletadaCheck)
        {
            return;
        }
        MisionCompletadaCheck = true;
        EventoMisionCompletada?.Invoke(this);
    }
}

[Serializable]
public class MisionRecompensaItem 
{
    public InventarioItem Item;
    public int Cantidad;
}
