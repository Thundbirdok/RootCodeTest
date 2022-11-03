using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameResources.AddressablesHelper
{
    public static class AddressablesLoader
    {
        public async static Task<Component> Instantiate(AssetReference asset, Transform parent, Type type)
        {
            var obj = await asset.InstantiateAsync(parent).Task;

            if (obj.TryGetComponent(type, out var component))
            {
                return component;
            }

            return null;
        }

        public static void Unload(AssetReference asset, GameObject instance)
        {
            instance.SetActive(false);
            
            asset.ReleaseInstance(instance);
        }
    }
}
