using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, ICanvasRaycastFilter
{
    private Transform nowparent;//记录原始坐标位置
    private bool isRaycastLocationValid = true;//默认射线不能穿透物体

    public void OnBeginDrag(PointerEventData eventData)
    {
        nowparent = transform.parent;//初始位置
        isRaycastLocationValid = false;//设置为可以穿透
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //获取鼠标终点位置可能存在的物品
        GameObject go = eventData.pointerCurrentRaycast.gameObject;

        if (go != null)//落点位置不为空
        {
            Debug.Log(go.name);//打印一下落点位置名称
            if (go.tag == ("Gird") && go.layer.Equals(9))//鼠标终点位置是空格子而且为背包层（判断将装备或道具拖到背包空物体处）
            {
                SetParentAndPosition(transform, go.transform);
            }
            else if (go.tag.Equals("Assemblygroove")   && go.transform.parent.gameObject.layer.Equals(9)
                  && transform.parent.gameObject.layer.Equals(9))
            //鼠标落下的终点也是背包的一个物体而且落点位置父物体层数为背包层，此时拖拽的物品父物体对象层数也为背包层（判断为背包内物体随意交换）
            {
                //将拖拽的物品1放到鼠标终点下的位置
                SetParentAndPosition(transform, go.transform.parent);
                //将鼠标终点的物品2放到原来物品1的位置
                SetParentAndPosition(go.transform, nowparent);
                if (transform.position == go.transform.position)
                {
                    Debug.Log("error");
                }
            }
            else if (go.tag == transform.tag && go.layer.Equals(10))//标签相同，而且落点在装备层（判断从背包内往装备栏放置装备）
            {
                //将拖拽的物品1放到鼠标终点下的位置
                SetParentAndPosition(transform, go.transform);
            }
            else if (go.tag == transform.tag && transform.gameObject.layer == go.layer)//标签相同而且层相同(判断装备栏内装备交换位置与从装备栏拖到背包相同类型装备互换位置)
            {
                //将拖拽的物品1放到鼠标终点下的位置
                SetParentAndPosition(transform, go.transform.parent);
                SetParentAndPosition(go.transform, nowparent);
            }
            else//无效位置，物品回到原来的位置
            {
                SetParentAndPosition(transform, nowparent);
            }
        }
        else
        {
            SetParentAndPosition(transform, nowparent);
        }
        isRaycastLocationValid = true;//射线不可以穿透物体
    }
    // 将child放到parent下做其子物体
    private void SetParentAndPosition(Transform child, Transform parent)
    {
        child.SetParent(parent);
        child.position = parent.position;//子物体的坐标跟随父物体
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationValid;
    }
}