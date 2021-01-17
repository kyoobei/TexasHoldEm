using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject m_prefabToClone = null;
    [SerializeField] private int m_cloneCount = 0;
    private Queue<GameObject> m_queueClones = new Queue<GameObject>();
    private List<GameObject> m_releaseCloneList = new List<GameObject>();
    
    public Action OnClearPooler;

    #region UNITY METHODS
    private void Start()
    {
        for(int i = 0; i < m_cloneCount; i++)
        {
            GameObject go = Instantiate(m_prefabToClone);
            go.transform.SetParent(this.transform);
            go.SetActive(false);
            ResetCloneTransform(go.transform);
            m_queueClones.Enqueue(go);
        }
    }
    #endregion

    #region USER DEFINED METHODS
    public GameObject GetClone()
    {
        GameObject cloneToReturn = null;
        if(m_queueClones.Count > 0)
        {
            cloneToReturn = m_queueClones.Dequeue();
            m_releaseCloneList.Add(cloneToReturn);
        }
        else if(m_queueClones.Count <= 0)
        {
            cloneToReturn = Instantiate(m_prefabToClone);
            cloneToReturn.transform.SetParent(this.transform);
            ResetCloneTransform(cloneToReturn.transform);
            m_releaseCloneList.Add(cloneToReturn);
        }
        return cloneToReturn;
    }
    public void ReturnAllClone()
    {
        if(m_releaseCloneList.Count > 0)
        {
            for(int i = 0; i < m_releaseCloneList.Count; i++)
            {
                ResetCloneTransform(m_releaseCloneList[i].transform);
                m_releaseCloneList[i].SetActive(false);
                m_queueClones.Enqueue(m_releaseCloneList[i]);
            }
            m_releaseCloneList.Clear();
        }
        OnClearPooler?.Invoke();
    }
    private void ResetCloneTransform(Transform cloneTransform)
    {
        cloneTransform.transform.localPosition = Vector3.zero;
        cloneTransform.transform.localRotation = Quaternion.identity;
        cloneTransform.transform.localScale = Vector3.one;
    }
    #endregion
}
