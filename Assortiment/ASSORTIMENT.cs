//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assortiment
{
    using System;
    using System.Collections.Generic;
    
    public partial class ASSORTIMENT
    {
        public ASSORTIMENT()
        {
            this.OTCHETI = new HashSet<OTCHETI>();
        }
    
        public int Id_assorti { get; set; }
        public string name_assorti { get; set; }
        public int Id_category { get; set; }
        public string opisaniye_assorti { get; set; }
        public Nullable<decimal> price { get; set; }
        public string image { get; set; }
    
        public virtual ICollection<OTCHETI> OTCHETI { get; set; }
        public virtual CATEGORY CATEGORY { get; set; }
    }
}