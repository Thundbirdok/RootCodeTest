using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameResources.AddressablesHelper
{
    public static class AddressablesLoader
    {
        /// <summary>
        /// Instantiate asset reference and try get component of type
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="parent"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async static Task<Component> InstantiateAndTryGetComponent(AssetReference asset, Transform parent, Type type)
        {
            var instance = await asset.InstantiateAsync(parent).Task;

            if (instance.TryGetComponent(type, out var component))
            {
                return component;
            }

            Unload(asset, instance);
            
            return null;
        }

        public static void Unload(AssetReference asset, GameObject instance)
        {
            instance.SetActive(false);
            
            asset.ReleaseInstance(instance);
        }
    }
}
