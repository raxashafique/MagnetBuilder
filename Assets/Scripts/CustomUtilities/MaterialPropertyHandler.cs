using System;
using UnityEngine;

namespace CustomUtilities
{
	[RequireComponent(typeof(Renderer))]
	public class MaterialPropertyHandler:MonoBehaviour
	{
		private Renderer m_renderer;
		private MaterialPropertyBlock m_materialPropertyBlock;

		private void Awake()
		{
			m_renderer = GetComponent<Renderer>();
			m_materialPropertyBlock = new MaterialPropertyBlock();
		}

		public void SetColor(Color color)
		{
			m_renderer.GetPropertyBlock(m_materialPropertyBlock);
			m_materialPropertyBlock.SetColor("_Color",color);
			m_renderer.SetPropertyBlock(m_materialPropertyBlock);

		}

	}
}