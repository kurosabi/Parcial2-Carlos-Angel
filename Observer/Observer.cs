using linq.Torneo;
using System.Collections.Generic;
using System;

namespace observer.Observer
{
    public interface señal
    {
        #region Methods
        public void Agregar(Seleccion temp);
        public void Elimina(Seleccion temp);
        public void actualiza(Seleccion temp);
        #endregion Methods
    }
    
    public interface observerPartidos
    {
        #region Methods
        public void update(Partidos temp);
        #endregion Methods
    }

}