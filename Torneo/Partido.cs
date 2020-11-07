using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Partido
    {
        #region Properties  
        public Equipo EquipoLocal { get; set; }
        public Equipo EquipoVisitante { get; set; }
        public string EquipoGanador { get; set; }

        #endregion Properties

        #region Initialize
        public Partido(Seleccion EquipoLocal, Seleccion EquipoVisitante) 
        {
            this.EquipoLocal = new Equipo(EquipoLocal);
            this.EquipoVisitante = new Equipo(EquipoVisitante);
        }
        #endregion Initialize
        #region Methods
        private void CalcularExpulsiones()
        {
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 50).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            
            int max = random.Next(1,5);
            
            for(int i = 0; i < max; i++){
                int position = random.Next(JugadoresLocales.Count);
                String expulsadoLocal = JugadoresLocales[position];
                position = random.Next(JugadoresVisitantes.Count);
                String expulsadoVisitante = JugadoresVisitantes[position];
                EquipoLocal.ExpulsarJugador(expulsadoLocal);
                EquipoVisitante.ExpulsarJugador(expulsadoVisitante);
            }
        }

        private void CalcularResultado()
        {
            Random random = new Random();
            EquipoLocal.Goles = random.Next(0,6);
            EquipoVisitante.Goles = random.Next(0,6);
        }

        public string Resultado()
        {
            string resultado = "0 - 0";
            try
            {
                CalcularExpulsiones();
                CalcularResultado();
                CalcularSancionados();
                resultado = EquipoLocal.Goles.ToString() + " - " + EquipoVisitante.Goles.ToString();
                EquipoVisitante.Asistencias = EquipoVisitante.Goles;
                EquipoLocal.Asistencias = EquipoLocal.Goles;

                if(EquipoLocal.Goles > EquipoVisitante.Goles){

                    EquipoGanador = EquipoLocal.Seleccion.Nombre;

                }
                else if(EquipoVisitante.Goles > EquipoLocal.Goles){

                    EquipoGanador = EquipoVisitante.Seleccion.Nombre;
                }
                else{

                    EquipoGanador = "Empatados";
                }
            }
            catch(LoseForWException ex)
            {
                Console.WriteLine(ex.Message);
                EquipoLocal.Goles -= EquipoLocal.Goles;
                EquipoVisitante.Goles -= EquipoVisitante.Goles;
                if (ex.NombreEquipo == EquipoLocal.Seleccion.Nombre)
                {
                    EquipoVisitante.Goles += 3;
                    resultado = "0 - 3";
                    EquipoVisitante.Asistencias += 3;
                    EquipoGanador = EquipoVisitante.Seleccion.Nombre;
                }
                else
                {
                    EquipoLocal.Goles += 3;
                    resultado = "3 - 0";
                    EquipoLocal.Asistencias += 3;
                    EquipoGanador = EquipoLocal.Seleccion.Nombre;
                }
            }
            
            return resultado;
        }

        private void CalcularSancionados()
        {
            Random random = new Random();
            List<string> jugadoresVacios = Enumerable.Repeat(string.Empty, 50).ToList();
            List<String> JugadoresLocales = EquipoLocal.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();
            List<String> JugadoresVisitantes = EquipoVisitante.Seleccion.Jugadores.Select(j => j.Nombre).ToList().Concat(jugadoresVacios).ToList();

            int max = random.Next(0,8);
            for(int i = 0; i < max; i++){
                int position = random.Next(JugadoresLocales.Count);
                String sancionadoLocal = JugadoresLocales[position];
                position = random.Next(JugadoresVisitantes.Count);
                String sancionadoVisitante = JugadoresVisitantes[position];
                EquipoLocal.SancionarJugador(sancionadoLocal);
                EquipoVisitante.SancionarJugador(sancionadoVisitante);
            }
        }

        public int GolesLocal()
        {
            return EquipoLocal.Goles;
        }
        public int GolesVisitante()
        {
            return EquipoVisitante.Goles;
        }
        
        #endregion Methods

    }
}