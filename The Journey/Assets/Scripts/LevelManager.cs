using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public DeathScreen deathScreen;
        [SerializeField] Player player;
        [SerializeField] Transform sleepingSpotEntrance;
        void Awake()
        {
            var enemies = FindObjectsOfType<EnemyPatroller>();
            foreach (var item in enemies)
            {
                item.OnDeath += GameOverByDeath;
            }

            if (PlayerPrefs.GetInt(PlayerPrefsVariables.IsGettingBackFromCave) == 0) return;
            player.transform.position = sleepingSpotEntrance.position;
            PlayerPrefs.SetInt(PlayerPrefsVariables.IsGettingBackFromCave, 0);

            sleepingSpotEntrance.gameObject.SetActive(true);
        }

        private void GameOverByDeath()
        {
            deathScreen.Invoke();
        }
    }
}
