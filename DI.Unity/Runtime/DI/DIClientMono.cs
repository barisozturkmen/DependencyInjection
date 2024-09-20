#if UNITY
using UnityEngine;

namespace DI
{
    public abstract class DIClientMono : MonoBehaviour
    {
        private void Awake()
        {
            DIContext.Injector.InjectDependencies(this);
        }
    }
}
#endif