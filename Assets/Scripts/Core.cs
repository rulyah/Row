using System;
using System.Collections.Generic;
using Configs;
using States;
using UI;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private UIController _uiController;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Factory _factory;


    public Model model => _model;
    public UIController uiController => _uiController;
    
    
    public Factory factory => _factory;
    public List<Sprite> sprites => _sprites;


    public List<Slot> slots;

    private StateMachine<Core> _stateMachine;
    
    private void Start()
    {
        _uiController.Init();
        model.Init();
        slots = new List<Slot>(104);
        _stateMachine = new StateMachine<Core>(new CreateSlotsState(this));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameConfig.isVictory = true;
            Debug.Log(GameConfig.isVictory.ToString());
        }
    }
}

