using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class CraftingTable : MonoBehaviour
{
    [SerializeField] List<CraftingRecipeSO> craftingRecipeSOList;
    [SerializeField] BoxCollider placeItemsAreaBoxCollider;
    [SerializeField] private Transform itemSpawnPoint;
    [SerializeField] private GameObject smoke;

    private CraftingRecipeSO craftingRecipeSO;

    private void Awake()
    {
        NextRecipe();
    }

    public void NextRecipe()
    {
        if(craftingRecipeSO == null)
        {
            craftingRecipeSO = craftingRecipeSOList[0];
        }
        else
        {
            int index = craftingRecipeSOList.IndexOf(craftingRecipeSO);
            index = (index +1) % craftingRecipeSOList.Count;
            craftingRecipeSO = craftingRecipeSOList[index];
        }
        
    }
    public void Craft()
    {
        Debug.Log("Craft");

        Collider[] collidersArray = Physics.OverlapBox(placeItemsAreaBoxCollider.transform.position, placeItemsAreaBoxCollider.size,
            placeItemsAreaBoxCollider.transform.rotation);

        foreach (CraftingRecipeSO recipe in craftingRecipeSOList)
        {
            List<ItemDataSO> inputListItems = new List<ItemDataSO>(recipe.inputItemDataSOList);
            List<GameObject> consumeItemGameObjectList = new List<GameObject>();

            foreach (Collider collider in collidersArray)
            {
                Debug.Log(collider);
                if (collider.TryGetComponent(out PickUpInteract pickUpInteract))
                {
                    inputListItems.Remove(pickUpInteract.GetItemDataSO());
                    consumeItemGameObjectList.Add(pickUpInteract.gameObject);
                    Debug.Log(inputListItems);
                }
            }

            if (inputListItems.Count == 0)
            {
                Debug.Log("YEs");
                Instantiate(recipe.outputItemDataSO.prefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
                Instantiate(smoke, itemSpawnPoint.position, itemSpawnPoint.rotation);

                foreach (GameObject consumItem in consumeItemGameObjectList)
                {
                    Destroy(consumItem.gameObject);
                }
                return;
            }
        }

        Debug.Log("NO");
    }
}

