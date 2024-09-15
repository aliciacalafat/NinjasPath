using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : Singleton<CraftingManager>
{
    //Referencia del prefab RecetaTarjeta y donde se va a crear:
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    //Referencia receta info.
    [Header("Receta Info")]
    [SerializeField] private Image primerMaterialIcono;
    [SerializeField] private Image segundoMaterialIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombre;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidad;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidad;
    [SerializeField] private TextMeshProUGUI recetaMensaje;
    [SerializeField] private Button buttonCraftear;

    //Referencia item resultado
    [Header("Item Resultado")]
    [SerializeField] private Image itemResultadoIcono;
    [SerializeField] private TextMeshProUGUI itemResultadoNombre;
    [SerializeField] private TextMeshProUGUI itemResultadoDescripcion;

    //Para poder cargar las recetas en recetaContenedor, necesitamos
    //una referencia de la lista de recetas.
    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    //Para guardar la info de la receta que se está seleccionando, creamos la siguiente propiedad:
    public Receta RecetaSeleccionada { get; set; }

    //Para llamar al metodo de cargarRecetas:
    private void Start()
    {
        CargarRecetas();
    }

    //Para crear las recetas en el panel:
    private void CargarRecetas()
    {
        for(int i = 0; i < recetas.Recetas.Length; i++)
        {
            RecetaTarjeta receta = Instantiate(recetaTarjetaPrefab, recetaContenedor);
            receta.ConfigurarRecetaTarjeta(recetas.Recetas[i]);
        }
    }

    //Para mostrar recetas:
    public void MostrarReceta(Receta receta)
    {
        RecetaSeleccionada = receta;
        primerMaterialIcono.sprite = receta.Item1.Icono;
        segundoMaterialIcono.sprite = receta.Item2.Icono;
        primerMaterialNombre.text = receta.Item1.Nombre;
        segundoMaterialNombre.text = receta.Item2.Nombre;
        primerMaterialCantidad.text =
            $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item1.ID)}/{receta.Item1CantidadRequerida}";
        segundoMaterialCantidad.text =
            $"{Inventario.Instance.ObtenerCantidadDeItems(receta.Item2.ID)}/{receta.Item2CantidadRequerida}";
        
        if (SePuedeCraftear(receta))
        {
            recetaMensaje.text = "Receta Disponible";
            buttonCraftear.interactable = true;
        }
        else
        {
            recetaMensaje.text = "Necesitas mas Materiales";
            buttonCraftear.interactable = false;
        }

        itemResultadoIcono.sprite = receta.ItemResultado.Icono;
        itemResultadoNombre.text = receta.ItemResultado.Nombre;
        itemResultadoDescripcion.text = receta.ItemResultado.DescripcionItemCrafting();
    }

    //Para craftear:
    public bool SePuedeCraftear(Receta receta)
    {
        if (Inventario.Instance.ObtenerCantidadDeItems(receta.Item1.ID) >= receta.Item1CantidadRequerida
        && Inventario.Instance.ObtenerCantidadDeItems(receta.Item2.ID) >= receta.Item2CantidadRequerida)
        {
            return true;
        }

        return false;
    }

     //Para craftear item:
    public void Craftear()
    {
        for (int i = 0; i < RecetaSeleccionada.Item1CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item1.ID);
        }

        for (int i = 0; i < RecetaSeleccionada.Item2CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item2.ID);
        }

        Inventario.Instance.AñadirItem(RecetaSeleccionada.ItemResultado, RecetaSeleccionada.ItemResultadoCantidad);
        MostrarReceta(RecetaSeleccionada);
    }
}
