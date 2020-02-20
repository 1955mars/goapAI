using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public Action action;

    public Node(Node parent, float cost, Dictionary<string, int> state, Action action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(state);
        this.action = action;
    }
}

public class Planner
{
    public Queue<Action> Plan(List<Action> actions, Dictionary<string, int> goal, SimWorldStates states)
    {
        List<Action> usableActions = new List<Action>();
        foreach(Action a in actions)
        {
            if(a.IsAchievalble())
            {
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0, SimWorld.Instance.GetWorldStates().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if(!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if(cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if(leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<Action> result = new List<Action>();
        Node n = cheapest;
        while (n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<Action> queue = new Queue<Action>();
        foreach(Action a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach(Action a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<Action> usableActions, Dictionary<string, int> goal)
    {
        bool pathFound = false;
        foreach(Action action in usableActions)
        {
            if(action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string,int> effect in action.effects)
                {
                    if(!currState.ContainsKey(effect.Key))
                    {
                        currState.Add(effect.Key, effect.Value);
                    }
                }

                Node node = new Node(parent, parent.cost + action.cost, currState, action);
                
                if(GoalAchieved(goal, currState))
                {
                    leaves.Add(node);
                    pathFound = true;
                }
                else
                {
                    List<Action> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        pathFound = true;
                }
            }
        }
        return pathFound;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> kv in goal)
        {
            if (!state.ContainsKey(kv.Key))
                return false;
        }
        return true;
    }

    private List<Action> ActionSubset(List<Action> actions, Action rmAction)
    {
        List<Action> subset = new List<Action>();
        foreach(Action a in actions)
        {
            if (!a == rmAction)
                subset.Add(a);
        }
        return subset;
    }
}
