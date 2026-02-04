using APIGestionNotas.Models;
using APIGestionNotas.Managers;
using APIGestionNotas;

namespace APIGestionNotas
{
    public class Inicializador
    {
        private readonly INotaManager _notaManager;

        public Inicializador(INotaManager notaManager)
        {
            _notaManager = notaManager;
        }

        public void CargarDatosPrueba()
        {
            _notaManager.Create(new NotaDTO(0, "Comprar para la semana", "Comprar leche, pan integral, frutas, verduras y café. Revisar si falta arroz o fideos", new DateTime(2025, 11, 2), DateTime.Now));
            _notaManager.Create(new NotaDTO(1, "Reunión de trabajo", "Reunión con el equipo el jueves a las 10:00. Llevar avances del proyecto y dudas anotadas", new DateTime(2025, 10, 9), DateTime.Now));
            _notaManager.Create(new NotaDTO(2, "Ideas para el TP", "Pensar estructura del trabajo práctico, separar tareas y definir fechas de entrega", new DateTime(2025, 5, 8), DateTime.Now));
            _notaManager.Create(new NotaDTO(3, "Turno médico", "Turno con el clínico el neumonólogo 15 a las 9:30", new DateTime(2026, 1, 20), DateTime.Now));
            _notaManager.Create(new NotaDTO(4, "Películas pendientes", "Inception \n Interstellar \n El origen", new DateTime(2026, 2, 1), DateTime.Now));
            _notaManager.Create(new NotaDTO(5, "Recetas para navidad", "Vitel tone \n Ensalada de naranja y cebolla", new DateTime(2025, 12, 24), DateTime.Now));
            _notaManager.Create(new NotaDTO(6, "Nombre de perros", "- Luli \n- Lola \n- Magui \n- Gastón \n- Chimi", new DateTime(2026, 1, 18), DateTime.Now));
            _notaManager.Create(new NotaDTO(7, "Objetivos del mes", "Terminar el proyecto de la API, mejorar organización y practicar más C#", new DateTime(2026, 2, 3), DateTime.Now));
        }
    }
}
