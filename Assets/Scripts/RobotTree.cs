using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTree : MonoBehaviour
{
    private BehaviorTree _tree = new BehaviorTree(new Sequence(new List<Node> {
        
    }));

    void Update()
    {
        _tree.Tick();
    }
}
