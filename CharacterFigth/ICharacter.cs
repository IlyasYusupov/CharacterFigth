using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterFigth
{
    public interface ICharacter
    {
        public void PhysicalAttack();

        public void MagicalAttack();

        public void BlockAttack();

        public void SelfHeal();
    }
}
