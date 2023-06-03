using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftHandIk;
    [SerializeField] private Transform rightHandIk;
    [SerializeField] private GameObject log;
    [SerializeField] private PlayableDirector pd;


    private float speed = 4f;
    private Animator animator;

    private int speedId = Animator.StringToHash("Speed");
    private int isRunId = Animator.StringToHash("IsRun");
    private int horizontalId = Animator.StringToHash("Horizontal");
    private int vaultId = Animator.StringToHash("Vault");
    private int slideId = Animator.StringToHash("Slide");
    private int colliderId = Animator.StringToHash("Collider");
    private int isLogId = Animator.StringToHash("IsLog");


    private bool canFanQiang = false; // �Ƿ�Ҫ��ǽ
    private bool canSlide = false; // �Ƿ�Ҫ����
    private Vector3 matchTarget;
    private CharacterController cc;
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    Vector3 temp;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //transform.position += new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);
        animator.SetFloat(speedId, v * 4.3f);
        animator.SetFloat(horizontalId, h * 79f);

        ToVault();
        ToSlide();
    }

    /// <summary>
    /// ��ǽ
    /// </summary>
    private void ToVault()
    {
        canFanQiang = false;
        if (animator.GetFloat(speedId) > 3f && animator.GetCurrentAnimatorStateInfo(0).IsName("BaseTree"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.3f, transform.forward, out hit, 4.5f))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    // ������ײ���ĵ�ľ���
                    if (hit.distance > 3)
                    {
                        temp = hit.point;
                        Vector3 point = hit.point; // ��ײ�����Ǹ���
                        point.y = hit.transform.position.y + hit.collider.bounds.size.y + 0.05f;
                        matchTarget = point;
                        canFanQiang = true;
                    }

                }
            }
        }
        Debug.DrawLine(transform.position, matchTarget);
        Debug.DrawLine(transform.position, temp, UnityEngine.Color.red);
        animator.SetBool(vaultId, canFanQiang);
        // �ж����ڵĶ���״̬�Ƿ�Ϊ��ǽ�Ķ���
        // IsInTransition: �Ƿ��ڶ�����ת���ڼ�
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Vault") && !animator.IsInTransition(0))
        {
            //MatchTargetWeightMask:���ֶ���һ�£���1��1��1����ȫһ�£�0.5��0.5��0.5��50%һ�£������Ǹ���������ת��Ȩ��
            animator.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand,
                new MatchTargetWeightMask(Vector3.one, 0), 0.36f, 0.41f);
        }
        // ��������ߴ���0.5�ͽ�����ײ��
        cc.enabled = animator.GetFloat(colliderId) < 0.5f;
    }

    /// <summary>
    /// ����
    /// </summary>
    private void ToSlide()
    {
        canSlide = false;
        if (animator.GetFloat(speedId) > 3f && animator.GetCurrentAnimatorStateInfo(0).IsName("BaseTree"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 1.5f, transform.forward, out hit, 2.5f))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    // ������ײ���ĵ�ľ���
                    if (hit.distance > 1.5f)
                    {
                        temp = hit.point;
                        Vector3 point = hit.point;
                        point.y = 0;
                        matchTarget = point + transform.forward * 2;
                        canSlide = true;
                    }

                }
            }
        }
        //Debug.DrawLine(transform.position, matchTarget);
        Debug.DrawLine(transform.position, temp, UnityEngine.Color.red);
        animator.SetBool(slideId, canSlide);
        // �ж����ڵĶ���״̬�Ƿ�Ϊ��ǽ�Ķ���
        // IsInTransition: �Ƿ��ڶ�����ת���ڼ�
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slide") && !animator.IsInTransition(0))
        {
            //MatchTargetWeightMask:���ֶ���һ�£���1��1��1����ȫһ�£�0.5��0.5��0.5��50%һ�£������Ǹ���������ת��Ȩ��
            animator.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.Root,
                new MatchTargetWeightMask(new Vector3(1, 0, 1), 0), 0.17f, 0.7f);
        }
        // ��������ߴ���0.5�ͽ�����ײ��
        cc.enabled = animator.GetFloat(colliderId) < 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Log"))
        {
            Destroy(other.gameObject);
            log.SetActive(true);
            animator.SetBool(isLogId, true);
        }
        if (other.CompareTag("PlayTimeLine"))
        {
            pd.Play();
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (layerIndex == 1)
        {
            float weight = animator.GetBool(isLogId) ? 1 : 0;
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIk.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIk.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIk.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIk.rotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
        }
    }
}
