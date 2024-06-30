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

        void Start ()
        {
            var enemies = FindObjectsOfType<EnemyPatroller>();
            foreach (var item in enemies)
            {
                item.OnDeath += GameOverByDeath;
            }
        }

        private void GameOverByDeath()
        {
            deathScreen.Invoke();
        }
    }
}
