using System;
using System.Collections.Generic;
using System.Linq;
using Excepciones.CustomExceptions;

namespace linq.Torneo
{
    public class Equipo
    {
        #region Properties  
        public int Goles { get; set; }
        public int Asistencias { get; set; }
        public int Faltas { get; set; }
        public int TarjetasAmarillas { get; set; }
        public int TarjetasRojas { get; set; }
        public Seleccion Seleccion { get; set; }
        public bool EsLocal { get; set; }

        #endregion Properties

        #region Initialize
        public Equipo(Seleccion seleccion)
        {
            this.Seleccion = seleccion;
        }
        #endregion Initialize

        #region Methods
        public void ExpulsarJugador(string name)
        {
            try
            {
                Jugador jugadorExpulsado = Seleccion.Jugadores.First(j => j.Nombre == name);
                TarjetasRojas++;
                if (Seleccion.Jugadores.Count < 8)
                {
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
                Seleccion.Jugadores.Remove(jugadorExpulsado);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("No existe ese jugador para expulsarlo del equipo " + Seleccion.Nombre);
            }
            
        }

        public void SancionarJugador(string name){
            try{
                Jugador jugadorSancionado = Seleccion.Jugadores.First(j => j.Nombre == name);
                TarjetasAmarillas++;
                jugadorSancionado.Amarillas++;
                if(TarjetasAmarillas == 2){
                    Seleccion.Jugadores.Remove(jugadorSancionado);
                    TarjetasRojas++;
                    Console.WriteLine("Se explusa al jugador " + name + " por varias sanciones de tarjetas amarillas");
                }
                if(Seleccion.Jugadores.Count < 8){
                    LoseForWException ex = new LoseForWException(Seleccion.Nombre);
                    ex.NombreEquipo = Seleccion.Nombre;
                    throw ex;
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("El jugador no existe en el equipo " + Seleccion.Nombre + " para poder sancionarlo");
            }
        }
        #endregion Methods
    }
}