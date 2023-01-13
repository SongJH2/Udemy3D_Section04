using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("�⺻ �¾�")]
    [Tooltip("������ ���ּ��� �󸶳� ������ ��������")]
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("�¿� �̵� �Ÿ�")]
    [SerializeField] float xRange = 10f;
    [Tooltip("���� �̵� �Ÿ�")]
    [SerializeField] float yRange = 5f;

    [Header("ȸ�� ���� ����")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("������ �߻� ��ġ ���")]
    [Tooltip("������ ��ƼŬ ������Ʈ �ֱ�")]
    [SerializeField] GameObject[] lasers;

    private float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFire();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControll = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float yawDueToControll = xThrow * controlRollFactor;

        float pitch = pitchDueToPosition + pitchDueToControll;
        float yaw = yawDueToPosition;
        float roll = yawDueToControll;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFire()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }
    private void SetLaserActive( bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
