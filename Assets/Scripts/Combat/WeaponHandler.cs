using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private Weapon _weapon;

    public void EnableWeapon()
    {
        _weapon.gameObject.SetActive(true);
    }

    public void DisableWeapon()
    {
        _weapon.gameObject.SetActive(false);
    }

}
