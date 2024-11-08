using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTree : MonoBehaviour
{
    Animator _animator;

    private BehaviorTree _tree;

    [SerializeField] private GameObject _greenBin;
    [SerializeField] private GameObject _redBin;
    [SerializeField] private GameObject _yellowBin;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        _tree = new BehaviorTree(new Selector(new List<Node> {
            // Petting
            new Leaf(delegate() 
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Physics.Raycast(ray.origin, ray.direction, out hit);

                    if (hit.collider != null) 
                    {
                        if (hit.collider.gameObject.tag == "Robot") {
                            SetAnimatorBool("Petting", true);
                            return NodeState.SUCCESS;
                        }
                    }
                }

                if (_animator.GetBool("Petting"))
                    _animator.SetTrigger("Happy");

                SetAnimatorBool("Petting", false);

                return NodeState.FAILURE;
            }),
            // Pick up
            new Leaf(delegate() 
            {
                if (Input.GetMouseButton(1))
                {
                    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                    gameObject.transform.position += new Vector3(mouseDelta.x, 0, mouseDelta.y) * Time.deltaTime * 30;

                    if (_animator.GetBool("Falling") == false)
                        gameObject.transform.position += new Vector3(0, 2, 0);

                    SetAnimatorBool("Falling", true);

                    return NodeState.SUCCESS;
                }

                if (_animator.GetBool("Falling"))
                    gameObject.transform.position -= new Vector3(0, 2, 0);
                
                SetAnimatorBool("Falling", false);

                return NodeState.FAILURE;
            }),
            // Trash finding
            new Sequence(new List<Node> {
                // Find and walk to rubbish
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
                }),
                //Pick up rubbish, move to bin, and drop rubbish
                new Leaf(delegate() 
                {
                    GameObject found = GameObject.FindWithTag("Item");

                    if (found) {
                        GameObject target = _redBin;

                        if (found.GetComponent<Yellow>()) 
                            target = _yellowBin;
                        else if (found.GetComponent<Green>())
                            target = _greenBin;

                        bool reached = moveTowards(target.transform.position, 2.5f);

                        if (reached)
                        {
                            Destroy(found);
                            return NodeState.SUCCESS;
                        } 
                        else {
                            found.GetComponent<Rigidbody>().useGravity = false;
                            found.transform.position = transform.position + (-transform.forward) + new Vector3(0, 5, 0);
                        }
                    }
                    else {
                        return NodeState.FAILURE;
                    }

                    return NodeState.RUNNING;
                }),
            }),
            // Idle
            new Leaf(delegate() 
            {
                Vector3 direction = (transform.position - Camera.main.transform.position);

                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                lookRotation.x = 0;
                lookRotation.z = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 200 * Time.deltaTime);

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
            SetAnimatorBool("Walking", true);
            
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            lookRotation.x = 0;
            lookRotation.z = 0;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 200 * Time.deltaTime);

            transform.position += -transform.forward * 5 * Time.deltaTime;   
            return false; 
        } else {
            SetAnimatorBool("Walking", false);
            return true;
        }
    }

    private void SetAnimatorBool(string name, bool value)
    {
        if (_animator.GetBool(name) != value) {
            _animator.SetBool(name, value);
        }
    }
}
