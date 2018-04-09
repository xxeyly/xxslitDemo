using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	/// <summary>
	/// 
	/// </summary>
	public class TimeOfDay : Singleton<TimeOfDay> 
	{
		/// <summary></summary>
//		public Value<ET.TimeOfDay> State = new Value<ET.TimeOfDay>(ET.TimeOfDay.Day);

		/// <summary></summary>
		public float NormalizedTime { get { return m_NormalizedTime; } set { m_NormalizedTime = float.IsNaN(Mathf.Repeat(value, 1f)) ? 0f : Mathf.Repeat(value, 1f); m_CurrentHour = (int)(m_NormalizedTime * 24f);} }

		/// <summary></summary>
		public int CurrentHour { get { return m_CurrentHour; } }

		[Header("Setup")]

		[SerializeField]
		private Light m_Sun;

		[SerializeField]
		private Light m_Moon;

		[Header("General")]

		[SerializeField]
		private bool m_StopTime;

		[SerializeField]
		private bool m_ShowGUI;

		[SerializeField]
		[Range(0, 24)]
		[Tooltip("The current hour (00:00 AM to 12:00 PM to 24:00 PM)")]
		private int m_CurrentHour = 6;

		[SerializeField]
		[Tooltip("How many seconds are in a day.")]
		private int m_DayDuration = 900;

		[SerializeField]
		[Tooltip("On which axis should the moon and sun rotate?")]
		private Vector3 m_RotationAxis = Vector2.right;

		[Header("Fog")]

		[SerializeField]
		private FogMode m_FogMode = FogMode.ExponentialSquared;

		[SerializeField]
		[Tooltip("Fog intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_FogIntensity;

		[SerializeField]
		[Tooltip("Fog color variation over the whole day & night cycle.")]
		private Gradient m_FogColor;

		[Header("Sun")]

		[SerializeField]
		[Tooltip("Sun intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_SunIntensity;

		[SerializeField]
		[Tooltip("Sun color variation over the whole day & night cycle.")]
		private Gradient m_SunColor;

		[Header("Moon")]

		[SerializeField]
		[Tooltip("Moon intensity variation over the whole day & night cycle.")]
		private AnimationCurve m_MoonIntensity;

		[SerializeField]
		[Tooltip("Moon color variation over the whole day & night cycle.")]
		private Gradient m_MoonColor;

		[Header("Skybox")]

		[SerializeField]
		private Material m_Skybox;

		[SerializeField]
		private AnimationCurve m_SkyboxBlend;

//		private ET.TimeOfDay m_InternalState;

		private Transform m_SunTransform;
		private Transform m_MoonTransform;
		private float m_NormalizedTime;
		private float m_TimeIncrement;
	
	
		private void Awake()
		{
			if(!m_Sun || !m_Moon)
			{
				Debug.LogError("The moon or sun are not assigned in the inspector! please assign them and restart the game.", this);
				enabled = false;
				return;
			}

			m_SunTransform = m_Sun.transform;
			m_MoonTransform = m_Moon.transform;

			AccommodateEditorChanges();
/*
			m_InternalState = NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f) ? ET.TimeOfDay.Day : ET.TimeOfDay.Night;
			State.Set(m_InternalState);*/
		}

		private void Update()
		{
			// Time loop.
			if(!m_StopTime)
				NormalizedTime += Time.deltaTime * m_TimeIncrement;

			m_CurrentHour = (int)(NormalizedTime * 24f);

			// Ambient light.
			RenderSettings.ambientIntensity = Mathf.Clamp01(m_Sun.intensity);

			// Fog
			RenderSettings.fogDensity = m_FogIntensity.Evaluate(NormalizedTime);
			RenderSettings.fogColor = m_FogColor.Evaluate(NormalizedTime);

			// Sun
			m_SunTransform.rotation = Quaternion.Euler(m_RotationAxis * (NormalizedTime * 360f - 90f));
			m_Sun.intensity = m_SunIntensity.Evaluate(NormalizedTime);
			m_Sun.color = m_SunColor.Evaluate(NormalizedTime);
	
			m_Sun.enabled = m_Sun.intensity > 0f;

			// Moon
			m_MoonTransform.rotation = Quaternion.Euler(-m_RotationAxis * (NormalizedTime * 360f - 90f));
			m_Moon.intensity = m_MoonIntensity.Evaluate(NormalizedTime);
			m_Moon.color = m_MoonColor.Evaluate(NormalizedTime);

			m_Moon.enabled = m_Moon.intensity > 0f;

			// Skybox
			if(m_Skybox)
				m_Skybox.SetFloat("_Blend", m_SkyboxBlend.Evaluate(m_NormalizedTime));

			// States.
			/*var lastState = m_InternalState;
			m_InternalState = NormalizedTime.IsInRangeLimitsExcluded(0.25f, 0.75f) ? ET.TimeOfDay.Day : ET.TimeOfDay.Night;
			if(lastState != m_InternalState)
				State.Set(m_InternalState);

			GameController.NormalizedTime = NormalizedTime;*/
		}

		private void OnValidate()
		{
			AccommodateEditorChanges();

			m_SunTransform = m_Sun.transform;
			m_MoonTransform = m_Moon.transform;

			Update();
		}

		private void AccommodateEditorChanges()
		{
			m_TimeIncrement = 1f / m_DayDuration;
			m_NormalizedTime = m_CurrentHour / 24f;

			RenderSettings.fogMode = m_FogMode;
		}
	}
}
