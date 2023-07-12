using UnityEngine;

public class DetectEnemyPassing : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} detected {other.name}");
        ship.GetComponent<AvoidShit>().RotateToAvoidEnemy(name);
    }
}
