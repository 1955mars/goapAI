using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }
}

public class Agent : MonoBehaviour
{
    public List<Action> actions = new List<Action>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    //GPlanner planner;

    Queue<Action> actionQueue;
    public Action currentAction;
    SubGoal currentGoal;


    // Start is called before the first frame update
    void Start()
    {
        Action[] acts = this.GetComponents<Action>();

        foreach(Action a in acts)
        {
            actions.Add(a);
        }
    }

    void LateUpdate()
    {
        
    }
}
