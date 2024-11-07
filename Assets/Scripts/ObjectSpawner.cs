using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _hamburger;
    [SerializeField] private GameObject _trashBag;
    [SerializeField] private GameObject _pizzaBox;
    [SerializeField] private GameObject _container;

    public void SpawnItem(string type)
    {
        GameObject toSpawn = null;

        if (type == "Hamburger")
        {
            toSpawn = _hamburger;
        }

        if (type == "TrashBag")
        {
            toSpawn = _trashBag;
        }

        if (type == "PizzaBox")
        {
            toSpawn = _pizzaBox;
        }

        if (type == "Container")
        {
            toSpawn = _container;
        }

        if (toSpawn == null) {
            return;
        }
    }
    
}
