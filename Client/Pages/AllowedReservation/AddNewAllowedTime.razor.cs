using inzLessons.Shared.AllowedReservation;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.AllowedReservation
{
    public partial class AddNewAllowedTime
    {
        [Parameter]
        public DateTime Start { get; set; }

        [Parameter]
        public DateTime End { get; set; }

        [Parameter]
        public int MaxHourPerStudent { get; set; }

        [Parameter]
        public int Id { get; set; }

        AllowedReservationDTO model = new AllowedReservationDTO();
        bool WrongDateError = false;
        bool HourNotAvailable = false;

        protected override void OnParametersSet()
        {
            model.StartTime = Start;
            model.EndTime = End;
            model.MaxLessonTimePerStudent = MaxHourPerStudent;
            model.Id = Id;
        }

        protected override async Task OnInitializedAsync()
        {
        }

        private async void OnSubmit(AllowedReservationDTO modelSumbit)
        {
            HourNotAvailable = false;
            WrongDateError = false;

            if (modelSumbit.EndTime < modelSumbit.StartTime)
            {
                WrongDateError = true;
                StateHasChanged();
                return;
            }


            if (Id == 0)
            {
                if (!await allowedReservationServices.CheckAllowedReservationAvailable(model))
                {
                    HourNotAvailable = true;
                    StateHasChanged();
                    return;
                }
                await allowedReservationServices.AddAllowedTeacherHours(model);
            }
            else
            {
                await allowedReservationServices.EditAllowedTeacherHours(model);
            }

            DialogService.Close(modelSumbit);
        }
    }
}
