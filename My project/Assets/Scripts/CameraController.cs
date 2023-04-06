using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // ** ī�޶��� ���� �ð�.
    private float shakeTime = 0.1f;

    // ** ī�޶��� ���� ����.
    private Vector3 offset = new Vector3(0.02f, 0.02f, 0.0f);
    private Vector3 OldPosition;

    // ** �ڷ�ƾ �Լ� ����.
    IEnumerator Start()
    {
        // ** ī�޶��� ����ȿ���� �ֱ��� ī�޶� ��ġ�� �޾ƿ´�.
        OldPosition = Camera.main.transform.position;

        // ** 0.15�ʵ��� ����.
        while (shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;

            // ** �ݺ����� ����Ǵ� ���� �ݺ������� ȣ��.
            yield return null;

            // ** ī�޶� ���� ���� ��ŭ ������Ų��.
            Camera.main.transform.position = new Vector3(
                 Random.Range(OldPosition.x - offset.x, OldPosition.x + offset.x),
                 Random.Range(OldPosition.y - offset.y, OldPosition.y + offset.y),
                 -10.0f);
        }

        // ** �ݸ��� ����Ǹ� ī�޶� ��ġ�� �ٽ� ������ ���´�.
        Camera.main.transform.position = OldPosition;

        // ** Ŭ������ �����Ѵ�.
        Destroy(this.gameObject);
    }
}