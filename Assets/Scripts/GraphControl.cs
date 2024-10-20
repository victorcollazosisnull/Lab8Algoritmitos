using UnityEngine;

public class GraphControl : MonoBehaviour
{
    public MyList<NodoControl> AllNodes;
    [Header("Archivo de Texto")]
    public TextAsset textNodesPosition;
    public TextAsset textNodesConnections;
    [Header("Arreglo de Filas y Columnas")]
    public string[] arrayNodesRows;
    public string[] arraysNodesColumns;
    [Header("Prefab")]
    public GameObject objectNodePrefab;
    public string[] arrayNodesConnections;
    public EnemyControl currentEnemy;
    private void Awake()
    {
        AllNodes = new MyList<NodoControl>();
    }
    void Start()
    {
        DrawNodes();
        ConnectNodes();
        SetInitialNode();
        currentEnemy.allNodes = AllNodes;
    }
    void DrawNodes()
    {
        GameObject curretNode;
        arrayNodesRows = textNodesPosition.text.Split('\n');
        for (int i = 0; i < arrayNodesRows.Length; ++i)
        {
            arraysNodesColumns = arrayNodesRows[i].Split(';');
            Vector2 positionToCreate = new Vector2(float.Parse(arraysNodesColumns[0]), float.Parse(arraysNodesColumns[1]));
            curretNode = Instantiate(objectNodePrefab, positionToCreate, transform.rotation);
            NodoControl nodoControl = curretNode.GetComponent<NodoControl>();
            AllNodes.Add(nodoControl);
        }
    }
    void ConnectNodes()
    {
        arrayNodesConnections = textNodesConnections.text.Split('\n');

        for (int i = 0; i < arrayNodesConnections.Length; ++i)
        {
            string[] connections = arrayNodesConnections[i].Split(';');
            NodoControl currentNode = AllNodes.Get(i);

            for (int j = 0; j < connections.Length; ++j)
            {
                string[] nodeAndWeight = connections[j].Split(','); 
                int adjacentNodeIndex = int.Parse(nodeAndWeight[0]);
                float weight = float.Parse(nodeAndWeight[1]); 

                if (adjacentNodeIndex >= 0 && adjacentNodeIndex < AllNodes.Count())
                {
                    NodoControl adjacentNode = AllNodes.Get(adjacentNodeIndex);
                    currentNode.AddAdjacentNode(adjacentNode, weight);
                }
            }
        }
    }
    void SetInitialNode()
    {
        int position = Random.Range(0, AllNodes.Count());
        NodoControl targetNode = AllNodes.Get(position); 
        currentEnemy.SetNewPosition(targetNode.transform.position);
    }
}
