using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public abstract class ITriggerable : MonoBehaviour
    {
        public abstract void Triggered();
        public abstract void Untriggered();
    }
}
