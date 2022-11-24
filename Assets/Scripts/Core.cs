using System.Collections.Generic;
using States;
using UI;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private Model _model;
    [SerializeField] private UIController _uiController;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private Fabric _fabric;


    public Model model => _model;
    public UIController uiController => _uiController;
    public Fabric fabric => _fabric;
    public List<Sprite> sprites => _sprites;


    public List<Slot> slots;

    private StateMachine<Core> _stateMachine;
    
    private void Start()
    {
        _uiController.Init();
        model.Init();
        slots = new List<Slot>(104);
        _stateMachine = new StateMachine<Core>(new SetLevelTaskState(this));
    }
}

