using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameResources.Save
{
    public class SaveProgress : MonoBehaviour
    {
        [SerializeField]
        private Object[] saveProgresses;

        private readonly List<ISaveProgress> _saves = new List<ISaveProgress>();
        
        private void Start()
        {
            foreach (var obj in saveProgresses)
            {
                if (obj.GetType().GetInterfaces().Contains(typeof(ISaveProgress)) == false)
                {
                    continue;
                }

                if (obj is not ISaveProgress save)
                {
                    continue;
                }

                _saves.Add(save);
                save.Load();
            }
        }

        private void OnDestroy()
        {
            foreach (var save in _saves)
            {
                save.Save();
            }
        }
    }
}
