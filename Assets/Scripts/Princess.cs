using UnityEngine;

public class Princess : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D princessCollider)
    {
        var knight = princessCollider.gameObject.GetComponent<Knight>();

        if (knight != null)
        {
            GameController.Instance.PrincessFound();
        }
    }
}