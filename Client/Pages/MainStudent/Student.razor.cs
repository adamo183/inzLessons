using inzLessons.Shared.Message;
using inzLessons.Shared.Reservation;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.MainStudent
{
    public partial class Student
    {

        MessageDTO message = new MessageDTO();
        List<ReservationDTO> reservationList = new List<ReservationDTO>();
        IEnumerable<DisplayReservationDTO> reservationToDisplay = new List<DisplayReservationDTO>();
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
        string Text = "";
        List<MessageDTO> messageList = new List<MessageDTO>();
        int UserId;

        async void GetMessageList(int id)
        {
            messageList = await messageServices.GetMessageInReservation(id);
            StateHasChanged();
        }

        public async void SendMessageFile()
        {
            if (message.File != null && message.File.Length > 0)
            {
                message.Text = "Dodano załącznik " + message.FileName;
                message.ReservationId = selectedReservation.FirstOrDefault().Id;
                message.FileName = "tmp";
                var status = await messageServices.InsertMessageInReservation(message);
                message.File = null;
                message.FileName = null;
                if (status)
                {
                    messageList = await messageServices.GetMessageInReservation(message.ReservationId);
                }
                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            UserId = await localStorage.GetItemAsync<int>("PersonelId");
            ReservationParams reservation = new ReservationParams();
            reservation.Start = DateTime.Now;
            reservation.End = reservation.Start.AddDays(25);
            reservationList = await reservationServices.GetStudentReservation(reservation);
            if (reservationList != null && reservationList.Count > 0)
            {
                reservationToDisplay = reservationList.Select(x => new DisplayReservationDTO()
                {
                    Day = x.Start.ToShortDateString(),
                    StartTime = x.Start.ToShortTimeString(),
                    EndTime = x.End.ToShortTimeString(),
                    IsOnline = x.IsOnline,
                    Student = String.Join(",", x.Students.Select(x => x.DisplayFullName).ToArray()),
                    Id = x.Id,
                    Description = x.Description
                }).ToList();
            }

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
                if (status)
                {
                    messageList = await messageServices.GetMessageInReservation(message.ReservationId);
                }
                StateHasChanged();
            }
        }

        public void DownloadFile(byte[] fileToDownload, string name)
        {
            BlazorDownloadFileService.DownloadFile(name, fileToDownload, "application/octet-stream");
        }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            long maxFileSize = 1024 * 1024 * 15;
            var upload = false;

            using var content = new MultipartFormDataContent();

            var fileToRead = e.GetMultipleFiles().FirstOrDefault();

            var fileContent = new StreamContent(fileToRead.OpenReadStream(maxFileSize));
            message.File = await fileContent.ReadAsByteArrayAsync();
            message.FileName = e.File.Name;
        }
    }
}
