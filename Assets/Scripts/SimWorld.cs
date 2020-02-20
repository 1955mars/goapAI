using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SimWorld
{
    private static readonly SimWorld instance = new SimWorld();
    private static SimWorldStates worldStates;
    
    static SimWorld()
    {
        worldStates = new SimWorldStates();
    }

    private SimWorld()
    {

    }

    public static SimWorld Instance
    {
        get { return instance; }
    }

    public SimWorldStates GetWorldStates()
    {
        return worldStates;
    }
}
