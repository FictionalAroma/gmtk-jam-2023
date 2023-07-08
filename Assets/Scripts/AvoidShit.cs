using System.Collections;
using UnityEngine;

public class AvoidShit : MonoBehaviour
{
    [SerializeField] private float minRot = 10f;
    [SerializeField] private float maxRot = 15f;
    [SerializeField] private float rotateTime = 0.5f; // time taken to rotate
    [SerializeField] private float waitTime = 2f; // time to wait before rotating back

    private enum Direction
    {
        TopLeft,
        BotRight,
    }

    private bool isRotating = false;
    private Quaternion originalRotation;

    public void RotateToAvoidEnemy(string otherName)
    {
        if (isRotating) return; // already rotating, ignore further rotation requests

        if (!System.Enum.TryParse(otherName.Replace("Col", ""), out Direction direction))
            return;

        float rotAmount = Random.Range(minRot, maxRot);
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, (direction is Direction.TopLeft or Direction.BotRight ? 1 : -1) * rotAmount);

        StartCoroutine(RotateShip());

        IEnumerator RotateShip()
        {
            isRotating = true;
            originalRotation = transform.rotation; // store original rotation

            // Rotate to target and then back
            yield return Rotate(targetRotation);
            yield return new WaitForSeconds(waitTime); // wait for some time
            yield return Rotate(originalRotation);

            isRotating = false;
        }

        IEnumerator Rotate(Quaternion toRotation)
        {
            float startTime = Time.time;

            while (Time.time < startTime + rotateTime)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, (Time.time - startTime) / rotateTime);
                yield return null;
            }

            transform.rotation = toRotation; // ensure final rotation
        }
    }
}