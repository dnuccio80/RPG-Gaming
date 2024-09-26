using TMPro;
using UnityEngine;


namespace RPG.UI
{
    public class DamageText : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI damageText;

        private Animation anim;

        private void Awake()
        {
            anim = GetComponent<Animation>();   
        }


        public void SetDamageText(float damage)
        {
            damageText.text = damage.ToString();
        }

        private void DestroyeGO()
        {
            Destroy(gameObject);
        }


    }

}
