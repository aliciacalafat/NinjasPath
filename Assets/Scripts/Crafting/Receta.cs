using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Para poder ver la clase en el inspector:
[Serializable] 
public class Receta
{
    //Variables para la receta de items:
    public string Nombre;
    
    [Header("1er Material")]
    public InventarioItem Item1;
    public int Item1CantidadRequerida;
    
    [Header("2do Material")]
    public InventarioItem Item2;
    public int Item2CantidadRequerida;

    [Header("Resultado")]
    public InventarioItem ItemResultado;
    public int ItemResultadoCantidad;
}
