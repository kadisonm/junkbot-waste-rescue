using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTree : MonoBehaviour
{
    Animator _animator;

    private BehaviorTree _tree;

    void Awake()
    {
        _animator = GetComponent<Animator>();

        _tree = new BehaviorTree(new Selector(new List<Node> {
        new Sequence(new List<Node> {

        }),
        new Leaf(delegate() 
        {
            return NodeState.SUCCESS;
        })
    }));
    }

    void Update()
    {
        _tree.Tick();
    }
}
