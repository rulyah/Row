using System.Collections.Generic;
using States;
using UnityEngine;

public class Core : MonoBehaviour
{
    public List<Slot> slots;
    private StateMachine<Core> _stateMachine;
    
    private void Start()
    {
        slots = new List<Slot>(104);
        _stateMachine = new StateMachine<Core>(new LoadConfigsState(this));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Model.isVictory = true;
            //Debug.Log(Model.isVictory.ToString());
        }
    }
}

