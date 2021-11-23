using inzLessons.Shared.Message;
using inzLessons.Shared.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace inzLessons.Client.Pages.MainTeacher
{
    public partial class Teacher
    {
        public IList<DisplayReservationDTO> selectedReservation
        {
            get
            {
                return _selectedReservation;
            }
            set
            {
                if (value != _selectedReservation)
                {
                    _selectedReservation = value;
                    GetMessageList(value.FirstOrDefault().Id);
                }
            }
        }
        private IList<DisplayReservationDTO> _selectedReservation;

        List<ReservationDTO> reservationList = new List<ReservationDTO>();
        IEnumerable<DisplayReservationDTO> reservationToDisplay = new List<DisplayReservationDTO>();
        string Text = "";
        List<MessageDTO> messageList = new List<MessageDTO>();
        int UserId;

        async void GetMessageList(int id)
        {
            messageList = await messageServices.GetMessageInReservation(id);
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            UserId = await localStorage.GetItemAsync<int>("PersonelId");
            ReservationParams reservation = new ReservationParams();
            reservation.Start = DateTime.Now;
            reservation.End = reservation.Start.AddDays(25);
            reservationList = await reservationServices.GetTeacherReservation(reservation);
            reservationToDisplay = reservationList.Select(x => new DisplayReservationDTO()
            {
                Day = x.Start.ToShortDateString(),
                StartTime = x.Start.ToShortTimeString(),
                EndTime = x.End.ToShortTimeString(),
                IsOnline = x.IsOnline,
                Student = x.StudentName,
                Id = x.Id,
                Description = x.Description
            }).ToList();

            StateHasChanged();
        }

        public async void SendMessage()
        {
            if (!String.IsNullOrEmpty(Text))
            {
                MessageDTO message = new MessageDTO();
                message.Text = Text;
                message.ReservationId = selectedReservation.FirstOrDefault().Id;
                var status = await messageServices.InsertMessageInReservation(message);
            }
        }
    }
}
