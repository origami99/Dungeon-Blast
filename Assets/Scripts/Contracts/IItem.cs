using System.Collections.Generic;

namespace Assets.Scripts.Contracts
{
    public interface IItem<T>
    {
        T CurrentTier { get; set; }

        ICollection<T> Tiers { get; set; }
    }
}
