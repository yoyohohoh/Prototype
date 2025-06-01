using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCStateBase
{
    protected NPCController npcController;

    public NPCStateBase(NPCController npcController)
    {
        this.npcController = npcController;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}



