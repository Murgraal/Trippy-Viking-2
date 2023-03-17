using UnityEngine;
using static Code.ParallaxFunctions;

namespace Code
{
    public class ParallaxInit : MonoBehaviour
    {
        void Start()
        {
            var layerCounter = -1;
            foreach (var parallax in GetComponentsInChildren<Parallax>())
            {
                parallax.Init();
                SetOrderInLayerForAll(layerCounter,parallax.Renderers);
                layerCounter++;
            }
        }
    
    }
}
