using System.Linq;
using TMPro;
using Rand = UnityEngine.Random;
// Jumper

namespace Jumper
{
    public class RandomService : SingletonComponent<RandomService>
    {
        internal float Range(float v1, float v2)
        {
            return Rand.Range(v1, v2);
        }
    }
}