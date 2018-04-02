using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DuloGames.UI
{
    public class Demo_CharacterSelectMgr : MonoBehaviour
    {
        private static Demo_CharacterSelectMgr m_Mgr;
        public static Demo_CharacterSelectMgr instance
        {
            get { return m_Mgr; }
        }

        [SerializeField] private int m_IngameSceneId = 0;

        [Header("Camera Properties")]
        [SerializeField] private Camera m_Camera;
        [SerializeField] private float m_CameraSpeed = 10f;
        [SerializeField] private float m_CameraDistance = 10f;

        [Header("Character Slots")]
        [SerializeField] private List<Transform> m_Slots;

        [Header("Selected Character Info")]
        [SerializeField] private GameObject m_InfoContainer;
        [SerializeField] private Text m_NameText;
        [SerializeField] private Text m_LevelText;
        [SerializeField] private Text m_RaceClassText;

        [Header("Demo Properties")]
        [SerializeField] private GameObject m_CharacterPrefab;
        [SerializeField] private int m_AddCharacters = 5;

        private int m_SelectedIndex = -1;
        private Transform m_SelectedTransform;

        protected void Awake()
        {
            // Save a reference to the instance
            m_Mgr = this;

            // Get a camera if not set
            if (this.m_Camera == null) this.m_Camera = Camera.main;

            // Disable the info container
            if (this.m_InfoContainer != null) this.m_InfoContainer.SetActive(false);
        }

        protected void OnDestroy()
        {
            m_Mgr = null;
        }

        protected void OnEnable()
        {
            // Add characters for the demo
            if (this.m_CharacterPrefab)
            {
                for (int i = 0; i < this.m_AddCharacters; i++)
                {
                    string[] names = new string[10] { "Annika", "Evita", "Herb", "Thad", "Myesha", "Lucile", "Sharice", "Tatiana", "Isis", "Allen" };
                    string[] races = new string[5] { "Human", "Elf", "Orc", "Undead", "Programmer" };
                    string[] classes = new string[5] { "Warrior", "Mage", "Hunter", "Priest", "Designer" };

                    Demo_CharacterInfo info = new Demo_CharacterInfo();
                    info.name = names[Random.Range(0, 10)];
                    info.raceString = races[Random.Range(0, 5)];
                    info.classString = classes[Random.Range(0, 5)];
                    info.level = (int)Random.Range(1, 61);

                    this.AddCharacter(info, this.m_CharacterPrefab, i);
                }
            }

            // Select the first character
            this.SelectFirstAvailable();
        }

        protected void Update()
        {
            if (this.isActiveAndEnabled && this.m_Slots.Count == 0)
                return;

            // Make sure we have a slot transform
            if (this.m_SelectedTransform != null)
            {
                Vector3 targetPos = this.m_SelectedTransform.position + (-this.m_SelectedTransform.forward * this.m_CameraDistance);
                targetPos.y = this.m_Camera.transform.position.y;

                this.m_Camera.transform.position = Vector3.Lerp(this.m_Camera.transform.position, targetPos, Time.deltaTime * this.m_CameraSpeed);
            }
        }

        /// <summary>
        /// Adds a character to the character select.
        /// </summary>
        /// <param name="info">The character info.</param>
        /// <param name="modelPrefab">The character model prefab.</param>
        /// <param name="index">Index.</param>
        public void AddCharacter(Demo_CharacterInfo info, GameObject modelPrefab, int index)
        {
            if (this.m_Slots.Count == 0)
                return;

            if (modelPrefab == null)
                return;

            // Get the slot
            Transform slotTrans = this.m_Slots[index];

            // Make sure we have a slot transform
            if (slotTrans == null)
                return;

            // Get the character script
            Demo_CharacterSelectChar csc = slotTrans.gameObject.GetComponent<Demo_CharacterSelectChar>();

            // Set the character info
            if (csc != null)
            {
                csc.info = info;
                csc.index = index;
            }

            // Remove any child objects
            foreach (Transform child in slotTrans)
            {
                Destroy(child.gameObject);
            }

            // Add the character model
            GameObject model = Instantiate<GameObject>(modelPrefab);
            model.layer = slotTrans.gameObject.layer;
            model.transform.SetParent(slotTrans, false);
            model.transform.localScale = modelPrefab.transform.localScale;
            model.transform.localPosition = modelPrefab.transform.localPosition;
            model.transform.localRotation = modelPrefab.transform.localRotation;
        }

        /// <summary>
        /// Selects the first available character if any.
        /// </summary>
        public void SelectFirstAvailable()
        {
            if (this.m_Slots.Count == 0)
                return;

            foreach (Transform trans in this.m_Slots)
            {
                if (trans == null)
                    continue;

                // Get the character script
                Demo_CharacterSelectChar csc = trans.gameObject.GetComponent<Demo_CharacterSelectChar>();

                // Select the character
                if (csc != null && csc.info != null)
                {
                    this.SelectCharacter(csc);
                    break;
                }
            }
        }

        /// <summary>
        /// Selects the character at the given index.
        /// </summary>
        /// <param name="index"></param>
        public void SelectCharacter(int index)
        {
            if (this.m_Slots.Count == 0)
                return;

            // Get the slot
            Transform slotTrans = this.m_Slots[index];

            if (slotTrans == null)
                return;

            // Get the character script
            Demo_CharacterSelectChar csc = slotTrans.gameObject.GetComponent<Demo_CharacterSelectChar>();

            // Select the character
            if (csc != null) this.SelectCharacter(csc);
        }

        /// <summary>
        /// Selects the character.
        /// </summary>
        /// <param name="character">The character component.</param>
        public void SelectCharacter(Demo_CharacterSelectChar character)
        {
            // Check if already selected
            if (this.m_SelectedIndex == character.index)
                return;

            // Set the selected
            this.m_SelectedIndex = character.index;
            this.m_SelectedTransform = character.transform;

            if (character.info != null)
            {
                if (this.m_InfoContainer != null) this.m_InfoContainer.SetActive(true);
                if (this.m_NameText != null) this.m_NameText.text = character.info.name.ToLower();
                if (this.m_LevelText != null) this.m_LevelText.text = character.info.level.ToString();
                if (this.m_RaceClassText != null) this.m_RaceClassText.text = character.info.raceString.ToLower() + " " + character.info.classString.ToLower();
            }
            else
            {
                if (this.m_InfoContainer != null) this.m_InfoContainer.SetActive(false);
                if (this.m_NameText != null) this.m_NameText.text = "";
                if (this.m_LevelText != null) this.m_LevelText.text = "";
                if (this.m_RaceClassText != null) this.m_RaceClassText.text = "";
            }
        }

        /// <summary>
        /// Gets the character in the specified direction (1 or -1).
        /// </summary>
        /// <param name="direction">The direction 1 or -1.</param>
        /// <returns></returns>
        public Demo_CharacterSelectChar GetCharacterInDirection(float direction)
        {
            if (this.m_Slots.Count == 0)
                return null;

            if (this.m_SelectedTransform == null && this.m_Slots[0] != null)
                return this.m_Slots[0].gameObject.GetComponent<Demo_CharacterSelectChar>();

            Demo_CharacterSelectChar closest = null;
            float lastDistance = 0f;

            foreach (Transform trans in this.m_Slots)
            {
                // Skip the selected one
                if (trans.Equals(this.m_SelectedTransform))
                    continue;

                float curDirection = trans.position.x - this.m_SelectedTransform.position.x;

                // Check direction
                if (direction > 0f && curDirection > 0f || direction < 0f && curDirection < 0f)
                {
                    // Get the character component
                    Demo_CharacterSelectChar character = trans.GetComponent<Demo_CharacterSelectChar>();

                    // Make sure we have character component
                    if (character == null)
                        continue;

                    // Make sure it's populated
                    if (character.info == null)
                        continue;

                    // If we have no closest assigned yet
                    if (closest == null)
                    {
                        closest = character;
                        lastDistance = Vector3.Distance(this.m_SelectedTransform.position, trans.position);
                        continue;
                    }

                    // Comapre distance
                    if (Vector3.Distance(this.m_SelectedTransform.position, trans.position) <= lastDistance)
                    {
                        closest = character;
                        lastDistance = Vector3.Distance(this.m_SelectedTransform.position, trans.position);
                        continue;
                    }
                }
            }

            return closest;
        }

        /// <summary>
        /// Selects the next character.
        /// </summary>
        public void SelectNext()
        {
            Demo_CharacterSelectChar next = this.GetCharacterInDirection(1f);

            if (next != null)
            {
                this.SelectCharacter(next);
            }
        }

        /// <summary>
        /// Selects the previous character.
        /// </summary>
        public void SelectPrevious()
        {
            Demo_CharacterSelectChar prev = this.GetCharacterInDirection(-1f);

            if (prev != null)
            {
                this.SelectCharacter(prev);
            }
        }

        /// <summary>
        /// Remove the character at the given index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveCharacter(int index)
        {
            if (this.m_Slots.Count == 0)
                return;

            // Get the slot
            Transform slotTrans = this.m_Slots[index];

            if (slotTrans == null)
                return;

            // Get the character script
            Demo_CharacterSelectChar csc = slotTrans.gameObject.GetComponent<Demo_CharacterSelectChar>();

            // Unset the character info
            if (csc != null) csc.info = null;

            // Remove the child objects
            foreach (Transform child in slotTrans)
            {
                Destroy(child.gameObject);
            }

            // Unset the character info texts
            if (index == this.m_SelectedIndex)
            {
                if (this.m_InfoContainer != null) this.m_InfoContainer.SetActive(false);
                if (this.m_NameText != null) this.m_NameText.text = "";
                if (this.m_LevelText != null) this.m_LevelText.text = "";
                if (this.m_RaceClassText != null) this.m_RaceClassText.text = "";

                this.SelectFirstAvailable();
            }
        }

        /// <summary>
        /// Deletes the selected character.
        /// </summary>
        public void DeleteSelected()
        {
            if (this.m_SelectedIndex > -1)
            {
                this.RemoveCharacter(this.m_SelectedIndex);
            }
        }

        public void OnPlayClick()
        {
            UILoadingOverlay loadingOverlay = UILoadingOverlayManager.Instance.Create();

            if (loadingOverlay != null)
                loadingOverlay.LoadScene(this.m_IngameSceneId);
        }
    }
}
