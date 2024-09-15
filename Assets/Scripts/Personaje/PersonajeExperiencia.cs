using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    //Para coger las referencias del panel:
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    //Algunas Variables para definir la Experiencia,
    //como el nivelmaximo (nivelMax), cuanta experiencia se necesita
    //para subir de nivel (expBase), coeficiente que incrementa
    //exponencialmente la experiencia por cada nivel (valorIncremental)
    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    //Variables para controlar la experiencia:
    private float expActual;
    private float expRequeridaSiguienteNivel;

    //El nivel por el cual empezamos, la exp requerida para el 
    //siguiente nivel y esta misma exp en el panel:
    private void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    //Para probar la logica de Experiencia:
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }

    //Para añadir experiencia segun cierta cantidad:
    //Primero verificaremos (primer if) que la experiencia que estamos pasando
    //no sea 0. Si es 0, no hacemos nada. En realidad si fuera 0
    //no añadiría nada a nuestro código, pero pondremos un check extra.
    //Segundo accedemos a expActual y le sumamos la expObtenida (esto se
    //hace ya fuera del if). 
    //expActual es lo que siempre mostraremos en el panel de stats (stats.ExpActual),
    //que ira desde 0 hasta expRequerida. Y cuando expActual = expRequerida,
    //se subirá un nivel y expActual = 0, para volver a empezar (segundo if).
    //También se contempla (else if) el caso en que expActual supere la expRequerida
    //para subir de nivel, entonces se calculará la diferencia entre ambos
    //y esa será la cantidad que se necesita para subir de nivel.
    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida <= 0) return;
        expActual += expObtenida;
        stats.ExpActual = expActual;

        if (expActual == expRequeridaSiguienteNivel)
        {
            ActualizarNivel();
        }
        else if (expActual > expRequeridaSiguienteNivel)
        {
            float dif = expActual - expRequeridaSiguienteNivel;
            ActualizarNivel();
            AñadirExperiencia(dif);
        }

        stats.ExpTotal += expObtenida;
        ActualizarBarraExp();
    }

    //Para subir de nivel, actualizaremos la exp requerida para subir, el propio
    //nivel, actualizar el valor en el panel y los
    //puntos disponibles a gastar en atributos;
    //solo haremos esto con la condicion siguiente:
    private void ActualizarNivel()
    {
        if(stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    //Para actualizar la barra de exp:
    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expRequeridaSiguienteNivel);
    }
}
