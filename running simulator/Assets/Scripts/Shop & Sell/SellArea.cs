using UnityEngine;

public class SellArea : MonoBehaviour
{
    [SerializeField] private ParticleSystem onTriggerVFX;

    private void OnTriggerEnter(Collider other)
    {
        const int VFX_SCALE = 2;

        ParticleSystem vfx = Instantiate(onTriggerVFX, gameObject.transform.position, Quaternion.identity);
        vfx.gameObject.GetComponent<Transform>().localScale = Vector3.one * VFX_SCALE;

        SellSteps();
    }

    private void SellSteps()
    {
        // Deplete steps and add to coins
        Player.Instance.AddCoins(Mathf.FloorToInt(Player.Instance.Steps));
        Player.Instance.AddSteps(Player.Instance.Steps * -1);
    }
}
