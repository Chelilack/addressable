using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class button : MonoBehaviour
{
    public animationObject animationObject;
    bool flag = true;
    [SerializeField] private int numberNewStage;
    [SerializeField] private string animName;
    [SerializeField] private GameObject secondStage;
    [SerializeField] private AssetReference assetReference;
    [SerializeField] private AssetLabelReference firstLabel;
    [SerializeField] private AssetLabelReference secondLabel;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && flag)
        {

            if (animationObject != null)
            {
                animationObject.PlayAnimation(animName); 
            }
            else
            {
                Debug.LogError("Объект с анимацией не назначен!");
            }
            switch (numberNewStage) 
            {
                case 2:
                    SpawnSecondStage();
                    break;
                case 3:
                    SpawnThirdStage();
                    Debug.Log("3-spawned");
                    break;
            }
            
            flag = false;
        }
    }
    private void SpawnSecondStage() 
    {
        //Instantiate(secondStage);
        assetReference.LoadAssetAsync<GameObject>().Completed +=
            asyncOperationHandle => {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Instantiate(asyncOperationHandle.Result);
                }
                else
                {
                    Debug.Log("Failed to load!");
                }
            };
    }
    private void SpawnThirdStage() 
    {
        Addressables.LoadAssetAsync<GameObject>(firstLabel).Completed +=
            asyncOperationHandle => {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Instantiate(asyncOperationHandle.Result);
                }
                else
                {
                    Debug.Log("Failed to load!");
                }
            };
        Addressables.LoadAssetsAsync<GameObject>(secondLabel, null).Completed +=
            asyncOperationHandle => {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    IList<GameObject> loadedObjects = asyncOperationHandle.Result;
                    foreach (GameObject obj in loadedObjects)
                    {
                        Instantiate(obj);
                    }
                }
                else
                {
                    Debug.Log("Failed to load!");
                }
            };
    }
}
