using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{

    [SerializeField]
    private float baseAttackDamage;


    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        //controller.Agent.isStopped = true;
        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Anim.SetBool("attacking", true);

    }
    public override void OnUpdateState()
    {
        FaceTarget();
    }

    private void FaceTarget() //ASegurarme que el enemigo enfoca al player en todo momento.
    {
        Vector3 directionToTarget = (controller.Target.transform.position - transform.position).normalized;
        directionToTarget.y = 0;
        transform.rotation = Quaternion.LookRotation(directionToTarget); //Transforma una dirección en una rotación.
    }

    public override void OnExitState()
    {
        
    }

    //Se ejecuta cuando SE TEMRINA la animación de atacar.
    private void OnFinishAttackAnimation()
    {
        //Se nos ha escapado el jugador de nuestro rnago de ataque...
        if(Vector3.Distance(transform.position, controller.Target.transform.position) > controller.AttackDistance)
        {
            controller.Anim.SetBool("attacking", false);
            controller.ChangeState(controller.ChaseState);
        }
    }

}
