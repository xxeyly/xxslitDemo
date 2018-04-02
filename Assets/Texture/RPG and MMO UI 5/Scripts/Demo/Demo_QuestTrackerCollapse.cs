using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DuloGames.UI
{
    [ExecuteInEditMode]
    public class Demo_QuestTrackerCollapse : UIBehaviour
    {
        [SerializeField] private GameObject m_Content;
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private UIFlippable m_ArrowFlippable;
        [SerializeField] private UIFlippable m_ArrowFlippable2;
        [SerializeField] private bool m_ArrowInvertFlip = false;
        
        #region Unity Lifetime calls
        
        protected override void OnEnable()
        {
            base.OnEnable();

            // Hook the toggle change event
            if (this.m_Toggle != null)
            {
                this.m_Toggle.onValueChanged.AddListener(OnToggleStateChange);
            }

            // Apply the current toggle state
            if (this.m_Toggle != null)
            {
                this.OnToggleStateChange(this.m_Toggle.isOn);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // Unhook the toggle change event
            if (this.m_Toggle != null)
            {
                this.m_Toggle.onValueChanged.RemoveListener(OnToggleStateChange);
            }

            // Expand the view
            this.OnToggleStateChange(false);
        }

        #endregion
        
        public void OnToggleStateChange(bool state)
        {
            if (!IsActive())
                return;

            if (state)
            {
                if (this.m_Content != null)
                    this.m_Content.SetActive(true);

                if (this.m_ArrowFlippable != null)
                    this.m_ArrowFlippable.vertical = (this.m_ArrowInvertFlip ? false : true);
                
                if (this.m_ArrowFlippable2 != null)
                    this.m_ArrowFlippable2.vertical = (this.m_ArrowInvertFlip ? false : true);
            }
            else
            {
                if (this.m_Content != null)
                    this.m_Content.SetActive(false);

                if (this.m_ArrowFlippable != null)
                    this.m_ArrowFlippable.vertical = (this.m_ArrowInvertFlip ? true : false);
                
                if (this.m_ArrowFlippable2 != null)
                    this.m_ArrowFlippable2.vertical = (this.m_ArrowInvertFlip ? true : false);
            }
        }
    }
}
