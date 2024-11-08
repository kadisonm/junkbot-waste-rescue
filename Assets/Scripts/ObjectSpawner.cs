using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _hamburger;
    [SerializeField] private GameObject _trashBag;
    [SerializeField] private GameObject _pizzaBox;
    [SerializeField] private GameObject _container;

    private GameObject item;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && item != null) 
        {
            Rigidbody body = item.GetComponent<Rigidbody>();
            body.useGravity = true;
            item = null;
        }

        if (item != null) 
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            item.transform.position += new Vector3(mouseDelta.x, 0, mouseDelta.y);
        }
    }

    public void SpawnItem(string type)
    {
        if (item != null)
        {
            Rigidbody body = item.GetComponent<Rigidbody>();
            body.useGravity = true;
            item = null;
        }

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

        item = Instantiate(toSpawn, transform.position, Quaternion.identity);
    }  
}
