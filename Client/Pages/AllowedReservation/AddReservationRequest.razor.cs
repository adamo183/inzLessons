using inzLessons.Shared.ReservationRequest;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.AllowedReservation
{
    public partial class AddReservationRequest
    {
        protected override async Task OnInitializedAsync()
        {
            UserId = await localStorage.GetItemAsync<int>("PersonelId");
        }

        [Parameter]
        public int AllowedHourId { get; set; }

        [Parameter]
        public DateTime Start { get; set; }

        [Parameter]
        public DateTime End { get; set; }

        [Parameter]
        public int MaxHourPerStudent { get; set; }

        int UserId;
        int maxLessonTime = 1;
        ReservationRequestDTO model = new ReservationRequestDTO();
        bool WrongDateError = false;
        bool ToLongLessong = false;

        protected override void OnParametersSet()
        {
            model.StartTime = Start;
            model.EndTime = End;
            maxLessonTime = MaxHourPerStudent;
        }

        private async void OnSubmit(ReservationRequestDTO requestDTO)
        {
            ToLongLessong = false;
            WrongDateError = false;

            if (model.EndTime < model.StartTime)
            {
                WrongDateError = true;
                StateHasChanged();
                return;
            }

            if ((model.EndTime - model.StartTime).TotalHours > maxLessonTime)
            {
                ToLongLessong = true;
                StateHasChanged();
                return;
            }

            requestDTO.UserId = UserId;
            requestDTO.AllowedReservationId = AllowedHourId;
            var status = allowedReservationServices.AddReservationRequest(requestDTO);
            DialogService.Close(model);

        }

    }
}
