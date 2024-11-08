using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTree : MonoBehaviour
{
    Animator _animator;

    private BehaviorTree _tree;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _tree = new BehaviorTree(new Selector(new List<Node> {
        new Sequence(new List<Node> {
            new Leaf(delegate() 
            {
                GameObject found = GameObject.FindWithTag("Item");

                if (found) {
                    bool reached = moveTowards(found.transform.position, 1);

                    if (reached)
                    {
                        return NodeState.SUCCESS;
                    }
                }
                else {
                    return NodeState.FAILURE;
                }

                return NodeState.RUNNING;
            })
        }),
        new Leaf(delegate() 
        {
            return NodeState.SUCCESS;
        })
    }));
    }

    private void Update()
    {
        _tree.Tick();
    }

    private bool moveTowards(Vector3 newPos, float stopWithin)
    {
        Vector3 direction = (transform.position - newPos);

        if (direction.magnitude > stopWithin)
        {
            if (_animator.GetBool("Walking") == false) {
                _animator.SetBool("Walking", true);
            }
            
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            lookRotation.x = 0;
            lookRotation.z = 0;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 150 * Time.deltaTime);

            transform.position += -transform.forward * 3 * Time.deltaTime;   
            return false; 
        } else {
            _animator.SetBool("Walking", false);
            return true;
        }
    }
}
