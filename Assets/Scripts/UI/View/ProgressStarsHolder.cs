using Extensions;
using UnityEngine;

namespace UI.View
{
    public class ProgressStarsHolder : MonoBehaviour
    {
        [SerializeField] private ProgressStar[] _progressStars;

        public void Show(int level)
        {
            for (int i = 0; i < level; i++)
             _progressStars[i].CompleteStar.transform.Activate();
        }

        public void Hide()
        {
            for (int i = 0; i < _progressStars.Length; i++)
                _progressStars[i].CompleteStar.transform.Diactivate();
        }
    }
}