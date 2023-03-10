using System.Data;

namespace AwesomeDevEvents.API.Entities
{
    /// <summary>
    /// Classe
    /// </summary>
    public class DevEvent
    {
        /// <summary>
        /// Construtor do Evento
        /// </summary>               
        public DevEvent()
        {
            Speakers = new List<DevEventSpeaker>();
            IsDeleted = false;
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DevEventSpeaker> Speakers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void Update(string title, string description, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
