using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.ReceiveAttack(_player.CurrentAttack);
        }
    }
}
