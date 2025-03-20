using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State<EnemyController>
{
    [SerializeField]
    private float tiempoEsperaPathNoEncontrado;

    private Coroutine coroutinee;


    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        controller.Agent.stoppingDistance = controller.DistanciaAtaque;

        controller.Agent.speed = controller.MaximumSpeed;
    }

    public override void OnUpdateState()
    {
        controller.Anim.SetFloat("velocity", (controller.Agent.velocity.magnitude / controller.MaximumSpeed));

        if (controller.Agent.CalculatePath(controller.Target.position, new NavMeshPath()))
        {
            StopWaiting();

            controller.Agent.SetDestination(controller.Target.position);
            if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
            {
                controller.ChangeState(controller.AttackState);
            }
        }
        else
        {
            //Sólo se hace si la corrutina es nula. (oPERADOR de asignación de fusión nula) (asignar un valor sólo si la variable esta a nulo)
            coroutinee ??= StartCoroutine(StopAndReturn());
        }
    }


    private IEnumerator StopAndReturn()
    {

        yield return new WaitForSeconds(tiempoEsperaPathNoEncontrado);
        controller.ChangeState(controller.PatrolState);
    }
    public override void OnExitState()
    {
        StopWaiting();
    }
    private void StopWaiting()
    {
        StopAllCoroutines();
        coroutinee = null;
    }


}
