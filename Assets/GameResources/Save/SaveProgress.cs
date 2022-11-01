using System.Linq;
using UnityEngine;

namespace GameResources.Save
{
    public class SaveProgress : MonoBehaviour
    {
        [SerializeField]
        private Object[] saveProgresses;
        
        private void OnDestroy()
        {
            foreach (var obj in saveProgresses)
            {
                if (obj.GetType().GetInterfaces().Contains(typeof(ISaveProgress)))
                {
                    (obj as ISaveProgress).Save();
                }
            }
        }
    }
}
