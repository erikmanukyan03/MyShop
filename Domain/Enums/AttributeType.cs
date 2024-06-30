using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum AttributeType
    {
        [Display(Name = "General characteristics")]
        General,    
        [Display(Name = "Display")]
        Display,
        [Display(Name = "Camera")]
        Camera,
        [Display(Name = "Memory and Processor")]
        Memory_Proc,
        [Display(Name = "Network")]
        Network,
        [Display(Name = "Սնուցում")]
        Snucum,
        [Display(Name = "Other")]
        Other,
        [Display(Name = "Միակցիչներ")]
        Miakcichner,
        [Display(Name = "Միացություններ")]
        Miacutyunner,
        [Display(Name = "Memory")]
        Memory,

    }

}
