using System.Collections.Generic;
using Newtonsoft.Json;
using observer.Observer;

namespace linq.Torneo
{
    public class Seleccion : Observer
    {
        #region Properties  
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("puntos")]
        public int PuntosTotales { get; set; }

        [JsonProperty("goles")]
        public int GolesTotales { get; set; }

        [JsonProperty("asistencias")]
        public int AsistenciasTotales { get; set; }

        [JsonProperty("jugadores")]
        public List<Jugador> Jugadores { get; set; }

        #endregion Properties

        #region Initialize

        #endregion Initialize

        #region Methods

        public void SistemaPuntos(Partido puntos){

            if(p.EquipoLocal.Seleccion.Nombre == Nombre){

                AsistenciasTotales += puntos.EquipoLocal.Asistencias;
                GolesTotales += puntos.EquipoLocal.Goles;

                if(Nombre == puntos.EquipoGanador){

                    PuntosTotales += 3;
                }

                else if("Empatados" == puntos.EquipoGanador){

                    PuntosTotales += 1;
                }
            }

            else if(puntos.EquipoVisitante.Seleccion.Nombre == Nombre){

                AsistenciasTotales += puntos.EquipoVisitante.Asistencias;
                GolesTotales += puntos.EquipoVisitante.Goles;

                if(Nombre == puntos.EquipoGanador){

                    PuntosTotales += 3;
                }

                else if("Empatados" == puntos.EquipoGanador){

                    PuntosTotales += 1;
                }
            }
        }
        #endregion Methods
    }
}