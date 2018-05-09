using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities
{
    /// <summary>
    /// 所有业务实体都需要继承实现的接口规约
    /// </summary>
    public interface IEntity
    {
        string ID { get; set; }
        string Name { get; set; }
    }
}
