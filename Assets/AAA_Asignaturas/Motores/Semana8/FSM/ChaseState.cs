using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State<EnemyController>
{
    [SerializeField]
    private float chaseVelocity;

    [SerializeField]
    private float timeBeforeBackToPatrol;

    private Coroutine coroutine;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        controller.Agent.isStopped = false;
        controller.Agent.speed = chaseVelocity;
        controller.Agent.stoppingDistance = controller.AttackDistance;
    }

    public override void OnUpdateState()
    {
        //Sólo si el objetivo es alcanzable...
        if(!controller.Agent.pathPending && controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath()))
        {
            StopMyCoroutine();

            controller.Agent.SetDestination(controller.Target.position);

            //No tengo calculos pendientses Y mi distancia hacia mi objetivo está por debajo de mi distancia de parada.
            if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
            {

                controller.ChangeState(controller.AttackState);
            }
        }
        else
        {
            coroutine ??= StartCoroutine(StopAndReturn());
        }
    }

    private void StopMyCoroutine()
    {


        StopAllCoroutines();
        coroutine = null;
    }

    private IEnumerator StopAndReturn()
    {
        yield return new WaitForSeconds(timeBeforeBackToPatrol);
        controller.ChangeState(controller.PatrolState);
    }

    public override void OnExitState()
    {
        StopMyCoroutine();
    }


}
