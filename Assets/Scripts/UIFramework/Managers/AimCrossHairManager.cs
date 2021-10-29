using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIFramework.Managers
{
    public enum CrossHair
    {
        Pistol = 0,
        Rifle = 1,
        Shotgun = 2,
    }

    public class AimCrossHairManager : MonoBehaviour
    {
        // a class to manage the cross hair UI

        public Sprite pistolCrossHair;
        public Sprite rifleCrossHair;
        public Sprite shotgunCrossHair;

        [Space]
        public Color crossHaiOriginalColor;
        public Color targetingEnemyColor;
        public Transform virtualCamera;

        [Space] public bool detectingEnemyAiming = true;
        public float enemyDetectRaycastDistance = 10f;

        private Dictionary<CrossHair, Sprite> crossHairSprites;
        private Image _crossHairImage;  // the image component

        // private CrossHair _crossHair = CrossHair.Pistol;  // the current cross hair used

        private void Awake()
        {
            crossHairSprites = new Dictionary<CrossHair, Sprite>
            {
                {CrossHair.Pistol, pistolCrossHair},
                {CrossHair.Rifle, rifleCrossHair},
                {CrossHair.Shotgun, shotgunCrossHair},
            };
            _crossHairImage = GetComponent<Image>();

            ChangeCrossHair(CrossHair.Rifle);
        }

        private void Update()
        {
            if (detectingEnemyAiming)
            {
                if (Physics.Raycast(virtualCamera.position, virtualCamera.forward,
                    enemyDetectRaycastDistance, LayerMask.GetMask($"Enemy")))
                {
                    TargetEnemy();
                }
                else LoseEnemyTargeting();
                Debug.DrawRay(virtualCamera.position, virtualCamera.forward * enemyDetectRaycastDistance, Color.green); // debug
            }
        }

        public void ChangeCrossHair(CrossHair newCrossHair)
        {
            // change the cross hair
            _crossHairImage.sprite = crossHairSprites[newCrossHair];
        }

        private void TargetEnemy()
        {
            // cross hair turns red when aiming at enemies
            _crossHairImage.color = targetingEnemyColor;
        }

        private void LoseEnemyTargeting()
        {
            // cross hair turns white losing targeting enemies
            _crossHairImage.color = crossHaiOriginalColor;
        }
    }
}
