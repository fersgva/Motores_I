using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curso202425
{
    [CreateAssetMenu(menuName ="WeaponSO_2425")]
    public class WeaponSO : ScriptableObject
    {
        public int municionCargador;
        public int municionRecamara;
        public float danho;
        public float distanciaAtaque;

        public enum TipoArma { Manual, Automatica, Melee};

        public TipoArma tipoArma;
    }

}
