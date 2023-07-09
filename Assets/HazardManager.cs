using System.Collections;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hazards;
    
    private void Start()
    {
        StartCoroutine(ActivateHazard());
    }

    private IEnumerator ActivateHazard()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 8));

            Hazards hazard = hazards[Random.Range(0, hazards.Length)].GetComponent<Hazards>();
            if (!hazard.hazardIsActive)
                hazard.ActivateHazard();
        }
    }
}
