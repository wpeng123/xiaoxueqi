using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, ICanvasRaycastFilter
{
    private Transform nowparent;//��¼ԭʼ����λ��
    private bool isRaycastLocationValid = true;//Ĭ�����߲��ܴ�͸����

    public void OnBeginDrag(PointerEventData eventData)
    {
        nowparent = transform.parent;//��ʼλ��
        isRaycastLocationValid = false;//����Ϊ���Դ�͸
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //��ȡ����յ�λ�ÿ��ܴ��ڵ���Ʒ
        GameObject go = eventData.pointerCurrentRaycast.gameObject;

        if (go != null)//���λ�ò�Ϊ��
        {
            Debug.Log(go.name);//��ӡһ�����λ������
            if (go.tag == ("Gird") && go.layer.Equals(9))//����յ�λ���ǿո��Ӷ���Ϊ�����㣨�жϽ�װ��������ϵ����������崦��
            {
                SetParentAndPosition(transform, go.transform);
            }
            else if (go.tag.Equals("Assemblygroove")   && go.transform.parent.gameObject.layer.Equals(9)
                  && transform.parent.gameObject.layer.Equals(9))
            //������µ��յ�Ҳ�Ǳ�����һ������������λ�ø��������Ϊ�����㣬��ʱ��ק����Ʒ������������ҲΪ�����㣨�ж�Ϊ�������������⽻����
            {
                //����ק����Ʒ1�ŵ�����յ��µ�λ��
                SetParentAndPosition(transform, go.transform.parent);
                //������յ����Ʒ2�ŵ�ԭ����Ʒ1��λ��
                SetParentAndPosition(go.transform, nowparent);
                if (transform.position == go.transform.position)
                {
                    Debug.Log("error");
                }
            }
            else if (go.tag == transform.tag && go.layer.Equals(10))//��ǩ��ͬ�����������װ���㣨�жϴӱ�������װ��������װ����
            {
                //����ק����Ʒ1�ŵ�����յ��µ�λ��
                SetParentAndPosition(transform, go.transform);
            }
            else if (go.tag == transform.tag && transform.gameObject.layer == go.layer)//��ǩ��ͬ���Ҳ���ͬ(�ж�װ������װ������λ�����װ�����ϵ�������ͬ����װ������λ��)
            {
                //����ק����Ʒ1�ŵ�����յ��µ�λ��
                SetParentAndPosition(transform, go.transform.parent);
                SetParentAndPosition(go.transform, nowparent);
            }
            else//��Чλ�ã���Ʒ�ص�ԭ����λ��
            {
                SetParentAndPosition(transform, nowparent);
            }
        }
        else
        {
            SetParentAndPosition(transform, nowparent);
        }
        isRaycastLocationValid = true;//���߲����Դ�͸����
    }
    // ��child�ŵ�parent������������
    private void SetParentAndPosition(Transform child, Transform parent)
    {
        child.SetParent(parent);
        child.position = parent.position;//�������������游����
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationValid;
    }
}