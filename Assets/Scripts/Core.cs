using System.Collections.Generic;
using States;
using UI;
using UnityEngine;

public class Core : MonoBehaviour
{
    public List<Slot> slots;
    private StateMachine<Core> _stateMachine;
    
    private void Start()
    {
        slots = new List<Slot>(104);
        UIController.instance.Init();
        AudioController.instance.Init();
        _stateMachine = new StateMachine<Core>(new LoadConfigsState(this));
    }
}

