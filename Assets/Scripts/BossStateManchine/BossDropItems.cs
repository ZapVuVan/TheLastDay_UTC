using UnityEngine;

public class BossDropItems : MonoBehaviour
{
    [SerializeField] private GameObject itemToDrop;
    [SerializeField] private Vector3 dropOffset = new Vector3(0, 0.5f, 0);

    private void OnEnable() => BossAIController.OnBossDied += Drop;
    private void OnDisable() => BossAIController.OnBossDied -= Drop;

    private void Drop() =>
        Instantiate(itemToDrop, transform.position + dropOffset, Quaternion.identity);
}