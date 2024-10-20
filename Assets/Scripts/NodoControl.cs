using UnityEngine;
public class AdjacentNodeInfo
{
    public NodoControl node;
    public float weight;

    public AdjacentNodeInfo(NodoControl node, float weight)
    {
        this.node = node;
        this.weight = weight;
    }
}
public class NodoControl : MonoBehaviour
{
    public MyList<NodoControl> adjacentNodes;
    public MyList<float> adjacentWeights;
    void Awake()
    {
        adjacentNodes = new MyList<NodoControl>();
        adjacentWeights = new MyList<float>();
    }
    public void AddAdjacentNode(NodoControl adjacentNode, float weight)
    {
        adjacentNodes.Add(adjacentNode);
        adjacentWeights.Add(weight);
    }
    public AdjacentNodeInfo MovedAdjacentNode()
    {
        int index = Random.Range(0, adjacentNodes.Count());
        float weight = adjacentWeights.Get(index);
        NodoControl nextNode = adjacentNodes.Get(index);
        return new AdjacentNodeInfo(nextNode, weight);
    }
}
