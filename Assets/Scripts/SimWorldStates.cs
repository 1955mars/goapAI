using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimWorldState
{
    public string key;
    public int value;
}

public class SimWorldStates
{
    public Dictionary<string, int> simStates;

    public SimWorldStates()
    {
        simStates = new Dictionary<string, int>();
    }

    public bool HasState(string key)
    {
        return simStates.ContainsKey(key);
    }

    void AddState(string key, int value)
    {
        simStates.Add(key, value);
    }

    public void UpdateState(string key, int value)
    {
        if(simStates.ContainsKey(key))
        {
            simStates[key] += value;
            if(simStates[key] <= 0)
            {
                RemoveState(key);
            }
        }
        else
        {
            simStates.Add(key, value);
        }
    }

    public void RemoveState(string key)
    {
        if(simStates.ContainsKey(key))
        {
            simStates.Remove(key);
        }
    }

    public void SetState(string key, int value)
    {
        if(simStates.ContainsKey(key))
        {
            simStates[key] = value;
        }
        else
        {
            simStates.Add(key, value);
        }
    }

    public Dictionary<string, int> GetStates()
    {
        return simStates;
    }

}
