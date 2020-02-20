using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Action : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public GameObject targetTag; //Pickup the gameobjects using tag
    public float duration = 0;

    public SimWorldState[] preConditions; //To get the information through inspector
    public SimWorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public SimWorldStates agentBeliefs;

    public bool running = false;

    public Action()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if(preConditions != null)
        {
            foreach(SimWorldState ws in preConditions)
            {
                preconditions.Add(ws.key, ws.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (SimWorldState ws in afterEffects)
            {
                effects.Add(ws.key, ws.value);
            }
        }
    }

    public bool IsAchievalble()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if(!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
