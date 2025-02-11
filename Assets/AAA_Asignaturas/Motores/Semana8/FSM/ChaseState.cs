using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State<EnemyController>
{

    [SerializeField]
    private float timeBeforeBackToPatrol;

    private Coroutine coroutine;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        controller.Agent.stoppingDistance = controller.AttackDistance;
        controller.Agent.speed = controller.MaximumVelocity;
    }

    public override void OnUpdateState()
    {
        controller.Anim.SetFloat("velocity", controller.Agent.velocity.magnitude / controller.MaximumVelocity);
        //Sólo si el objetivo es alcanzable...
        if (!controller.Agent.pathPending && controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath()))
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
