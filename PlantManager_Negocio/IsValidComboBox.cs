using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantManager_Negocio
{
    public class IsValidComboBox : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string val = value as string;
            if (val != null && !val.Equals("Seleccionar"))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
