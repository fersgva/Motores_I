using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [SerializeField]
    private float timeBetweenAttacks;

    private float timer;
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        controller.Agent.isStopped = true; //Nos paramos
        controller.Anim.SetBool("attacking", true); //Lanzamos estado de ataque en el animator.


    }
    public override void OnUpdateState()
    {
            FaceTarget(); //Nos aseguramos de enfocar al objetivo.
    }

    private void FaceTarget()
    {
        Vector3 directionToTarget = (controller.Target.position - transform.position).normalized;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }

    private void OnAttackLanded()
    {
        
    }

    private void OnFinishAttackAnimation()
    {
        //Sólo cuando terminamos la animación es cuando comprobamos la distancia.
        //No lo hago por agent para que no tengamos que recalcular destino.
        if(Vector3.Distance(transform.position, controller.Target.position) > controller.DistanciaAtaque)
        {
            controller.Agent.isStopped = false;
            controller.Anim.SetBool("attacking", false);
            controller.ChangeState(controller.ChaseState);

        }
    }

    public override void OnExitState()
    {
        controller.ExitAttackState();
    }

}
