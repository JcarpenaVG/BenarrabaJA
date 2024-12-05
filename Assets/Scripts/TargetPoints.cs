using UnityEngine;

public class TargetPoints : MonoBehaviour
{
    [SerializeField] private int pointsGained;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Dart"))
        {
            GuayacanManager.Instance.Points += pointsGained;
            collision.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
