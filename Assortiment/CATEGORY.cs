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
    
    public partial class CATEGORY
    {
        public CATEGORY()
        {
            this.ASSORTIMENT = new HashSet<ASSORTIMENT>();
        }
    
        public int Id_category { get; set; }
        public string name_categ { get; set; }
        public string opisaniye { get; set; }
    
        public virtual ICollection<ASSORTIMENT> ASSORTIMENT { get; set; }
    }
}
