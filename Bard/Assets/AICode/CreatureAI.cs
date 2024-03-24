using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class CreatureAI : MonoBehaviour
{

    public Creature myCreature;
    public Creature targetCreature;

    [Header("Config")]
    public LayerMask obstacles;
    [SerializeField] bool searchShortcut =false;
    [SerializeField] bool snapToGrid =false;
    [SerializeField] bool drawDebugLines;

    [Header("Pathfinding")]
    Pathfinder<Vector2> pathfinder;
    List <Vector2> path;
    List<Vector2> pathLeftToGo= new List<Vector2>();
    [SerializeField] float gridSize = 0.5f;

    // State machine
    CreatureAIState currentState;
    public CreatureAIIdleState idleState{get; private set;}
    public CreatureAIHugState hugState{get; private set;}

    public void ChangeState(CreatureAIState newState) {
        currentState = newState;
        currentState.BeginStateBase();
    }


    // Start is called before the first frame update
    void Start()
    {
        /*idleState = new CreatureAIIdleState(this);
        hugState = new CreatureAIHugState(this);
        currentState = idleState;*/

        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        GetMoveCommand(GetTarget().transform.position);

        if (pathLeftToGo.Count > 0) //if the target is not yet reached
        {
            Vector3 dir = (Vector3)pathLeftToGo[0]-myCreature.transform.position ;
            myCreature.MoveCreature(dir.normalized);
            if ((Vector2)myCreature.transform.position == pathLeftToGo[0]) 
            {
                myCreature.transform.position = pathLeftToGo[0];
                pathLeftToGo.RemoveAt(0);
            }
        }

        if (drawDebugLines)
        {
            for (int i=0;i<pathLeftToGo.Count-1;i++) //visualize your path in the sceneview
            {
                Debug.DrawLine(pathLeftToGo[i], pathLeftToGo[i+1]);
            }
        }
    }

    void GetMoveCommand(Vector2 target)
    {
       Vector2 closestNode = GetClosestNode(myCreature.transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            if (searchShortcut && path.Count>0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }

        }

    }

    public Creature GetTarget(){
        return targetCreature;
    }

    Vector2 GetClosestNode(Vector2 target) 
    {
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    float GetDistance(Vector2 A, Vector2 B) 
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }


    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();
        
        for (int i=0;i<path.Count;i++)
        {
            newPath.Add(path[i]);
            for (int j=path.Count-1;j>i;j-- )
            {
                if (!Physics2D.Linecast(path[i],path[j], obstacles))
                {
                    
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }
}
