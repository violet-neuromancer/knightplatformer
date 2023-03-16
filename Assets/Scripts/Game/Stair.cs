using UnityEngine;

namespace Game
{
    public class Stair : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D colliderStair)
        {
            var knight = colliderStair.gameObject.GetComponent<Knight>();
            if (knight != null) knight.OnStair = true;
        }

        private void OnTriggerExit2D(Collider2D colliderStair)
        {
            var knight = colliderStair.gameObject.GetComponent<Knight>();
            if (knight != null) knight.OnStair = false;
        }
    }
}