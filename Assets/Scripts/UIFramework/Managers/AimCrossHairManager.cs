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
        public Vector3 targetPoint;
        private RaycastHit hitInfo;

        public GameObject testBall;
        
        public static AimCrossHairManager Instance { get; private set; }

        // private CrossHair _crossHair = CrossHair.Pistol;  // the current cross hair used

        private void Awake()
        {
            Instance = this;
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
                var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                if (Physics.Raycast(ray, out hitInfo))
                {
                    targetPoint = hitInfo.point;  //记录碰撞的目标点
                    if (hitInfo.collider.gameObject.layer == 9) TargetEnemy();
                    else LoseEnemyTargeting();
                }
                else targetPoint = ray.direction * 500;

                testBall.transform.position = targetPoint;
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
