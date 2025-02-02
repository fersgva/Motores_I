using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    [SerializeField]
    private float timeBetweenAttacks;

    [SerializeField]
    private float baseAttackDamage;


    private float timer;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        timer = timeBetweenAttacks;

        controller.Agent.isStopped = true;
        controller.Agent.stoppingDistance = controller.AttackDistance;

    }
    public override void OnUpdateState()
    {
        controller.Agent.SetDestination(controller.Target.position);

        //Si tengo el player en rango de ataque,....
        if(!controller.Agent.pathPending && controller.Agent.remainingDistance < controller.Agent.stoppingDistance)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks)
            {
                Debug.Log("Hago daño!");
                timer = 0f;
            }
            
        }
        else
        {
            controller.ChangeState(controller.PatrolState);
        }
    }
    public override void OnExitState()
    {
        
    }

}
