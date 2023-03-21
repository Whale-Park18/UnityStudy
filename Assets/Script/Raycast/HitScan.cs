using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

namespace WhalePark18.Raycast
{
    public enum MouseType { Left = 0, Right, Middle, }

    public class HitScan : MonoBehaviour
    {
        public float attackRate = 0.3f;
        public bool isAuto = true;
        public float maxdistance = 50;
        public int hitLimit = 1;

        private float lastAttackTime;

        private void Update()
        {
            UpdateAction();
        }

        private void UpdateAction()
        {
            if(Input.GetMouseButtonDown((int)MouseType.Left))
            {
                StartAction();
            }

            if (Input.GetMouseButtonUp((int)MouseType.Left))
            {
                StopAction();
            }
        }

        public void StartAction(MouseType type = MouseType.Left)
        {
            switch(type)
            {
                case MouseType.Left:
                    {
                        if (isAuto) 
                            StartCoroutine("OnAttackLoop");
                        else
                            OnAttack();
                    }
                    break;
            }
        }

        public void StopAction(MouseType type = MouseType.Left)
        {
            switch(type)
            {
                case MouseType.Left:
                    {
                        if (isAuto)
                            StopCoroutine("OnAttackLoop");
                    }
                    break;
            }
        }


        private IEnumerator OnAttackLoop()
        {
            while(true)
            {
                OnAttack();
                yield return new WaitForFixedUpdate();
            }
        }

        private void OnAttack()
        {
            if (Time.time - lastAttackTime < attackRate) return;

            lastAttackTime = Time.time;
            //OnRaycast();
            OnRaycastAll();
        }

        private void OnRaycast()
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxdistance);
            UnityEngine.Debug.DrawRay(transform.position, transform.forward * hitInfo.distance, Color.red);
            print(hitInfo.collider.name);
        }

        private void OnRaycastAll()
        {
            RaycastHit[] hitInfoList = Physics.RaycastAll(transform.position, transform.forward, maxdistance);
            Array.Sort(hitInfoList, (RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));

            for(int i = 0; i < hitLimit; i++)
            {
                UnityEngine.Debug.DrawRay(transform.position, transform.forward * hitInfoList[i].distance, Color.red);

            }
        }
    }
}