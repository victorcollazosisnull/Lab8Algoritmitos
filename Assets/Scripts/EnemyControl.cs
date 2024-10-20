using UnityEngine;
public class EnemyControl : MonoBehaviour
{
    [Header("Movimiento del Enemigo")]
    public Vector2 positionToMove;
    public float speedMove;
    [Header("Vida del Enemigo")]
    public float energy = 50f;
    public float maxEnergy = 50f;
    public float timeNewEnergy = 5f; 
    public float newEnergy = 50f; 
    public bool isResting = false; 
    private float restTimer = 0f;
    private NodoControl currentNode;
    public MyList<NodoControl> allNodes;
    void Update()
    {
        if (isResting)
        {
            restTimer -= Time.deltaTime; 

            if (restTimer <= 0f)
            {
                RecoverEnergy();
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMove, speedMove * Time.deltaTime);
        }

    }
    public void SetNewPosition(Vector2 newPosition)
    {
        positionToMove = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Nodo"))
        {
            currentNode = collision.GetComponent<NodoControl>(); 
            if (currentNode != null)
            {
                AdjacentNodeInfo nextNodeInfo = currentNode.MovedAdjacentNode();
                if (energy >= nextNodeInfo.weight)
                {
                    SetNewPosition(nextNodeInfo.node.transform.position);
                    energy = energy - nextNodeInfo.weight;
                    Debug.Log("Energía: " + energy);
                }
                else
                {
                    Resting();
                }
            }
        }
    }
    private void Resting()
    {
        isResting = true;
        restTimer = timeNewEnergy;
        Debug.Log("Enemigo zzzz...");
    }
    private void RecoverEnergy()
    {
        energy = Mathf.Min(energy + newEnergy, maxEnergy);
        if (energy >= maxEnergy)
        {
            isResting = false;
            Debug.Log("Energía fina: " + energy);

            if (allNodes != null && allNodes.Count() > 0)
            {
                NodoControl nextNode = MoveNextNode();
                if (nextNode != null)
                {
                    SetNewPosition(nextNode.transform.position);
                }
            }
        }
    }
    private NodoControl MoveNextNode()
    {
        for (int i = 0; i < currentNode.adjacentNodes.Count(); i++)
        {
            NodoControl node = currentNode.adjacentNodes.Get(i);
            if (node != currentNode) 
            {
                return node; 
            }
        }
        return null; 
    }
}
