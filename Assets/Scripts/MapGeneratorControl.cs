using UnityEngine;
public class MapGeneratorControl : MonoBehaviour
{
    [Header("Archivo de Texto")]
    public TextAsset txtMap;
    [Header("Arreglo de Filas y Columnas")]
    public string[] arrayRows;
    public string[] arraysColumns;
    [Header("Arreglo de Sprites")]
    public Sprite[] arraySprites;
    [Header("Prefab")]
    public GameObject objectMapPrefab;
    [Header("Posiciones")]
    public Vector2 positionInitial;
    public Vector2 separationFromMapParts;
    void Start()
    {
        DrawMap();
    }
    void DrawMap()
    {
        int index = 0;
        GameObject currentMapPart;
        Vector2 positionToCreateMapPart;
        arrayRows = txtMap.text.Split('\n');
        for (int i = 0; i < arrayRows.Length; ++i)
        {
            arraysColumns = arrayRows[i].Split(',');
            for (int j = 0; j < arraysColumns.Length; ++j)
            {
                index = int.Parse(arraysColumns[j]);

                positionToCreateMapPart = new Vector2(positionInitial.x + separationFromMapParts.x * j,
                positionInitial.y - separationFromMapParts.y * i);

                currentMapPart = Instantiate(objectMapPrefab, positionToCreateMapPart, transform.rotation);
                currentMapPart.GetComponent<MapPartController>().SetSprite(arraySprites[index]);

                currentMapPart.transform.SetParent(this.transform);
            }
        }
    }
}
