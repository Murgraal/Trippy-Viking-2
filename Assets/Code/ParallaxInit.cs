using UnityEngine;

namespace Code
{
    public class ParallaxInit : MonoBehaviour
    {
        void Start()
        {
            var layerCounter = -1;
            foreach (var parallax in GetComponentsInChildren<Parallax>())
            {
                parallax.SetOrderInLayerForAll(layerCounter);
                layerCounter++;
            }
        }
    
    }
}
