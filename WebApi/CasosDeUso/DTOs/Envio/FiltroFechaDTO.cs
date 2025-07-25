﻿using CasosDeUso.InterfacesCasosUso;
using ExcepcionesPropias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasosDeUso.DTOs.Envio
{
    public class FiltroFechaDTO:IValidable
    {
        public DateTime? FInicio { get; set; }
        public DateTime? FFin { get; set; }
        public string? Estado { get; set; }
        public string? Email { get; set; }

        public void Validar()
        {
            if (FInicio == null && FFin == null)
            {
                throw new DatosInvalidosException("Debe ingresar al menos una fecha para filtrar.");
            }
            if(FFin != null && FInicio != null && FInicio > FFin)
            {
                throw new DatosInvalidosException("La fecha de inicio no puede ser superior a la de fin.");
            }
            if((Estado != null)&& !(Estado == "Finalizado" || Estado == "En_Proceso"))
            {
                throw new DatosInvalidosException("Estado no válido");
            }
            if (Email != null && string.IsNullOrEmpty(Email))
            {
                throw new DatosInvalidosException("El Email no puede quedar vacio.");
            }
        }
    }
}