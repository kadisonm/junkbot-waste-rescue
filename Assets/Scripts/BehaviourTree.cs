using System.Collections.Generic;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}

public abstract class Node
{
    public abstract NodeState Evaluate();
}

public abstract class Composite : Node 
{ 
    protected List<Node> children = new List<Node>(); 

    public Composite(List<Node> children) 
    { 
        this.children = children; 
    } 
}

public class Sequence : Composite
{
    public Sequence(List<Node> children) : base(children) {} 

    public override NodeState Evaluate() 
    { 
        foreach(Node child in children) 
        {
            NodeState childState = child.Evaluate();

            if (childState == NodeState.RUNNING) 
            { 
                return NodeState.RUNNING; 
            } 

            if (childState == NodeState.FAILURE) 
            { 
                return NodeState.FAILURE; 
            } 
        }

        return NodeState.SUCCESS; 
    }
}

public class Selector : Composite
{
    public Selector(List<Node> children) : base(children) {} 

    public override NodeState Evaluate() 
    { 
        foreach(Node child in children) 
        {
            NodeState childState = child.Evaluate();

            if (childState == NodeState.RUNNING) 
            { 
                return NodeState.RUNNING; 
            } 
            
            if (childState == NodeState.SUCCESS) 
            { 
                return NodeState.SUCCESS; 
            } 
        }

        return NodeState.FAILURE; 
    }
}

public class Leaf : Node 
{ 
    public delegate NodeState Action();

    private Action _action;

    public Leaf(Action action) 
    { 
        _action = action; 
    } 
    public override NodeState Evaluate() 
    { 
        return _action(); 
    } 
}

public class BehaviorTree
{
    private Node root;

    public BehaviorTree(Node root)
    {
        this.root = root;
    }

    public void Tick()
    {
        root.Evaluate();
    }
}
